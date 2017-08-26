using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using System.Collections.Generic;
using d60.Cirqus.Views;
using d60.Cirqus.Views.ViewManagers;
using d60.Cirqus.Ntfs.Events;
using d60.Cirqus.Events;
using d60.Cirqus;
using Benefits.Domain.ViewModels;
using Microsoft.AspNet.SignalR;

[assembly: OwinStartup(typeof(Benefits.Web.Startup))]

namespace Benefits.Web
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            SetupCommandProcessor();
            app.MapSignalR();
        }

        private void SetupCommandProcessor()
        {
            var estimateViewModels = new InMemoryViewManager<BenefitEstimateViewModel>();
            estimateViewModels.Updated += x => Broadcast(x);
            var viewManagers = new List<IViewManager> { estimateViewModels };

            var eventstore = new NtfsEventStore(this.GetDataFolder());
            var processor = CommandProcessor.With()
                .EventStore(c => c.RegisterInstance(eventstore))
                .EventDispatcher(e => e.UseViewManagerEventDispatcher(viewManagers.ToArray()))
                .Create();

            TinyIoC.TinyIoCContainer.Current.Register<InMemoryViewManager<BenefitEstimateViewModel>>(estimateViewModels);
            TinyIoC.TinyIoCContainer.Current.Register<ICommandProcessor>(processor);
            TinyIoC.TinyIoCContainer.Current.Register<IEventStore>(eventstore);
            TinyIoC.TinyIoCContainer.Current.Register<List<IViewManager>>(viewManagers);
        }

        private string GetDataFolder()
        {
            var folder = this.GetType().Assembly.Location;
            folder = folder.Substring(0, folder.LastIndexOf("\\"));
            folder = System.IO.Path.Combine(folder, "EVENTSTORE_DATA");
            if (!System.IO.Directory.Exists(folder)) System.IO.Directory.CreateDirectory(folder);
            return folder;
        }
        
        public static void Broadcast(BenefitEstimateViewModel estimate)
        {
            var clients = GlobalHost.ConnectionManager.GetHubContext<Hubs.BenefitQuoteHub>().Clients;
            clients.Client(estimate.Id).updateEstimate(estimate);
        }
    }
}

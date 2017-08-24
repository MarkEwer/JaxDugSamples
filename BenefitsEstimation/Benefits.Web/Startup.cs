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

[assembly: OwinStartup(typeof(Benefits.Web.Startup))]

namespace Benefits.Web
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            SetupCommandProcessor(app);
            app.MapSignalR();
        }

        private void SetupCommandProcessor(IAppBuilder app)
        {
            var _view = new InMemoryViewManager<BenefitEstimateViewModel>();
            var viewManagers = new List<IViewManager> { _view };
            var eventstore = new NtfsEventStore(this.GetDataFolder());
            var processor = CommandProcessor.With()
                .EventStore(c => c.RegisterInstance(eventstore))
                .EventDispatcher(e => e.UseViewManagerEventDispatcher(viewManagers.ToArray()))
                .Create();

            TinyIoC.TinyIoCContainer.Current.Register<ICommandProcessor>(processor).AsSingleton();
            TinyIoC.TinyIoCContainer.Current.Register<IEventStore>(eventstore).AsSingleton();
            TinyIoC.TinyIoCContainer.Current.Register<List<IViewManager>>(viewManagers).AsSingleton();
        }

        private string GetDataFolder()
        {
            var folder = this.GetType().Assembly.Location;
            folder = folder.Substring(0, folder.LastIndexOf("\\"));
            folder = System.IO.Path.Combine(folder, "EVENTSTORE_DATA");
            if (!System.IO.Directory.Exists(folder)) System.IO.Directory.CreateDirectory(folder);
            return folder;
        }
    }
}

using Benefits.Domain.Commands;
using d60.Cirqus;
using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Benefits.Web.Hubs
{
    public class BenefitQuoteHub : Hub
    {
        private ICommandProcessor _processor {  get { return TinyIoC.TinyIoCContainer.Current.Resolve<ICommandProcessor>(); } }

        public void AddEmployee(AddEmployeeToBenefitsEstimate cmd)
        {
            var result = this._processor.ProcessCommand(cmd);
        }

        public void SetSalary(SetEmployeeSalary cmd)
        {
            var result = this._processor.ProcessCommand(cmd);
        }

        public void AddSpouse(AddSpouseToBenefitsEstimate cmd)
        {
            var result = this._processor.ProcessCommand(cmd);
        }

        public void AddDependent(AddDependentToBenefitsEstimate cmd)
        {
            var result = this._processor.ProcessCommand(cmd);
        }

        public void RemoveSpouse(RemoveSpouseToBenefitsEstimate cmd)
        {
            var result = this._processor.ProcessCommand(cmd);
        }

        public void RemoveDependent(RemoveDependentToBenefitsEstimate cmd)
        {
            var result = this._processor.ProcessCommand(cmd);
        }
    }
}
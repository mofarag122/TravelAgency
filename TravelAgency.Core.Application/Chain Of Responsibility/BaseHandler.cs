using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgency.Core.Application.Chain_Of_Responsibility
{
    public abstract class BaseHandler<TResult> : IHandler<TResult> where TResult : class
    {
        private protected IHandler<TResult>? handler;
        public abstract TResult Handle(object? TParameter);
     
        public void SetNext(IHandler<TResult> handler)
        {
            this.handler = handler; 
        }
    }
}

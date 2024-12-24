using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgency.Core.Application.Chain_Of_Responsibility
{
    public interface IHandler<TResult>
    {
        public void SetNext(IHandler<TResult> handler);

        public TResult Handle(object? TParameter);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgency.Core.Application.Exceptions
{
    public class BadRequest : ApplicationException
    {
        public BadRequest(string Description):base(Description) 
        {
        }
       
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgency.Core.Application.Exceptions
{
    public class NotFound : ApplicationException
    {
        public NotFound(string name, object key)
          : base($"{name} With {key} is not found")
        {

        }
    }
}

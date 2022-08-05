using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardgamesEShopManagement.Domain.Exceptions
{
    public class GenericItemException : Exception
    {
        public GenericItemException(string message) : base(message)
        {

        }
    }
}

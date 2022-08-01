using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardgamesEShopManagement.Domain.Models
{
    internal class Address
    {
        public string AddressDetails { get; set; }      
        public string City { get; set; }
        public string County { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
    }
}

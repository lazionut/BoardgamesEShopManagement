using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardgamesEShopManagement.Domain.Models
{
    internal class Address
    {
        private string _address;
        private string _city;
        private string _county;
        private string _country;
        private string _phone;

        public Address(string address, string city, string county, string country, string phone)
        {
            _address = address;
            _city = city;
            _county = county;
            _country = country;
            _phone = phone;
        }
    }
}

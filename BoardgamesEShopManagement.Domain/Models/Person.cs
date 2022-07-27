using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardgamesEShopManagement.Domain.Models
{
    internal class Person
    {
        private readonly int _addressId;
        private readonly int _personId;
        private readonly string _firstName;
        private readonly string _lastName;
        private string _email;

        public Person(int addressId, int personId, string firstName, string lastName, string email)
        {
            _addressId = addressId;
            _personId = personId;
            _firstName = firstName;
            _lastName = lastName;
            _email = email;
        }

        public int PersonId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}

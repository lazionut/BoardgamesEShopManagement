using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

using BoardgamesEShopManagement.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace BoardgamesEShopManagement.Application.Addresses.Commands.UpdateAddress
{
    public class UpdateAddressRequest : IRequest<Address>
    {
        public int AddressId { get; set; }
        public string AddressDetails { get; set; } = null!;
        public string AddressCity { get; set; } = null!;
        public string AddressCounty { get; set; } = null!;
        public string AddressCountry { get; set; } = null!;
        public string AddressPhone { get; set; } = null!;
        public Account Account { get; set; } = null!;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardgamesEShopManagement.Application.Models
{
    public class VideogameFactory : IProductFactory
    {
        public IProduct CreateProduct(string name, string description, decimal price)
        {
            return new Videogame
            {
                Name = name,
                Description = description,
                Price = price,
            };
        }
    }
}

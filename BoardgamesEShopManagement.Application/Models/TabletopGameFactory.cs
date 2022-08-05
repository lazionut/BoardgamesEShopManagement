using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardgamesEShopManagement.Application.Models
{
    public class TabletopGameFactory : IProductFactory
    {
        public IProduct CreateProduct(string name, string description, decimal price)
        {
            return new TabletopGame
            {
                Name = name,
                Description = description,
                Price = price,
            };
        }
    }
}

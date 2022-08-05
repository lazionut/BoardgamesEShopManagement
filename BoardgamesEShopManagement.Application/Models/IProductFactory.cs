using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardgamesEShopManagement.Application.Models
{
    public interface IProductFactory
    {
        IProduct CreateProduct(string name, string description, decimal price);
    }
}

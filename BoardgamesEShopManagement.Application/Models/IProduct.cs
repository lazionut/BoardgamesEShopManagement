using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardgamesEShopManagement.Application.Models
{
    public interface IProduct
    {
        string Name { get; set; }
        string Description { get; set; }
        decimal Price { get; set; }
        void Play();
    }
}

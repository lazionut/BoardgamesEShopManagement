using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardgamesEShopManagement
{
    internal class Product : ICloneable
    {
        public int ProductId { get; set; }
        public string ProductImage { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public decimal ProductPrice { get; set; }

        public Product()
        {

        }

        public Product(string productImage, string productName, string productDescription, decimal productPrice)
        {
            ProductImage = productImage;
            ProductName = productName;
            ProductDescription = productDescription;
            ProductPrice = productPrice;
        }

        public object Clone()
        {
            return new Product(ProductImage, ProductName, ProductDescription, ProductPrice);
        }

        public string ToString(bool option)
        {
            if (option)
            { return ProductName + " " + ProductDescription; }
            else
            { return ProductName; }
        }

        public string ToString(bool option, decimal price)
        {
            if (option == true && price != 0)
            { return price.ToString(); }
            else
            { return price.ToString(); }
        }

        public override string ToString()
        {
            return $"{ProductId} - {ProductName} -> {ProductDescription} - ({ProductPrice}))";
        }
    }
}

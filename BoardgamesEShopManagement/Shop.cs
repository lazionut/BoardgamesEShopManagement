using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardgamesEShopManagement
{
    internal class Shop : IEnumerable<Product>
    {
        private List<Product> _products = new List<Product>();

        public void Add (Product product)
        {
            _products.Add(product);
        }

        public IEnumerator<Product> GetEnumerator()
        {
            return new ProductEnumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public Product this[int index] => _products[index];
        public int Count => _products.Count;
    }
}

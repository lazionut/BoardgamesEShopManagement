using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardgamesEShopManagement
{
    internal class ProductEnumerator : IEnumerator<Product>
    {
        private Shop _shop;
        private int _index;

        public ProductEnumerator(Shop shop)
        {
            _shop = shop;
            _index = -1;
        }
        public Product Current => _shop[_index];

        object IEnumerator.Current => Current;

        public void Dispose()
        {
            Console.WriteLine("Not implemented yet");
        }

        public bool MoveNext()
        {
            ++_index;
            return _index < _shop.Count;
        }

        public void Reset()
        {
            _index = -1;
        }
    }
}

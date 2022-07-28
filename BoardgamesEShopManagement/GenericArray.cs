using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardgamesEShopManagement
{
    internal class GenericArray<T>
    {
        private readonly T[] _elements;
        private int _currentIndex = -1;
        private readonly int _maxSize;

        public GenericArray(int maxSize)
        {
            _maxSize = maxSize;
            _elements = new T[maxSize];
        }

        public int Count => _currentIndex + 1;

        public void AddElement(T element)
        {
            if (_currentIndex > _maxSize - 1)
                throw new InvalidOperationException("The array is full!");

            _elements[++_currentIndex] = element;
        }

        public T GetElement(int index)
        {
            ValidateIndex(index);

            return _elements[index];
        }

        public void SetElement(T element, int index)
        {
            ValidateIndex(index);

            _elements[index] = element;
        }

        public void SwapElements(int initialIndex, int targetIndex)
        {
            ValidateIndex(initialIndex);
            ValidateIndex(targetIndex);

            T aux = _elements[initialIndex];
            _elements[initialIndex] = _elements[targetIndex];
            _elements[targetIndex] = aux;
        }

        public void SwapElements(T initialElement, T targetElement)
        {
            int initialIndex = -1;
            int targetIndex = -1;

            for (int index = 0; index <= _currentIndex; ++index)
            {
                if (initialElement.Equals(_elements[index]))
                {
                    initialIndex = index;
                }
                if (targetElement.Equals(_elements[index]))
                {
                    targetIndex = index;
                }
            }

            SwapElements(initialIndex, targetIndex);
        }

        public void SwapElements(T element, int targetIndex)
        {
            int initialIndex = -1;

            for (int index = 0; index <= _currentIndex; ++index)
            {
                if (element.Equals(_elements[index]))
                {
                    initialIndex = index;
                }
            }

            SwapElements(initialIndex, targetIndex);
        }

        private void ValidateIndex(int index)
        {
            if (index > _maxSize - 1 || index == -1) throw new InvalidOperationException("Invalid index!");
        }
    }
}

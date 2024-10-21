using System;
using System.Collections.Generic;

namespace SortingApp
{
    public class SortingContext
    {
        private ISortingStrategy _strategy;

        public void SetStrategy(ISortingStrategy strategy)
        {
            _strategy = strategy;
        }

        public void Sort<T>(List<T> list) where T : IComparable
        {
            _strategy.Sort(list);
        }
    }
}

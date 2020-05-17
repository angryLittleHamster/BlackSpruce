using System;
using System.Collections.Generic;
using System.Text;

namespace BlackSpruce.Shared.Patterns.CompositePattern
{
    public class CompositeBuilder<T>
    {
        private T _currentItem;

        public CompositeBuilder(T rootItem)
        {
            this.RootItem = rootItem;
            this._currentItem = this.RootItem;
        }

        public T RootItem { get; }

        
    }
}

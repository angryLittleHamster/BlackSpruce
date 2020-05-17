using System;
using System.Collections.Generic;
using System.Text;

namespace BlackSpruce.Shared.Patterns.CompositePattern
{
    public abstract class CompositeItem<T, TResult>
    {
        public CompositeItem(T item)
        {
            this.Item = item;
        }

        public virtual T Item { get; }

        public abstract TResult PrimaryOperation();
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace BlackSpruce.Shared.Patterns.CompositePattern
{
    public abstract class NodeItem<T, TResult> : CompositeItem<T, TResult>
    {
        public NodeItem(T item) : base(item)
        {
        }

        
    }
}

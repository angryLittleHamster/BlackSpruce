using System;
using System.Collections.Generic;
using System.Text;

namespace BlackSpruce.Shared.Patterns.CompositePattern
{
    public abstract class ComponentItem<T, TResult> : CompositeItem<T, TResult>
    {
        public ComponentItem(T item) : base(item)
        {
            
        }

        public IList<CompositeItem<T, TResult>> Items { get; } = new List<CompositeItem<T, TResult>>();

        public void Add(CompositeItem<T, TResult> component)
        {
            this.Items.Add(component);
        }

        public void Remove(CompositeItem<T, TResult> component)
        {
            this.Items.Remove(component);
        }



    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BlackSpruce.Shared.Patterns.SpecificationPattern.Interfaces
{
    public interface IGenericSpecification<T>
    {
        Expression<Func<T, bool>> Expression { get; }

        bool IsSatisfiedBy(T entity);

    }
}

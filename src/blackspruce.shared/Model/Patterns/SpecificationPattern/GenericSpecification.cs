using BlackSpruce.Shared.Patterns.SpecificationPattern.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BlackSpruce.Shared.Patterns.SpecificationPattern
{
    public class GenericSpecification<T> : IGenericSpecification<T>
    {
        public GenericSpecification(Expression<Func<T,bool>> expression)
        {
            Expression = expression;
        }

        public Expression<Func<T, bool>> Expression { get;  }

        public virtual bool IsSatisfiedBy(T entity)
        {
            return Expression.Compile().Invoke(entity);
        }

    }
}

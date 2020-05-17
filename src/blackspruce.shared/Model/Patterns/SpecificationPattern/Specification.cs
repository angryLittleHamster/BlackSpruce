using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BlackSpruce.Shared.Patterns.SpecificationPattern
{
    public abstract class Specification<T>
    {
        public bool IsSatisfiedBy(T entity)
        {
            Func<T, bool> predicate = ToExpression().Compile();
            return predicate(entity);
        }

        public static readonly Specification<T> All = new IdentitySpecification<T>();

        public abstract Expression<Func<T, bool>> ToExpression();


        //helper functions
        public Specification<T> Not()
        {
            return new NotSpecification<T>(this);
        }
        public Specification<T> Or(Specification<T> specification)
        {
            if (this == All || specification == All)
            {
                return All;   // true || x == true
            }
            return new OrSpecification<T>(this, specification);
        }
        public Specification<T> And(Specification<T> specification)
        {
            if (this == All)
            {
                return specification; //x && true == x
            }
            if (specification == All)
            {
                return this; // true && x == x
            }
            return new AndSpecification<T>(this, specification);
        }
        
    }

    internal sealed class IdentitySpecification<T> : Specification<T>
    {
        public override Expression<Func<T, bool>> ToExpression() => x => true;
    }

    internal sealed class NotSpecification<T> : Specification<T>
    {
        private readonly Specification<T> _specification;
        

        public NotSpecification(Specification<T> specification)
        {
            _specification = specification;
        }

        public override Expression<Func<T, bool>> ToExpression()
        {
            Expression<Func<T, bool>> expression = _specification.ToExpression();

            UnaryExpression notExpression = Expression.Not(expression.Body);

            return Expression.Lambda<Func<T, bool>>(notExpression, expression.Parameters.Single());
        }
    }

    internal sealed class OrSpecification<T> : Specification<T>
    {
        private readonly Specification<T> _left;
        private readonly Specification<T> _right;

        public OrSpecification(Specification<T> left, Specification<T> right)
        {
            _left = left;
            _right = right;
        }

        public override Expression<Func<T, bool>> ToExpression()
        {
            Expression<Func<T, bool>> leftExpression = _left.ToExpression();
            Expression<Func<T, bool>> rightExpression = _right.ToExpression();
            //https://stackoverflow.com/questions/9778749/expression-lambda-variable-x-of-type-referenced-from-scope-but-it-is-n
            InvocationExpression invokedExpr = Expression.Invoke(rightExpression, leftExpression.Parameters);
            BinaryExpression orExpression = Expression.OrElse(leftExpression.Body, invokedExpr);

            return Expression.Lambda<Func<T, bool>>(orExpression, leftExpression.Parameters);
        }
    }

    internal sealed class AndSpecification<T> : Specification<T>
    {
        private readonly Specification<T> _left;
        private readonly Specification<T> _right;

        public AndSpecification(Specification<T> left, Specification<T> right)
        {
            _left = left;
            _right = right;
        }

        public override Expression<Func<T, bool>> ToExpression()
        {
            Expression<Func<T, bool>> leftExpression = _left.ToExpression();
            Expression<Func<T, bool>> rightExpression = _right.ToExpression();

            InvocationExpression invokedExpr = Expression.Invoke(rightExpression, leftExpression.Parameters);
            BinaryExpression andExpression = Expression.AndAlso(leftExpression.Body, invokedExpr);

            return Expression.Lambda<Func<T, bool>>(andExpression, leftExpression.Parameters);
        }
    }
}

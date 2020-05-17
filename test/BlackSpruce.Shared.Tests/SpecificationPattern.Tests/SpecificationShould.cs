using BlackSpruce.Shared.Patterns.SpecificationPattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BlackSpruce.Shared.Tests.SpecificationPatternTests
{
    public class SpecificationShould
    {
        private static List<ValueTestClass> _seedList()
        {
            //Arrange
            return new List<ValueTestClass>() {
                new ValueTestClass(Int32.MinValue),
                new ValueTestClass(-1),
                new ValueTestClass(1),
                new ValueTestClass(2),
                new ValueTestClass(3),
                new ValueTestClass(4),
                new ValueTestClass(5),
                new ValueTestClass(6),
                new ValueTestClass(10),
                new ValueTestClass(20),
                new ValueTestClass(30),
                new ValueTestClass(40),
                new ValueTestClass(50),
                new ValueTestClass(60),
                new ValueTestClass(100),
                new ValueTestClass(200),
                new ValueTestClass(300),
                new ValueTestClass(400),
                new ValueTestClass(500),
                new ValueTestClass(600),
                new ValueTestClass(Int32.MaxValue)
            };
        }

        [Fact]
        public void HandleChildClassSpecifications()
        {
            var expectedSpec10Resuls = 8;
            var expectedSpec100Results = 6;
            IList<ValueTestClass> listOfIntegers = _seedList();
            var specLessThan10 = new ValueLessThan10ThresholdSpecification();
            var specGreaterThan100 = new ValueGreaterThan100ThresholdSpecification();

            //Action
            var spec10Results = listOfIntegers.AsQueryable().Where(specLessThan10.ToExpression()).ToList();
            var spec100Results = listOfIntegers.AsQueryable().Where(specGreaterThan100.ToExpression()).ToList();

            //Assert
            Assert.Equal(expectedSpec10Resuls, spec10Results.Count());
            Assert.Equal(expectedSpec100Results, spec100Results.Count());

        }

        [Fact]
        public void HandleOrSpecifications()
        {
            var expectedResults = 14;
            IList<ValueTestClass> listOfIntegers = _seedList();
            var spec = Specification<ValueTestClass>.All;
            spec = spec.And(new ValueLessThan10ThresholdSpecification());
            spec = spec.Or(new ValueGreaterThan100ThresholdSpecification());

            
            //Action
            var orExpression = spec.ToExpression();
            var results = listOfIntegers.AsQueryable().Where(orExpression.Compile()).ToList();

            //Assert
            Assert.Equal(expectedResults, results.Count());

        }

        [Fact]
        public void HandleAndSpecifications()
        {
            var expectedResults = 6;
            IList<ValueTestClass> listOfIntegers = _seedList();
            var spec = Specification<ValueTestClass>.All;
            spec = spec.And(new ValueLessThan10ThresholdSpecification());
            spec = spec.And(new ValueGreaterThanNegativeOneSpecification());


            //Action
            var orExpression = spec.ToExpression();
            var results = listOfIntegers.AsQueryable().Where(orExpression.Compile()).ToList();

            //Assert
            Assert.Equal(expectedResults, results.Count());

        }


    }

    internal class ValueTestClass
    {
        public ValueTestClass(long someValue)
        {
            SomePropetryValue = someValue;
        }
        public long SomePropetryValue { get; set; }
    }

    internal sealed class ValueLessThan10ThresholdSpecification : Specification<ValueTestClass>
    {
        public override Expression<Func<ValueTestClass, bool>> ToExpression() => exceedsThreshold => exceedsThreshold.SomePropetryValue < 10;
    }
    internal sealed class ValueGreaterThanNegativeOneSpecification : Specification<ValueTestClass>
    {
        public override Expression<Func<ValueTestClass, bool>> ToExpression() => exceedsThreshold => exceedsThreshold.SomePropetryValue > -1;
    }

    internal sealed class ValueGreaterThan100ThresholdSpecification : Specification<ValueTestClass>
    {
        public override Expression<Func<ValueTestClass, bool>> ToExpression() => exceedsThreshold => exceedsThreshold.SomePropetryValue > 100;
    }
}

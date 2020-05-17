using BlackSpruce.Shared.Patterns.SpecificationPattern;
using BlackSpruce.Shared.Patterns.SpecificationPattern.Interfaces;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BlackSpruce.Shared.Tests.SpecificationPatternTests
{
    public class GenericSpecificationShould
    {
        [Theory]
        [InlineData(0,10,true)]
        [InlineData(100, 10, false)]
        [InlineData(10, 10, true)]
        [InlineData(Int64.MinValue, 10, true)]
        [InlineData(Int64.MaxValue, 10, false)]
        [InlineData(Int32.MinValue, 10, true)]
        [InlineData(Int32.MaxValue, 10, false)]
        public void AddExpressionToSpecificationtoBeSatisified(long propertyValue, long specificationValue, bool expectedResult)
        {
            //Arrange
            var aTestClassWithValue = new TestClass() { SomeProperyValue = propertyValue };
            IGenericSpecification<TestClass> specification = new GenericSpecification<TestClass>(x => x.SomeProperyValue <= specificationValue);
            
            //Action
            var actualresult = specification.IsSatisfiedBy(aTestClassWithValue);

            //Assert
            Assert.True(specification is IGenericSpecification<TestClass>);
            Assert.Equal(expectedResult, actualresult);
        }

    }

    internal class TestClass
    {
        public long SomeProperyValue { get; set; }
    }
}

using BlackSpruce.Shared.Model.Patterns.StrategyPattern.RetailTaxStrategies;
using System.Runtime.InteropServices.ComTypes;
using Xunit;

namespace BlackSpruce.Shared.Tests.StrategyPatternTests.RetailTaxStrategiesTests
{
    public class RetailTaxStrategiesShould
    {
        [Theory]
        [InlineData(0.0, 0.0)]
        [InlineData(1.00, 0.15)]
        [InlineData(2.33, 0.3495)]
        public void CalculateCorrectBaseHarmonizedSalesTax(decimal purchaseAmount, decimal expectedResult)
        {
            //Arrange
            var purchaseItem = new Item(purchaseAmount);
            var taxStrategy = new HarmonizedRetailSalesTaxStrategy<Item>();
            //Action
            var sut = taxStrategy.CalculateRetailSalesTaxFor(purchaseItem);
            //Assert
            Assert.Equal(expectedResult, sut);
            var expectedStr = expectedResult.ToString("C");
            Assert.Equal(expectedStr, sut.ToString("C"));
        }
        [Fact]
        public void ThrowExceptionForInvalidItemAmountForHarmonizedSalesTax()
        {//Arrange
            var purchaseItem = new Item(-100.00M);
            var taxStrategy = new HarmonizedRetailSalesTaxStrategy<Item>();
            //Action
            //Assert
            Assert.Throws<RetailTaxCalculatorException>(() => taxStrategy.CalculateRetailSalesTaxFor(purchaseItem));

        }
        [Theory]
        [InlineData(0.07, 0.05, true, 0.0, 0.0)]
        [InlineData(0.07, 0.05, true, 1.00, 0.1235)]
        [InlineData(0.07, 0.05, true, 2.33, 0.287755)]
        [InlineData(0.07, 0.05, false, 0.0, 0.0)]
        [InlineData(0.07, 0.05, false, 1.00, 0.12)]
        [InlineData(0.07, 0.05, false, 2.33, 0.2796)]
        public void CalculateCorrectBaseNonHarmonizedSalesTax(decimal localRetailSalesTax, decimal nationalRetailSalesTax, bool localCompoundsNationalTax,
                                                                decimal purchaseAmount, decimal expectedResult)
        {
            //Arrange
            var purchaseItem = new Item(purchaseAmount);
            var taxStrategy = new NonHarmonizedRetailSalesTaxStrategy<Item>(localRetailSalesTax, nationalRetailSalesTax, localCompoundsNationalTax);

            //Action
            var sut = taxStrategy.CalculateRetailSalesTaxFor(purchaseItem);
            //Assert
            Assert.Equal(expectedResult, sut);
            var expectedStr = expectedResult.ToString("C");
            Assert.Equal(expectedStr, sut.ToString("C"));
        }
        [Theory]
        [InlineData(0.07, 0.05, true)]
        [InlineData(0.07, 0.05, false)]
        public void ThrowExceptionForInvalidItemAmountForNonHarmonizedSalesTax(decimal localRetailSalesTax, decimal nationalRetailSalesTax, bool localCompoundsNationalTax)
        {//Arrange
            var purchaseItem = new Item(-100.00M);
            var taxStrategy = new NonHarmonizedRetailSalesTaxStrategy<Item>(localRetailSalesTax, nationalRetailSalesTax, localCompoundsNationalTax);
            //Action
            //Assert
            Assert.Throws<RetailTaxCalculatorException>(() => taxStrategy.CalculateRetailSalesTaxFor(purchaseItem));

        }
    }


    internal class Item : ITaxableItem
    {
       
        public Item(decimal amount)
        {
            AmountBeforeTaxes = amount;
        }

        public decimal AmountBeforeTaxes { get; set; }
        public decimal TaxableAmount { get => AmountBeforeTaxes; }

             
    }
}

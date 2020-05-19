using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackSpruce.Shared.Model.Patterns.StrategyPattern.RetailTaxStrategies
{
    public class NonHarmonizedRetailSalesTaxStrategy<T> : IRetailSalesTaxStrategy<T>
        where T: ITaxableItem
    {
        private bool _localCompoundsNationalTaxAmount;
        public NonHarmonizedRetailSalesTaxStrategy(decimal localRetailSalesTax, decimal nationalRetailSalesTax, bool localCompoundsNationalTaxAmount = true)
        {
            _localCompoundsNationalTaxAmount = localCompoundsNationalTaxAmount;
            LocalRetailSalesTax = localRetailSalesTax;
            NationalRetailSalesTax = nationalRetailSalesTax;
        }

        public decimal LocalRetailSalesTax { get; private set; }
        public decimal NationalRetailSalesTax { get; private set; }
       
        public decimal CalculateRetailSalesTaxFor(T item)
        {
            if (item.TaxableAmount == 0.00M)
            {
                return 0.00M;
            }
            if (item.TaxableAmount < 0.0M)
            {
                throw new RetailTaxCalculatorException($"Invalid Taxable Amount: {item.TaxableAmount:C} for {nameof(LocalRetailSalesTax)}: {LocalRetailSalesTax} and for {nameof(NationalRetailSalesTax)}: {NationalRetailSalesTax} ");
                
            }
            else
            {
                if (_localCompoundsNationalTaxAmount)
                {
                    var nationalTaxPortion = item.TaxableAmount * NationalRetailSalesTax;
                    var localTaxPortion = (item.TaxableAmount + nationalTaxPortion) * LocalRetailSalesTax;
                    
                    return localTaxPortion + nationalTaxPortion;

                }
                else
                {
                    return (item.TaxableAmount * NationalRetailSalesTax) + (item.TaxableAmount * LocalRetailSalesTax);
                }

            }
            
            
        }
    }
}

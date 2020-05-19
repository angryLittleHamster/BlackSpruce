using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackSpruce.Shared.Model.Patterns.StrategyPattern.RetailTaxStrategies
{
    public class HarmonizedRetailSalesTaxStrategy<T> : IRetailSalesTaxStrategy<T> 
        where T: ITaxableItem
    {
        public HarmonizedRetailSalesTaxStrategy(decimal retailSalesTax = 0.15M)
        {
            RetailSalesTax = retailSalesTax;
        }
        

        public decimal RetailSalesTax { get; private set; }

        public virtual decimal CalculateRetailSalesTaxFor(T item)
        {
            if (item.TaxableAmount < 0.0M)
            {
                throw new RetailTaxCalculatorException($"Invalid Taxable Amount: {item.TaxableAmount:C} for {nameof(RetailSalesTax)}: {RetailSalesTax}");

            }
            if (item.TaxableAmount == 0.00M)
            {
                return 0.00M;
            }
            else
            {
                return item.TaxableAmount * RetailSalesTax;
            }
        }
    }
}

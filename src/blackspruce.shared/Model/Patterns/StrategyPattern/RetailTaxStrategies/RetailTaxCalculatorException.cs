using System;

namespace BlackSpruce.Shared.Model.Patterns.StrategyPattern.RetailTaxStrategies
{
    public class RetailTaxCalculatorException : ArgumentException
    {
        public RetailTaxCalculatorException(string message)
            :base(message)
        {

        }
        
    }
}

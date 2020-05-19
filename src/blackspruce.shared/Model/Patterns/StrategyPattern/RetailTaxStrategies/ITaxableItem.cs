using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackSpruce.Shared.Model.Patterns.StrategyPattern.RetailTaxStrategies
{
    public interface ITaxableItem
    {
        decimal TaxableAmount { get; }
    }
}

namespace BlackSpruce.Shared.Model.Patterns.StrategyPattern.RetailTaxStrategies
{
    public interface IRetailSalesTaxStrategy<T> where T : ITaxableItem
    {
        decimal CalculateRetailSalesTaxFor(T item);
    }
}

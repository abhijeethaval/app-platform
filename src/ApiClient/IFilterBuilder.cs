namespace ApiClient
{
    internal interface IFilterBuilder
    {
        void Append(LogicalOperator @operator);
        void Append<TProp>(string propertyName, TProp value, FilterOperator @operator);
        void Append<T>(Filter<T> segments);
        void Append<T>(FilterGroup<T> segments);
    }
}

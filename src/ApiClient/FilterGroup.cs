namespace ApiClient
{
    public class FilterGroup<T> : Filter<T>
    {
        internal override void Visit(IFilterBuilder filterBuilder)
        {
            filterBuilder.Append(this);
        }
    }
}

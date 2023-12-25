namespace ApiClient
{
    public abstract class FilterSegment
    {
        internal abstract void Visit(IFilterBuilder filterBuilder);
    }
}

namespace ApiClient
{
    public static class ApiFilterExtensions
    {
        public static string AsApiFilter<T>(this Filter<T>.LogicBuilder logicBuilder)
        {
            var filterBuilder = new ApiFilterBuilder();
            logicBuilder.ParenFilter.Visit(filterBuilder);
            return filterBuilder.ToString();
        }
    }
}

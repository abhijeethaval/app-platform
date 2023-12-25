namespace ApiClient
{
    public enum LogicalOperator
    {
        And,
        Or,
    }

    internal class LogicalOperatorSegment : FilterSegment
    {
        private LogicalOperator @operator;

        public LogicalOperatorSegment(LogicalOperator @operator)
        {
            this.@operator = @operator;
        }

        public LogicalOperator Operator { get; set; }

        internal override void Visit(IFilterBuilder filterBuilder)
        {
            filterBuilder.Append(@operator);
        }
    }
}

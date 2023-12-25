using System;
using System.Linq.Expressions;

namespace ApiClient
{
    internal enum FilterOperator
    {
        EqualTo,
        NotEqualTo,
        GreaterThan,
        LessThan,
    }

    internal class ConditionSegment<T, TProp> : FilterSegment
    {
        private Expression<Func<T, TProp>> propertyExpression;
        private TProp value;
        private FilterOperator op;

        public ConditionSegment(Expression<Func<T, TProp>> propertyExpression, TProp value, FilterOperator op)
        {
            this.propertyExpression = propertyExpression;
            this.value = value;
            this.op = op;
        }

        internal override void Visit(IFilterBuilder filterBuilder)
        {
            filterBuilder.Append(PropertyInfo<T, TProp>.GetPropertyName(this.propertyExpression), value, op);
        }
    }
}

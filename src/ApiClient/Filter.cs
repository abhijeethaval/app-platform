using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ApiClient
{
    public class Filter<T> : FilterSegment
    {
        protected internal readonly List<FilterSegment> segments = new List<FilterSegment>();

        public LogicBuilder EqualTo<TProp>(Expression<Func<T, TProp>> propertyExpression, TProp value)
        {
            this.segments.Add(new ConditionSegment<T, TProp>(propertyExpression, value, FilterOperator.EqualTo));
            return new LogicBuilder(this);
        }

        public LogicBuilder NotEqualTo<TProp>(Expression<Func<T, TProp>> propertyExpression, TProp value)
        {
            this.segments.Add(new ConditionSegment<T, TProp>(propertyExpression, value, FilterOperator.NotEqualTo));
            return new LogicBuilder(this);
        }


        public LogicBuilder LessThan(Expression<Func<T, DateTime>> propertyExpression, DateTime value)
        {
            this.segments.Add(new ConditionSegment<T, DateTime>(propertyExpression, value, FilterOperator.LessThan));
            return new LogicBuilder(this);
        }

        public LogicBuilder LessThan(Expression<Func<T, double>> propertyExpression, double value)
        {
            this.segments.Add(new ConditionSegment<T, double>(propertyExpression, value, FilterOperator.LessThan));
            return new LogicBuilder(this);
        }

        public LogicBuilder GreaterThan(Expression<Func<T, DateTime>> propertyExpression, DateTime value)
        {
            this.segments.Add(new ConditionSegment<T, DateTime>(propertyExpression, value, FilterOperator.GreaterThan));
            return new LogicBuilder(this);
        }

        public LogicBuilder GreaterThan(Expression<Func<T, double>> propertyExpression, double value)
        {
            this.segments.Add(new ConditionSegment<T, double>(propertyExpression, value, FilterOperator.GreaterThan));
            return new LogicBuilder(this);
        }

        public LogicBuilder Group(Action<FilterGroup<T>> configureInnerFilter)
        {
            var innerFilter = new FilterGroup<T>();
            configureInnerFilter(innerFilter);
            this.segments.Add(innerFilter);
            return new LogicBuilder(this);
        }

        internal override void Visit(IFilterBuilder filterBuilder)
        {
            filterBuilder.Append(this);
        }

        public readonly struct LogicBuilder
        {
            internal LogicBuilder(Filter<T> filter)
            {
                this.ParenFilter = filter;
            }

            internal Filter<T> ParenFilter { get; }

            public Filter<T> And()
            {
                this.ParenFilter.segments.Add(new LogicalOperatorSegment(LogicalOperator.And));
                return this.ParenFilter;
            }

            public Filter<T> Or()
            {
                this.ParenFilter.segments.Add(new LogicalOperatorSegment(LogicalOperator.Or));
                return this.ParenFilter;
            }
        }
    }
}

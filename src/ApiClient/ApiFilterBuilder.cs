using System;
using System.Collections.Generic;
using System.Text;

namespace ApiClient
{
    internal class ApiFilterBuilder : IFilterBuilder
    {
        private readonly StringBuilder apiFilter = new StringBuilder();

        public void Append<TProp>(string propertyName, TProp value, FilterOperator @operator)
        {
            this.apiFilter.Append(propertyName).Append(" ").Append(ConvertToFilterOperatorString(@operator)).Append(" ");
            this.AppendValue(value);
        }

        public void Append(LogicalOperator @operator)
        {
            this.apiFilter.Append(" ").Append(ConvertToLogicalOperatorString(@operator)).Append(" ");
        }

        public void Append<T>(Filter<T> filter)
        {
            AppendSegments(filter.segments);
        }

        public void Append<T>(FilterGroup<T> filterGroup)
        {
            this.apiFilter.Append('(');
            AppendSegments(filterGroup.segments);
            this.apiFilter.Append(")");
        }

        private void AppendSegments(List<FilterSegment> segments)
        {
            foreach (var segment in segments)
            {
                segment.Visit(this);
            }
        }

        public override string ToString()
        {
            return this.apiFilter.ToString();
        }

        private void AppendValue<TProp>(TProp value)
        {
            if (value is string stringValue)
            {
                this.apiFilter.Append("'").Append(stringValue).Append("'");
            }

            else if (value is DateTime dateTimeValue)
            {
                this.apiFilter.Append("'").Append(dateTimeValue.ToString()).Append("'");
            }

            if (value is double doubleValue)
            {
                this.apiFilter.Append(doubleValue.ToString());
            }
        }

        private static string ConvertToFilterOperatorString(FilterOperator @operator)
        {
            switch (@operator)
            {
                case FilterOperator.EqualTo: return "$eq";
                case FilterOperator.NotEqualTo: return "$ne";
                case FilterOperator.GreaterThan: return "$gt";
                case FilterOperator.LessThan: return "$lt";
                case FilterOperator.GreaterThanOrEqualTo: return "$ge";
                case FilterOperator.LessThanOrEqualTo: return "$le";
                default: throw new NotImplementedException();
            };
        }
        private static string ConvertToLogicalOperatorString(LogicalOperator @operator)
        {
            switch (@operator)
            {
                case LogicalOperator.And: return "$and";
                case LogicalOperator.Or: return "$or";
                default: throw new NotImplementedException();
            }
        }
    }
}

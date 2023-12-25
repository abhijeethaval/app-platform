using System;
using System.Collections.Concurrent;
using System.Linq.Expressions;

namespace ApiClient
{
    public class PropertyInfo<T, TProp>
    {
        private static readonly ConcurrentDictionary<Expression<Func<T, TProp>>, string> _propertyNameCache =
            new ConcurrentDictionary<Expression<Func<T, TProp>>, string>();

        public static string GetPropertyName(Expression<Func<T, TProp>> expression)
        {
            return _propertyNameCache.GetOrAdd(expression, ExtractPropertyName);
        }

        private static string ExtractPropertyName(Expression<Func<T, TProp>> expression)
        {
            if (expression.Body is MemberExpression body)
            {
                return body?.Member.Name;
            }

            body = (expression.Body as UnaryExpression).Operand as MemberExpression;
            return body?.Member.Name;
        }
    }
}

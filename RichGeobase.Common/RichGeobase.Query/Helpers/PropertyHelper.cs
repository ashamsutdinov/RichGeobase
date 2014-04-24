using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace RichGeobase.Query.Helpers
{
    public static class PropertyHelper
    {
        public static string GetMemberName<T>(this T instance, Expression<Func<T, object>> expression)
        {
            return GetMemberName(expression);
        }

        public static string GetMemberName<T>(Expression<Func<T, object>> expression)
        {
            if (expression == null)
            {
                throw new ArgumentException("The expression cannot be null.");
            }

            return GetMemberName(expression.Body);
        }

        public static string GetMemberName<T>(this T instance, Expression<Action<T>> expression)
        {
            return GetMemberName(expression);
        }

        public static string GetMemberName<T>(Expression<Action<T>> expression)
        {
            if (expression == null)
            {
                throw new ArgumentException("The expression cannot be null.");
            }

            return GetMemberName(expression.Body);
        }

        /// <summary>
        /// Determine if a property exists in an object
        /// </summary>
        /// <param name="propertyName">Name of the property </param>
        /// <param name="srcObject">The object to inspect</param>
        /// <param name="ignoreCase">Indicates whether to ignore case or not; default is true.</param>
        /// <returns>true if the property exists, false otherwise</returns>
        public static bool IsPropertyExists(this object srcObject, string propertyName, bool ignoreCase = true)
        {
            return IsPropertyExistsInternal(srcObject.GetType().GetProperties(), propertyName, ignoreCase);
        }

        public static bool IsPropertyExists<T>(string propertyName, bool ignoreCase = true)
        {
            return IsPropertyExistsInternal(typeof(T).GetProperties(), propertyName, ignoreCase);
        }


        private static bool IsPropertyExistsInternal(IEnumerable<PropertyInfo> propertyInfos, string propertyName, bool ignoreCase)
        {
            return propertyInfos.Any(propertyInfo => string.Compare(propertyInfo.Name, propertyName, ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal) == 0);
        }

        private static string GetMemberName(Expression expression)
        {
            if (expression == null)
            {
                throw new ArgumentException("The expression cannot be null.");
            }

            if (expression is MemberExpression)
            {
                // Reference type property or field
                var memberExpression =
                    (MemberExpression)expression;
                return memberExpression.Member.Name;
            }

            if (expression is MethodCallExpression)
            {
                // Reference type method
                var methodCallExpression =
                    (MethodCallExpression)expression;
                return methodCallExpression.Method.Name;
            }

            if (expression is UnaryExpression)
            {
                // Property, field of method returning value type
                var unaryExpression = (UnaryExpression)expression;
                return GetMemberName(unaryExpression);
            }

            throw new ArgumentException("Invalid expression");
        }

        private static string GetMemberName(UnaryExpression unaryExpression)
        {
            if (unaryExpression.Operand is MethodCallExpression)
            {
                var methodExpression = (MethodCallExpression)unaryExpression.Operand;
                return methodExpression.Method.Name;
            }

            return ((MemberExpression)unaryExpression.Operand).Member.Name;
        }
    }
}
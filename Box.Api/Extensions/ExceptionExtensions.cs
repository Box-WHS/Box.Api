using System;
using System.Linq.Expressions;
using StackExchange.Redis;

namespace Box.Api.Extensions
{
    internal static class ExceptionExtensions
    {
        public static void ThrowIfNull<T>(Expression<Func<T>> expression) where T: class
        {
            if (!(expression.Body is MemberExpression body))
            {
                throw new ArgumentException("expected property or field expression");
            }

            var compiled = expression.Compile();
            if (compiled() == null)
            {
                throw new ArgumentNullException(body.Member.Name);
            }
        }

        public static void ThrowIfNullOrEmpty(Expression<Func<string>> expression)
        {
            if (!(expression.Body is MemberExpression body))
            {
                throw new ArgumentException("expected property or field expression");
            }

            var compiled = expression.Compile();
            if (string.IsNullOrEmpty(compiled()))
            {
                throw new ArgumentException("String is null or empty", body.Member.Name);
            }
        }

        public static void ThrowIfNull<T>(Expression<Func<T>> expression, Func<Exception, Exception> outerException)
            where T : class
        {
            ThrowIfNull(() => outerException);
            
            if (!(expression.Body is MemberExpression body))
            {
                throw new ArgumentException("expected property or field expression");
            }

            var compiled = expression.Compile();
            if (compiled() == null)
            {
                throw outerException(new ArgumentNullException(body.Member.Name));
            }
        }

    }
}
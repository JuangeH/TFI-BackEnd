using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace Transversal.Extensions
{
    public static class ModelStateDictionaryExtensions
    {
        public static void AddModelError<TModel>(
            this ModelStateDictionary source,
            Expression<Func<TModel, object>> lambdaExpression, string error) =>
            source.AddModelError(GetPropertyName(lambdaExpression), error);


        public static void AddIdentityResultErrors(this ModelStateDictionary source, IdentityResult identityResult)
            => identityResult.Errors.ToList().ForEach(e => source.AddModelError(e.Code, e.Description));


        public static IEnumerable<KeyValuePair<string, IEnumerable<string>>> SerializeErrors(this ModelStateDictionary source)
            => source.ToDictionary(kvp => kvp.Key,
                kvp => kvp.Value.Errors.Select(e => e.ErrorMessage)).Where(m => m.Value.Any());

        public static IDictionary<string, IEnumerable<string>> ToStandardDictionary(this ModelStateDictionary source) =>
            source.ToDictionary(e => e.Key, e => e.Value.Errors.Select(error => error.ErrorMessage));

        private static string GetPropertyName(Expression lambdaExpression)
        {
            IList<string> list = new List<string>();
            var e = lambdaExpression;

            while (true)
            {
                switch (e.NodeType)
                {
                    case ExpressionType.Lambda:
                        e = ((LambdaExpression)e).Body;
                        break;
                    case ExpressionType.MemberAccess:
                        var propertyInfo = ((MemberExpression)e).Member as PropertyInfo;
                        var prop = propertyInfo != null
                                          ? propertyInfo.Name
                                          : null;
                        list.Add(prop);

                        var memberExpression = (MemberExpression)e;
                        if (memberExpression.Expression.NodeType != ExpressionType.Parameter)
                        {
                            var parameter = GetParameterExpression(memberExpression.Expression);
                            if (parameter != null)
                            {
                                e = Expression.Lambda(memberExpression.Expression, parameter);
                                break;
                            }
                        }
                        return string.Join(".", list.Reverse());
                    default:
                        return null;
                }
            }
        }

        private static ParameterExpression GetParameterExpression(Expression expression)
        {
            while (expression.NodeType == ExpressionType.MemberAccess)
            {
                expression = ((MemberExpression)expression).Expression;
            }
            return expression.NodeType == ExpressionType.Parameter ? (ParameterExpression)expression : null;
        }
    }
}

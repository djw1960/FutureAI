using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Serv
{
    /// <summary>    
    /// EntityFramework动态多条件查询与Lambda表达式树
    /// 在常规的信息系统中, 我们有需要动态多条件查询的情况, 例如UI上有多个选择项可供用户选择多条件查询数据
    /// 动态多条件查询    
    /// </summary>
    public static class PredicateBuilder
    {
        /// <summary>
        /// 默认True条件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static Expression<Func<T, bool>> True<T>() { return param => true; }
        /// <summary>
        /// 默认False条件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static Expression<Func<T, bool>> False<T>() { return param => false; }
        /// <summary>    
        /// 从指定的lambda表达式创建谓词表达式.    
        /// </summary>    
        public static Expression<Func<T, bool>> Create<T>(Expression<Func<T, bool>> predicate) { return predicate; }
        /// <summary>    
        /// 拼接And条件  
        /// </summary>    
        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> first, Expression<Func<T, bool>> second)
        {
            return first.Compose(second, Expression.AndAlso);
        }
        /// <summary>    
        /// 拼接Or条件
        /// </summary>    
        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> first, Expression<Func<T, bool>> second)
        {
            return first.Compose(second, Expression.OrElse);
        }
        /// <summary>    
        /// 拼接Not条件
        /// </summary>    
        public static Expression<Func<T, bool>> Not<T>(this Expression<Func<T, bool>> expression)
        {
            var negated = Expression.Not(expression.Body);
            return Expression.Lambda<Func<T, bool>>(negated, expression.Parameters);
        }
        /// <summary>    
        /// 使用指定的合并函数将第一个表达式与第二个表达式组合.    
        /// </summary>    
        static Expression<T> Compose<T>(this Expression<T> first, Expression<T> second, Func<Expression, Expression, Expression> merge)
        {
            // zip参数（从第二个参数到第一个参数的映射）    
            var map = first.Parameters
                .Select((f, i) => new { f, s = second.Parameters[i] })
                .ToDictionary(p => p.s, p => p.f);

            // 用第一个参数替换第二个lambda表达式中的参数   
            var secondBody = ParameterRebinder.ReplaceParameters(map, second.Body);

            // 用第一个表达式创建参数合并的lambda表达式 
            return Expression.Lambda<T>(merge(first.Body, secondBody), first.Parameters);
        }
        /// <summary>
        /// DateBetween And 用法 
        /// 使用方法:
        /// DateTime dateInTheMiddle;
        /// DataContext.EventHistories.Where(IsDateBetween<EventHistory>(h => new { h.FromDate, h.ToDate }, dateInTheMiddle))
        /// </summary>
        /// <typeparam name="TElement"></typeparam>
        /// <param name="queryable"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public static IQueryable<TElement> IsDateBetween<TElement>(this IQueryable<TElement> queryable,Expression<Func<TElement, DateTime>> fromDate,Expression<Func<TElement, DateTime>> toDate,DateTime date)
        {
            var p = fromDate.Parameters.Single();
            Expression member = p;

            Expression fromExpression = Expression.Property(member, (fromDate.Body as MemberExpression).Member.Name);
            Expression toExpression = Expression.Property(member, (toDate.Body as MemberExpression).Member.Name);

            var after = Expression.LessThanOrEqual(fromExpression,
                 Expression.Constant(date, typeof(DateTime)));

            var before = Expression.GreaterThanOrEqual(
                toExpression, Expression.Constant(date, typeof(DateTime)));

            Expression body = Expression.And(after, before);

            var predicate = Expression.Lambda<Func<TElement, bool>>(body, p);
            return queryable.Where(predicate);
        }
        /// <summary>
        ///上面的方法中由三个条件动态组成,  一个是匹配productName, 另两个是beginUpdateDate与endUpdateDate.在判断它们是否为时, 构建最终查询条件集合.
        ///最后把结果传给某个Repository类, 完成相应的数据访问.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="productName"></param>
        /// <param name="beignUpdateDate"></param>
        /// <param name="endUpdateDate"></param>
        /// <returns></returns>
        //public static Expression<Func<T, bool>> BuildFindByAllQuery<T>(string productName, DateTime? beignUpdateDate, DateTime? endUpdateDate)
        //{
        //    var list = new List<Expression<Func<T, bool>>>();
        //    if (!string.IsNullOrEmpty(productName)) list.Add(c => c.ProductName == productName);
        //    if (beignUpdateDate != null) list.Add(c => c.UpdatedTime >= beignUpdateDate);
        //    if (endUpdateDate != null) list.Add(c => c.UpdatedTime <= endUpdateDate);
        //    //Add more condition
        //    Expression<Func<T, bool>> productQueryTotal = null;
        //    foreach (var expression in list)
        //    {
        //        productQueryTotal = expression.And(expression);
        //    }

        //    return productQueryTotal;
        //}
    }
    /// <summary>
    /// ParameterRebinder
    /// </summary>
    internal class ParameterRebinder : ExpressionVisitor
    {
        /// <summary>
        /// 参数表达式映射
        /// </summary>
        readonly Dictionary<ParameterExpression, ParameterExpression> map;
        /// <summary>
        /// 初始化一个新的实例：“参见 cref＝“ParameterRebinder”/>类。
        /// </summary>
        /// <param name="map">The map.</param>
        ParameterRebinder(Dictionary<ParameterExpression, ParameterExpression> map)
        {
            this.map = map ?? new Dictionary<ParameterExpression, ParameterExpression>();
        }
        /// <summary>
        /// 替换参数.
        /// </summary>
        /// <param name="map">The map.</param>
        /// <param name="exp">The exp.</param>
        /// <returns>Expression</returns>
        public static Expression ReplaceParameters(Dictionary<ParameterExpression, ParameterExpression> map, Expression exp)
        {
            return new ParameterRebinder(map).Visit(exp);
        }
        /// <summary>
        /// 访问参数
        /// </summary>
        /// <param name="p">The p.</param>
        /// <returns>Expression</returns>
        protected override Expression VisitParameter(ParameterExpression p)
        {
            ParameterExpression replacement;

            if (map.TryGetValue(p, out replacement))
            {
                p = replacement;
            }

            return base.VisitParameter(p);
        }
    }
}

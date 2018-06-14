using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF.IRespository
{
    using System.Linq.Expressions;
    public interface IBaseRespository<TEntity> where TEntity:class
    {
        /// <summary>
        /// 新增方法
        /// </summary>
        /// <param name="eitity"></param>
        void Add(TEntity entity);
        /// <summary>
        /// 删除方法
        /// </summary>
        /// <param name="entity"></param>
        void Remove(TEntity entity);
        /// <summary>
        /// 根据条件删除
        /// </summary>
        /// <param name="whereLambda"></param>
         void RemoveBy(System.Linq.Expressions.Expression<Func<TEntity, bool>> whereLambda);
        /// <summary>
        /// 修改方法
        /// </summary>
        /// <param name="entity">自定实体为加入到EF中</param>
        /// <param name="propertiesName">要修改的属性名</param>
        void Update(TEntity entity,string[] propertiesName);
        /// 查询
        /// </summary>
        /// <param name="whereLambda">查询表达式</param>
        /// <returns></returns>
        TEntity Single(Expression<Func<TEntity, bool>> whereLambda);
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="whereLambda">查询表达式</param>
        /// <returns></returns>
        IQueryable<TEntity> where(Expression<Func<TEntity, bool>> whereLambda);
        /// <summary>
        /// 查询 排序
        /// </summary>
        /// <typeparam name="TOrderKey">排序字段的类型</typeparam>
        /// <param name="whereLambda">查询表达式</param>
        /// <param name="orderByLambda">排序表达式</param>
        /// <param name="isAsc">标记排序默认方式为正序</param>
        /// <returns></returns>
        IQueryable<TEntity> where<TOrderKey>(Expression<Func<TEntity, bool>> whereLambda, Expression<Func<TEntity, TOrderKey>> orderByLambda, bool isAsc=true);
        /// <summary>
        /// 根据条件 连表 分页 排序
        /// </summary>
        /// <typeparam name="TOrderKey">排序字段的类型</typeparam>
        /// <param name="pagedData">分页相关数据实体</param>
        /// <param name="whereLambda">查询表达式</param>
        /// <param name="orderByLambda">排序表达式</param>
        /// <param name="isAsc">默认排序方式</param>
        /// <param name="includeEntitysName">要连接的表名</param>
        void wherePaged<TOrderKey>(Entitys.FormatModels.PagedData<TEntity> pagedData, Expression<Func<TEntity, bool>> whereLambda, Expression<Func<TEntity, TOrderKey>> orderByLambda, bool isAsc = true, params string[] includeEntitysName);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parms"></param>
        /// <returns></returns>
        int ExecuteNoQuery(string sql, params System.Data.SqlClient.SqlParameter[] parms);
        /// <summary>
        /// 保存EF容器中的更改
        /// </summary>
        /// <returns></returns>
        int SaveChanges();
    }
}

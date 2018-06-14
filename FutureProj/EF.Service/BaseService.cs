using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF.Service
{
    using EF.IRespository;
    using EF.IService;
    using System.Data;
    using System.Reflection;
    using System.Runtime.Remoting.Messaging;
    public abstract class BaseService<TEntity>:IBaseService<TEntity>
        where TEntity:class
    {
        protected IRespository.IBaseRespository<TEntity> dal = null;

        public BaseService()
        {
            Setdal();
        }
        public abstract void Setdal();

        public IRespository.IRespositorySession DBSession
        {
            get
            {
                return DbSessionFactory.GetDbSession();
            }
        }

        #region 3.0 新增方法+void Add(TEntity entity)
        /// <summary>
        /// 新增方法
        /// </summary>
        /// <param name="eitity"></param>
        public void Add(TEntity entity)
        {
            dal.Add(entity);
        } 
        #endregion

        #region 4.0 删除方法+void Remove(TEntity entity)
        /// <summary>
        /// 删除方法
        /// </summary>
        /// <param name="entity">实体未添加到EF容器中</param>
        public void Remove(TEntity entity)
        {
            dal.Remove(entity);
        } 

        #endregion

        #region 4.1 根据条件删除+ void RemoveBy(System.Linq.Expressions.Expression<Func<TEntity, bool>> whereLambda)
        /// <summary>
        /// 根据条件删除
        /// </summary>
        /// <param name="whereLambda"></param>
        public void RemoveBy(System.Linq.Expressions.Expression<Func<TEntity, bool>> whereLambda)
        {
            dal.RemoveBy(whereLambda);
        } 
        #endregion

        #region 5.0 修改方法+void Update(TEntity entity, string[] propertiesName)
        /// <summary>
        ///  修改方法
        /// </summary>
        /// <param name="entity">未加入EF容器中的实体</param>
        /// <param name="propertiesName">要修改的属性集合</param>
        public void Update(TEntity entity, string[] propertiesName)
        {
            dal.Update(entity, propertiesName);
        } 
        #endregion

        #region 6.0 跟训单个实体+TEntity Single(System.Linq.Expressions.Expression<Func<TEntity, bool>> whereLambda)
        /// <summary>
        /// 根据条件查询单个实体
        /// </summary>
        /// <param name="whereLambda"></param>
        /// <returns></returns>
        public TEntity Single(System.Linq.Expressions.Expression<Func<TEntity, bool>> whereLambda)
        {
            return dal.Single(whereLambda);
        } 
        #endregion

        #region 6.0 where 查询+IQueryable<TEntity> where(System.Linq.Expressions.Expression<Func<TEntity, bool>> whereLambda)
        /// <summary>
        /// 根据条件查询
        /// </summary>
        /// <param name="whereLambda">查询表达式</param>
        /// <returns></returns>
        public IQueryable<TEntity> where(System.Linq.Expressions.Expression<Func<TEntity, bool>> whereLambda)
        {
            return dal.where(whereLambda);
        } 
        #endregion

        #region 6.0 where查询 排序+IQueryable<TEntity> where<TOrderKey>(System.Linq.Expressions.Expression<Func<TEntity, bool>> whereLambda, System.Linq.Expressions.Expression<Func<TEntity, TOrderKey>> orderByLambda, bool isAsc = true)
        /// <summary>
        /// 条件查询，排序
        /// </summary>
        /// <typeparam name="TOrderKey">排序字段的类型（自动）</typeparam>
        /// <param name="whereLambda">查询表达式</param>
        /// <param name="orderByLambda">排序表达式</param>
        /// <param name="isAsc">排序方式（顺序还是倒序）</param>
        /// <returns></returns>
        public IQueryable<TEntity> where<TOrderKey>(System.Linq.Expressions.Expression<Func<TEntity, bool>> whereLambda, System.Linq.Expressions.Expression<Func<TEntity, TOrderKey>> orderByLambda, bool isAsc = true)
        {
           return dal.where(whereLambda, orderByLambda, isAsc);
        }
        #endregion

        #region 8.0 where 根据条件 查询 排序 分页
        /// <summary>
        /// 根据条件 连表 分页 排序
        /// </summary>
        /// <typeparam name="TOrderKey">排序字段的类型</typeparam>
        /// <param name="pagedData">分页相关数据实体</param>
        /// <param name="whereLambda">查询表达式</param>
        /// <param name="orderByLambda">排序表达式</param>
        /// <param name="isAsc">默认排序方式</param>
        /// <param name="includeEntitysName">要连接的表名</param>
        public void wherePaged<TOrderKey>(Entitys.FormatModels.PagedData<TEntity> pagedData, System.Linq.Expressions.Expression<Func<TEntity, bool>> whereLambda, System.Linq.Expressions.Expression<Func<TEntity, TOrderKey>> orderByLambda, bool isAsc = true, params string[] includeEntitysName)
        {
            dal.wherePaged(pagedData, whereLambda, orderByLambda, isAsc, includeEntitysName);
        } 
        #endregion

        #region 9.0 直接执行sql语句
        /// <summary>
        /// 直接执行sql语句
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parms"></param>
        /// <returns></returns>
        public int ExecuteNoQuery(string sql, params System.Data.SqlClient.SqlParameter[] parms)
        {
            return dal.ExecuteNoQuery(sql, parms);
        } 
        #endregion

        #region 10.0 保存方法+int SaveChanges()
        /// <summary>
        /// 保存EF容器的修改
        /// </summary>
        /// <returns></returns>
        public int SaveChanges()
        {
            return dal.SaveChanges();
        } 
        #endregion
    }
}

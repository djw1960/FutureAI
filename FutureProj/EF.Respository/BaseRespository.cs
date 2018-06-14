using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF.Respository
{
    using EF.IRespository;
    using System.Data;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Reflection;
    using System.Runtime.Remoting.Messaging;
    using EF.Entitys.FormatModels;
    using System.Linq.Expressions;

    public class BaseRespository<TEntity>:IBaseRespository<TEntity>
        where TEntity:class
    {
        #region 1.0 实现单例的EF上下文容器+CommonCRMEntities db
        public DbContext db
        {
            get
            {
                //CommonCRMEntities temp = CallContext.GetData(typeof(CommonCRMEntities).FullName) as CommonCRMEntities;
                //if (temp == null)
                //{
                //    temp = new CommonCRMEntities();
                //    CallContext.SetData(typeof(CommonCRMEntities).FullName, temp);
                //}
                return EFFactory.GetDbContext();
            }
        } 
        #endregion

        #region 2.0 封装dbSet,关闭Configuration的保存验证
        DbSet<TEntity> dbSet;
        public DbSet<TEntity> DbSet
        {
            get
            {
                return dbSet;
            }
        }
        public BaseRespository()
        {
            db.Configuration.ValidateOnSaveEnabled = false;
            dbSet = db.Set<TEntity>();
        } 
        #endregion

        #region 3.0 新增方法+void Add(TEntity entity)
        /// <summary>
        /// 新增方法
        /// </summary>
        /// <param name="eitity"></param>
        public void Add(TEntity entity)
        {
            DbSet.Add(entity);
        } 
        #endregion

        #region 4.0 删除方法+void Remove(TEntity entity)
        /// <summary>
        /// 删除方法
        /// </summary>
        /// <param name="entity">实体未添加到EF容器中</param>
        public void Remove(TEntity entity)
        {
            DbSet.Attach(entity);
            DbSet.Remove(entity);
        } 

        #endregion

        #region 4.1 根据条件删除+ void RemoveBy(System.Linq.Expressions.Expression<Func<TEntity, bool>> whereLambda)
        /// <summary>
        /// 根据条件删除
        /// </summary>
        /// <param name="whereLambda"></param>
        public void RemoveBy(System.Linq.Expressions.Expression<Func<TEntity, bool>> whereLambda)
        {
            //1.根据条件 使用 EF 将要删除的对象 查询出来，并存入 EF容器
            var list = DbSet.Where(whereLambda);
            //2.循环对象，并修改对象的 状态为“删除”
            foreach (var item in list)
            {
                DbSet.Remove(item);
            }
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
            //1.0 把要修改的实体添加到EF容器中
            DbEntityEntry entry = db.Entry(entity);
            //2.0 修改EF中的状态
            DbSet.Attach(entity);//entry.State = EntityState.Unchanged;
            //3.0 修改实体中要求的字段的IsModified属性为true
            foreach (string item in propertiesName)
            {
                entry.Property(item).IsModified = true;
            }
        } 
        #endregion

        #region 6.0 single 根据条件查询+TEntity Single(Expression<Func<TEntity, bool>> whereLambda)
        /// <summary>
        /// 根据条件查询单个实体信息
        /// </summary>
        /// <param name="whereLambda">查询表达式</param>
        /// <returns></returns>
        public TEntity Single(System.Linq.Expressions.Expression<Func<TEntity, bool>> whereLambda)
        {
            return DbSet.SingleOrDefault(whereLambda);
        } 
        #endregion

        #region 6.0 根据条件查询+IQueryable<TEntity> where(Expression<Func<TEntity, bool>> whereLambda)
        /// <summary>
        /// 根据条件查询
        /// </summary>
        /// <param name="whereLambda"></param>
        /// <returns></returns>
        public IQueryable<TEntity> where(System.Linq.Expressions.Expression<Func<TEntity, bool>> whereLambda)
        {
            return DbSet.Where(whereLambda);
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
            if (isAsc)
            {
                return DbSet.Where(whereLambda).OrderBy(orderByLambda);
            }
            else
            {
                return DbSet.Where(whereLambda).OrderByDescending(orderByLambda);
            }
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
            //1.0 连表确定原始数据集
            DbQuery<TEntity> dbQuery = DbSet as DbQuery<TEntity>;
            if (includeEntitysName.Any())
            {
                foreach (string item in includeEntitysName)
                {
                    dbQuery = dbQuery.Include(item);
                }
            }
            //2.0 查询排序确定结果数据集
            IQueryable<TEntity> filterQuery;
            if (isAsc)
            {
                filterQuery = dbQuery.Where(whereLambda).OrderBy(orderByLambda);
            }
            else
            {
                filterQuery = dbQuery.Where(whereLambda).OrderByDescending(orderByLambda);
            }
            //3.0 分页
            pagedData.rows = filterQuery.Skip((pagedData.PageIndex - 1) * pagedData.PageSize).Take(pagedData.PageSize);
            pagedData.total = filterQuery.Count();
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
            return db.Database.ExecuteSqlCommand(sql, parms);
        }
        #endregion

        #region 10.0 保存方法+int SaveChanges()
        /// <summary>
        /// 保存EF容器的修改
        /// </summary>
        /// <returns></returns>
        public int SaveChanges()
        {
            return db.SaveChanges();
        }
        #endregion

    }
}

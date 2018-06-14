using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF.IRespository
{
    public partial interface IRespositorySession
    {
        int SaveChanges();

        /// <summary>
        /// 可直接执行sql，或者调用存储过程
        /// ExecuteSqlCommand("  DELETE FROM [dbo].[PayOrder] WHERE ID =@ID ", sqlParameters);
        /// ExecuteSqlCommand(" EXEC [dbo].[PROC_TestGetCount] @ID ,@count out,@result out ", insp, csp, rsp);
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parms"></param>
        /// <returns></returns>
        int Proc_ExecuteSqlCommand(string sql, params System.Data.SqlClient.SqlParameter[] parms);
        /// <summary>
        /// 可直接执行sql或调用存储过程
        /// SqlQuery<TSContet>(" SELECT ID FROM DBO.PayOrder").ToList();
        /// SqlQuery<TSContet>(" EXEC PROC_TestPayList @totalcount out", total).ToList();
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="parms"></param>
        /// <returns></returns>
        List<T> Proc_SqlQuery<T>(string sql, params System.Data.SqlClient.SqlParameter[] parms) where T:class;
    }
}

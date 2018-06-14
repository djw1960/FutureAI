using EF.Common;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.EntityClient;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            EF.edmx.DBEntities dB = new edmx.DBEntities();
            //1.0 执行sql语句
            //2.0 执行无返回存储过程
            //3 执行返回output值存储过程
            //4 执行返回list和output返回存储过程

            #region 存储过程测试
            ////-------------------------------------------------------------------
            //int n=dB.Database.ExecuteSqlCommand("  SELECT COUNT(1) FROM [dbo].[PayOrder] ");
            //System.Console.WriteLine(n);
            //var sqlParameters=new SqlParameter[]{
            //    new SqlParameter("ID","6")
            //};
            //n = dB.Database.ExecuteSqlCommand("  DELETE FROM [dbo].[PayOrder] WHERE ID =@ID ", sqlParameters);
            //System.Console.WriteLine(n);

            //List<TSContet> list=dB.Database.SqlQuery<TSContet>(" SELECT ID FROM DBO.PayOrder").ToList();
            //System.Console.WriteLine(list.Count);

            //System.Console.WriteLine("----------------------------------------------------------");

            ////2.0 执行无返回存储过程,返回受影响行数
            //n = dB.Database.ExecuteSqlCommand(" EXEC DBO.PROC_TestPayOrder @ID ", sqlParameters);
            //System.Console.WriteLine(n);


            //var insp = new SqlParameter("ID", "6");
            //insp.Direction = System.Data.ParameterDirection.Input;
            ////var resp = new SqlParameter("return_value", System.Data.SqlDbType.Int);
            ////resp.Direction = System.Data.ParameterDirection.Output;
            //var csp = new SqlParameter("count", System.Data.SqlDbType.Int);
            //csp.Direction = System.Data.ParameterDirection.Output;
            //var rsp = new SqlParameter("result", System.Data.SqlDbType.Int);
            //rsp.Direction = System.Data.ParameterDirection.Output; 
            #endregion
            int n = 0;
            SqlParameter[] sp = {
            //    new SqlParameter(){ ParameterName="@RETURN", Direction=System.Data.ParameterDirection.ReturnValue,SqlDbType= System.Data.SqlDbType.Int},
                new SqlParameter(){ ParameterName="@ID", Direction=System.Data.ParameterDirection.Input,Value=6},
                new SqlParameter(){ ParameterName="@count", Direction=System.Data.ParameterDirection.Output,SqlDbType= System.Data.SqlDbType.Int},
                new SqlParameter(){ ParameterName="@result", Direction=System.Data.ParameterDirection.Output,SqlDbType= System.Data.SqlDbType.Int},
            };
            n =dB.Database.ExecuteSqlCommand(" EXEC [dbo].[PROC_TestGetCount] @ID ,@count out,@result out ", sp);

            var total = new SqlParameter("totalcount", System.Data.SqlDbType.Int);
            total.Direction = System.Data.ParameterDirection.Output;
            List<TSContet> paylist = dB.Database.SqlQuery<TSContet>(" EXEC PROC_TestPayList @totalcount out", total).ToList();

            foreach (var item in paylist)
            {
                System.Console.WriteLine(item.ID);
            }



            System.Console.ReadKey();
        }

        
    }
    public class TSContet
    {
        public long ID { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF.Respository
{
    using EF.IRespository;
    public partial class RespositorySession : IRespositorySession
    {
        public int SaveChanges()
        {
            return EFFactory.GetDbContext().SaveChanges();
        }
        public int Proc_ExecuteSqlCommand(string sql, params System.Data.SqlClient.SqlParameter[] parms)
        {
            return EFFactory.GetDBEntity().Database.ExecuteSqlCommand(sql, parms);
        }
        public List<T> Proc_SqlQuery<T>(string sql, params System.Data.SqlClient.SqlParameter[] parms) where T : class
        {
            return EFFactory.GetDBEntity().Database.SqlQuery<T>(sql, parms).ToList();
        }
    }
}

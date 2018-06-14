using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF.Service
{
    public partial class ServiceSession:IService.IServiceSession
    {
        public int Proc_ExecuteSqlCommand(string sql, params SqlParameter[] parms)
        {
            return DbSessionFactory.GetDbSession().Proc_ExecuteSqlCommand(sql, parms);
        }

        public List<T> Proc_SqlQuery<T>(string sql, params SqlParameter[] parms) where T : class
        {
            return DbSessionFactory.GetDbSession().Proc_SqlQuery<T>(sql, parms);
        }

        public int SaveChanges()
        {
           return DbSessionFactory.GetDbSession().SaveChanges();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace EF.Respository
{
    public static class EFFactory
    {
        public static DbContext GetDbContext()
        {
            DbContext dbContext = CallContext.GetData("DBContext") as DbContext;
            if (dbContext == null)
            {
                dbContext = new DBEntities();
                CallContext.SetData("DBContext", dbContext);
            }
            return dbContext;
        }
        public static DBEntities GetDBEntity()
        {
            DBEntities dbEntity = CallContext.GetData("DBEntities") as DBEntities;
            if (dbEntity == null)
            {
                dbEntity = new DBEntities();
                CallContext.SetData("DBEntities", dbEntity);
            }
            return dbEntity;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF.Common.Extends
{
    /// <summary>
    /// SQL扩展类
    /// </summary>
    public static class SqlExtend
    {
        public static SqlParameter GetNewParameter(this SqlParameter sp, System.Data.ParameterDirection dr, string key, object value)
        {
            SqlParameter sparam = new SqlParameter();
            sparam.ParameterName = key;
            sparam.Direction = dr;
            switch (dr)
            {
                case System.Data.ParameterDirection.Input:
                    sparam.Value = value;
                    break;
                case System.Data.ParameterDirection.Output:
                    sparam.SqlDbType = (System.Data.SqlDbType)value;
                    break;
                case System.Data.ParameterDirection.InputOutput:
                    sparam.SqlDbType = (System.Data.SqlDbType)value;
                    break;
                case System.Data.ParameterDirection.ReturnValue:
                    sparam.SqlDbType = (System.Data.SqlDbType)value;
                    break;
            }
            return sparam;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF.Entitys.FormatModels
{
    public class PagedData<TEntity>
    {
        /// <summary>
        /// 当前页码
        /// </summary>
        public int PageIndex;
        /// <summary>
        /// 每页条数
        /// </summary>
        public int PageSize;
        /// <summary>
        /// 总数据行数
        /// </summary>
        public int total;
        /// <summary>
        /// 总页数
        /// </summary>
        public int PageCount
        {
            get
            {
                return (int)Math.Ceiling(Convert.ToDouble(total) / Convert.ToDouble(PageSize));
            }
        }
        /// <summary>
        /// 当前页的数据集合
        /// </summary>
        public IEnumerable<TEntity> rows;
    }
}

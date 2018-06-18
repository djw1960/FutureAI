using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serv.Entitys
{
    public class SHFE_DataModel
    {
        public string o_code  { get; set; }
        public List<SHFE_CangDanModel> o_cursor { get; set; }
        public string o_issueno { get; set; }
        public string o_msg { get; set; }
        public string o_totalissueno { get; set; }
        public string o_tradingday { get; set; }
    }
    public class SHFE_CangDanModel
    {
        /// <summary>
        /// 地点
        /// </summary>
        public string REGNAME { get; set; }
        /// <summary>
        /// 品种
        /// </summary>
        public string VARNAME { get; set; }
        /// <summary>
        /// 仓库
        /// </summary>
        public string WHABBRNAME { get; set; }
        /// <summary>
        /// 仓单
        /// </summary>
        public string WRTWGHTS { get; set; }
        /// <summary>
        /// 仓单变化
        /// </summary>
        public string WRTCHANGE { get; set; }
    }
}

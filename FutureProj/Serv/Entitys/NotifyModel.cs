using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serv.Entitys
{
    public class NotifyModel
    {
        public string BillNo { get; set; }
        public string Amount { get; set; }
        public string CustomerNo { get; set; }
        public string DataTime { get; set; }
        public string Status { get; set; }
        public string SignType { get; set; }
        public string Sign { get; set; }
        /// <summary>
        /// 商城名称
        /// </summary>
        public string Source { get; set; }
        /// <summary>
        /// 通道名称
        /// </summary>
        public string Channel { get; set; }
        /// <summary>
        /// 支付手续费
        /// </summary>
        public string Service_charge { get; set; }
        /// <summary>
        /// 手续费率
        /// </summary>
        public string Charge_val { get; set; }
        /// <summary>
        /// 手续费计算方式
        /// 1-按笔数，2-按百分比
        /// </summary>
        public string Charge_type { get; set; }
        /// <summary>
        /// 手续费计算规则
        /// 1:四舍五入,2:向下取整,3:向上取整
        /// </summary>
        public string Calculation_type { get; set; }
        /// <summary>
        /// 第三方商户订单号
        /// </summary>
        public string OrderNo { get; set; }
        
    }
    public class NotifyResultModel
    {
        public bool IsSuccess { get; set; }
        public string Content { get; set; }
    }
}

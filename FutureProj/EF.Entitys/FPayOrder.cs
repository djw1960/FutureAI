//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace EF.Entitys
{
    using System;
    using System.Collections.Generic;
    
    public partial class FPayOrder
    {
        public long ID { get; set; }
        public string GNo { get; set; }
        public int Amount { get; set; }
        public int Status { get; set; }
        public int IsFinish { get; set; }
        public string PNo { get; set; }
        public int IsDel { get; set; }
        public System.DateTime AddDate { get; set; }
        public Nullable<System.DateTime> UpdateDate { get; set; }
        public string Remark { get; set; }
    }
}

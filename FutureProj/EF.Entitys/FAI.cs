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
    
    public partial class FAI
    {
        public int ID { get; set; }
        public System.DateTime DT { get; set; }
        public string Name { get; set; }
        public decimal NPrice { get; set; }
        public string TurnType { get; set; }
        public string DataType { get; set; }
        public int IsPublish { get; set; }
        public string Remark { get; set; }
        public Nullable<int> Status { get; set; }
        public Nullable<decimal> Result { get; set; }
    }
}

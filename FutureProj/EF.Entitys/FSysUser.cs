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
    
    public partial class FSysUser
    {
        public int ID { get; set; }
        public string Login { get; set; }
        public string Pwd { get; set; }
        public string UserName { get; set; }
        public int RoleID { get; set; }
        public int IsAvailable { get; set; }
        public System.DateTime AddDate { get; set; }
        public string Remark { get; set; }
    }
}

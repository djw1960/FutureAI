//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace EF.edmx
{
    using System;
    using System.Collections.Generic;
    
    public partial class FSysUser_Invite
    {
        public int ID { get; set; }
        public int UID { get; set; }
        public string InviteCode { get; set; }
        public bool IsUsed { get; set; }
        public Nullable<int> UseBy { get; set; }
    }
}

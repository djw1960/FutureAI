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
    
    public partial class FSys_LoginSession
    {
        public long ID { get; set; }
        public long UID { get; set; }
        public string Token { get; set; }
        public string Source { get; set; }
        public System.DateTime TimeOut { get; set; }
        public string UserType { get; set; }
    }
}

﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class DBEntities : DbContext
    {
        public DBEntities()
            : base("name=DBEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<FAI> FAI { get; set; }
        public virtual DbSet<FDataMaterial> FDataMaterial { get; set; }
        public virtual DbSet<FDataRepository> FDataRepository { get; set; }
        public virtual DbSet<FNews> FNews { get; set; }
        public virtual DbSet<FPayGood> FPayGood { get; set; }
        public virtual DbSet<FPayMert> FPayMert { get; set; }
        public virtual DbSet<FPayOrder> FPayOrder { get; set; }
        public virtual DbSet<FSys_Config> FSys_Config { get; set; }
        public virtual DbSet<FSys_LoginSession> FSys_LoginSession { get; set; }
        public virtual DbSet<FSys_Role> FSys_Role { get; set; }
        public virtual DbSet<FSysUser> FSysUser { get; set; }
        public virtual DbSet<FPayWay> FPayWay { get; set; }
        public virtual DbSet<FDataLV> FDataLV { get; set; }
        public virtual DbSet<FDataReposInit> FDataReposInit { get; set; }
    }
}

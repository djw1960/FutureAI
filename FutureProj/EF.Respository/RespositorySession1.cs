﻿//------------------------------------------------------------------------------
// <auto-generated>
//
//CodeBy DJW
// </auto-generated>
//------------------------------------------------------------------------------
namespace EF.Respository
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    public partial class RespositorySession:IRespository.IRespositorySession
    {
    	public IRespository.Sons.IFAI FAI
    	{
    		get { return new Respository.Sons.FAI(); }
    	}
    	public IRespository.Sons.IFDataMaterial FDataMaterial
    	{
    		get { return new Respository.Sons.FDataMaterial(); }
    	}
    	public IRespository.Sons.IFDataRepository FDataRepository
    	{
    		get { return new Respository.Sons.FDataRepository(); }
    	}
    	public IRespository.Sons.IFNews FNews
    	{
    		get { return new Respository.Sons.FNews(); }
    	}
    	public IRespository.Sons.IFPayGood FPayGood
    	{
    		get { return new Respository.Sons.FPayGood(); }
    	}
    	public IRespository.Sons.IFPayMert FPayMert
    	{
    		get { return new Respository.Sons.FPayMert(); }
    	}
    	public IRespository.Sons.IFPayOrder FPayOrder
    	{
    		get { return new Respository.Sons.FPayOrder(); }
    	}
    	public IRespository.Sons.IFSys_Config FSys_Config
    	{
    		get { return new Respository.Sons.FSys_Config(); }
    	}
    	public IRespository.Sons.IFSys_LoginSession FSys_LoginSession
    	{
    		get { return new Respository.Sons.FSys_LoginSession(); }
    	}
    	public IRespository.Sons.IFSys_Role FSys_Role
    	{
    		get { return new Respository.Sons.FSys_Role(); }
    	}
    	public IRespository.Sons.IFSysUser FSysUser
    	{
    		get { return new Respository.Sons.FSysUser(); }
    	}
    	public IRespository.Sons.IFPayWay FPayWay
    	{
    		get { return new Respository.Sons.FPayWay(); }
    	}
    }
    
}

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
    	public IRespository.Sons.IFNews FNews
    	{
    		get { return new Respository.Sons.FNews(); }
    	}
    	public IRespository.Sons.IFRepository FRepository
    	{
    		get { return new Respository.Sons.FRepository(); }
    	}
    	public IRespository.Sons.IInRawMaterial InRawMaterial
    	{
    		get { return new Respository.Sons.InRawMaterial(); }
    	}
    }
    
}

using CMS.Entities;
using CMS.Repo.Repository;
using SimpleCrud.AppDbContext;
using SimpleCrud.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMS.Repo.RepositoryImpl
{
    public class MyWorkRepositoryImpl:IRepositoryImpl<MyWork>, MyWorkRepository
    {
        public MyWorkRepositoryImpl(DbClass context) : base(context)
    {

    }
    public DbClass DbClass
    {
        get { return context as DbClass; }
    }
}
}

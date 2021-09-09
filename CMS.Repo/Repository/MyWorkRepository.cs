using CMS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CMS.Repo.Repository
{
   public interface MyWorkRepository
    {
        MyWork GetById(int Id);
        void Insert(MyWork entity);
        void Update(MyWork entity);
        void Remove(MyWork entity);
        IEnumerable<MyWork> GetAll();
        IQueryable<MyWork> Queryable();
    }
}

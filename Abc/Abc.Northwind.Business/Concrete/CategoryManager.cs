using Abc.Northwind.Business.Abstract;
using System;
using System.Collections.Generic;
using System.Text;
using Abc.Northwind.Entities.Concrete;
using Abc.Northwind.DataAccess.Abstract;

namespace Abc.Northwind.Business.Concrete
{
    public class CategoryManager : ICategoryService
    {
        private ICategoryDAL _categoryDAL;

        public CategoryManager(ICategoryDAL categoryDAL)
        {
            _categoryDAL = categoryDAL;
        }

        public List<Category> GetAll()
        {
            return _categoryDAL.GetList();
        }
    }
}

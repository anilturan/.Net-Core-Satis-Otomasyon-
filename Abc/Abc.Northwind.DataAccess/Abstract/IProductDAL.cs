using Abc.Core.DataAccess;
using Abc.Northwind.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Abc.Northwind.DataAccess.Abstract
{
    //Burası nesneye özel olarak oluşturulan Interface'im
    public interface IProductDAL : IEntityRepository<Product>
    {

    }
}

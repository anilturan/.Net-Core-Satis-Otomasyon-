using Abc.Northwind.Business.Abstract;
using System;
using System.Collections.Generic;
using System.Text;
using Abc.Northwind.Entities.Concrete;
using Abc.Northwind.DataAccess.Concrete.EntityFramework;
using Abc.Northwind.DataAccess.Abstract;

namespace Abc.Northwind.Business.Concrete
{
    public class ProductManager : IProductService
    {
        private IProductDAL _productDAL;

        //IProductDAL Base Interface olduğundan dolayı DAL katmanındaki EF ve NHibernate ya da başka bir ORM bu Interface'i implement edecektir dolayısıyla bu interface'i implement eden her ORM için bu komut çalışacaktır.
        public ProductManager(IProductDAL productDAL)
        {
            _productDAL = productDAL;
        }

        public void Add(Product product)
        {
            _productDAL.Add(product);
        }

        public void Delete(Product product)
        {
            _productDAL.Delete(product);
        }

        public List<Product> GetAll()
        {
            //KÖTÜ KULLANIMA BİR ÖRNEK: SOLID'IN D'si DER Kİ; ÜST KATMAN ALT KATMANI HİÇBİR ZAMAN NEW'LEYEMEZ.
            //ŞU ANDA BLL'İ EF'E BAĞIMLI HALE GETİRMİŞ OLDUK.

            //EFProductDAL productDAL = new EFProductDAL();
            //return productDAL.GetList();

            return _productDAL.GetList();
        }

        public List<Product> GetByCategory(int categoryId)
        {
            //Operasyonu gönderiyoruz.
            return _productDAL.GetList(p => p.CategoryId == categoryId || categoryId == 0);
        }

        public Product GetById(int productId)
        {
            return _productDAL.Get(p => p.ProductId == productId);
        }

        public void Update(Product product)
        {
            _productDAL.Update(product);
        }
    }
}

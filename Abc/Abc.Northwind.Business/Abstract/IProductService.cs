using Abc.Northwind.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Abc.Northwind.Business.Abstract
{
    //Sonuna Service kelimesi getirilmesinin sebebi; genellikle bu katmanı service katmanları kullanıyorlar ayrıca UI'a çıkmayı (sunmayı) yapan katman olduğu için...

    //Neden IRepositoryBase'den kalıtım almadık? 
    //Repository katmanı nesnelerin veritabanıyla olan etkileşimini gerçekleştirir. Ben bu katmanda kontrol, loglama, caching yapabilirim bu yüzden birbirlerinden bağımsız olmalılar. Ayrıca Repository katmanı DAL'da olması gereken bir patterndir ve IRepositoryBase'deki tüm metotları kullanmayacağım.
    public interface IProductService
    {
        List<Product> GetAll();
        List<Product> GetByCategory(int categoryId);
        void Add(Product product);
        void Update(Product product);
        void Delete(Product product);
        Product GetById(int productId);
    }
}

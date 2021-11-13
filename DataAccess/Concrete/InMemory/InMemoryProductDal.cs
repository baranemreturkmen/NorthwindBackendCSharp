using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.InMemory
{   

    //Burada yapılan işlerin çoğunu EntityFramework bizim yerimize yapacak zaten. Olayı daha iyi anlamak adına burada yaptığımız şey kendi verimizi oluşturup simülasyon yapmak.


    public class InMemoryProductDal : IProductDal
    {

        //Global değişkenler _ ile başlar.

        List<Product> _products;

        public InMemoryProductDal()
        {
            _products = new List<Product> { 
            new Product { ProductName = "Bardak",CategoryId=1,ProductId=1,UnitPrice=15,UnitsInStock=15},
            new Product { ProductName = "Masa",CategoryId=2,ProductId=2,UnitPrice=25,UnitsInStock=25},
            new Product { ProductName = "Sandalye",CategoryId=5,ProductId=3,UnitPrice=35,UnitsInStock=35 },
            new Product { ProductName = "Tabak",CategoryId=4,ProductId=4,UnitPrice=45,UnitsInStock=45}
        };
            }

        public void Add(Product product)
        {
            _products.Add(product);
        }

        public void Delete(Product product)
        {
            //_products.Remove(product);
            //neden bu şekilde yazdığımda silme işlemi 
            //gerçekleşmez? Metoda parametre olarak verdigim product nesnesinin referansi farkli oldugu icin listenin icerisinde bu nesneyi bulamayacak dolayısıyla da silemeyecek urunu.


            //Kendim bir product olusturup referansi ona atiyorum. Tek tek elimdeki elemanları dolaşıp gonderdigim nesnede ki id onceden olusturudugum listedeli product nesnesinin id'sine eşitse, listede silinmesi gereken elemanı buluyorum. 

            //Product productToDelete = new Product(); productToDelete uzerine sadece referans numarasi atamasi yapacagim için burada newleme yaparak boşu boşuna belleği yormuş oluyorum. 

            Product productToDelete = null;

            foreach (var p in _products)
            {
                if(product.ProductId == p.ProductId)
                {
                    productToDelete = p;
                }
            }

            _products.Remove(productToDelete);

            //LINQ - Language Integrated Query -> Liste bazlı yapıları aynen sql'deki filtreleme imkanı veriyor. Yukarıdaki kodu çok daha az eforla linq sayesinde yazabilirim.

            //productToDelete = _products.SingleOrDefault(p=>p.ProductId == product.ProductId);

            //SingleOrDefault metodu sayesinde _products'daki nesneleri tek tek dolaşma imkanı kazanıyorum. Buradaki p foreach'da dolaşırken kullandığım p ile aynı işleve sahip. Sadece takma isim. _products listesinin içinde her p için metodda parametre olarak gelen product'ın id'sine eşit ise o p'nin referansini al ve ona eşitle demek. id ile yapılan işlemlerde genellikle SingleOrDefault ama FirsOrDefault veya First'de kullanabilirim bunun yerine. SingleOrDefault 2 tane dönerse hata verir Id bazlı yapılarda kullanmamızın sebebi bu. 
        }

        public Product Get(Expression<Func<Product, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public List<Product> GetAll()
        {
            return _products;
        }

        public List<Product> GetAll(Expression<Func<Product, bool>> filter = null)
        {
            throw new NotImplementedException();
        }

        public List<Product> GetAllByCategory(int categoryId)
        {
           return _products.Where(p=> p.CategoryId == categoryId).ToList();
        }

        public List<ProductDetailDto> GetProductDetails()
        {
            throw new NotImplementedException();
        }

        public void Update(Product product)
        {   
            //Delete ile aynı mantık. Hangi product güncellenecek onu arıyorum.

            Product productToUpdate =_products.SingleOrDefault(p => p.ProductId == product.ProductId);

            //Bulduktan sonra güncelleme işlemlerini gerçekleştiriyorum.

            productToUpdate.ProductName = product.ProductName;

            productToUpdate.CategoryId = product.CategoryId;

            productToUpdate.UnitPrice = product.UnitPrice;

            productToUpdate.UnitsInStock = product.UnitsInStock;

            //Metoddali product nesnem kullanıcının ekrandan yolladığı data aslında. Elimdeki dataları gelen bilgiye göre güncelliyorum.

        }
    }
}

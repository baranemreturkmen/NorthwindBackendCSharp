using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfProductDal : EfEntityRepositoryBase<Product, NorthwindContext>, IProductDal
    {

        //EfEntityRepositoryBase<Product,NorthwindContext> genel crud operasyonları içeriyor. IProductDal ise sadece Product'a ait özel operasyonları içerecek.

        //public void Add(Product entity)
        //{   
        //    //Bir class'ı newlediğimde o class ile işim bitince belli bir zaman sonra Garbage Collector gelir ve o class'ı atar. using bloğu içerisinde yazdığım nesneler using bitince anında silinir. Garbage Collector'e kendi gider ve beni bellekten at der. Bunu yapmamın sebebi Northwind context'in bellek için çok pahalı olması. Yazmasam da olur using içerisinde ama belleği yoran fazla yer kaplayan işler için bunu yapmak önemli. 
        //    using (NorthwindContext context = new NorthwindContext())
        //    {
        //        //import işlemi yaptığımız using ile aynı şey değil bu using bloğu bu arada. IDisposable pattern implementation of c#.


        //        //context.Entry(entity) ile git veri kaynağından metodda parametre olarak gelen Product'ı bir tanesi ile eşleştir. Referansları farklı olacağı için eşleştirmem lazım. Ama ekleme işlemi olacağı için zaten veri kaynağında yok, eklemek istediğim nesne bu yüzden bunu belirtmem gerek. 

        //        var addedEntity = context.Entry(entity); //ilişkilendirdim veri kaynağım ile. Referansı yakaladım.

        //        addedEntity.State = EntityState.Added; //ekleme yap, bu eklenecek bir nesne diyorum.

        //        context.SaveChanges(); //ve ekle. 


        //    }
        //}

        //public void Delete(Product entity)
        //{
        //    using (NorthwindContext context = new NorthwindContext())
        //    {
        //        var deletedEntity = context.Entry(entity);

        //        deletedEntity.State = EntityState.Deleted;

        //        context.SaveChanges();  

        //    }
        //}

        //public Product Get(Expression<Func<Product, bool>> filter)
        //{
        //    using (NorthwindContext context = new NorthwindContext())
        //    {
        //        return context.Set<Product>().SingleOrDefault(filter);
        //    }
        //}

        //public List<Product> GetAll(Expression<Func<Product, bool>> filter = null)
        //{
        //    using (NorthwindContext context = new NorthwindContext())
        //    {
        //        //Eğer filtre null ise :'den önceki kısım çalışır, null değilse :'nin sağı yani 2. kısım çalışır. DbSet'deki product tablosuna yerleşiyorum. Daha sonra veri tabanında ki bütün tabloyu listeye çeviriyorum. Yani arka planda bizim için bu yapı select * from products döndürüyor. 
        //        return filter == null ? context.Set<Product>().ToList() : context.Set<Product>().Where(filter).ToList();

        //        //:'nin sağ tarafında context.Set<Product>().Where(filter).ToList(); gelen filtreyi Where'in içerisine vermeyi unutma!!! Sonuçta filtre null değilse burası çalışacak filtereleme işlemi için Where ve filtrenin kendisine ihtiyacım var.

        //    }
        //}

        //public void Update(Product entity)
        //{
        //    using (NorthwindContext context = new NorthwindContext())
        //    {
        //        var updatedEntity = context.Entry(entity);

        //        updatedEntity.State = EntityState.Modified;

        //        context.SaveChanges();

        //    }
        //}

        //Burada yazdığım kodlar diğer Dal classlar içinde tekrar eden yapıda olduğu için Core'a taşıdım. Core.DataAccess.EntityFramework'un altında. 
        public List<ProductDetailDto> GetProductDetails()
        {
            using (NorthwindContext context =new NorthwindContext())
            {
                var result = from p in context.Products join c in context.Categories on p.CategoryId equals c.CategoryId select new ProductDetailDto { ProductId = p.ProductId, ProductName = p.ProductName, CategoryName = c.CategoryName, UnitsInStock = p.UnitsInStock };

                //Yeni dto tablomda neyi istiyorsam onu gösteriyorum yukarıda. Yeni dto tablosunda göstermek istediğim kolonların Products ve Categories tablolarına ait olduğuna dikkat edilsin.

                return result.ToList();

                //result IQueryable dediğimiz bir döngü türü olduğu için listeye çevirdim. 
            }

            
        }
    }
}

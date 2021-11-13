using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.DataAccess.EntityFramework
{   

    //EntityFramework'u base bir repository haline getirdik ve DataAccess'i rahatlattık. 

    public class EfEntityRepositoryBase<TEntity,TContext>:IEntityRepository<TEntity>
        where TEntity:class,IEntity,new()
        where TContext:DbContext,new()
    {
        public void Add(TEntity entity)
        {
            //Bir class'ı newlediğimde o class ile işim bitince belli bir zaman sonra Garbage Collector gelir ve o class'ı atar. using bloğu içerisinde yazdığım nesneler using bitince anında silinir. Garbage Collector'e kendi gider ve beni bellekten at der. Bunu yapmamın sebebi Northwind context'in bellek için çok pahalı olması. Yazmasam da olur using içerisinde ama belleği yoran fazla yer kaplayan işler için bunu yapmak önemli. 
            using (TContext context = new TContext())
            {
                //import işlemi yaptığımız using ile aynı şey değil bu using bloğu bu arada. IDisposable pattern implementation of c#.


                //context.Entry(entity) ile git veri kaynağından metodda parametre olarak gelen Product'ı bir tanesi ile eşleştir. Referansları farklı olacağı için eşleştirmem lazım. Ama ekleme işlemi olacağı için zaten veri kaynağında yok, eklemek istediğim nesne bu yüzden bunu belirtmem gerek. 

                var addedEntity = context.Entry(entity); //ilişkilendirdim veri kaynağım ile. Referansı yakaladım.

                addedEntity.State = EntityState.Added; //ekleme yap, bu eklenecek bir nesne diyorum.

                context.SaveChanges(); //ve ekle. 


            }
        }

        public void Delete(TEntity entity)
        {
            using (TContext context = new TContext())
            {
                var deletedEntity = context.Entry(entity);

                deletedEntity.State = EntityState.Deleted;

                context.SaveChanges();

            }
        }

        public TEntity Get(Expression<Func<TEntity, bool>> filter)
        {
            using (TContext context = new TContext())
            {
                return context.Set<TEntity>().SingleOrDefault(filter);
            }
        }

        public List<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null)
        {
            using (TContext context = new TContext())
            {
                //Eğer filtre null ise :'den önceki kısım çalışır, null değilse :'nin sağı yani 2. kısım çalışır. DbSet'deki product tablosuna yerleşiyorum. Daha sonra veri tabanında ki bütün tabloyu listeye çeviriyorum. Yani arka planda bizim için bu yapı select * from products döndürüyor. 
                return filter == null ? context.Set<TEntity>().ToList() : context.Set<TEntity>().Where(filter).ToList();

                //:'nin sağ tarafında context.Set<Product>().Where(filter).ToList(); gelen filtreyi Where'in içerisine vermeyi unutma!!! Sonuçta filtre null değilse burası çalışacak filtereleme işlemi için Where ve filtrenin kendisine ihtiyacım var.

            }
        }

        public void Update(TEntity entity)
        {
            using (TContext context = new TContext())
            {
                var updatedEntity = context.Entry(entity);

                updatedEntity.State = EntityState.Modified;

                context.SaveChanges();

            }
        }
    }
}

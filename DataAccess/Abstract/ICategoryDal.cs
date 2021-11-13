using Core.DataAccess;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface ICategoryDal:IEntityRepository<Category>
    {
        //Normalde interface'in kendisi public değil. Operasyonları public bu aradaki farkı unutma!

        //Burada IProductDal'da yaptığım işlerin hemen hemen aynısının yapıyorum. O zaman bir interface oluşturalım burada ve IProductDal ve daha sonraki tablolara karşılık gelen interfacelere ait işlemleri o interface devralsın. Bizim buradaki interfacelerimizde generic bir şekilde o interface'i devralsın tekrar tekrar aynı şeyleri yapmayalım. Buna Generic Repository Design Pattern deniyormuş. 
        //List<Category> GetAll();

        //void Add(Category category);

        //void Update(Category category);

        //void Delete(Category category);

        //List<Category> GetAllByCategory(int categoryId);
    }
}

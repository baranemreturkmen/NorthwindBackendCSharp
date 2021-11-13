using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.DataAccess
{   

    //Generic constraint -> generic kısıt -> T tipi sadece veri tabanı nesnelerini alabilsin istiyorum.
    //class : referans tip olabilir demek. Çoğunlukla class olabilir diye zannedilir ama referans olabilir anlamında. 
    //Ama bu seferde bu adam burada herhangi bir class yazabilir. Ben herhangi bir class yazmasını istemiyorum. Veri tabanına ait classlar sadece yazılsın istiyorum. Bu durumda ya IEntity nesnesi olabilir referans tipim yada IEntity'den türeyen bir referans, nesne olabilir yani veri tabanına ait olma zorunluluğu geldi. Fakat buraya IEntity geldiğinde sistem patlayacak çünkü ben sadece IEntity'den türeyen veri tabanı nesnelerini istiyorum. Bu durumda new() kullanırım. new()-> newlenebilir olmalı demek. IEntity interface'i newlemez olduğu için otomatikman devre dışı kaldı. 

    public interface IEntityRepository<T> where T:class,IEntity,new()
    {

        //Expression<Func<T,bool>> filter=null sayesinde filtreleme işlemleri yapabiliyorum. Farklı filtreleme işlemler için ayrı ayrı metodlar yazmama gerek kalmıyor. Delegate diyorlar bu yapıya. filter = null diyerek filtre verilmemesi durumunda T type'ı hangi tipse Product, Category vs. tüm hepsini çekiyorum. 

        List<T> GetAll(Expression<Func<T,bool>> filter=null);

        T Get(Expression<Func<T,bool>> filter);

        void Add(T entity);

        void Update(T entity);

        void Delete(T entity);

    }

    //NuGet -> .Net Framework'e ait paket ihtaçlarımı Nuget üzerinden karşılıyorum, bir çeşit paket yöneticisi. EntityFramework'de ORM object relational mapping. Veritabanına ait tabloları class yapıyor ve kodlarımıza entegre ediyor. 
}

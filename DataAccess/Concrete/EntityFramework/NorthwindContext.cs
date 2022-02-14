using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{   

    //Bu class db tablolaları ile proje classlarını bağlayacak, ilişkilendirecek. 

    //İsmini context verince bu class'ım context olmuş olmuyor. EntityFramework ile gelen base context class'ı inherit etmem gerekiyor.
    public class NorthwindContext:DbContext
    {

        //Bu metod benim projem hangi veri tabanı ile ilişkiliyi belirteceğim yer aslında.
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            //Bağlantıyı ayarladım nereye bağlanacağımı görüyorum. Kullanıcı adı ve şifrem veri tabanına bağlanırken görünsün istemiyorum bundan dolayı Trusted_Connection=true'ı kullandık.

            optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=Northwind;Trusted_Connection=true");

            
        }
        //Veri tabanımın ne olduğunu söyledim ama hangi nesne hangi nesneye karşılık gelecek? Yani hangi class'ım hangi tabloya karşılık gelecek?  

        //Buradaki Products tablo birden fazla satır var birden fazla Product var içerisinde o yüzden çoğul. Ama Product tek bir satıra tek bir ürüne karşılık geliyor. EntityFramework'de arka planda tekilleştirme çoğullaştırma servisi var o çalışıyor. 

        public DbSet<Product> Products { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Order> Orders { get; set; }

    }
}

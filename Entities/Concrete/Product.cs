using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Product:IEntity
    {
        //Product entitisine DataAccess ve Business katmanlarından erişmek zorundayım bundan dolayı public erişim belirleyicisini seçtik. Default olarak Csharpta erişim belirleyicisi internal. Internal olduğu zaman bu class sadece Entities projesi altından erişebiliyorum. Proje bazlı, dll bazlı bir erişim belirleyici.

        public int ProductId { get; set; }


        public int CategoryId { get; set; }

        public string ProductName { get; set; }

        public short UnitsInStock { get; set; }

        public decimal UnitPrice { get; set; }


    }
}

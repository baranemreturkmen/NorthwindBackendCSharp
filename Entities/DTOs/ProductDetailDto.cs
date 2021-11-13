using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class ProductDetailDto:IDto
    {
        //DTO -> Data Transfer Object
        //IEntities'i implemente etmeyecek çünkü bu bir tabloya karşılık gelmiyor veritabanında ki ben burada 2 tablonun inner join'inini kullanmak istiyorum. 
        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public string CategoryName { get; set; }

        public short UnitsInStock { get; set; }
    }
}

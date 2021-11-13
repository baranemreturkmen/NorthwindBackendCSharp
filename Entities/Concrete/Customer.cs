using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Customer:IEntity
    {   
        //Ozelliklerim Customer tablosundaki sütun degerlerine karşılık geliyor bundan dolayı veri tipinin tablodaki sütun değerleri ile uyumlu olması önemli. 

        public string CustomerId { get; set; }

        public string ContactName { get; set; }

        public string CompanyName { get; set; }

        public string City { get; set; }
    }
}

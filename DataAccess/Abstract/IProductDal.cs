using Core.DataAccess;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IProductDal:IEntityRepository<Product>
    {
        //IEntityRepository'e ragmen neden ayrıca IProductDal yazdım çünkü bu tarz Product nesnesine ait Dto'ları burada kullanacağım.

        List<ProductDetailDto> GetProductDetails();

    }
}

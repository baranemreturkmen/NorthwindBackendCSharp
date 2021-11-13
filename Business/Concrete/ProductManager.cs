using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {
        //Dependency injection için nesnemi oluşturdum. InMemory'ye veya başka herhangibir EntityFramework vs. DataAccess nesnesine bağımlılığım yok. Sadece DataAccess'deki IProductDal üzerinden ilerliyorum. Yarın öbürgün DataAccess'de yöntem değiştirdiğim zaman yeni bir yöntem geldiğinde gelen yeni yöntem IProductDal'ı implemente edeceği için IProductDal üzerinden dependency injection yaptığımdan dolayı hiçbir bağımlılığım yok şuan Business katmanında.!!!
        IProductDal _productDal;

        //Constructor ile yani constructor injection yaparak dependency injection yapıyorum.
        public ProductManager(IProductDal productDal)
        {
            _productDal = productDal;
        }

        public IResult Add(Product product)
        {
            
            _productDal.Add(product);

            //Business Codes

            if (product.ProductName.Length<2)
            {
                //magic strings -> stringleri ayrı ayrı yazma durumu şöyle bir problem doğuruyor her yerde mesela aşağıdaki mesajı tekrarlıyorum ama farklı şekilerde mesela 2. kez bu mesajı verdiğimde şöyle verdiğimi düşün ->Ürün ismi EN AZ(minimum kullandın burada ama) 2 karakter olmalıdır. Bu durumda standart olmuyor kurumsallıktan uzaklaşıyorum.
                return new ErrorResult(Messages.ProductNameInvalid);
            }

            return new SuccessResult(Messages.ProductAdded);

            //IResult SuccessResult'in referansını tutabilir. Result IResult'ı implemente etmişti. SuccessResult'da Result'ı inherit etmişti.

            //return new SuccessResult(); mesaj vermeden böyle de yollayabilirim Result'ı ve ondan türeyen class'ları yazarken buna uygun bir yapı kuruldu.
        }

        public IDataResult<List<Product>> GetAll()
        {
            //İş Kodları-Kuralları Burada

            //InMemoryProductDal inMemoryProductDal = new InMemoryProductDal(); 

            //Burada bir bağımlılık oluştu aslında. DataAccess tarafında kullanmak istediğim sistem değiştiği zaman projemde inMemoryProductDal nesnesini kullandığım her yeri değiştirmek zorunda kalacağım. Bundan dolayı Business katmanında aslında herhangi bir sınıfı newleyip bağımlılık oluşturmuyoruz. Bunun yerine kullanacağımız yöntem dependency injection olacak.!!!

            if(DateTime.Now.Hour == 22)
            {
                return new ErorrDataResult<List<Product>>(Messages.MaintenanceTime);
            }

            return new SuccessDataResult<List<Product>>(_productDal.GetAll(),Messages.ProductsListed);

            //Dependency injection sayesinde DataAccess'de bulunan metodumu çağırma imkanı buldum ve Business katmanım ile DataAccess katmanımı birbirine bağlamış oldum. Business katmanında kullanıcının isteği iş kurallarından geçerse return _productDal.GetAll(); çalışacak ve DataAccess'e erişmiş olacağım. 
        }

        public IDataResult<List<Product>> GetAllByCategoryId(int id)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(p =>p.CategoryId == id));

            //lambda ifadesi ile filtreleme yaparken p'nin Product'ın sahip olduğu propertyleri otomatik algıladığını fark ettim. Bunun sebebi de GetAll yaparken _productDal üzerinden GetAll yapıyoruz ve _productDal IProductDal tipinde. IProductDal ise base interface'im olan IEntity'i inherit ederken Product tipinde (base interface'i generic oluşturduk.) inherit ediyor.
        }

        public IDataResult<Product> GetById(int productId)
        {
            return new SuccessDataResult<Product>(_productDal.Get(p=>p.ProductId == productId));
        }

        public IDataResult<List<Product>> GetByUnitPrice(decimal min, decimal max)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(p=>p.UnitPrice>=min && p.UnitPrice<=max));

        }

        public IDataResult<List<ProductDetailDto>> GetProductDetails()
        {
            return new SuccessDataResult<List<ProductDetailDto>>(_productDal.GetProductDetails());
        }
    }
}

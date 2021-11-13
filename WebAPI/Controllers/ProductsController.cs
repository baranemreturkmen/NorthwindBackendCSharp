using Business.Abstract;
using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    //[ApiController] attribute diyoruz C#'ta Javada attribute farklı birşey javada annotation diyoruz bu yapıya. Attribute -> bir class ile ilgili bilgi verme o class'ı imzalama yöntemidir. Bu class'ın bir controller görevi üstlendiğini söylüyoruz .Net'e.

    //[Route("api/[controller]")] ise bize nasıl istekte bulunacaklarını söylüyor yani biz bu isteği yaparken insanlar karşı taraf bize nasıl ulaşacak. 

    public class ProductsController : ControllerBase
    {
        //Loosly coupled -> Bir bağımlılık var ama soyuta bağımlılık var. Benim manager'ım değiştiği zaman herhangi bir problemle karşılaşmayacağız.

        //alanı _ ile vermek .Net projelerinde sıklıkla yapılan bir naming convention
        IProductService _productService;

        //fieldlar default olarak private'dır bu arada.

        //Javascriptte constructor injection yapmamıza gerek yok bu arada, constructor parametresi olarak gelen productService'i constructor dışında kullanmak amacıyla constructor injection yapıyorum. Bu durum da java'ya ve c#'a özgü. Javascriptte constructor parametresini constructor injection'a gerek duymadan aynen kullanabilirim.
        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        //Soyutlama üzerinden loosly coupled yaparak yani interface üzerinden bir bağımlılık inşa ettik ama bu sefer de bu interface üzerinden hangi manager class'ından data aldığımızı bilmiyoruz. Birden fazla manager IProductService'i implemente edebilir(bizim kodumuzda şuan için bir tane ediyor ama buna rağmen bulamıyor List<Product> Get metodunun içinde) ve bu durumda IProductService'i implemente eden manager'ı burada belirtmem gerek. new Product Manger yaptık az önce dependency chain oluştu bağımlılığımız arttı. Şuan ki sorun IProductService'e ait elimizde somut bir referans olmaması. Bu durumda benim bu sorunu aşmam için IoC Container kullanmam lazım.

        //IoC Container-> Inversion of Control -> Değişimin kontrolü. Aslında IoC container bellekte bir yer ve bu yere referanslar koyuyoruz ve bu referanslara kim ihtiyaç duyuyorsa ona ihtiyaç duyulan referansı IoC container sayesinde temin ediyoruz.Yani burada ProductsController'a diyoruz ki biliyorum bir IProductService ihtiyacın var ben senin için bellekte bir tane newledim sana onu veriyorum diyorum.
        
        //Asp .Net web api IProductService'i gördüğü anda bizim yerimize gidip IoC container'a bakacak ve ona karşılık gelen IProductService'a karşılık gelen referansı bulacak ve kullanacak. Web api'nin kendi içinde bir IoC yapısı var.
           
        [HttpGet("getall")]
        public IActionResult GetAll()
        {   
            //Dependency chain --> bağımlılık zinciri.
            //IProductService productService = new ProductManager(new EfProductDal());

            //Gerçek hayatta bu apiyi geliştirenler bu api bize ne zaman ne veriyor tarzında dökümantasyon sağlarlar. Swagger gibi ürünler var bunu sağlayan.
            var result = _productService.GetAll();

            if(result.Success)
            {   
                //İşlem başarılıysa Datayı verelim sadece result.Data
                return Ok(result);
            }
            
            //İşlem başarısız ise hata mesajını verelim. result.Message vs. yapabilirim istersem.
            return BadRequest(result);
        }

        //Ürün alıcam controller'ın bilgiği yer burası çünkü post attribute'u burada o yüzden almak istediğim nesneyi parametre olarak bu metodda geçiyorum. Client'dan elde ettiğim ürünü ekliyorum burada. Benim için şuan swagger. Frontend'in dahil olmasıyla birlikte bu react angular vue ile yazılan herhangi bir site veya mobil uygulama olacak. Bu noktada merkez olarak siteyi düşün. Referansın site-uygulama. Get-> Almak. Site verileri backendden alıyor. Mesela kullanıcı ön tarafta bazı ürünleri görmek istiyor. Post -> yollamak site backend'e veri yolluyor. Mesela kullanıcı siteye ürün ekliyor vs. vs.

        [HttpGet("getbyid")]
        //Bu noktada HttpGet'i default olarak bırakırsam hata alırım çünkü aşağıdaki get metodum parametre aldı imzası farklı ama buna rağmen Swagger tarafında yada postman tarafında vs. hata alıcam çünkü benim bu noktada 2 adet aynı endpoint'im var Get() işlemini yapan. Client tarafı sen hangi Get() işlemini yapmak istiyorsun ben anlamadım diye hata verecektir. Bu noktada requestlerimizin içerisine isim vermek daha doğru olacaktır. İsimlerle alias veriyoruz veya routingde tercih edilebilirdi. (Daha önceden Get idi id ile işlem yapan ve tüm ürünleri getiren metodların ismi aynıydı imzalar aynı olmamamasına rağmen hata almıştık. Requestlere alias verdik ve okunurluk artsın diye metod isimlerini de requestlere verdiğimiz aliaslara uygun şekilde verdik.)
        public IActionResult GetById(int id)
        {
            var result = _productService.GetById(id);
            if(result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("add")]
        public IActionResult Add(Product product)
        {
            var result = _productService.Add(product);

            if(result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        //Aynı mantıkla güncelleme ve silme için post'u kullanabilirim sonuçta client tarafından veri tabanında ki bilgiyi değiştiriyorum. Ama güncellemeler için HttpPut ve silme için HttpDelete'de kullanılabiliyor ayırmak için. Ama sektörde %99 güncelleme ve silme için de post kullanılıyor. 
    }
}

using Business.Abstract;
using Business.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            
            
            //IoC arka planda newleme iþini bizim için yapacak. Birisi senden IProductService isterse constructorda, arka planda ProductManager oluþtur ve onu ver diyoruz. Yani controller'a IProductService içeren bir baðýmlýlýk görürsen karþýlýðý ProductManager olsun diyoruz. Singleton tüm bellekte bir tane ProductManager oluþturuyor ve birçok client'a ayný instance'ý veriyor. Bu durum bizi birçok instance üretiminden kurtarýp daha performanslý bir yapýya ulaþtýrýyor. Ama singleton uygulayabilmek için üretilen instance'ýn içerisinde data tutmamasý lazým çünkü bu durumda ayný instance'ý birçok client'a geçeceðimiz için ayný datayýda birçok client'a yollamýþ oluruz. Bu arada bunlar javada ki bean konfigürasyonlarýna karþýlýk geliyor. 

            
                                services.AddSingleton<IProductService, ProductManager>();

            //AddSingleton yeterli olmayacak bu arada çünkü ProductManager'ý newlerken ProductManager'ýnda IProductDal'a baðýmlý olduðunu gördüm.O zaman ben bu baðýmlýlýðý da set etmeliyim.

            //Eðer biri benden IProductDal isterse ona EfProductDal'ý ver diyorum.

            //Bu yapýyý ileride daha farklý bir mimariye taþýyor olacaðýz. Autofac, Ninject, CastleWindsor, StructureMap, LightInject, DryInject gibi. .Net'in kendi içinde IoC altyapýsý yokken bu adamlar bu altyapýyý sunuyordu. Klasik mvc mimarisinde built in IoC mimarisi yoktu. .Net'in kendi IoC alt yapýsý varken neden bu adamlara hala ihtiyaç duyuluyor? Projenin ilerleyen kýsýmlarýnda AOP yapýlacak.

            //AOP -> Ben bütün metodlarýmý loglamak istiyorum. Normalde ILoggerService.log() diye bir metod çaðýrýrým. Bunun yerine metodun üzerine þöyle bir attribute koyacaz -> [LogAspect] bu da git bu metodu yokla anlamýna gelecek. Çünkü loglama iþini de artýk business'a taþýmak istemiyorum. Business sadece business yapsýn istiyorum. Bu yapý bu arada SpringBoot'da default olarak var. Veya diyecez ki [Validate] doðrulama iþlemi yap, [Cache] cache'le, [RemoveCache] ürüneklendi cache'i uçur [Transaction] hata var transaction'ý bitir, [Performance] bu metodun çalýþmasý 5 saniyeyi geçerse beni uyar vs. Sadece metoda deðil class'a veya arka planda baþka yerlere de bu attributelarý ekleyebilirim bu olaya AOP deniyor. Autofac özellikle AOP konusunda diðerlerine göre daha iyi. Bundan dolayý .Net'in kendi IOC containerýna autofac'i enjekte edeceðiz.
            services.AddSingleton<IProductDal,EfProductDal>();

            //Vs2019 swagger'ý burada uygulamýþ projeye eklemiþ.
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebAPI", Version = "v1" });
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebAPI v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

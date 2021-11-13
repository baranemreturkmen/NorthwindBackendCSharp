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
            
            
            //IoC arka planda newleme i�ini bizim i�in yapacak. Birisi senden IProductService isterse constructorda, arka planda ProductManager olu�tur ve onu ver diyoruz. Yani controller'a IProductService i�eren bir ba��ml�l�k g�r�rsen kar��l��� ProductManager olsun diyoruz. Singleton t�m bellekte bir tane ProductManager olu�turuyor ve bir�ok client'a ayn� instance'� veriyor. Bu durum bizi bir�ok instance �retiminden kurtar�p daha performansl� bir yap�ya ula�t�r�yor. Ama singleton uygulayabilmek i�in �retilen instance'�n i�erisinde data tutmamas� laz�m ��nk� bu durumda ayn� instance'� bir�ok client'a ge�ece�imiz i�in ayn� datay�da bir�ok client'a yollam�� oluruz. Bu arada bunlar javada ki bean konfig�rasyonlar�na kar��l�k geliyor. 

            
                                services.AddSingleton<IProductService, ProductManager>();

            //AddSingleton yeterli olmayacak bu arada ��nk� ProductManager'� newlerken ProductManager'�nda IProductDal'a ba��ml� oldu�unu g�rd�m.O zaman ben bu ba��ml�l��� da set etmeliyim.

            //E�er biri benden IProductDal isterse ona EfProductDal'� ver diyorum.

            //Bu yap�y� ileride daha farkl� bir mimariye ta��yor olaca��z. Autofac, Ninject, CastleWindsor, StructureMap, LightInject, DryInject gibi. .Net'in kendi i�inde IoC altyap�s� yokken bu adamlar bu altyap�y� sunuyordu. Klasik mvc mimarisinde built in IoC mimarisi yoktu. .Net'in kendi IoC alt yap�s� varken neden bu adamlara hala ihtiya� duyuluyor? Projenin ilerleyen k�s�mlar�nda AOP yap�lacak.

            //AOP -> Ben b�t�n metodlar�m� loglamak istiyorum. Normalde ILoggerService.log() diye bir metod �a��r�r�m. Bunun yerine metodun �zerine ��yle bir attribute koyacaz -> [LogAspect] bu da git bu metodu yokla anlam�na gelecek. ��nk� loglama i�ini de art�k business'a ta��mak istemiyorum. Business sadece business yaps�n istiyorum. Bu yap� bu arada SpringBoot'da default olarak var. Veya diyecez ki [Validate] do�rulama i�lemi yap, [Cache] cache'le, [RemoveCache] �r�neklendi cache'i u�ur [Transaction] hata var transaction'� bitir, [Performance] bu metodun �al��mas� 5 saniyeyi ge�erse beni uyar vs. Sadece metoda de�il class'a veya arka planda ba�ka yerlere de bu attributelar� ekleyebilirim bu olaya AOP deniyor. Autofac �zellikle AOP konusunda di�erlerine g�re daha iyi. Bundan dolay� .Net'in kendi IOC container�na autofac'i enjekte edece�iz.
            services.AddSingleton<IProductDal,EfProductDal>();

            //Vs2019 swagger'� burada uygulam�� projeye eklemi�.
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

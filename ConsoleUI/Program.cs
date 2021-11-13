using Business.Concrete;
using DataAccess.Concrete.InMemory;
using DataAccess.Concrete.EntityFramework;
using System;

namespace ConsoleUI
{   

    //SOLID -> O-> Open Closed Principle -> Yazdığım koda yeni bir özellik ekliyorsam eğer daha önce yazdığım kodları değiştirmeden bunu yapabilmeliyim.
    class Program
    {
        static void Main(string[] args)
        {
            //Business katmanında dependency injection yaparken interface üzerinden yapmıştım. Interface InMemoryProdcutDal'ın referansını tutabilir çünkü InMemoryProdcutDal interface'i implemente etti.!!!

            ProductTest();

            
            //CategoryTest();
        }

        private static void CategoryTest()
        {
            //Şu an için newliyoruz  sonrasında IoC container ile yapacağız. 
            CategoryManager categoryManager = new CategoryManager(new EfCategoryDal());

            foreach (var category in categoryManager.GetAll())
            {
                Console.WriteLine(category.CategoryName);
            }
        }

        private static void ProductTest()
        {
            ProductManager productManager = new ProductManager(new EfProductDal());

            //foreach (var product in productManager.GetAll())
            //{
            //    Console.WriteLine(product.ProductName);
            //}

            //Console.WriteLine("-------------------------------------");

            //foreach (var product in productManager.GetAllByCategoryId(2))
            //{
            //    Console.WriteLine(product.ProductName);
            //}

            //Console.WriteLine("--------------------------");

            //foreach (var product in productManager.GetByUnitPrice(40, 100))
            //{
            //    Console.WriteLine(product.ProductName);
            //}

            var result = productManager.GetProductDetails();

            if(result.Success == true)
            {
                foreach (var product in result.Data)
                {
                    Console.WriteLine(product.ProductName + "/" + product.CategoryName);

                    //Dto tablomda gostermek istediğim kolonları belirlemiştim EfProductDal'da GetProductDetails() metodunun içinde linq ile. Burada da o seçtiğim kolonların arasından ProductName ile CategoryName'i console'a yazdırmayı tercih ettim.
                }
            }

            else
            {
                Console.WriteLine(result.Message);
            }

            
        }
    }
}

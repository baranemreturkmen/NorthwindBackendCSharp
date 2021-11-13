using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Constants
{
    public static class Messages
    {
        //Ben bu class'ı her mesaj verdiğimde kullanacağım. O zaman bu class'ı sürekli newleyeceğim. Bu durumda belleği yormuş olacağım o zaman static yapalım. Uygulama hayatı boyunca bu class'ın sadece bir instance olacak sabir bir şekilde bellekte hep kalacak GarbageCollector'e takılmadan ve ben hep o instance'ı kullanacağım.

        //Static class'ın field'ları da static olmak zorunda.
        
        public static string ProductAdded = "Ürün eklendi.";

        //Bu intro seviyesinde bu yapıları daha profesyonel öğrenebilmek için çok dil destekli vs. DevArchitecture'a göz at.

        //Basit bir değişken olmasında rağmen fieldlarım büyük harfle PascalCase ile      yazdım çünkü public olduğu için. private bir field olsaydı camelCase yazardı.
        public static string ProductNameInvalid = "Ürün ismi geçersiz.";
        internal static string MaintenanceTime = "Sistem bakımda.";
        internal static string ProductsListed = "Ürünler listelendi.";
    }
}

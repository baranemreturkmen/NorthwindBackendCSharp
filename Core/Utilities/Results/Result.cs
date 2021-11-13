using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Results
{   
    //Utilities altındaAbstract ve Concrete diye klasörleme  işine girişmedik. Biraz overdesign'a kaçıyordu. Ama istenirse yapılabilir.

    public class Result : IResult
    {
        //getter read only'dir fakat constructor'da set edilebilir.

        //Başarılı veya başarısız vs. tarzında mesajlar ile dönerken constructor yapısı ile dönmeyi tercih ettik. Programcı kodlara ekleme yaparken kafasına göre setter ile dönmesini istemedik. 
        public Result(bool success, string message):this(success)
        {
            Message = message;
            //Success = success;

            //Kod tekrarı yapmıyorum yukarıdaki yöntemle 2 parametre göndersem bile Success için aşağıdaki 2. constructor'ı çağırıyorum.

            //this demek class'ın kendisi demek this(success) ile aşağıdaki 2. constructor'ı işaret ediyorum.
        }

        //Mesaj dönmek istemessem kullanıcıya 2. constructor devreye girecek. Method overloading yaptık. Metodlar aynı ama imzalar farklı.
        public Result(bool success)
        {
            Success = success;
        }

        //Gelen mesajları bu propertyler sayesinde okuyorum. Propertylerin tek amacı gelen mesajı kullanıcıya göstermek. Constructor sayesinde set yapıyordum. Bundan sadece read only yaptık.
        public bool Success { get; }

        public string Message { get; }
    }
}

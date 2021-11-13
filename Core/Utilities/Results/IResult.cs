using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Results
{   
    //Temel void metodların result döndürebilmesi için bu interface'den faydalanacağız. 
    public interface IResult
    {   
        //Property sadece okuma yapıyor. Set işlemini constructor ile gerçekleştireceğiz. 

        //İşlem başarılı mı başarısız mı?
        bool Success { get; }

        //Kullanıcıya işlem hakkında bilgi verme amaçlı. 
        string Message { get; }

        //Interface'in içerisine nasıl ki metod verebiliyorsak property'de verebilirsin.
    }
}

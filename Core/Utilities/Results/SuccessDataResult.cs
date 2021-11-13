using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Results
{
    public class SuccessDataResult<T>:DataResult<T>
    {   
        //Farklı farklı imkanlar tanıdım alternatif yapılar oluşturdum ister data ister mesaj ver istersen hiçbirşey geriye dönme tarzında. 
        public SuccessDataResult(T data,string message):base(data,true,message)
        {

        }

        public SuccessDataResult(T data):base(data,true)
        {

        }

        //Çalıştığım T'nin default'unu dönüyorum. Bir şey döndürmek istemiyorum onun yerine default halini dönüyorum. Default burada dataya karşılık geliyor örneğin return tipi int ama ben birşey döndürmek istemiyorum o zaman int'in default'unu geçsin diyebiliriz.

        //Son 2 constructor yapısını zaten çok fazla kullanmıyoruz.
        public SuccessDataResult(string message):base(default,true,message)
        {

        }

        public SuccessDataResult():base(default,true)
        {

        }

    }
}

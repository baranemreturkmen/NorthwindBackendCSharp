using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Results
{
    public class ErorrDataResult<T> : DataResult<T>
    {
        //Farklı farklı imkanlar tanıdım alternatif yapılar oluşturdum ister data ister mesaj ver istersen hiçbirşey geriye dönme tarzında. 
        public ErorrDataResult(T data, string message) : base(data, false, message)
        {

        }

        public ErorrDataResult(T data) : base(data, false)
        {

        }

        //Çalıştığım T'nin default'unu dönüyorum. Bir şey döndürmek istemiyorum onun yerine default halini dönüyorum. Default burada dataya karşılık geliyor örneğin return tipi int ama ben birşey döndürmek istemiyorum o zaman int'in default'unu geçsin diyebiliriz.

        //Son 2 constructor yapısını zaten çok fazla kullanmıyoruz.
        public ErorrDataResult(string message) : base(default, false, message)
        {

        }

        public ErorrDataResult() : base(default, false)
        {

        }

    }
}

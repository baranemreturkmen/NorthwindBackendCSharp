using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Results
{
    public class SuccessResult:Result
    {   
        //base ile ben Base class'ım olan Result'ın 1. constructor'ına ture yolluyorum başarılı anlamında. Message'da businessdan gelen mesajım. Success olduğu için zaten direkt true yolluyorum.
       
            
        public SuccessResult(string message):base(true,message)
        {

        }

        //Veya mesaj vermek istemiyor da olabiliriz. Bu durumda Base class'ın tek parametreli olan 2. constructor'ını (sadece success içeren) çağırırız.

        public SuccessResult():base(true)
        {

        }

        //Yani burada aslında şöyle bir yapı kurduk. IResult interface'inde en başta sadece getter içeren 2 tane property oluşturduk. message kullanıcıya döneceğim mesajı, success ise kullanıcının yaptığı işlemlerin business'da ki iş kurallarından geçip geçmemesine göre true yada false içeriyor. IResult interface'i base interface'im ve bu interface base class olan Result class'ı implement ediyor. IResult interface'inde ki propertylere karşılık implementasyon sonucu Result'da propertyler oluştu ama ben business tarafında kullanıcıya mesaj vereceksem mesaj verme işini constructorlar ile yapmak istiyorum. Zaten en başta base interface'de property yazarken yalnızca read only olmasının yani sadece getter içermesinin sebebi buydu. Elimde bir result var ama ben result'ın base class olmasını istiyorum yani kullanıcı isteği businessdan başarılı bir şekilde geçiyorsa başarılı mesaj veren yapı ayrı bir class olsun(SuccessResult), kullanıcı isteği businessda iş kurallarına takılıyorsa başarısız mesaj dönmem gerekiyorsa bu mesajı dönen başka bir class olsun(ErrorResult). Bu 2 class'da Result'ı inherit etsin. Öyle bir yapı kuralım ki hem SuccessResult hem de ErrorResult içerisinde kullanıcıya vermek istediğim mesajı constructorda bu 2 class üzerinde vereyim ama SuccessResult üzerinden base class'daki Success propertysine true vereyim, ErrorResult'da da base class'daki Success propertysine false vereyim bunu da bu 2 class'ın içerisinde base anahtar kelimesi ile sağlayayım.


        //Bu yapıları kurmak yerine direkt başarılı başarısız diye default değer de yollayabilirdik ama bu seferde sonrasında sistem içerisinde default değer'i biz kendimiz mesaj olarak koyduğumuz için aradan zaman geçince geliştirme yapmaya kalktığımızda default değerleri hangi durumda koyuyorduk vs. tarzında karıştırmaya veya unutmaya açık bir sistem geliştirmiş olacaktık.Burada success olur da geliştirici kullanıcı vs. sonucu kıyaslamak isterse diye var aslında. false ve true üzerinde gitmemizin sebebi bu biryerde aradan zaman geçse de geliştirici kıyaslama yaptığın karışıklık vs. olmasın.
    }
}

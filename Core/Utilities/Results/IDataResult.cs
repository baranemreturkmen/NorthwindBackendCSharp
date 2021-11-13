using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Results
{
    public interface IDataResult<T>:IResult
    {   
        //Hem mesaj dönen hem de data dönen durumlar için bu interface'e ihtiyacım var.
        T Data { get; }
    }
}

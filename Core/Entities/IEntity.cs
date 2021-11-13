using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public interface IEntity
    {
        //IEntity implemente eden class bir veritabanı tablosudur. 


        //Core katmanı diğer katmanları referans almaz. Alırsa referans aldığı katmana bağımlı olur. Core katmanı bağımsız olmak zorunda. Bu şekilde bağımsız yapıları core katmanı altında toplayarak kod bağımlılığından kurtuluyorum değişime açık oluyorum ve open-closed principle güçlenmiş oluyor. 
    }
}

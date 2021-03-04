using ApiExamen.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiExamen.Models;

namespace ApiExamen.Repository.IRepository
{
   public interface IReciboRepository
    {
        IEnumerable<Recibo> GetRecibos();
      
        bool RegistraRecibo(Recibo recibo);
        bool ActualizarRecibo(Recibo recibo);
        Recibo UnRecibo(int id);
        bool BorrarRecibo(Recibo recibo);
        bool Guardar();
        
    }
}

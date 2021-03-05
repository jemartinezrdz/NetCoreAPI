using ApiExamen.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiExamen.Repository.IRepository
{
   public interface IReciboRepository
    {
        IEnumerable<Recibo> GetRecibos();
        IEnumerable<Recibo> ultimoRecibo();

        bool RegistraRecibo(Recibo recibo);
        bool ActualizarRecibo(Recibo recibo);
        bool BorrarRecibo(Recibo recibo);
        bool Guardar();
        
    }
}

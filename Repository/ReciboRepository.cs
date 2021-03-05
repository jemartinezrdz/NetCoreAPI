using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiExamen.Data;
using ApiExamen.Models;
using ApiExamen.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ApiExamen.Repository
{
    public class ReciboRepository : IReciboRepository
    {
        private readonly AppDbContext _bd;

        public ReciboRepository(AppDbContext bd)
        {
            _bd = bd;
        }

        public bool ActualizarRecibo(Recibo recibo)
        {
            try
            {
                _bd.Database.ExecuteSqlRaw("EXEC spAdministrar_Recibos {0}, {1}, {2}, {3}, {4}, {5}",
                    recibo.proveedor,
                    recibo.monto,
                    recibo.moneda,
                    recibo.comentario,
                    "Actualizar",
                    recibo.idRecibo);
                return Guardar();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public bool BorrarRecibo(Recibo recibo)
        {
            try
            {
                _bd.Database.ExecuteSqlRaw("EXEC spAdministrar_Recibos {0}, {1}, {2}, {3}, {4}, {5}",
                    ".",
                    0,
                    ".",
                    ".",
                    "Borrar",
                    recibo.idRecibo);
                return Guardar();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public bool RegistraRecibo(Recibo recibo)
        {
            try
            {
                _bd.Database.ExecuteSqlRaw("EXEC spAdministrar_Recibos {0}, {1}, {2}, {3}, {4}, {5}",
                    recibo.proveedor,
                    recibo.monto,
                    recibo.moneda,
                    recibo.comentario,
                    "Insertar",
                    "");
                return Guardar();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
           
        }

        

        public IEnumerable<Recibo> GetRecibos()
        {
           
            var recibo = _bd.Recibo.FromSqlRaw("EXEC spAdministrar_Recibos");
            return recibo;
        }

        public IEnumerable<Recibo> ultimoRecibo()
        {
            var recibo = _bd.Recibo.FromSqlRaw("EXEC spAdministrar_Recibos {0}, {1}, {2}, {3}, {4}, {5}",
                    ".",
                    0,
                    ".",
                    ".",
                    "Ultimo",
                    "");
            return recibo;
        }

        public bool Guardar()
        {
            return _bd.SaveChanges() >= 0 ? true : false;
        }

        
    }
}

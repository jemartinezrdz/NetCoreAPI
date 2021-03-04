using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ApiExamen.Models
{
    public class Recibo
    {
        [Key]
        public int idRecibo { get; set; }
        public string proveedor { get; set; }

        public decimal? monto { get; set; }
        public string? moneda { get; set; }

        public string? comentario { get; set; }

        public DateTime fecha { get; set; }

    }
}

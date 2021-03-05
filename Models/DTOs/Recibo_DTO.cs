using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiExamen.Models
{
    public class Recibo_DTO
    {
        [Key]

        public int idRecibo { get; set; }
        [Required(ErrorMessage = "El valor de proveedor es obligatorio")]
        public string? proveedor { get; set; }
        [Required(ErrorMessage = "El valor de monto es obligatorio")]
        public decimal? monto { get; set; }
        [Required(ErrorMessage = "El valor de moneda es obligatorio")]
        public string? moneda { get; set; }
        [Required(ErrorMessage = "El valor de comentario es obligatorio")]
        public string? comentario { get; set; }

        public DateTime fecha { get; set; }



    }
}

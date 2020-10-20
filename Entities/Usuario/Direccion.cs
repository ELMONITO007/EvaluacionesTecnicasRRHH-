using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
   public class Direccion :EntityBase
    {
        public override int Id { get; set; }

        [DisplayName("Dirección")]
        [Required]


        [StringLength(20, ErrorMessage = "El maximo de caracteres es de 20")]

        public string direccion { get; set; }
    }
}

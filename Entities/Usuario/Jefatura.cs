using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
 public   class Jefatura :EntityBase
    {
        public override int Id { get; set; }
        [System.ComponentModel.DisplayName ("Jefatura")]
        [StringLength(20, ErrorMessage = "El maximo de caracteres es de 20")]
        public string jefatura { get; set; }
    }
}

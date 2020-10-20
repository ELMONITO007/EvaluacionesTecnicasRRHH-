using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{ 
  public  class Sector : EntityBase
    {
        public override int Id { get; set; }

        [System.ComponentModel.DisplayName("Sector")]
        [StringLength(20, ErrorMessage = "El maximo de caracteres es de 20")]
        public string sector { get; set; }
    }
}

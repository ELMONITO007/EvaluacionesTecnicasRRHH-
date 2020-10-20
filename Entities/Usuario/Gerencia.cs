using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
  public  class Gerencia : EntityBase
    {
        public override int Id { get; set; }
        [System.ComponentModel.DisplayName("Gerencia")]
        [StringLength(20, ErrorMessage = "El maximo de caracteres es de 20")]
        public string gerencia { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
   public class Direccion :EntityBase
    {
        public override int Id { get; set; }

        [DisplayName("Dirección")]
        public string direccion { get; set; }
    }
}

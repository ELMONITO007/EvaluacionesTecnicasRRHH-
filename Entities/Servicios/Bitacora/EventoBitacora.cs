using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Servicios.Bitacora
{
 public   class EventoBitacora :EntityBase
    {
        public override int Id { get; set; }
        [Required]
        [DisplayName("Evento")]
        public string eventoBitacora { get; set; }



    }
}

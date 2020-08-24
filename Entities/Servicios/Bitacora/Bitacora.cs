using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Servicios.Bitacora
{
   public class Bitacora :EntityBase
    {

        public override int Id { get; set; }
      
        public EventoBitacora    eventoBitacora { get; set; }

        [Required]
        [DisplayName("Identificador")]

        public string fecha { get; set; }
        public string hora { get; set; }
        public Usuarios usuarios { get; set; }


        public Bitacora()
        {
            usuarios = new Usuarios();
            eventoBitacora = new EventoBitacora();
        }



    }
}

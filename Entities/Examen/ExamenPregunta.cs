using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Examen
{
  public  class ExamenPregunta : EntityBase
    {
        public override int Id { get; set; }

        public Pregunta pregunta { get; set; }
        public Examen examen { get; set; }

        [DisplayName("¿Respuesta Correcta?")]
        public bool correcta { get; set; }


    }
}

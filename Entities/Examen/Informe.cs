using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Examen
{
  public  class Informe:EntityBase
    {
        public override int Id { get; set; }

        public int cantidadExamen { get; set; }
        public int cantidadAprobado { get; set; }
        public int cantidadDesaprobado { get; set; }
        public int cantidadPreguntas { get; set; }
        public int cantidadExamenTerminado { get; set; }
        public int cantidadArealizar { get; set; }
    }
}

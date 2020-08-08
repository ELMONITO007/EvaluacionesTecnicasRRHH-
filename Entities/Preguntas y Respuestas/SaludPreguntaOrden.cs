using Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Entities
{
   public class SaludPreguntaOrden : SaludPregunta
    {
        [DisplayName("¿Tiene mas de 3 Elementos?")]
        public bool MasDeTresElementos { get; set; }


        [DisplayName("¿Estan todos los Elementos?")]
        public bool VeriricarElemento { get; set; }


        [DisplayName("¿Falta el primer Elemento?")]
        public bool PrimerElemento { get; set; }

        public SaludPreguntaOrden()
        {
            pregunta = new Pregunta();
        }
    }
}

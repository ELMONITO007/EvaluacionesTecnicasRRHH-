using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Entities
{
public     class SaludPregunta
    {
        public Pregunta pregunta { get; set; }
        [DisplayName("¿Tiene asignado por lo menos una categoria?")]
        public bool TieneCategoria{ get; set; }

        [DisplayName("¿Se puede usar la respuesta en el Examen?")]
        public bool SaludDeLaPregunta { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
   public     class SaludPreguntaMultipleChoiceCompuesto :SaludPregunta
    {
        [DisplayName("¿Tiene Por lo menos 2 subpreguntas?")]
        public bool TieneMasDeDosSubpreguntas { get; set; }

        [DisplayName("¿Las subpreguntas tiene mas de 2 respuestas?")]
        public bool TieneMasDeDosRespuestas { get; set; }

        [DisplayName("¿Las subpreguntas tiene la pregunta correcta?")]
        public bool TienePreguntaCorrecta { get; set; }

    }
}

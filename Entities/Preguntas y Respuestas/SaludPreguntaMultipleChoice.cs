using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Entities
{
 public   class SaludPreguntaMultipleChoice : SaludPregunta

    {
        [DisplayName("¿Tiene una Respuesta correcta?")]
        public bool RespuestaCorrecta { get; set; }

        [DisplayName("¿Tiene mas de 3 Respuestas?")]
        public bool MasDeTresRespuestas { get; set; }

       
       public SaludPreguntaMultipleChoice()
        {
            pregunta = new Pregunta();
        }


    }
}

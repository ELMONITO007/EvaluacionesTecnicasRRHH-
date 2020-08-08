using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace Entities
{
  public  class MultipleChoiceCompuesto : Respuesta
    {
        public bool RespuestaObtenida { get; set; }
        [Required]
        [DataMember]
        [DisplayName("Identidicador SubPregunta")]
        public int id_SubPregunta { get; set; }

        public List<Pregunta> ListaSubPreguntas { get; set; }
        [Required]
        [DataMember]
        [DisplayName("Respuesta Correcta")]
        public bool Correcta { get; set; }
      public  Pregunta Pregunta = new Pregunta();

        public MultipleChoiceCompuesto()
        {
            ListaSubPreguntas = new List<Pregunta>();
            pregunta = new Pregunta();
        }


    }
}

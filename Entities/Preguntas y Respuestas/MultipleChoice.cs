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

    public  class MultipleChoice : Respuesta
    {
        public bool RespuestaObtenida { get; set; }
        public     List<MultipleChoice> ListaMultipleChoice ;
        [Required]
        [DataMember]
        [DisplayName("Respuesta Correcta")]
      
        public bool Correcta { get; set; }
        public MultipleChoice(Pregunta pr)
        {
            ListaMultipleChoice = new List<MultipleChoice>();
            pregunta = new Pregunta();
            pregunta = pr;
        }
        public List<Respuesta> respuestas { get; set; }
        public MultipleChoice()
        {
            ListaMultipleChoice = new List<MultipleChoice>();
            pregunta = new Pregunta();
            respuestas = new List<Respuesta>();
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
   public class SaludCategoria : EntityBase
    {
        [DataMember]
        public override int Id { get; set; }
        [DisplayName("¿Hay suficiente preguntas para hacer un examen?")]
        public bool SaludCategtoria { get; set; }

        [DisplayName("¿Tiene mas de 20 preguntas?")]
        public bool TieneMasDe20Preguntas { get; set; }
        [DisplayName("Preguntas con nivel Facil: ")]
        public int nivelBajo { get; set; }

        [DisplayName("Preguntas con nivel Medio: ")]
        public int nivelMedio{ get; set; }


        [DisplayName("Preguntas con nivel Dificil: ")]
        public int nivelAlto { get; set; }

        [DisplayName("¿Tiene mas de 20 preguntas que se puedan usar?")]
        public bool TieneMasDe20PreguntasConBuenaSalud { get; set; }
        [DisplayName("¿Cantidad de preguntas que tiene la categoria?")]
        public int CantidadDePreguntas { get; set; }
        public bool SaludRespuesta { get; set; }
        public Pregunta Pregunta { get; set; }
        public SaludPregunta saludPregunta { get; set; }
        public List<SaludCategoria> ListaPreguntasTotal { get; set; }

        public SaludCategoria()
        {
            saludPregunta = new SaludPregunta();
               Pregunta = new Pregunta();
            ListaPreguntasTotal = new List<SaludCategoria>();
        }
    }
}

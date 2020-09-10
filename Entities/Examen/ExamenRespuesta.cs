using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Examen
{
    public class ExamenRespuesta : EntityBase
    {
        public override int Id { get; set; }


        public List<ExamenRespuesta> examenRespuestas { get; set; }
        public Examen examen { get; set; }
        public Pregunta pregunta { get; set; }
        public bool respondio { get; set; }
        public MultipleChoice respuesta { get; set; }
        public bool correcta { get; set; }
        public Pregunta  subPregunta { get; set; }


        public ExamenRespuesta()
        {
            examen = new Examen();
            subPregunta = new Pregunta();
            pregunta = new Pregunta();
            respuesta = new MultipleChoice();
            examenRespuestas = new List<ExamenRespuesta>();
        }

    }
}

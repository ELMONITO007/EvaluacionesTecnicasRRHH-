using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
 public   class SaludPreguntaComponent
    {
       public SaludPregunta VerificarSalud(Pregunta pregunta)
        {
            if (pregunta.tipoPregunta.TipoDePregunta=="MultipleChoice")
            {
                SaludPreguntaMultipleChoiceComponent saludPreguntaMultipleChoiceComponent = new SaludPreguntaMultipleChoiceComponent();
                return saludPreguntaMultipleChoiceComponent.VerificarMultipleChoice(pregunta);
            }
            else if(pregunta.tipoPregunta.TipoDePregunta == "Ordenado")
            {
                SaludPreguntaOrdenadoComponent saludPreguntaOrdenadoComponent = new SaludPreguntaOrdenadoComponent();
                return saludPreguntaOrdenadoComponent.VerificarOrdenado(pregunta);
            }
            else
            {
                SaludPreguntaMultipleChoiceCompuestoComponent saludPreguntaMultipleChoiceCompuesto = new SaludPreguntaMultipleChoiceCompuestoComponent();
                return saludPreguntaMultipleChoiceCompuesto.VerificarMultipleChoiceCompuesto(pregunta);
            }

        }






    }
}

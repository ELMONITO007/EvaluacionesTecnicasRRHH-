using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using Data;
using Entities;

namespace Negocio
{
 public   class RespuestaComponent
    {

      public List<Respuesta> ObtenerRespuesta(int Id_Pregunta)
        {
            string TipoDePregunta = ObtenerTipoPregunta(Id_Pregunta);
            List<Respuesta> respuestas = new List<Respuesta>();


            if (TipoDePregunta== "MultipleChoice")
            {
                MultipleChoiceComponent multipleChoiceComponent = new MultipleChoiceComponent();
                List<   MultipleChoice> multipleChoice = new List< MultipleChoice>();

                multipleChoice = multipleChoiceComponent.listaRespuestaMultipleChoiceAlAzar(Id_Pregunta).ListaMultipleChoice;
                foreach (MultipleChoice item in multipleChoice)
                {
                    respuestas.Add(item);
                }
            }
            else if (TipoDePregunta == "Ordenado")
            {
                OrdenComponent ordenComponent = new OrdenComponent();
                Orden ordenes = new Orden();
                ordenes = ordenComponent.listaRespuestaOrdenAlAzar(Id_Pregunta);
                foreach (Orden item in ordenes.listaOrden)
                {
                    respuestas.Add(item);
                }

            }

            return respuestas;
        }

        public string ObtenerTipoPregunta(int id_Pregunta)
        {
            PreguntaComponent preguntaComponent = new PreguntaComponent();
            Pregunta pregunta = new Pregunta();
            pregunta = preguntaComponent.ReadBy(id_Pregunta);

            if (pregunta is null)
            {
                return "";
             
            }
            else
            {
                return pregunta.tipoPregunta.TipoDePregunta;
            }

        }
       
        
    }
}

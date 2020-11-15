using Data;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
  public  class SaludPreguntaMultipleChoiceCompuestoComponent
    {
        public MultipleChoiceCompuesto ObtenerPreguntaaDeUnaSubpregunta(int Id)
        {
            MultipleChoiceCompuestoDAC multipleChoiceCompuesto = new MultipleChoiceCompuestoDAC();
            return multipleChoiceCompuesto.ObtenerPreguntaaDeUnaSubpregunta(Id);
        }
        public bool VerificarMasDeDosSubpregunta(Pregunta preguntas)
        {
            MultipleChoiceCompustoComponent multipleChoiceCompustoComponent = new MultipleChoiceCompustoComponent();
            MultipleChoiceCompuesto multipleChoiceCompuesto = new MultipleChoiceCompuesto();
            multipleChoiceCompuesto = multipleChoiceCompustoComponent.ReadByPregunta(preguntas.Id);
            int contarLista = multipleChoiceCompuesto.ListaSubPreguntas.Count;
            if (contarLista > 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool VerificarMasDeDosRespuestas(Pregunta preguntas)
        {
            int aux=0;
            MultipleChoiceCompustoComponent multipleChoiceCompustoComponent = new MultipleChoiceCompustoComponent();
            MultipleChoiceCompuesto multipleChoiceCompuesto = new MultipleChoiceCompuesto();
            multipleChoiceCompuesto = multipleChoiceCompustoComponent.ReadByPregunta(preguntas.Id);
            int contarLista = multipleChoiceCompuesto.ListaSubPreguntas.Count;

           
            if (contarLista > 0)
            {
                foreach (Pregunta item in multipleChoiceCompuesto.ListaSubPreguntas)
                {
                    MultipleChoiceComponent multipleChoiceComponent = new MultipleChoiceComponent();
                    List<MultipleChoice> multipleChoice = new List<MultipleChoice>();
                    multipleChoice = multipleChoiceComponent.listaRespuestaMultipleChoiceAlAzar(item.Id).ListaMultipleChoice;
                    if (multipleChoice.Count<3)
                    {
                        aux = 1;
                    }
                   
                   
                }

            }
            else
            {
                aux = 1;
            }


            if (aux==0)
            {
                return true;
            }
            else
            { 
                return false;
            }

        }

        public bool VerificarTengaRespuestas(Pregunta preguntas)
        {
            int aux = 0;
   
         
            MultipleChoiceCompustoComponent multipleChoiceCompustoComponent = new MultipleChoiceCompustoComponent();
            MultipleChoiceCompuesto multipleChoiceCompuesto = new MultipleChoiceCompuesto();
            multipleChoiceCompuesto = multipleChoiceCompustoComponent.ReadByPregunta(preguntas.Id);

           
            int contarLista = multipleChoiceCompuesto.ListaSubPreguntas.Count;
            if (contarLista > 0)
            {
                foreach (Pregunta item in multipleChoiceCompuesto.ListaSubPreguntas)
                {
                    int contarCorrectas = 0;
                    MultipleChoiceComponent multipleChoiceComponent = new MultipleChoiceComponent();
                    List<MultipleChoice> multipleChoice = new List<MultipleChoice>();
                    multipleChoice = multipleChoiceComponent.listaRespuestaMultipleChoiceAlAzar(item.Id).ListaMultipleChoice;
                    foreach (MultipleChoice Subitem in multipleChoice)
                    {
                        

                        if (Subitem.Correcta)
                        {
                            contarCorrectas++;

                        }
                       
                    }

                    if (contarCorrectas == 0)
                    {
                        aux = 0;
                    }

                }

            }
            else
            {
                aux = 1;
            }


            if (aux == 0)
            {
                return true;
            }
            else
            {
                return false;
            }


        }


        public SaludPreguntaMultipleChoiceCompuesto VerificarMultipleChoiceCompuesto(Pregunta pregunta)
        {

            SaludPreguntaMultipleChoiceCompuesto saludPreguntaMultipleChoiceCompuesto = new SaludPreguntaMultipleChoiceCompuesto();
            saludPreguntaMultipleChoiceCompuesto.TieneMasDeDosRespuestas = VerificarMasDeDosRespuestas(pregunta);
            saludPreguntaMultipleChoiceCompuesto.TieneMasDeDosSubpreguntas = VerificarMasDeDosSubpregunta(pregunta);
            saludPreguntaMultipleChoiceCompuesto.TienePreguntaCorrecta = VerificarTengaRespuestas(pregunta);
            saludPreguntaMultipleChoiceCompuesto.pregunta = pregunta;

            if (saludPreguntaMultipleChoiceCompuesto.TieneMasDeDosRespuestas && saludPreguntaMultipleChoiceCompuesto.TieneMasDeDosSubpreguntas && saludPreguntaMultipleChoiceCompuesto.TienePreguntaCorrecta)
            {
                saludPreguntaMultipleChoiceCompuesto.SaludDeLaPregunta = true;
            }
            else
            {
                saludPreguntaMultipleChoiceCompuesto.SaludDeLaPregunta = false;
            }
            return saludPreguntaMultipleChoiceCompuesto;

        }


    }
}

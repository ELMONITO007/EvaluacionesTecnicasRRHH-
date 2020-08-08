using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
public    class SaludPreguntaMultipleChoiceComponent
    {
       
        public bool PreguntaConCategoria(Pregunta pregunta)
        {
            PreguntaCategoriaComponent preguntaCategoriaComponent = new PreguntaCategoriaComponent();
            Int16 id =short.Parse( pregunta.Id.ToString());
      
          List<PreguntaCategoria> pregunta1 = new List<PreguntaCategoria>();
            pregunta1 = preguntaCategoriaComponent.Read(id);
            if (pregunta1 is null || pregunta1.Count==0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }


        public bool VerificarMasDeTresRespuestas(SaludPreguntaMultipleChoice pregunta)
        {
            if (pregunta.pregunta.ListaRespuesta.Count>=3)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool VerificarSiTieneRespuestaCorrecta(SaludPreguntaMultipleChoice pregunta)
        {
            int a = 0;
            foreach (MultipleChoice item in pregunta.pregunta.ListaRespuesta)
            {
                if (item.Correcta==true)
                {
                    a=1;
                
                }
               

            }

            if (a==1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public SaludPreguntaMultipleChoice VerificarMultipleChoice(Pregunta pregunta)
        {
            SaludPreguntaMultipleChoice saludPreguntaMultipleChoice = new SaludPreguntaMultipleChoice();
            MultipleChoiceComponent multipleChoiceComponent = new MultipleChoiceComponent();
            saludPreguntaMultipleChoice.pregunta = pregunta;
            List<MultipleChoice> multiples = new List<MultipleChoice>();
            multiples = multipleChoiceComponent.listaRespuestaMultipleChoiceAlAzar(pregunta.Id).ListaMultipleChoice;

            foreach (MultipleChoice item in multiples)
            {
                saludPreguntaMultipleChoice.pregunta.ListaRespuesta.Add(item);
            }


            saludPreguntaMultipleChoice.TieneCategoria = PreguntaConCategoria(pregunta);
                saludPreguntaMultipleChoice.MasDeTresRespuestas = VerificarMasDeTresRespuestas(saludPreguntaMultipleChoice);
            saludPreguntaMultipleChoice.RespuestaCorrecta = VerificarSiTieneRespuestaCorrecta(saludPreguntaMultipleChoice);
            if (saludPreguntaMultipleChoice.MasDeTresRespuestas ==true && saludPreguntaMultipleChoice.RespuestaCorrecta==true && saludPreguntaMultipleChoice.TieneCategoria== true)
            {
                saludPreguntaMultipleChoice.SaludDeLaPregunta = true;
            }
            else
            {
                saludPreguntaMultipleChoice.SaludDeLaPregunta = false;
            }
            return saludPreguntaMultipleChoice;

        }

       

    }
}

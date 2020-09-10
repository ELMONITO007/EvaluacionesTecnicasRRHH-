using Data.Examen;
using Entities;
using Entities.Examen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Examen
{
    public class ExamenRespuestaComponent : IRepository<ExamenRespuesta>
    {
        public ExamenRespuesta Create(ExamenRespuesta entity)
        {
            ExamenRespuestaDAC examenRespuestaDAC = new ExamenRespuestaDAC();
            return examenRespuestaDAC.Create(entity);
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<ExamenRespuesta> Read()
        {
            throw new NotImplementedException();
        }

        public ExamenRespuesta ReadBy(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(ExamenRespuesta entity)
        {
            throw new NotImplementedException();
        }


        public List<ExamenRespuesta> ReadByExamen(int id)
        {

            //Obtener las respuestas realizadas
            List<ExamenRespuesta> examenRespuestas = new List<ExamenRespuesta>();
            ExamenRespuestaDAC examenRespuestaDAC = new ExamenRespuestaDAC();
            examenRespuestas = examenRespuestaDAC.ReadByExamen(id);
            //obtengo una copia para comprarar
            List<ExamenRespuesta> examenRespuestasCopia = new List<ExamenRespuesta>();

            examenRespuestasCopia = examenRespuestas;

            //inicializo las variables
            List<ExamenRespuesta> preguntas = new List<ExamenRespuesta>();
            ExamenRespuesta respuestas = new ExamenRespuesta();
       
            //recorro la primer lista
            foreach (ExamenRespuesta item in examenRespuestas)
            {
                TipoPregunta tipoPregunta = new TipoPregunta();
                TipoPreguntaComponent tipoPreguntaComponent = new TipoPreguntaComponent();
                tipoPregunta = tipoPreguntaComponent.ReadByPregunta(item.pregunta.Id);
                
                if (tipoPregunta.TipoDePregunta== "MultipleChoice")
                {
                    foreach (ExamenRespuesta subItem in examenRespuestas)
                    {

                   
                        if (subItem.pregunta.Id==item.pregunta.Id)
                        {
                           
                            respuestas.pregunta.Id = item.pregunta.Id;
                            respuestas.examen.Id = item.examen.Id;
                            respuestas.correcta = subItem.correcta;
                            MultipleChoice multipleChoice = new MultipleChoice();
                            multipleChoice.Id = subItem.respuesta.Id;
                            multipleChoice.Correcta = subItem.correcta;
                            multipleChoice.RespuestaObtenida = subItem.respondio;
                            respuestas.pregunta.ListaMC.Add(multipleChoice);
                           
                        }
                        else
                        {
                            preguntas.Add(respuestas);
                            respuestas = new ExamenRespuesta();
                           
                        }
                    }

                }

              


            }

            List<ExamenRespuesta> resultDuplicado = new List<ExamenRespuesta>();
            List<ExamenRespuesta> result = new List<ExamenRespuesta>();

            foreach (var item in preguntas)
            {
                if (item.pregunta.Id!=0)
                {
                    resultDuplicado.Add(item);

                }
            }

            int v = 0;
            int cont = 1;
            while (resultDuplicado.Count>cont)
            {
                int cantidadRespuesta = resultDuplicado[cont].pregunta.ListaMC.Count();
                result.Add(resultDuplicado[cont]);
                cont = cont + cantidadRespuesta;
            }
          
            return result;



        }



        public List<ExamenRespuesta> ObtenerRespuestas(List<ExamenRespuesta> respuestas)

        {
            List<ExamenRespuesta> result = new List<ExamenRespuesta>();



            foreach (ExamenRespuesta item in respuestas)
            {

                ExamenRespuesta examenRespuesta = new ExamenRespuesta();
                PreguntaComponent pregunta = new PreguntaComponent();

                examenRespuesta.pregunta = pregunta.ReadBy(item.pregunta.Id);
            

     
                foreach (MultipleChoice subItem in item.pregunta.ListaMC)
                {
                    MultipleChoice multipleChoice = new MultipleChoice();
                    MultipleChoiceComponent multipleChoiceComponent = new MultipleChoiceComponent();
                    multipleChoice = multipleChoiceComponent.ReadBy(subItem.Id);
                    multipleChoice.Correcta = subItem.Correcta;
                    multipleChoice.RespuestaObtenida = subItem.RespuestaObtenida;
                    examenRespuesta.pregunta.ListaMC.Add(multipleChoice);

                }
                result.Add(examenRespuesta);

            }


            return result;






        }


    }
}

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

        public List<ExamenRespuesta> ReadByPregunta(int id,int id_examen)


        {

            ExamenRespuestaDAC examen = new ExamenRespuestaDAC();
            return examen.ReadByPregunta(id, id_examen);
        }


        public List<ExamenRespuesta> ObtenerRespuestasDeUnaPregunta(ExamenPregunta examenPreguntas,int id_Examen)
        {
            List<ExamenRespuesta> examenRespuestas = new List<ExamenRespuesta>();
            List<ExamenRespuesta> result = new List<ExamenRespuesta>();
            if (examenPreguntas.pregunta.tipoPregunta.TipoDePregunta == "MultipleChoice")

            {
                examenRespuestas = ReadByPregunta(examenPreguntas.pregunta.Id,id_Examen);
            

            foreach (ExamenRespuesta item in examenRespuestas)
            {
                ExamenRespuesta examen = new ExamenRespuesta();
              

                examen = item;
               

                    MultipleChoiceComponent multipleChoiceComponent = new MultipleChoiceComponent();
                
                    examen.respuesta = multipleChoiceComponent.ReadBy(item.respuesta.Id);
                   
                    result.Add(examen);
                }
             
              
            }


            if (examenPreguntas.pregunta.tipoPregunta.TipoDePregunta == "MultipleChoiceCompuesto")
            {

                //obtengos los id de las subpregunta
                List<ExamenPregunta> listaSubpreguntas = new List<ExamenPregunta>();
                ExamenPreguntaComponent examen = new ExamenPreguntaComponent();
                listaSubpreguntas = examen.ReadBySubPregunta(examenPreguntas.pregunta.Id);
                
                //recorro la lista
                foreach (ExamenPregunta item in listaSubpreguntas)
                {
                    ExamenRespuesta unExamen = new ExamenRespuesta();
                    ExamenPregunta examenPreguntaMCC = new ExamenPregunta();
                    PreguntaComponent preguntaComponent = new PreguntaComponent();
                    ExamenRespuestaDAC examenRespuesta = new ExamenRespuestaDAC();
                 
                    List<ExamenRespuesta> respuestas = new List<ExamenRespuesta>();
                    examenPreguntaMCC.pregunta = preguntaComponent.ReadBySubPregunta(item.Id);
                    examenPreguntaMCC.listaExamenRespuesta.Clear();
                    respuestas = examenRespuesta.ReadBySubPregunta(item.Id,id_Examen);
                    foreach (ExamenRespuesta subItem in respuestas)
                    {
                        ExamenRespuesta respuesta = new ExamenRespuesta();
                        MultipleChoiceCompustoComponent multipleChoiceCompustoComponent = new MultipleChoiceCompustoComponent(); 
                        respuesta = subItem;

                        respuesta.respuesta = multipleChoiceCompustoComponent.ReadyRespuesta(subItem.respuesta.Id);
                        examenPreguntaMCC.listaExamenRespuesta.Add(respuesta);
                        
                    }
                    unExamen.examenPregunta = examenPreguntaMCC;
                    result.Add(unExamen);
                }




            }
            return result;

        }




        public Entities.Examen.Examen ReadByExamen(int id)
        {

            //Obtener datos examen 

            Entities.Examen.Examen examen = new Entities.Examen.Examen();
            ExamenComponent examenComponent = new ExamenComponent();
            examen = examenComponent.ReadBy(id);

            //obtener las preguntas
            ExamenPreguntaComponent examenPreguntaComponent = new ExamenPreguntaComponent();
            List<ExamenPregunta> LisEP= new List<ExamenPregunta>();
            LisEP = examenPreguntaComponent.ReadByExamen(id);
            examen.listaExamenPregunta.Clear();
            List<ExamenPregunta> ListaPreguntas = new List<ExamenPregunta>();

            //Obtengo los datos de la pregunta
            for (int item = 0; item < LisEP.Count; item++)
            {
                ExamenPregunta unExamePregunta = new ExamenPregunta();
                PreguntaComponent preguntaComponent = new PreguntaComponent();
                unExamePregunta = LisEP[item];
                unExamePregunta.pregunta = preguntaComponent.ReadBy(unExamePregunta.pregunta.Id);
                ListaPreguntas.Add(unExamePregunta);
            }



            //Obtengo las respuesta

            foreach (ExamenPregunta item in ListaPreguntas)
            {

                ExamenPregunta unExamePregunta = new ExamenPregunta();

                unExamePregunta = item;
                unExamePregunta.listaExamenRespuesta = ObtenerRespuestasDeUnaPregunta(item,id);


                examen.listaExamenPregunta.Add(unExamePregunta);

            }



           
          
            return examen;



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

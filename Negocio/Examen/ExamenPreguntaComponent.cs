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
    public class ExamenPreguntaComponent : IRepository<ExamenPregunta>
    {
        public List<ExamenPregunta> ReadBySubPregunta(int Id_Examen)
        {
            ExamenPreguntaDAC examenPreguntaDAC = new ExamenPreguntaDAC();
            return examenPreguntaDAC.ReadBySubPregunta(Id_Examen);
        }
            public ExamenPregunta Create(ExamenPregunta entity)
        {
            ExamenPreguntaDAC examenPreguntaDAC = new ExamenPreguntaDAC();
            return examenPreguntaDAC.Create(entity);
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<ExamenPregunta> Read()
        {
            throw new NotImplementedException();
        }
        public List<ExamenPregunta>  ReadByExamen(int Id_examen)
        {
           
            ExamenPreguntaDAC examenPreguntaDAC = new ExamenPreguntaDAC();
            List<ExamenPregunta> Lista = new List<ExamenPregunta>();
            List<ExamenPregunta> result = new List<ExamenPregunta>();
            result= examenPreguntaDAC.ReadByExamen(Id_examen);
           

            foreach (ExamenPregunta item in result)
            {
                ExamenPregunta examenPregunta = new ExamenPregunta();
                examenPregunta = item;
                PreguntaComponent preguntaComponent = new PreguntaComponent();
                examenPregunta.pregunta = preguntaComponent.ReadBy(item.pregunta.Id);
                if (examenPregunta.pregunta.tipoPregunta.TipoDePregunta == "MultipleChoice")
                {
                    MultipleChoiceComponent multipleChoiceComponent = new MultipleChoiceComponent();
                    List<MultipleChoice> multipleChoices = new List<MultipleChoice>();
                    examenPregunta.pregunta.ListaMC = multipleChoiceComponent.listaRespuestaMultipleChoiceAlAzar(examenPregunta.pregunta.Id).ListaMultipleChoice;
                    int a = 0;
                    for (int i = 0; i < examenPregunta.pregunta.ListaMC.Count(); i++)
                    {
                        if (examenPregunta.pregunta.ListaMC[i].LaRespuesta == "NS/NC")
                        {
                            MultipleChoice multipleChoice = new MultipleChoice();
                            multipleChoice = examenPregunta.pregunta.ListaMC[i];
                            MultipleChoice nuevo = new MultipleChoice();
                            nuevo = examenPregunta.pregunta.ListaMC[i];
                            examenPregunta.pregunta.ListaMC.Remove(multipleChoice);
                            examenPregunta.pregunta.ListaMC.Add(nuevo);
                            

                        }
                      
                    }
                    






                }
                else if (examenPregunta.pregunta.tipoPregunta.TipoDePregunta == "MultipleChoiceCompuesto")
                {
                    MultipleChoiceCompustoComponent multipleChoiceCompustoComponent = new MultipleChoiceCompustoComponent();
                    examenPregunta.pregunta.ListaPregunta = multipleChoiceCompustoComponent.ReadByPregunta(examenPregunta.pregunta.Id).ListaSubPreguntas;

                    foreach (Pregunta MCCprincipal in examenPregunta.pregunta.ListaPregunta)
                    {
                        for (int i = 0; i < MCCprincipal.ListaMC.Count(); i++)
                        {
                            if (MCCprincipal.ListaMC[i].LaRespuesta == "NS/NC")
                            {
                                MultipleChoice multipleChoice = new MultipleChoice();
                                multipleChoice = MCCprincipal.ListaMC[i];
                                MultipleChoice nuevo = new MultipleChoice();
                                nuevo = MCCprincipal.ListaMC[i];
                                MCCprincipal.ListaMC.Remove(multipleChoice);
                                MCCprincipal.ListaMC.Add(nuevo);


                            }

                        }
                    }



                }
                Lista.Add(examenPregunta);


            }


            return Lista;
        }
        public ExamenPregunta ReadBy(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(ExamenPregunta entity)
        {

            ExamenPreguntaDAC examenPreguntaDAC=new ExamenPreguntaDAC();
            examenPreguntaDAC.Update(entity);
        }
    }
}

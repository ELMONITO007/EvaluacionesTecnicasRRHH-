using Data.Examen;
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
                    examenPregunta.pregunta.ListaMC = multipleChoiceComponent.listaRespuestaMultipleChoiceAlAzar(examenPregunta.pregunta.Id).ListaMultipleChoice;

                }
                else if (examenPregunta.pregunta.tipoPregunta.TipoDePregunta == "MultipleChoiceCompuesto")
                {
                    MultipleChoiceCompustoComponent multipleChoiceCompustoComponent = new MultipleChoiceCompustoComponent();
                    examenPregunta.pregunta.ListaPregunta = multipleChoiceCompustoComponent.ReadByPregunta(examenPregunta.pregunta.Id).ListaSubPreguntas;
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
            throw new NotImplementedException();
        }
    }
}

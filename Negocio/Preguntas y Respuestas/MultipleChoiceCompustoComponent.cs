using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using Entities;

namespace Negocio
{
    public class MultipleChoiceCompustoComponent : Component<MultipleChoiceCompuesto>
    {
        public override MultipleChoiceCompuesto Create(MultipleChoiceCompuesto objeto)
        {
            MultipleChoiceCompuestoDAC multipleChoiceCompustoComponent = new MultipleChoiceCompuestoDAC();


        return multipleChoiceCompustoComponent.Create(objeto);
        }

        public override void Delete(int id)
        {
            MultipleChoiceCompuestoDAC multipleChoiceCompuestoDAC = new MultipleChoiceCompuestoDAC();
            multipleChoiceCompuestoDAC.Delete(id);
        }

        public override List<MultipleChoiceCompuesto> Read()
        {
            throw new NotImplementedException();
        }

        public override MultipleChoiceCompuesto ReadBy(int id)
        {
            
            MultipleChoiceCompuestoDAC multipleChoiceCompuestoDAC = new MultipleChoiceCompuestoDAC();
          



         return   multipleChoiceCompuestoDAC.ReadByRespuesta(id); ;
        }
        public MultipleChoiceCompuesto ReadByPregunta(int id)
        {
            MultipleChoiceCompuesto resultado = new MultipleChoiceCompuesto();
            //obtengo un multipleChoiceCompuesto
            MultipleChoiceCompuesto multipleChoiceCompuesto = new MultipleChoiceCompuesto();
            //obtener la pregunta
            PreguntaComponent preguntaComponent = new PreguntaComponent();
            multipleChoiceCompuesto.pregunta = preguntaComponent.ReadBy(id);
            resultado.pregunta = multipleChoiceCompuesto.pregunta;

            //Obtener las subPregunta
            List<MultipleChoiceCompuesto> ListamultipleChoiceCompuestos = new List<MultipleChoiceCompuesto>();
            MultipleChoiceCompuestoDAC multipleChoiceCompuestoDAC = new MultipleChoiceCompuestoDAC();
            ListamultipleChoiceCompuestos = multipleChoiceCompuestoDAC.ReadBy(id);
            //Asignar las subPregunta a la lista de preguntas
            foreach (MultipleChoiceCompuesto item in ListamultipleChoiceCompuestos)
            {
                Pregunta result = new Pregunta();
                result = preguntaComponent.ReadBySubPregunta(item.id_SubPregunta);
                multipleChoiceCompuesto.ListaSubPreguntas.Add(result);
               
            }
            //Obtener las Respuesta de las subPreguntas

            foreach (Pregunta item in multipleChoiceCompuesto.ListaSubPreguntas)
            {
                MultipleChoiceComponent multipleChoiceComponent = new MultipleChoiceComponent();
               List< MultipleChoice> multipleChoice = new List<MultipleChoice>() ;
                multipleChoice = multipleChoiceComponent.listaRespuestaMultipleChoiceAlAzar(item.Id).ListaMultipleChoice;
                item.ListaMC = multipleChoice;
                MultipleChoiceCompuesto multiple = new MultipleChoiceCompuesto();
                multiple.pregunta = item;
                resultado.ListaSubPreguntas.Add(item);
            }



            return resultado;
        }
        public override void Update(MultipleChoiceCompuesto objeto)
        {
            MultipleChoiceCompuestoDAC multipleChoiceCompuestoDAC = new MultipleChoiceCompuestoDAC() ;
            multipleChoiceCompuestoDAC.Update(objeto);
        }
       public Respuesta ReadyRespuesta(int id)
        {
            MultipleChoiceCompuestoDAC multipleChoiceCompuestoDAC = new MultipleChoiceCompuestoDAC();
          return  multipleChoiceCompuestoDAC.ReadByRespuesta(id);
        }
    }
}

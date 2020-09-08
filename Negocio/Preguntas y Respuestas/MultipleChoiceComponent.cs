using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using Entities;

namespace Negocio
{
    public class MultipleChoiceComponent : Component<MultipleChoice>
    {
        public override MultipleChoice Create(MultipleChoice objeto)
        {

            MultipleChoice result = default(MultipleChoice);
            var multipleChoiceDAC = new MultipleChoiceDAC();

            result = multipleChoiceDAC.Create(objeto);
            return result;
        }

        public override void Delete(int id)
        {
            var multipleChoiceDAC = new MultipleChoiceDAC();
            multipleChoiceDAC.Delete(id);
        }

        public override List<MultipleChoice> Read()
        {
            List<MultipleChoice> result = default(List<MultipleChoice>);
            var multipleChoiceDAC = new MultipleChoiceDAC();
            result = multipleChoiceDAC.Read();
            return result;
        }


        public override MultipleChoice ReadBy(int id)
        {
            MultipleChoice result = default(MultipleChoice);
            var multipleChoiceDAC = new MultipleChoiceDAC();
            result = multipleChoiceDAC.ReadBy(id);
            return result;
        }

        public override void Update(MultipleChoice objeto)
        {
            var multipleChoiceDAC = new MultipleChoiceDAC();
            multipleChoiceDAC.Update(objeto);


        }
        public MultipleChoice listaRespuestaMultipleChoiceAlAzar(int id_pregunta)
        {

            PreguntaComponent preguntaComponent = new PreguntaComponent();
            MultipleChoice result = new MultipleChoice();
            var multipleChoiceDAC = new MultipleChoiceDAC();
            result.ListaMultipleChoice = multipleChoiceDAC.listaRespuestaMultipleChoiceAlAzar(id_pregunta);
            result.pregunta = preguntaComponent.ReadBy(id_pregunta);
            return result;
        }


        public List<MultipleChoice> ObtenerRespuestaCorrecta(int id_pregunta)
        {


            List<MultipleChoice> result = default(List<MultipleChoice>);
            var multipleChoiceDAC = new MultipleChoiceDAC();
            result = multipleChoiceDAC.ObtenerRespuestaCorrecta(id_pregunta);
            return result;
        }

        public bool ObtenerLaRespuesta(int id)
        {
            MultipleChoice result = default(MultipleChoice);
            var multipleChoiceDAC = new MultipleChoiceDAC();
            result = multipleChoiceDAC.ObtenerLaRespuestaCorrecta(id);
            return result.Correcta;



        }
    }
}

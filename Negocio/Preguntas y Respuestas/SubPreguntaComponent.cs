using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using Entities;
namespace Negocio
{
 public class SubPreguntaComponent : Component<SubPregunta>
    {
        public override SubPregunta Create(SubPregunta objeto)
        {
            SubPreguntaDAC subPreguntaDAC = new SubPreguntaDAC();
            PreguntaComponent preguntaComponent = new PreguntaComponent();
            SubPregunta subPregunta = new SubPregunta();
            subPregunta.pregunta = preguntaComponent.ReadBy(objeto.pregunta.Id);
            subPregunta.subPregunta.nivel = subPregunta.pregunta.nivel;
            
            subPregunta.subPregunta.tipoPregunta= subPregunta.pregunta.tipoPregunta;
            subPregunta.subPregunta.Imagen = "Sin Imagen";
            subPregunta.subPregunta.SubPregunta = true;
            subPregunta.subPregunta.LaPregunta = objeto.subPregunta.LaPregunta;
            //Obtener la categoria
            List<PreguntaCategoria> preguntas = new List<PreguntaCategoria>();
            PreguntaCategoriaComponent preguntaCategoriaComponent = new PreguntaCategoriaComponent();
            preguntas = preguntaCategoriaComponent.Read(Int16.Parse(objeto.pregunta.Id.ToString()));
            foreach (PreguntaCategoria item in preguntas)
            {
                subPregunta.subPregunta.categoria = item.Categorias;
                break;
            }

            //creo la subpregunta en la tabla pregunta con subpregunta en True
            preguntaComponent.CreateSubPregunta(subPregunta.subPregunta);

            //Obtengo el id de la subpregunta creada
            subPregunta.subPregunta = preguntaComponent.ReadBySubPregunta(objeto.subPregunta.LaPregunta);


            return subPreguntaDAC.Create(subPregunta);
        }

        public override void Delete(int id)
        {
          SubPreguntaDAC subPreguntaDAC=new SubPreguntaDAC();
            subPreguntaDAC.Delete(id);
        }

        public override List<SubPregunta> Read()
        {
            throw new NotImplementedException();
        }

        public override SubPregunta ReadBy(int id)
        {
            throw new NotImplementedException();
        }

        public override void Update(SubPregunta objeto)
        {
            SubPreguntaDAC subPreguntaDAC = new SubPreguntaDAC();
            subPreguntaDAC.Update(objeto);
        }
    }
}

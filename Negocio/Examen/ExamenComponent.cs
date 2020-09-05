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
    public class ExamenComponent : IRepository<Entities.Examen.Examen>
    {
        public bool VerificarSiRieneExamenActivo(Entities.Examen.Examen entity)
        {

            ExamenDAC examenDAC = new ExamenDAC();
            Entities.Examen.Examen examen = new Entities.Examen.Examen();
            examen = examenDAC.ReadByEstado(entity.usuario.Id);
            if (examen is null)
            {
                return false;
            }
            else
            {
                return true;
            }

        }



        public Entities.Examen.Examen Create(Entities.Examen.Examen entity)
        {


            //Veriificar si el usuario tiene examenes activos
            if (VerificarSiRieneExamenActivo(entity))
            {
                entity.error = "Activo";
            }
            else
            {

                //Verifico la salud del examen
                SaludCategoria saludCategoria = new SaludCategoria();
                SaludCategoriaComponent saludCategoriaComponent = new SaludCategoriaComponent(entity.Categoria.Id);

                saludCategoria = saludCategoriaComponent.verificarSaludTipoPregunta();
                if (saludCategoria.TieneMasDe20PreguntasConBuenaSalud)
                {



                    //obtengo 20 pregunta al azar
                    PreguntaComponent preguntaComponent = new PreguntaComponent();
                    Pregunta pregunta = new Pregunta();
                    Pregunta enviar = new Pregunta();


                    enviar.categoria.Id = entity.Categoria.Id;
                    entity.listaPregunta = preguntaComponent.ObtenerPreguntasAlAzar(enviar, 20);
                    //Completo los datos del examen
                    entity.Fecha = DateTime.Parse(DateTime.Now.ToString("dd/MM/yyyy hh:mm"));
                    entity.Resultado = 0;
                    entity.Aprobado = false;
                    entity.Estado = "A realizar";

                    //Creo el examen
                    ExamenComponent examenComponent = new ExamenComponent();
                    examenComponent.Create(entity);
                    //Obtengo el ID del examen creado

                    Entities.Examen.Examen examen = new Entities.Examen.Examen();
                    examen = examenComponent.ReadBy(entity.Fecha);
                    int Id = examen.Id;

                    //guardo las preguntas en la Base

                    foreach (Pregunta item in entity.listaPregunta)
                    {
                        ExamenPregunta examenPregunta = new ExamenPregunta();
                        ExamenPreguntaComponent examenPreguntaComponent = new ExamenPreguntaComponent();
                        examenPregunta.examen.Id = Id;
                        examenPregunta.pregunta.Id = item.Id;
                        examenPregunta.correcta = false;
                        examenPreguntaComponent.Create(examenPregunta);
                    }
                    entity.error = "Creado";

                }
                else
                {
                    entity.error = "Salud";
                }
            }



            return entity;
        }

        public void Delete(int id)
        {
            ExamenComponent examenComponent = new ExamenComponent();
            examenComponent.Delete(id);
        }

        public List<Entities.Examen.Examen> Read()
        {
            ExamenComponent examenComponent = new ExamenComponent();
            return examenComponent.Read();
        }

        public Entities.Examen.Examen ReadBy(int id)
        {
            ExamenDAC examenDAC = new ExamenDAC();
            return examenDAC.ReadBy(id);
        }
        public Entities.Examen.Examen ReadBy(DateTime Fecha)
        {
            ExamenDAC examenDAC = new ExamenDAC();
            return examenDAC.ReadBy(Fecha);
        }

        public void Update(Entities.Examen.Examen entity)
        {
            ExamenDAC examenDAC = new ExamenDAC();


            examenDAC.Update(entity);
        }
    }
}

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



        public Entities.Examen.Examen Create(Entities.Examen.Examen entity, int cantidad)
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
                    entity.listaPregunta = preguntaComponent.ObtenerPreguntasAlAzar(enviar, cantidad);
                    //Completo los datos del examen
                    entity.Fecha = DateTime.Parse(DateTime.Now.ToString("dd/MM/yyyy HH:mm"));
                    entity.Resultado = 0;
                    entity.Aprobado = false;
                    entity.Estado = "A realizar";

                    //Creo el examen
                    ExamenDAC examenComponent = new ExamenDAC();
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

                        examenPregunta.examen.Id = examen.Id;
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
            ExamenDAC examenComponent = new ExamenDAC();
            List<Entities.Examen.Examen> examen = new List<Entities.Examen.Examen>();
            List<Entities.Examen.Examen> Result = new List<Entities.Examen.Examen>();

            examen = examenComponent.Read();

            foreach (Entities.Examen.Examen item in examen)
            {
                UsuariosComponent usuarios = new UsuariosComponent();
                CategoriaComponent categoriaComponent = new CategoriaComponent();
                Entities.Examen.Examen unExamen = new Entities.Examen.Examen();
                unExamen = item;
                unExamen.usuario = usuarios.ReadBy(item.usuario.Id);
                unExamen.Categoria = categoriaComponent.ReadBy(item.Categoria.Id);
                Result.Add(unExamen);
            }
            return Result;
        }

        public Entities.Examen.Examen ReadBy(int id)
        {

            ExamenPreguntaComponent examenPreguntaComponent = new ExamenPreguntaComponent();
            List<ExamenPregunta> ListaPreguntasExamen = new List<ExamenPregunta>();
            ListaPreguntasExamen = examenPreguntaComponent.ReadByExamen(id);


            ExamenDAC examenDAC = new ExamenDAC();
            Entities.Examen.Examen examen = new Entities.Examen.Examen();
            UsuariosComponent usuarios = new UsuariosComponent();
            examen = examenDAC.ReadBy(id);
            int a = examen.usuario.Id;
            examen.listaExamenPregunta = ListaPreguntasExamen;
            examen.usuario = usuarios.ReadBy(a);



            return examen;
        }

        public Entities.Examen.Examen VerExamenUsuario(int id)
        {
            //obtengo el ultimo examen a realizar
            Entities.Examen.Examen examen = new Entities.Examen.Examen();
            ExamenDAC examenComponent = new ExamenDAC();
            examen = examenComponent.ReadByUsuario(id);

            //obtengo las preguntas y respusta del examen

            ExamenPreguntaComponent examenPreguntaComponent = new ExamenPreguntaComponent();
            List<ExamenPregunta> ListaPreguntasExamen = new List<ExamenPregunta>();
            ListaPreguntasExamen = examenPreguntaComponent.ReadByExamen(examen.Id);


            //asigno las preguntas al examen
            examen.listaExamenPregunta = ListaPreguntasExamen;

            //Obtengo los datos del usuario
            UsuariosComponent usuariosComponent = new UsuariosComponent();
            examen.usuario = usuariosComponent.ReadBy(id);


            return examen;



        }






        public Entities.Examen.Examen ReadBy(DateTime Fecha)
        {
            ExamenDAC examenDAC = new ExamenDAC();
            return examenDAC.ReadBy(Fecha);
        }






        public void Terminarexamen(List<Pregunta> preguntas, Entities.Examen.Examen examen)
        {
            List<ExamenPregunta> examenPreguntas = new List<ExamenPregunta>();
            List<ExamenRespuesta> ListaExamenRespuesta = new List<ExamenRespuesta>();
            foreach (Pregunta item in preguntas)
            { int aux = 0;
                //obtengo el tipo de pregunta
                TipoPreguntaComponent tipoPreguntaComponent = new TipoPreguntaComponent();
                TipoPregunta tipoPregunta = new TipoPregunta();
                tipoPregunta = tipoPreguntaComponent.ReadBy(item.tipoPregunta.Id);
                if (tipoPregunta.TipoDePregunta == "MultipleChoice")
                {
                    foreach (MultipleChoice mcPrincipal in item.ListaMC)
                    {
                        MultipleChoiceComponent multipleChoiceComponent = new MultipleChoiceComponent();

                        ExamenRespuesta respuesta = new ExamenRespuesta();
                        respuesta.examen.Id = examen.Id;
                        respuesta.pregunta.Id = item.Id;
                        respuesta.respuesta.Id = mcPrincipal.Id;
                        respuesta.respondio = mcPrincipal.RespuestaObtenida;
                        bool respuestaBase = multipleChoiceComponent.ObtenerLaRespuesta(mcPrincipal.Id);
                       

                        if (respuestaBase == mcPrincipal.RespuestaObtenida)
                        {
                            respuesta.correcta = respuestaBase;
                        }
                        else
                        {
                            respuesta.correcta = respuestaBase;
                            aux = 1;
                            
                        }
                        ListaExamenRespuesta.Add(respuesta);
                    }

                }

                if (tipoPregunta.TipoDePregunta == "MultipleChoiceCompuesto")
                {
                    foreach (Pregunta mccPrincipal in item.ListaPregunta)
                    {
                        foreach (MultipleChoice mccSecundario in mccPrincipal.ListaMC)
                        {
                            MultipleChoiceComponent multipleChoiceComponent = new MultipleChoiceComponent();

                            ExamenRespuesta respuesta = new ExamenRespuesta();
                            respuesta.examen.Id = examen.Id;
                            respuesta.pregunta.Id = item.Id;
                            respuesta.respuesta.Id = mccSecundario.Id;
                            respuesta.subPregunta.SubPregunta = true;
                            respuesta.subPregunta.Id = mccPrincipal.Id;
                            respuesta.respondio = mccSecundario.RespuestaObtenida;
                            bool respuestaBase = multipleChoiceComponent.ObtenerLaRespuesta(mccSecundario.Id);
                            if (respuestaBase == mccSecundario.RespuestaObtenida)
                            {
                                respuesta.correcta = respuestaBase;
                            }
                            else
                            {
                                respuesta.correcta = respuestaBase;
                                aux = 1;

                            }
                            ListaExamenRespuesta.Add(respuesta);
                        }

                    }


                }


                ExamenPregunta examenPregunta = new ExamenPregunta();
                examenPregunta.examen.Id = examen.Id;
                examenPregunta.pregunta.Id = item.Id;
                if (aux == 1)
                {
                    examenPregunta.correcta = false;
                }
                else
                {
                    examenPregunta.correcta = true;

                }
                examenPreguntas.Add(examenPregunta);

            }

            //Registrar respuestas correctas

            RegistrarPreguntarCorrectas(examenPreguntas);

            // Obtener resultado

            Entities.Examen.Examen examenTerminado = new Entities.Examen.Examen();
            examenTerminado = ObtenerResultados(examenPreguntas);
            examenTerminado.Id = examen.Id;
            examenTerminado.Fecha = DateTime.Parse(DateTime.Now.ToString("dd/MM/yyyy HH:mm"));
            examenTerminado.Estado = "Realizado";
            ExamenComponent examenComponent = new ExamenComponent();
            examenComponent.Update(examenTerminado);
            //Bloquear usuario

            UsuariosComponent usuariosComponent = new UsuariosComponent();
            usuariosComponent.Bloquear(examen.usuario.Id);

            //Registrar las respuestas

            foreach (ExamenRespuesta item in ListaExamenRespuesta)
            {
                ExamenRespuestaComponent examenRespuestaComponent = new ExamenRespuestaComponent();
                examenRespuestaComponent.Create(item);
            }

        }


        public Entities.Examen.Examen ObtenerResultados(List<ExamenPregunta> lista)
        {
            Entities.Examen.Examen examen = new Entities.Examen.Examen();
            int cantidad = lista.Count();
            examen.RespuestasCorrectas = 0;
            examen.RespuestasIncorrectas = 0;
            foreach (ExamenPregunta item in lista)
            {
                if (item.correcta)
                {
                    examen.RespuestasCorrectas++;
                }
                else
                {
                    examen.RespuestasIncorrectas++;

                }
            }

            if (examen.RespuestasCorrectas>14)
            {
                examen.Aprobado = true;
            }

            else
            {
                examen.Aprobado = false;
            }

            examen.Resultado = cantidad - examen.RespuestasIncorrectas;

            return examen;


        }

        public void RegistrarPreguntarCorrectas(List<ExamenPregunta> lista)
        {

            foreach (ExamenPregunta item in lista)
            {

                ExamenPreguntaComponent examenPreguntaComponent = new ExamenPreguntaComponent();
                examenPreguntaComponent.Update(item);

            }

        }






      






      







        public Entities.Examen.Examen ObtenerExamenTerminado(int id_examen)
        {
            Entities.Examen.Examen examen = new Entities.Examen.Examen();


            return examen;




        }

        public void Update(Entities.Examen.Examen entity)
        {
            ExamenDAC examenDAC = new ExamenDAC();


            examenDAC.Update(entity);
        }
    }
}

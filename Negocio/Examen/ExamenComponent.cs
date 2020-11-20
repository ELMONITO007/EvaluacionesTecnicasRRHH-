using Data.Examen;
using Entities;
using Entities.Examen;
using System;
using System.Collections.Generic;
using System.Globalization;
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
            examen.facil = 0;
            examen.medio = 0;
            examen.dificil = 0;

            foreach (var item in examen.listaExamenPregunta)
            {
                if (item.pregunta.nivel.ElNivel=="Medio")
                {
                    examen.medio++;
                }
                if (item.pregunta.nivel.ElNivel == "Facil")
                {
                    examen.facil++;
                }
                if (item.pregunta.nivel.ElNivel == "Dificil")
                {
                    examen.dificil++;
                }
            }


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

           

            Entities.Examen.Examen examen = new Entities.Examen.Examen();
            examen = examenDAC.ReadBy(Fecha);

           




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
                        ExamenRespuestaComponent examenRespuestaComponent = new ExamenRespuestaComponent();

                        if (respuestaBase == mcPrincipal.RespuestaObtenida)
                        {
                            respuesta.correcta = true;
                        }
                        else
                        {
                            respuesta.correcta = false;
                            aux = 1;
                            
                        }
                        examenRespuestaComponent.Create(respuesta);
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
                            ExamenRespuestaComponent examenRespuestaComponent = new ExamenRespuestaComponent();
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
                                respuesta.correcta = true;
                            }
                            else
                            {
                                respuesta.correcta = false;
                                aux = 1;

                            }
                            examenRespuestaComponent.Create(respuesta);
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
                ExamenPreguntaComponent examenPreguntaComponent = new ExamenPreguntaComponent();
                examenPreguntaComponent.Update(examenPregunta);
                examenPreguntas.Add(examenPregunta);

            }



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
            int ap = lista.Count * 70 / 100;
            if (examen.RespuestasCorrectas> ap)
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
               int a= saludCategoriaComponent.CantidadPreguntaConBuenaSalud();
                if (a>=entity.cantidadPreguntas)
                {



                    //obtengo  pregunta al azar
                    PreguntaComponent preguntaComponent = new PreguntaComponent();
                    Pregunta pregunta = new Pregunta();
                    Pregunta enviar = new Pregunta();
                    UsuariosComponent usuariosComponent = new UsuariosComponent();
                    usuariosComponent.Desloquear(entity.usuario.Id);

                    enviar.categoria.Id = entity.Categoria.Id;
                    entity.listaPregunta = preguntaComponent.ObtenerPreguntasAlAzar(enviar, entity.cantidadPreguntas);
                    //Completo los datos del examen
                    string dia = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");
                    entity.Fecha = DateTime.ParseExact(dia, "dd-MM-yyyy HH:mm:ss", CultureInfo.CurrentUICulture);
                    entity.Resultado = 0;
                    entity.Aprobado = false;
                    entity.Estado = "A realizar";
                    //completo la cantidad por nivel
                    List<Nivel> niveles = preguntaComponent.ObtenerCantidadDePreguntasParaExamen(enviar, entity.cantidadPreguntas);


                    foreach (var item in niveles)
                    {
                        if (item.ElNivel == "Facil")
                        {
                            entity.facil = item.porcentajePorPregunta;
                        }

                        else if (item.ElNivel == "Medio")

                        {
                            entity.medio = item.porcentajePorPregunta;
                        }
                        else
                        {
                            entity.dificil = item.porcentajePorPregunta;
                        }
                    }



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
    }
}

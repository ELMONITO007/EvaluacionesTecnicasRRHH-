using Entities;
using Evaluaciones.Models;
using Evaluaciones_Tecnicas.Filter;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Evaluaciones_Tecnicas.Controllers.Preguntas_y_Respuestas
{
    public class CreadorPreguntasController : Controller
    {
        // GET: Preguntas/Details/5
        [AuthorizerUser(_roles: "CrearPregunta")]
        public ActionResult Details(int id)
        {
            PreguntaComponent pregunta = new PreguntaComponent();
            return View(pregunta.ReadBy(id));
        }

        // GET: Preguntas
        [AuthorizerUser(_roles: "CrearPregunta")]
        public ActionResult Index()
        {


            PreguntaComponent preguntaComponent = new PreguntaComponent();
            List<Pregunta> listaPreguntas = new List<Pregunta>();
            return View(preguntaComponent.Read());


        }

     
        // GET: Preguntas/Create
        [AuthorizerUser(_roles: "CrearPregunta")]
        public ActionResult Create()
        {
            CategoriaComponent categoriaComponent = new CategoriaComponent();

            #region Categoria
            PreguntaModels preguntaModels = new PreguntaModels();
            Pregunta pregunta = new Pregunta();
            pregunta.ListaCategoria = categoriaComponent.Read();
            pregunta.ListaCategoria.Select(y =>
                                new
                                {
                                    Id = y.Id,
                                    LaCategoria = y.LaCategoria
                                });

            ViewBag.categoriaLista = new SelectList(preguntaModels.ListaCategoria, "Id", "LaCategoria");


            #endregion


            #region Nivel

            NivelComponent nivelComponent = new NivelComponent();
            pregunta.ListaNivel = nivelComponent.Read();
            pregunta.ListaNivel.Select(w =>
                                new
                                {
                                    Id = w.Id,
                                    ElNivel = w.ElNivel
                                });
            ViewBag.ListaNivel = new SelectList(preguntaModels.ListaNivel, "Id", "ElNivel");
            #endregion


            #region TipoPregunta

            TipoPreguntaComponent tipoPreguntaComponent = new TipoPreguntaComponent();
            pregunta.ListatipoPregunta = tipoPreguntaComponent.Read();
            pregunta.ListatipoPregunta.Select(X =>
                                new
                                {
                                    Id = X.Id,
                                    TipoDePregunta = X.TipoDePregunta
                                });

            ViewBag.TipoDePreguntaLista = new SelectList(preguntaModels.ListaTipo, "Id", "TipoDePregunta");
            #endregion

            return View();
        }


        [AuthorizerUser(_roles: "CrearPregunta")]
        [HttpPost]
       
        public ActionResult Create(FormCollection formCollection, Pregunta _pregunta, HttpPostedFileBase file)
        {
            Pregunta pregunta = new Pregunta();
            Pregunta result = new Pregunta();
            PreguntaComponent preguntaComponent = new PreguntaComponent();

            if (pregunta.File != null)
            {
                pregunta.File = _pregunta.File;
            }

            pregunta.LaPregunta = formCollection.Get("LaPregunta");
            pregunta.categoria.Id = int.Parse(formCollection.Get("categoria.Id"));
            pregunta.tipoPregunta.Id = int.Parse(formCollection.Get("tipoPregunta.Id"));
            pregunta.nivel.Id = int.Parse(formCollection.Get("nivel.Id"));
            result = preguntaComponent.Create(pregunta);

            if (result is null)
            {
                return RedirectToAction("ErrorPage", new { pregunta = pregunta.LaPregunta });
            }
            else
            {
                return RedirectToAction("index");
            }

        }
        [AuthorizerUser(_roles: "CrearPregunta")]
        public ActionResult ErrorPage(string pregunta)
        {
            Pregunta preguntaError = new Pregunta();
            preguntaError.LaPregunta = pregunta;
            return View(preguntaError);
        }

        // GET: Preguntas/Edit/5
        [AuthorizerUser(_roles: "CrearPregunta")]
        public ActionResult Edit(int id)
        {
            PreguntaModels preguntaModels = new PreguntaModels();
            PreguntaComponent preguntaComponent = new PreguntaComponent();
            CategoriaComponent categoriaComponent = new CategoriaComponent();
            Pregunta pregunta = new Pregunta();



            #region Nivel

            NivelComponent nivelComponent = new NivelComponent();
            pregunta.ListaNivel = nivelComponent.Read();
            pregunta.ListaNivel.Select(w =>
                                new
                                {
                                    Id = w.Id,
                                    ElNivel = w.ElNivel
                                });
            ViewBag.ListaNivel = new SelectList(preguntaModels.ListaNivel, "Id", "ElNivel");
            #endregion


            #region TipoPregunta

            TipoPreguntaComponent tipoPreguntaComponent = new TipoPreguntaComponent();
            pregunta.ListatipoPregunta = tipoPreguntaComponent.Read();
            pregunta.ListatipoPregunta.Select(X =>
                                new
                                {
                                    Id = X.Id,
                                    TipoDePregunta = X.TipoDePregunta
                                });

            ViewBag.TipoDePreguntaLista = new SelectList(preguntaModels.ListaTipo, "Id", "TipoDePregunta");
            #endregion

            return View(preguntaComponent.ReadBy(id));
        }

        // POST: Preguntas/Edit/5
        [AuthorizerUser(_roles: "CrearPregunta")]
        [HttpPost]
    
        public ActionResult Edit(int id, HttpPostedFileBase file, string LaPregunta, string Nivel,FormCollection collection)
        {

            try
            {
                // TODO: Add update logic here
                PreguntaComponent preguntaComponent = new PreguntaComponent();
                PreguntaComponent preguntaComponent2 = new PreguntaComponent();
                Pregunta preguntaAntesDeModificar = new Pregunta();
                Pregunta pregunta = new Pregunta();
                preguntaAntesDeModificar = preguntaComponent.ReadBy(id);

                /*   */
                if (file is null)
                {
                    pregunta.Imagen = preguntaAntesDeModificar.Imagen;
                }
                else
                {
                    string ruta;
                    ruta = @"C:\Imagenes\";
                    ruta += file.FileName;
                    file.SaveAs(ruta);
                    pregunta.Imagen = ruta;
                }
                pregunta.Id = id;
                pregunta.LaPregunta = LaPregunta;

                pregunta.nivel.Id = int.Parse(collection.Get("nivel.id"));
                pregunta.tipoPregunta.Id = preguntaAntesDeModificar.tipoPregunta.Id;
                preguntaComponent2.Update(pregunta);

                return RedirectToAction("PreguntaCategoria", "CreadorPreguntas");
            }
            catch (Exception e)
            {
                return View(e.Message);
            }
        }
    

        // GET: Preguntas/Delete/5
        [AuthorizerUser(_roles: "CrearPregunta")]
        public ActionResult Delete(int id)
        {
            PreguntaComponent pregunta = new PreguntaComponent();
            return View(pregunta.ReadBy(id));
        }

        // POST: Preguntas/Delete/5
        [HttpPost]
        [AuthorizerUser(_roles: "CrearPregunta")]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                PreguntaComponent pregunta = new PreguntaComponent();
                pregunta.Delete(id);
                return RedirectToAction("PreguntaCategoria", "creadorPreguntas");
            }
            catch
            {
                return View();
            }
        }





     
        [AuthorizerUser(_roles: "CrearPregunta")]
        public ActionResult TipoPregunta()
        {
            try
            {
                // TODO: Add delete logic here
                TipoPreguntaComponent pregunta = new TipoPreguntaComponent();
       
                return View(pregunta.Read());
            }
            catch
            {
                return View();
            }
        }
        [AuthorizerUser(_roles: "CrearPregunta")]
        public ActionResult PreguntaCategoria(string Pregunta, string Categoria, string preguntaParcial)
        {
            try
            {

                ViewBag.categoriaLista = new SelectList(CategoriaModels.ListCategoriaOrdenada(), "Id", "LaCategoria");

                ViewBag.categoriaPregunta = new SelectList(PreguntaModels.ListCategoriaOrdenada(), "Id", "LaPregunta");



                PreguntaCategoriaModels preguntaCategoriaModels = new PreguntaCategoriaModels();


                return View(preguntaCategoriaModels.Buscar(Pregunta, Categoria, preguntaParcial));
            }
            catch
            {
                return View();
            }
        }


        [AuthorizerUser(_roles: "CrearPregunta")]
        public ActionResult NivelPregunta()
        {
            try
            {
                // TODO: Add delete logic here
                NivelComponent pregunta = new NivelComponent();

                return View(pregunta.Read());
            }
            catch
            {
                return View();
            }
        }

        [AuthorizerUser(_roles: "CrearPregunta")]
        public ActionResult CategoriaPregunta()
        {
            try
            {
                // TODO: Add delete logic here
                CategoriaComponent pregunta = new CategoriaComponent();

                return View(pregunta.Read());
            }
            catch
            {
                return View();
            }
        }


        [AuthorizerUser(_roles: "CrearPregunta")]
        public ActionResult Categoria(Int16 id)
        {
            PreguntaCategoriaModels preguntaCategoriaModels = new PreguntaCategoriaModels();

            return View(preguntaCategoriaModels.Read(id));
        }


        [AuthorizerUser(_roles: "CrearPregunta")]
        
        public ActionResult AgregarCategoria(int id)
        {
            PreguntaCategoriaModels preguntaCategoriaModels = new PreguntaCategoriaModels();
            PreguntaCategoriaModels result = new PreguntaCategoriaModels();
            result = preguntaCategoriaModels.ObtenerCategoria(id);

            #region Categoria

            result.categorias.Select(y =>
                                new
                                {
                                    Id = y.Id,
                                    LaCategoria = y.LaCategoria
                                });

            ViewBag.categoriaLista = new SelectList(result.categorias, "Id", "LaCategoria");


            #endregion
            return View(result);
        }


        [AuthorizerUser(_roles: "CrearPregunta")]

        [HttpPost]
        public ActionResult AgregarCategoria(FormCollection collection)
        {
            try
            {
                PreguntaCategoriaComponent preguntaCategoriaComponent = new PreguntaCategoriaComponent();

                PreguntaCategoria preguntaCategoria = new PreguntaCategoria();
                if (collection.Count > 1)
                {
                    preguntaCategoria.Categorias.Id = int.Parse(collection.Get("Categoria"));
                    preguntaCategoria.Pregunta.Id = int.Parse(collection.Get("ID_Pregunta"));
                    preguntaCategoriaComponent.Create(preguntaCategoria);
                }




                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }



        [AuthorizerUser(_roles: "Administrador,CrearPregunta,RRHH")]
        // GET: PreguntaCategoria/Delete/5
        public ActionResult QuitarCategoria(int Id_categoria, int Id_Pregunta)
        {
            PreguntaCategoria preguntaCategoria = new PreguntaCategoria(Id_categoria, Id_Pregunta);
            PreguntaCategoriaComponent preguntaCategoriaComponent = new PreguntaCategoriaComponent();


            return View(preguntaCategoriaComponent.ReadBy(preguntaCategoria));
        }


        [AuthorizerUser(_roles: "Administrador,CrearPregunta,RRHH")]
        // POST: PreguntaCategoria/Delete/5
        [HttpPost]
        public ActionResult QuitarCategoria(int Id_categoria, int Id_Pregunta, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                PreguntaCategoria preguntaCategoria = new PreguntaCategoria(Id_categoria, Id_Pregunta);
                PreguntaCategoriaComponent preguntaCategoriaComponent = new PreguntaCategoriaComponent();
                preguntaCategoriaComponent.Delete(preguntaCategoria);
                return RedirectToAction("PreguntaCategoria");
            }
            catch
            {
                return View();
            }
        }
    }
}

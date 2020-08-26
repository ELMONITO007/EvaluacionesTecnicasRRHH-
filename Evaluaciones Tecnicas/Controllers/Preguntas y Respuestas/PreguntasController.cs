using Evaluaciones.Models;
using Entities;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Web.Hosting;
using Evaluaciones_Tecnicas.Filter;

namespace Evaluaciones.Controllers
{
    

    public class PreguntasController : Controller
    {
        // GET: Preguntas
        [AuthorizerUser(_roles: "Administrador,CrearPregunta,RRHH")]
        public ActionResult Index()
        {
           
                    
            PreguntaComponent preguntaComponent = new PreguntaComponent();
            List<Pregunta> listaPreguntas = new List<Pregunta>();
            return View(preguntaComponent.Read());


        }

        // GET: Preguntas/Details/5
        [AuthorizerUser(_roles: "Administrador,CrearPregunta,RRHH")]
        public ActionResult Details(int id)
        {
            PreguntaComponent pregunta = new PreguntaComponent();
            return View(pregunta.ReadBy(id));
        }

        // GET: Preguntas/Create
        [AuthorizerUser(_roles: "Administrador,CrearPregunta,RRHH")]
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
            pregunta.ListaNivel = nivelComponent.Read() ;
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
        [AuthorizerUser(_roles: "Administrador,CrearPregunta,RRHH")]
        public ActionResult ErrorPage(string pregunta)
        {
            Pregunta preguntaError = new Pregunta();
            preguntaError.LaPregunta = pregunta;
            return View(preguntaError);
        }
      
        [HttpPost]
        [AuthorizerUser(_roles: "Administrador,CrearPregunta,RRHH")]
        public ActionResult Create(FormCollection formCollection,Pregunta _pregunta, HttpPostedFileBase file)
        {
            Pregunta pregunta = new Pregunta();
            Pregunta result = new Pregunta();
            PreguntaComponent preguntaComponent = new PreguntaComponent();

            if (pregunta.File!=null)
            {
                pregunta.File = _pregunta.File;
            }
            
            pregunta.LaPregunta = formCollection.Get("LaPregunta");
            pregunta.categoria.Id = int.Parse(formCollection.Get("categoria.Id"));
            pregunta.tipoPregunta.Id = int.Parse(formCollection.Get("tipoPregunta.Id"));
            pregunta.nivel.Id = int.Parse(formCollection.Get("nivel.Id"));
            result= preguntaComponent.Create(pregunta);

            if (result is null)
            {
                return RedirectToAction("ErrorPage", new { pregunta = pregunta.LaPregunta });
            }
            else
            {
                return RedirectToAction("index", "preguntacategoria");
            }
          
        }

        // GET: Preguntas/Edit/5
        [AuthorizerUser(_roles: "Administrador,CrearPregunta,RRHH")]
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
        [HttpPost]
        [AuthorizerUser(_roles: "Administrador,CrearPregunta,RRHH")]
        public ActionResult Edit(int id, HttpPostedFileBase file, string LaPregunta, string Categoria, string Nivel)
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
            
                pregunta.nivel.Id = int.Parse(Nivel);
                pregunta.tipoPregunta.Id = preguntaAntesDeModificar.tipoPregunta.Id;
                preguntaComponent2.Update(pregunta);

                return RedirectToAction("Index", "PreguntaCategoria");
            }
            catch (Exception e)
            {
                return View(e.Message);
            }
        }

        // GET: Preguntas/Delete/5
        [AuthorizerUser(_roles: "Administrador,CrearPregunta,RRHH")]
        public ActionResult Delete(int id)
        {
            PreguntaComponent pregunta = new PreguntaComponent();
            return View(pregunta.ReadBy(id));
        }

        // POST: Preguntas/Delete/5
        [HttpPost]
        [AuthorizerUser(_roles: "Administrador,CrearPregunta,RRHH")]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                PreguntaComponent pregunta = new PreguntaComponent();
                pregunta.Delete(id);
                return RedirectToAction("Index", "PreguntaCategoria");
            }
            catch
            {
                return View();
            }
        }



    }
}

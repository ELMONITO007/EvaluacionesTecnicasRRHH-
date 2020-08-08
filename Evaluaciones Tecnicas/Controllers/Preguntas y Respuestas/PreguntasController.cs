using Evaluaciones.Models;
using Entities;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Evaluaciones.Controllers
{
    [Authorize(Roles = "Administrador,CreadorPreguntas")]//para entrar en admin debe estar logueado y  asignarle el rol

    public class PreguntasController : Controller
    {
        // GET: Preguntas
        public ActionResult Index()
        {
                    
            PreguntaComponent preguntaComponent = new PreguntaComponent();
            List<Pregunta> listaPreguntas = new List<Pregunta>();
            return View(preguntaComponent.Read());


        }

        // GET: Preguntas/Details/5
        public ActionResult Details(int id)
        {
            PreguntaComponent pregunta = new PreguntaComponent();
            return View(pregunta.ReadBy(id));
        }
     
        // GET: Preguntas/Create
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

        public ActionResult ErrorPage(string pregunta)
        {
            Pregunta preguntaError = new Pregunta();
            preguntaError.LaPregunta = pregunta;
            return View(preguntaError);
        }

        // POST: Preguntas/Create
        [HttpPost]
        public ActionResult Create(HttpPostedFileBase file, string LaPregunta, string LaCategoria, string TipoPregunta,string Nivel)
         {
            PreguntaComponent preguntaComponent = new PreguntaComponent();
           
            if (preguntaComponent.ReadBy(LaPregunta))
            {
           
                return RedirectToAction("ErrorPage",new {pregunta=LaPregunta});
            }
            else
            {
                try
                {
                    string ruta = "";
                    if (file != null)
                    {

                        ruta = @"C:\Imagenes\";
                        ruta += file.FileName;
                        file.SaveAs(ruta);
                    }
                    else
                    {
                        ruta = "Sin Imagen";
                    }
                    // TODO: Add insert logic here
                    Pregunta pregunta = new Pregunta(LaPregunta, int.Parse(Nivel), int.Parse(TipoPregunta), int.Parse(LaCategoria), ruta);


                    preguntaComponent.Create(pregunta);

                    return RedirectToAction("Index", "PreguntaCategoria");
                }
                catch
                {
                    return View();
                }
            }
           
        }

        


        // GET: Preguntas/Edit/5
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
        public ActionResult Delete(int id)
        {
            PreguntaComponent pregunta = new PreguntaComponent();
            return View(pregunta.ReadBy(id));
        }

        // POST: Preguntas/Delete/5
        [HttpPost]
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

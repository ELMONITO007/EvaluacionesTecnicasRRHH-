using Entities;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Evaluaciones.Controllers
{
    //[Authorize(Roles = "Administrador,CreadorPreguntas")]//para entrar en admin debe estar logueado y  asignarle el rol
    public class SubPreguntaController : Controller
    {
        // GET: SubPregunta
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ErrorPage(string pregunta)
        {
            Pregunta preguntaError = new Pregunta();
            preguntaError.LaPregunta = pregunta;
            return View(preguntaError);
        }
        // GET: SubPregunta/Details/5
        public ActionResult Details(int id, int id_pregunta)
        {
            PreguntaComponent pregunta = new PreguntaComponent();
            SubPregunta subPregunta = new SubPregunta();

            subPregunta.pregunta = pregunta.ReadBySubPregunta(id);
            subPregunta.Id = id_pregunta;
            return View(subPregunta);
        }

        // GET: SubPregunta/Create
        public ActionResult Create(int id)
        {
            SubPregunta subPregunta = new SubPregunta();
            PreguntaComponent pregunta = new PreguntaComponent();

            subPregunta.pregunta= pregunta.ReadBy(id);
            return View(subPregunta);
        }

        // POST: SubPregunta/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            PreguntaComponent preguntaComponent = new PreguntaComponent();
           string Lapregunta= collection.Get("subPregunta.LaPregunta");

            if (preguntaComponent.ReadBy(Lapregunta))
            {

                return RedirectToAction("ErrorPage", new { pregunta = Lapregunta });
            }
            else
            {

                try
                {
                    // TODO: Add insert logic here
                    SubPreguntaComponent subPreguntaComponent = new SubPreguntaComponent();
                    SubPregunta subPregunta = new SubPregunta();
                    subPregunta.pregunta.Id = int.Parse(collection.Get("pregunta.id"));
                    subPregunta.subPregunta.LaPregunta = collection.Get("subPregunta.LaPregunta");
                    subPreguntaComponent.Create(subPregunta);
                    return RedirectToAction("Index", "MultipleChoiceCompuesto", new { id = subPregunta.pregunta.Id });
                }
                catch (Exception e)
                {
                    return View();
                }
            }
        }

        // GET: SubPregunta/Edit/5
        public ActionResult Edit(int id, int id_pregunta)
        {
            PreguntaComponent pregunta = new PreguntaComponent();
            SubPregunta subPregunta = new SubPregunta();
            
            subPregunta.pregunta = pregunta.ReadBySubPregunta(id);
            subPregunta.Id = id_pregunta;
            return View(subPregunta);
        }

        // POST: SubPregunta/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection,string Id_Pregunta)
        {
            try
            {
                SubPreguntaComponent subPreguntaComponent = new SubPreguntaComponent();
                SubPregunta subPregunta = new SubPregunta();
                subPregunta.pregunta.Id = id;
               
                subPregunta.pregunta.LaPregunta = collection.Get("pregunta.LaPregunta");
                subPreguntaComponent.Update(subPregunta);

                // TODO: Add update logic here

                return RedirectToAction("Index", "MultipleChoiceCompuesto",new { id=int.Parse(Id_Pregunta)});
            }
            catch(Exception e)
            {
                return View();
            }
        }

        // GET: SubPregunta/Delete/5
        public ActionResult Delete(int id, int id_pregunta)
        {
            PreguntaComponent pregunta = new PreguntaComponent();
            SubPregunta subPregunta = new SubPregunta();
            subPregunta.pregunta = pregunta.ReadBySubPregunta(id);
            subPregunta.Id = id_pregunta;
            return View(subPregunta);
        }

        // POST: SubPregunta/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection, string Id_Pregunta)
        {
            try
            {
                // TODO: Add delete logic here
                SubPreguntaComponent subPregunta = new SubPreguntaComponent();
                subPregunta.Delete(id);
                return RedirectToAction("Index", "MultipleChoiceCompuesto", new { id = int.Parse(Id_Pregunta) });
            }
            catch
            {
                return View();
            }
        }
    }
}

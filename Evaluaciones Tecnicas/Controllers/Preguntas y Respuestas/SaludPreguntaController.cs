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
    public class SaludPreguntaController : Controller
    {
        // GET: SaludPregunta
        public ActionResult Index(int id)
        {
            Pregunta pregunta = new Pregunta();
            PreguntaComponent preguntaComponent = new PreguntaComponent();
            pregunta = preguntaComponent.ReadBy(id);
            if (pregunta.tipoPregunta.TipoDePregunta=="MultipleChoice")
            {
                RedirectToAction("SaludMultipleChoice","SaludPregunta", new { id=pregunta.Id});
            }
            else if (pregunta.tipoPregunta.TipoDePregunta == "Ordenado")
            {
                RedirectToAction("SaludOrdenado", "SaludPregunta", new { id = pregunta.Id });
            }
            else
            {
                RedirectToAction("SaludMultipleChoiceCompuesto","SaludPregunta", new { id = pregunta.Id });
            }
            return View();
        }
        public ActionResult SaludMultipleChoice(int id)
        {
            SaludPreguntaComponent saludPregunta = new SaludPreguntaComponent();
            Pregunta pregunta = new Pregunta();
            PreguntaComponent preguntaComponent = new PreguntaComponent();
            pregunta = preguntaComponent.ReadBy(id);


            return View(saludPregunta.VerificarSalud(pregunta));
        }
        public ActionResult SaludMultipleChoiceCompuesto(int id)
        {
            SaludPreguntaComponent saludPregunta = new SaludPreguntaComponent();
            Pregunta pregunta = new Pregunta();
            PreguntaComponent preguntaComponent = new PreguntaComponent();
            pregunta = preguntaComponent.ReadBy(id);


            return View(saludPregunta.VerificarSalud(pregunta));
        }
        // GET: SaludPregunta/Details/5
        public ActionResult SaludOrdenado(int id)
        {
            SaludPreguntaComponent saludPregunta = new SaludPreguntaComponent();
            Pregunta pregunta = new Pregunta();
            PreguntaComponent preguntaComponent = new PreguntaComponent();
            pregunta = preguntaComponent.ReadBy(id);

            return View(saludPregunta.VerificarSalud(pregunta));
        }
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: SaludPregunta/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SaludPregunta/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: SaludPregunta/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: SaludPregunta/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: SaludPregunta/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: SaludPregunta/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}

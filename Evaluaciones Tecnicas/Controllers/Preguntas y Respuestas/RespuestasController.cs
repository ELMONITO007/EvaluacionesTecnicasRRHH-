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

    public class RespuestasController : Controller
    {
        // GET: Respuestas
        public ActionResult Index()
        {

            return View();
        }

        // GET: Respuestas/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }


        // GET: Respuestas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Respuestas/Create
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
        // GET: Respuestas/Create
        public ActionResult Respuesta(int Id_Pregunta)
        {
            PreguntaComponent preguntaComponent = new PreguntaComponent();
            Pregunta pregunta = new Pregunta();
            pregunta = preguntaComponent.ReadBy(Id_Pregunta);

            string TipoRespuesta = pregunta.tipoPregunta.TipoDePregunta;
            if (TipoRespuesta=="MultipleChoice")
            {
                return RedirectToAction("Index", "MultipleChoice", new { id = Id_Pregunta });

            }
            else if (TipoRespuesta=="Ordenado")
            {
                return RedirectToAction("Index", "Orden", new { id = Id_Pregunta });
            }
          
            else if (TipoRespuesta == "MultipleChoiceCompuesto")
            {
                 return RedirectToAction("Index", "MultipleChoiceCompuesto", new { id = Id_Pregunta });
            }
            
            return View();
        }

        // POST: Respuestas/Create
        [HttpPost]
        public ActionResult Respuesta(FormCollection collection)
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

        // GET: Respuestas/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Respuestas/Edit/5
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

        // GET: Respuestas/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Respuestas/Delete/5
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

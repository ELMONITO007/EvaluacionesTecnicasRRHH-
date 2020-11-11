using Entities.Examen;
using Negocio.Examen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Evaluaciones_Tecnicas.Controllers.Examen
{
    public class ExamenRespuestasController : Controller
    {
        // GET: ExamenRespuestas
        public ActionResult Index(int id)
        {
            ExamenRespuestaComponent examenRespuestaComponent = new ExamenRespuestaComponent();
            ExamenComponent examenComponent = new ExamenComponent();
            Entities.Examen.Examen examenRespuestas = new Entities.Examen.Examen();
            examenRespuestas = examenComponent.ReadBy(id);

            examenRespuestas = examenRespuestaComponent.ReadByExamen(id);

           


           
         




            return View(examenRespuestas);
        }

        // GET: ExamenRespuestas/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ExamenRespuestas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ExamenRespuestas/Create
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

        // GET: ExamenRespuestas/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ExamenRespuestas/Edit/5
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

        // GET: ExamenRespuestas/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ExamenRespuestas/Delete/5
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

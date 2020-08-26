using Negocio;
using Entities;

using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Evaluaciones_Tecnicas.Filter;

namespace Safari.UI.Web.Controllers
{
   
    public class TipoPreguntaController : Controller
    {

        // GET: TipoPregunta
        [AuthorizerUser(_roles: "Administrador,CrearPregunta,RRHH")]
        [Route("TipoPregunta", Name = "TipoPreguntaControllerRouteIndex")]
        public ActionResult Index()
        {
            TipoPreguntaComponent tipoPregunta = new TipoPreguntaComponent();
          
           
            return View(tipoPregunta.Read());
        }

        // GET: TipoPregunta/Details/5
        [AuthorizerUser(_roles: "Administrador,CrearPregunta,RRHH")]
        public ActionResult Details(int id)
        {

            TipoPreguntaComponent tipoPregunta = new TipoPreguntaComponent();
            return View(tipoPregunta.ReadBy(id));
        }

        // GET: TipoPregunta/Create
        [AuthorizerUser(_roles: "Administrador,CrearPregunta,RRHH")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: TipoPregunta/Create
        [AuthorizerUser(_roles: "Administrador,CrearPregunta,RRHH")]
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                TipoPreguntaComponent tipoPreguntaComponent = new TipoPreguntaComponent();
                TipoPregunta tipoPregunta = new TipoPregunta();
                tipoPregunta.TipoDePregunta = collection.Get("TipoDePregunta");
                tipoPreguntaComponent.Create(tipoPregunta);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: TipoPregunta/Edit/5
        [AuthorizerUser(_roles: "Administrador,CrearPregunta,RRHH")]
        public ActionResult Edit(int id)
        {
            TipoPreguntaComponent tipoPreguntaComponent = new TipoPreguntaComponent();

            return View(tipoPreguntaComponent.ReadBy(id));

        }

        // POST: TipoPregunta/Edit/5
        [AuthorizerUser(_roles: "Administrador,CrearPregunta,RRHH")]
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
                // TODO: Add update logic here
                TipoPreguntaComponent tipoPreguntaComponent = new TipoPreguntaComponent();
                TipoPregunta tipoPregunta = new TipoPregunta();
                tipoPregunta.TipoDePregunta = collection.Get("TipoDePregunta");
                tipoPregunta.Id = int.Parse(collection.Get("Id"));
                tipoPreguntaComponent.Update(tipoPregunta);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: TipoPregunta/Delete/5
        [AuthorizerUser(_roles: "Administrador,CrearPregunta,RRHH")]
        public ActionResult Delete(int id)
        {

            TipoPreguntaComponent tipoPreguntaComponent = new TipoPreguntaComponent();

            return View(tipoPreguntaComponent.ReadBy(id));
        }
        public ActionResult SaludTipoPregunta(int id)
        {
            SaludCategoriaComponent saludCategoriaComponent = new SaludCategoriaComponent(id);
            return View( saludCategoriaComponent.verificarSaludTipoPregunta());


        }
        // POST: TipoPregunta/Delete/5
        [AuthorizerUser(_roles: "Administrador,CrearPregunta,RRHH")]
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                TipoPreguntaComponent tipoPreguntaComponent = new TipoPreguntaComponent();
                TipoPregunta tipoPregunta = new TipoPregunta();
               
                tipoPregunta.Id = id;
                tipoPreguntaComponent.Delete(id);
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

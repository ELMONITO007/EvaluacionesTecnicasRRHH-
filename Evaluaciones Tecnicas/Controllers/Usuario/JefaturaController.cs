using Entities;
using Evaluaciones_Tecnicas.Filter;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Evaluaciones.Controllers.Usuario_Examen
{
    public class JefaturaController : Controller
    {

        // GET: Jefatura
        [AuthorizerUser(_roles: "Administrador,RRHH")]
        public ActionResult Index()
        {
            JefaturaComponent jefaturaComponent = new JefaturaComponent();

            return View(jefaturaComponent.Read());
        }

        // GET: Jefatura/Details/5
        [AuthorizerUser(_roles: "Administrador,RRHH")]
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Jefatura/Create
        [AuthorizerUser(_roles: "Administrador,RRHH")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Jefatura/Create
        [AuthorizerUser(_roles: "Administrador,RRHH")]
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here
                JefaturaComponent jefaturaComponent = new JefaturaComponent();
                Jefatura jefatura = new Jefatura();
                jefatura.jefatura = collection.Get("jefatura");
                if (jefaturaComponent.Verificar(jefatura))
                {
                    jefaturaComponent.Create(jefatura);
                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("ErrorPage", new { id = jefatura.jefatura});
                }
               
            }
            catch
            {
                return View();
            }
        }

        // GET: Jefatura/Edit/5
        [AuthorizerUser(_roles: "Administrador,RRHH")]
        public ActionResult Edit(int id)
        {
            JefaturaComponent jefaturaComponent = new JefaturaComponent();

            return View(jefaturaComponent.ReadBy(id));
        }

        // POST: Jefatura/Edit/5
        [AuthorizerUser(_roles: "Administrador,RRHH")]
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
                JefaturaComponent jefaturaComponent = new JefaturaComponent();
                Jefatura jefatura = new Jefatura();
                jefatura.jefatura = collection.Get("jefatura");
                jefatura.Id = id;
                if (jefaturaComponent.Verificar(jefatura))
                {
                    jefaturaComponent.Update(jefatura);
                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("ErrorPage", new { id = jefatura.jefatura });
                }
                
              
            }
            catch
            {
                return View();
            }
        }

        // GET: Jefatura/Delete/5
        [AuthorizerUser(_roles: "Administrador,RRHH")]
        public ActionResult Delete(int id)
        {
            JefaturaComponent jefaturaComponent = new JefaturaComponent();

            return View(jefaturaComponent.ReadBy(id));
        }

        // POST: Jefatura/Delete/5
        [AuthorizerUser(_roles: "Administrador,RRHH")]
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                JefaturaComponent jefaturaComponent = new JefaturaComponent();
                jefaturaComponent.Delete(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        [AuthorizerUser(_roles: "Administrador,RRHH")]
        public ActionResult ErrorPage(string id)
        {
            Jefatura jefatura = new Jefatura();
            jefatura.jefatura = id;
            return View(jefatura);

        }
    }
}

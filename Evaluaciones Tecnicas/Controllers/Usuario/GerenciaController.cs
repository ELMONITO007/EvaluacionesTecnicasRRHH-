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
    //[Authorize(Roles = "Administrador")]//para entrar en admin debe estar logueado y  asignarle el rol
    public class GerenciaController : Controller
    {
        // GET: Gerencia
        [AuthorizerUser(_roles: "Administrador,RRHH")]
        public ActionResult Index()
        {
            GerenciaComponent gerenciaComponent = new GerenciaComponent();
            return View(gerenciaComponent.Read());
        }

        // GET: Gerencia/Details/5
        [AuthorizerUser(_roles: "Administrador,RRHH")]
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Gerencia/Create
        [AuthorizerUser(_roles: "Administrador,RRHH")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Gerencia/Create
        [AuthorizerUser(_roles: "Administrador,RRHH")]
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here
                GerenciaComponent gerenciaComponent = new GerenciaComponent();
                Gerencia gerencia = new Gerencia();
                gerencia.gerencia = collection.Get("gerencia");
                if (gerenciaComponent.Verificar(gerencia))
                {
                    gerenciaComponent.Create(gerencia);
                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("ErrorPage", new { id = gerencia.gerencia });
                }
                
            }
            catch
            {
                return View();
            }
        }

        // GET: Gerencia/Edit/5
        [AuthorizerUser(_roles: "Administrador,RRHH")]
        public ActionResult Edit(int id)
        {
            GerenciaComponent gerenciaComponent = new GerenciaComponent();
            return View(gerenciaComponent.ReadBy(id));
        }

        // POST: Gerencia/Edit/5
        [AuthorizerUser(_roles: "Administrador,RRHH")]
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
                GerenciaComponent gerenciaComponent = new GerenciaComponent();
                Gerencia gerencia = new Gerencia();
                gerencia.gerencia = collection.Get("gerencia");
                gerencia.Id = id;
                if (gerenciaComponent.Verificar(gerencia))
                {
                    gerenciaComponent.Update(gerencia);
                    return RedirectToAction("Index");
                }
                else
                {

                    return RedirectToAction("ErrorPage", "Empresa", new { id = gerencia.gerencia });
                }

   
            }
            catch
            {
                return View();
            }
        }

        // GET: Gerencia/Delete/5
        [AuthorizerUser(_roles: "Administrador,RRHH")]
        public ActionResult Delete(int id)
        {
            GerenciaComponent gerenciaComponent = new GerenciaComponent();
            return View(gerenciaComponent.ReadBy(id));
        }

        // POST: Gerencia/Delete/5
        [AuthorizerUser(_roles: "Administrador,RRHH")]
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                GerenciaComponent gerenciaComponent = new GerenciaComponent();
                gerenciaComponent.Delete(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        public ActionResult ErrorPage(string id)
        {
            Gerencia gerencia = new Gerencia();
            gerencia.gerencia = id;
            return View(gerencia);

        }
    }
}

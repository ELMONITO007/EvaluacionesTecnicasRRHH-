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

    public class DireccionController : Controller
    {
        // GET: Direccion
        [AuthorizerUser(_roles: "Administrador,RRHH")]
        public ActionResult Index()
        {
            DireccionComponent direccionComponent = new DireccionComponent();

            return View(direccionComponent.Read());
        }

        // GET: Direccion/Details/5
    [AuthorizerUser(_roles: "Administrador,RRHH")]
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Direccion/Create
        [AuthorizerUser(_roles: "Administrador,RRHH")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Direccion/Create
        [HttpPost]
        [AuthorizerUser(_roles: "Administrador,RRHH")]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here
                DireccionComponent direccionComponent = new DireccionComponent();
                Direccion direccion = new Direccion();
                direccion.direccion = collection.Get("direccion");
                if (direccionComponent.Verificar(direccion))
                {
                    direccionComponent.Create(direccion);
                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("ErrorPage", new { id = direccion.direccion });
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: Direccion/Edit/5
        [AuthorizerUser(_roles: "Administrador,RRHH")]
        public ActionResult Edit(int id)
        {
            DireccionComponent direccion = new DireccionComponent();
            return View(direccion.ReadBy(id));
        }

        // POST: Direccion/Edit/5
        [HttpPost]
        [AuthorizerUser(_roles: "Administrador,RRHH")]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
                DireccionComponent direccionComponent = new DireccionComponent();
                Direccion direccion = new Direccion();
                direccion.direccion = collection.Get("direccion");
                direccion.Id = id;
                if (direccionComponent.Verificar(direccion))
                {
                    direccionComponent.Update(direccion);
                    return RedirectToAction("Index");
                }
                else
                {

                    return RedirectToAction("ErrorPage", "Empresa", new { id = direccion.direccion });
                }
            }
            catch (Exception e)
            {
                return View();
            }
        }

        // GET: Direccion/Delete/5
        [AuthorizerUser(_roles: "Administrador,RRHH")]
        public ActionResult Delete(int id)
        {
            DireccionComponent direccion = new DireccionComponent();
            return View(direccion.ReadBy(id));
        }

        // POST: Direccion/Delete/5
        [AuthorizerUser(_roles: "Administrador,RRHH")]
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                DireccionComponent direccion = new DireccionComponent();
                direccion.Delete(id);
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
            Direccion direccion = new Direccion();
            direccion.direccion = id;
            return View(direccion);

        }
    }
}
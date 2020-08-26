using Entities;
using Evaluaciones_Tecnicas.Filter;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Evaluaciones.Controllers
{
    public class RolesController : Controller
    {
        // GET: Roles
        [AuthorizerUser(_roles: "Administrador,RRHH")]
        public ActionResult Index()
        {
            RolesComponent roles = new RolesComponent();
            return View(roles.Read());
        }

        // GET: Roles/Details/5
        [AuthorizerUser(_roles: "Administrador,RRHH")]
        public ActionResult Details(int id)
        {
            RolesComponent roles = new RolesComponent();
            return View(roles.ReadBy(id));
        }

        // GET: Roles/Create
        [AuthorizerUser(_roles: "Administrador")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Roles/Create
        [AuthorizerUser(_roles: "Administrador")]
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here
                RolesComponent rolesComponent = new RolesComponent();
                Roles roles = new Roles();
                roles.name = collection.Get("name");
                rolesComponent.Create(roles);
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                return View();
            }
        }

        // GET: Roles/Edit/5
        [AuthorizerUser(_roles: "Administrador")]
        public ActionResult Edit(int id)
        {
            RolesComponent rolesComponent = new RolesComponent();
            return View(rolesComponent.ReadBy(id));
        }

        // POST: Roles/Edit/5
        [AuthorizerUser(_roles: "Administrador")]
        [HttpPost]
        public ActionResult Edit(string id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
                RolesComponent rolesComponent = new RolesComponent();
                Roles roles = new Roles();
                roles.name = collection.Get("name");
                roles.id = id;
                rolesComponent.Update(roles);
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                return View();
            }
        }

        // GET: Roles/Delete/5
        [AuthorizerUser(_roles: "Administrador")]
        public ActionResult Delete(int id)
        {

            RolesComponent rolesComponent = new RolesComponent();
            return View(rolesComponent.ReadBy(id));
        }

        // POST: Roles/Delete/5
        [AuthorizerUser(_roles: "Administrador")]
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                RolesComponent rolesComponent = new RolesComponent();
                rolesComponent.Delete(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}

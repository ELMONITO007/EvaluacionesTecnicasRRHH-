using Entities;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace Evaluaciones.Controllers.Usuario_Examen
{
    [Authorize(Roles = "Administrador")]//para entrar en admin debe estar logueado y  asignarle el rol
    public class EmpresaController : Controller
    {
        // GET: Empresa
        public ActionResult Index()
        {
            EmpresaComponent empresa = new EmpresaComponent();
            return View(empresa.Read());
        }

        // GET: Empresa/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Empresa/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Empresa/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here
                EmpresaComponent empresaComponent = new EmpresaComponent();
                Empresa empresa = new Empresa();
                empresa.empresa = collection.Get("empresa");
                if (empresaComponent.Verificar(empresa))
                {
                    empresaComponent.Create(empresa);
                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("ErrorPage",new {id=empresa.empresa});
                }


               
            }
            catch
            {
                return View();
            }
        }

        // GET: Empresa/Edit/5
        public ActionResult Edit(int id)
        {
            EmpresaComponent empresaComponent = new EmpresaComponent();
            return View(empresaComponent.ReadBy(id));
        }

        // POST: Empresa/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                EmpresaComponent empresaComponent = new EmpresaComponent();
                Empresa empresa = new Empresa();
                empresa.empresa = collection.Get("empresa");
                empresa.Id = id;
                if (empresaComponent.Verificar(empresa))
                {
                    empresaComponent.Update(empresa);
                    return RedirectToAction("Index");
                }
                else
                {

                    return RedirectToAction("ErrorPage","Empresa", new { id = empresa.empresa });
                }
            }
            catch (Exception e)
            {
                return View();
            }
        }

        // GET: Empresa/Delete/5
        public ActionResult Delete(int id)
        {
            EmpresaComponent empresaComponent = new EmpresaComponent();
            return View(empresaComponent.ReadBy(id));
        }

        // POST: Empresa/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                EmpresaComponent empresaComponent = new EmpresaComponent();
                empresaComponent.Delete(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult ErrorPage(string id)
        {
            Empresa empresa = new Empresa();
            empresa.empresa = id;
            return View(empresa);

        }
    }
}

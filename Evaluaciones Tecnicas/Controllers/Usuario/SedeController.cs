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
    public class SedeController : Controller
    {
        // GET: Sede
        [AuthorizerUser(_roles: "Administrador,RRHH")]
        public ActionResult Index()
        {
            SedeComponent sedeComponent = new SedeComponent();
            return View(sedeComponent.Read());
        }
        [AuthorizerUser(_roles: "Administrador,RRHH")]

        // GET: Sede/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Sede/Create
        [AuthorizerUser(_roles: "Administrador,RRHH")]
        public ActionResult Create()
        {
            EmpresaComponent     empresaComponent = new EmpresaComponent();
            Sede sede = new Sede();
            sede.ListaEmpresas = empresaComponent.Read();
            sede.ListaEmpresas.Select(y =>
                                new
                                {
                                    Id = y.Id,
                                    empresa = y.empresa
                                });
            ViewBag.EmpresaLista = new SelectList(sede.ListaEmpresas, "Id", "empresa");
            return View();
        }

        // POST: Sede/Create
        [AuthorizerUser(_roles: "Administrador,RRHH")]
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here
                Sede sede = new Sede();
                sede.sede = collection.Get("sede");
                sede.codigo = collection.Get("codigo");
                sede.empresa.Id = int.Parse(collection.Get("empresa.Id"));
                SedeComponent sedeComponent = new SedeComponent();
                if (sedeComponent.Verificar(sede))
                {
                    sedeComponent.Create(sede);
                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("ErrorPage", new { id = sede.sede,empresa=sede.empresa.empresa });

                }
                
            }
            catch (Exception e)
            {
                return View();
            }
        }

        // GET: Sede/Edit/5
        [AuthorizerUser(_roles: "Administrador,RRHH")]
        public ActionResult Edit(int id)
        {
            EmpresaComponent empresaComponent = new EmpresaComponent();
            SedeComponent sedeComponent = new SedeComponent();
            Sede sede = new Sede();
            sede.ListaEmpresas = empresaComponent.Read();
            sede.ListaEmpresas.Select(y =>
                                new
                                {
                                    Id = y.Id,
                                    empresa = y.empresa
                                });
            ViewBag.EmpresaLista = new SelectList(sede.ListaEmpresas, "Id", "empresa");
            return View(sedeComponent.ReadBy(id));
        }

        // POST: Sede/Edit/5
        [AuthorizerUser(_roles: "Administrador,RRHH")]
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
                Sede sede = new Sede();
                sede.Id = id;
                sede.sede = collection.Get("sede");
                sede.codigo = collection.Get("codigo");
                sede.empresa.Id = int.Parse(collection.Get("empresa.Id"));
                SedeComponent sedeComponent = new SedeComponent();
                if (sedeComponent.Verificar(sede))
                {
                    sedeComponent.Update(sede);
                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("ErrorPage", new { id = sede.sede, empresa = sede.empresa.empresa });

                }
               
           
            }
            catch (Exception e)
            {
                return View();
            }
        }

        // GET: Sede/Delete/5
        [AuthorizerUser(_roles: "Administrador,RRHH")]
        public ActionResult Delete(int id)
        {
            SedeComponent sedeComponent = new SedeComponent();
            return View(sedeComponent.ReadBy(id));
        }

        // POST: Sede/Delete/5
        [AuthorizerUser(_roles: "Administrador,RRHH")]
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                SedeComponent sedeComponent = new SedeComponent();
                sedeComponent.Delete(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        [AuthorizerUser(_roles: "Administrador,RRHH")]
        public ActionResult ErrorPage(string id,string empresa)
        {
            Sede sede = new Sede();
            sede.sede = id;
            sede.empresa.empresa = empresa;
            return View(sede);

        }
    }
}

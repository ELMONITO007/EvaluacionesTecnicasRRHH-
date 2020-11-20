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
    //[Authorize(Roles = "Administrador")]//para entrar en admin debe estar logueado y  asignarle el rol

    public class CategoriaController : Controller
    {
        [AuthorizerUser(_roles: "Administrador,RRHH")]
        public ActionResult SaludTipoPregunta(int id)
        {
            SaludCategoriaComponent saludCategoriaComponent = new SaludCategoriaComponent(id);
            SaludCategoria salud = new SaludCategoria();
            salud = saludCategoriaComponent.verificarSaludTipoPregunta();
            salud.CantidadDePreguntasBuenaSalud = saludCategoriaComponent.CantidadPreguntaConBuenaSalud();
            return View(salud);


        }
        // GET: Categoria
        [AuthorizerUser(_roles: "Administrador")]
        public ActionResult Index()
        {
            CategoriaComponent categoriaComponent = new CategoriaComponent();

            return View(categoriaComponent.Read());
        }
        // GET: Categoria
        [AuthorizerUser(_roles: "RRHH")]
        public ActionResult IndexRRHH()
        {
            CategoriaComponent categoriaComponent = new CategoriaComponent();

            return View(categoriaComponent.Read());
        }

        // GET: Categoria/Details/5
        [AuthorizerUser(_roles: "Administrador")]
        public ActionResult Details(int id)
        {
            CategoriaComponent categoriaComponent = new CategoriaComponent();

            return View(categoriaComponent.ReadBy(id));
        }

        // GET: Categoria/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Categoria/Create
        [AuthorizerUser(_roles: "Administrador")]
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here
                CategoriaComponent categoriaComponent = new CategoriaComponent();
                Categoria categoria = new Categoria();
                categoria.LaCategoria= collection.Get("LaCategoria");
                categoria.Descripcion = collection.Get("Descripcion");
                categoriaComponent.Create(categoria);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Categoria/Edit/5
        [AuthorizerUser(_roles: "Administrador")]
        public ActionResult Edit(int id)
        {
            CategoriaComponent categoriaComponent = new CategoriaComponent();

            return View(categoriaComponent.ReadBy(id));
        }

        // POST: Categoria/Edit/5
        [AuthorizerUser(_roles: "Administrador")]
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
                CategoriaComponent categoriaComponent = new CategoriaComponent();
                Categoria categoria = new Categoria();
                categoria.Id = id;
                categoria.LaCategoria = collection.Get("LaCategoria");
                categoria.Descripcion = collection.Get("Descripcion");
                categoriaComponent.Update(categoria);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Categoria/Delete/5
        [AuthorizerUser(_roles: "Administrador")]
        public ActionResult Delete(int id)
        {
            CategoriaComponent categoriaComponent = new CategoriaComponent();

            return View(categoriaComponent.ReadBy(id));
        }

        // POST: Categoria/Delete/5
        [AuthorizerUser(_roles: "Administrador")]
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                CategoriaComponent categoriaComponent = new CategoriaComponent();
                categoriaComponent.Delete(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}

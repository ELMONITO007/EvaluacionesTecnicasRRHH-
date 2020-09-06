using Negocio;
using Negocio.Examen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Evaluaciones_Tecnicas.Controllers.Examen
{
    public class ExamenController : Controller
    {
        // GET: Examen
        public ActionResult Index()
        {
            ExamenComponent examenComponent = new ExamenComponent(); 
            return View(examenComponent.Read());
        }

        // GET: Examen/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Examen/Create
        public ActionResult Create(int id_Usuario)
        {
            Entities.Examen.Examen examen = new Entities.Examen.Examen();
            UsuariosComponent usuariosComponent = new UsuariosComponent();
            examen.usuario = usuariosComponent.ReadBy(id_Usuario);
            CategoriaComponent categoriaComponent = new CategoriaComponent();
            examen.listaCategoria = categoriaComponent.Read();
            examen.listaCategoria.Select(y =>
                                new
                                {
                                    y.Id,
                                    y.LaCategoria
                                });

            ViewBag.ListaCategoria = new SelectList(examen.listaCategoria, "Id", "LaCategoria");


            return View(examen);
        }

        // POST: Examen/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here
               Entities.Examen.Examen examen=new Entities.Examen.Examen();
                ExamenComponent examenComponent = new ExamenComponent();
                examen.usuario.Id = int.Parse(collection.Get("usuario.Id"));
                examen.Categoria.Id= int.Parse(collection.Get("Categoria.LaCategoria"));
                examenComponent.Create(examen);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Examen/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Examen/Edit/5
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

        // GET: Examen/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Examen/Delete/5
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

        public ActionResult ErrorPage(Entities.Examen.Examen examen)
        {
            return View(examen);
        }


        public ActionResult VerExamenSistema(int id)
        {
            ExamenComponent examen = new ExamenComponent();
            return View(examen.ReadBy(id));

        }
    }
}

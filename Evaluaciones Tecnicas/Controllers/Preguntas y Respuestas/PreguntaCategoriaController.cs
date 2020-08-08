using Evaluaciones.Models;
using Entities;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Evaluaciones.Controllers
{
    [Authorize(Roles = "Administrador,CreadorPreguntas")]//para entrar en admin debe estar logueado y  asignarle el rol

    public class PreguntaCategoriaController : Controller
    {
        // GET: PreguntaCategoria
        public ActionResult Index(string Pregunta, string Categoria, string preguntaParcial)
        {
            
           
            ViewBag.categoriaLista = new SelectList(CategoriaModels.ListCategoriaOrdenada(), "Id", "LaCategoria");

            ViewBag.categoriaPregunta = new SelectList(PreguntaModels.ListCategoriaOrdenada(), "Id", "LaPregunta");



            PreguntaCategoriaModels preguntaCategoriaModels = new PreguntaCategoriaModels();
           
           
                return View(preguntaCategoriaModels.Buscar(Pregunta, Categoria, preguntaParcial));
           

           
           

            
          
        }

        // GET: PreguntaCategoria/Details/5
        public ActionResult Categoria(Int16 id)
        {
            PreguntaCategoriaModels preguntaCategoriaModels = new PreguntaCategoriaModels();
      
            return View(preguntaCategoriaModels.Read(id));
        }

        // GET: PreguntaCategoria/Create
        public ActionResult Create( int id)
        {
            PreguntaCategoriaModels preguntaCategoriaModels = new PreguntaCategoriaModels();
            PreguntaCategoriaModels result = new PreguntaCategoriaModels();
          result=  preguntaCategoriaModels.ObtenerCategoria(id);

            #region Categoria
            
            result.categorias.Select(y =>
                                new
                                {
                                    Id = y.Id,
                                    LaCategoria = y.LaCategoria
                                });

            ViewBag.categoriaLista = new SelectList(result.categorias, "Id", "LaCategoria");


            #endregion
            return View(result);
        }

        // POST: PreguntaCategoria/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                PreguntaCategoriaComponent preguntaCategoriaComponent = new PreguntaCategoriaComponent();

                PreguntaCategoria preguntaCategoria = new PreguntaCategoria();
                if (collection.Count >1)
                {
                    preguntaCategoria.Categorias.Id = int.Parse(collection.Get("Categoria"));
                    preguntaCategoria.Pregunta.Id = int.Parse(collection.Get("ID_Pregunta"));
                    preguntaCategoriaComponent.Create(preguntaCategoria);
                }
                
               
               

                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: PreguntaCategoria/Edit/5
        public ActionResult Edit(int id)
        {

         
            return View();
        }

        // POST: PreguntaCategoria/Edit/5
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

        // GET: PreguntaCategoria/Delete/5
        public ActionResult Delete(int Id_categoria, int Id_Pregunta)
        {
            PreguntaCategoria preguntaCategoria = new PreguntaCategoria(Id_categoria, Id_Pregunta);
            PreguntaCategoriaComponent preguntaCategoriaComponent = new PreguntaCategoriaComponent();


            return View(preguntaCategoriaComponent.ReadBy(preguntaCategoria));
        }

        // POST: PreguntaCategoria/Delete/5
        [HttpPost]
        public ActionResult Delete(int Id_categoria, int Id_Pregunta,FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                PreguntaCategoria preguntaCategoria = new PreguntaCategoria(Id_categoria, Id_Pregunta);
                PreguntaCategoriaComponent preguntaCategoriaComponent = new PreguntaCategoriaComponent();
                preguntaCategoriaComponent.Delete(preguntaCategoria);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}

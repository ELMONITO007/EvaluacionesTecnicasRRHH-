using Entities;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Evaluaciones.Controllers
{
    //[Authorize(Roles = "Administrador,CreadorPreguntas")]//para entrar en admin debe estar logueado y  asignarle el rol

    public class OrdenController : Controller
    {
        // GET: Orden
        public ActionResult Index(int id)
        {
            OrdenComponent ordenComponent = new OrdenComponent();
            
            
                
            return View(ordenComponent.listaRespuestaOrdenAlAzar(id));
        }

        // GET: Orden/Details/5
        public ActionResult Details(int id)
        {
            OrdenComponent ordenComponent = new OrdenComponent();
            return View(ordenComponent.ReadBy(id));
        }

        // GET: Orden/Create
        public ActionResult Create(int id)
        {
            try
            {
                OrdenComponent ordenComponent = new OrdenComponent();
                PreguntaComponent preguntaComponent = new PreguntaComponent();
                Pregunta pregunta = new Pregunta();
                pregunta = preguntaComponent.ReadBy(id);
                Orden orden = new Orden();
                orden.pregunta = pregunta;

                orden.OrdenesDisponibles = ordenComponent.OrdenDiponible(id);
                List<SelectListItem> lst = new List<SelectListItem>();
                if (orden.OrdenesDisponibles.Count != 0)
                {
                    foreach (int item in orden.OrdenesDisponibles)
                    {
                     

                        lst.Add(new SelectListItem() { Text = item.ToString(), Value = item.ToString() });
                      

                       

                    }
                    ViewBag.Opciones = lst;
                    return View(orden);
                }
                else
                {
                    lst.Add(new SelectListItem() { Text = "", Value ="0"});
                    ViewBag.Opciones = lst;
                 return   RedirectToAction("ErrorCreate", new { id = id });
                }


             
               
            }
            catch (Exception e )
            {

                throw;
            }
          
        }

        // POST: Orden/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                Orden orden = new Orden();
                orden.LaRespuesta = collection.Get("LaRespuesta");
                orden.NumeroOrden = int.Parse(collection.Get("NumeroOrden"));
                orden.pregunta.Id = int.Parse(collection.Get("pregunta.Id")); ;
                OrdenComponent ordenComponent = new OrdenComponent();
                
                ordenComponent.Create(orden);
                // TODO: Add insert logic here

                return RedirectToAction("Index", new { id = orden.pregunta.Id });
            }
            catch
            {
                return View();
            }
        }

        public ActionResult ErrorCreate(int id)
        {
            PreguntaComponent pregunta = new PreguntaComponent();
            return View(pregunta.ReadBy(id));

        }
        // GET: Orden/Edit/5
        public ActionResult Edit(int id)
        {
            OrdenComponent ordenComponent = new OrdenComponent();
            Orden orden = new Orden();
            orden = ordenComponent.ReadBy(id);
            orden.OrdenesDisponibles=ordenComponent.OrdenDiponible(orden.pregunta.Id);
            List<SelectListItem> lst = new List<SelectListItem>();
            if (orden.OrdenesDisponibles.Count != 0)
            {
                foreach (int item in orden.OrdenesDisponibles)
                {


                    lst.Add(new SelectListItem() { Text = item.ToString(), Value = item.ToString() });




                }
              lst.Add(new SelectListItem() { Text = orden.NumeroOrden.ToString(), Value = orden.NumeroOrden.ToString() });
                ViewBag.Opciones = lst;
               
            }
            else
            {
                lst.Add(new SelectListItem() { Text = orden.NumeroOrden.ToString(), Value = orden.NumeroOrden.ToString() });
                ViewBag.Opciones = lst;
             
            }
            return View(orden);
        }

        // POST: Orden/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
                Orden orden = new Orden();
                orden.Id = id;
                orden.LaRespuesta = collection.Get("LaRespuesta");
                orden.NumeroOrden = int.Parse(collection.Get("NumeroOrden"));
                orden.pregunta.Id = int.Parse(collection.Get("pregunta.Id")); ;
                OrdenComponent ordenComponent = new OrdenComponent();
                ordenComponent.Update(orden);
                return RedirectToAction("Index", new { id = orden.pregunta.Id });
            }
            catch
            {
                return View();
            }
        }


    
        
        // GET: Orden/Delete/5
        public ActionResult Delete(int id)
        {
            OrdenComponent ordenComponent = new OrdenComponent();
            return View(ordenComponent.ReadBy(id));
        }

        // POST: Orden/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                OrdenComponent orden = new OrdenComponent();
                orden.Delete(id);
                return RedirectToAction("Index",new { id =int.Parse( collection.Get("pregunta.Id") )});
            }
            catch (Exception e)
            {
                return View();
            }
        }
    }
}

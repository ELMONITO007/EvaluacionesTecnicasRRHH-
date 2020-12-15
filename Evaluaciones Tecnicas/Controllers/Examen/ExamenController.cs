using Entities;
using Entities.Examen;
using Evaluaciones_Tecnicas.Filter;
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
        [AuthorizerUser(_roles: "Administrador,RRHH")]
        // GET: Examen
        public ActionResult Index()
        {
            ExamenComponent examenComponent = new ExamenComponent(); 
            return View(examenComponent.Read());
        }
        [AuthorizerUser(_roles: "Administrador,RRHH")]
        // GET: Examen/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }
        [AuthorizerUser(_roles: "Administrador,RRHH")]
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
        [AuthorizerUser(_roles: "Administrador,RRHH")]
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
             examen.cantidadPreguntas= int.Parse(collection.Get("CantidadPreguntas"));
                examen.Categoria.Id= int.Parse(collection.Get("Categoria.LaCategoria"));
                examenComponent.Create(examen);
                if (examen.error=="Salud")
                {
                  
                    return RedirectToAction("ErrorPage", new { cantidas = examen.cantidadPreguntas });
                }
                else if (examen.error == "Creado")
                {
                    return RedirectToAction("Index");
                }
                else
                {
                
                    return RedirectToAction("ErrorPage", new { cantidas = examen.error });
                    
                }
            }
            catch (Exception e)
            {
                return View();
            }
        }
           [AuthorizerUser(_roles: "Administrador,RRHH")]


    
        // GET: Examen/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }
        [AuthorizerUser(_roles: "Administrador,RRHH")]
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
        [AuthorizerUser(_roles: "Administrador,RRHH")]
        // GET: Examen/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }
        [AuthorizerUser(_roles: "Administrador,RRHH")]
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
        [AuthorizerUser(_roles: "Administrador,RRHH")]
        public ActionResult ErrorPage(int cantidas)
        {
            Entities.Examen.Examen examen = new Entities.Examen.Examen();
            examen.cantidadPreguntas = cantidas;
      
            return View(examen);
        }
        [AuthorizerUser(_roles: "Administrador,RRHH")]

        public ActionResult VerExamenSistema(int id)
        {
            ExamenComponent examen = new ExamenComponent();
            return View(examen.ReadBy(id));

        }


        [AuthorizerUser(_roles: "Examen")]
        public ActionResult VerExamenUsuario()
        {
            Usuarios usuarios = new Usuarios();

            usuarios = (Usuarios)Session["UserName"];
            string Email = usuarios.Email;
            UsuariosComponent usuariosComponent = new UsuariosComponent();
            usuarios = usuariosComponent.ReadByEmail(Email);
            ExamenComponent examen = new ExamenComponent();

            return View(examen.VerExamenUsuario(usuarios.Id));

        }



        [HttpPost]
        public ActionResult VerExamenUsuario(List<Pregunta> listaPregunta, FormCollection collection)


        {
         
            
            ExamenComponent examenComponent = new ExamenComponent();
            Entities.Examen.Examen examen = new Entities.Examen.Examen();
            examen.Id =int.Parse( collection.Get("Id"));
            examen.usuario.Id= int.Parse(collection.Get("usuario.Id"));
            examen.Categoria.Id = int.Parse(collection.Get("categoria.Id"));
            examen.pregunta.ListaPregunta = listaPregunta;
            examenComponent.Terminarexamen(listaPregunta,examen);
            Session["UserName"] = null;
            Session.Abandon();

            return RedirectToAction("index", "home");
        }

    }
}

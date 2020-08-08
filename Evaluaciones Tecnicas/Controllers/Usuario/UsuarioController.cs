
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Evaluaciones.Models;
using System.Threading.Tasks;
using Negocio;
using Entities;
using System.Web.UI.WebControls;



namespace Evaluaciones.Controllers
{
    //[Authorize(Roles = "Administrador")]//para entrar en admin debe estar logueado y  asignarle el rol
    public class UsuarioController : Controller
    {
        //[Route("Usuarios", Name = "UsuariosControllerRouteIndex")]
        // GET: Usuario
        public ActionResult Index()
        {
            UsuariosComponent usuariosComponent = new UsuariosComponent();

            return View(usuariosComponent.Read());
        }

        // GET: Usuario/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }
        public ActionResult Create()
        {
            return View();
        }
        // Post: Usuario/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                Usuarios usuario = new Usuarios();
                usuario.Password = collection.Get("Password");
                usuario.Email = collection.Get("Email");
                usuario.UserName = collection.Get("Email");
                usuario.Nombre= collection.Get("Nombre");
                usuario.Apellido = collection.Get("Apellido");
                UsuariosComponent usuariosComponent = new UsuariosComponent();
                
              bool result = usuariosComponent.Crear(usuario);

                if (result)
                {
                    return RedirectToAction("index");
                }
                else
                {
                    RedirectToAction("ErrorPage");
                }
            }
            catch (Exception e)
            {
                return RedirectToAction("index");
                throw;
            }

            return RedirectToAction("index");

        }

        public ActionResult ErrorPage(String Username)
        {
            Usuarios usuarios = new Usuarios();
            usuarios.UserName = Username;
            return View(usuarios);
        }

        // GET: Usuario/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Usuario/Edit/5
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

        // GET: Usuario/Delete/5
        public ActionResult Delete(int id)
        {
            UsuariosComponent usuariosComponent = new UsuariosComponent();
            return View(usuariosComponent.ReadBy(id));
        }

        // POST: Usuario/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                UsuariosComponent usuariosComponent = new UsuariosComponent();
                Usuarios usuarios = new Usuarios();
                usuarios.Id = id;
                usuariosComponent.Delete(id);
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}

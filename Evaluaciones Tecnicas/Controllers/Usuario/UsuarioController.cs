
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Evaluaciones.Models;
using System.Threading.Tasks;
using Negocio;
using Entities;
using System.Web.UI.WebControls;
using Negocio.Servicios;
using Evaluaciones_Tecnicas.Filter;
using System.Linq;

namespace Evaluaciones.Controllers
{
    //[Authorize(Roles = "Administrador")]//para entrar en admin debe estar logueado y  asignarle el rol
    public class UsuarioController : Controller
    {
        [AuthorizerUser(_roles: "Administrador,RRHH")]
        // GET: Usuario
        public ActionResult Index()
        {
            UsuariosComponent usuariosComponent = new UsuariosComponent();
           
            return View(usuariosComponent.Read());
        }

        // GET: Usuario/Details/5
        [AuthorizerUser(_roles: "Administrador,RRHH")]
        public ActionResult Details(int id)
        {
            return View();
        }
        [AuthorizerUser(_roles: "Administrador,RRHH")]
        public ActionResult Create()
        {
            Usuarios usuarios = new Usuarios();
            RolesComponent rolesComponent = new RolesComponent();
            usuarios.roles = rolesComponent.Read();
            usuarios.roles.Select(y =>
                                  new
                                  {
                                      y.id,
                                    y.name
                                  });

            ViewBag.categoriaLista = new SelectList(usuarios.roles, "name", "name");

            return View(usuarios);
        }
        // Post: Usuario/Create
        [AuthorizerUser(_roles: "Administrador,RRHH")]
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
                usuario.unRol.name = collection.Get("unRol.name");
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

        [AuthorizerUser(_roles: "Administrador,RRHH")]
        public ActionResult UsuarioExamen()
        {

            return View();
        }

        [HttpPost]
        [AuthorizerUser(_roles: "Administrador,RRHH")]
        public ActionResult UsuarioExamen(FormCollection collection)
        {
            return View();

        }

        [AuthorizerUser(_roles: "Administrador,RRHH")]
        public ActionResult ErrorPage(String Username)
        {
            Usuarios usuarios = new Usuarios();
            usuarios.UserName = Username;
            return View(usuarios);
        }

        // GET: Usuario/Edit/5
        [AuthorizerUser(_roles: "Administrador,RRHH")]
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Usuario/Edit/5
        [AuthorizerUser(_roles: "Administrador,RRHH")]
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
        [AuthorizerUser(_roles: "Administrador,RRHH")]
        public ActionResult Delete(int id)
        {
            UsuariosComponent usuariosComponent = new UsuariosComponent();
            return View(usuariosComponent.ReadBy(id));
        }

        // POST: Usuario/Delete/5
        [HttpPost]
        [AuthorizerUser(_roles: "Administrador,RRHH")]
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

        [AuthorizerUser(_roles: "Administrador,RRHH")]
        public ActionResult Desbloquear(int id)
        {
            UsuariosComponent usuariosComponent = new UsuariosComponent();
            usuariosComponent.Desloquear(id);
            return RedirectToAction("Index");
        }
    }
}

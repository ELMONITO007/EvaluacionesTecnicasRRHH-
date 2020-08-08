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
    //[Authorize(Roles = "Administrador")]//para entrar en admin debe estar logueado y  asignarle el rol
    public class UsuarioRolesController : Controller
    {
        // GET: UsuarioRoles
        public ActionResult Index(int id)
        {
            UsuarioRolesComponent usuarioRolesComponent = new UsuarioRolesComponent();
            List<UsuarioRoles> usuarioRoles = new List<UsuarioRoles>();
            usuarioRoles = usuarioRolesComponent.ReadByUsuario(id);
            if (usuarioRoles.Count==0)
            {
                UsuarioRoles usuario = new UsuarioRoles();
                usuario.usuarios.Id = id;
                usuarioRoles.Add(usuario);
                    
            }

            return View(usuarioRoles);
        }

        // GET: UsuarioRoles/Details/5
        public ActionResult Details(int id,string idRol)
        {
            UsuarioRolesComponent usuarioRolesComponent = new UsuarioRolesComponent();
            return View(usuarioRolesComponent.ReadBy(idRol,id));
        }


        public ActionResult Usuario (int Id)
        {
            UsuarioRolesComponent usuarioRolesComponent = new UsuarioRolesComponent();
            return View(usuarioRolesComponent.ReadByUsuario(Id));
        }
        public ActionResult Roles(String Id)
        {
            UsuarioRolesComponent usuarioRolesComponent = new UsuarioRolesComponent();
            return View(usuarioRolesComponent.ReadByRoles(Id));
        }

        // GET: UsuarioRoles/Create
        public ActionResult Create(int id)
        {
            UsuarioRolesModels usuarioRoles = new UsuarioRolesModels();
            List<UsuarioRolesModels> result = new List<UsuarioRolesModels>();
            result = usuarioRoles.obtenerRolesDisponiblesDelUsuario(id);
            result.Select(y =>
                            new
                            {
                                id_Rol = y.id_Rol,
                                name = y.name
                            });

            ViewBag.RolesLista = new SelectList(result, "id_Rol", "name");




            return View(usuarioRoles.ReadyBy(id));
        }

        // POST: UsuarioRoles/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                UsuarioRolesComponent usuarioRolesComponent = new UsuarioRolesComponent();
                UsuarioRoles usuarioRoles = new UsuarioRoles();
                usuarioRoles.roles.id = collection.Get("id_Rol");
                usuarioRoles.usuarios.Id =int.Parse( collection.Get("id_Usuario"));
                usuarioRolesComponent.Create(usuarioRoles);
                // TODO: Add insert logic here

                return RedirectToAction("Index",new { id = usuarioRoles.usuarios.Id });
            }
            catch (Exception e)
            {
                return View();
            }
        }

        // GET: UsuarioRoles/Edit/5
        public ActionResult Edit(int id_Usuario, String id_Roles)
        {
            try
            {
                UsuarioRolesModels usuarioRoles = new UsuarioRolesModels();
                List<UsuarioRolesModels> result = new List<UsuarioRolesModels>();
                result = usuarioRoles.obtenerRolesDisponiblesDelUsuario(id_Usuario);
                result.Select(y =>
                                new
                                {
                                    id_Rol = y.id_Rol,
                                    name = y.name
                                });

                ViewBag.RolesLista = new SelectList(result, "id_Rol", "name");
                    

                return View(usuarioRoles.ReadBy(id_Roles, id_Usuario));
            }
            catch (Exception e)
            {

                throw;
            }
          
        }

        // POST: UsuarioRoles/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                UsuarioRolesComponent usuarioRolesComponent = new UsuarioRolesComponent();
                UsuarioRoles usuario = new UsuarioRoles();
                usuario.roles.id = collection.Get("id_Rol");
                usuario.usuarios.Id =int.Parse( collection.Get("id_Usuario"));
                usuarioRolesComponent.Update(usuario);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: UsuarioRoles/Delete/5
        public ActionResult Delete(string id_Usuario, String id_Roles)
        {
            return View();
        }

        // POST: UsuarioRoles/Delete/5
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
    }
}


using Entities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using CaptchaMvc.HtmlHelpers;
using Negocio;
using Negocio.Servicios;
using Evaluaciones_Tecnicas.Filter;

namespace Evaluaciones.Controllers
{
    [ExceptionFilter]
    public class LoginController : Controller
    {
        // GET: Login
        [ExceptionFilter]
        public ActionResult Index()
        {

            return View();
        }

        public ActionResult Login()
        {

            return View();
        }
        [HttpPost]
        public ActionResult Login(Usuarios usuarios)
        {

            return View();
        }

        [AllowAnonymous]
        public ActionResult Usuarios()
        {

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Usuarios(Usuarios usuarios)
        {
            //if (this.IsCaptchaValid("Ingrese las letras correctamente"))
            //{
                if (ModelState.IsValid)
                {

                    LoginComponent loginComponent = new LoginComponent();
                    LoginError loginError = new LoginError();
                    loginError = loginComponent.VerificarLogin(usuarios);
                    if (loginError.error == "")
                    {
                        Session["UserName"] = usuarios;
                       


                        return RedirectToAction("index","admin");
                    }


                    else
                    {
                        ViewBag.ErrorLogin = loginError.error;
                        return View("index");
                    }

                }
                else
                {
                    ViewBag.ErrorLogin = "Error en el usuario o contraseña";
                    return View("index");
                }
            //}
            //else
            //{
            //    BitacoraComponent bitacoraComponent = new BitacoraComponent();
            //    Entities.Servicios.Bitacora.Bitacora bitacora = new Entities.Servicios.Bitacora.Bitacora();
               
            //    UsuariosComponent usuariosComponent = new UsuariosComponent();
            //    Usuarios unusuario = new Usuarios();
            //    unusuario = usuariosComponent.ReadByEmail(usuarios.Email);
            //    if ( unusuario is null)
            //    {

            //    }
            //    else
            //    {
            //        bitacora.usuarios = unusuario;
            //        bitacora.eventoBitacora.Id = 8;
            //        bitacoraComponent.Create(bitacora);
            //    }


            //    ViewBag.ErrorLogin = "Error en el Captcha";
            //    return View("index");
            //}


        }

        // GET: Login/Details/5
        public ActionResult Perfil(string email)
        {
            UsuariosComponent usuariosComponent = new UsuariosComponent();
            return View(usuariosComponent.ReadByEmail(email));
         
        }

        // GET: Login/Create
        public ActionResult Create()
        {

            return View();
        }

        // POST: Login/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Login/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Login/Edit/5
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

        // GET: Login/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Login/Delete/5
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

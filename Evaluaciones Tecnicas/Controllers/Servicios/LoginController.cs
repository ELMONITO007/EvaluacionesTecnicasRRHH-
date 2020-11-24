
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
using System.Net;
using System.Net.Http;
using reCAPTCHA.MVC;
using Newtonsoft.Json.Linq;

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
        [CaptchaValidator(ErrorMessage = "Validación Captcha incorrecta.",
                  RequiredMessage = "La validación Captcha es requerida.")]
        public ActionResult Index(Usuarios usuarios, bool captchaValid)
        {
            if (!captchaValid)
            {
               
                return View("index");
            }
            else
            {

           
            LoginComponent loginComponent = new LoginComponent();
                LoginError loginError = new LoginError();
                loginError = loginComponent.VerificarLogin(usuarios);
          
            if (loginError.error == "")
            {
                
                Session["UserName"] = usuarios;
                    UsuariosComponent usuariosComponent = new UsuariosComponent();
                    Usuarios unUsuario = new Usuarios();
                    unUsuario = usuariosComponent.ReadByEmail(usuarios.Email);
                    UsuarioRolesComponent usuarioRolesComponent = new UsuarioRolesComponent();
                    string pagina = "";
    

                    foreach (UsuarioRoles item in usuarioRolesComponent.obtenerRolesDisponiblesDelUsuario(unUsuario.Id))
                    {
                        pagina = item.roles.name;
                        break;

               

                      }
                #region Layout
                if (pagina == "Administrador")
                {
                    Session["Layout"] = "_Layout2";
                }
                if (pagina == "RRHH")
                {
                    Session["Layout"] = "_LayoutRRHH";
                }
                if (pagina == "CrearPregunta")
                {
                    Session["Layout"] = "_LayoutCreador";
                }

                #endregion
                #region Error
                ViewBag.ErrorLogin = loginError.error;
                return RedirectToAction(pagina, "Admin");
            }
            else
            {
                ViewBag.ErrorLogin = loginError.error;

                return View("index");
            }
                #endregion

            }

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

        public ActionResult Deslogueo()
        {
            Session["UserName"] = null;
            Session.Abandon();

            return RedirectToAction("index","home");
        }
    }
}

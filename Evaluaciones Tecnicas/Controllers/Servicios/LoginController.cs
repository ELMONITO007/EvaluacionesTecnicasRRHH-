
using Entities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using CaptchaMvc.HtmlHelpers;
using GreenElectric.Negocio;

namespace WebApplication7.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
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
            if (this.IsCaptchaValid("Ingrese las letras correctamente"))
            {
                if (ModelState.IsValid)
                {

                    LoginComponent loginComponent = new LoginComponent();
                    LoginError loginError = new LoginError();
                    loginError = loginComponent.VerificarLogin(usuarios);
                    if (loginError.error == "")
                    {
                        Session["UserName"] = usuarios.Email;
                       


                        return View();
                    }


                    else
                    {
                        return View("index");
                    }

                }
                else
                {
                    return View("index");
                }
            }
            else
            {
               
                return View("index");
            }


        }

        // GET: Login/Details/5
        public ActionResult Details(int id)
        {
            return View();
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

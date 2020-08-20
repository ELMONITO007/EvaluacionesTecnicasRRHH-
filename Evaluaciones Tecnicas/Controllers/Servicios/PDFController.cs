using Entities;
using Negocio;
using Negocio.Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Evaluaciones_Tecnicas.Controllers.Servicios
{
    public class PDFController : Controller
    {
        // GET: PDF
        public ActionResult Index()
        {
            CrearPDF pDF = new CrearPDF();
            return View(pDF.Read());
        }

        // GET: PDF/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PDF/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PDF/Create
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

        // GET: PDF/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PDF/Edit/5
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

        // GET: PDF/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PDF/Delete/5
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

       
        public ActionResult Abrir(string email)
        {
            Usuarios usuarios = new Usuarios();
                return View(usuarios);
           
        }
        [HttpPost]
        public ActionResult Abrir(FormCollection formCollection)
        {
            try
            {
                // TODO: Add delete logic here
                CrearPDF pDF = new CrearPDF();
                string email = formCollection.Get("Email");
                pDF.AbrirPDF(email);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

    }
}

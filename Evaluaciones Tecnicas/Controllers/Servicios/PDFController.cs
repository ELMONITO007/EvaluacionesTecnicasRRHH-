using Entities;
using Evaluaciones_Tecnicas.Filter;
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
        [AuthorizerUser(_roles: "Administrador,RRHH")]
        public ActionResult Index()
        {
            CrearPDF pDF = new CrearPDF();
            return View(pDF.Read());
        }

        // GET: PDF/Details/5
        [AuthorizerUser(_roles: "Administrador,RRHH")]
        public ActionResult Abrir(string email)
        {
            try
            {
                // TODO: Add delete logic here
                CrearPDF pDF = new CrearPDF();
                
                pDF.AbrirPDF(email);
                return RedirectToAction("Index","UsuarioExamen");
            }
            catch
            {
                return View();
            }

        }
       

    }
}

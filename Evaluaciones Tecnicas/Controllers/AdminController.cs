using Evaluaciones_Tecnicas.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Evaluaciones.Controllers
{
  

 
    public class AdminController : Controller
    {
        // GET: Admin
        [AuthorizerUser(_roles: "Administrador,CrearPregunta,RRHH")]
        public ActionResult Index()
        {
            return View();
        }

        
    }
}

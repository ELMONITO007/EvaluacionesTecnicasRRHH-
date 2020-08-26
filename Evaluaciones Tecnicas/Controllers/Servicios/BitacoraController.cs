using Evaluaciones_Tecnicas.Filter;
using Negocio.Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Evaluaciones_Tecnicas.Controllers.Servicios
{
    public class BitacoraController : Controller
    {
        // GET: Bitacora
        [AuthorizerUser(_roles: "Administrador,RRHH")]
        public ActionResult Index()
        {
            BitacoraComponent bitacoraComponent = new BitacoraComponent();

            return View(bitacoraComponent.Read());
        }

       
    }
}

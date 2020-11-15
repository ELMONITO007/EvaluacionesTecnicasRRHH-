using Entities;
using Entities.Examen;
using Evaluaciones_Tecnicas.Filter;
using Negocio;
using Negocio.Examen;
using Negocio.Informes;
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
        public ActionResult Administrador()
        {
            InformeComponent informeComponent = new InformeComponent();
            Informe informe = new Informe();
            informe = informeComponent.ObtenerInforme();
            return View(informe);
        }

        [AuthorizerUser(_roles: "RRHH")]
        public ActionResult RRHH()
        {
            return View();
        }

        [AuthorizerUser(_roles: "CrearPregunta")]
        public ActionResult CrearPregunta()
        {
            return View();
        }


        [AuthorizerUser(_roles: "Examen")]
        public ActionResult Examen()
        {
            Usuarios usuarios = new Usuarios();

            usuarios = (Usuarios)Session["UserName"];
            string Email = usuarios.Email;
            UsuariosComponent usuariosComponent = new UsuariosComponent();
            usuarios = usuariosComponent.ReadByEmail(Email);
            ExamenComponent examen = new ExamenComponent();

            return View(examen.VerExamenUsuario(usuarios.Id));

        }



    }
}

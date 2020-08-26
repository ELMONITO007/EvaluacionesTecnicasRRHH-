using Entities;
using Evaluaciones_Tecnicas.Filter;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace Evaluaciones.Controllers
{
    [ExceptionFilter]
    public class HomeController : Controller
    {
        [ExceptionFilter]
        public ActionResult TestExamen()
        {
            PreguntaComponent preguntaComponent = new PreguntaComponent();
            Pregunta pregunta = new Pregunta();
            Pregunta enviar = new Pregunta();
            enviar.categoria.Id = 1;
            pregunta.ListaPregunta = preguntaComponent.ObtenerPreguntasAlAzar(enviar, 10);

            return View(pregunta);
        }
        [ExceptionFilter]
        [HttpPost]
        public ActionResult TestExamen(FormCollection formCollection)
        {
            return View();
        }
        [ExceptionFilter]
        public ActionResult Index()
        {
            return View();
        }

    }
}
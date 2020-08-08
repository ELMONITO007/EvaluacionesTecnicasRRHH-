using Entities;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace Evaluaciones.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult TestExamen()
        {
            PreguntaComponent preguntaComponent = new PreguntaComponent();
            Pregunta pregunta = new Pregunta();
            Pregunta enviar = new Pregunta();
            enviar.categoria.Id = 1;
            pregunta.ListaPregunta = preguntaComponent.ObtenerPreguntasAlAzar(enviar, 10);

            return View(pregunta);
        }
        [HttpPost]
        public ActionResult TestExamen(FormCollection formCollection)
        {
            return View();
        }
            public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
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
    //[Authorize(Roles = "Administrador,CreadorPreguntas")]//para entrar en admin debe estar logueado y  asignarle el rol
    public class SaludPreguntaController : Controller
    {
        // GET: SaludPregunta
        [AuthorizerUser(_roles: "Administrador,CrearPregunta,RRHH")]
        public ActionResult Index(int id)
        {
            Pregunta pregunta = new Pregunta();
            PreguntaComponent preguntaComponent = new PreguntaComponent();
            pregunta = preguntaComponent.ReadBy(id);
            if (pregunta.tipoPregunta.TipoDePregunta=="MultipleChoice")
            {
                RedirectToAction("SaludMultipleChoice","SaludPregunta", new { id=pregunta.Id});
            }
            else if (pregunta.tipoPregunta.TipoDePregunta == "Ordenado")
            {
                RedirectToAction("SaludOrdenado", "SaludPregunta", new { id = pregunta.Id });
            }
            else
            {
                RedirectToAction("SaludMultipleChoiceCompuesto","SaludPregunta", new { id = pregunta.Id });
            }
            return View();
        }
        [AuthorizerUser(_roles: "Administrador,CrearPregunta,RRHH")]
        public ActionResult SaludMultipleChoice(int id)
        {
            SaludPreguntaComponent saludPregunta = new SaludPreguntaComponent();
            Pregunta pregunta = new Pregunta();
            PreguntaComponent preguntaComponent = new PreguntaComponent();
            pregunta = preguntaComponent.ReadBy(id);


            return View(saludPregunta.VerificarSalud(pregunta));
        }
        [AuthorizerUser(_roles: "Administrador,CrearPregunta,RRHH")]
        public ActionResult SaludMultipleChoiceCompuesto(int id)
        {
            SaludPreguntaComponent saludPregunta = new SaludPreguntaComponent();
            Pregunta pregunta = new Pregunta();
            PreguntaComponent preguntaComponent = new PreguntaComponent();
            pregunta = preguntaComponent.ReadBy(id);


            return View(saludPregunta.VerificarSalud(pregunta));
        }
        // GET: SaludPregunta/Details/5
        [AuthorizerUser(_roles: "Administrador,CrearPregunta,RRHH")]
        public ActionResult SaludOrdenado(int id)
        {
            SaludPreguntaComponent saludPregunta = new SaludPreguntaComponent();
            Pregunta pregunta = new Pregunta();
            PreguntaComponent preguntaComponent = new PreguntaComponent();
            pregunta = preguntaComponent.ReadBy(id);

            return View(saludPregunta.VerificarSalud(pregunta));
        }
       
    }
}

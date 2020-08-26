using System;
using Evaluaciones.Models;
using Entities;
using Negocio;

using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Negocio;
using Evaluaciones_Tecnicas.Filter;

namespace Evaluaciones.Controllers
{
    //[Authorize(Roles = "Administrador,CreadorPreguntas")]//para entrar en admin debe estar logueado y  asignarle el rol

    public class MultipleChoiceCompuestoController : Controller
    {
        // GET: MultipleChoiceCompuesto
        [AuthorizerUser(_roles: "Administrador,CrearPregunta,RRHH")]
        public ActionResult Index(int id)
        {
            MultipleChoiceCompustoComponent multipleChoiceCompustoComponent = new MultipleChoiceCompustoComponent();
            
            return View(multipleChoiceCompustoComponent.ReadByPregunta(id));
        }

        // GET: MultipleChoiceCompuesto/Details/5
        [AuthorizerUser(_roles: "Administrador,CrearPregunta,RRHH")]
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: MultipleChoiceCompuesto/Create
        [AuthorizerUser(_roles: "Administrador,CrearPregunta,RRHH")]
        public ActionResult Create(int id,int id_Pregunta)
        {
            PreguntaComponent preguntaComponent = new PreguntaComponent();

            MultipleChoiceCompuesto multipleChoiceCompuesto = new MultipleChoiceCompuesto();
            multipleChoiceCompuesto.pregunta = preguntaComponent.ReadBySubPregunta(id);
            multipleChoiceCompuesto.Id = id_Pregunta;
            return View(multipleChoiceCompuesto);
        }

        // POST: MultipleChoiceCompuesto/Create
        [AuthorizerUser(_roles: "Administrador,CrearPregunta,RRHH")]
        [HttpPost]
        public ActionResult Create(FormCollection collection,string id_Pregunta)
        {
            try
            {
                // TODO: Add insert logic here
                MultipleChoiceCompustoComponent multipleChoiceCompustoComponent = new MultipleChoiceCompustoComponent();
                MultipleChoiceCompuesto multipleChoiceCompuesto = new MultipleChoiceCompuesto();
                multipleChoiceCompuesto.LaRespuesta = collection.Get("LaRespuesta");
             
                if (collection.Get("Correcta").Count() ==10)
                {
                    multipleChoiceCompuesto.Correcta = true;
                }
                else
                {
                    multipleChoiceCompuesto.Correcta = false;
                }

                multipleChoiceCompuesto.pregunta.Id = int.Parse(collection.Get("pregunta.id"));
                multipleChoiceCompustoComponent.Create(multipleChoiceCompuesto);
                return RedirectToAction("Index", "multipleChoiceCompuesto",new { id=int.Parse(id_Pregunta) });
            }
            catch (Exception e)
            {
                return View();
            }
        }

        // GET: MultipleChoiceCompuesto/Edit/5
        [AuthorizerUser(_roles: "Administrador,CrearPregunta,RRHH")]
        public ActionResult Edit(int id,int id_Pregunta)
        {
            MultipleChoiceCompuesto multipleChoiceCompuesto = new MultipleChoiceCompuesto();
            MultipleChoiceCompustoComponent multipleChoiceCompustoComponent = new MultipleChoiceCompustoComponent();
            multipleChoiceCompuesto = multipleChoiceCompustoComponent.ReadBy(id);
            multipleChoiceCompuesto.id_SubPregunta = id_Pregunta;
     

            return View(multipleChoiceCompuesto);
        }

        // POST: MultipleChoiceCompuesto/Edit/5
        [AuthorizerUser(_roles: "Administrador,CrearPregunta,RRHH")]
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection,int id_Pregunta)
        {
            try
            {
                MultipleChoiceCompuesto multipleChoiceCompuesto = new MultipleChoiceCompuesto();
                MultipleChoiceCompustoComponent multipleChoiceCompustoComponent = new MultipleChoiceCompustoComponent();
                multipleChoiceCompuesto.Id = id;
                multipleChoiceCompuesto.LaRespuesta = collection.Get("LaRespuesta");
                if (collection.Get("Correcta").Count() == 10)
                {
                    multipleChoiceCompuesto.Correcta = true;
                }
                else
                {
                    multipleChoiceCompuesto.Correcta = false;
                }
                // TODO: Add update logic here
                multipleChoiceCompustoComponent.Update(multipleChoiceCompuesto);
                return RedirectToAction("Index",new { id=id_Pregunta});
            }
            catch
            {
                return View();
            }
        }

        // GET: MultipleChoiceCompuesto/Delete/5
        [AuthorizerUser(_roles: "Administrador,CrearPregunta,RRHH")]
        public ActionResult Delete(int id,int id_Pregunta)
        {
            MultipleChoiceCompuesto multipleChoiceCompuesto = new MultipleChoiceCompuesto();
            MultipleChoiceCompustoComponent multipleChoiceCompustoComponent = new MultipleChoiceCompustoComponent();
            multipleChoiceCompuesto = multipleChoiceCompustoComponent.ReadBy(id);
            multipleChoiceCompuesto.id_SubPregunta = id_Pregunta;


            return View(multipleChoiceCompuesto);

        }

        // POST: MultipleChoiceCompuesto/Delete/5
        [AuthorizerUser(_roles: "Administrador,CrearPregunta,RRHH")]
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection, int id_Pregunta)
        {
            try
            {
                // TODO: Add delete logic here
                MultipleChoiceCompustoComponent multipleChoiceCompustoComponent = new MultipleChoiceCompustoComponent();
                multipleChoiceCompustoComponent.Delete(id);
                return RedirectToAction("Index",new { id=id_Pregunta});
            }
            catch
            {
                return View();
            }
        }
    }
}

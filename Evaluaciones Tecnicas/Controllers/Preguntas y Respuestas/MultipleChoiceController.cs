using Entities;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Evaluaciones.Controllers
{
    //[Authorize(Roles = "Administrador,CreadorPreguntas")]//para entrar en admin debe estar logueado y  asignarle el rol

    public class MultipleChoiceController : Controller
    {
        // GET: MultipleChoice
        public ActionResult Index(int Id)
        {

            MultipleChoiceComponent multipleChoiceComponent = new MultipleChoiceComponent();
           
                return View(multipleChoiceComponent.listaRespuestaMultipleChoiceAlAzar(Id));


        }

        // GET: MultipleChoice/Details/5
        public ActionResult Details(int id)
        {
            MultipleChoiceComponent multipleChoiceComponent = new MultipleChoiceComponent();

            return View(multipleChoiceComponent.ReadBy(id));
        }

        // GET: MultipleChoice/Create
        public ActionResult Create(int id)
        {
            MultipleChoice multipleChoice = new MultipleChoice();
            PreguntaComponent preguntaComponent = new PreguntaComponent();
            Pregunta pregunta = new Pregunta();
            pregunta = preguntaComponent.ReadBy(id);

            multipleChoice.pregunta = pregunta;
           
            return View(multipleChoice);
        }

        // POST: MultipleChoice/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here
                MultipleChoice multipleChoice = new MultipleChoice();
                if (collection.Get("correcta").Count()==10)
                {
                    multipleChoice.Correcta = true;
                }
                else
                {
                    multipleChoice.Correcta = false;
                }
          
                multipleChoice.LaRespuesta = collection.Get("LaRespuesta");
                multipleChoice.pregunta.Id = int.Parse(collection.Get("pregunta.Id"));
                MultipleChoiceComponent multipleChoiceComponent = new MultipleChoiceComponent();
                multipleChoiceComponent.Create(multipleChoice);

                return RedirectToAction("Index",new { id = multipleChoice.pregunta.Id });
            }
            catch (Exception e)
            {
                return View();
            }
        }

        // GET: MultipleChoice/Edit/5
        public ActionResult Edit(int id)
        {
            MultipleChoiceComponent multipleChoiceComponent = new MultipleChoiceComponent();

            return View(multipleChoiceComponent.ReadBy(id));
        }

        // POST: MultipleChoice/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                MultipleChoice multipleChoice = new MultipleChoice();
                multipleChoice.Id = id;

                if (collection.Get("Correcta").Count() == 10)
                {
                    multipleChoice.Correcta = true;
                }
                else
                {
                    multipleChoice.Correcta = false;
                }
                multipleChoice.LaRespuesta = collection.Get("LaRespuesta");
                multipleChoice.pregunta.Id = int.Parse(collection.Get("pregunta.Id")); 
                MultipleChoiceComponent multipleChoiceComponent = new MultipleChoiceComponent();
                multipleChoiceComponent.Update(multipleChoice);

                return RedirectToAction("Index", new { id = multipleChoice.pregunta.Id });
            }
            catch(Exception e)
            {
                return View();
            }
        }

        // GET: MultipleChoice/Delete/5
        public ActionResult Delete(int id)
        {
            MultipleChoiceComponent multipleChoiceComponent = new MultipleChoiceComponent();

            return View(multipleChoiceComponent.ReadBy(id));
        }

        // POST: MultipleChoice/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                MultipleChoiceComponent multipleChoiceComponent = new MultipleChoiceComponent();
                multipleChoiceComponent.Delete(id);
                return RedirectToAction("Index", new { id = int.Parse(collection.Get("pregunta.Id")) });
            }
            catch (Exception e)
            {
                return View();
            }
        }
    }
}

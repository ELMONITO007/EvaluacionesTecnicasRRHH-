﻿using Negocio;
using Entities;

using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Evaluaciones_Tecnicas.Filter;

namespace Evaluaciones.Controllers
{
    //[Authorize(Roles = "Administrador")]//para entrar en admin debe estar logueado y  asignarle el rol

    public class NivelController : Controller
    {
        // GET: Nivel
        [AuthorizerUser(_roles: "Administrador")]
        public ActionResult Index()
        {
            NivelComponent nivelComponent = new NivelComponent();
            return View(nivelComponent.Read());
        }

        // GET: Nivel/Details/5
        [AuthorizerUser(_roles: "Administrador")]
        public ActionResult Details(int id)
        {
            NivelComponent nivelComponent = new NivelComponent();
            return View(nivelComponent.ReadBy(id));
        }

        // GET: Nivel/Create
        [AuthorizerUser(_roles: "Administrador")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Nivel/Create
        [AuthorizerUser(_roles: "Administrador")]
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here
                NivelComponent nivelComponent = new NivelComponent();
                Nivel nivel = new Nivel();
                nivel.ElNivel= collection.Get("ElNivel");
                nivelComponent.Create(nivel);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Nivel/Edit/5
        [AuthorizerUser(_roles: "Administrador")]
        public ActionResult Edit(int id)
        {
            NivelComponent nivelComponent = new NivelComponent();
            return View(nivelComponent.ReadBy(id));
        }

        // POST: Nivel/Edit/5
        [AuthorizerUser(_roles: "Administrador")]
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
                NivelComponent nivelComponent = new NivelComponent();
                Nivel nivel = new Nivel();
                nivel.ElNivel = collection.Get("ElNivel");
                nivel.Id = id;
                nivelComponent.Update(nivel);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Nivel/Delete/5
        [AuthorizerUser(_roles: "Administrador")]
        public ActionResult Delete(int id)
        {
            NivelComponent nivelComponent = new NivelComponent();
            return View(nivelComponent.ReadBy(id));
        }


        // POST: Nivel/Delete/5
        [AuthorizerUser(_roles: "Administrador")]
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                NivelComponent nivelComponent = new NivelComponent();
              
               
                nivelComponent.Delete(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}

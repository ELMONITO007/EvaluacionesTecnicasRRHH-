using Entities;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Evaluaciones.Controllers.Usuario_Examen
{
    //[Authorize(Roles = "Administrador")]//para entrar en admin debe estar logueado y  asignarle el rol
    public class SectorController : Controller
    {
        // GET: Sector
        public ActionResult Index()
        {
            SectorComponent sectorComponent = new SectorComponent();
            return View(sectorComponent.Read());
        }

        // GET: Sector/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Sector/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Sector/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here
                SectorComponent sectorComponent = new SectorComponent();
                Sector sector = new Sector();
                sector.sector = collection.Get("sector");
                if (sectorComponent.Verificar(sector))
                {
                    sectorComponent.Create(sector);
                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("ErrorPage", new { id = sector.sector });
                }

            }
            catch
            {
                return View();
            }
        }

        // GET: Sector/Edit/5
        public ActionResult Edit(int id)
        {
            SectorComponent sectorComponent = new SectorComponent();
            return View(sectorComponent.ReadBy(id));
        }

        // POST: Sector/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                // TODO: Add insert logic here
                SectorComponent sectorComponent = new SectorComponent();
                Sector sector = new Sector();
                sector.sector = collection.Get("sector");
                sector.Id = id;
                if (sectorComponent.Verificar(sector))
                {
                    sectorComponent.Update(sector);
                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("ErrorPage", new { id = sector.sector });
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: Sector/Delete/5
        public ActionResult Delete(int id)
        {
            SectorComponent sectorComponent = new SectorComponent();
            return View(sectorComponent.ReadBy(id));
        }

        // POST: Sector/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                SectorComponent sectorComponent = new SectorComponent();
                sectorComponent.Delete(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        public ActionResult ErrorPage(string id)
        {
            Sector sector = new Sector();
            sector.sector = id;
            return View(sector);

        }

    }
}

using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Entities;
using Data;

namespace Evaluaciones_Tecnicas.Controllers.Usuario
{
    public class UsuarioExamenController : Controller
    {
        // GET: UsuarioExamen
        public ActionResult Index()
        {
            UsuarioParaExamenComponent usuarioParaExamenComponent = new UsuarioParaExamenComponent();
            return View(usuarioParaExamenComponent.Read());
        }

        // GET: UsuarioExamen/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: UsuarioExamen/Create
        public ActionResult Create()
        {
            UsuarioParaExamen usuarioParaExamen = new UsuarioParaExamen();
            #region Sede
            SedeComponent sedeComponent = new SedeComponent();
            usuarioParaExamen.ListaSedes = sedeComponent.Read();
            usuarioParaExamen.ListaSedes.Select(y =>
                                new
                                {
                                    y.Id,
                                    y.sede
                                });

            ViewBag.ListaSedes = new SelectList(usuarioParaExamen.ListaSedes, "Id", "sede");

            #endregion

            #region Direccion

            DireccionComponent direccionComponent = new DireccionComponent();
            usuarioParaExamen.ListaDireccion = direccionComponent.Read();
            usuarioParaExamen.ListaDireccion.Select(y =>
                                new
                                {
                                    Id = y.Id,
                                    direccion = y.direccion
                                });

            ViewBag.ListaDireccion = new SelectList(usuarioParaExamen.ListaDireccion, "Id", "direccion");

            #endregion

            #region Gerencia
            GerenciaComponent gerenciaComponent = new GerenciaComponent();
            usuarioParaExamen.ListaGerencia = gerenciaComponent.Read();

            usuarioParaExamen.ListaGerencia.Select(y =>
                                new
                                {
                                    Id = y.Id,
                                    gerencia = y.gerencia
                                });

            ViewBag.ListaGerencia = new SelectList(usuarioParaExamen.ListaGerencia, "Id", "gerencia");
            #endregion

            #region Jefatura
            JefaturaComponent jefaturaComponent = new JefaturaComponent();
            usuarioParaExamen.ListaJefatura = jefaturaComponent.Read();
            usuarioParaExamen.ListaJefatura.Select(y =>
                                new
                                {
                                    Id = y.Id,
                                    gerencia = y.jefatura
                                });

            ViewBag.ListaJefatura = new SelectList(usuarioParaExamen.ListaJefatura, "Id", "jefatura");
            #endregion
            #region Sector
            SectorComponent sectorComponent = new SectorComponent();
            usuarioParaExamen.ListaSector = sectorComponent.Read();

            usuarioParaExamen.ListaSector.Select(y =>
                                new
                                {
                                    Id = y.Id,
                                    sector = y.sector
                                });

            ViewBag.ListaSector = new SelectList(usuarioParaExamen.ListaSector, "Id", "sector");

            #endregion

            return View(usuarioParaExamen);
        }

        // POST: UsuarioExamen/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                UsuarioParaExamen usuarioParaExamen = new UsuarioParaExamen();
                UsuarioParaExamenComponent usuarioParaexamenDAC = new UsuarioParaExamenComponent();
                usuarioParaExamen.usuarios.Nombre = collection.Get("usuarios.Nombre");
                usuarioParaExamen.usuarios.Email = collection.Get("usuarios.Email");
                usuarioParaExamen.usuarios.Apellido = collection.Get("usuarios.Apellido");
                usuarioParaExamen.sede.Id =int.Parse( collection.Get("sede.sede"));

                usuarioParaExamen.direccion.Id = int.Parse(collection.Get("direccion.direccion"));
                usuarioParaExamen.gerencia.Id = int.Parse(collection.Get("gerencia.gerencia"));
                usuarioParaExamen.jefatura.Id = int.Parse(collection.Get("jefatura.jefatura"));
                usuarioParaExamen.sector.Id = int.Parse(collection.Get("sector.sector"));
                // TODO: Add insert logic here
                usuarioParaexamenDAC.Create(usuarioParaExamen);
                return RedirectToAction("Index");
            }
            catch(Exception e)
            {
                return View();
            }
        }

        // GET: UsuarioExamen/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: UsuarioExamen/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: UsuarioExamen/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: UsuarioExamen/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}

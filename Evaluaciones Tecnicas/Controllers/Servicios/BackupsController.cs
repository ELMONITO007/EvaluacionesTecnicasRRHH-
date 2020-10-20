using Entities;
using Evaluaciones_Tecnicas.Filter;
using Negocio;
using Negocio.Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Evaluaciones_Tecnicas.Controllers.Servicios
{

    public class BackupsController : Controller
    {
        [AuthorizerUser(_roles: "Administrador")]
        // GET: Backup
        public ActionResult Index()
        {
            BackupComponent backupComponent = new BackupComponent();
            return View(backupComponent.Read());
        }
        [AuthorizerUser(_roles: "Administrador")]
        // GET: Backup/Details/5
        public ActionResult ConsistenciaBD()
        {
            LoginComponent loginComponent = new LoginComponent();
            DVV dVV = new DVV();
            dVV.estado = loginComponent.VerificarDVV();


            return View(dVV);
        }
        [HttpPost]
        public ActionResult ConsistenciaBD(FormCollection collection)
        {
            try
            {
                bool status = bool.Parse(collection.Get("estado"));
                if (!status)
                {
                    BackupComponent backupComponent = new BackupComponent();
                    backupComponent.RestaurarBase();
                }

                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                return View();
            }
        }
        [AuthorizerUser(_roles: "Administrador")]
        // GET: Backup/Create
        public ActionResult Create()
        {
            BackupComponent backupComponent = new BackupComponent();
            Usuarios usuarios = new Usuarios();
            Usuarios user = new Usuarios();
            user = (Usuarios)Session["UserName"];
            UsuariosComponent usuariosComponent = new UsuariosComponent();
            user = usuariosComponent.ReadByEmail(user.Email);
            backupComponent.Create(user.Id);
            return RedirectToAction("Index");
        }


        [AuthorizerUser(_roles: "Administrador")]
        // GET: Backup/Edit/5
        public ActionResult Restore(int id)
        {
            try
            {
                // TODO: Add update logic here
                Backups backups = new Backups();

                backups.Id = id;
                BackupComponent backupComponent = new BackupComponent();

                Usuarios user = new Usuarios();
                user = (Usuarios)Session["UserName"];
                UsuariosComponent usuariosComponent = new UsuariosComponent();
                user = usuariosComponent.ReadByEmail(user.Email);
                backups.usuarios = user;
                backupComponent.RestoreDatabase(backups);
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                return View();
            }
        }

        [AuthorizerUser(_roles: "Administrador")]
        // GET: Backup/Delete/5
        public ActionResult Delete(int id)
        {

            BackupComponent backupComponent = new BackupComponent();
            return View(backupComponent.ReadBy(id));
        }
        [AuthorizerUser(_roles: "Administrador")]
        // POST: Backup/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                BackupComponent backupComponent = new BackupComponent();
                backupComponent.Delete(id);
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                return View();
            }
        }
    }
}

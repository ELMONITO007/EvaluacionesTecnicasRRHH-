using Data;
using Entities;
using Entities.Servicios.Bitacora;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Hosting;

namespace Negocio.Servicios
{
    public class BackupComponent : IRepository<Backups>
    {
        public static void BackupDatabase(Backups backup)
        {

            Entities.Backups backupRestore = new Entities.Backups();
            BackupDAC backupDAC = new BackupDAC();
            backupRestore = backup;
            backupRestore.Path = @"C:\Auxiliar\" + @backup.Nombre + ".bak";
            BitacoraComponent bitacoraComponent = new BitacoraComponent();
            Bitacora bitacora = new Bitacora();
            bitacora.eventoBitacora.Id = 7;
            bitacora.fecha = DateTime.Now.ToString("dd-MM-yyyy");
            bitacora.hora = DateTime.Now.ToString("hh mm ss");
            UsuariosComponent usuarios = new UsuariosComponent();
            bitacora.usuarios = usuarios.ReadBy(backup.usuarios.Id);

            backupDAC.CreateBackup(backupRestore);
            bitacoraComponent.Create(bitacora);
        }

        public void RestaurarBase()
        {
            Entities.Backups backupRestore = new Entities.Backups();
            BackupDAC backupDAC = new BackupDAC();
            backupRestore = backupDAC.ReadBy();
            backupRestore.Path = @"C:\Auxiliar\" + backupRestore.Nombre + ".bak";


            UsuariosComponent usuariosComponent = new UsuariosComponent();

            RestoreDatabase(backupRestore);


        }


        public void RestoreDatabase(Backups backup)
        {
            Entities.Backups backupRestore = new Entities.Backups();
            BackupDAC backupDAC = new BackupDAC();
            backupRestore = backupDAC.ReadBy(backup.Id);
            backupRestore.Path = HostingEnvironment.MapPath("~/Backup/" + backupRestore.Nombre + ".bak");
            backupDAC.Restore(backupRestore);
            Bitacora bitacora = new Bitacora();
            bitacora.eventoBitacora.Id = 6;

            UsuariosComponent usuarios = new UsuariosComponent();
            bitacora.usuarios.Email = "andres.benitez@transener.com.ar";
            BitacoraComponent bitacoraComponent = new BitacoraComponent();
            bitacoraComponent.Create(bitacora);
        }

        public Backups Create(int legajo)
        {
            Entities.Backups entity = new Backups();
            entity.Fecha = DateTime.Now.ToString("dd-MM-yyyy hh mm ss");
            entity.Nombre = "Backup-" + entity.Fecha;
            entity.usuarios.Id = legajo;

            BackupDAC backupDAC = new BackupDAC();
            Entities.Backups backups = new Backups();
            backups = backupDAC.Create(entity);
            BackupDatabase(entity);

            return backups;
        }

        public void Delete(int id)
        {
            BackupDAC backupDAC = new BackupDAC();
            Entities.Backups backups = new Backups();
            backups = backupDAC.ReadBy(id);
            backups.Path = HostingEnvironment.MapPath("~/Backup/" + backups.Nombre + ".bak");
            File.Delete(backups.Path);
            backupDAC.Delete(id);
        }

        public List<Backups> Read()
        {
            BackupDAC backupDAC = new BackupDAC();
            return backupDAC.Read();
        }

        public Backups ReadBy(int id)
        {
            BackupDAC backupDAC = new BackupDAC();
            return backupDAC.ReadBy(id);
        }

        public void Update(Backups entity)
        {
            throw new NotImplementedException();
        }

        public Backups Create(Backups entity)
        {
            throw new NotImplementedException();
        }
    }
}

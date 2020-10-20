using Entities;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class BackupDAC : DataAccessComponent, IRepository<Backups>
    {

        private Backups Load(IDataReader dr)
        {
            Backups backups = new Backups();
            backups.Id = GetDataValue<int>(dr, "ID_Backup");
            backups.Fecha = GetDataValue<string>(dr, "fecha");
            backups.Nombre = GetDataValue<string>(dr, "Nombre");


            return backups;
        }

        public Backups Create(Backups entity)
        {
            const string SQL_STATEMENT = "insert into [dbo].[Backup](Nombre,Fecha,activo)values(@Nombre,@Fecha,1) ";
            var db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);
            using (DbCommand cmd = db.GetSqlStringCommand(SQL_STATEMENT))
            {

                db.AddInParameter(cmd, "@Nombre", DbType.String, entity.Nombre);

                db.AddInParameter(cmd, "@Fecha", DbType.String, entity.Fecha);

                entity.Id = Convert.ToInt32(db.ExecuteScalar(cmd));
            }
            return entity;
        }

        public void Delete(int id)
        {
            const string SQL_STATEMENT = "update [dbo].[Backup] set Activo=0 where id_Backup=@Id";
            var db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);
            using (DbCommand cmd = db.GetSqlStringCommand(SQL_STATEMENT))
            {
                db.AddInParameter(cmd, "@Id", DbType.Int32, id);
                db.ExecuteNonQuery(cmd);
            }
        }

        public List<Backups> Read()
        {
            const string SQL_STATEMENT = "SELECT * FROM[evalucionesTecnicas].[dbo].[Backup] where Activo = 1";

            List<Backups> result = new List<Backups>();
            var db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);
            using (DbCommand cmd = db.GetSqlStringCommand(SQL_STATEMENT))
            {
                using (IDataReader dr = db.ExecuteReader(cmd))
                {
                    while (dr.Read())
                    {
                        Backups backups = Load(dr);
                        result.Add(backups);
                    }

                }
            }
            return result;
        }

        public Backups ReadBy(int id)
        {
            const string SQL_STATEMENT = "select * from  [dbo].[Backup] where activo=1 and id_Backup=@Id";
            Backups backups = null;

            var db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);
            using (DbCommand cmd = db.GetSqlStringCommand(SQL_STATEMENT))
            {
                db.AddInParameter(cmd, "@Id", DbType.Int32, id);
                using (IDataReader dr = db.ExecuteReader(cmd))
                {
                    if (dr.Read())
                    {
                        backups = Load(dr);
                    }
                }
            }
            return backups;
        }
        public Backups ReadBy()
        {
            const string SQL_STATEMENT = "select top 1 * from [dbo].[Backup] order by ID_Backup desc";
            Backups backups = null;

            var db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);
            using (DbCommand cmd = db.GetSqlStringCommand(SQL_STATEMENT))
            {

                using (IDataReader dr = db.ExecuteReader(cmd))
                {
                    if (dr.Read())
                    {
                        backups = Load(dr);
                    }
                }
            }
            return backups;
        }
        public Backups ReadBy(string id)
        {
            const string SQL_STATEMENT = "select * from Backup where activo=1 and id=@Id";
            Backups backups = null;

            var db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);
            using (DbCommand cmd = db.GetSqlStringCommand(SQL_STATEMENT))
            {
                db.AddInParameter(cmd, "@Id", DbType.Int32, id);
                using (IDataReader dr = db.ExecuteReader(cmd))
                {
                    if (dr.Read())
                    {
                        backups = Load(dr);
                    }
                }
            }
            return backups;
        }
        public void Update(Backups entity)
        {
            throw new NotImplementedException();
        }
        public void Restore(Backups entity)
        {
            const string SQL_STATEMENT = "alter database evalucionesTecnicas    set offline with rollback immediate   restore database  evalucionesTecnicas from disk=@path     alter database  evalucionesTecnicas  set online";




            var db = DatabaseFactory.CreateDatabase(CONNECTION_Restore);
            using (DbCommand cmd = db.GetSqlStringCommand(SQL_STATEMENT))
            {

                db.AddInParameter(cmd, "@path", DbType.String, entity.Path);



                db.ExecuteNonQuery(cmd);
            }

        }


        public void CreateBackup(Backups entity)
        {
            const string SQL_STATEMENT = "backup database evalucionesTecnicas to disk= @path WiTH Format,name=@nombre";

            var db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);
            using (DbCommand cmd = db.GetSqlStringCommand(SQL_STATEMENT))
            {

                db.AddInParameter(cmd, "@path", DbType.String, entity.Path);

                db.AddInParameter(cmd, "@nombre", DbType.String, entity.Nombre);

                db.ExecuteNonQuery(cmd);
            }

        }

    }
}

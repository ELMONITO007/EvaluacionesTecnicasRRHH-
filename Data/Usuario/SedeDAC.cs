using Entities;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class SedeDAC : DataAccessComponent, IRepository<Sede>
    {
        private Sede LoadSede(IDataReader dr)
        {
            Sede sede = new Sede();
            sede.Id = GetDataValue<int>(dr, "ID_sede");
            sede.sede = GetDataValue<string>(dr, "sede");
            sede.codigo = GetDataValue<string>(dr, "Codigo");
            sede.empresa.Id= GetDataValue<int>(dr, "Empresa");
            return sede;
        }
        public Sede Create(Sede entity)
        {
            const string SQL_STATEMENT = "insert into sede (sede ,Empresa,Codigo,Activo) values(@Sede,@ID_Empresa,@Codigo,1) ";
            var db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);
            using (DbCommand cmd = db.GetSqlStringCommand(SQL_STATEMENT))
            {
                db.AddInParameter(cmd, "@Sede", DbType.String, entity.sede);
                db.AddInParameter(cmd, "@Codigo", DbType.String, entity.codigo);
                db.AddInParameter(cmd, "@ID_Empresa", DbType.Int32, entity.empresa.Id);
                entity.Id = Convert.ToInt32(db.ExecuteScalar(cmd));
            }


            return entity;
        }

        public void Delete(int id)
        {
            const string SQL_STATEMENT = "update sede set Activo=0 where ID_sede=@Id";
            var db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);
            using (DbCommand cmd = db.GetSqlStringCommand(SQL_STATEMENT))
            {
                db.AddInParameter(cmd, "@Id", DbType.Int32, id);
                db.ExecuteNonQuery(cmd);
            }
        }

        public List<Sede> Read()
        {
            const string SQL_STATEMENT = "select * from sede where activo=1";

            List<Sede> result = new List<Sede>();
            var db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);
            using (DbCommand cmd = db.GetSqlStringCommand(SQL_STATEMENT))
            {
                using (IDataReader dr = db.ExecuteReader(cmd))
                {
                    while (dr.Read())
                    {
                        Sede sede = LoadSede(dr);
                        result.Add(sede);
                    }
                }
            }
            return result;
        }

        public Sede ReadBy(int id)
        {
            const string SQL_STATEMENT = "select * from sede where activo=1 and ID_sede=@Id";
            Sede sede = null;

            var db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);
            using (DbCommand cmd = db.GetSqlStringCommand(SQL_STATEMENT))
            {
                db.AddInParameter(cmd, "@Id", DbType.Int32, id);
                using (IDataReader dr = db.ExecuteReader(cmd))
                {
                    if (dr.Read())
                    {
                        sede = LoadSede(dr);
                    }
                }
            }
            return sede;
        }
        public Sede ReadBy(Sede sede)
        {
            const string SQL_STATEMENT = "select * from sede where activo=1 and sede=@Id and Empresa=@ID_Empresa";
            Sede result = null;

            var db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);
            using (DbCommand cmd = db.GetSqlStringCommand(SQL_STATEMENT))
            {
                db.AddInParameter(cmd, "@Id", DbType.String, sede.sede);
                db.AddInParameter(cmd, "@ID_Empresa", DbType.Int32, sede.empresa.Id);
                using (IDataReader dr = db.ExecuteReader(cmd))
                {
                    if (dr.Read())
                    {
                        result = LoadSede(dr);
                    }
                }
            }
            return result;
        }

        public void Update(Sede entity)
        {
            const string SQL_STATEMENT = "update Sede set Empresa=@Empresa, Sede=@Sede ,codigo=@codigo where Id_Sede=@Id";

            var db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);
            using (DbCommand cmd = db.GetSqlStringCommand(SQL_STATEMENT))
            {
                db.AddInParameter(cmd, "@Id", DbType.Int32, entity.Id);
                db.AddInParameter(cmd, "@Sede", DbType.String, entity.sede);
                db.AddInParameter(cmd, "@Empresa", DbType.Int32, entity.empresa.Id);
                db.AddInParameter(cmd, "@Codigo", DbType.String, entity.codigo);
                db.ExecuteNonQuery(cmd);
            }
        }
    }
}

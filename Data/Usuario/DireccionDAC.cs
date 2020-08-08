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
    public class DireccionDAC : DataAccessComponent, IRepository<Direccion>
    {
        private Direccion LoadDireccion(IDataReader dr)
        {
            Direccion direccion = new Direccion();
            direccion.Id = GetDataValue<int>(dr, "ID_Direccion");
            direccion.direccion = GetDataValue<string>(dr, "Direccion");

            return direccion;
        }

        public Direccion Create(Direccion entity)
        {
            const string SQL_STATEMENT = "insert into Direccion (Direccion,Activo) values(@Direccion,1) ";
            var db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);
            using (DbCommand cmd = db.GetSqlStringCommand(SQL_STATEMENT))
            {
                db.AddInParameter(cmd, "@Direccion", DbType.String, entity.direccion);

                entity.Id = Convert.ToInt32(db.ExecuteScalar(cmd));
            }


            return entity;
        }

        public void Delete(int id)
        {
            const string SQL_STATEMENT = "update Direccion set Activo=0 where ID_Direccion=@Id";
            var db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);
            using (DbCommand cmd = db.GetSqlStringCommand(SQL_STATEMENT))
            {
                db.AddInParameter(cmd, "@Id", DbType.Int32, id);
                db.ExecuteNonQuery(cmd);
            }
        }

        public List<Direccion> Read()
        {
            const string SQL_STATEMENT = "select * from Direccion where activo=1";

            List<Direccion> result = new List<Direccion>();
            var db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);
            using (DbCommand cmd = db.GetSqlStringCommand(SQL_STATEMENT))
            {
                using (IDataReader dr = db.ExecuteReader(cmd))
                {
                    while (dr.Read())
                    {
                        Direccion direccion = LoadDireccion(dr);
                        result.Add(direccion);
                    }
                }
            }
            return result;
        }

        public Direccion ReadBy(int id)
        {
            const string SQL_STATEMENT = "select * from Direccion where activo=1 and ID_direccion=@Id";
            Direccion direccion = null;

            var db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);
            using (DbCommand cmd = db.GetSqlStringCommand(SQL_STATEMENT))
            {
                db.AddInParameter(cmd, "@Id", DbType.Int32, id);
                using (IDataReader dr = db.ExecuteReader(cmd))
                {
                    if (dr.Read())
                    {
                        direccion = LoadDireccion(dr);
                    }
                }
            }
            return direccion;
        }
        public Direccion ReadBy(string id)
        {
            const string SQL_STATEMENT = "select * from Direccion where activo=1 and direccion=@Id";
            Direccion direccion = null;

            var db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);
            using (DbCommand cmd = db.GetSqlStringCommand(SQL_STATEMENT))
            {
                db.AddInParameter(cmd, "@Id", DbType.String, id);
                using (IDataReader dr = db.ExecuteReader(cmd))
                {
                    if (dr.Read())
                    {
                        direccion = LoadDireccion(dr);
                    }
                }
            }
            return direccion;
        }

        public void Update(Direccion entity)
        {
            const string SQL_STATEMENT = "update Direccion set Direccion=@Direccion where ID_Direccion=@Id";

            var db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);
            using (DbCommand cmd = db.GetSqlStringCommand(SQL_STATEMENT))
            {
                db.AddInParameter(cmd, "@Id", DbType.Int32, entity.Id);
                db.AddInParameter(cmd, "@Direccion", DbType.String, entity.direccion);
                db.ExecuteNonQuery(cmd);

            }
        }
    }
}

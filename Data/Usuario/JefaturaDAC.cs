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
    public class JefaturaDAC : DataAccessComponent, IRepository<Jefatura>
    {
        private Jefatura LoadJefatura(IDataReader dr)
        {
            Jefatura jefatura = new Jefatura();
            jefatura.Id = GetDataValue<int>(dr, "ID_Jefatura");
            jefatura.jefatura = GetDataValue<string>(dr, "Jefatura");

            return jefatura;
        }
        public Jefatura Create(Jefatura entity)
        {
            const string SQL_STATEMENT = "insert into Jefatura (Jefatura,Activo) values(@Jefatura,1) ";
            var db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);
            using (DbCommand cmd = db.GetSqlStringCommand(SQL_STATEMENT))
            {
                db.AddInParameter(cmd, "@Jefatura", DbType.String, entity.jefatura);
              
                entity.Id = Convert.ToInt32(db.ExecuteScalar(cmd));
            }


            return entity;
        }

        public void Delete(int id)
        {
            const string SQL_STATEMENT = "update Jefatura set Activo=0 where ID_Jefatura=@Id";
            var db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);
            using (DbCommand cmd = db.GetSqlStringCommand(SQL_STATEMENT))
            {
                db.AddInParameter(cmd, "@Id", DbType.Int32, id);
                db.ExecuteNonQuery(cmd);
            }
        }

        public List<Jefatura> Read()
        {
            const string SQL_STATEMENT = "select * from Jefatura where activo=1";
            List<Jefatura> result = new List<Jefatura>();
            var db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);
            using (DbCommand cmd = db.GetSqlStringCommand(SQL_STATEMENT))
            {
                using (IDataReader dr = db.ExecuteReader(cmd))
                {
                    while (dr.Read())
                    {
                        Jefatura jefatura = LoadJefatura(dr);
                        result.Add(jefatura);
                    }
                }
            }
            return result;

        }

        public Jefatura ReadBy(int id)
        {
            const string SQL_STATEMENT = "select * from Jefatura where activo=1 and ID_Jefatura=@Id";
            Jefatura jefatura = null;

            var db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);
            using (DbCommand cmd = db.GetSqlStringCommand(SQL_STATEMENT))
            {
                db.AddInParameter(cmd, "@Id", DbType.Int32, id);
                using (IDataReader dr = db.ExecuteReader(cmd))
                {
                    if (dr.Read())
                    {
                        jefatura = LoadJefatura(dr);
                    }
                }
            }
            return jefatura;
        }
        public Jefatura ReadBy(string id)
        {
            const string SQL_STATEMENT = "select * from Jefatura where activo=1 and Jefatura=@Id";
            Jefatura jefatura = null;

            var db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);
            using (DbCommand cmd = db.GetSqlStringCommand(SQL_STATEMENT))
            {
                db.AddInParameter(cmd, "@Id", DbType.String, id);
                using (IDataReader dr = db.ExecuteReader(cmd))
                {
                    if (dr.Read())
                    {
                        jefatura = LoadJefatura(dr);
                    }
                }
            }
            return jefatura;
        }
        public void Update(Jefatura entity)
        {
            const string SQL_STATEMENT = "update Jefatura set Jefatura=@Jefatura where Id_Jefatura=@Id";

            var db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);
            using (DbCommand cmd = db.GetSqlStringCommand(SQL_STATEMENT))
            {
                db.AddInParameter(cmd, "@Id", DbType.Int32, entity.Id);
                db.AddInParameter(cmd, "@Jefatura", DbType.String, entity.jefatura);
                db.ExecuteNonQuery(cmd);

            }
        }
    }
}

using Microsoft.Practices.EnterpriseLibrary.Data;
using Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Data
{
    public class NivelDAC : DataAccessComponent, IRepository<Nivel>
    {
        public Nivel Create(Nivel entity)
        {
            const string SQL_STATEMENT = "insert into NivelPregunta(Nivel,Activo)values(@Nivel,1) ";
            var db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);
            using (DbCommand cmd = db.GetSqlStringCommand(SQL_STATEMENT))
            {
                db.AddInParameter(cmd, "@Nivel", DbType.String, entity.ElNivel);
                entity.Id = Convert.ToInt32(db.ExecuteScalar(cmd));
            }


            return entity;
        }

        public void Delete(int id)
        {
            const string SQL_STATEMENT = "update NivelPregunta set Activo=0 where ID_Nivel=@Id";
            var db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);
            using (DbCommand cmd = db.GetSqlStringCommand(SQL_STATEMENT))
            {
                db.AddInParameter(cmd, "@Id", DbType.Int32, id);
                db.ExecuteNonQuery(cmd);
            }
        }

        public List<Nivel> Read()
        {
            const string SQL_STATEMENT = "select ID_Nivel,Nivel from NivelPregunta where Activo=1";

            List<Nivel> result = new List<Nivel>();
            var db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);
            using (DbCommand cmd = db.GetSqlStringCommand(SQL_STATEMENT))
            {
                using (IDataReader dr = db.ExecuteReader(cmd))
                {
                    while (dr.Read())
                    {
                        Nivel nivel = LoadNivel(dr);
                        result.Add(nivel);
                    }
                }
            }
            return result;
        }

        public Nivel ReadBy(int id)
        {
            const string SQL_STATEMENT = "select ID_Nivel,Nivel from NivelPregunta where Activo=1 and ID_Nivel=@Id";
            Nivel nivel = null;

            var db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);
            using (DbCommand cmd = db.GetSqlStringCommand(SQL_STATEMENT))
            {
                db.AddInParameter(cmd, "@Id", DbType.Int32, id);
                using (IDataReader dr = db.ExecuteReader(cmd))
                {
                    if (dr.Read())
                    {
                        nivel = LoadNivel(dr);
                    }
                }
            }
            return nivel;
        }

        public void Update(Nivel entity)
        {
            const string SQL_STATEMENT = "update NivelPregunta set Nivel=@Nivel where ID_Nivel=@Id ";

            var db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);
            using (DbCommand cmd = db.GetSqlStringCommand(SQL_STATEMENT))
            {
                db.AddInParameter(cmd, "@Id", DbType.Int32, entity.Id);
                db.AddInParameter(cmd, "@Nivel", DbType.String, entity.ElNivel);
                db.ExecuteNonQuery(cmd);
            }
        }
    

        private Nivel LoadNivel(IDataReader dr)
        {
            Nivel nivel = new Nivel();
            nivel.Id = GetDataValue<int>(dr, "ID_Nivel");
            nivel.ElNivel = GetDataValue<string>(dr, "Nivel");
           
            return nivel;
        }

    }
}

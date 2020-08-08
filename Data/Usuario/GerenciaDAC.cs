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
   public class GerenciaDAC : DataAccessComponent, IRepository<Gerencia>
    {

        private Gerencia LoadGerencia(IDataReader dr)
        {
            Gerencia gerencia = new Gerencia();
            gerencia.Id = GetDataValue<int>(dr, "ID_gerencia");
            gerencia.gerencia = GetDataValue<string>(dr, "gerencia");

            return gerencia;
        }
        public Gerencia Create(Gerencia entity)
        {
            const string SQL_STATEMENT = "insert into Gerencia (sede ,Activo) values(@Gerencia,1) ";
            var db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);
            using (DbCommand cmd = db.GetSqlStringCommand(SQL_STATEMENT))
            {
                db.AddInParameter(cmd, "@Gerencia", DbType.String, entity.gerencia);
      
                entity.Id = Convert.ToInt32(db.ExecuteScalar(cmd));
            }


            return entity;
        }

        public void Delete(int id)
        {
            const string SQL_STATEMENT = "update Gerencia set Activo=0 where ID_Gerencia=@Id";
            var db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);
            using (DbCommand cmd = db.GetSqlStringCommand(SQL_STATEMENT))
            {
                db.AddInParameter(cmd, "@Id", DbType.Int32, id);
                db.ExecuteNonQuery(cmd);
            }
        }

        public List<Gerencia> Read()
        {
            const string SQL_STATEMENT = "select * from Gerencia where activo=1";

            List<Gerencia> result = new List<Gerencia>();
            var db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);
            using (DbCommand cmd = db.GetSqlStringCommand(SQL_STATEMENT))
            {
                using (IDataReader dr = db.ExecuteReader(cmd))
                {
                    while (dr.Read())
                    {
                        Gerencia sede = LoadGerencia(dr);
                        result.Add(sede);
                    }
                }
            }
            return result;
        }

        public Gerencia ReadBy(int id)
        {
            const string SQL_STATEMENT = "select * from Gerencia where activo=1 and ID_Gerencia=@Id";
            Gerencia gerencia = null;

            var db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);
            using (DbCommand cmd = db.GetSqlStringCommand(SQL_STATEMENT))
            {
                db.AddInParameter(cmd, "@Id", DbType.Int32, id);
                using (IDataReader dr = db.ExecuteReader(cmd))
                {
                    if (dr.Read())
                    {
                        gerencia = LoadGerencia(dr);
                    }
                }
            }
            return gerencia;
        }
        public Gerencia ReadBy(string id)
        {
            const string SQL_STATEMENT = "select * from Gerencia where activo=1 and Gerencia=@Id";
            Gerencia gerencia = null;

            var db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);
            using (DbCommand cmd = db.GetSqlStringCommand(SQL_STATEMENT))
            {
                db.AddInParameter(cmd, "@Id", DbType.String, id);
                using (IDataReader dr = db.ExecuteReader(cmd))
                {
                    if (dr.Read())
                    {
                        gerencia = LoadGerencia(dr);
                    }
                }
            }
            return gerencia;
        }

        public void Update(Gerencia entity)
        {
            const string SQL_STATEMENT = "update Gerencia set Gerencia=@Gerencia where Id_Gerencia=@Id";

            var db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);
            using (DbCommand cmd = db.GetSqlStringCommand(SQL_STATEMENT))
            {
                db.AddInParameter(cmd, "@Id", DbType.Int32, entity.Id);
                db.AddInParameter(cmd, "@Gerencia", DbType.String, entity.gerencia);
              

            }
        }

    }
}

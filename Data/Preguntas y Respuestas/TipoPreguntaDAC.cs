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
    public class TipoPreguntaDAC : DataAccessComponent, IRepository<TipoPregunta>
    {
        private TipoPregunta LoadCategoria(IDataReader dr)
        {
            TipoPregunta tipoPregunta = new TipoPregunta();
            tipoPregunta.Id = GetDataValue<int>(dr, "ID_TipoPregunta");
            tipoPregunta.TipoDePregunta = GetDataValue<string>(dr, "TipoPregunta");
           
            return tipoPregunta;
        }
        public TipoPregunta Create(TipoPregunta entity)
        {
            const string SQL_STATEMENT = "insert into TipoPregunta(TipoPregunta,Activo)values(@TipoPregunta,1) ";
            var db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);
            using (DbCommand cmd = db.GetSqlStringCommand(SQL_STATEMENT))
            {
                db.AddInParameter(cmd, "@TipoPregunta", DbType.String, entity.TipoDePregunta);
             
                entity.Id = Convert.ToInt32(db.ExecuteScalar(cmd));
            }


            return entity;
        }

        public void Delete(int id)
        {
           
                const string SQL_STATEMENT = "update TipoPregunta set Activo=0 where ID_TipoPregunta=@Id";
                var db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);
                using (DbCommand cmd = db.GetSqlStringCommand(SQL_STATEMENT))
                {
                    db.AddInParameter(cmd, "@Id", DbType.Int32, id);
                    db.ExecuteNonQuery(cmd);
                
                 }
        }

        public List<TipoPregunta> Read()
        {
            const string SQL_STATEMENT = "select ID_TipoPregunta,TipoPregunta from TipoPregunta where activo=1";

            List<TipoPregunta> result = new List<TipoPregunta>();
            var db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);
            using (DbCommand cmd = db.GetSqlStringCommand(SQL_STATEMENT))
            {
                using (IDataReader dr = db.ExecuteReader(cmd))
                {
                    while (dr.Read())
                    {
                        TipoPregunta tipoPregunta = LoadCategoria(dr);
                        result.Add(tipoPregunta);
                    }
                }
            }
            return result;
        }
    

        public TipoPregunta ReadBy(int id)
        {
            const string SQL_STATEMENT = "select ID_TipoPregunta,TipoPregunta from TipoPregunta  where activo=1 and ID_TipoPregunta=@Id";
            TipoPregunta tipoPregunta = null;

            var db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);
            using (DbCommand cmd = db.GetSqlStringCommand(SQL_STATEMENT))
            {
                db.AddInParameter(cmd, "@Id", DbType.Int32, id);
                using (IDataReader dr = db.ExecuteReader(cmd))
                {
                    if (dr.Read())
                    {
                        tipoPregunta = LoadCategoria(dr);
                    }
                }
            }
            return tipoPregunta;
        }

        public void Update(TipoPregunta entity)
        {
            const string SQL_STATEMENT = "update TipoPregunta set TipoPregunta=@TipoPregunta where ID_TipoPregunta=@Id ";

            var db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);
            using (DbCommand cmd = db.GetSqlStringCommand(SQL_STATEMENT))
            {
                db.AddInParameter(cmd, "@Id", DbType.Int32, entity.Id);
                db.AddInParameter(cmd, "@TipoPregunta", DbType.String, entity.TipoDePregunta);
              
                db.ExecuteNonQuery(cmd);
            }
        }


    }
}

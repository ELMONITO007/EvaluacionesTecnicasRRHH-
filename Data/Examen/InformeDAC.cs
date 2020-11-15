using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Examen
{
  public  class InformeDAC :DataAccessComponent
    {
        public int CantidadExamen()
        {
            const string SQL_STATEMENT = "select * from examen where activo=1";
            int result = 0;
          
            var db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);
            using (DbCommand cmd = db.GetSqlStringCommand(SQL_STATEMENT))
            {
                using (IDataReader dr = db.ExecuteReader(cmd))
                {
                    while (dr.Read())
                    {
                       result++;
                    }
                }
            }
            return result;
        }
        public int CantidadRealizado()
        {
            const string SQL_STATEMENT = "select * from examen where activo=1 and Estado='Realizado'";
            int result = 0;

            var db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);
            using (DbCommand cmd = db.GetSqlStringCommand(SQL_STATEMENT))
            {
                using (IDataReader dr = db.ExecuteReader(cmd))
                {
                    while (dr.Read())
                    {
                        result++;
                    }
                }
            }
            return result;
        }
        public int CantidadARealizar()
        {
            const string SQL_STATEMENT = "select * from examen where activo=1 and Estado='A realizar'";
            int result = 0;

            var db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);
            using (DbCommand cmd = db.GetSqlStringCommand(SQL_STATEMENT))
            {
                using (IDataReader dr = db.ExecuteReader(cmd))
                {
                    while (dr.Read())
                    {
                        result++;
                    }
                }
            }
            return result;
        }


        public int CantidadAprobado()
        {
            const string SQL_STATEMENT = "select * from examen where activo=1  and Aprobado=1";
            int result = 0;

            var db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);
            using (DbCommand cmd = db.GetSqlStringCommand(SQL_STATEMENT))
            {
                using (IDataReader dr = db.ExecuteReader(cmd))
                {
                    while (dr.Read())
                    {
                        result++;
                    }
                }
            }
            return result;
        }
        public int CantidadDesaprobado()
        {
            const string SQL_STATEMENT = "select * from examen where activo=1  and Aprobado=0";
            int result = 0;

            var db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);
            using (DbCommand cmd = db.GetSqlStringCommand(SQL_STATEMENT))
            {
                using (IDataReader dr = db.ExecuteReader(cmd))
                {
                    while (dr.Read())
                    {
                        result++;
                    }
                }
            }
            return result;
        }

        public int CantidadPreguntas()
        {
            const string SQL_STATEMENT = "select * from Pregunta where activo=1 and SubPregunta=0";
            int result = 0;

            var db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);
            using (DbCommand cmd = db.GetSqlStringCommand(SQL_STATEMENT))
            {
                using (IDataReader dr = db.ExecuteReader(cmd))
                {
                    while (dr.Read())
                    {
                        result++;
                    }
                }
            }
            return result;
        }
    }
}

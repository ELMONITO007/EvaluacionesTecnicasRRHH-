using Entities;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Data.Servicios
{
    public class PDFDac : DataAccessComponent, IRepository<PDF>
    {
        private PDF LoadPDF(IDataReader dr)
        {
            PDF pDF = new PDF();
            pDF.unUsuario.usuarios.Id = GetDataValue<int>(dr, "Id");
            pDF.path = GetDataValue<string>(dr, "path");
           

            return pDF;
        }


        public PDF Create(PDF entity)
        {
            const string SQL_STATEMENT = "insert into PDF(Id,path)values(@Id,@path)";
            var db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);
            using (DbCommand cmd = db.GetSqlStringCommand(SQL_STATEMENT))
            {
                db.AddInParameter(cmd, "@Id", DbType.String, entity.unUsuario.usuarios.Id);
                db.AddInParameter(cmd, "@path", DbType.String, entity.path);
                db.ExecuteNonQuery(cmd);

            }

            return entity;
        }

        public void Delete(int id)
        {
            const string SQL_STATEMENT = "delete pdf  where id=@Id";
            var db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);
            using (DbCommand cmd = db.GetSqlStringCommand(SQL_STATEMENT))
            {
                db.AddInParameter(cmd, "@Id", DbType.Int32, id);
                db.ExecuteNonQuery(cmd);
            }
        }

        public List<PDF> Read()
        {
            const string SQL_STATEMENT = "select * from pdf ";

            List<PDF> result = new List<PDF>();
            var db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);
            using (DbCommand cmd = db.GetSqlStringCommand(SQL_STATEMENT))
            {
                using (IDataReader dr = db.ExecuteReader(cmd))
                {
                    while (dr.Read())
                    {
                        PDF pDF = LoadPDF(dr);
                        result.Add(pDF);
                    }
                }
            }
            return result;
        }

        public PDF ReadBy(int id)
        {
            const string SQL_STATEMENT = "select * from pdf where  id=@Id";
            PDF pDF = null;

            var db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);
            using (DbCommand cmd = db.GetSqlStringCommand(SQL_STATEMENT))
            {
                db.AddInParameter(cmd, "@Id", DbType.Int32, id);
                using (IDataReader dr = db.ExecuteReader(cmd))
                {
                    if (dr.Read())
                    {
                        pDF = LoadPDF(dr);
                    }
                }
            }
            return pDF;
        }

        public void Update(PDF entity)
        {
            throw new NotImplementedException();
        }
    }
}

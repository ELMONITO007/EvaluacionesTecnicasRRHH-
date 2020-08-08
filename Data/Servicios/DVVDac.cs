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
    public class DVVDac : DataAccessComponent, IRepository<DVV>
    {
        private DVV LoadDVV(IDataReader dr)
        {
            DVV dvv = new DVV();
            dvv.Id = GetDataValue<int>(dr, "Id_DVV");
            dvv.tabla = GetDataValue<string>(dr, "tabla");
            dvv.dvv = GetDataValue<string>(dr, "dvv");
            return dvv;
        }
        public DVV Create(DVV entity)
        {
            const string SQL_STATEMENT = "insert into DVV(Tabla,DVV)values(@tabla,@DVV)";
            var db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);
            using (DbCommand cmd = db.GetSqlStringCommand(SQL_STATEMENT))
            {
                db.AddInParameter(cmd, "@tabla", DbType.String, entity.tabla);
                db.AddInParameter(cmd, "@DVV", DbType.String, entity.dvv);
                db.ExecuteNonQuery(cmd);
        
            }

            return entity;
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<DVV> Read()
        {
            throw new NotImplementedException();
        }

        public DVV ReadBy(int id)
        {
            throw new NotImplementedException();
        }

        public DVV ReadByTabla(string tabla)
        {
            const string SQL_STATEMENT = "select top 1 * from DVV where tabla=@tabla order by ID_DVV desc";

            DVV result = new DVV();
            var db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);
            using (DbCommand cmd = db.GetSqlStringCommand(SQL_STATEMENT))
            {
                db.AddInParameter(cmd, "@tabla", DbType.String, tabla);
                using (IDataReader dr = db.ExecuteReader(cmd))
                {
                    if (dr.Read())
                    {

                        result = LoadDVV(dr);
                    }
                }
            }
            return result;
        }
        public void Update(DVV entity)
        {
            throw new NotImplementedException();
        }
    }
}

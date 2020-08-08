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
  public  class EmpresaDAC : DataAccessComponent, IRepository<Empresa>
    {
        private Empresa LoadEmpresa(IDataReader dr)
        {
            Empresa empresa = new Empresa();
            empresa.Id = GetDataValue<int>(dr, "ID_Empresa");
            empresa.empresa = GetDataValue<string>(dr, "Empresa");

            return empresa;
        }
        public Empresa Create(Empresa entity)
        {
            const string SQL_STATEMENT = "insert into Empresa (Empresa ,Activo) values(@Empresa,1) ";
            var db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);
            using (DbCommand cmd = db.GetSqlStringCommand(SQL_STATEMENT))
            {
                db.AddInParameter(cmd, "@Empresa", DbType.String, entity.empresa);

                entity.Id = Convert.ToInt32(db.ExecuteScalar(cmd));
            }


            return entity;
        }

        public void Delete(int id)
        {
            const string SQL_STATEMENT = "update Empresa set Activo=0 where Id_Empresa=@Id";
            var db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);
            using (DbCommand cmd = db.GetSqlStringCommand(SQL_STATEMENT))
            {
                db.AddInParameter(cmd, "@Id", DbType.Int32, id);
                db.ExecuteNonQuery(cmd);
            }
        }

        public List<Empresa> Read()
        {
            const string SQL_STATEMENT = "select * from Empresa where activo=1";

            List<Empresa> result = new List<Empresa>();
            var db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);
            using (DbCommand cmd = db.GetSqlStringCommand(SQL_STATEMENT))
            {
                using (IDataReader dr = db.ExecuteReader(cmd))
                {
                    while (dr.Read())
                    {
                        Empresa empresa = LoadEmpresa(dr);
                        result.Add(empresa);
                    }
                }
            }
            return result;
        }

        public Empresa ReadBy(int id)
        {
            const string SQL_STATEMENT = "select * from Empresa where activo=1 and ID_Empresa=@Id";
            Empresa empresa = null;

            var db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);
            using (DbCommand cmd = db.GetSqlStringCommand(SQL_STATEMENT))
            {
                db.AddInParameter(cmd, "@Id", DbType.Int32, id);
                using (IDataReader dr = db.ExecuteReader(cmd))
                {
                    if (dr.Read())
                    
                    {
                        empresa = LoadEmpresa(dr);
                    }
                }
            }
            return empresa;
        }
        public Empresa ReadBy(string id)
        {
            const string SQL_STATEMENT = "select * from Empresa where activo=1 and Empresa=@Id";
            Empresa empresa = null;

            var db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);
            using (DbCommand cmd = db.GetSqlStringCommand(SQL_STATEMENT))
            {
                db.AddInParameter(cmd, "@Id", DbType.String, id);
                using (IDataReader dr = db.ExecuteReader(cmd))
                {
                    if (dr.Read())

                    {
                        empresa = LoadEmpresa(dr);
                    }
                }
            }
            return empresa;
        }
        public void Update(Empresa entity)
        {
            const string SQL_STATEMENT = "update Empresa set Empresa=@Empresa where ID_Empresa=@Id";

            var db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);
            using (DbCommand cmd = db.GetSqlStringCommand(SQL_STATEMENT))
            {
                db.AddInParameter(cmd, "@Id", DbType.Int32, entity.Id);
                db.AddInParameter(cmd, "@Empresa", DbType.String, entity.empresa);
                db.ExecuteNonQuery(cmd);

            }
        }
    }
}

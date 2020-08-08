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
    public class CategoriaDAC : DataAccessComponent,IRepository<Categoria>
    {
        private Categoria LoadCategoria(IDataReader dr)
        {
            Categoria categoria = new Categoria();
            categoria.Id = GetDataValue<int>(dr, "ID_Categoria");
            categoria.LaCategoria = GetDataValue<string>(dr, "Categoria");
            categoria.Descripcion = GetDataValue<string>(dr, "Descripcion");
            return categoria;
        }


        public Categoria Create(Categoria entity)
        {
            const string SQL_STATEMENT = "insert into categoria(Categoria,Descripcion,Activo)values(@Categoria,@Descripcion,1) ";
            var db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);
            using (DbCommand cmd = db.GetSqlStringCommand(SQL_STATEMENT))
            {
                db.AddInParameter(cmd, "@Categoria", DbType.String, entity.LaCategoria);
                db.AddInParameter(cmd, "@Descripcion", DbType.String, entity.Descripcion);
                entity.Id = Convert.ToInt32(db.ExecuteScalar(cmd));
            }


            return entity;
        }

        public void Delete(int id)
        {
            const string SQL_STATEMENT = "update Categoria set Activo=0 where ID_Categoria=@Id";
            var db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);
            using (DbCommand cmd = db.GetSqlStringCommand(SQL_STATEMENT))
            {
                db.AddInParameter(cmd, "@Id", DbType.Int32, id);
                db.ExecuteNonQuery(cmd);
            }
        }

        public List<Categoria> Read()
        {
            const string SQL_STATEMENT = "select ID_Categoria,Categoria,Descripcion from categoria where activo=1";

            List<Categoria> result = new List<Categoria>();
            var db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);
            using (DbCommand cmd = db.GetSqlStringCommand(SQL_STATEMENT))
            {
                using (IDataReader dr = db.ExecuteReader(cmd))
                {
                    while (dr.Read())
                    {
                        Categoria categoria = LoadCategoria(dr);
                        result.Add(categoria);
                    }
                }
            }
            return result;
        }

        public Categoria ReadBy(int id)
        {
            const string SQL_STATEMENT = "select ID_Categoria,Categoria,Descripcion from categoria where activo=1 and ID_Categoria=@Id";
            Categoria categoria = null;

            var db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);
            using (DbCommand cmd = db.GetSqlStringCommand(SQL_STATEMENT))
            {
                db.AddInParameter(cmd, "@Id", DbType.Int32, id);
                using (IDataReader dr = db.ExecuteReader(cmd))
                {
                    if (dr.Read())
                    {
                        categoria = LoadCategoria(dr);
                    }
                }
            }
            return categoria;
        }

        public void Update(Categoria entity)
        {
            const string SQL_STATEMENT = "update Categoria set Categoria=@Categoria, Descripcion=@Descripcion where ID_Categoria=@Id ";

            var db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);
            using (DbCommand cmd = db.GetSqlStringCommand(SQL_STATEMENT))
            {
                db.AddInParameter(cmd, "@Id", DbType.Int32, entity.Id);
                db.AddInParameter(cmd, "@Categoria", DbType.String, entity.LaCategoria);
                db.AddInParameter(cmd, "@Descripcion", DbType.String, entity.Descripcion);
                db.ExecuteNonQuery(cmd);
            }
        }
    }
}

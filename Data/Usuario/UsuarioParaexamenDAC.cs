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
    public class UsuarioParaexamenDAC : DataAccessComponent, IRepository<UsuarioParaExamen>
    {
        private UsuarioParaExamen LoadUsuarioParaexamen(IDataReader dr)
        {
            UsuarioParaExamen usuarioParaExamen = new UsuarioParaExamen();
            usuarioParaExamen.usuarios.Id = GetDataValue<int>(dr, "ID");
            usuarioParaExamen.sede.Id = GetDataValue<int>(dr, "Id_Sede");
            usuarioParaExamen.direccion.Id = GetDataValue<int>(dr, "Id_direccion");
            usuarioParaExamen.gerencia.Id = GetDataValue<int>(dr, "Id_gerencia");
            usuarioParaExamen.jefatura.Id = GetDataValue<int>(dr, "Id_jefatura");
            usuarioParaExamen.sector.Id = GetDataValue<int>(dr, "Id_sector");


            
          
            return usuarioParaExamen;
        }
        public UsuarioParaExamen Create(UsuarioParaExamen entity)
        {
            const string SQL_STATEMENT = "insert into UsuarioParaExamen(Id,Id_Sede,Id_Direccion,Id_Gerencia,Id_Jefatura,Id_Sector,activo)values(@Id,@Id_Sede,@Id_Direccion,@Id_Gerencia,@Id_Jefatura,@Id_Sector,1)";
            var db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);
            using (DbCommand cmd = db.GetSqlStringCommand(SQL_STATEMENT))
            {
                db.AddInParameter(cmd, "@ID", DbType.String, entity.usuarios.Id);
                db.AddInParameter(cmd, "@Id_Sede", DbType.Int32, entity.sede.Id);
                db.AddInParameter(cmd, "@Id_direccion", DbType.Int32, entity.direccion.Id);
                db.AddInParameter(cmd, "@Id_gerencia", DbType.Int32, entity.gerencia.Id);
                db.AddInParameter(cmd, "@Id_jefatura", DbType.Int32, entity.jefatura.Id);
                db.AddInParameter(cmd, "@Id_sector", DbType.Int32, entity.sector.Id);
           
                entity.Id = Convert.ToInt32(db.ExecuteScalar(cmd));
            }

            return entity;
        }

        public void Delete(int id)
        {
            const string SQL_STATEMENT = "update UsuarioParaExamen set Activo=0 where ID=@Id";
            var db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);
            using (DbCommand cmd = db.GetSqlStringCommand(SQL_STATEMENT))
            {
                db.AddInParameter(cmd, "@Id", DbType.Int32, id);
                db.ExecuteNonQuery(cmd);
            }
        }

        public List<UsuarioParaExamen> Read()
        {
            const string SQL_STATEMENT = "select * from UsuarioParaExamen where activo=1";

            List<UsuarioParaExamen> result = new List<UsuarioParaExamen>();
            var db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);
            using (DbCommand cmd = db.GetSqlStringCommand(SQL_STATEMENT))
            {
                using (IDataReader dr = db.ExecuteReader(cmd))
                {
                    while (dr.Read())
                    {
                        UsuarioParaExamen usuario = LoadUsuarioParaexamen(dr);
                        result.Add(usuario);
                    }
                }
            }
            return result;
        }

        public UsuarioParaExamen ReadBy(int id)
        {

            const string SQL_STATEMENT = "select * from UsuarioParaExamen where activo=1 where activo=1 and id=@Id ";
            UsuarioParaExamen result = null;

            var db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);
            using (DbCommand cmd = db.GetSqlStringCommand(SQL_STATEMENT))
            {
                db.AddInParameter(cmd, "@Id", DbType.String, id);
               
                using (IDataReader dr = db.ExecuteReader(cmd))
                {
                    if (dr.Read())
                    {
                        result = LoadUsuarioParaexamen(dr);
                    }
                }
            }
            return result;
        }
        public UsuarioParaExamen ReadBy(string id)
        {

            const string SQL_STATEMENT = "select * from UsuarioParaExamen where activo=1 where activo=1 and id=@Id ";
            UsuarioParaExamen result = null;

            var db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);
            using (DbCommand cmd = db.GetSqlStringCommand(SQL_STATEMENT))
            {
                db.AddInParameter(cmd, "@Id", DbType.String, id);

                using (IDataReader dr = db.ExecuteReader(cmd))
                {
                    if (dr.Read())
                    {
                        result = LoadUsuarioParaexamen(dr);
                    }
                }
            }
            return result;
        }

        public void Update(UsuarioParaExamen entity)
        {
            const string SQL_STATEMENT = "update UsuarioParaExamen set Id_Sede=@Id_Sede,Id_Direccion=@Id_Direccion,Id_Gerencia=@Id_Gerencia,Id_Jefatura=@Id_Jefatura,Id_Sector=@Id_Sector where id=@Id";

            var db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);
            using (DbCommand cmd = db.GetSqlStringCommand(SQL_STATEMENT))
            {
                db.AddInParameter(cmd, "@ID", DbType.String, entity.usuarios.Id);
                db.AddInParameter(cmd, "@Id_Sede", DbType.Int32, entity.sede.Id);
                db.AddInParameter(cmd, "@Id_direccion", DbType.Int32, entity.direccion.Id);
                db.AddInParameter(cmd, "@Id_gerencia", DbType.Int32, entity.gerencia.Id);
                db.AddInParameter(cmd, "@Id_jefatura", DbType.Int32, entity.jefatura.Id);
                db.AddInParameter(cmd, "@Id_sector", DbType.Int32, entity.sector.Id);
         
                db.ExecuteNonQuery(cmd);
            }
        }


    }
}

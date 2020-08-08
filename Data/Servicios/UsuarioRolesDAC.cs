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
    public class UsuarioRolesDAC : DataAccessComponent, IRepository<UsuarioRoles>
    {
        private UsuarioRoles LoadUsuarioRoles(IDataReader dr)
        {
            try
            {
                UsuarioRoles usuarioRoles = new UsuarioRoles();
                usuarioRoles.usuarios.Id = GetDataValue<int>(dr, "UserId");
                usuarioRoles.usuarios.UserName = GetDataValue<string>(dr, "UserName");
                usuarioRoles.usuarios.Email = GetDataValue<string>(dr, "Email");


                usuarioRoles.roles.id = GetDataValue<string>(dr, "RoleId");
                usuarioRoles.roles.name = GetDataValue<string>(dr, "name");
                return usuarioRoles;
            }
            catch (Exception e)
            {

                throw;
            }
  
        }

        
        public UsuarioRoles Create(UsuarioRoles entity)
        {
            const string SQL_STATEMENT = "insert into AspNetUserRoles(RoleId,UserId)values(@RoleId, @UserId) ";
            var db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);
            using (DbCommand cmd = db.GetSqlStringCommand(SQL_STATEMENT))
            {
                db.AddInParameter(cmd, "@UserId", DbType.Int32, entity.usuarios.Id);
                db.AddInParameter(cmd, "@RoleId", DbType.String, entity.roles.id);
                entity.Id = Convert.ToInt32(db.ExecuteScalar(cmd));
            }


            return entity;
        }
    

        public void Delete(UsuarioRoles entity)
        {
            const string SQL_STATEMENT = "delete from AspNetUserRoles where UserId=@UserId and RoleId=@RoleId";
            var db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);
            using (DbCommand cmd = db.GetSqlStringCommand(SQL_STATEMENT))
            {
                db.AddInParameter(cmd, "@RoleId", DbType.Int32, entity.usuarios.Id);
                db.AddInParameter(cmd, "@UserId", DbType.String, entity.roles.id);
                db.ExecuteNonQuery(cmd);
            }
        }
        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<UsuarioRoles> Read()
        {
            const string SQL_STATEMENT = "select * from AspNetUserRoles as ur inner join AspNetRoles as r on ur.RoleId=r.Id inner join Usuario as u on u.Id=ur.UserId where r.activo=1 and u.activo=1";

            List<UsuarioRoles> result = new List<UsuarioRoles>();
            var db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);
            using (DbCommand cmd = db.GetSqlStringCommand(SQL_STATEMENT))
            {
                using (IDataReader dr = db.ExecuteReader(cmd))
                {
                    while (dr.Read())
                    {
                        UsuarioRoles usuarioRoles = LoadUsuarioRoles(dr);
                        result.Add(usuarioRoles);
                    }
                }
            }
            return result;
        }

        public UsuarioRoles ReadBy(int id)
        {
            throw new NotImplementedException();
        }

        public UsuarioRoles ReadBy(string Id_roles, int Id_usuario)
        {
            const string SQL_STATEMENT = "select * from AspNetUserRoles as ur inner join AspNetRoles as r on ur.RoleId=r.Id inner join Usuario as u on u.Id=ur.UserId where r.activo=1 and u.activo=1 and UserId=@UserId  and RoleId=@RoleId";
            string a = "select * from AspNetUserRoles as ur inner join AspNetRoles as r on ur.RoleId=r.Id inner join Usuario as u on u.Id=ur.UserId where r.activo=1 and u.activo=1 and UserId='" + Id_usuario +"'  and RoleId="+Id_roles;
          
            UsuarioRoles   roles  = null;

            var db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);
            using (DbCommand cmd = db.GetSqlStringCommand(SQL_STATEMENT))
            {
                db.AddInParameter(cmd, "@UserId", DbType.String,Id_usuario);
                db.AddInParameter(cmd, "@RoleId", DbType.String, Id_roles);
                using (IDataReader dr = db.ExecuteReader(cmd))
                {
                    if (dr.Read())
                    {
                        roles = LoadUsuarioRoles(dr);
                    }
                }
            }
            return roles;
        }

        public List<UsuarioRoles> ReadByUsuario(int Id_usuario)
        {
            const string SQL_STATEMENT = "select * from AspNetUserRoles as ur inner join AspNetRoles as r on ur.RoleId=r.Id inner join Usuario as u on u.Id=ur.UserId where r.activo=1 and u.activo=1 and UserId=@UserId";

            List<UsuarioRoles> result = new List<UsuarioRoles>();
            var db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);
            using (DbCommand cmd = db.GetSqlStringCommand(SQL_STATEMENT))
            {
                db.AddInParameter(cmd, "@UserId", DbType.String,Id_usuario);
                using (IDataReader dr = db.ExecuteReader(cmd))
                {
                    while (dr.Read())
                    {
                        UsuarioRoles usuarioRoles = LoadUsuarioRoles(dr);
                        result.Add(usuarioRoles);
                    }
                }
            }
            return result;
        }
        public List<UsuarioRoles> ReadByRol(string Id_roles)
        {
            const string SQL_STATEMENT = "select * from AspNetUserRoles as ur inner join AspNetRoles as r on ur.RoleId=r.Id inner join Usuario as u on u.Id=ur.UserId where r.activo=1 and u.activo=1 and RoleId=@RoleId";
            List<UsuarioRoles> result = new List<UsuarioRoles>();
            var db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);
            using (DbCommand cmd = db.GetSqlStringCommand(SQL_STATEMENT))
            {
                db.AddInParameter(cmd, "@RoleId", DbType.String, Id_roles);

                using (IDataReader dr = db.ExecuteReader(cmd))
                {
                    while (dr.Read())
                    {
                        UsuarioRoles usuarioRoles = LoadUsuarioRoles(dr);
                        result.Add(usuarioRoles);
                    }
                }
            }
            return result;
        }

        public void Update(UsuarioRoles entity)
        {
            const string SQL_STATEMENT = "update AspNetUserRoles set RoleId=@RoleId where RoleId=@RoleId and UserId=@UserId ";
            var db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);
            using (DbCommand cmd = db.GetSqlStringCommand(SQL_STATEMENT))
            {
                db.AddInParameter(cmd, "@UserId ", DbType.Int32, entity.usuarios.Id);
                db.AddInParameter(cmd, "@RoleId", DbType.String, entity.roles.id);
               
            }

        }
    }
}

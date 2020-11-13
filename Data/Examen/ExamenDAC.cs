using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Examen;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace Data.Examen
{
    public class ExamenDAC :DataAccessComponent,  IRepository<Entities.Examen.Examen>
    {
        private Entities.Examen.Examen LoadCategoria(IDataReader dr)
        {
            Entities.Examen.Examen examen = new Entities.Examen.Examen();
            examen.Categoria.Id = GetDataValue<int>(dr, "ID_Categoria");
            examen.Id = GetDataValue<int>(dr, "ID_Examen");
            examen.Fecha = GetDataValue<DateTime>(dr, "Fecha");
            examen.Resultado = GetDataValue<int>(dr, "Resultado");
            examen.cantidadPreguntas = GetDataValue<int>(dr, "Cantidad");
            examen.Aprobado = GetDataValue<bool>(dr, "Aprobado");
            examen.usuario.Id = GetDataValue<int>(dr, "ID");
            examen.Estado = GetDataValue<string>(dr, "Estado");
            examen.RespuestasCorrectas = GetDataValue<int>(dr, "RespuestasCorrectas");
            examen.RespuestasIncorrectas = GetDataValue<int>(dr, "RespuestasIncorrectas");
            return examen;
        }
        public Entities.Examen.Examen Create(Entities.Examen.Examen entity)
        {

            const string SQL_STATEMENT = "insert into Examen(Fecha,Estado,Resultado,Aprobado,Id,ID_Categoria,Activo,RespuestasCorrectas,RespuestasIncorrectas,Cantidad)values(@Fecha,@Estado,@Resultado,@Aprobado,@Id,@IdCategoria,1,0,0,@Cantidad) ";
            var db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);
            using (DbCommand cmd = db.GetSqlStringCommand(SQL_STATEMENT))
            {
                db.AddInParameter(cmd, "@Cantidad", DbType.Int32, entity.cantidadPreguntas);
                db.AddInParameter(cmd, "@Fecha", DbType.DateTime, entity.Fecha);
                db.AddInParameter(cmd, "@Estado", DbType.String, entity.Estado);
                db.AddInParameter(cmd, "@Resultado", DbType.Int16, entity.Resultado);
                db.AddInParameter(cmd, "@Aprobado", DbType.Boolean, entity.Aprobado);
                db.AddInParameter(cmd, "@Id", DbType.Int16, entity.usuario.Id);
                db.AddInParameter(cmd, "@IdCategoria", DbType.Int16, entity.Categoria.Id);
                entity.Id = Convert.ToInt32(db.ExecuteScalar(cmd));
            }

            return entity;
        }

        public void Delete(int id)
        {
            const string SQL_STATEMENT = "update Examen set Activo=0 where ID_Examen=@Id";
            var db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);
            using (DbCommand cmd = db.GetSqlStringCommand(SQL_STATEMENT))
            {
                db.AddInParameter(cmd, "@Id", DbType.Int32, id);
                db.ExecuteNonQuery(cmd);
            }
        }

        public List<Entities.Examen.Examen> Read()
        {
            const string SQL_STATEMENT = "select * from examen where activo=1";

            List<Entities.Examen.Examen> result = new List<Entities.Examen.Examen>();
            var db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);
            using (DbCommand cmd = db.GetSqlStringCommand(SQL_STATEMENT))
            {
                using (IDataReader dr = db.ExecuteReader(cmd))
                {
                    while (dr.Read())
                    {
                        Entities.Examen.Examen examen = LoadCategoria(dr);
                        result.Add(examen);
                    }
                }
            }
            return result;
        }

        public Entities.Examen.Examen ReadBy(int id)
        {
            const string SQL_STATEMENT = "select * from examen where activo=1 and ID_examen=@Id";
            Entities.Examen.Examen examen = null;

            var db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);
            using (DbCommand cmd = db.GetSqlStringCommand(SQL_STATEMENT))
            {
                db.AddInParameter(cmd, "@Id", DbType.Int32, id);
                using (IDataReader dr = db.ExecuteReader(cmd))
                {
                    if (dr.Read())
                    {
                        examen = LoadCategoria(dr);
                    }
                }
            }
            return examen;
        }
        public Entities.Examen.Examen ReadByUsuario(int id)
        {
            const string SQL_STATEMENT = "select top 1 *  from examen where ID=@Id and Estado='a Realizar' order by Fecha desc ";
            Entities.Examen.Examen examen = null;

            var db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);
            using (DbCommand cmd = db.GetSqlStringCommand(SQL_STATEMENT))
            {
                db.AddInParameter(cmd, "@Id", DbType.Int32, id);
                using (IDataReader dr = db.ExecuteReader(cmd))
                {
                    if (dr.Read())
                    {
                        examen = LoadCategoria(dr);
                    }
                }
            }
            return examen;
        }


        public Entities.Examen.Examen ReadByEstado(int id)
        {
            const string SQL_STATEMENT = "select * from examen where activo=1 and ID=@Id and estado='A Realizar'";
            Entities.Examen.Examen examen = null;

            var db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);
            using (DbCommand cmd = db.GetSqlStringCommand(SQL_STATEMENT))
            {
                db.AddInParameter(cmd, "@Id", DbType.Int32, id);
                using (IDataReader dr = db.ExecuteReader(cmd))
                {
                    if (dr.Read())
                    {
                        examen = LoadCategoria(dr);
                    }
                }
            }
            return examen;
        }

        public Entities.Examen.Examen ReadBy(DateTime  Fecha)
        {
            const string SQL_STATEMENT = "select * from examen where activo=1 and Fecha=@Id";
            Entities.Examen.Examen examen = null;

            var db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);
            using (DbCommand cmd = db.GetSqlStringCommand(SQL_STATEMENT))
            {
                db.AddInParameter(cmd, "@Id", DbType.DateTime, Fecha);
                using (IDataReader dr = db.ExecuteReader(cmd))
                {
                    if (dr.Read())
                    {
                        examen = LoadCategoria(dr);
                    }
                }
            }
            return examen;
        }
        public void Update(Entities.Examen.Examen entity)
        {
            const string SQL_STATEMENT = "update Examen set Fecha=@Fecha, Estado=@Estado, Resultado=@Resultado , Aprobado=@Aprobado,RespuestasCorrectas=@RespuestasCorrectas,RespuestasIncorrectas=@RespuestasIncorrectas   where ID_examen=@Id ";

            var db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);
            using (DbCommand cmd = db.GetSqlStringCommand(SQL_STATEMENT))
            {
                db.AddInParameter(cmd, "@Id", DbType.Int32, entity.Id);
                db.AddInParameter(cmd, "@Fecha", DbType.DateTime, entity.Fecha);
                db.AddInParameter(cmd, "@Estado", DbType.String, entity.Estado);
                db.AddInParameter(cmd, "@Resultado", DbType.Int16, entity.Resultado);
                db.AddInParameter(cmd, "@Aprobado", DbType.Boolean, entity.Aprobado);
                db.AddInParameter(cmd, "@RespuestasCorrectas", DbType.Int16, entity.RespuestasCorrectas);
                db.AddInParameter(cmd, "@RespuestasIncorrectas", DbType.Int16, entity.RespuestasIncorrectas);
                db.ExecuteNonQuery(cmd);
            }
        }
    }
}

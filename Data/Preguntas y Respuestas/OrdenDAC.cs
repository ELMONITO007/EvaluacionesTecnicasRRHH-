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
    public class OrdenDAC : DataAccessComponent, IRepository<Orden>
    {
        private Orden LoadOrden(IDataReader dr)
        {
            Orden orden = new Orden();
            orden.Id = GetDataValue<int>(dr, "ID_Respuesta");
            orden.NumeroOrden = GetDataValue<int>(dr, "Orden");
            orden.LaRespuesta = GetDataValue<string>(dr, "Respuesta");
            orden.pregunta.Id = GetDataValue<int>(dr, "ID_Pregunta");
            orden.pregunta.LaPregunta = GetDataValue<string>(dr, "Pregunta");
            orden.pregunta.categoria.Id = GetDataValue<int>(dr, "ID_Categoria");
            orden.pregunta.categoria.LaCategoria = GetDataValue<string>(dr, "Categoria");
            return orden;
        }
        private Orden LoadOrdenmin(IDataReader dr)
        {
            Orden orden = new Orden();
            orden.Id = GetDataValue<int>(dr, "ID_Respuesta");
            orden.NumeroOrden = GetDataValue<int>(dr, "Orden");
            orden.LaRespuesta = GetDataValue<string>(dr, "Respuesta");
            orden.pregunta.Id = GetDataValue<int>(dr, "ID_Pregunta");
            return orden;
        }


        public Orden Create(Orden entity)
        {
            const string SQL_STATEMENT = "insert into Respuesta(Respuesta,Orden,ID_Pregunta,Activo)values(@Respuesta,@Orden,@ID_Pregunta,1)";
            var db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);
            using (DbCommand cmd = db.GetSqlStringCommand(SQL_STATEMENT))
            {

                db.AddInParameter(cmd, "@Respuesta", DbType.String, entity.LaRespuesta);
                db.AddInParameter(cmd, "@Orden", DbType.Int32, entity.NumeroOrden);
                db.AddInParameter(cmd, "@ID_Pregunta", DbType.Int32, entity.pregunta.Id);
                
                entity.Id = Convert.ToInt32(db.ExecuteScalar(cmd));
            }


            return entity;
        }

        public void Delete(int id)
        {
            const string SQL_STATEMENT = "update Respuesta set Activo=0 where ID_Respuesta=@Id";
            var db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);
            using (DbCommand cmd = db.GetSqlStringCommand(SQL_STATEMENT))
            {
                db.AddInParameter(cmd, "@Id", DbType.Int32, id);
                db.ExecuteNonQuery(cmd);
            }
        }

        public List<Orden> Read()
        {
            const string SQL_STATEMENT = "Select ca.Categoria, pc.ID_Categoria, ID_Respuesta, Pregunta,r.Respuesta,r.Orden,r.ID_Pregunta from Respuesta as r join Pregunta  as p  on r.ID_Pregunta=p.ID_Pregunta inner join PreguntaCategoria as pc on pc.ID_Pregunta=p.ID_Pregunta inner join Categoria as ca on ca.ID_Categoria=pc.ID_Categoria where r.Activo=1 and p.Activo=1 and orden is not Null order by ID_Pregunta";

            List<Orden> result = new List<Orden>();
            var db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);
            using (DbCommand cmd = db.GetSqlStringCommand(SQL_STATEMENT))
            {
                using (IDataReader dr = db.ExecuteReader(cmd))
                {
                    while (dr.Read())
                    {
                        Orden orden = LoadOrden(dr);
                        result.Add(orden);
                    }
                }
            }
            return result;
        }

        public Orden ReadBy(int id)
        {
            const string SQL_STATEMENT = "Select ca.Categoria, pc.ID_Categoria, ID_Respuesta, Pregunta,r.Respuesta,r.Orden,r.ID_Pregunta from Respuesta as r join Pregunta  as p  on r.ID_Pregunta=p.ID_Pregunta inner join PreguntaCategoria as pc on pc.ID_Pregunta=p.ID_Pregunta inner join Categoria as ca on ca.ID_Categoria=pc.ID_Categoria where r.Activo=1 and p.Activo=1 and ID_Respuesta=@id and orden is not Null";
            Orden   orden = null;

            var db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);
            using (DbCommand cmd = db.GetSqlStringCommand(SQL_STATEMENT))
            {
                db.AddInParameter(cmd, "@Id", DbType.Int32, id);
                using (IDataReader dr = db.ExecuteReader(cmd))
                {
                    if (dr.Read())
                    {
                        orden = LoadOrden(dr);
                    }
                }
            }
            return orden;
        }

        public List<Orden> listaRespuestaOrdenAlAzar(int id_pregunta)
        {
            const string SQL_STATEMENT = "select ID_Respuesta,Respuesta,Orden,ID_Pregunta from Respuesta where Activo=1 and ID_Pregunta=@Id and orden is not Null order by NEWID()";

            List<Orden> result = new List<Orden>();
            var db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);
            using (DbCommand cmd = db.GetSqlStringCommand(SQL_STATEMENT))
            {
                db.AddInParameter(cmd, "@Id", DbType.Int32, id_pregunta);
                using (IDataReader dr = db.ExecuteReader(cmd))
                {
                    while (dr.Read())
                    {
                        Orden orden = LoadOrdenmin(dr);
                        result.Add(orden);
                    }
                }
            }
            return result;
        }

        public void Update(Orden entity)
        {
            const string SQL_STATEMENT = "update Respuesta set Respuesta=@Respuesta,Orden=@Orden where ID_Respuesta=@Id ";

            var db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);
            using (DbCommand cmd = db.GetSqlStringCommand(SQL_STATEMENT))
            {
                db.AddInParameter(cmd, "@Id", DbType.Int32, entity.Id);
                db.AddInParameter(cmd, "@Respuesta", DbType.String, entity.LaRespuesta);
                db.AddInParameter(cmd, "@Orden", DbType.Int32, entity.NumeroOrden);
           
                db.ExecuteNonQuery(cmd);
            }
        }

        public List<Orden> listaRespuestaCorrecta(int id_pregunta)
        {
            const string SQL_STATEMENT = "select ID_Respuesta,Respuesta,Orden,ID_Pregunta from Respuesta where Activo=1 and ID_Pregunta=@Id and orden is not Null order by Orden";

            List<Orden> result = new List<Orden>();
            var db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);
            using (DbCommand cmd = db.GetSqlStringCommand(SQL_STATEMENT))
            {
                db.AddInParameter(cmd, "@Id", DbType.Int32, id_pregunta);
                using (IDataReader dr = db.ExecuteReader(cmd))
                {
                    while (dr.Read())
                    {
                        Orden orden = LoadOrdenmin(dr);
                        result.Add(orden);
                    }
                }
            }
            return result;
        }
    }
}

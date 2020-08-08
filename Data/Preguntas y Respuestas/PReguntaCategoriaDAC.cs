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
    public class PReguntaCategoriaDAC : DataAccessComponent, IRepository<PreguntaCategoria>
    {
        private PreguntaCategoria LoadPregunta(IDataReader dr)
        {
            PreguntaCategoria pregunta = new PreguntaCategoria();
           
           pregunta.Pregunta.Id= GetDataValue<int>(dr, "ID_Pregunta");
            pregunta.Pregunta.LaPregunta = GetDataValue<string>(dr, "Pregunta");
            pregunta.Categorias.Id= GetDataValue<int>(dr, "ID_Categoria");
            pregunta.Categorias.LaCategoria = GetDataValue<string>(dr, "Categoria");
            pregunta.TipoPregunta.Id = GetDataValue<int>(dr, "ID_TipoPregunta");
            pregunta.TipoPregunta.TipoDePregunta = GetDataValue<string>(dr, "TipoPregunta");
            return pregunta;
        }
        public PreguntaCategoria Create(PreguntaCategoria entity)
        {
            const string SQL_STATEMENT = "insert into PreguntaCategoria(ID_Categoria,ID_Pregunta)values	(@ID_Categoria,@ID_Pregunta)";
            var db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);
            using (DbCommand cmd = db.GetSqlStringCommand(SQL_STATEMENT))
            {
                db.AddInParameter(cmd, "@ID_Categoria", DbType.Int32, entity.Categorias.Id);
                db.AddInParameter(cmd, "@ID_Pregunta", DbType.Int32, entity.Pregunta.Id);
                db.ExecuteNonQuery(cmd);
            }
            return entity;
        }

        public void Delete(int id)
        {
           
            throw new NotImplementedException();
        }
        public void Delete(PreguntaCategoria preguntaCategoria)
        {
            const string SQL_STATEMENT = "delete from PreguntaCategoria where ID_Categoria=@ID_Categoria and ID_Pregunta=@ID_Pregunta";
            var db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);
            using (DbCommand cmd = db.GetSqlStringCommand(SQL_STATEMENT))
            {
                db.AddInParameter(cmd, "@ID_Categoria", DbType.Int32, preguntaCategoria.Categorias.Id);
                db.AddInParameter(cmd, "@ID_Pregunta", DbType.Int32, preguntaCategoria.Pregunta.Id);
                db.ExecuteNonQuery(cmd);
            }
        }

        /*leo todo*/
        public List<PreguntaCategoria> Read()
        {
            const string SQL_STATEMENT = "select pc.ID_Pregunta,Pregunta,pc.ID_Categoria,Categoria,p.ID_TipoPregunta,tp.TipoPregunta from PreguntaCategoria as pc inner join Pregunta as p on pc.ID_Pregunta=p.ID_Pregunta inner join Categoria as c on pc.ID_Categoria=c.ID_Categoria inner join TipoPregunta as tp on tp.ID_TipoPregunta=p.ID_TipoPregunta  where p.Activo=1 and SubPregunta=0 order by Pregunta";

            List<PreguntaCategoria> result = new List<PreguntaCategoria>();
            var db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);
            using (DbCommand cmd = db.GetSqlStringCommand(SQL_STATEMENT))
            {
                using (IDataReader dr = db.ExecuteReader(cmd))
                {
                    while (dr.Read())
                    {
                        PreguntaCategoria pregunta = LoadPregunta(dr);
                        result.Add(pregunta);
                    }
                }
            }
            return result;
        }

        /*Busco Por categoria de pregunta*/
        public List<PreguntaCategoria> Read(int ID_categoria )
        {
            const string SQL_STATEMENT = "select pc.ID_Pregunta,Pregunta,pc.ID_Categoria,Categoria,p.ID_TipoPregunta,tp.TipoPregunta from PreguntaCategoria as pc inner join Pregunta as p on pc.ID_Pregunta=p.ID_Pregunta inner join Categoria as c on pc.ID_Categoria=c.ID_Categoria inner join TipoPregunta as tp on tp.ID_TipoPregunta=p.ID_TipoPregunta  where p.Activo=1 and pc.ID_Categoria = @id and SubPregunta=0 order by Pregunta";

            List<PreguntaCategoria> result = new List<PreguntaCategoria>();
            var db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);
            using (DbCommand cmd = db.GetSqlStringCommand(SQL_STATEMENT))
            {
                db.AddInParameter(cmd, "@Id", DbType.Int32, ID_categoria);
                using (IDataReader dr = db.ExecuteReader(cmd))
                {
                    while (dr.Read())
                    {
                        PreguntaCategoria pregunta = LoadPregunta(dr);
                        result.Add(pregunta);
                    }
                }
            }
            return result;
        }

        /*Busco Por id Pregunta*/
        public List<PreguntaCategoria> Read(Int16 ID_Pregunta)
        {
            const string SQL_STATEMENT = "select pc.ID_Pregunta,Pregunta,pc.ID_Categoria,Categoria,p.ID_TipoPregunta,tp.TipoPregunta from PreguntaCategoria as pc inner join Pregunta as p on pc.ID_Pregunta=p.ID_Pregunta inner join Categoria as c on pc.ID_Categoria=c.ID_Categoria inner join TipoPregunta as tp on tp.ID_TipoPregunta=p.ID_TipoPregunta  where p.Activo=1 and pc.ID_Pregunta=@id and SubPregunta=0 order by Pregunta";

            List<PreguntaCategoria> result = new List<PreguntaCategoria>();
            var db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);
            using (DbCommand cmd = db.GetSqlStringCommand(SQL_STATEMENT))
            {
                db.AddInParameter(cmd, "@Id", DbType.Int32, ID_Pregunta);
                using (IDataReader dr = db.ExecuteReader(cmd))
                {
                    while (dr.Read())
                    {
                        PreguntaCategoria pregunta = LoadPregunta(dr);
                        result.Add(pregunta);
                    }
                }
            }
            return result;
        }

        /*Busco Por id Pregunta*/
        public List<PreguntaCategoria> Read(String preguntaParcial)
        {
             string SQL_STATEMENT = preguntaParcial;

            List<PreguntaCategoria> result = new List<PreguntaCategoria>();
            var db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);
            using (DbCommand cmd = db.GetSqlStringCommand(SQL_STATEMENT))
            {
                db.AddInParameter(cmd, "@Id", DbType.String, preguntaParcial);
                using (IDataReader dr = db.ExecuteReader(cmd))
                {
                    while (dr.Read())
                    {
                        PreguntaCategoria pregunta = LoadPregunta(dr);
                        result.Add(pregunta);
                    }
                }
            }
            return result;
        }
        public PreguntaCategoria ReadBy(int id)
        {


            const string SQL_STATEMENT = "select pc.ID_Pregunta,Pregunta,pc.ID_Categoria,Categoria,p.ID_TipoPregunta,tp.TipoPregunta from PreguntaCategoria as pc inner join Pregunta as p on pc.ID_Pregunta=p.ID_Pregunta inner join Categoria as c on pc.ID_Categoria=c.ID_Categoria inner join TipoPregunta as tp on tp.ID_TipoPregunta=p.ID_TipoPregunta  where p.Activo=1 and pc.ID_Pregunta=@id and SubPregunta=0 order by Pregunta";

            PreguntaCategoria result = new PreguntaCategoria();
            var db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);
            using (DbCommand cmd = db.GetSqlStringCommand(SQL_STATEMENT))
            {
                db.AddInParameter(cmd, "@Id", DbType.Int32, id);
                using (IDataReader dr = db.ExecuteReader(cmd))
                {
                    if (dr.Read())
                    {
                        result = LoadPregunta(dr);
                      
                    }
                }
            }
            return result;
        }
        public PreguntaCategoria ReadBy(PreguntaCategoria preguntaCategoria)
        {
            const string SQL_STATEMENT = "select pc.ID_Pregunta,Pregunta,pc.ID_Categoria,Categoria,p.ID_TipoPregunta,tp.TipoPregunta from PreguntaCategoria as pc inner join Pregunta as p on pc.ID_Pregunta=p.ID_Pregunta inner join Categoria as c on pc.ID_Categoria=c.ID_Categoria inner join TipoPregunta as tp on tp.ID_TipoPregunta=p.ID_TipoPregunta  where p.Activo=1 and pc.ID_Categoria=@ID_Categoria and pc.ID_Pregunta=@ID_Pregunta and SubPregunta=0";
            PreguntaCategoria pregunta = null;

            var db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);
            using (DbCommand cmd = db.GetSqlStringCommand(SQL_STATEMENT))
            {
                db.AddInParameter(cmd, "@ID_Categoria", DbType.Int32, preguntaCategoria.Categorias.Id);
                db.AddInParameter(cmd, "@ID_Pregunta", DbType.Int32, preguntaCategoria.Pregunta.Id);

                using (IDataReader dr = db.ExecuteReader(cmd))
                {
                    if (dr.Read())
                    {
                        pregunta = LoadPregunta(dr);
                    }
                }
            }
            return pregunta;
            throw new NotImplementedException();
        }

        public void Update(PreguntaCategoria entity)
        {
            throw new NotImplementedException();
        }
    }
}

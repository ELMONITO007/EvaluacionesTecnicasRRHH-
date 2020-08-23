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
    public class PreguntaDAC : DataAccessComponent, IRepository<Pregunta>
    {
        private Pregunta LoadPregunta(IDataReader dr)
        {
            Pregunta pregunta = new Pregunta();
            pregunta.Id = GetDataValue<int>(dr, "ID_Pregunta");
            pregunta.LaPregunta = GetDataValue<string>(dr, "Pregunta");
            pregunta.Imagen = GetDataValue<string>(dr, "Imagen");
            pregunta.nivel.Id = GetDataValue<int>(dr, "ID_Nivel");
            pregunta.nivel.ElNivel = GetDataValue<string>(dr, "Nivel");
          
            pregunta.tipoPregunta.Id = GetDataValue<int>(dr, "ID_TipoPregunta");
            pregunta.tipoPregunta.TipoDePregunta = GetDataValue<string>(dr, "TipoPregunta");

            return pregunta;
        }

        private Pregunta LoadPreguntaCompleto(IDataReader dr)
        {
            Pregunta pregunta = new Pregunta();
            pregunta.Id = GetDataValue<int>(dr, "ID_Pregunta");
            pregunta.LaPregunta = GetDataValue<string>(dr, "Pregunta");
            pregunta.Imagen = GetDataValue<string>(dr, "Imagen");
            pregunta.nivel.Id = GetDataValue<int>(dr, "ID_Nivel");
            pregunta.nivel.ElNivel = GetDataValue<string>(dr, "Nivel");
            pregunta.tipoPregunta.Id = GetDataValue<int>(dr, "ID_Categoria");
            pregunta.tipoPregunta.Id = GetDataValue<int>(dr, "ID_TipoPregunta");
            pregunta.tipoPregunta.TipoDePregunta = GetDataValue<string>(dr, "TipoPregunta");

            return pregunta;
        }

        public Pregunta Create(Pregunta entity)
        {
            try
            {
                const string SQL_STATEMENT = "insert into Pregunta (Pregunta,Imagen,ID_Nivel,ID_TipoPregunta,activo,subpregunta)values(@Pregunta,@Imagen,@ID_Nivel,@ID_TipoPregunta,1,@subpregunta)  insert into PreguntaCategoria(ID_Pregunta, ID_Categoria)values((select ID_Pregunta from Pregunta where Pregunta = @Pregunta),@ID_Categoria)";
                var db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);
                using (DbCommand cmd = db.GetSqlStringCommand(SQL_STATEMENT))
                {
                    db.AddInParameter(cmd, "@Pregunta", DbType.String, entity.LaPregunta);
                    db.AddInParameter(cmd, "@Imagen", DbType.String, entity.Imagen);
                    db.AddInParameter(cmd, "@ID_Nivel", DbType.Int32, entity.nivel.Id);
                    db.AddInParameter(cmd, "@ID_Categoria", DbType.Int32, entity.categoria.Id);
                    db.AddInParameter(cmd, "@subpregunta", DbType.Boolean, entity.SubPregunta);
                    db.AddInParameter(cmd, "@ID_TipoPregunta", DbType.Int32, entity.tipoPregunta.Id);
              
                    db.ExecuteNonQuery(cmd);
                }

            }
            catch (Exception e )
            {

                throw;
            }
            




            return entity;
        }

        public void Delete(int id)
        {
            const string SQL_STATEMENT = "update Pregunta set Activo=0 where Id_pregunta=@Id";
            var db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);
            using (DbCommand cmd = db.GetSqlStringCommand(SQL_STATEMENT))
            {
                db.AddInParameter(cmd, "@Id", DbType.Int32, id);
                db.ExecuteNonQuery(cmd);
            }
        }

        public List<Pregunta> Read()
        {
            const string SQL_STATEMENT = "select Nivel, tp.TipoPregunta,  p.ID_Pregunta,p.Pregunta,p.Imagen,p.ID_Nivel,p.ID_TipoPregunta from pregunta as p inner join TipoPregunta as tp on tp.ID_TipoPregunta =p.ID_TipoPregunta inner join NivelPregunta as np on np.ID_Nivel = p.ID_Nivel where p.activo=1 and SubPregunta=0";

            List<Pregunta> result = new List<Pregunta>();
            var db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);
            using (DbCommand cmd = db.GetSqlStringCommand(SQL_STATEMENT))
            {
                using (IDataReader dr = db.ExecuteReader(cmd))
                {
                    while (dr.Read())
                    {
                        Pregunta pregunta = LoadPregunta(dr);
                        result.Add(pregunta);
                    }
                }
            }
            return result;
        }

        public Pregunta ReadBy(int id)
        {
            const string SQL_STATEMENT = "select Nivel, tp.TipoPregunta, p.ID_Pregunta,p.Pregunta,p.Imagen,p.ID_Nivel,p.ID_TipoPregunta from pregunta as p   inner join TipoPregunta as tp on tp.ID_TipoPregunta =p.ID_TipoPregunta inner join NivelPregunta as np on np.ID_Nivel = p.ID_Nivel  where p.activo=1 and p.ID_Pregunta=@id and SubPregunta=0";
            Pregunta pregunta = null;

            var db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);
            using (DbCommand cmd = db.GetSqlStringCommand(SQL_STATEMENT))
            {
                db.AddInParameter(cmd, "@Id", DbType.Int32, id);
                using (IDataReader dr = db.ExecuteReader(cmd))
                {
                    if (dr.Read())
                    {
                        pregunta = LoadPregunta(dr);
                    }
                }
            }
            return pregunta;
        }
        public Pregunta ReadBySubPregunta(int id)
        {
            const string SQL_STATEMENT = "select Nivel, tp.TipoPregunta, p.ID_Pregunta,p.Pregunta,p.Imagen,p.ID_Nivel,p.ID_TipoPregunta from pregunta as p   inner join TipoPregunta as tp on tp.ID_TipoPregunta =p.ID_TipoPregunta inner join NivelPregunta as np on np.ID_Nivel = p.ID_Nivel  where p.activo=1 and p.ID_Pregunta=@id and SubPregunta=1";
            Pregunta pregunta = null;

            var db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);
            using (DbCommand cmd = db.GetSqlStringCommand(SQL_STATEMENT))
            {
                db.AddInParameter(cmd, "@Id", DbType.Int32, id);
                using (IDataReader dr = db.ExecuteReader(cmd))
                {
                    if (dr.Read())
                    {
                        pregunta = LoadPregunta(dr);
                    }
                }
            }
            return pregunta;
        }
        public Pregunta ReadBySubPregunta(String LaPregunta)
        {
            const string SQL_STATEMENT = "select Nivel, tp.TipoPregunta, p.ID_Pregunta,p.Pregunta,p.Imagen,p.ID_Nivel,p.ID_TipoPregunta from pregunta as p   inner join TipoPregunta as tp on tp.ID_TipoPregunta =p.ID_TipoPregunta inner join NivelPregunta as np on np.ID_Nivel = p.ID_Nivel  where p.activo=1 and p.Pregunta=@id and SubPregunta=1";
            Pregunta pregunta = null;

            var db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);
            using (DbCommand cmd = db.GetSqlStringCommand(SQL_STATEMENT))
            {
                db.AddInParameter(cmd, "@Id", DbType.String, LaPregunta);
                using (IDataReader dr = db.ExecuteReader(cmd))
                {
                    if (dr.Read())
                    {
                        pregunta = LoadPregunta(dr);
                    }
                }
            }
            return pregunta;
        }
        public Pregunta ReadByPregunta(String LaPregunta)
        {
            const string SQL_STATEMENT = "select Nivel, tp.TipoPregunta, p.ID_Pregunta,p.Pregunta,p.Imagen,p.ID_Nivel,p.ID_TipoPregunta from pregunta as p   inner join TipoPregunta as tp on tp.ID_TipoPregunta =p.ID_TipoPregunta inner join NivelPregunta as np on np.ID_Nivel = p.ID_Nivel  where p.activo=1 and p.Pregunta=@id and SubPregunta=0";
            Pregunta pregunta = null;

            var db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);
            using (DbCommand cmd = db.GetSqlStringCommand(SQL_STATEMENT))
            {
                db.AddInParameter(cmd, "@Id", DbType.String, LaPregunta);
                using (IDataReader dr = db.ExecuteReader(cmd))
                {
                    if (dr.Read())
                    {
                        pregunta = LoadPregunta(dr);
                    }
                }
            }
            return pregunta;
        }
        public void Update(Pregunta entity)
        {
            const string SQL_STATEMENT = "update Pregunta set Pregunta=@Pregunta, Imagen=@Imagen,ID_Nivel=@ID_Nivel,ID_TipoPregunta=@ID_TipoPregunta  where ID_Pregunta=@Id ";

            var db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);
            using (DbCommand cmd = db.GetSqlStringCommand(SQL_STATEMENT))
            {
                db.AddInParameter(cmd, "@Id", DbType.Int32, entity.Id);
                db.AddInParameter(cmd, "@Pregunta", DbType.String, entity.LaPregunta);
                db.AddInParameter(cmd, "@Imagen", DbType.String, entity.Imagen);
                db.AddInParameter(cmd, "@ID_Nivel", DbType.Int32, entity.nivel.Id);
                  
                db.AddInParameter(cmd, "@ID_TipoPregunta", DbType.String, entity.tipoPregunta.Id);
                db.ExecuteNonQuery(cmd);
            }
        }

        public List<Pregunta> ObtenerPreguntarAlAzarPorNivelYCategoria(Pregunta pregunta)
        {
           
             string SQL_STATEMENT = "select np.Nivel,  p.ID_Pregunta,SubPregunta,ID_Categoria,p.Pregunta,p.Imagen,p.ID_Nivel,p.ID_TipoPregunta,tp.TipoPregunta  from Pregunta as p inner join PreguntaCategoria as pc on p.ID_Pregunta=pc.ID_Pregunta inner join TipoPregunta as tp on tp.ID_TipoPregunta =p.ID_TipoPregunta inner join NivelPregunta as np on p.ID_Nivel=np.ID_Nivel  where  p.Activo=1 and pc.ID_Categoria=@ID_categoria and Subpregunta=0 order by NEWID()";

            List<Pregunta> result = new List<Pregunta>();
           
            var db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);
            using (DbCommand cmd = db.GetSqlStringCommand(SQL_STATEMENT))
            {
              
              
                db.AddInParameter(cmd, "@ID_categoria", DbType.Int32, pregunta.categoria.Id);

                using (IDataReader dr = db.ExecuteReader(cmd))
                {
                    while (dr.Read())
                    {
                        Pregunta pr = LoadPreguntaCompleto(dr);
                        result.Add(pr);
                    }
                }
            }
            return result;

        }
        public List<Pregunta> LeerPorTipoDePregunta(int id)
        {
            const string SQL_STATEMENT = "select Nivel, tp.TipoPregunta, p.ID_Pregunta,p.Pregunta,p.Imagen,p.ID_Nivel,p.ID_TipoPregunta from pregunta as p inner join PreguntaCategoria as pc on p.ID_Pregunta=pc.ID_Pregunta inner join Categoria as c on c.ID_Categoria=pc.ID_Categoria inner join TipoPregunta as tp on tp.ID_TipoPregunta =p.ID_TipoPregunta inner join NivelPregunta as np on np.ID_Nivel = p.ID_Nivel where p.activo=1 and p.ID_TipoPregunta=@ID_TipoPregunta";

            List<Pregunta> result = new List<Pregunta>();
            var db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);
            using (DbCommand cmd = db.GetSqlStringCommand(SQL_STATEMENT))
            {
                db.AddInParameter(cmd, "@ID_TipoPregunta", DbType.Int32, id);
                using (IDataReader dr = db.ExecuteReader(cmd))
                {
                    
                    while (dr.Read())
                    {
                        Pregunta pregunta = LoadPregunta(dr);
                        result.Add(pregunta);
                    }
                }
            }
            return result;
        }

        public bool ReadBy(string pregunta)
        {
            const string SQL_STATEMENT = "select *  from Pregunta where Pregunta=@Pregunta";
      

            var db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);
            using (DbCommand cmd = db.GetSqlStringCommand(SQL_STATEMENT))
            {
                db.AddInParameter(cmd, "@Pregunta", DbType.String, pregunta);
                using (IDataReader dr = db.ExecuteReader(cmd))
                {
                    if (dr.Read())
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }

        }
    }
}

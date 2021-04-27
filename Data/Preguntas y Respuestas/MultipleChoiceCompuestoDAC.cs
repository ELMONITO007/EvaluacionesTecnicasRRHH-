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
  public  class MultipleChoiceCompuestoDAC : DataAccessComponent, IRepository<MultipleChoiceCompuesto>
    {
        private MultipleChoiceCompuesto LoadMultipleChoice(IDataReader dr)
        {
            MultipleChoiceCompuesto multipleChoice = new MultipleChoiceCompuesto();
            multipleChoice.Id = GetDataValue<int>(dr, "ID_Pregunta");

           multipleChoice.id_SubPregunta= GetDataValue<int>(dr, "ID_SubPregunta");
            return multipleChoice;
        }

        private MultipleChoiceCompuesto LoadMultipleChoiceRespuesta(IDataReader dr)
        {
            MultipleChoiceCompuesto multipleChoice = new MultipleChoiceCompuesto();
            multipleChoice.Correcta = GetDataValue<bool>(dr, "Correcta");
            multipleChoice.LaRespuesta= GetDataValue<string>(dr, "Respuesta");
            multipleChoice.Pregunta.Id = GetDataValue<int>(dr, "ID_Pregunta");
            multipleChoice.Id = GetDataValue<int>(dr, "ID_Respuesta");
            return multipleChoice;
        }

        public MultipleChoiceCompuesto Create(MultipleChoiceCompuesto entity)
        {
            const string SQL_STATEMENT = "insert into Respuesta(Respuesta,Correcta,ID_Pregunta,Activo ) values (@Respuesta,@Correcta,@ID_Pregunta,1)";
            var db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);
            using (DbCommand cmd = db.GetSqlStringCommand(SQL_STATEMENT))
            {
                db.AddInParameter(cmd, "@Respuesta", DbType.String, entity.LaRespuesta);
                db.AddInParameter(cmd, "@Correcta", DbType.Boolean, entity.Correcta);
                db.AddInParameter(cmd, "@ID_Pregunta", DbType.Int32, entity.pregunta.Id);
                entity.Id = Convert.ToInt32(db.ExecuteScalar(cmd));
            }
            return entity;
        }

        public void Delete(int id)
        {
            const string SQL_STATEMENT = "update Respuesta set Activo=0 where ID_Respuesta=@id ";

            var db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);
            using (DbCommand cmd = db.GetSqlStringCommand(SQL_STATEMENT))
            {
                db.AddInParameter(cmd, "@Id", DbType.Int32, id);
                
                db.ExecuteNonQuery(cmd);
            }
        }

        public List<MultipleChoiceCompuesto> Read()
        {
            const string SQL_STATEMENT = "select sp.ID_Pregunta,sp.ID_SubPregunta from SubPregunta as sp inner join pregunta as p on p.ID_Pregunta = sp.ID_SubPregunta where sp.ID_Pregunta=4 and Activo=1";

            List<MultipleChoiceCompuesto> result = new List<MultipleChoiceCompuesto>();
            var db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);
            using (DbCommand cmd = db.GetSqlStringCommand(SQL_STATEMENT))
            {
                using (IDataReader dr = db.ExecuteReader(cmd))
                {
                    while (dr.Read())
                    {
                        MultipleChoiceCompuesto multipleChoice = LoadMultipleChoice(dr);
                        result.Add(multipleChoice);
                    }
                }
            }
            return result;
        }

        public List<MultipleChoiceCompuesto> ReadBy(int id)
        {
            const string SQL_STATEMENT = "select sp.ID_Pregunta,sp.ID_SubPregunta from SubPregunta as sp inner join pregunta as p on p.ID_Pregunta = sp.ID_SubPregunta where   Activo=1 and sp.ID_Pregunta=@Id ";

            List<MultipleChoiceCompuesto> result = new List<MultipleChoiceCompuesto>();

            var db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);
            using (DbCommand cmd = db.GetSqlStringCommand(SQL_STATEMENT))
            {
                db.AddInParameter(cmd, "@Id", DbType.Int32, id);
                using (IDataReader dr = db.ExecuteReader(cmd))
                {
                    while (dr.Read())
                    {
                        MultipleChoiceCompuesto multipleChoice = LoadMultipleChoice(dr);
                        result.Add(multipleChoice);
                    }
                }
            }
            return result;
        }

      public MultipleChoiceCompuesto ReadByRespuesta(int Id)
        {
            const string SQL_STATEMENT = "select DISTINCT ID_Respuesta,Respuesta,Correcta,ID_Pregunta from Respuesta where Activo=1 and ID_Respuesta=@id and Correcta is not null";
            MultipleChoiceCompuesto categoria = null;

            var db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);
            using (DbCommand cmd = db.GetSqlStringCommand(SQL_STATEMENT))
            {
                db.AddInParameter(cmd, "@Id", DbType.Int32, Id);
                using (IDataReader dr = db.ExecuteReader(cmd))
                {
                    if (dr.Read())
                    {
                        categoria = LoadMultipleChoiceRespuesta(dr);
                    }
                }
            }
            return categoria;

        }
        public MultipleChoiceCompuesto ObtenerPreguntaaDeUnaSubpregunta(int Id)
        {
            const string SQL_STATEMENT = "select sp.ID_Pregunta,sp.ID_SubPregunta from SubPregunta as sp inner join pregunta as p on p.ID_Pregunta = sp.ID_SubPregunta where   Activo=1 and sp.ID_SubPregunta=@Id";
            MultipleChoiceCompuesto categoria = null;

            var db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);
            using (DbCommand cmd = db.GetSqlStringCommand(SQL_STATEMENT))
            {
                db.AddInParameter(cmd, "@Id", DbType.Int32, Id);
                using (IDataReader dr = db.ExecuteReader(cmd))
                {
                    if (dr.Read())
                    {
                        categoria = LoadMultipleChoice(dr);
                    }
                }
            }
            return categoria;

        }
        public void Update(MultipleChoiceCompuesto entity)
        {
            const string SQL_STATEMENT = "update Respuesta set Respuesta=@Respuesta,Correcta=@Correcta where ID_Respuesta=@Id ";

            var db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);
            using (DbCommand cmd = db.GetSqlStringCommand(SQL_STATEMENT))
            {
                db.AddInParameter(cmd, "@Id", DbType.Int32, entity.Id);
                db.AddInParameter(cmd, "@Respuesta", DbType.String, entity.LaRespuesta);
                db.AddInParameter(cmd, "@Correcta", DbType.Boolean, entity.Correcta);
                db.ExecuteNonQuery(cmd);
            }
        }

  MultipleChoiceCompuesto IRepository<MultipleChoiceCompuesto>.ReadBy(int id)
        {
            throw new NotImplementedException();
        }
    }

   
}

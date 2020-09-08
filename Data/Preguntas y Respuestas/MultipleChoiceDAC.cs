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
    public class MultipleChoiceDAC : DataAccessComponent, IRepository<MultipleChoice>
    {
        private MultipleChoice LoadMultipleChoice(IDataReader dr)
        {
            MultipleChoice multipleChoice = new MultipleChoice();
            multipleChoice.Id = GetDataValue<int>(dr, "ID_Respuesta");
            multipleChoice.Correcta = GetDataValue<bool>(dr, "Correcta");
            multipleChoice.LaRespuesta = GetDataValue<string>(dr, "Respuesta");
            multipleChoice.pregunta.Id = GetDataValue<int>(dr, "ID_Pregunta");
            return multipleChoice;
        }

        public MultipleChoice Create(MultipleChoice entity)
        {
            const string SQL_STATEMENT = "insert into Respuesta(Respuesta,Correcta,ID_Pregunta,Activo)values(@Respuesta,@Correcta,@ID_Pregunta,1)";
            var db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);
            using (DbCommand cmd = db.GetSqlStringCommand(SQL_STATEMENT))
            {

                db.AddInParameter(cmd, "@Respuesta", DbType.String, entity.LaRespuesta);
                db.AddInParameter(cmd, "@Orden", DbType.Boolean, entity.Correcta);
                db.AddInParameter(cmd, "@ID_Pregunta", DbType.Int32, entity.pregunta.Id);
                db.AddInParameter(cmd, "@Correcta", DbType.Boolean, entity.Correcta);

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

        public List<MultipleChoice> Read()
        {
            const string SQL_STATEMENT = "Select ID_Respuesta,Respuesta,Correcta,ID_Pregunta from Respuesta where Activo=1 and Correcta is not null";

            List<MultipleChoice> result = new List<MultipleChoice>();
            var db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);
            using (DbCommand cmd = db.GetSqlStringCommand(SQL_STATEMENT))
            {
                using (IDataReader dr = db.ExecuteReader(cmd))
                {
                    while (dr.Read())
                    {
                        MultipleChoice multipleChoice = LoadMultipleChoice(dr);
                        result.Add(multipleChoice);
                    }
                }
            }
            return result;
        }

        public MultipleChoice ReadBy(int id)
        {
            const string SQL_STATEMENT = "select ID_Respuesta,Respuesta,Correcta,ID_Pregunta from Respuesta where Activo=1 and ID_Respuesta=@id and Correcta is not null";
            MultipleChoice multipleChoice = null;

            var db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);
            using (DbCommand cmd = db.GetSqlStringCommand(SQL_STATEMENT))
            {
                db.AddInParameter(cmd, "@Id", DbType.Int32, id);
                using (IDataReader dr = db.ExecuteReader(cmd))
                {
                    if (dr.Read())
                    {
                        multipleChoice = LoadMultipleChoice(dr);
                    }
                }
            }
            return multipleChoice;
        }

        public void Update(MultipleChoice entity)
        {
            const string SQL_STATEMENT = "update Respuesta set Respuesta=@Respuesta,Correcta=@Correcta,ID_Pregunta=@ID_Pregunta where ID_Respuesta=@Id ";

            var db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);
            using (DbCommand cmd = db.GetSqlStringCommand(SQL_STATEMENT))
            {
                db.AddInParameter(cmd, "@Id", DbType.Int32, entity.Id);
                db.AddInParameter(cmd, "@Respuesta", DbType.String, entity.LaRespuesta);
                db.AddInParameter(cmd, "@Correcta", DbType.Boolean, entity.Correcta);
                db.AddInParameter(cmd, "@ID_Pregunta", DbType.Int32, entity.pregunta.Id);
                db.ExecuteNonQuery(cmd);
            }
        }

        public List<MultipleChoice> listaRespuestaMultipleChoiceAlAzar(int id_pregunta)
        {
            const string SQL_STATEMENT = "select ID_Respuesta,Respuesta,Correcta,ID_Pregunta from Respuesta where Activo=1 and ID_Pregunta=@Id and correcta is not null  order by NEWID()";

            List<MultipleChoice> result = new List<MultipleChoice>();
            var db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);
            using (DbCommand cmd = db.GetSqlStringCommand(SQL_STATEMENT))
            {
                db.AddInParameter(cmd, "@Id", DbType.Int32, id_pregunta);
                using (IDataReader dr = db.ExecuteReader(cmd))
                {
                    while (dr.Read())
                    {
                        MultipleChoice multiple = LoadMultipleChoice(dr);
                        result.Add(multiple);
                    }
                }
            }
            return result;
        }

        public List< MultipleChoice> ObtenerRespuestaCorrecta(int id)
        {
            const string SQL_STATEMENT = "select ID_Respuesta,Respuesta,Correcta,ID_Pregunta from Respuesta where Activo=1 and Correcta=1  and ID_Pregunta=@Id and Correcta is not null ";
          

            List<MultipleChoice> result = new List<MultipleChoice>();
            var db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);
            using (DbCommand cmd = db.GetSqlStringCommand(SQL_STATEMENT))
            {
           
                db.AddInParameter(cmd, "@Id", DbType.Int32, id);
                using (IDataReader dr = db.ExecuteReader(cmd))
                {
                    while (dr.Read())
                    {
                        MultipleChoice multipleChoice = LoadMultipleChoice(dr);
                        result.Add(multipleChoice);
                    }
                }
            }
            return result;
        }
        public MultipleChoice ObtenerLaRespuestaCorrecta(int id)
        {
            const string SQL_STATEMENT = "select ID_Respuesta,Respuesta,Correcta,ID_Pregunta from Respuesta where Activo=1  and ID_Respuesta=@Id and Correcta is not null ";


            MultipleChoice result = new MultipleChoice();
            var db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);
            using (DbCommand cmd = db.GetSqlStringCommand(SQL_STATEMENT))
            {

                db.AddInParameter(cmd, "@Id", DbType.Int32, id);
                using (IDataReader dr = db.ExecuteReader(cmd))
                {
                    if (dr.Read())
                    {
                        result = LoadMultipleChoice(dr);
                     
                    }
                }
            }
            return result;
        }

    }
}

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
    public class SubPreguntaDAC : DataAccessComponent, IRepository<SubPregunta>
    {
        private SubPregunta LoadSubPregunta(IDataReader dr)
        {
            SubPregunta subPregunta = new SubPregunta();
            subPregunta.subPregunta.Id = GetDataValue<int>(dr, "ID_SubPregunta");
            subPregunta.pregunta.Id = GetDataValue<int>(dr, "ID_Pregunta");
            return subPregunta;
        }

        public SubPregunta Create(SubPregunta entity)
        {
            try
            {
                const string SQL_STATEMENT = "insert into SubPregunta(ID_Pregunta,ID_SubPregunta)values(@ID_Pregunta,@ID_SubPregunta)";
                var db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);
                using (DbCommand cmd = db.GetSqlStringCommand(SQL_STATEMENT))
                {
                    db.AddInParameter(cmd, "@ID_SubPregunta", DbType.Int32, entity.subPregunta.Id);
                    db.AddInParameter(cmd, "@ID_Pregunta", DbType.Int32, entity.pregunta.Id);

                    db.ExecuteNonQuery(cmd);
                }

            }
            catch (Exception e)
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

        public List<SubPregunta> Read()
        {
            throw new NotImplementedException();
        }

        public SubPregunta ReadBy(int id)
        {


            const string SQL_STATEMENT = "select ID_TipoPregunta,TipoPregunta from TipoPregunta  where activo=1 and ID_TipoPregunta=@Id";
            SubPregunta tipoPregunta = null;

            var db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);
            using (DbCommand cmd = db.GetSqlStringCommand(SQL_STATEMENT))
            {
                db.AddInParameter(cmd, "@Id", DbType.Int32, id);
                using (IDataReader dr = db.ExecuteReader(cmd))
                {
                    if (dr.Read())
                    {
                        tipoPregunta = LoadSubPregunta(dr);
                    }
                }
            }
            return tipoPregunta;
        }

        public void Update(SubPregunta entity)
        {
            const string SQL_STATEMENT = "update Pregunta set Pregunta=@Pregunta where ID_Pregunta=@Id ";

            var db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);
            using (DbCommand cmd = db.GetSqlStringCommand(SQL_STATEMENT))
            {
                db.AddInParameter(cmd, "@Id", DbType.Int32, entity.pregunta.Id);
                db.AddInParameter(cmd, "@Pregunta", DbType.String, entity.pregunta.LaPregunta);

               
                db.ExecuteNonQuery(cmd);
            }
        }
    }
}

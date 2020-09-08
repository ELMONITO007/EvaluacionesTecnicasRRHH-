using Entities.Examen;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Examen
{
    public class ExamenPreguntaDAC : DataAccessComponent, IRepository<ExamenPregunta>
    {
        private ExamenPregunta LoadCategoria(IDataReader dr)
        {
            ExamenPregunta examenPregunta = new ExamenPregunta();
            examenPregunta.pregunta.Id = GetDataValue<int>(dr, "ID_pregunta");
            examenPregunta.examen.Id = GetDataValue<int>(dr, "ID_examen");
            examenPregunta.correcta= GetDataValue<bool>(dr, "correcta");
            return examenPregunta;
        }
        public ExamenPregunta Create(ExamenPregunta entity)
        {
            const string SQL_STATEMENT = "insert into examenpregunta(ID_pregunta,ID_examen,correcta)values(@ID_pregunta,@ID_examen,@correcta) ";
            var db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);
            using (DbCommand cmd = db.GetSqlStringCommand(SQL_STATEMENT))
            {
                db.AddInParameter(cmd, "@ID_pregunta", DbType.String, entity.pregunta.Id);
                db.AddInParameter(cmd, "@ID_examen", DbType.String, entity.examen.Id);
                db.AddInParameter(cmd, "@correcta", DbType.Boolean, entity.correcta);
                entity.Id = Convert.ToInt32(db.ExecuteScalar(cmd));
            }
            return entity;
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<ExamenPregunta> Read()
        {
            throw new NotImplementedException();
        }
        public List<ExamenPregunta> ReadByExamen(int Id_Examen)
        {


            const string SQL_STATEMENT = "select * from examenPregunta where Id_Examen=@ID_examen";

            List<ExamenPregunta> result = new List<ExamenPregunta>();
            var db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);
            using (DbCommand cmd = db.GetSqlStringCommand(SQL_STATEMENT))
            {
                db.AddInParameter(cmd, "@ID_examen", DbType.Int32, Id_Examen);
                using (IDataReader dr = db.ExecuteReader(cmd))
                {
                    while (dr.Read())
                    {
                        ExamenPregunta examen = LoadCategoria(dr);
                        result.Add(examen);
                    }
                }
            }
            return result;
        }
        public ExamenPregunta ReadBy(int id)
        {
            throw new NotImplementedException();
        }



        public void Update(ExamenPregunta entity)
        {
            const string SQL_STATEMENT = "update ExamenPregunta set Correcta=@Correcta where ID_Examen=@ID_Examen and ID_Pregunta=@ID_Pregunta";
            var db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);
            using (DbCommand cmd = db.GetSqlStringCommand(SQL_STATEMENT))
            {
                db.AddInParameter(cmd, "@ID_pregunta", DbType.String, entity.pregunta.Id);
                db.AddInParameter(cmd, "@ID_examen", DbType.String, entity.examen.Id);
                db.AddInParameter(cmd, "@correcta", DbType.Boolean, entity.correcta);
                entity.Id = Convert.ToInt32(db.ExecuteScalar(cmd));
            }
        }
    }
}

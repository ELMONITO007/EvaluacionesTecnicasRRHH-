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
    public class ExamenRespuestaDAC : DataAccessComponent, IRepository<ExamenRespuesta>
    {

        private ExamenRespuesta LoadExamenRespuesta(IDataReader dr)
        {
            ExamenRespuesta examenRespuesta = new ExamenRespuesta();
            examenRespuesta.examen.Id = GetDataValue<int>(dr, "Id_Examen");
            examenRespuesta.respuesta.Id = GetDataValue<int>(dr, "Id_Respuesta");

            examenRespuesta.pregunta.Id = GetDataValue<int>(dr, "Id_Pregunta");
            examenRespuesta.subPregunta.Id = GetDataValue<int>(dr, "Id_SubPregunta");
            examenRespuesta.correcta = GetDataValue<bool>(dr, "Correcta");
            examenRespuesta.respondio = GetDataValue<bool>(dr, "Respondio");
            return examenRespuesta;
        }

        public ExamenRespuesta Create(ExamenRespuesta entity)
        {

            const string SQL_STATEMENT = "insert into ExamenRespuestas(Id_Examen,Id_Respuesta,Id_Pregunta,Id_SubPregunta,Correcta,Respondio)values(@Id_Examen,@Id_Respuesta,@Id_Pregunta,@Id_SubPregunta,@Correcta,@Respondio) ";
            var db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);
            using (DbCommand cmd = db.GetSqlStringCommand(SQL_STATEMENT))
            {
                db.AddInParameter(cmd, "@Id_Examen", DbType.Int32, entity.examen.Id);
                db.AddInParameter(cmd, "@Id_Respuesta", DbType.Int32, entity.respuesta.Id);
                db.AddInParameter(cmd, "@Id_Pregunta", DbType.Int32, entity.pregunta.Id);
                db.AddInParameter(cmd, "@Id_SubPregunta", DbType.Int32, entity.subPregunta.Id);
                db.AddInParameter(cmd, "@Correcta", DbType.Boolean, entity.correcta);
                db.AddInParameter(cmd, "@Respondio", DbType.Boolean, entity.respondio );
                entity.Id = Convert.ToInt32(db.ExecuteScalar(cmd));
            }
            return entity;
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
        public List<ExamenRespuesta> ReadByExamen(int id)
        {
            const string SQL_STATEMENT = "select * from examenRespuestas where Id_Examen=@Id";

            List<Entities.Examen.ExamenRespuesta> result = new List<Entities.Examen.ExamenRespuesta>();
            var db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);
            using (DbCommand cmd = db.GetSqlStringCommand(SQL_STATEMENT))
            {
                db.AddInParameter(cmd, "@Id", DbType.Int32, id);
                using (IDataReader dr = db.ExecuteReader(cmd))
                {
                    while (dr.Read())
                    {
                        Entities.Examen.ExamenRespuesta examen = LoadExamenRespuesta(dr);
                        result.Add(examen);
                    }
                }
            }
            return result;
        }
        public List<ExamenRespuesta> Read()
        {
            throw new NotImplementedException();
        }

        public ExamenRespuesta ReadBy(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(ExamenRespuesta entity)
        {
            throw new NotImplementedException();
        }
    }
}

using Microsoft.Practices.EnterpriseLibrary.Data;
using Entities.Servicios.Bitacora;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Servicios
{
    public class BitacoraDAC : DataAccessComponent, IRepository<Bitacora>
    {
        private Bitacora LoadBitacora(IDataReader dr)
        {
            Bitacora bitacora = new Bitacora();
            bitacora.Id = GetDataValue<int>(dr, "ID_Bitacora");
            bitacora.usuarios.Id = GetDataValue<int>(dr, "Id");
            bitacora.eventoBitacora.Id = GetDataValue<int>(dr, "ID_EventoBitacora");
            bitacora.fecha = GetDataValue<DateTime>(dr, "Fecha");
            bitacora.hora= GetDataValue<string>(dr, "Hora");

            return bitacora;
        }
        public Bitacora Create(Bitacora entity)
        {
            const string SQL_STATEMENT = "insert into Bitacora(Id,ID_EventoBitacora,Fecha,Hora)values(@id,@ID_EventoBitacora,@Fecha,@Hora) ";
            var db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);
            using (DbCommand cmd = db.GetSqlStringCommand(SQL_STATEMENT))
            {
                db.AddInParameter(cmd, "@id", DbType.Int32, entity.usuarios.Id);
                db.AddInParameter(cmd, "@ID_EventoBitacora", DbType.Int32, entity.eventoBitacora.Id);

                db.AddInParameter(cmd, "@Fecha", DbType.Date, entity.fecha);
                db.AddInParameter(cmd, "@Hora", DbType.Time, entity.hora);
                entity.Id = Convert.ToInt32(db.ExecuteScalar(cmd));
            }
            return entity;
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<Bitacora> Read()
        {
            const string SQL_STATEMENT = "select * from Bitacora";

            List<Bitacora> result = new List<Bitacora>();
            var db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);
            using (DbCommand cmd = db.GetSqlStringCommand(SQL_STATEMENT))
            {
                using (IDataReader dr = db.ExecuteReader(cmd))
                {
                    while (dr.Read())
                    {
                        Bitacora tipoPregunta = LoadBitacora(dr);
                        result.Add(tipoPregunta);
                    }
                }
            }
            return result;
        }

        public Bitacora ReadBy(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Bitacora entity)
        {
            throw new NotImplementedException();
        }
    }
}

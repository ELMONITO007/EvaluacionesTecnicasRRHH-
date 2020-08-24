using Entities;
using Entities.Servicios.Bitacora;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Servicios
{
    public class EventoBitacoraDAC : DataAccessComponent, IRepository<EventoBitacora>
    {
        public EventoBitacora Create(EventoBitacora entity)
        {
            const string SQL_STATEMENT = "insert into EventoBitacora(EventoBitacora)values(@EventoBitacora,)";
            var db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);
            using (DbCommand cmd = db.GetSqlStringCommand(SQL_STATEMENT))
            {
                db.AddInParameter(cmd, "@EventoBitacora", DbType.String, entity.eventoBitacora);
             
                db.ExecuteNonQuery(cmd);

            }

            return entity;
        }

        public void Delete(int id)
        {

            throw new NotImplementedException();
        }

        public List<EventoBitacora> Read()
        {
            const string SQL_STATEMENT = "select * from EventoBitacora ";

            List<EventoBitacora> result = new List<EventoBitacora>();
            var db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);
            using (DbCommand cmd = db.GetSqlStringCommand(SQL_STATEMENT))
            {
                using (IDataReader dr = db.ExecuteReader(cmd))
                {
                    while (dr.Read())
                    {
                        EventoBitacora eventoBitacora = LoadEventoBitacoraD(dr);
                        result.Add(eventoBitacora);
                    }
                }
            }
            return result;
        }

        public EventoBitacora ReadBy(int id)
        {
            const string SQL_STATEMENT = "select *from EventoBitacora  where ID_EventoBitacora=@Id";
            EventoBitacora eventoBitacora = null;

            var db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);
            using (DbCommand cmd = db.GetSqlStringCommand(SQL_STATEMENT))
            {
                db.AddInParameter(cmd, "@Id", DbType.Int32, id);
                using (IDataReader dr = db.ExecuteReader(cmd))
                {
                    if (dr.Read())
                    {
                        eventoBitacora = LoadEventoBitacoraD(dr);
                    }
                }
            }
            return eventoBitacora;
        }

        public void Update(EventoBitacora entity)
        {
            throw new NotImplementedException();
        }

        private EventoBitacora LoadEventoBitacoraD(IDataReader dr)
        {
            EventoBitacora eventoBitacora = new EventoBitacora();
            eventoBitacora.Id = GetDataValue<int>(dr, "ID_EventoBitacora");
            eventoBitacora.eventoBitacora = GetDataValue<string>(dr, "EventoBitacora");
          
            return eventoBitacora;
        }
    
    }
}

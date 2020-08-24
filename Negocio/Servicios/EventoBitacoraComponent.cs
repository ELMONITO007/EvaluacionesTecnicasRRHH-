using Data.Servicios;
using Entities.Servicios.Bitacora;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class EventoBitacoraComponent : IRepository<EventoBitacora>
    {
        public bool VerificaEvento(EventoBitacora entity)

        {
            EventoBitacoraDAC eventoBitacoraDAC = new EventoBitacoraDAC();
            EventoBitacora evento = new EventoBitacora();
            evento = eventoBitacoraDAC.ReadBy(entity.eventoBitacora);


            if (evento is null)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public EventoBitacora Create(EventoBitacora entity)
        {
            if (VerificaEvento(entity))
            {
                EventoBitacoraDAC eventoBitacoraDAC = new EventoBitacoraDAC();
                return eventoBitacoraDAC.Create(entity);
            }
            else
            {
                return null;
            }
           
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<EventoBitacora> Read()
        {
            EventoBitacoraDAC eventoBitacoraDAC = new EventoBitacoraDAC();
            return eventoBitacoraDAC.Read();
        }

        public EventoBitacora ReadBy(int id)
        {
            EventoBitacoraDAC eventoBitacoraDAC = new EventoBitacoraDAC();
            return eventoBitacoraDAC.ReadBy(id);
        }

        public void Update(EventoBitacora entity)
        {
            throw new NotImplementedException();
        }
    }
}

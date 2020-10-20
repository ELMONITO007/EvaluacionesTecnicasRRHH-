using Data;
using Data.Servicios;
using Entities;
using Entities.Servicios.Bitacora;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Servicios
{
    public class BitacoraComponent : IRepository<Bitacora>
    {
        public Bitacora Create(Bitacora entity)
        {
            Bitacora bitacora = new Bitacora();
            bitacora = entity;
            bitacora.fecha = DateTime.Now.ToString("dd/MM/yyyy");
            bitacora.hora = DateTime.Now.ToString("HH:mm");
            UsuariosComponent usuariosComponent = new UsuariosComponent();
            Usuarios usuarios = new Usuarios();
       
            usuarios = usuariosComponent.ReadByEmail(entity.usuarios.Email);
            bitacora.usuarios = usuarios;
            BitacoraDAC bitacoraDAC = new BitacoraDAC();

            return bitacoraDAC.Create(bitacora);
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<Bitacora> Read()
        {
            BitacoraDAC bitacoraDAC = new BitacoraDAC();
            List<Bitacora> bitacoras = new List<Bitacora>();
            foreach (Bitacora item in bitacoraDAC.Read())
            {
                EventoBitacora eventoBitacora = new EventoBitacora();
                EventoBitacoraDAC eventoBitacoraDAC = new EventoBitacoraDAC();
                eventoBitacora = eventoBitacoraDAC.ReadBy(item.eventoBitacora.Id);
                item.eventoBitacora = eventoBitacora;
                UsuarioDac usuarioDac = new UsuarioDac();
                Usuarios usuarios = new Usuarios();
                usuarios=usuarioDac.ReadBy(item.usuarios.Id);
                item.usuarios = usuarios;


                bitacoras.Add(item);

            }

            return bitacoras;
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

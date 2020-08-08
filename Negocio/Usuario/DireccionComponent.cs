using Data;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class DireccionComponent : Component<Direccion>
    {
        public override Direccion Create(Direccion objeto)
        {
            Direccion result = default(Direccion);
            var direccionDAC = new DireccionDAC();

            result = direccionDAC.Create(objeto);
            return result;
        }

        public override void Delete(int id)
        {
            var direccionDAC = new DireccionDAC();

         direccionDAC.Delete(id);
        }

        public override List<Direccion> Read()
        {
            List<Direccion> result = default(List<Direccion>);
            var direccionDAC = new DireccionDAC();
            result = direccionDAC.Read();
            return result;

        }

        public override Direccion ReadBy(int id)
        {
            Direccion result = default(Direccion);
            var direccionDAC = new DireccionDAC();

            result = direccionDAC.ReadBy(id);
            return result;
        }

        public override void Update(Direccion objeto)
        {
            var direccionDAC = new DireccionDAC();

            direccionDAC.Update(objeto);
        }

        public bool Verificar(Direccion direccion)
        {
            Direccion result = default(Direccion);
            var direccionDAC = new DireccionDAC();

            result = direccionDAC.ReadBy(direccion.direccion);
            if (result is null)
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

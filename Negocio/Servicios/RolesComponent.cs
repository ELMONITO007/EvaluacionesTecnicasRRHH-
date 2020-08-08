using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using Entities;


namespace Negocio
{
    public class RolesComponent : Component<Roles>
    {
        public override Roles Create(Roles objeto)
        {
            Roles result = default(Roles);
            var roles = new RolesDAC();

            result = roles.Create(objeto);
            return result;
        }

        public override void Delete(int id)
        {
            var roles = new RolesDAC();
            roles.Delete(id.ToString());
        }

        public override List<Roles> Read()
        {
            List<Roles> result = default(List<Roles>);
            var roles = new RolesDAC();
            result = roles.Read();
            return result;
        }

        public override Roles ReadBy(int id)
        {
            Roles result = default(Roles);
            var roles = new RolesDAC();

            result = roles.ReadBy(id.ToString());
            return result;
        }

        public override void Update(Roles objeto)
        {
         
            var roles = new RolesDAC();

          roles.Update(objeto);
          
        }
    }
}

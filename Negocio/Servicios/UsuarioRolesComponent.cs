using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using Entities;
namespace Negocio
{
    public class UsuarioRolesComponent : Component<UsuarioRoles>
    {
        public override UsuarioRoles Create(UsuarioRoles objeto)
        {
            UsuarioRoles result = default(UsuarioRoles);
            var  usuarioRolesDAC= new UsuarioRolesDAC();

            result = usuarioRolesDAC.Create(objeto);
            return result;
        }

        public override void Delete(int id)
        {
            throw new NotImplementedException();
        }
        public  void Delete(UsuarioRoles objeto)
        {
            var usuarioRolesDAC = new UsuarioRolesDAC();
            usuarioRolesDAC.Delete(objeto);
        }

        public override List<UsuarioRoles> Read()
        {

            List<UsuarioRoles> result = default(List<UsuarioRoles>);
            var usuarioRolesDAC = new UsuarioRolesDAC();
            result = usuarioRolesDAC.Read();
            return result;
        }

        public override UsuarioRoles ReadBy(int id)
        {
            throw new NotImplementedException();
        }
        public  UsuarioRoles ReadBy(string id_role, int id_Usuario)
        {
            UsuarioRoles result = default(UsuarioRoles);
            var usuarioRolesDAC = new UsuarioRolesDAC();
            result = usuarioRolesDAC.ReadBy(id_role, id_Usuario);
            return result;
        }
        public List< UsuarioRoles> ReadByUsuario(int id_Usuario)
        {
            List<UsuarioRoles> result = default(List<UsuarioRoles>);
            var usuarioRolesDAC = new UsuarioRolesDAC();
            result = usuarioRolesDAC.ReadByUsuario(id_Usuario);
            return result;
        }
        public List<UsuarioRoles> ReadByRoles(string id_Roles)
        {
            List<UsuarioRoles> result = default(List<UsuarioRoles>);
            var usuarioRolesDAC = new UsuarioRolesDAC();
            result = usuarioRolesDAC.ReadByRol(id_Roles);
            return result;
        }
        public override void Update(UsuarioRoles objeto)
        {
            var usuarioRolesDAC = new UsuarioRolesDAC();
           
            
            usuarioRolesDAC.Update(objeto);
        }

        public List<UsuarioRoles> obtenerRolesDisponiblesDelUsuario(int id_Usuario)
        {

            RolesComponent rolesComponent = new RolesComponent();
            List<Roles> roles = new List<Roles>();
            roles = rolesComponent.Read();



            List<UsuarioRoles> usuarioRoles = new List<UsuarioRoles>();
            usuarioRoles = ReadByUsuario(id_Usuario);
            List<UsuarioRoles> result = new List<UsuarioRoles>();


            foreach (Roles item in roles)
            {
                int aux = 0;

                foreach (UsuarioRoles itemRoles in usuarioRoles)
                {
                    if (item.id==itemRoles.roles.id)
                    {
                        aux = 1;
                        break;
                    }

                }
                if (aux==0)
                {
                    UsuarioRoles usuario = new UsuarioRoles();
                    usuario.roles = item;
                    result.Add(usuario);
                }


            }


            return result;


        }
    }
}

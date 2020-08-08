using Entities;
using Negocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Evaluaciones.Models
{
    public class UsuarioRolesModels
    {

        [Required]
        [DisplayName("Identificador Rol")]
        public int id_Rol { get; set; }

        [Required]
        [DisplayName("Nombre del Permiso")]
        public string name { get; set; }

        [Required]
        [DisplayName("Identificador")]
        public int id_Usuario { get; set; }

        [Required]
        [DisplayName("Nombre de usuario")]
        public string UserName { get; set; }
        [Required]
        [DisplayName("Correo electronico")]
        public string Email { get; set; }



        public List<UsuarioRolesModels> obtenerRolesDisponiblesDelUsuario(int id_Usuario)
        {
            UsuarioRolesComponent usuarioRolesComponent = new UsuarioRolesComponent();
            List<UsuarioRolesModels> result = new List<UsuarioRolesModels>();
            List<UsuarioRoles> usuarioRoles = new List<UsuarioRoles>();
            usuarioRoles= usuarioRolesComponent.obtenerRolesDisponiblesDelUsuario(id_Usuario);
            result = LoadUsuarioRolesModels(usuarioRoles);


            return result;

        }
        public UsuarioRolesModels ReadBy(string Id_roles, int Id_usuario)
        {
            UsuarioRolesComponent usuarioRolesComponent = new UsuarioRolesComponent();
            UsuarioRoles usuarioRoles = new UsuarioRoles();
            usuarioRoles = usuarioRolesComponent.ReadBy( Id_roles,  Id_usuario);

            return LoadUsuarioRolesModels(usuarioRoles);
        }

        public  List<UsuarioRolesModels> LoadUsuarioRolesModels(List<UsuarioRoles> usuarioRoles)
        {
            List<UsuarioRolesModels> result = new List<UsuarioRolesModels>();
            foreach (UsuarioRoles item in usuarioRoles)
            {
                UsuarioRolesModels usuarioRolesModels = new UsuarioRolesModels();
                usuarioRolesModels.Email = item.usuarios.Email;
                usuarioRolesModels.id_Rol = item.roles.Id;
                usuarioRolesModels.id_Usuario = item.usuarios.Id;
                usuarioRolesModels.name = item.roles.name;
                usuarioRolesModels.UserName = item.usuarios.UserName;
                result.Add(usuarioRolesModels);
            }
          
            return result;
        }


        public UsuarioRolesModels LoadUsuarioRolesModels(UsuarioRoles usuarioRoles)
        {
            UsuarioRolesModels result = new UsuarioRolesModels();
           
               result.Email = usuarioRoles.usuarios.Email;
               result.id_Rol = usuarioRoles.roles.Id;
               result.id_Usuario = usuarioRoles.usuarios.Id;
               result.name = usuarioRoles.roles.name;
               result.UserName = usuarioRoles.usuarios.UserName;
              
            

            return result;
        }

        public UsuarioRolesModels ReadyBy(int Id_Usuario)
        {
            UsuariosComponent usuariosComponent = new UsuariosComponent();
            Usuarios usuarios = new Usuarios();
            usuarios = usuariosComponent.ReadBy(Id_Usuario);
            UsuarioRolesModels result = new UsuarioRolesModels();

            result.Email =usuarios.Email;
          
            result.id_Usuario =usuarios.Id;
          
            result.UserName =usuarios.UserName;

            return result;
        }

       
    }
}
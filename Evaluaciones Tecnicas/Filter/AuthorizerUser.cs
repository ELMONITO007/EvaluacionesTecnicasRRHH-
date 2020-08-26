using Entities;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Evaluaciones_Tecnicas.Filter
{
    [AttributeUsage(AttributeTargets.Method,AllowMultiple =false)]
    public class AuthorizerUser :AuthorizeAttribute
    {
        private Usuarios usuario;
      private string rolesUsuario;
        public AuthorizerUser(string _roles ="")
        {
            this.rolesUsuario = _roles;
        }


        public List<Roles> GenerarListaRoles()
        {
            List<Roles> listaRoles = new List<Roles>();
            bool coma = this.rolesUsuario.Contains(",");
            RolesComponent rolesComponent = new RolesComponent();
            if (coma)
            {
                char delimitador = ',';
                string[] valores = rolesUsuario.Split(delimitador);

                foreach (string item in valores)
                {
                    Roles rol = new Roles();
                    rol = rolesComponent.ReadByNombreRol(item);
                    listaRoles.Add(rol);
                }




            }
            else
            {
                Roles rol = new Roles();
                rol = rolesComponent.ReadByNombreRol(rolesUsuario);
                listaRoles.Add(rol);
            }

            return listaRoles;
        
        }


        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            try
            {
                





                usuario = (Usuarios)HttpContext.Current.Session["userName"];
                UsuarioRolesComponent usuarioRolesComponent = new UsuarioRolesComponent();
                bool result = usuarioRolesComponent.VerificarSiTieneElRol(usuario, GenerarListaRoles());
                if (!result)
                {

                    filterContext.Result = new RedirectResult("/login");
                }
             
               


            }
            catch (Exception)
            {

                throw;
            }


        }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using Entities;
using Entities.Servicios.Bitacora;
using Negocio.Servicios;

namespace Negocio
{

    public class UsuariosComponent : Component<Usuarios>
    {
        public void ResetearIntentos(int id)
        {
            var usuario = new UsuarioDac();
            usuario.AgregarErrorUsryPass(0, id);
        }


        #region Crear

        public bool Verificar(string userName)
        {
            Usuarios usuarios = new Usuarios();
            UsuarioDac usuarioDac = new UsuarioDac();
            usuarios = usuarioDac.ReadByEmail(userName);
            if (usuarios is null)
            {
                return true;
            }
            else
            {
                return false;
            }

        }


        public void CambiarContraseña(Usuarios objeto)

        {


            UsuarioParcial usuariosFormateado = new UsuarioParcial();

            Usuarios usuarios = new Usuarios();
            usuarios = ReadBy(objeto.Id);
            usuariosFormateado.Email = usuarios.Email;
            usuariosFormateado.UserName = usuarios.UserName;
            usuariosFormateado.Password = usuarios.Password;
            usuarios.DVH.DVH = DigitoVerificadorH.getDigitoEncriptado(usuariosFormateado);
            EncriptarSHA256 encriptarSHA256 = new EncriptarSHA256(objeto.Password);
            usuarios.Password = encriptarSHA256.Hashear();
            UsuarioDac usuarioDac = new UsuarioDac();
            usuarioDac.Update(usuarios);
       
            DVVComponent dVVComponent = new DVVComponent();
            dVVComponent.CrearDVV(ListaDVH(), "Usuario");

            Bitacora bitacora = new Bitacora();
            BitacoraComponent bitacoraComponent = new BitacoraComponent();
            bitacora.usuarios = usuarios;
            bitacora.eventoBitacora.Id = 10;
            bitacoraComponent.Create(bitacora);




        }

        public bool Crear(Usuarios objeto)
        {
            Usuarios usuarios = new Usuarios();

            UsuarioParcial usuariosFormateado = new UsuarioParcial();


            usuariosFormateado.Email = objeto.Email;
            usuariosFormateado.UserName = objeto.UserName;
            usuariosFormateado.Password = objeto.Password;

            if (Verificar(objeto.UserName))
            {
                usuarios = objeto;
                usuarios.DVH.DVH = DigitoVerificadorH.getDigitoEncriptado(usuariosFormateado);
                EncriptarSHA256 encriptarSHA256 = new EncriptarSHA256(objeto.Password);
                usuarios.Password = encriptarSHA256.Hashear();
                UsuarioDac usuarioDac = new UsuarioDac();

                usuarioDac.Create(usuarios);

                UsuarioRoles unUsuario = new UsuarioRoles();
                unUsuario.usuarios = usuarioDac.ReadByEmail(objeto.Email);

                RolesComponent rolesComponent = new RolesComponent();

                UsuarioRolesComponent usuarioRolesComponent = new UsuarioRolesComponent();
                unUsuario.roles = rolesComponent.ReadByNombreRol(objeto.unRol.name);
                usuarioRolesComponent.Create(unUsuario);


                DVVComponent dVVComponent = new DVVComponent();
                dVVComponent.CrearDVV(ListaDVH(), "Usuario");

                Bitacora bitacora = new Bitacora();
                BitacoraComponent bitacoraComponent = new BitacoraComponent();
                bitacora.usuarios = unUsuario.usuarios;
                bitacora.eventoBitacora.Id = 8;
                bitacoraComponent.Create(bitacora);

                return true;
            }
            else
            {
                return false;
            }
        }




        #endregion
        public int AgregarErrorUsryPass(int id)

        {

            var usuario = new UsuarioDac();
            Entities.Usuarios unUsuario = new Usuarios();
            unUsuario = usuario.ReadBy(id);

            usuario.AgregarErrorUsryPass(unUsuario.CantidadIntentos + 1, id);


            return unUsuario.CantidadIntentos + 1;

        }
        public void Bloquear(int id)
        {
            var usuario = new UsuarioDac();

            usuario.Bloquear(id);
        }
        public void Desloquear(int id)
        {
            var usuario = new UsuarioDac();
            usuario.AgregarErrorUsryPass(0, id);
            usuario.Desbloquear(id);


        }
        public override void Delete(int id)
        {
            var usuario = new UsuarioDac();
            usuario.Delete(id);
        }

        public override List<Usuarios> Read()
        {
            List<Usuarios> result = default(List<Usuarios>);
            var usuario = new UsuarioDac();
            result = usuario.Read();
            return result;
        }

        public override Usuarios ReadBy(int id)
        {
            Usuarios result = default(Usuarios);
            var usuario = new UsuarioDac();
            result = usuario.ReadBy(id);
            return result;
        }

        public override void Update(Usuarios objeto)
        {
            throw new NotImplementedException();
        }

        public Usuarios ReadByEmail(string emailUsername)
        {
            UsuarioDac usuarioDac = new UsuarioDac();
            return usuarioDac.ReadByEmail(emailUsername);
        }

        public string ListaDVH()
        {
            string lista = "";
            UsuarioDac usuarioDac = new UsuarioDac();
            List<Usuarios> usuarios = new List<Usuarios>();
            usuarios = usuarioDac.ReadDVH();

            foreach (Usuarios item in usuarios)
            {
                lista = lista + item.DVH.DVH;
            }

            return lista;

        }

        public override Usuarios Create(Usuarios objeto)
        {
            throw new NotImplementedException();
        }


    }
}

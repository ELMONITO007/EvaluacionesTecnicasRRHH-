
using Entities;
using Entities.Servicios.Bitacora;
using Negocio;
using Negocio.Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
  public  class LoginComponent
    {
        public bool VerificarBloqueado(int id)
        {
            UsuariosComponent usuariosComponent = new UsuariosComponent();
            Usuarios usuarioTabla = new Usuarios();
            usuarioTabla = usuariosComponent.ReadBy(id);
            if (usuarioTabla.Bloqueado)
            {
                return false;
            }
            else
            {

                return true;
            }
        }
        public void VerificarIntentos(int id)
        {
            UsuariosComponent usuariosComponent = new UsuariosComponent();
            Usuarios usuarioTabla = new Usuarios();
            usuarioTabla = usuariosComponent.ReadBy(id);
            if (usuarioTabla.CantidadIntentos >=4)
            {
                usuariosComponent.Bloquear(id);
            }
           
        }


        public bool VerificarDVV()
        {
            UsuariosComponent usuariosComponent = new UsuariosComponent();
            DVVComponent dVVComponent = new DVVComponent();
            DVV dvvTabla = new DVV();
            dvvTabla = dVVComponent.ObtenerDVV("Usuario");
            string dvhTabla = usuariosComponent.ListaDVH();
            DVV dvvGenerado = new DVV();
            dvvGenerado = dVVComponent.GenerarDVV(dvhTabla, "Usuario");
            if (dvvTabla.dvv==dvvGenerado.dvv)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool VerificarDVH(int id,Usuarios usuarios)
        {
            UsuarioParcial usuariosFormateado = new UsuarioParcial();
            usuariosFormateado.Bloqueado = usuarios.Bloqueado;
            usuariosFormateado.Email = usuarios.Email;
            usuariosFormateado.UserName = usuarios.Email;
            usuariosFormateado.Password = usuarios.Password;
            UsuariosComponent usuariosComponent = new UsuariosComponent();
            Usuarios usuarioTabla = new Usuarios();
            usuarioTabla = usuariosComponent.ReadBy(id);
            string dvhIngresado = DigitoVerificadorH.getDigitoEncriptado(usuariosFormateado);

            if (usuarioTabla.DVH.DVH==dvhIngresado)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool VeriricarUserName(Usuarios usuarios)
        {
            UsuariosComponent usuariosComponent = new UsuariosComponent();
            Usuarios usuarioTabla = new Usuarios();
            usuarioTabla = usuariosComponent.ReadByEmail(usuarios.Email);
            if (usuarioTabla is null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }



        public bool VerificarContraseña(int id, Usuarios usuarios)
        {
            UsuariosComponent usuariosComponent = new UsuariosComponent();
            Usuarios usuarioTabla = new Usuarios();
            usuarioTabla = usuariosComponent.ReadBy(id);
            EncriptarSHA256 encriptarSHA256 = new EncriptarSHA256(usuarios.Password);
            string pass = encriptarSHA256.Hashear();
            if (usuarioTabla.Password== pass)
            {
                return true;
            }
            else
            {
                return false;
            }

        }


        public LoginError VerificarLogin(Usuarios usuarios)

        {
            BitacoraComponent bitacoraComponent = new BitacoraComponent();
            Bitacora bitacora = new Bitacora();
            bitacora.usuarios = usuarios;
            bool userName = VeriricarUserName(usuarios);
            LoginError loginError = new LoginError();
            if (userName)
            {
                UsuariosComponent usuariosComponent = new UsuariosComponent();
                Usuarios usuarioTabla = new Usuarios();
               

                usuarioTabla = usuariosComponent.ReadByEmail(usuarios.Email);
                bool password = VerificarContraseña(usuarioTabla.Id, usuarios);
                VerificarIntentos(usuarioTabla.Id);
                if (password)
                {
                    bool DVH=VerificarDVH(usuarioTabla.Id, usuarios);

                    if (DVH)
                    {
                        bool DVV = VerificarDVV();
                        if (DVV)
                        {

                            if (VerificarBloqueado(usuarioTabla.Id))
                            {
                                bitacora.eventoBitacora.Id = 5;
                                bitacoraComponent.Create(bitacora);
                                loginError.error = "";

                            }
                            else
                            {
                                loginError.error = "La cuenta esta Bloqueada. Envie un email con el error a dolores.conde@transener.com.ar ";
                                bitacora.eventoBitacora.Id = 1;
                                bitacoraComponent.Create(bitacora);
                            }
                        }
                        else
                        {
                            loginError.error = "Error Critico V. Envie un email con el error a dolores.conde@transener.com.ar";
                            bitacora.eventoBitacora.Id = 2;
                            bitacoraComponent.Create(bitacora);
                        }
                    }
                    else
                    {
                        loginError.error = "Error Critico H. Envie un email con el error a dolores.conde@transener.com.ar";
                        bitacora.eventoBitacora.Id = 3;
                        bitacoraComponent.Create(bitacora);
                    }
                }
                else
                {
                    loginError.error = "Usuario o Contraseña Invalido";
                    bitacora.eventoBitacora.Id = 4;
                    bitacoraComponent.Create(bitacora);
                }
            }

            else
            {
                loginError.error = "Usuario o Contraseña Invalido";
                bitacora.eventoBitacora.Id = 4;
                bitacoraComponent.Create(bitacora);
            }

            return loginError;
        
        }

    }
}

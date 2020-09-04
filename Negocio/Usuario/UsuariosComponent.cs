using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using Entities;

namespace Negocio
{
        public class UsuariosComponent : Component<Usuarios>
        {


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

              
                    public bool  Crear(Usuarios objeto)
                    { 
                        Usuarios usuarios = new Usuarios();

                        UsuarioParcial usuariosFormateado = new UsuarioParcial();
                        usuariosFormateado.Bloqueado = objeto.Bloqueado;
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
                            DVVComponent dVVComponent = new DVVComponent();
                            dVVComponent.CrearDVV(ListaDVH(), "Usuario");

                            return true;
                            }
                            else
                            {
                                return false;
                            }
                    }

        
       

        #endregion

        public  void Bloquear(int id)
        {
            var usuario = new UsuarioDac();
            
            usuario.Bloquear(id);
        }
        public void Desloquear(int id)
        {
            var usuario = new UsuarioDac();
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
               string lista="" ;
                UsuarioDac usuarioDac = new UsuarioDac();
                List<Usuarios> usuarios = new List<Usuarios>();
                usuarios = usuarioDac.ReadDVH();

                foreach (Usuarios item in usuarios)
                {
                    lista=lista+item.DVH.DVH;
                }

                return lista;
        
            }
           
        public override Usuarios Create(Usuarios objeto)
        {
            throw new NotImplementedException();
        }


    }
    }

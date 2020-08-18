using Data;
using Entities;
using iTextSharp.text.pdf;
using Negocio.Servicios;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class UsuarioParaExamenComponent : Component<UsuarioParaExamen>
    {
        public override UsuarioParaExamen Create(UsuarioParaExamen objeto)
        {

            //crear el usuario
            Usuarios usuarios = new Usuarios();
            usuarios.Bloqueado = objeto.usuarios.Bloqueado;
            usuarios.Email = objeto.usuarios.Email;
            usuarios.UserName = objeto.usuarios.UserName;
            usuarios.Password = objeto.usuarios.Password;
            UsuariosComponent usuariosComponent = new UsuariosComponent();
            bool result = usuariosComponent.Crear(usuarios);


            //obtener el usuario creado
            Usuarios unusuario = new Usuarios();
            unusuario = usuariosComponent.ReadByEmail(usuarios.Email);
            unusuario.Password = usuarios.Password;

            //Inicializar la clase CrearPDF 
            CrearPDF crearPDF = new CrearPDF();
            //si no existe el usuario registro los datos adicionales
            if (result)
            {
                objeto.usuarios = unusuario;
                UsuarioParaexamenDAC usuarioParaexamenDAC = new UsuarioParaexamenDAC();
                usuarioParaexamenDAC.Create(objeto);
                ;
                crearPDF.CrearPDFParaUsuarioExamen(objeto);


            }

            else
            {
                //verificar si esta bloqueado y desbloquear

                LoginComponent loginComponent = new LoginComponent();
                bool bloqueado = loginComponent.VerificarBloqueado(unusuario.Id);

                if (!bloqueado)
                {
                    usuariosComponent.Desloquear(unusuario.Id);
                    crearPDF.AbrirPDF(objeto.usuarios.Email);
                }

            }
            return objeto;
        }

              
            

        

        public override void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public override List<UsuarioParaExamen> Read()
        {
            throw new NotImplementedException();
        }

        public override UsuarioParaExamen ReadBy(int id)
        {
            throw new NotImplementedException();
        }

        public override void Update(UsuarioParaExamen objeto)
        {
            throw new NotImplementedException();
        }
    }
}

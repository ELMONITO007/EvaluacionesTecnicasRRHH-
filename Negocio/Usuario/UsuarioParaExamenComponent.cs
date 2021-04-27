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

        public string EmailEmpresarial(string email)
        {
            string[] separadas;

            separadas = email.Split('@');
            if (separadas[1]=="@transener.com.ar" || separadas[1] == "@transba.com.ar")
            {
                return "Interno";
            }
            else
            {
                return "Externo";
            }
        }

        public void CambiarContraseña(UsuarioParaExamen objeto)
        {
            UsuariosComponent usuariosComponent = new UsuariosComponent();
            usuariosComponent.CambiarContraseña(objeto.usuarios);
            PDF pDF = new PDF();
            UsuarioParaExamen usuarioParaExamen = new UsuarioParaExamen();
            usuarioParaExamen.usuarios = usuariosComponent.ReadBy(objeto.usuarios.Id);
            pDF.unUsuario = usuarioParaExamen;
            pDF.unUsuario.usuarios.Password = objeto.usuarios.Password;
            CrearPDF crearPDF = new CrearPDF();
            crearPDF.Create(pDF);
        }
        public override UsuarioParaExamen Create(UsuarioParaExamen objeto)
        {

            //crear el usuario
            Usuarios usuarios = new Usuarios();
            usuarios.Bloqueado = false;
            usuarios.Email = objeto.usuarios.Email;
            usuarios.UserName = objeto.usuarios.Email;
            usuarios.Nombre = objeto.usuarios.Nombre;
            usuarios.Apellido = objeto.usuarios.Apellido;
            usuarios.Tipo = EmailEmpresarial(objeto.usuarios.Email);
            usuarios.Password = CrearContraseña(objeto) ;
            string _heather = usuarios.Password;
            UsuariosComponent usuariosComponent = new UsuariosComponent();
            usuarios.unRol.name = "Examen";
            bool result = usuariosComponent.Crear(usuarios);


            //obtener el usuario creado
            Usuarios unusuario = new Usuarios();
            unusuario = usuariosComponent.ReadByEmail(usuarios.Email);
            unusuario.Password = usuarios.Password;

            ////Agregar el permiso Examen

            //UsuarioRoles usuarioRoles = new UsuarioRoles();
            //UsuarioRolesComponent usuarioRolesComponent = new UsuarioRolesComponent();
            //usuarioRoles.usuarios.Id = unusuario.Id;
            //usuarioRoles.roles.id = "2";
            //usuarioRolesComponent.Create(usuarioRoles);


            //Inicializar la clase CrearPDF 
            CrearPDF crearPDF = new CrearPDF();
            //si no existe el usuario registro los datos adicionales
            if (result)
            {
                objeto.usuarios = unusuario;
                UsuarioParaexamenDAC usuarioParaexamenDAC = new UsuarioParaexamenDAC();
                usuarioParaexamenDAC.Create(objeto);
                
                PDF pDF = new PDF();
                objeto.usuarios.Password = _heather;
                pDF.unUsuario = objeto;
                crearPDF.Create(pDF);


            }

            else
            {
                //verificar si esta bloqueado y desbloquear

                LoginComponent loginComponent = new LoginComponent();
                bool bloqueado = loginComponent.VerificarBloqueado(unusuario.Id);

                if (!bloqueado)
                {
                    usuariosComponent.Desloquear(unusuario.Id);
                   
                }
                crearPDF.AbrirPDF(objeto.usuarios.Email);

            }
            return objeto;
        }


        public string CrearContraseña(UsuarioParaExamen usuarioParaExamen)
        {
            string result;
            var chars = "*#$";
            var stringChars = new char[1];
            var random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            Sede sede = new Sede();
            SedeComponent sedeComponent = new SedeComponent();
            sede = sedeComponent.ReadBy(usuarioParaExamen.sede.Id);
            EmpresaComponent empresaComponent = new EmpresaComponent();
            Empresa empresa = new Empresa();
            empresa = empresaComponent.ReadBy(sede.empresa.Id);

            Gerencia gerencia = new Gerencia();
            GerenciaComponent gerenciaComponent = new GerenciaComponent();
            gerencia = gerenciaComponent.ReadBy(usuarioParaExamen.gerencia.Id);

            string finalString = new String(stringChars);
            result = empresa.empresa + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + gerencia.gerencia + finalString;
            return result;
        }



        public override void Delete(int id)
        {
            UsuarioParaexamenDAC usuarioParaexamenDAC = new UsuarioParaexamenDAC();
            usuarioParaexamenDAC.Delete(id);
        }

        public override List<UsuarioParaExamen> Read()
        {
            UsuarioParaexamenDAC usuarioParaexamenDAC = new UsuarioParaexamenDAC();
            List<UsuarioParaExamen> result = new List<UsuarioParaExamen>();
           
            foreach (UsuarioParaExamen item in usuarioParaexamenDAC.Read())
            {
                UsuarioParaExamen usuarioParaExamen = new UsuarioParaExamen();

                UsuariosComponent usuarios = new UsuariosComponent();
                usuarioParaExamen.usuarios = usuarios.ReadBy(item.usuarios.Id);


                DireccionDAC direccionDAC = new DireccionDAC();
                Direccion direccion = new Direccion();
                direccion = direccionDAC.ReadBy(item.direccion.Id);
                usuarioParaExamen.direccion = direccion;

                SedeDAC sedeDAC = new SedeDAC();
                Sede sede = new Sede();
                sede= sedeDAC.ReadBy(item.sede.Id);
                usuarioParaExamen.sede = sede;
                    
                EmpresaDAC empresaDAC = new EmpresaDAC();
                usuarioParaExamen.sede.empresa = empresaDAC.ReadBy(sede.empresa.Id);


             
                GerenciaDAC gerenciaDAC = new GerenciaDAC();
                usuarioParaExamen.gerencia = gerenciaDAC.ReadBy(item.gerencia.Id);

                JefaturaDAC jefaturaDAC = new JefaturaDAC();
                usuarioParaExamen.jefatura = jefaturaDAC.ReadBy(item.jefatura.Id);

                SectorDAC sector = new SectorDAC();
                usuarioParaExamen.sector = sector.ReadBy(item.sector.Id);

                result.Add(usuarioParaExamen);


            }
            return result;
        }

        public override UsuarioParaExamen ReadBy(int id)
        {
            UsuarioParaExamen usuarioParaExamen = new UsuarioParaExamen();
            UsuarioParaexamenDAC usuarioParaexamenDAC = new UsuarioParaexamenDAC();
            usuarioParaExamen = usuarioParaexamenDAC.ReadBy(id);

            UsuariosComponent usuarios = new UsuariosComponent();
            usuarioParaExamen.usuarios = usuarios.ReadBy(usuarioParaExamen.usuarios.Id);


            DireccionDAC direccionDAC = new DireccionDAC();
            Direccion direccion = new Direccion();
            direccion = direccionDAC.ReadBy(usuarioParaExamen.direccion.Id);
            usuarioParaExamen.direccion = direccion;

            SedeDAC sedeDAC = new SedeDAC();
            Sede sede = new Sede();
            sede = sedeDAC.ReadBy(usuarioParaExamen.sede.Id);
            usuarioParaExamen.sede = sede;

            EmpresaDAC empresaDAC = new EmpresaDAC();
            usuarioParaExamen.sede.empresa = empresaDAC.ReadBy(usuarioParaExamen.sede.empresa.Id);



            GerenciaDAC gerenciaDAC = new GerenciaDAC();
            usuarioParaExamen.gerencia = gerenciaDAC.ReadBy(usuarioParaExamen.gerencia.Id);

            JefaturaDAC jefaturaDAC = new JefaturaDAC();
            usuarioParaExamen.jefatura = jefaturaDAC.ReadBy(usuarioParaExamen.jefatura.Id);

            SectorDAC sector = new SectorDAC();
            usuarioParaExamen.sector = sector.ReadBy(usuarioParaExamen.sector.Id);

            return usuarioParaExamen;
        }

        public override void Update(UsuarioParaExamen objeto)
        {
            throw new NotImplementedException();
        }
    }
}

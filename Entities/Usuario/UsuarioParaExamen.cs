using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
  public  class UsuarioParaExamen :EntityBase
    {

        public Usuarios usuarios { get; set; }
        public Sede sede { get; set; }
        public List<Sede> ListaSedes = new List<Sede>();
        public Direccion direccion { get; set; }
        public List<Direccion> ListaDireccion = new List<Direccion>();
        public Gerencia gerencia { get; set; }
        public List<Gerencia> ListaGerencia = new List<Gerencia>();
        public Jefatura jefatura { get; set; }
        public List<Jefatura> ListaJefatura = new List<Jefatura>();
        public Sector sector { get; set; }
        public List<Sector> ListaSector = new List<Sector>();
        public string tipo { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; } 
        public override int Id { get; set; }

        public UsuarioParaExamen()
        {
            usuarios = new Usuarios();
            sede = new Sede();
            direccion = new Direccion();
            gerencia = new Gerencia();
            jefatura = new Jefatura();
            sector = new Sector();

        }
    }
}

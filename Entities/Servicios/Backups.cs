using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
  public  class Backups : EntityBase
    {
        public Usuarios usuarios { get; set; }
        public string Nombre { get; set; }
        public string Fecha { get; set; }
        public override int Id { get; set; }
        public string Path { get; set; }

        public Backups()
        {
            usuarios = new Usuarios();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
  public  class PDF : EntityBase
    {
        public override int Id { get; set; }
        public UsuarioParaExamen unUsuario { get; set; }
        public string path { get; set; }

        public  PDF()
        {
            unUsuario = new UsuarioParaExamen();

        }
    }
}

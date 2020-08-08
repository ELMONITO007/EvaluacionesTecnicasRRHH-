using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Sede : EntityBase
    {
        [DataMember]
        public override int Id { get; set; }
        [DisplayName("Sede") ]
        public string sede { get; set; }
         [DisplayName("Codigo EETT") ]
        public string codigo { get; set; }
        [DisplayName("Empresa")]
        public Empresa empresa { get; set; }
        public List<Empresa> ListaEmpresas = new List<Empresa>();
        public Sede ()
        {

            empresa = new Empresa();
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
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
        [StringLength(20, ErrorMessage = "El maximo de caracteres es de 20")]
   
        [MinLength(2, ErrorMessage = "El minimo de caracteres es de 2")]

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

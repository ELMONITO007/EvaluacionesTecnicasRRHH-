using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;
namespace Entities
{

    public   class TipoPregunta : EntityBase
    {
        [DataMember]
        public override int Id { get; set; }
        [DataMember]
        [DisplayName("Tipo de Pregunta")]
        [Required]

        public string TipoDePregunta { get; set; }


    }
}

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

    public class Nivel : EntityBase
    {
        [DataMember]
        public override int Id { get; set; }


        [DataMember]
        [DisplayName("Nivel")]
        [StringLength(20, ErrorMessage = "El maximo de caracteres es de 20")]
        [MinLength(1, ErrorMessage = "El minimo de caracteres es de 1")]
        [Required]
        public string ElNivel { get; set; }

    }
}

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
  
    public class Categoria : EntityBase
    {
        [DataMember]
        public override int Id { get; set; }

  
        [DisplayName("Categoria")]
        [StringLength(30, ErrorMessage = "El maximo de caracteres es de 30")]
        [MinLength(0, ErrorMessage = "El minimo de caracteres es de 1")]
        [Required]
        public string LaCategoria { get; set; }

  
        [DisplayName("Descripción")]

        [RegularExpression(@"[ A-Za-zäÄëËïÏöÖüÜáéíóúáéíóúÁÉÍÓÚÂÊÎÔÛâêîôûàèìòùÀÈÌÒÙ.-]+", ErrorMessage = "ingresar solo letras")]
        [StringLength(100, ErrorMessage = "El maximo de caracteres es de 100")]
        [MinLength(5, ErrorMessage = "El minimo de caracteres es de 5")]

        public string Descripcion { get; set; }


    }
}

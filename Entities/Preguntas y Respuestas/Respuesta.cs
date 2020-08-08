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
    [Serializable]
    [DataContract]
    public abstract class Respuesta : EntityBase
    {
        [Required]
        [DataMember]
        [DisplayName("Respuesta")]
        [DataType(DataType.Text, ErrorMessage = "Ingrese texto")]
        public string LaRespuesta { get; set; }

        [DataMember]
        public override int Id { get; set; }
        [DataMember]
        public  Pregunta pregunta { get; set; }
    }
}

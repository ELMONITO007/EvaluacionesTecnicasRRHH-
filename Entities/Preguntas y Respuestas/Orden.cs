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
 
    public   class Orden : Respuesta
    {
        public List<Orden> listaOrden { get; set; }
        [DataMember]
        [DisplayName("Orden")]
        [Required]
       
        public int NumeroOrden { get; set; }
        public List<int> OrdenesDisponibles;
        public Orden(Pregunta pr)
        {
            listaOrden = new List<Orden>();
            pregunta =new Pregunta();
            pregunta = pr;
        }
        public Orden()
        {
            listaOrden = new List<Orden>();
            pregunta = new Pregunta();
        }
    }
}

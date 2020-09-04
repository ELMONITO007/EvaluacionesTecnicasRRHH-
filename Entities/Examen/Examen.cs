using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Examen
{
    public class Examen : EntityBase
    {
        public List<Pregunta> listaPregunta { get; set; }
        public Categoria Categoria { get; set; }
        public override int Id { get; set; }

        [DisplayName("Fecha examen")]
        public DateTime Fecha { get; set; }

        [DisplayName("¿Examen Realizado?")]
        public string Estado { get; set; }


        [DisplayName("Resultado")]
        public int Resultado { get; set; }

        [DisplayName("¿Aprobo?")]
        public bool Aprobado { get; set; }

        public Usuarios  usuario { get; set; }


        public Examen()
        {
            listaPregunta = new List<Pregunta>();
               Categoria categoria = new Categoria();
            usuario = new Usuarios();
        }




    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class SubPregunta : EntityBase
    {
        public override int Id { get; set; }
        public Pregunta pregunta { get; set; }
        public Pregunta subPregunta  { get; set; }


    public SubPregunta()
        {
            pregunta = new Pregunta();
            subPregunta = new Pregunta();

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using Entities;


namespace Negocio
{
    public class TipoPreguntaComponent : Component<TipoPregunta>
    {
        public override TipoPregunta Create(TipoPregunta objeto)
        {
            TipoPregunta result = default(TipoPregunta);
            var tipoPreguntaDAC = new TipoPreguntaDAC();

            result = tipoPreguntaDAC.Create(objeto);
            return result;
        }

        public override void Delete(int id)
        {
            var tipoPreguntaDAC = new TipoPreguntaDAC();
            tipoPreguntaDAC.Delete(id);
        }

        public override List<TipoPregunta> Read()
        {
            List<TipoPregunta> result = default(List<TipoPregunta>);
            var tipoPreguntaDAC = new TipoPreguntaDAC();
            result = tipoPreguntaDAC.Read();
            return result;
        }

        public override TipoPregunta ReadBy(int id)
        {
            TipoPregunta result = default(TipoPregunta);
            var tipoPreguntaDAC = new TipoPreguntaDAC();
            result = tipoPreguntaDAC.ReadBy(id);
            return result;

        }

        public override void Update(TipoPregunta objeto)
        {
            var tipoPreguntaDAC = new TipoPreguntaDAC();
            tipoPreguntaDAC.Update(objeto);
        }
    }
}

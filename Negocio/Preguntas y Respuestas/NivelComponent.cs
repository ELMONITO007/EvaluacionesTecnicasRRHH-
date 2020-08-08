using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using Entities;

namespace Negocio
{
    public class NivelComponent : Component<Nivel>
    {
        public override Nivel Create(Nivel objeto)
        {
            Nivel result = default(Nivel);
            var nivelDAC = new NivelDAC();

            result = nivelDAC.Create(objeto);
            return result;
        }

        public override void Delete(int id)
        {
            var nivelDAC = new NivelDAC();
            nivelDAC.Delete(id);
        }

        public override List<Nivel> Read()
        {
            List<Nivel> result = default(List<Nivel>);

            var nivelDAC = new NivelDAC();
            result = nivelDAC.Read();
            return result;
        }

        public override Nivel ReadBy(int id)
        {
            Nivel result = default(Nivel);
            var nivelDAC = new NivelDAC();
            result = nivelDAC.ReadBy(id);
            return result;
        }

        public override void Update(Nivel objeto)
        {
            var nivelDAC = new NivelDAC();
            nivelDAC.Update(objeto);
        }
    }
}

using Data;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class JefaturaComponent : Component<Jefatura>
    {
        public override Jefatura Create(Jefatura objeto)
        {
            Jefatura result = default(Jefatura);
            var jefaturaDAC = new JefaturaDAC();

            result = jefaturaDAC.Create(objeto);
            return result;
        }

        public override void Delete(int id)
        {
            var jefaturaDAC = new JefaturaDAC();

            jefaturaDAC.Delete(id);
        }

        public override List<Jefatura> Read()
        {
            List<Jefatura> result = default(List<Jefatura>); 
            var jefaturaDAC = new JefaturaDAC();

            result = jefaturaDAC.Read();
            return result;
        }

        public override Jefatura ReadBy(int id)
        {
          Jefatura result = default(Jefatura);
            var jefaturaDAC = new JefaturaDAC();

            result = jefaturaDAC.ReadBy(id);
            return result;
        }

        public override void Update(Jefatura objeto)
        {
            var jefaturaDAC = new JefaturaDAC();
            jefaturaDAC.Update(objeto);
        }
        public bool Verificar(Jefatura jefatura)
        {
            var jefaturaDAC = new JefaturaDAC();

            Jefatura result = default(Jefatura);
            result = jefaturaDAC.ReadBy(jefatura.jefatura);
            if (result is null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}

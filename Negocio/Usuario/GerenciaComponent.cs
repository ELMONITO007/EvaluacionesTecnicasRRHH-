using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using Entities;
namespace Negocio
{
    public class GerenciaComponent : Component<Gerencia>
    {
        public override Gerencia Create(Gerencia objeto)
        {
            Gerencia result = default(Gerencia);
            var gerenciaDAC = new GerenciaDAC();

            result = gerenciaDAC.Create(objeto);
            return result;
        }

        public override void Delete(int id)
        {
            var gerenciaDAC = new GerenciaDAC();
            gerenciaDAC.Delete(id);
        }

        public override List<Gerencia> Read()
        {
            List<Gerencia> result = default(List<Gerencia>);
            var gerenciaDAC = new GerenciaDAC();
            result = gerenciaDAC.Read();
            return result;
        }

        public override Gerencia ReadBy(int id)
        {
            Gerencia result = default(Gerencia);
            var gerenciaDAC = new GerenciaDAC();

            result = gerenciaDAC.ReadBy(id);
            return result;
        }

        public override void Update(Gerencia objeto)
        {
            var gerenciaDAC = new GerenciaDAC();
      gerenciaDAC.Update(objeto);
        }

        public bool Verificar(Gerencia gerencia)
        {
           var gerenciaDAC = new GerenciaDAC();

            Gerencia result = default(Gerencia);
            result = gerenciaDAC.ReadBy(gerencia.gerencia);
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Data;
using Entities;
namespace Negocio
{
    public class EmpresaComponent : Component<Empresa>
    {
        public override Empresa Create(Empresa objeto)
        {
            Empresa result = default(Empresa);
            var EmpresaDAC = new EmpresaDAC();

            result = EmpresaDAC.Create(objeto);
            return result;
        }

        public override void Delete(int id)
        {
            var EmpresaDAC = new EmpresaDAC();
            EmpresaDAC.Delete(id);
        }

        public override List<Empresa> Read()
        {
            List<Empresa> result = default(List<Empresa>);
            var EmpresaDAC = new EmpresaDAC();
            result = EmpresaDAC.Read();
            return result;
        }

        public override Empresa ReadBy(int id)
        {
            Empresa result = default(Empresa);
            var EmpresaDAC = new EmpresaDAC();

            result = EmpresaDAC.ReadBy(id);
            return result;
        }

        public override void Update(Empresa objeto)
        {
            var EmpresaDAC = new EmpresaDAC();
            EmpresaDAC.Update(objeto);
        }
        public bool Verificar(Empresa empresa)
        {
            Empresa result = default(Empresa);
            var EmpresaDAC = new EmpresaDAC();

            result = EmpresaDAC.ReadBy(empresa.empresa);
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

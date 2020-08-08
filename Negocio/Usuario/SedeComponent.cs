using Data;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class SedeComponent : Component<Sede>
    {
        public override Sede Create(Sede objeto)
        {
            Sede result = default(Sede);
            var sedeDAC = new SedeDAC();

            result = sedeDAC.Create(objeto);
            return result;
        }

        public override void Delete(int id)
        {
            var sedeDAC = new SedeDAC();

           sedeDAC.Delete(id);
        }

        public override List<Sede> Read()
        {

            List<Sede> result = default(List<Sede>);
            var sedeDAC = new SedeDAC();
            List<Sede> sedes = new List<Sede>();
            result = sedeDAC.Read();

            foreach (Sede item in result)
            {
                Sede sede = new Sede();
                EmpresaComponent empresaComponent = new EmpresaComponent();
                Empresa empresa = new Empresa();
                empresa = empresaComponent.ReadBy(item.empresa.Id);
                sede.empresa = empresa;
                sede.Id = item.Id;
                sede.sede = item.sede;
                sede.codigo = item.codigo;
                sedes.Add(sede);
            }
            return sedes;
        }

        public override Sede ReadBy(int id)
        {
            Sede result = default(Sede);
            var sedeDAC = new SedeDAC();
            EmpresaComponent empresaComponent = new EmpresaComponent();
            Empresa empresa = new Empresa();

            result = sedeDAC.ReadBy(id);
            empresa = empresaComponent.ReadBy(result.empresa.Id);
            result.empresa = empresa;

            return result;
        }

        public override void Update(Sede objeto)
        {
            var sedeDAC = new SedeDAC();

             sedeDAC.Update(objeto);
        }
        public bool Verificar(Sede sede)
        {
            var sedeDAC = new SedeDAC();

            Sede result = default(Sede);
            result = sedeDAC.ReadBy(sede);
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

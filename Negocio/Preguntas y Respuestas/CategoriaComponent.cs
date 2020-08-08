using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using Entities;
namespace Negocio
{
    public class CategoriaComponent : Component<Categoria>
    {
        public override Categoria Create(Categoria objeto)
        {
            Categoria result = default(Categoria);
            var categoriaDAC = new CategoriaDAC();

            result = categoriaDAC.Create(objeto);
            return result;
        }

        public override void Delete(int id)
        {
            var categoriaDAC = new CategoriaDAC();
            categoriaDAC.Delete(id);

        }

        public override List<Categoria> Read()
        {

            List<Categoria> result = default(List<Categoria>);
            var categoriaDAC = new CategoriaDAC();
            result = categoriaDAC.Read();
            return result;
        }

        public override Categoria ReadBy(int id)
        {

           Categoria result = default(Categoria);
            var categoriaDAC = new CategoriaDAC();
            result = categoriaDAC.ReadBy(id);
            return result;
        }

        public override void Update(Categoria objeto)
        {
            var categoriaDAC = new CategoriaDAC();
            categoriaDAC.Update(objeto);
        }
    }
}

using Data.Examen;
using Entities.Examen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Examen
{
    public class ExamenPreguntaComponent : IRepository<ExamenPregunta>
    {
        public ExamenPregunta Create(ExamenPregunta entity)
        {
            ExamenPreguntaDAC examenPreguntaDAC = new ExamenPreguntaDAC();
            return examenPreguntaDAC.Create(entity);
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<ExamenPregunta> Read()
        {
            throw new NotImplementedException();
        }

        public ExamenPregunta ReadBy(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(ExamenPregunta entity)
        {
            throw new NotImplementedException();
        }
    }
}

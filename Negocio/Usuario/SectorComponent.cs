using Data;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class SectorComponent : Component<Sector>
    {
        public override Sector Create(Sector objeto)
        {
            Sector result = default(Sector);
            var sectorDAC = new SectorDAC();

            result = sectorDAC.Create(objeto);
            return result;
        }

        public override void Delete(int id)
        {
            var sectorDAC = new SectorDAC();
            sectorDAC.Delete(id);
        }

        public override List<Sector> Read()
        {
            List<Sector> result = default(List<Sector>);

            var sectorDAC = new SectorDAC();

            result = sectorDAC.Read();
            return result;

        }

        public override Sector ReadBy(int id)
        {
       Sector result = default(Sector);

            var sectorDAC = new SectorDAC();

            result = sectorDAC.ReadBy(id);
            return result;
        }

        public override void Update(Sector objeto)
        {
            var sectorDAC = new SectorDAC();

           sectorDAC.Update(objeto);
        }
        public bool Verificar(Sector sector)
        {
            var sectorDAC = new SectorDAC();

            Sector result = default(Sector);
            result = sectorDAC.ReadBy(sector.sector);
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

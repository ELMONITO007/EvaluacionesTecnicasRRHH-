using Data.Examen;
using Entities.Examen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Informes
{
   public class InformeComponent
    {
        public Informe ObtenerInforme()
        {
            Informe informe = new Informe();
            InformeDAC informeDAC = new InformeDAC();
            informe.cantidadAprobado = informeDAC.CantidadAprobado();
            informe.cantidadArealizar = informeDAC.CantidadARealizar();
            informe.cantidadDesaprobado = informeDAC.CantidadDesaprobado()-informe.cantidadArealizar;
            informe.cantidadExamen = informeDAC.CantidadExamen();
            informe.cantidadExamenTerminado = informeDAC.CantidadRealizado();
            informe.cantidadPreguntas = informeDAC.CantidadPreguntas();

            return informe;
        }

    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
   public class DigitoVerificadorH : EntityBase
    {
        public string DVH { get; set; }
        public DigitoVerificadorH()

        {

        }

        public override int Id { get; set; }

        public static string getDigitoEncriptado(object unObjeto)
        {
            string digitoAEncriptar = DVGReflection.GetDVH(unObjeto);
            EncriptarSHA256 e = new EncriptarSHA256(digitoAEncriptar);
            string digitoEncriptado = e.Hashear();
            return digitoEncriptado;
        }
    }
}

using Data;
using Entities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
  public  class DVVComponent
    {
        public DVV GenerarDVV(string DVV,string tabla)
        {
            DVV dVV = new DVV();
           
            
            EncriptarSHA256 encriptarSHA256 = new EncriptarSHA256(DVV);
            dVV.dvv = encriptarSHA256.Hashear();
            dVV.tabla = tabla;
            return dVV;

        }

    public DVV ObtenerDVV(String tabla)
        {
            DVVDac dVVDac = new DVVDac();
            return dVVDac.ReadByTabla(tabla);

        }

        public void CrearDVV(string DVV, string tabla)
        {
            DVVDac dVVDac = new DVVDac();
            dVVDac.Create(GenerarDVV(DVV, tabla));

        }

        public void Login(Usuarios usuarios)
        { 
        
        }
    }
}

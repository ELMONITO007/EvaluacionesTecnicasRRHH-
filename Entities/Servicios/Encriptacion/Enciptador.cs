
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{

  public abstract class Enciptador : EntityBase
    {
        public string valorInicial;

        public string valorEncriptado;

        public abstract string Hashear();
    }
}


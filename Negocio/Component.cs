using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Common;


namespace Negocio
{
   public abstract class Component<T>
    {
        
        public abstract T Create(T objeto);
        public abstract List<T> Read();
        public abstract T ReadBy(int id);
        public abstract void Update(T objeto);

        public abstract void Delete(int id);

    }
}

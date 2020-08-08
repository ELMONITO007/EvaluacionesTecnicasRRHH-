using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
 public   class Jefatura :EntityBase
    {
        public override int Id { get; set; }
        [System.ComponentModel.DisplayName ("Jefatura")]
        public string jefatura { get; set; }
    }
}

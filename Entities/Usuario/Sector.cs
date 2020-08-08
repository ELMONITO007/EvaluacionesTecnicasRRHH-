using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{ 
  public  class Sector : EntityBase
    {
        public override int Id { get; set; }

        [System.ComponentModel.DisplayName("Sector")]
        public string sector { get; set; }
    }
}

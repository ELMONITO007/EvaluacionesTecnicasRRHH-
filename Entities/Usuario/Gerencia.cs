using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
  public  class Gerencia : EntityBase
    {
        public override int Id { get; set; }
        [System.ComponentModel.DisplayName("Gerencia")]
        public string gerencia { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Empresa : EntityBase
    {
        [DataMember]
        public override int Id { get; set; }

        [DisplayName("Empresa")]
        public string empresa { get; set; }




    }
}

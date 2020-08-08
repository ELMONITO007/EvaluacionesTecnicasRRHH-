using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities
{
   public class Roles : EntityBase
    {
        public override int Id { get; set; }
        [Required]
        [DisplayName("Identificador")]
        public string id { get; set; }

        [Required]
        [DisplayName("Nombre del Permiso")]
        public string name { get; set; }



        public Roles(string _id,string _NombrePermiso)
        {
            id = id;
            name = _NombrePermiso;
        }

        public Roles()
        {

        }

    }
}

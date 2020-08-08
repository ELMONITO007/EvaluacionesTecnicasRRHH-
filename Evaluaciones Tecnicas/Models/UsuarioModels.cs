using Entities;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Evaluaciones.Models
{
    public class UsuarioModels
    {
        public Usuarios  usuario { get; set; }
        [Required]
        [DisplayName("Identificador")]
        public string id { get; set; }

        [Required]
        [DisplayName("Nombre de usuario")]
        public string UserName { get; set; }
        [Required]
        [DisplayName("Correo electronico")]
        public string Email { get; set; }
        [DisplayName("Password")]
        public string Password { get; set; }

        public UsuarioModels()
        {
            usuario = new Usuarios();
        
        }


    }
}
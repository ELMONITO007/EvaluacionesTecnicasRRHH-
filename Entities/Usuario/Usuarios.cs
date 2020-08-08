

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities
{
    public class Usuarios : EntityBase
    {
        [DisplayName("Identificador")]
        public override int Id { get; set; }
        public LoginError   loginError { get; set; }

        [DisplayName("Apellido")]
        public string Apellido { get; set; }

        [DisplayName("Nombre")]
        public string Nombre { get; set; }


        [DisplayName("Nombre de usuario")]
        public string UserName { get; set; }

        [DisplayName("¿Esta Bloquedo?")]
        public bool Bloqueado { get; set; }

       
        [DisplayName("Intentos")]
        public int CantidadIntentos { get; set; }

        [Required]
        [EmailAddress]
        [DisplayName("Correo electronico")]
        public string Email { get; set; }


        [Required]
        [StringLength(100, ErrorMessage = "El número de caracteres de {0} debe ser al menos {2}.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }
        public Usuarios (int _Id, string _NombreUsuario ,string _email,string _Password)
        {
            Id = _Id;
            UserName = _NombreUsuario;
            Email = _email;
            Password = _Password;
        }

        public DigitoVerificadorH    DVH { get; set; }
        public Usuarios()
        {
            loginError = new LoginError();
            DVH = new DigitoVerificadorH();
        }
    }
}

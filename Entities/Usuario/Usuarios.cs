

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
        public LoginError loginError { get; set; }

        [DisplayName("Apellido")]
        [Required]

        [RegularExpression(@"[ A-Za-zäÄëËïÏöÖüÜáéíóúáéíóúÁÉÍÓÚÂÊÎÔÛâêîôûàèìòùÀÈÌÒÙ.-]+", ErrorMessage = "ingresar solo letras")]
        [StringLength(20, ErrorMessage = "El maximo de caracteres es de 20")]
        [MinLength(2, ErrorMessage = "El minimo de caracteres es de 2")]
        public string Apellido { get; set; }

        [DisplayName("Nombre")]
        [Required]

        [RegularExpression(@"[ A-Za-zäÄëËïÏöÖüÜáéíóúáéíóúÁÉÍÓÚÂÊÎÔÛâêîôûàèìòùÀÈÌÒÙ.-]+", ErrorMessage = "ingresar solo letras")]
        [StringLength(20, ErrorMessage = "El maximo de caracteres es de 20")]
        [MinLength(2, ErrorMessage = "El minimo de caracteres es de 2")]
        public string Nombre { get; set; }

        [DisplayName("¿Es un Colaborador?")]
        public string Tipo { get; set; }
        [DisplayName("Nombre de usuario")]
        [Required]

        [RegularExpression(@"[ A-Za-zäÄëËïÏöÖüÜáéíóúáéíóúÁÉÍÓÚÂÊÎÔÛâêîôûàèìòùÀÈÌÒÙ.-]+", ErrorMessage = "ingresar solo letras")]
        [StringLength(20, ErrorMessage = "El maximo de caracteres es de 20")]
        [MinLength(2, ErrorMessage = "El minimo de caracteres es de 2")]
        public string UserName { get; set; }

        [DisplayName("¿Esta Bloquedo?")]
        public bool Bloqueado { get; set; }


        [DisplayName("Intentos")]
        public int CantidadIntentos { get; set; }

        [Required]
        [DisplayName("Email")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Formato invalido")]
        public string Email { get; set; }

        public  string GoogleCaptchaToken { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [StringLength(20, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 4)]
        [Display(Name = "Contraseña")]
        [RegularExpression("^((?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])|(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[^a-zA-Z0-9])|(?=.*?[A-Z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])|(?=.*?[a-z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])).{8,}$", ErrorMessage = "El password debe cumplir el siguiente formato:Mayuscula, miniscula, numeros y caracteres especiales (Ej !@#$%^&*)")]

        public string Password { get; set; }
        public List<Roles> roles { get; set; }
        public Roles unRol { get; set; }
        public Usuarios(int _Id, string _NombreUsuario, string _email, string _Password)
        {
            Id = _Id;
            UserName = _NombreUsuario;
            Email = _email;
            Password = _Password;
            roles = new List<Roles>();
        }

        public DigitoVerificadorH DVH { get; set; }
        public Usuarios()
        {
            loginError = new LoginError();
            DVH = new DigitoVerificadorH();
            roles = new List<Roles>();
            unRol = new Roles();
        }


    
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Examen
{
    public class Examen : EntityBase
    {
        [DisplayName("Respuestas correctas")]
        public int RespuestasCorrectas { get; set; }

        [DisplayName("Respuestas Incorrectas")]
        public int RespuestasIncorrectas { get; set; }

        public Pregunta pregunta { get; set; }
        public List<ExamenPregunta> listaExamenPregunta { get; set; }
        public List<Categoria>listaCategoria { get; set; }
        public string error { get; set; }
        public List<Pregunta> listaPregunta { get; set; }
        public Categoria Categoria { get; set; }
        public override int Id { get; set; }


        [Required]
        [DisplayName("Preguntas")]
        [Range(10, 99, ErrorMessage = "Ingrese un numero entre 10 y 99")]
        public int cantidadPreguntas { get; set; }

        [DisplayName("Fecha examen")]
        public DateTime Fecha { get; set; }

        [DisplayName("¿Examen Realizado?")]
        public string Estado { get; set; }


        [DisplayName("Resultado")]
        public int Resultado { get; set; }

        [DisplayName("¿Aprobo?")]
        public bool Aprobado { get; set; }

        public Usuarios  usuario { get; set; }

        public int facil { get; set; }
        public int medio { get; set; }
        public int dificil { get; set; }
        public Examen()
        {
            listaPregunta = new List<Pregunta>();
            Categoria = new Categoria();
            usuario = new Usuarios();
            listaCategoria = new List<Categoria>();
            pregunta = new Pregunta();
           
        }
    


    }
}

using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace Entities
{

    public   class Pregunta : EntityBase
    {
        [Required]
        [Display(Name = "Supported Files .png | .jpg")]
      
        public HttpPostedFileBase File { get; set; }

        [DataMember]
        public override int Id { get; set; }

        [DataMember]
        [DisplayName("Pregunta")]
        [Required]
        public string LaPregunta { get; set; }

        [DataMember]
        [Required]
        public Nivel nivel { get; set; }
        public List<Nivel>ListaNivel { get; set; }
        [DataMember]
        [DisplayName("Imagen")]
        public string Imagen { get; set; }

        [DataMember]
        public TipoPregunta tipoPregunta { get; set; }
        public List<TipoPregunta> ListatipoPregunta { get; set; }
        [DataMember]
        public List<Respuesta> ListaRespuesta{ get; set; }
        [DataMember]
        public List<Pregunta> ListaPregunta { get; set; }
        public bool SubPregunta { get; set; }
        public Categoria categoria { get; set; }
        public List< Categoria> ListaCategoria { get; set; }
        public List<MultipleChoice> ListaMC { get; set; }
        public Pregunta ()
        {
            ListaRespuesta = new List<Respuesta>();
            nivel = new Nivel();
            categoria = new Categoria();
            tipoPregunta = new TipoPregunta();
            ListaCategoria = new List<Categoria>();
            ListaNivel = new List<Nivel>();
            ListatipoPregunta = new List<TipoPregunta>();
            ListaRespuesta = new List<Respuesta>();
            ListaMC = new List<MultipleChoice>();
            ListaPregunta = new List<Pregunta>();
        }

        public  Pregunta(string _pregunta,int _nivel,int _TipoPregunta,int _categoria,string laImagen)
        {
            LaPregunta = _pregunta;
            nivel = new Nivel();
            nivel.Id = _nivel;
            tipoPregunta = new TipoPregunta();
            tipoPregunta.Id = _TipoPregunta;
            categoria = new Categoria();
            categoria.Id = _categoria;
            Imagen = laImagen;
            ListaRespuesta = new List<Respuesta>();

        }
}
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
   public class PreguntaCategoria :EntityBase
    {

        public override int Id { get; set; }
        public List<Categoria> ListaCategorias { get; set; }
        public Categoria Categorias { get; set; }
        public Pregunta Pregunta { get; set; }
        public TipoPregunta TipoPregunta { get; set; }
        public PreguntaCategoria ()
        {
            ListaCategorias = new List<Categoria>();
            Categorias = new Categoria();
            Pregunta = new Pregunta();
            TipoPregunta = new TipoPregunta();
        }
        public PreguntaCategoria(int id_C, int id_P)
        {
            
            ListaCategorias = new List<Categoria>();
            Categorias = new Categoria();
            Categorias.Id = id_C;
            TipoPregunta = new TipoPregunta();
            Pregunta = new Pregunta();
            Pregunta.Id = id_P;
        }
    }
}

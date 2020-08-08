using Entities;
using Negocio;
using System;

using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Evaluaciones.Models
{
    public  class PreguntaModels
    {
        public List<Categoria> ListaCategoria { get; set; }
        public int Id_Categoria { get; set; }
        public List<TipoPregunta> ListaTipo { get; set; }
        public string Id_Tipo { get; set; }
        public List<Nivel> ListaNivel { get; set; }
        public string Id_Nivel { get; set; }
        public string Pregunta { get; set; }
        
        public string Imagen { get; set; }

        public PreguntaModels()
        {
            CategoriaComponent categoriaComponent = new CategoriaComponent();
            ListaCategoria = new List<Categoria>();
            ListaCategoria = categoriaComponent.Read();


            TipoPreguntaComponent tipoPreguntaComponent = new TipoPreguntaComponent();
            ListaTipo = new List<TipoPregunta>();
            ListaTipo = tipoPreguntaComponent.Read();

            NivelComponent nivelComponent = new NivelComponent();
            ListaNivel = new List<Nivel>();
            ListaNivel = nivelComponent.Read();
        }


        public static List<Pregunta> ListCategoriaOrdenada()
        {
            PreguntaComponent preguntaComponent = new PreguntaComponent();

            var listaPregunta = preguntaComponent.Read();
            Pregunta pregunta = new Pregunta();
            pregunta.Id = 0;
            pregunta.LaPregunta = "";
            listaPregunta.Add(pregunta);
            List<Pregunta> objListOrder = new List<Pregunta>();
            objListOrder = listaPregunta.OrderBy(o => o.Id).ToList();
            objListOrder.Select(y =>
                                new
                                {
                                    Id = y.Id,
                                    LaCategoria = y.LaPregunta

                                });
            return objListOrder;

        }


    }
}
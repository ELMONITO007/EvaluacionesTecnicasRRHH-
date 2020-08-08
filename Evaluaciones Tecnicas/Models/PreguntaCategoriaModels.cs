using Entities;
using Negocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Evaluaciones.Models
{
    public class PreguntaCategoriaModels
    {
        [DisplayName("ID")]
        public int ID_Pregunta { get; set; }
        public string Pregunta { get; set; }

        [DisplayName("ID")]
        public int ID_Categoria { get; set; }
        public string Categoria { get; set; }

        [DisplayName("ID")]
        public int ID_TipoPregunta { get; set; }
        public string TipoPregunta { get; set; }
        public List<Categoria> categorias { get; set; }

        public  List<PreguntaCategoriaModels> Read()
        {

            PreguntaComponent preguntaComponent = new PreguntaComponent();
            List<Pregunta> ListaPreguntas = new List<Pregunta>();
            ListaPreguntas = preguntaComponent.Read();
            List<PreguntaCategoriaModels> result = new List<PreguntaCategoriaModels>();
            foreach (Pregunta item in ListaPreguntas)
            {
                PreguntaCategoriaModels preguntaCategoriaModels = new PreguntaCategoriaModels();
                preguntaCategoriaModels.Pregunta = item.LaPregunta;
                preguntaCategoriaModels.ID_Pregunta = item.Id;
                preguntaCategoriaModels.ID_Categoria = 1;
                preguntaCategoriaModels.Categoria = "";
                preguntaCategoriaModels.ID_TipoPregunta = item.tipoPregunta.Id;
                preguntaCategoriaModels.TipoPregunta = item.tipoPregunta.TipoDePregunta;
                result.Add(preguntaCategoriaModels);


            }

            //PreguntaCategoriaComponent preguntaCategoriaComponent = new PreguntaCategoriaComponent();

            //List<PreguntaCategoria> preguntaCategorias = new List<PreguntaCategoria>();
            //preguntaCategorias = preguntaCategoriaComponent.Read();


           
            //result = LoadCategoriaModels(preguntaCategorias);
            return result;


        }
        public List<PreguntaCategoriaModels> Read(int ID_Categoria)
        {
            PreguntaCategoriaComponent preguntaCategoriaComponent = new PreguntaCategoriaComponent();

            List<PreguntaCategoria> preguntaCategorias = new List<PreguntaCategoria>();
            preguntaCategorias = preguntaCategoriaComponent.Read(ID_Categoria);


            List<PreguntaCategoriaModels> result = new List<PreguntaCategoriaModels>();
            result = LoadCategoriaModels(preguntaCategorias);
            return result;


        }

        public List<PreguntaCategoriaModels> Read(Int16 ID_Pregunta)
        {
            PreguntaCategoriaComponent preguntaCategoriaComponent = new PreguntaCategoriaComponent();

            List<PreguntaCategoria> preguntaCategorias = new List<PreguntaCategoria>();
            preguntaCategorias = preguntaCategoriaComponent.Read(ID_Pregunta);


            List<PreguntaCategoriaModels> result = new List<PreguntaCategoriaModels>();
            result = LoadCategoriaModels(preguntaCategorias);
            return result;


        }
        public List<PreguntaCategoriaModels> Read(string Pregunta)
        {
            PreguntaCategoriaComponent preguntaCategoriaComponent = new PreguntaCategoriaComponent();

            List<PreguntaCategoria> preguntaCategorias = new List<PreguntaCategoria>();
            preguntaCategorias = preguntaCategoriaComponent.Read(Pregunta);


            List<PreguntaCategoriaModels> result = new List<PreguntaCategoriaModels>();
            result = LoadCategoriaModels(preguntaCategorias);
            return result;


        }
        public List<PreguntaCategoriaModels> Buscar(string _ID_Pregunta, string _IDCategoria, string preguntaParcial)
        {
            List<PreguntaCategoriaModels> result = new List<PreguntaCategoriaModels>();
               string cadena = "select pc.ID_Pregunta,Pregunta,pc.ID_Categoria,Categoria,p.ID_TipoPregunta,tp.TipoPregunta from PreguntaCategoria as pc inner join Pregunta as p on pc.ID_Pregunta=p.ID_Pregunta inner join Categoria as c on pc.ID_Categoria=c.ID_Categoria inner join TipoPregunta as tp on tp.ID_TipoPregunta=p.ID_TipoPregunta  where p.Activo=1 and SubPregunta=0 ";
      
      
          
           if (_ID_Pregunta !="0" && _IDCategoria == "0" &&  preguntaParcial == "")
            {
                cadena = cadena + "and pc.ID_Pregunta=" + _ID_Pregunta + " order by Pregunta";
                result = Read(cadena);
            }
            else if (_ID_Pregunta == "0" && _IDCategoria != "0" && preguntaParcial == "")
            {
                cadena = cadena + "and pc.ID_Categoria="+_IDCategoria+" order by Pregunta";
                result = Read(cadena);
            }

            else if (_ID_Pregunta == "0" && _IDCategoria == "0" && preguntaParcial != "")
            {
                cadena = cadena + "and pregunta like '%" +preguntaParcial + "%' order by Pregunta";
                result = Read(cadena);
            }
            else
            {
                result = Read();
            }
            return result;
        }



        public List<PreguntaCategoriaModels> LoadCategoriaModels(List<PreguntaCategoria> preguntaCategorias)
        {
            PreguntaCategoriaComponent preguntaCategoriaComponent = new PreguntaCategoriaComponent();

         


            List<PreguntaCategoriaModels> result = new List<PreguntaCategoriaModels>();
            foreach (PreguntaCategoria item in preguntaCategorias)
            {
                PreguntaCategoriaModels preguntaCategoriaModels = new PreguntaCategoriaModels();
                preguntaCategoriaModels.ID_Categoria = item.Categorias.Id;
                preguntaCategoriaModels.ID_Pregunta = item.Pregunta.Id;
                preguntaCategoriaModels.Pregunta = item.Pregunta.LaPregunta;
                preguntaCategoriaModels.Categoria = item.Categorias.LaCategoria;
                preguntaCategoriaModels.ID_TipoPregunta = item.TipoPregunta.Id;
                preguntaCategoriaModels.TipoPregunta = item.TipoPregunta.TipoDePregunta;
                result.Add(preguntaCategoriaModels);

            }
            return result;


        }


        public PreguntaCategoriaModels ObtenerCategoria(int id)
        {
            try
            {
                PreguntaCategoriaComponent preguntaCategoriaComponent = new PreguntaCategoriaComponent();
                PreguntaCategoria preguntaCategorias = new PreguntaCategoria();
                preguntaCategorias = preguntaCategoriaComponent.ReadBy(id);
                PreguntaCategoriaModels preguntaCategoriaModels = new PreguntaCategoriaModels();
                preguntaCategoriaModels.ID_Categoria = preguntaCategorias.Categorias.Id;
                preguntaCategoriaModels.ID_Pregunta = preguntaCategorias.Pregunta.Id;
                preguntaCategoriaModels.Pregunta = preguntaCategorias.Pregunta.LaPregunta;
                preguntaCategoriaModels.Categoria = preguntaCategorias.Categorias.LaCategoria;
                preguntaCategoriaModels.categorias = preguntaCategoriaComponent.obtenerCategoria(id.ToString()).ListaCategorias;

                return preguntaCategoriaModels;
            }
            catch (Exception e)
            {

                throw;
            }
          


        }

    }
    }
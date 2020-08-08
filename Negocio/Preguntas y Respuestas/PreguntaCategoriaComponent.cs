using Data;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class PreguntaCategoriaComponent : Component<PreguntaCategoria>
    {
        public override PreguntaCategoria Create(PreguntaCategoria objeto)
        {
            PReguntaCategoriaDAC pReguntaCategoriaDAC = new PReguntaCategoriaDAC();
            return pReguntaCategoriaDAC.Create(objeto);
        }

        public override void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public  void Delete(PreguntaCategoria preguntaCategoria )
        {
            PReguntaCategoriaDAC pReguntaCategoriaDAC = new PReguntaCategoriaDAC();
            pReguntaCategoriaDAC.Delete(preguntaCategoria);
        }

        public override List<PreguntaCategoria> Read()
        {
            PReguntaCategoriaDAC pReguntaCategoriaDAC = new PReguntaCategoriaDAC();
            return pReguntaCategoriaDAC.Read();
        }
        public  List<PreguntaCategoria> Read(int Id_Categoria)
        {
            PReguntaCategoriaDAC pReguntaCategoriaDAC = new PReguntaCategoriaDAC();
            return pReguntaCategoriaDAC.Read(Id_Categoria);
        }
        public List<PreguntaCategoria> Read(Int16 Id_Pregunta)
        {
            PReguntaCategoriaDAC pReguntaCategoriaDAC = new PReguntaCategoriaDAC();
            return pReguntaCategoriaDAC.Read(Id_Pregunta);
        }
        public List<PreguntaCategoria> Read(string pregunta)
        {
            PReguntaCategoriaDAC pReguntaCategoriaDAC = new PReguntaCategoriaDAC();
            return pReguntaCategoriaDAC.Read(pregunta);
        }



        public override PreguntaCategoria ReadBy(int id)
        {
            PReguntaCategoriaDAC pReguntaCategoriaDAC = new PReguntaCategoriaDAC();
            return pReguntaCategoriaDAC.ReadBy(id);

        }
        public  PreguntaCategoria ReadBy(PreguntaCategoria preguntaCategoria)
        {
            PReguntaCategoriaDAC pReguntaCategoriaDAC = new PReguntaCategoriaDAC();
            return pReguntaCategoriaDAC.ReadBy(preguntaCategoria);

        }
      


        public override void Update(PreguntaCategoria objeto)
        {
            throw new NotImplementedException();
        }


        public PreguntaCategoria obtenerCategoria(string id)

        {
            CategoriaComponent categoriaComponent = new CategoriaComponent();
            List<Categoria> categorias = new List<Categoria>();
            categorias = categoriaComponent.Read();
            Int16 pregunta = Int16.Parse(id);
            List<PreguntaCategoria> ListaCompleta = new List<PreguntaCategoria>();
            ListaCompleta = Read(pregunta);
            PreguntaCategoria ListaCNueva= new PreguntaCategoria();
            foreach (Categoria item in categorias)
            {
                int aux = 0;

                foreach (PreguntaCategoria pc in ListaCompleta)
                {
                    if (item.Id==pc.Categorias.Id)
                    {
                        aux = 1;

                    }
                }
                if (aux==0)
                {
                    Categoria preguntaCategoria = new Categoria();
                    preguntaCategoria.Id = item.Id;
                    preguntaCategoria.LaCategoria = item.LaCategoria;
                    ListaCNueva.ListaCategorias.Add(preguntaCategoria);

                }

            }




            return ListaCNueva;


        }
    }
}

using Entities;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Evaluaciones.Models
{
    public static class CategoriaModels
    {
        public static List<Categoria> ListCategoriaOrdenada()
        {
            CategoriaComponent categoriaComponent = new CategoriaComponent();

            var listaCategoria = categoriaComponent.Read();
            Categoria categoriaVacia = new Categoria();
            categoriaVacia.Id = 0;
            categoriaVacia.LaCategoria = "";
            listaCategoria.Add(categoriaVacia);
            List<Categoria> objListOrder = new List<Categoria>();
            objListOrder = listaCategoria.OrderBy(o => o.Id).ToList();
            objListOrder.Select(y =>
                                new
                                {
                                    Id = y.Id,
                                    LaCategoria = y.LaCategoria

                                });
            return objListOrder;

        }

    }
}
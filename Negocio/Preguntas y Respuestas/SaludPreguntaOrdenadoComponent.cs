using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class SaludPreguntaOrdenadoComponent
    {
        public bool PreguntaConCategoria(Pregunta pregunta)
        {
            PreguntaCategoriaComponent preguntaCategoriaComponent = new PreguntaCategoriaComponent();
            Int16 id = short.Parse(pregunta.Id.ToString());

            List<PreguntaCategoria> pregunta1 = new List<PreguntaCategoria>();
            pregunta1 = preguntaCategoriaComponent.Read(id);
            if (pregunta1 is null || pregunta1.Count == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public bool VerificarMasDeTresItems(SaludPreguntaOrden pregunta)
        {
            if (pregunta.pregunta.ListaRespuesta.Count >= 3)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public bool PrimerElemento(SaludPreguntaOrden pregunta)
        {
            List<int> ListaInicial = new List<int>();


            foreach (Orden item in pregunta.pregunta.ListaRespuesta)
            {
                ListaInicial.Add(item.NumeroOrden);
            }

            ListaInicial.Sort();

            if (ListaInicial.Count==0)
            {
                return false;
            }
            else
            {
                int primervalor = ListaInicial.ElementAt(0);
                int total = ListaInicial.Count;
                int ultimoValor = ListaInicial.ElementAt(total - 1);
                if (primervalor != 1)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
           

           
        }

        public bool FaltanElementos(SaludPreguntaOrden pregunta)
        {
            List<int> ListaInicial = new List<int>();


            foreach (Orden item in pregunta.pregunta.ListaRespuesta)
            {
                ListaInicial.Add(item.NumeroOrden);
            }

            ListaInicial.Sort();

          
            int total = ListaInicial.Count;
            int ultimoValor = ListaInicial.ElementAt(total - 1);
            int Result = total - ultimoValor;
            if (Result==0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public SaludPreguntaOrden VerificarOrdenado( Pregunta pregunta)
        {

            
            SaludPreguntaOrden saludPreguntaOrden=new SaludPreguntaOrden();
            OrdenComponent ordenComponent = new OrdenComponent();
            saludPreguntaOrden.pregunta = pregunta;
            Orden ordenes = new Orden();
            ordenes = ordenComponent.listaRespuestaOrdenAlAzar(pregunta.Id);
            foreach (Orden item in ordenes.listaOrden)
            {
                saludPreguntaOrden.pregunta.ListaRespuesta.Add(item);
            }

            saludPreguntaOrden.TieneCategoria = PreguntaConCategoria(pregunta);
            saludPreguntaOrden.MasDeTresElementos = VerificarMasDeTresItems(saludPreguntaOrden);
            saludPreguntaOrden.PrimerElemento = PrimerElemento(saludPreguntaOrden);
            saludPreguntaOrden.VeriricarElemento =PrimerElemento(saludPreguntaOrden);

            if (saludPreguntaOrden.MasDeTresElementos == true && saludPreguntaOrden.PrimerElemento==true && saludPreguntaOrden.VeriricarElemento==true&&saludPreguntaOrden.TieneCategoria==true)
            {
                saludPreguntaOrden.SaludDeLaPregunta = true;
            }
            else
            {
                saludPreguntaOrden.SaludDeLaPregunta = false;
            }
            return saludPreguntaOrden;

        }
    }






}

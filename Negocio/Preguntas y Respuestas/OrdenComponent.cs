using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using Entities;

namespace Negocio
{
  public  class OrdenComponent : Component<Orden>
    {
        public override Orden Create(Orden objeto)
        {

            Orden result = default(Orden);
            var orden = new OrdenDAC();

            result = orden.Create(objeto);
            return result;
        }

        public override void Delete(int id)
        {
            var orden = new OrdenDAC();
            orden.Delete(id);
        }

        public override List<Orden> Read()
        {
            List<Orden> result = default(List<Orden>);
            var orden = new OrdenDAC();
            result = orden.Read();
            return result;
        }


        public override Orden ReadBy(int id)
        {
            Orden result = default(Orden);
            var orden = new OrdenDAC();
            result = orden.ReadBy(id);
            return result;
        }

        public override void Update(Orden objeto)
        {
            var orden = new OrdenDAC();
            orden.Update(objeto);


        }
        public List<Orden> listaRespuestaCorrecta(int id_pregunta)
        {


            List<Orden> result = default(List<Orden>);
            var orden = new OrdenDAC();
            result = orden.listaRespuestaCorrecta(id_pregunta);
     
            return result;
        }
        public Orden listaRespuestaOrdenAlAzar(int id_pregunta)
        {


            Orden result = new Orden();
            PreguntaComponent preguntaComponent = new PreguntaComponent();
            result.pregunta = preguntaComponent.ReadBy(id_pregunta);
            var ordenDAC = new OrdenDAC();
            result.listaOrden = ordenDAC.listaRespuestaOrdenAlAzar(id_pregunta);

            return result;
        }
        public List<int> OrdenDiponible(int id_pregunta)
        {
            List<int> listaNumero = new List<int>();
            List<int> listaFinal = new List<int>();
            List<int> listaComparar = new List<int>();
            listaComparar.Add(1);
            listaComparar.Add(2);
            listaComparar.Add(3);
            listaComparar.Add(4);
            listaComparar.Add(5);
            List<Orden> result = default(List<Orden>);
            result = listaRespuestaCorrecta(id_pregunta);
            foreach (Orden item in result)
            {
                
                    listaNumero.Add (item.NumeroOrden);
                
            }

            foreach (int item in listaComparar)
            {
                int aux = 0;
                foreach (int lista in listaNumero)
                {
                    if (item==lista)
                    {
                        aux = 1;
                        break;
                    }
                }
                if (aux==0)
                {
                    listaFinal.Add(item);
                }

            }
            return listaFinal;
        }
    }
}

using Data;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class SaludCategoriaComponent : Component<SaludCategoria>
    {
        SaludCategoria saludCategoria = new SaludCategoria();
        List<SaludCategoria> ListaSaludPreguntas = new List<SaludCategoria>();
        public SaludCategoriaComponent(int id_TipoPregunta)
        {
           
            saludCategoria = ReadBy(id_TipoPregunta);
            ObtenerSaludDeLasPreguntas();
        }
        public override SaludCategoria Create(SaludCategoria objeto)
        {
            throw new NotImplementedException();
        }

        public override void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public override List<SaludCategoria> Read()
        {
            throw new NotImplementedException();
        }

        public override SaludCategoria ReadBy(int id)
        {

            SaludCategoriaDAC saludCategoriaDAC = new SaludCategoriaDAC();
            return saludCategoriaDAC.ReadBy(id);
           
        }

        public override void Update(SaludCategoria objeto)
        {
            throw new NotImplementedException();
        }
        

        public bool VerificarTieneMasDe20Preguntas()

        {
            if (saludCategoria.ListaPreguntasTotal.Count==0)
            {
                return false;
            }
            else
            {
                if (saludCategoria.ListaPreguntasTotal.Count > 19)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
          

        }
        int facil = 0;
        int medio = 0;
        int dificil = 0;

        public List<SaludCategoria>ObtenerSaludDeLasPreguntas()
        {
            

            if (saludCategoria is null)
            {

            }
            else
            {
                foreach (SaludCategoria item in saludCategoria.ListaPreguntasTotal)
                {
                    SaludPreguntaComponent saludPreguntaComponent = new SaludPreguntaComponent();
                    SaludCategoria saludCategoria = new SaludCategoria();
                    saludCategoria.saludPregunta = saludPreguntaComponent.VerificarSalud(item.Pregunta);
                    saludCategoria.Pregunta = item.Pregunta;
                    saludCategoria.SaludCategtoria = saludCategoria.saludPregunta.SaludDeLaPregunta;
                    if (item.Pregunta.nivel.ElNivel=="Facil")
                    {
                       facil++;
                    }

                    else if (item.Pregunta.nivel.ElNivel == "Medio")
                        
                    {
                       medio++;
                    }
                    else
                    {
                        dificil++;
                    }
                    ListaSaludPreguntas.Add(saludCategoria);
                }
            }
           




            return ListaSaludPreguntas;

        }

        

        public bool TieneMasDe20PreguntasConBuenaSalud()

        {
            int buenaSalud = 0;

            if (ListaSaludPreguntas.Count==0)
            {
                return false;
            }
            else
            {
                foreach (SaludCategoria item in ListaSaludPreguntas)
                {
                    if (item.SaludCategtoria)
                    {
                        buenaSalud++;
                    }
                }
                if (buenaSalud > 19)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
          

        }


        public SaludCategoria verificarSaludTipoPregunta()
        {
            CategoriaComponent categoriaComponent = new CategoriaComponent();
            SaludCategoria result = new SaludCategoria();

            result.CantidadDePreguntas = ListaSaludPreguntas.Count();
            result.TieneMasDe20Preguntas = VerificarTieneMasDe20Preguntas();
            result.TieneMasDe20PreguntasConBuenaSalud = TieneMasDe20PreguntasConBuenaSalud();
            result.Pregunta = saludCategoria.Pregunta;
     
            result.ListaPreguntasTotal = ListaSaludPreguntas;

            if (result.TieneMasDe20PreguntasConBuenaSalud )
            {
                result.SaludCategtoria = true;
            }
            else
            {
                result.SaludCategtoria = false;
            }
            result.nivelBajo = facil;
            result.nivelMedio = medio;
            result.nivelAlto = dificil;
       
            return result;
        }



        public int CantidadPreguntaConBuenaSalud()
        {
            int cantidad = 0;
            SaludCategoria result = new SaludCategoria();
            result = verificarSaludTipoPregunta();
            foreach (SaludCategoria item in result.ListaPreguntasTotal)
            {
                if (item.saludPregunta.SaludDeLaPregunta)
                {
                    cantidad++;
                }
            }



            return cantidad;        
        
        
        }


    }
}

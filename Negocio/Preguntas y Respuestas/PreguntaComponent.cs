using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Web.Hosting;
using Data;
using Entities;

namespace Negocio
{
    public class PreguntaComponent : Component<Pregunta>
    {
        public  Pregunta CreateSubPregunta(Pregunta objeto)
        {

            Pregunta result = default(Pregunta);
            var preguntaNivel = new PreguntaDAC();
            objeto.SubPregunta = true;
            result = preguntaNivel.Create(objeto);
            return result;
        }
        public override Pregunta Create(Pregunta objeto)
        {
            


            Pregunta result = default(Pregunta);
            var preguntaNivel = new PreguntaDAC();
            objeto.SubPregunta = false;
            Pregunta verificar = new Pregunta();
            verificar = preguntaNivel.ReadByPregunta(objeto.LaPregunta);
            if (verificar!= null)
            {
                result = null;
                return result;
            }
            else
            
            {
                if (objeto.File is null)
                {
                    objeto.Imagen = "Sin imagen";
                }
                else
                {
                    string filename = Path.GetFileNameWithoutExtension(objeto.File.FileName);
                    string FileExtension = Path.GetExtension(objeto.File.FileName);
                    string ruta = "C:/imagenes/" + filename + FileExtension;
                    objeto.Imagen = filename + FileExtension;
                    if (VerificarExisteArchivo(ruta))
                    {
                        objeto.Imagen = "C:/imagenes/" + filename + FileExtension;
                    }
                    else
                    {
                        objeto.Imagen = "C:/imagenes/" + filename + FileExtension;
                        objeto.File.SaveAs(ruta);
                    }
                }

                result = preguntaNivel.Create(objeto);
                return result;
            }
           




        }




        public bool VerificarExisteArchivo(string path)
        {
            if (File.Exists(path))
            {
                return true;
            }
            else
            {
                return false;
            }


        }


        public override void Delete(int id)
        {
            var preguntaNivel = new PreguntaDAC();
            preguntaNivel.Delete(id);
        }

        public override List<Pregunta> Read()
        {
            List<Pregunta> result = default(List<Pregunta>);
            var preguntaNivel = new PreguntaDAC();
            result = preguntaNivel.Read();
            return result;
        }


        public override Pregunta ReadBy(int id)
        {
            Pregunta result = new Pregunta();
            var preguntaNivel = new PreguntaDAC();
            result = preguntaNivel.ReadBy(id);
            return result;
        }
        public  Pregunta ReadBySubPregunta(string laPregunta)
        {
            Pregunta result = new Pregunta();
            var preguntaNivel = new PreguntaDAC();
            result = preguntaNivel.ReadBySubPregunta(laPregunta);
            return result;
        }
        public  Pregunta ReadBySubPregunta(int id)
        {
            Pregunta result = new Pregunta();
            var preguntaNivel = new PreguntaDAC();
            result = preguntaNivel.ReadBySubPregunta(id);
            return result;
        }
        public override void Update(Pregunta objeto)
        {
            var preguntaNivel = new PreguntaDAC();
            preguntaNivel.Update(objeto);


        }


     

        public List<Nivel> ObtenerCantidadDePreguntasParaExamen(Pregunta pregunta,int cantidad)
        {
            int facil = 0;
            int medio = 0;
            int dificil = 0;
            SaludCategoriaComponent saludCategoriaComponent = new SaludCategoriaComponent(pregunta.categoria.Id);
            foreach (var item in saludCategoriaComponent.saludCategoria.ListaPreguntasTotal)
            {

                if (item.Pregunta.nivel.ElNivel == "Facil")
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

                }


                List<Nivel> result = new List<Nivel>();
           





            int total = facil + medio + dificil;

            Nivel nivelFacil = new Nivel();
            nivelFacil.ElNivel = "Facil";
            double totalFacilD= (100 * facil) / total;
            int totalFacil = (int)Math.Round(totalFacilD);
            nivelFacil.porcentajePorPregunta = (cantidad * totalFacil) / 100;
       


            Nivel nivelMedio = new Nivel();
            nivelMedio.ElNivel = "Medio";
            double totalMediod= (100 * medio) / total;
            int totalMedio = (int)Math.Round(totalMediod);
            nivelMedio.porcentajePorPregunta = (cantidad * totalMedio) / total;

       


            Nivel nivelDificil = new Nivel();
            nivelDificil.ElNivel = "Dificil";
            double totalDificild = (100 * dificil) / total;
            int totalDificil = (int)Math.Round(totalDificild);
            nivelDificil.porcentajePorPregunta =(cantidad*totalDificil)/100;


            int totalCalculado = nivelFacil.porcentajePorPregunta + nivelMedio.porcentajePorPregunta + nivelDificil.porcentajePorPregunta;
            int totalAControlar = totalCalculado - cantidad;

            if (totalCalculado>0)
            {
                if (nivelFacil.porcentajePorPregunta > nivelMedio.porcentajePorPregunta)
                {
                    nivelFacil.porcentajePorPregunta = nivelFacil.porcentajePorPregunta - totalAControlar;
                }
                else
                {
                    nivelMedio.porcentajePorPregunta = nivelMedio.porcentajePorPregunta - totalAControlar;

                }


               
            }
            else if (totalCalculado < 0)
            {
                if (nivelFacil.porcentajePorPregunta < nivelMedio.porcentajePorPregunta)
                {
                    nivelFacil.porcentajePorPregunta = nivelFacil.porcentajePorPregunta + totalAControlar;
                }
                else
                {
                    nivelMedio.porcentajePorPregunta = nivelMedio.porcentajePorPregunta + totalAControlar;

                }
            }

            result.Add(nivelDificil);
            result.Add(nivelFacil);
            result.Add(nivelMedio);
            return result;

        }


        public List<Pregunta> ObtenerPreguntasAlAzar(Pregunta pregunta,int cantidad)
        {
            int contar = 0;

            List<Pregunta> preguntas = new List<Pregunta>();
            List<Pregunta> result = default(List<Pregunta>);
            var preguntaNivel = new PreguntaDAC();
            List<Nivel> porcentajePorNivel = new List<Nivel>();
            porcentajePorNivel = ObtenerCantidadDePreguntasParaExamen(pregunta, cantidad);
            int facil = 0;
            int medio = 0;
            int dificil = 0;
            foreach (var item in porcentajePorNivel)
            {
                if (item.ElNivel == "Facil")
                {
                    facil=item.porcentajePorPregunta;
                }

                else if (item.ElNivel == "Medio")

                {
                    medio=item.porcentajePorPregunta;
                }
                else
                {
                    dificil=item.porcentajePorPregunta;
                }

            }
        


           


            preguntas = obtenerRespuestaPorNivelCategoriayCantidad("Facil", pregunta.categoria.Id, facil);
            preguntas.AddRange(obtenerRespuestaPorNivelCategoriayCantidad("Dificil", pregunta.categoria.Id, dificil));
            preguntas.AddRange(obtenerRespuestaPorNivelCategoriayCantidad("Medio", pregunta.categoria.Id, medio));

            return preguntas;
        }



        public List<Pregunta> obtenerRespuestaPorNivelCategoriayCantidad(string nivel,int categoria, int cantidad)

        {
            List<Pregunta> preguntas = new List<Pregunta>();
            List<Pregunta> result = default(List<Pregunta>);
            var preguntaNivel = new PreguntaDAC();

            Pregunta pregunta = new Pregunta();
            pregunta.nivel.ElNivel = nivel;
            pregunta.categoria.Id = categoria;

            result = preguntaNivel.ObtenerPreguntarAlAzarPorNivelYCategoria(pregunta);
            //Verificar la salud de las pregunta
            int contar = 0;
            foreach (Pregunta item in result)
            {
                SaludPreguntaComponent saludPreguntaComponent = new SaludPreguntaComponent();
                SaludPregunta saludPregunta = new SaludPregunta();
                saludPregunta = saludPreguntaComponent.VerificarSalud(item);
                if (saludPregunta.SaludDeLaPregunta)
                {

                    //Obtener respuestas

                    if (item.tipoPregunta.TipoDePregunta == "MultipleChoice")
                    {
                        MultipleChoiceComponent multipleChoiceComponent = new MultipleChoiceComponent();
                        item.ListaMC = multipleChoiceComponent.listaRespuestaMultipleChoiceAlAzar(item.Id).ListaMultipleChoice;

                    }
                    else if (item.tipoPregunta.TipoDePregunta == "MultipleChoiceCompuesto")
                    {
                        MultipleChoiceCompustoComponent multipleChoiceCompustoComponent = new MultipleChoiceCompustoComponent();
                        item.ListaPregunta = multipleChoiceCompustoComponent.ReadByPregunta(item.Id).ListaSubPreguntas;
                    }


                    preguntas.Add(item);
                    contar++;
                }


                if (contar == cantidad)
                {
                    break;
                }


            }
            return preguntas;

        }
        public List<Pregunta> LeerPorTipoDePregunta(TipoPregunta tipoPregunta)
        {
            List<Pregunta> result = new List<Pregunta>();
            PreguntaDAC preguntaDAC = new PreguntaDAC();
            result = preguntaDAC.LeerPorTipoDePregunta(tipoPregunta.Id);

            return result;
        }



        public bool ReadBy(string pregunta)
        {
            PreguntaDAC preguntaDAC = new PreguntaDAC();
            return preguntaDAC.ReadBy(pregunta);
        }


        }
}

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
                    string ruta = HostingEnvironment.MapPath("~/imagenes/" + filename + FileExtension);

                    if (VerificarExisteArchivo(ruta))
                    {
                        objeto.Imagen = "~/imagenes/" + filename + FileExtension;
                    }
                    else
                    {
                        objeto.Imagen = "~/imagenes/" + filename + FileExtension;
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
        public List<Pregunta> ObtenerPreguntasAlAzar(Pregunta pregunta,int cantidad)
        {
            int contar = 0;

            List<Pregunta> preguntas = new List<Pregunta>();
            List<Pregunta> result = default(List<Pregunta>);
            var preguntaNivel = new PreguntaDAC();
            result = preguntaNivel.ObtenerPreguntarAlAzarPorNivelYCategoria(pregunta);
            //Verificar la salud de las pregunta
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

                    //else if (item.tipoPregunta.TipoDePregunta == "Orden")
                    //{
                    //    OrdenComponent ordenComponent = new OrdenComponent();
                    //    item.ListaOrden(ordenComponent.rea)
                    //}
                    preguntas.Add(item);
                    contar++;
                }
               

                if (contar==cantidad)
                {
                    break;
                }


            }


            return preguntas;
        }



        //public List<Pregunta> obtenerpregunta(Nivel nivel, int Cantidad, Categoria categoria)
        //{
        //    List<Pregunta> result = default(List<Pregunta>);
        //    Pregunta temp = new Pregunta();
        //    temp.nivel = nivel;
        //    temp.categoria = categoria;

        //    result = ObtenerPreguntasAlAzar(temp, Cantidad);


        //    return result;
        //}


        //public List<Pregunta> obtenerLaspreguntas(List<Pregunta> preguntas, int CantidadFacil, int CantidadMedio, int CantidadDificil)
        //{
        //    List<Pregunta> result = new List<Pregunta>();
        //    Pregunta temp = new Pregunta();
        //    foreach (var item in preguntas)
        //    {
        //        List<Pregunta> listaTemp = default(List<Pregunta>);
        //        if (item.nivel.ElNivel=="Facil")
        //        {
        //            listaTemp = obtenerpregunta(item.nivel, CantidadFacil, item.categoria);
        //            foreach (Pregunta item2 in listaTemp)
        //            {
        //                result.Add(item2);
        //            }
        //        }
        //        else if (item.nivel.ElNivel == "Medio")
        //        {

        //            listaTemp = obtenerpregunta(item.nivel, CantidadMedio, item.categoria);
        //            foreach (Pregunta item2 in listaTemp)
        //            {
        //                result.Add(item2);
        //            }
        //        }
        //        else if (item.nivel.ElNivel == "Dificil")
        //        {
        //            listaTemp = obtenerpregunta(item.nivel, CantidadDificil, item.categoria);
        //            foreach (Pregunta item2 in listaTemp)
        //            {
        //                result.Add(item2);
        //            }
        //        }

        //    }
        //    return result;
        //}
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

﻿using Data.Examen;
using Entities;
using Entities.Examen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Examen
{
    public class ExamenComponent : IRepository<Entities.Examen.Examen>
    {
        public bool VerificarSiRieneExamenActivo(Entities.Examen.Examen entity)
        {

            ExamenDAC examenDAC = new ExamenDAC();
            Entities.Examen.Examen examen = new Entities.Examen.Examen();
            examen = examenDAC.ReadByEstado(entity.usuario.Id);
            if (examen is null)
            {
                return false;
            }
            else
            {
                return true;
            }

        }



        public Entities.Examen.Examen Create(Entities.Examen.Examen entity)
        {


            //Veriificar si el usuario tiene examenes activos
            if (VerificarSiRieneExamenActivo(entity))
            {
                entity.error = "Activo";
            }
            else
            {

                //Verifico la salud del examen
                SaludCategoria saludCategoria = new SaludCategoria();
                SaludCategoriaComponent saludCategoriaComponent = new SaludCategoriaComponent(entity.Categoria.Id);

                saludCategoria = saludCategoriaComponent.verificarSaludTipoPregunta();
                if (saludCategoria.TieneMasDe20PreguntasConBuenaSalud)
                {



                    //obtengo 20 pregunta al azar
                    PreguntaComponent preguntaComponent = new PreguntaComponent();
                    Pregunta pregunta = new Pregunta();
                    Pregunta enviar = new Pregunta();


                    enviar.categoria.Id = entity.Categoria.Id;
                    entity.listaPregunta = preguntaComponent.ObtenerPreguntasAlAzar(enviar, 20);
                    //Completo los datos del examen
                    entity.Fecha = DateTime.Parse(DateTime.Now.ToString("dd/MM/yyyy HH:mm"));
                    entity.Resultado = 0;
                    entity.Aprobado = false;
                    entity.Estado = "A realizar";

                    //Creo el examen
                    ExamenDAC examenComponent = new ExamenDAC();
                    examenComponent.Create(entity);
                    //Obtengo el ID del examen creado

                    Entities.Examen.Examen examen = new Entities.Examen.Examen();
                    examen = examenComponent.ReadBy(entity.Fecha);
                    int Id = examen.Id;

                    //guardo las preguntas en la Base

                    foreach (Pregunta item in entity.listaPregunta)
                    {
                        ExamenPregunta examenPregunta = new ExamenPregunta();
                        ExamenPreguntaComponent examenPreguntaComponent = new ExamenPreguntaComponent();
                       
                        examenPregunta.examen.Id = examen.Id;
                        examenPregunta.pregunta.Id = item.Id;
                        examenPregunta.correcta = false;
                        examenPreguntaComponent.Create(examenPregunta);
                    }
                    entity.error = "Creado";

                }
                else
                {
                    entity.error = "Salud";
                }
            }



            return entity;
        }

        public void Delete(int id)
        {
            ExamenComponent examenComponent = new ExamenComponent();
            examenComponent.Delete(id);
        }

        public List<Entities.Examen.Examen> Read()
        {
            ExamenDAC examenComponent = new ExamenDAC();
          List<  Entities.Examen.Examen> examen = new List<Entities.Examen.Examen>();
            List<Entities.Examen.Examen> Result = new List<Entities.Examen.Examen>();

            examen =  examenComponent.Read();

            foreach (Entities.Examen.Examen item in examen)
            {
                UsuariosComponent usuarios = new UsuariosComponent();
                CategoriaComponent categoriaComponent = new CategoriaComponent();
                Entities.Examen.Examen unExamen = new Entities.Examen.Examen();
                unExamen = item;
                unExamen.usuario = usuarios.ReadBy(item.usuario.Id);
                unExamen.Categoria = categoriaComponent.ReadBy(item.Categoria.Id);
                Result.Add(unExamen);
            }
            return Result;
        }

        public Entities.Examen.Examen ReadBy(int id)
        {

            ExamenPreguntaComponent examenPreguntaComponent = new ExamenPreguntaComponent();
            List<ExamenPregunta> ListaPreguntasExamen = new List<ExamenPregunta>();
            ListaPreguntasExamen = examenPreguntaComponent.ReadByExamen(id);


            ExamenDAC examenDAC = new ExamenDAC();
            Entities.Examen.Examen examen = new Entities.Examen.Examen();
            UsuariosComponent usuarios=new UsuariosComponent();
                examen= examenDAC.ReadBy(id);
            int a = examen.usuario.Id;
            examen.listaExamenPregunta = ListaPreguntasExamen;
            examen.usuario = usuarios.ReadBy(a);


           
            return examen;
        }

        public Entities.Examen.Examen VerExamenUsuario(int id)
        {
            //obtengo el ultimo examen a realizar
            Entities.Examen.Examen examen = new Entities.Examen.Examen();
            ExamenDAC examenComponent = new ExamenDAC();
            examen = examenComponent.ReadByUsuario(id);

            //obtengo las preguntas y respusta del examen

            ExamenPreguntaComponent examenPreguntaComponent = new ExamenPreguntaComponent();
            List<ExamenPregunta> ListaPreguntasExamen = new List<ExamenPregunta>();
            ListaPreguntasExamen = examenPreguntaComponent.ReadByExamen(examen.Id);


            //asigno las preguntas al examen
            examen.listaExamenPregunta = ListaPreguntasExamen;

            //Obtengo los datos del usuario
            UsuariosComponent usuariosComponent = new UsuariosComponent();
            examen.usuario = usuariosComponent.ReadBy(id);


            return examen;



        }





        public Entities.Examen.Examen ReadBy(DateTime Fecha)
        {
            ExamenDAC examenDAC = new ExamenDAC();
            return examenDAC.ReadBy(Fecha);
        }


        public void Terminarexamen(List<Pregunta> preguntas)
            { //


        
        
            }




        public void Update(Entities.Examen.Examen entity)
        {
            ExamenDAC examenDAC = new ExamenDAC();


            examenDAC.Update(entity);
        }
    }
}
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iTextSharp;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System.Web.Hosting;
using System.Diagnostics;

namespace Negocio.Servicios
{
 public   class CrearPDF
    {
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
        public void CrearPDFParaUsuarioExamen(UsuarioParaExamen usuarioParaExamen)
        {
            string ruta = HostingEnvironment.MapPath("~/PDF/" + usuarioParaExamen.usuarios.Email + "2.pdf");
            string rutaConPass = HostingEnvironment.MapPath("~/PDF/" + usuarioParaExamen.usuarios.Email + ".pdf");
            if (VerificarExisteArchivo(rutaConPass))
            {
                AbrirPDF(rutaConPass);
            }
            else
            {
                // Se crea el documento con el tamaño de página tradicional
                Document doc = new Document(PageSize.LETTER);
                // Indicamos donde vamos a guardar el documento
                PdfWriter writer = PdfWriter.GetInstance(doc,
                                new FileStream(HostingEnvironment.MapPath("~/PDF/" + usuarioParaExamen.usuarios.Email + "2.pdf"), FileMode.Create));

                
                // Se le coloca el título y el autor


                doc.AddCreator(usuarioParaExamen.sede.empresa.empresa);

                // Abrimos el archivo
                doc.Open();
                // Se crea el tipo de Font que vamos utilizar

                iTextSharp.text.Font _standardFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);


                // Creamos la imagen y le ajustamos el tamaño
                if (usuarioParaExamen.sede.empresa.empresa == "TB")
                {
                    iTextSharp.text.Image imagen = iTextSharp.text.Image.GetInstance(HostingEnvironment.MapPath("~/img/TB.png"));
                    imagen.BorderWidth = 0;
                    imagen.Alignment = Element.ALIGN_CENTER;
                    float percentage = 0.0f;
                    percentage = 75 / imagen.Width;
                    imagen.ScalePercent(percentage * 75);
                    // Insertamos la imagen en el documento
                    doc.Add(imagen);
                }
                else
                {
                    iTextSharp.text.Image imagen = iTextSharp.text.Image.GetInstance(HostingEnvironment.MapPath("~/img/TR.PNG"));
                    imagen.BorderWidth = 0;
                    imagen.Alignment = Element.ALIGN_RIGHT;
                    float percentage = 0.0f;
                    percentage = 150 / imagen.Width;
                    imagen.ScalePercent(percentage * 100);
                    // Insertamos la imagen en el documento
                    doc.Add(imagen);
                }




                // Se escribe el encabezamiento en el documento
                doc.Add(new Paragraph("Datos para acceder al sistemas de examenes"));
                doc.Add(Chunk.NEWLINE);

                // Se escribe el encabezamiento en el documento
                doc.Add(new Paragraph("Pagina Web: https://Evaluaciones.transener.com.ar"));
                doc.Add(Chunk.NEWLINE);

                // Se crean las tablas 
                PdfPTable tblPrueba = new PdfPTable(2);
                tblPrueba.WidthPercentage = 100;


                // Se configura el título de las columnas de la tabla
                PdfPCell clUsuario = new PdfPCell(new Phrase("Usuario", _standardFont));
                clUsuario.BorderWidth = 0;
                clUsuario.BorderWidthBottom = 0.75f;

                PdfPCell clContraseña = new PdfPCell(new Phrase("Contraseña", _standardFont));
                clContraseña.BorderWidth = 0;
                clContraseña.BorderWidthBottom = 0.75f;

                // se añade las celdas a la tabla
                tblPrueba.AddCell(clUsuario);
                tblPrueba.AddCell(clContraseña);

                // se llena la tabla con información
                clUsuario = new PdfPCell(new Phrase(usuarioParaExamen.usuarios.Email, _standardFont));
                clContraseña = new PdfPCell(new Phrase(usuarioParaExamen.usuarios.Password, _standardFont));

                // Añadimos las celdas a la tabla
                tblPrueba.AddCell(clUsuario);
                tblPrueba.AddCell(clContraseña);
                doc.Add(tblPrueba);




                doc.Close();
                writer.Close();
                //Contraseña al pdf
                using (var input = new FileStream(ruta, FileMode.Open, FileAccess.Read, FileShare.Read))
                using (var output = new FileStream(rutaConPass, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    var reader = new PdfReader(input);
                    PdfEncryptor.Encrypt(reader, output, true, "RRHH2020*", "RRHH2020*", PdfWriter.ALLOW_PRINTING);


                }
                doc.Close();
                writer.Close();
                File.Delete(ruta);
            }

            

           
        
        }


        public void AbrirPDF(string email)
        {
            Process.Start(HostingEnvironment.MapPath("~/PDF/" + email + ".pdf"));

        }

    }
}

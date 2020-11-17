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
using Data.Servicios;
using Data;
using System.Security;
using System.ComponentModel;

namespace Negocio.Servicios
{
 public   class CrearPDF :IRepository<PDF>
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
  

            

          


        public void AbrirPDF(string email)
        {
            //Process process = new Process();
            //process.StartInfo.UseShellExecute = true;
            //process.StartInfo.FileName = @"C:\PDF\" + email + ".pdf";
            //process.Start();



            System.Diagnostics.Process command = new System.Diagnostics.Process();
            command.StartInfo.FileName = @"C:\Program Files (x86)\Foxit Software\Foxit Reader\FoxitReader.exe";
            command.StartInfo.Arguments = @"C:\PDF\" + email + ".pdf";

            command.StartInfo.UserName = "webconfig";
            command.StartInfo.Domain = "";
            System.Security.SecureString pwd = new System.Security.SecureString();

            foreach (char c in "Vale5861!")
                pwd.AppendChar(c);

            command.StartInfo.Password = pwd;
            command.StartInfo.CreateNoWindow = false;
            command.StartInfo.Verb = "open";
            command.StartInfo.UseShellExecute = false;

            try
            {
                command.Start();
            }

            catch (Win32Exception e11)
            {
                
            }
            command.WaitForExit();
            command.Close();

        }

        public PDF Create(PDF entity)
        {
            string ruta = @"C:\PDF\" + entity.unUsuario.usuarios.Email + "2.pdf";
            string rutaConPass = @"C:\PDF\" + entity.unUsuario.usuarios.Email + ".pdf";
            PDF pDF = new PDF();
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
                                new FileStream(@"C:\PDF\" + entity.unUsuario.usuarios.Email + "2.pdf", FileMode.Create));


                // Se le coloca el título y el autor


                doc.AddCreator(entity.unUsuario.sede.empresa.empresa);

                // Abrimos el archivo
                doc.Open();
                // Se crea el tipo de Font que vamos utilizar

                iTextSharp.text.Font _standardFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);


                // Creamos la imagen y le ajustamos el tamaño
                if (entity.unUsuario.sede.empresa.empresa == "TB")
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
                clUsuario = new PdfPCell(new Phrase(entity.unUsuario.usuarios.Email, _standardFont));
                clContraseña = new PdfPCell(new Phrase(entity.unUsuario.usuarios.Password, _standardFont));

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


                //se guarda la ubicacion del archivo
                PDFDac pDFDac = new PDFDac();
          
                pDF.path = "~/PDF/" +  entity.unUsuario.usuarios.Email + ".pdf";
                pDF.unUsuario = entity.unUsuario;
                pDFDac.Create(pDF);
               
            }
            return pDF;
        }

        public List<PDF> Read()
        {
            PDFDac pDFDac = new PDFDac();
            List<PDF> result = new List<PDF>();
            result = pDFDac.Read();
            List<PDF> pDFs = new List<PDF>();
            foreach (PDF item in result)
            {
                PDF pDF = new PDF();
                UsuarioDac usuarioDac = new UsuarioDac();

                pDF.path = item.path;
                pDF.unUsuario.usuarios = usuarioDac.ReadBy(item.unUsuario.usuarios.Id);
                pDFs.Add(pDF);
            }
            return pDFs;
        }

        public PDF ReadBy(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(PDF entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            PDFDac pDFDac = new PDFDac();
            pDFDac.Delete(id);
        }
    }
}

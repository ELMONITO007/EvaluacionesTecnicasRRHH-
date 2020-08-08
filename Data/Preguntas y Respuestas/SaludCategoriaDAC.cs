using Microsoft.Practices.EnterpriseLibrary.Data;
using Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class SaludCategoriaDAC : DataAccessComponent, IRepository<SaludCategoria>
    {

        private SaludCategoria LoadNivel(IDataReader dr)
        {
            SaludCategoria saludCategoria = new SaludCategoria();
            saludCategoria.Pregunta.Id = GetDataValue<int>(dr, "ID_Pregunta");
            saludCategoria.Pregunta.LaPregunta = GetDataValue<string>(dr, "Pregunta");
            saludCategoria.Pregunta.categoria.LaCategoria = GetDataValue<string>(dr, "categoria");
            saludCategoria.Pregunta.tipoPregunta.Id = GetDataValue<int>(dr, "ID_TipoPregunta");
            saludCategoria.Pregunta.tipoPregunta.TipoDePregunta = GetDataValue<string>(dr, "TipoPregunta");
            saludCategoria.Pregunta.nivel.ElNivel= GetDataValue<string>(dr, "Nivel");
            return saludCategoria;
        }
        public SaludCategoria Create(SaludCategoria entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<SaludCategoria> Read()
        {
            throw new NotImplementedException();
        }

        public SaludCategoria ReadBy(int id)
        {

            const string SQL_STATEMENT = "select np.Nivel, p.ID_Pregunta,p.Pregunta,p.ID_TipoPregunta,TipoPregunta,categoria from pregunta as p inner join PreguntaCategoria as pc on p.ID_Pregunta=pc.ID_Pregunta inner join Categoria as c on c.ID_Categoria=pc.ID_Categoria inner join TipoPregunta as tp on tp.ID_TipoPregunta =p.ID_TipoPregunta inner join NivelPregunta as np on np.ID_Nivel=p.ID_Nivel where p.activo=1 and SubPregunta=0 and  pc.ID_Categoria=@Id";

            SaludCategoria result = new SaludCategoria();
            var db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);
            using (DbCommand cmd = db.GetSqlStringCommand(SQL_STATEMENT))
            {
                db.AddInParameter(cmd, "@Id", DbType.Int32, id);
                using (IDataReader dr = db.ExecuteReader(cmd))
                {
                    while (dr.Read())
                    {
                        SaludCategoria nivel = LoadNivel(dr);
                        result.ListaPreguntasTotal.Add(nivel);
                    }
                }
            }
            return result;
         
        }

        public void Update(SaludCategoria entity)
        {
            throw new NotImplementedException();
        }
    }
}

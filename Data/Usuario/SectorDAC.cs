using Entities;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class SectorDAC : DataAccessComponent, IRepository<Sector>
    {
        private Sector LoadSector(IDataReader dr)
        {
            Sector sector = new Sector();
            sector.Id = GetDataValue<int>(dr, "ID_sector");
            sector.sector = GetDataValue<string>(dr, "sector");

            return sector;
        }
        public Sector Create(Sector entity)
        {
            const string SQL_STATEMENT = "insert into Sector (Sector,Activo) values(@Sector,1) ";
            var db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);
            using (DbCommand cmd = db.GetSqlStringCommand(SQL_STATEMENT))
            {
                db.AddInParameter(cmd, "@Sector", DbType.String, entity.sector);

                entity.Id = Convert.ToInt32(db.ExecuteScalar(cmd));
            }


            return entity;
        }

        public void Delete(int id)
        {
            const string SQL_STATEMENT = "update Sector set Activo=0 where ID_Sector=@Id";
            var db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);
            using (DbCommand cmd = db.GetSqlStringCommand(SQL_STATEMENT))
            {
                db.AddInParameter(cmd, "@Id", DbType.Int32, id);
                db.ExecuteNonQuery(cmd);
            }
        }

        public List<Sector> Read()
        {
            const string SQL_STATEMENT = "select * from sector where activo=1";

            List<Sector> result = new List<Sector>();
            var db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);
            using (DbCommand cmd = db.GetSqlStringCommand(SQL_STATEMENT))
            {
                using (IDataReader dr = db.ExecuteReader(cmd))
                {
                    while (dr.Read())
                    {
                        Sector sector = LoadSector(dr);
                        result.Add(sector);
                    }
                }
            }
            return result;
        }

        public Sector ReadBy(int id)
        {
            const string SQL_STATEMENT = "select * from sector where activo=1 and ID_sector=@Id";
            Sector sector = null;

            var db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);
            using (DbCommand cmd = db.GetSqlStringCommand(SQL_STATEMENT))
            {
                db.AddInParameter(cmd, "@Id", DbType.Int32, id);
                using (IDataReader dr = db.ExecuteReader(cmd))
                {
                    if (dr.Read())
                    {
                        sector = LoadSector(dr);
                    }
                }
            }
            return sector;
        }
        public Sector ReadBy(string id)
        {
            const string SQL_STATEMENT = "select * from sector where activo=1 and sector=@Id";
            Sector sector = null;

            var db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);
            using (DbCommand cmd = db.GetSqlStringCommand(SQL_STATEMENT))
            {
                db.AddInParameter(cmd, "@Id", DbType.String, id);
                using (IDataReader dr = db.ExecuteReader(cmd))
                {
                    if (dr.Read())
                    {
                        sector = LoadSector(dr);
                    }
                }
            }
            return sector;
        }
        public void Update(Sector entity)
        {
            const string SQL_STATEMENT = "update Sector set Sector=@Sector where Id_Sector=@Id";

            var db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);
            using (DbCommand cmd = db.GetSqlStringCommand(SQL_STATEMENT))
            {
                db.AddInParameter(cmd, "@Id", DbType.Int32, entity.Id);
                db.AddInParameter(cmd, "@Sector", DbType.String, entity.sector);
                db.ExecuteNonQuery(cmd);

            }
        }

       
    }
}

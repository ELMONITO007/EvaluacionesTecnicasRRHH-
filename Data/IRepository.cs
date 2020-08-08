using Entities;
using System.Collections.Generic;

namespace Data
{
    public interface IRepository<TEntity> where TEntity : EntityBase
    {
        TEntity Create(TEntity entity);
        List<TEntity> Read();
        TEntity ReadBy(int id);
        void Update(TEntity entity);
        void Delete(int id);
    }
}

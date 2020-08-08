using Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Negocio
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

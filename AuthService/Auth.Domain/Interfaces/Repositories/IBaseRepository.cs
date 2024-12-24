using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Application.Interfaces.Repositories
{
    public interface IBaseRepository<TEntity>
    {   // прописываем CRUD операции
        IQueryable<TEntity> GetAll(); // select - извлеение записей из бд

        Task<TEntity> CreateAsync(TEntity entity); // insert

        TEntity Update(TEntity entity); // update 

        TEntity Remove(TEntity entity);  // delete

        Task<int> SaveChangeAsync (); 

    }
}

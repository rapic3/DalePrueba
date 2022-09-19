using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Dale.Repository.SQLServer
{

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IRepository<TEntity> where TEntity : class
    {

        #region Get Entity

        Task<IQueryable<TEntity>> GetAllAsync(bool withoutDefaultFilters = false,
            Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            int? take = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null);
        Task<TEntity> FirstOrDefaultAsync(
            Expression<Func<TEntity, bool>> predicate,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null);

        Task<TEntity> GetByIdAsync(Guid Id);

        Task<bool> ExistAsync(Guid id);
        #endregion

        #region Create Entity
        Task AddAsync(TEntity entity);
        Task<TEntity> AddAndReturnAsync(TEntity entity);

        Task<string> AddAndReturnIdAsync(TEntity entity);
        #endregion

        #region Update Entity
        Task<bool> UpdateListAsync(List<TEntity> entities);
        Task<bool> UpdateAsync(TEntity t);
        Task<TEntity> UpdateAndReturnAsync(TEntity entity);
        #endregion

        #region Delete
        Task<bool> DeleteAsync(TEntity entity);

        Task<bool> DeleteAsync(Guid id, bool isSoftDelete = true);
        #endregion

        Task<bool> SaveAllAsync();
        Task<IDbContextTransaction> BeginTransactionAsync();
        void CommitTransaction();
        void RollbackTransaction();
    }
}

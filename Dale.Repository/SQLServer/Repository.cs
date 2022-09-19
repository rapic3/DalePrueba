using Dale.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Storage;
using System.Linq.Expressions;

namespace Dale.Repository.SQLServer
{
    /// <summary>
    /// Clase para el manejo del repositorio
    /// </summary>
    /// <author>
    /// Robert Pineda
    /// </author>
    /// <remarks>
    /// 14/09/2022
    /// </remarks>
    public class Repository<T> : IRepository<T> where T : BaseModel
    {
        private readonly MSDbContext _context;

        public Repository(MSDbContext Context)
        {
            _context = Context;
        }

        /// <summary>
        /// Prepares the query to return data with diferentes options.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="predicate">The predicate.</param>
        /// <param name="include">The include.</param>
        /// <param name="orderBy">The order by.</param>
        /// <param name="take">The take.</param>
        /// <returns></returns>
        protected IQueryable<T> PrepareQuery(
            IQueryable<T> query,
            Expression<Func<T, bool>> predicate = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            int? take = null
        )
        {
            if (include != null)
            {
                query = include(query);
            }

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            if (take.HasValue)
            {
                query = query.Take(Convert.ToInt32(take));
            }

            return query;
        }

        #region Get
        public virtual async Task<IQueryable<T>> GetAllAsync(
            bool withoutDefaultFilters = true,
            Expression<Func<T, bool>> predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            int? take = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null
        )
        {
            try
            {
                IQueryable<T> query = _context.Set<T>().AsQueryable();
                if (withoutDefaultFilters)
                {
                    query = query.Where(x => x.IsActive);
                }
                query = PrepareQuery(query, predicate, include, orderBy, take);
                return await Task.Run(() => query);
            }
            catch (Exception ex)
            {
                throw new Exception("Ha ocurrido un error listando las entidades", ex);
            }

        }

        public virtual async Task<T> FirstOrDefaultAsync(
            Expression<Func<T, bool>> predicate,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null
        )
        {
            try
            {
                IQueryable<T> query = _context.Set<T>().AsQueryable();
                query = PrepareQuery(query, predicate, include, orderBy);
                return await query.FirstOrDefaultAsync();

            }
            catch (Exception ex)
            {
                throw new Exception("Ha ocurrido un error buscando la entidad", ex);
            }
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            var ret = await _context.Set<T>().FindAsync(id);    

            return ret;
        }

        public async Task<bool> ExistAsync(Guid id)
        {
            return await _context.Set<T>().AnyAsync(e => e.Id.Equals(id));
        }
        #endregion

        #region Create
        public virtual async Task AddAsync(T entity)
        {
            DateTimeOffset date = DateTimeOffset.UtcNow;
            try
            {
                entity.CreatedAt = date;
                entity.IsActive = true;

                await _context.AddAsync(entity);

                await SaveAllAsync();

            }
            catch (Exception ex)
            {
                throw new Exception("Ha ocurrido un error insertando la entidad " + entity.GetType().Name, ex);
            }
        }

        public async Task<T> AddAndReturnAsync(T entity)
        {
            DateTimeOffset date = DateTimeOffset.UtcNow;
            try
            {
                entity.CreatedAt = date;
                entity.IsActive = true;

                await _context.Set<T>().AddAsync(entity);
                await SaveAllAsync();

                return entity;

            }
            catch (Exception ex)
            {
                throw new Exception("Ha ocurrido un error insertando la entidad " + entity.GetType().Name, ex);
            }
        }

        public async Task<string> AddAndReturnIdAsync(T entity)
        {
            DateTimeOffset date = DateTimeOffset.UtcNow;
            try
            {
                entity.CreatedAt = date;
                entity.IsActive = true;

                await _context.Set<T>().AddAsync(entity);
                await SaveAllAsync();

                return entity.Id.ToString();

            }
            catch (Exception ex)
            {
                throw new Exception("Ha ocurrido un error insertando la entidad " + entity.GetType().Name, ex);
            }
        }
        #endregion

        #region Update

        public async Task<bool> UpdateListAsync(List<T> entities)
        {
            DateTimeOffset date = DateTimeOffset.UtcNow;
            try
            {
                foreach (var entity in entities)
                {
                    entity.UpdatedAt = date;
                    _context.Set<T>().Update(entity);
                }

                return await SaveAllAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Ha ocurrido un error actualizando las entidades ", ex);
            }
        }

        public async Task<bool> UpdateAsync(T entity)
        {
            DateTimeOffset date = DateTimeOffset.UtcNow;
            try
            {
                entity.UpdatedAt = date;
                _context.Set<T>().Update(entity);

                return await SaveAllAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Ha ocurrido un error actualizando la entidad " + entity.GetType().Name, ex);
            }
        }

        public async Task<T> UpdateAndReturnAsync(T entity)
        {
            DateTimeOffset date = DateTimeOffset.UtcNow;
            try
            {
                entity.UpdatedAt = date;
                _context.Set<T>().Update(entity);
                await SaveAllAsync();
                return entity;

            }
            catch (Exception ex)
            {
                throw new Exception("Ha ocurrido un error actualizando la entidad " + entity.GetType().Name, ex);
            }
        }
        #endregion

        #region Remove
        public virtual async Task<bool> DeleteAsync(T entity)
        {
            DateTimeOffset date = DateTimeOffset.UtcNow;
            try
            {
                entity.UpdatedAt = date;
                entity.IsActive = false;
                _context.Set<T>().Update(entity);

                return await SaveAllAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Ha ocurrido un error eliminado la entidad " + entity.GetType().Name, ex);
            }
        }

        public virtual async Task<bool> DeleteAsync(Guid id, bool isSoftDelete = true)
        {
            DateTimeOffset date = DateTimeOffset.UtcNow;
            try
            {
                T entity = await GetByIdAsync(id);

                if (entity != null)
                {
                    if (isSoftDelete)
                    {
                        entity.UpdatedAt = date;
                        entity.IsActive = false;
                        _context.Set<T>().Update(entity);
                    }
                    else
                    {
                        _context.Set<T>().Remove(entity);
                    }
                    return await SaveAllAsync();
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Ha ocurrido un error eliminando la entidad ", ex);
            }
        }
        #endregion 

        #region Transactions

        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            return await _context.Database.BeginTransactionAsync();
        }

        public void CommitTransaction()
        {
            _context.Database.CommitTransaction();
        }

        public void RollbackTransaction()
        {
            _context.Database.RollbackTransaction();
        }
        #endregion

        #region Save Changes
        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        Task<IDbContextTransaction> IRepository<T>.BeginTransactionAsync()
        {
            throw new NotImplementedException();
        }
        #endregion

    }
}

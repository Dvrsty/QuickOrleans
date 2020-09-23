using Quick.Common.Dtos;
using Quick.Common.Interface;
using Quick.Core;
using Quick.Interface;
using Quick.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Quick.Common.Extensions;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data.Common;

namespace Quick.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {

        private readonly QuickContext _context;
        private readonly DbSet<T> _dbSet;
        private readonly string _connStr;

        public Repository(IQuickContext context)
        {
            _context = context as QuickContext;
            _dbSet = _context.Set<T>();
            _connStr = _context.Database.GetDbConnection().ConnectionString;
        }

        public IQueryable<T> Entities
        {
            get { return _dbSet.AsNoTracking(); }
        }

        public IQueryable<T> TrackEntities
        {
            get { return _dbSet; }
        }

        public async Task<T> AddAsync(T entity, bool isSave = true)
        {
            _dbSet.Add(entity);
            if (isSave)
            {
                await SaveChangeAsync();
            }
            return entity;
        }

        public async Task AddRangeAsync(IEnumerable<T> entitys, bool isSave)
        {
            _dbSet.AddRange(entitys);
            if (isSave)
            {
                await SaveChangeAsync();
            }
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> where)
        {
            return await _dbSet.AsNoTracking().AnyAsync(@where);
        }

        public async Task<IDbContextTransaction> BeginTransactionAsync(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
        {
            //if (_context.Database.CurrentTransaction == null)
            //{
            //    return await _context.Database.BeginTransactionAsync(isolationLevel);
            //}
            return await _context.Database.BeginTransactionAsync(isolationLevel);
        }

        public async Task<IDbContextTransaction> UseTransactionAsync(IDbContextTransaction transaction)
        {
            return await _context.Database.UseTransactionAsync((DbTransaction)transaction);
        }

        public async Task CommitAsync()
        {
            var transaction = _context.Database.CurrentTransaction;
            if (transaction != null)
            {
                try
                {
                    await transaction.CommitAsync();
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            }
        }

        public async Task<int> CountAsync()
        {
            return await _dbSet.AsNoTracking().CountAsync();
        }

        public async Task<int> CountAsync(Expression<Func<T, bool>> where)
        {
            return await _dbSet.AsNoTracking().CountAsync(@where); ;
        }

        public async Task DeleteAsync(T entity, bool isSave = true)
        {
            _dbSet.Remove(entity);
            if (isSave)
            {
                await SaveChangeAsync();
            }
        }

        public async Task DeleteAsync(Expression<Func<T, bool>> where, bool isSave = true)
        {
            T[] entitys = _dbSet.Where<T>(@where).ToArray();
            if (entitys.Length > 0)
            {
                _dbSet.RemoveRange(entitys);
            }
            if (isSave)
            {
                await SaveChangeAsync();
            }
        }

        public async Task DeleteAsync(object id, bool isSave = true)
        {
            _dbSet.Remove(_dbSet.Find(id));
            if (isSave)
            {
                await SaveChangeAsync();
            }
        }

        public async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> where)
        {
            return await _dbSet.AsNoTracking().FirstOrDefaultAsync(@where);
        }

        public async Task<T> FirstOrDefaultAsync<TOrder>(Expression<Func<T, bool>> where, Expression<Func<T, TOrder>> order, bool isDesc = false)
        {
            if (isDesc)
            {
                return await _dbSet.AsNoTracking().OrderByDescending(order).FirstOrDefaultAsync(@where);
            }
            else
            {
                return await _dbSet.AsNoTracking().OrderBy(order).FirstOrDefaultAsync(@where);
            }
        }

        public async Task<T> GetById<Ttype>(Ttype id)
        {
            return await _dbSet.FindAsync (id);
        }


        public async Task RollbackAsync()
        {
            if (_context.Database.CurrentTransaction != null)
            {
                await _context.Database.CurrentTransaction.RollbackAsync();
            }
        }

        public async Task<int> SaveChangeAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public Task SortAsync<TOrder>(Func<T, TOrder> order, bool isDesc = true)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync(T entity, bool isSave = true)
        {
            var entry = _context.Entry(entity);
            if (entry.State == EntityState.Detached)
            {
                entry.State = EntityState.Modified;
            }
            if (isSave)
            {
                await SaveChangeAsync();
            }
        }
    }
}

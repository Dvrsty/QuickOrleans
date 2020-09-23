using Quick.Common.Interface;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Quick.IRepositories
{
    public interface IRepository
    {

    }

    public interface IRepository<T> : IRepository where T : class
    {
        /// <summary>
        /// 显式开启数据上下文事务
        /// </summary>
        /// <param name="isolationLevel">指定连接的事务锁定行为</param>
        Task<IDbContextTransaction> BeginTransactionAsync(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted);

        /// <summary>
        /// 加入事务
        /// </summary>
        /// <param name="transaction"></param>
        /// <returns></returns>
        Task<IDbContextTransaction> UseTransactionAsync(IDbContextTransaction transaction);

        /// <summary>
        /// 提交事务的更改
        /// </summary>
        Task CommitAsync();

        /// <summary>
        /// 显式回滚事务，仅在显式开启事务后有用
        /// </summary>
        Task RollbackAsync();

        /// <summary>
        ///  保存数据
        /// </summary>
        /// <returns></returns>
        Task<int> SaveChangeAsync();

        /// <summary>
        /// 获取 当前实体类型的查询数据集，数据将使用不跟踪变化的方式来查询，当数据用于展现时，推荐使用此数据集，如果用于新增，更新，删除时，请使用<see cref="TrackEntities"/>数据集
        /// </summary>
        IQueryable<T> Entities { get; }

        /// <summary>
        /// 获取 当前实体类型的查询数据集，当数据用于新增，更新，删除时，使用此数据集，如果数据用于展现，推荐使用<see cref="Entities"/>数据集
        /// </summary>
        IQueryable<T> TrackEntities { get; }

        /// <summary>
        ///  插入
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="isSave"></param>
        /// <returns></returns>
        Task<T> AddAsync(T entity, bool isSave = true);

        /// <summary>
        ///  批量插入
        /// </summary>
        /// <param name="entitys"></param>
        /// <param name="isSave"></param>
        /// <returns></returns>
        Task AddRangeAsync(IEnumerable<T> entitys, bool isSave);

        /// <summary>
        ///  主键ID删除
        /// </summary>
        /// <param name="id"></param>
        /// <param name="isSave"></param>
        /// <returns></returns>
        Task DeleteAsync(object id, bool isSave = true);

        /// <summary>
        /// 删除 - 通过实体对象删除
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="isSave">是否执行</param>
        Task DeleteAsync(T entity, bool isSave = true);

        /// <summary>
        /// 批量删除 - 通过条件删除
        /// </summary>
        /// <param name="where">过滤条件</param>
        /// <param name="isSave">是否执行</param>

        Task DeleteAsync(Expression<Func<T, bool>> @where, bool isSave = true);

        /// <summary>
        /// 修改 - 通过实体对象修改
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="isSave"></param>
        Task UpdateAsync(T entity, bool isSave = true);

        /// <summary>
        /// 是否满足条件
        /// </summary>
        /// <param name="where">过滤条件</param>
        /// <returns></returns>
        Task<bool> AnyAsync(Expression<Func<T, bool>> @where);

        /// <summary>
        /// 返回总条数
        /// </summary>
        /// <returns></returns>
        Task<int> CountAsync();

        /// <summary>
        /// 返回总条数 - 通过条件过滤
        /// </summary>
        /// <param name="where">过滤条件</param>
        /// <returns></returns>
        Task<int> CountAsync(Expression<Func<T, bool>> @where);

        /// <summary>
        /// 返回第一条记录
        /// </summary>
        /// <param name="where">过滤条件</param>
        /// <returns></returns>
        Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> @where);

        /// <summary>
        /// 返回第一条记录 - 通过条件过滤
        /// </summary>
        /// <typeparam name="TOrder">排序约束</typeparam>
        /// <param name="where">过滤条件</param>
        /// <param name="order">排序条件</param>
        /// <param name="isDesc">排序方式</param>
        /// <returns></returns>
        Task<T> FirstOrDefaultAsync<TOrder>(Expression<Func<T, bool>> @where, Expression<Func<T, TOrder>> order, bool isDesc = false);

        /// <summary>
        /// 根据ID查询
        /// </summary>
        /// <typeparam name="Ttype">字段类型</typeparam>
        /// <param name="id">主键ID</param>
        /// <returns></returns>
        Task<T> GetById<Ttype>(Ttype id);


        /// <summary>
        ///  排序
        /// </summary>
        /// <typeparam name="TOrder"></typeparam>
        /// <param name="order"></param>
        /// <param name="isDesc"></param>
        /// <returns></returns>
        Task SortAsync<TOrder>(Func<T, TOrder> order, bool isDesc = true);
    }
}

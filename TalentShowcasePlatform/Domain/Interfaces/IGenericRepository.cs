using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces;

public interface IGenericRepository<T> where T : class, IEntity
{
	IQueryable<T> Entities { get; }

	Task<T> GetByIdAsync(Guid id);
	Task<T> GetByIdAsync(Guid id, Func<IQueryable<T>, IQueryable<T>> include = null);
	Task<List<T>> GetAllAsync();
	Task<List<T>> GetAllAsync(Func<IQueryable<T>, IQueryable<T>> include = null);
	Task<T> AddAsync(T entity);
	Task<List<T>> AddRangeAsync(List<T> entities);
	Task UpdateAsync(T entity);
	Task DeleteAsync(T entity);
	Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> expression);
}

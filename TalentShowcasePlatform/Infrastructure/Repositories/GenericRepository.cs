using Domain.Common;
using Domain.Interfaces;
using Infrastructure.Persistences.BEContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
{
	private readonly DbContext _context;
	private readonly DbSet<T> _dbSet;

	public GenericRepository(DbContext context)
	{
		_context = context;
		_dbSet = _context.Set<T>();
	}

	public IQueryable<T> Entities => _dbSet;

	public async Task<T> GetByIdAsync(Guid id)
	{
		return await _dbSet.FindAsync(id);
	}

	public async Task<T> GetByIdAsync(Guid id, Func<IQueryable<T>, IQueryable<T>> include = null)
	{
		IQueryable<T> query = _dbSet;
		if (include != null)
		{
			query = include(query);
		}
		return await query.FirstOrDefaultAsync(e => e.Id == id);
	}

	public async Task<List<T>> GetAllAsync()
	{
		return await _dbSet.ToListAsync();
	}

	public async Task<List<T>> GetAllAsync(Func<IQueryable<T>, IQueryable<T>> include = null)
	{
		IQueryable<T> query = _dbSet;
		if (include != null)
		{
			query = include(query);
		}
		return await query.ToListAsync();
	}

	public async Task<T> AddAsync(T entity)
	{
		await _dbSet.AddAsync(entity);
		return entity;
	}

	public async Task<List<T>> AddRangeAsync(List<T> entities)
	{
		await _dbSet.AddRangeAsync(entities);
		return entities;
	}

	public async Task UpdateAsync(T entity)
	{
		_dbSet.Update(entity);
	}

	public async Task DeleteAsync(T entity)
	{
		_dbSet.Remove(entity);
	}

	public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> expression)
	{
		return await _dbSet.Where(expression).ToListAsync();
	}
}
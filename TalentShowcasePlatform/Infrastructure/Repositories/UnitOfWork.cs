﻿using Domain.Common;
using Domain.Interfaces;
using Infrastructure.Persistences.BEContext;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
	private readonly BEContext _context;
	private Hashtable _repositories;
	private bool disposed;

	public UnitOfWork(BEContext context)
	{
		_context = context ?? throw new ArgumentNullException(nameof(_context));
	}

	public void Dispose()
	{
		Dispose(true);
		GC.SuppressFinalize(this);
	}

	protected virtual void Dispose(bool disposing)
	{
		if (disposed)
		{
			if (disposing)
			{
				//dispose managed resources
				_context.Dispose();
			}
		}
		//dispose unmanaged resources
		disposed = true;
	}

	public IGenericRepository<T> Repository<T>() where T : BaseEntity
	{
		if (_repositories == null)
			_repositories = new Hashtable();

		var type = typeof(T).Name;

		if (!_repositories.ContainsKey(type))
		{
			var repositoryType = typeof(GenericRepository<>);

			var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(T)), _context);

			_repositories.Add(type, repositoryInstance);
		}

		return (IGenericRepository<T>)_repositories[type];
	}

	public Task RollBacl()
	{
		_context.ChangeTracker.Entries().ToList().ForEach(x => x.Reload());
		return Task.CompletedTask;
	}

	public async Task<int> Save(CancellationToken cancellationToken)
	{
		return await _context.SaveChangesAsync(cancellationToken);
	}

	public Task<int> SaveAndRemoveCache(CancellationToken cancellationToken, params string[] cacheKeys)
	{
		throw new NotImplementedException();
	}
}

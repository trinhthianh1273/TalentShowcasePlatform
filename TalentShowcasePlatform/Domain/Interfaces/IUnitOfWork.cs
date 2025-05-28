using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces;

public interface IUnitOfWork : IDisposable
{
	IGenericRepository<T> Repository<T>() where T : BaseEntity;
	Task<int> Save(CancellationToken cancellationToken);
	Task<int> Save();
	Task<int> SaveAndRemoveCache(CancellationToken cancellationToken, params string[] cacheKeys);
	Task RollBacl();
}

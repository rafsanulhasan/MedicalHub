using MedicalHub.App.Web.Data.Entities;

using Microsoft.EntityFrameworkCore.ChangeTracking;

using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace MedicalHub.App.Web.Data.Repositories
{
	public interface IRepository<TEntity, TKey>
		where TKey : IEquatable<TKey>
		where TEntity : class, IEntity<TKey>, new()
	{
		IQueryable<TEntity> GetAll(
			Expression<Func<TEntity, bool>>? filterPredicate = null,
			int? skip = null,
			int? take = null
		);

		Task<TEntity> GetSingleAsync(
			Expression<Func<TEntity, bool>> filterPredicate,
			CancellationToken cancellationToken = default
		);

		Task<TEntity> GetSingleOrDefaultAsync(
			Expression<Func<TEntity, bool>> filterPredicate,
			CancellationToken cancellationToken = default
		);

		Task<TEntity> GetSingleAsync(
			TKey key,
			CancellationToken cancellationToken = default
		);

		Task<TEntity> GetSingleOrDefaultAsync(
			TKey key,
			CancellationToken cancellationToken = default
		);

		EntityEntry<TEntity> Create(TEntity entity);
		EntityEntry<TEntity> Delete(TEntity entity);
		EntityEntry<TEntity> Update(TEntity entity);
		Task SaveChangesAsync(CancellationToken cancellationToken = default);
	}
}

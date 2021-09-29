using MedicalHub.App.Web.Data.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace MedicalHub.App.Web.Data.Repositories
{
	internal abstract class RepositoryBase<TEntity, TKey>
		: IRepository<TEntity, TKey>
		where TKey : IEquatable<TKey>
		where TEntity : class, IEntity<TKey>, new()
	{
		protected MedicalHubContext HubContext { get; }
		protected DbSet<TEntity> Set { get; }

		protected RepositoryBase(
			MedicalHubContext hubContext
		)
		{
			HubContext = hubContext;
			Set = HubContext.Set<TEntity>();
		}		

		public IQueryable<TEntity> GetAll(
			Expression<Func<TEntity, bool>>? filterPredicate = null, 
			int? skip = null, 
			int? take = null
		)
		{
			var query = Set.AsQueryable();
			if (filterPredicate is not null)
				query = query.Where(filterPredicate);
			if (skip.HasValue)
				query = query.Skip(skip.Value);
			if (take.HasValue)
				query = query.Take(take.Value);
			return query;
		}

		public Task<TEntity> GetSingleAsync(
			Expression<Func<TEntity, bool>> filterPredicate,
			CancellationToken cancellationToken = default
		) 
			=> Set.Where(filterPredicate).SingleAsync(cancellationToken);

		public Task<TEntity> GetSingleAsync(
			TKey key,
			CancellationToken cancellationToken = default
		) 
			=> Set.Where(s => s.Id.Equals(key)).SingleAsync(cancellationToken);

		public Task<TEntity> GetSingleOrDefaultAsync(
			Expression<Func<TEntity, bool>> filterPredicate,
			CancellationToken cancellationToken = default
		)
			=> Set.Where(filterPredicate).SingleOrDefaultAsync(cancellationToken);

		public Task<TEntity> GetSingleOrDefaultAsync(
			TKey key,
			CancellationToken cancellationToken = default
		) 
			=> Set.Where(s => s.Id.Equals(key)).SingleOrDefaultAsync(cancellationToken);

		public EntityEntry<TEntity> Create(TEntity entity) 
			=> Set.Add(entity);

		public EntityEntry<TEntity> Delete(TEntity entity) 
			=> Set.Remove(entity);

		public EntityEntry<TEntity> Update(TEntity entity) 
			=> Set.Update(entity);

		public async Task SaveChangesAsync(CancellationToken cancellationToken = default) 
			=> await HubContext.SaveChangesAsync(cancellationToken);
	}
}

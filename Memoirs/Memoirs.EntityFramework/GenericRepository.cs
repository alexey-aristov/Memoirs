using System;
using System.Data.Entity;
using System.Linq;
using Memoirs.Common;
using Memoirs.Common.Entities.Abstract;

namespace Memoirs.EntityFramework {
	public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class,IEntity {
		protected readonly DataContext _dataContext;
		protected readonly DbSet<TEntity> _dbSet;
		public GenericRepository ( DataContext dataContext ) {
			_dataContext = dataContext;
			_dbSet = _dataContext.Set<TEntity> ();
		}

		public virtual IQueryable<TEntity> Get ( string includeProperties = "" ) {
			return GetWithDeleted ( includeProperties ).Where ( a => !a.IsDeleted );
		}

		public IQueryable<TEntity> GetWithDeleted ( string includeProperties = "" ) {
			IQueryable<TEntity> query = _dbSet;
			foreach ( var includeProperty in includeProperties.Split
			( new char[] { ',' } , StringSplitOptions.RemoveEmptyEntries ) ) {
				query = query.Include ( includeProperty );
			}
			return query;
		}

		public virtual TEntity GetById ( int id ) {
			return _dbSet.Find ( id );
		}

		public virtual void Insert ( TEntity entity ) {
			_dbSet.Add ( entity );
		}

		public virtual void Delete ( int id ) {
			TEntity entityToDelete = _dbSet.Find ( id );
			Delete ( entityToDelete );
		}

		public virtual void Delete ( TEntity entity ) {
			if ( _dataContext.Entry ( entity ).State == EntityState.Detached ) {
				_dbSet.Attach ( entity );
			}
			_dbSet.Remove ( entity );
		}

		public virtual void Update ( TEntity entity ) {
			_dbSet.Attach ( entity );
			_dataContext.Entry ( entity ).State = EntityState.Modified;
		}

		public virtual void Save () {
			_dataContext.SaveChanges ();
		}
	}
}

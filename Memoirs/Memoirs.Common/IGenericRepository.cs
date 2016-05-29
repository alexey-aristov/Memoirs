using System.Linq;
using Memoirs.Common.EntityFramework.Entities.Abstract;

namespace Memoirs.Common {
	public interface IGenericRepository<TEntity> where TEntity:IEntity
	{
		IQueryable<TEntity> Get ( string includeProperties = "" );
		IQueryable<TEntity> GetWithDeleted ( string includeProperties = "" );
		TEntity GetById ( int id );
		void Add ( TEntity entity );
		void Delete ( int id );
		void Delete ( TEntity entity );
		void Update ( TEntity entity );
		void Save ();
	}
}

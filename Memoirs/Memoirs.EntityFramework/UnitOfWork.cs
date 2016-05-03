using System;
using Memoirs.Common;
using Memoirs.Common.Entities.Abstract;

namespace Memoirs.EntityFramework {
	public class UnitOfWork : IDisposable , IUnitOfWork {
		private readonly DataContext _dataContext = new DataContext ();
		private IGenericRepository<RecordBase> _postsRepository;

        private bool _disposed = false;

        public IGenericRepository<RecordBase> RecordsRepository {
			get { return _postsRepository ?? (_postsRepository = new GenericRepository<RecordBase>(_dataContext)); }
		}


		public void Save () {
			_dataContext.SaveChanges ();
		}
        
		protected virtual void Dispose ( bool disposing ) {
			if ( !this._disposed ) {
				if ( disposing ) {
					_dataContext.Dispose ();
				}
			}
			this._disposed = true;
		}

		public void Dispose () {
			Dispose ( true );
			GC.SuppressFinalize ( this );
		}
	}
}

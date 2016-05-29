using System;
using System.Data.Entity;
using Memoirs.Common.EntityFramework.Entities.Abstract;

namespace Memoirs.Common.EntityFramework
{
    public class UnitOfWorkEf : IDisposable, IUnitOfWork
    {
        private readonly DbContext _dataContext;// = new DataContext ();
        private IGenericRepository<RecordBase> _postsRepository;

        private bool _disposed = false;

        public UnitOfWorkEf(DbContext dbContext)
        {
            _dataContext = dbContext;

        }

        public IGenericRepository<RecordBase> RecordsRepository
        {
            get { return _postsRepository ?? (_postsRepository = new GenericRepository<RecordBase>(_dataContext)); }
        }


        public void Save()
        {
            _dataContext.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    _dataContext.Dispose();
                }
            }
            this._disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}

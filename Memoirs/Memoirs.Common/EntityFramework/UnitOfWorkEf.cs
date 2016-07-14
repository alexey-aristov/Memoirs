using System;
using System.Data.Entity;
using Memoirs.Common.EntityFramework.Entities.Abstract;

namespace Memoirs.Common.EntityFramework
{
    public class UnitOfWorkEf : IDisposable, IUnitOfWork
    {
        private readonly DbContext _dataContext;// = new DataContext ();
        private IGenericRepository<RecordBase> _postsRepository;
        private IGenericRepository<AppSetting> _appSettingsRepository;
        private IGenericRepository<Entities.EndOfPeriod> _endOfPeriodRepository;

        private bool _disposed = false;

        public UnitOfWorkEf(DbContext dbContext)
        {
            _dataContext = dbContext;

        }

        public IGenericRepository<RecordBase> RecordsRepository => _postsRepository ?? (_postsRepository = new GenericRepository<RecordBase>(_dataContext));

        public IGenericRepository<AppSetting> AppSettingsRepository => _appSettingsRepository ?? (_appSettingsRepository = new GenericRepository<AppSetting>(_dataContext));
        public IGenericRepository<Entities.EndOfPeriod> EndOfPeriodRepository => _endOfPeriodRepository ?? (_endOfPeriodRepository = new GenericRepository<Entities.EndOfPeriod>(_dataContext));


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

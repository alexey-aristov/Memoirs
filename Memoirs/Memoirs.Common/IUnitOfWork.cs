using Memoirs.Common.EntityFramework.Entities.Abstract;

namespace Memoirs.Common {
	public interface IUnitOfWork {
		IGenericRepository<RecordBase> RecordsRepository { get; }
		IGenericRepository<AppSetting> AppSettingsRepository { get; }
		IGenericRepository<EntityFramework.Entities.EndOfPeriod> EndOfPeriodRepository { get; }
		void Save();
	}
}

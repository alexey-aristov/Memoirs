using Memoirs.Common.EntityFramework.Entities.Abstract;

namespace Memoirs.Common {
	public interface IUnitOfWork {
		IGenericRepository<RecordBase> RecordsRepository { get; }
		IGenericRepository<AppSetting> AppSettingsRepository { get; }
		void Save();
	}
}

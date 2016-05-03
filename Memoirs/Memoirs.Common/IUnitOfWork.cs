using Memoirs.Common.Entities.Abstract;

namespace Memoirs.Common {
	public interface IUnitOfWork {
		IGenericRepository<RecordBase> RecordsRepository { get; }
		void Save();
	}
}

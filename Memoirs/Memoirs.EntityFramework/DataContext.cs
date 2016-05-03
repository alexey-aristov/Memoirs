using System.Data.Entity;
using Memoirs.Common.Entities.Abstract;

namespace Memoirs.EntityFramework {
	public class DataContext:DbContext {
		public DataContext()
			: base("DataConnection") // connectionstring name define in web.config
		{
			this.Configuration.ProxyCreationEnabled = false;
		}
		public DbSet<RecordBase> Records { get; set; }
	}
}

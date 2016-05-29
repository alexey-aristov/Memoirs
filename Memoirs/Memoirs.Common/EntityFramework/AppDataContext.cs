using System.Data.Entity;
using System.Linq;
using Memoirs.Common.EntityFramework.Entities.Abstract;
using Memoirs.Common.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Memoirs.Common.EntityFramework
{
    public class AppDataContext : IdentityDbContext<ApplicationUser>, IDataContext
    {
        public AppDataContext()
            : base("DataConnection")
        {
        }

        public new static AppDataContext Create()
        {
            return new AppDataContext();
        }
        public DbSet<RecordBase> Records { get; set; }
        public IQueryable<RecordBase> RecordsQuery
        {
            get { return Records; }
            set { Records = (DbSet<RecordBase>)value; } //not sure
        }
    }
}

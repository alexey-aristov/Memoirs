using System.Data.Entity;
using System.Linq;
using Memoirs.Common;
using Memoirs.Common.Entities.Abstract;
using Memoirs.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Memoirs.EntityFramework
{
    public class AppDataContext : IdentityContext
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

using System.ComponentModel.DataAnnotations.Schema;
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
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Entities.EndOfPeriod>().HasRequired(a => a.Record).WithOptional(a => a.EndOfPeriod).WillCascadeOnDelete(false);
        }

        public DbSet<RecordBase> Records { get; set; }
        public DbSet<AppSetting> AppSettings { get; set; }
        public DbSet<Entities.EndOfPeriod> EndOfPeriods { get; set; }
        public IQueryable<RecordBase> RecordsQuery
        {
            get { return Records; }
            set { Records = (DbSet<RecordBase>)value; } //not sure
        }

        public IQueryable<AppSetting> AppSettingsQuery
        {
            get { return AppSettings; }
            set { AppSettings = (DbSet<AppSetting>)value; } //not sure too
        }

        public IQueryable<Entities.EndOfPeriod> EndOfPeriodQuery
        {
            get { return EndOfPeriods; }
            set { EndOfPeriods = (DbSet<Entities.EndOfPeriod>)value; }//not sure too
        }
    }
}

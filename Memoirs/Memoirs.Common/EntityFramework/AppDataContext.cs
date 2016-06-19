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
        
        public DbSet<RecordBase> Records { get; set; }
        public DbSet<AppSetting> AppSettings { get; set; }
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
    }
}

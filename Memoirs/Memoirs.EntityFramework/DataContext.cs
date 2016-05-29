﻿using System.Data.Entity;
using System.Linq;
using Memoirs.Common;
using Memoirs.Common.Entities.Abstract;

namespace Memoirs.EntityFramework
{
    public class DataContext : DbContext, IDataContext
    {
        public DataContext()
            : base("DataConnection") // connectionstring name define in web.config
        {
            this.Configuration.ProxyCreationEnabled = false;
        }
        public DbSet<RecordBase> Records { get; set; }
        public IQueryable<RecordBase> RecordsQuery
        {
            get { return Records; }
            set { Records = (DbSet<RecordBase>)value; } //not sure
        }
    }
}

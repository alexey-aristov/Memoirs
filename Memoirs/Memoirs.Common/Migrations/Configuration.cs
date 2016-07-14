namespace Memoirs.Common.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Memoirs.Common.EntityFramework.AppDataContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Memoirs.Common.EntityFramework.AppDataContext context)
        {
            //todo : refactor
            //set login language to British (for user first day of week == monday)
            context.Database.ExecuteSqlCommand("ALTER LOGIN sa WITH DEFAULT_LANGUAGE = British");
            
        }
    }
}

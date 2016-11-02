using System.Collections.Generic;
using DataAccess.Entities;

namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<DataAccess.Context>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(DataAccess.Context context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            // Populate Offices
            context.Offices.AddOrUpdate(x => x.Id, new Office() { Id = 1, Location = "Vancouver" });
            context.Offices.AddOrUpdate(x => x.Id, new Office() { Id = 2, Location = "Regina" });
            context.Offices.AddOrUpdate(x => x.Id, new Office() { Id = 3, Location = "Winnipeg" });
            context.Offices.AddOrUpdate(x => x.Id, new Office() { Id = 4, Location = "Charlotte" });
            context.Offices.AddOrUpdate(x => x.Id, new Office() { Id = 5, Location = "Manila" });
            context.Offices.AddOrUpdate(x => x.Id, new Office() { Id = 6, Location = "Sydney" });

            // Populate Taps
            context.Taps.AddOrUpdate(x => x.Id, new Tap() { Id = 1, Name = "Draft", Content = 10000, MaxContent = 10000, OfficeId = 1, TapState = "New" });
            context.Taps.AddOrUpdate(x => x.Id, new Tap() { Id = 2, Name = "Beer", Content = 10000, MaxContent = 10000, OfficeId = 2, TapState = "New" });
            context.Taps.AddOrUpdate(x => x.Id, new Tap() { Id = 3, Name = "Beer2", Content = 10000, MaxContent = 10000, OfficeId = 3, TapState = "New" });
            context.Taps.AddOrUpdate(x => x.Id, new Tap() { Id = 4, Name = "Budweiser", Content = 10000, MaxContent = 10000, OfficeId = 4, TapState = "New" });
            context.Taps.AddOrUpdate(x => x.Id, new Tap() { Id = 5, Name = "Pale Pilsen", Content = 10000, MaxContent = 10000, OfficeId = 5, TapState = "New" });
            context.Taps.AddOrUpdate(x => x.Id, new Tap() { Id = 6, Name = "Red Horse", Content = 10000, MaxContent = 10000, OfficeId = 5, TapState = "New" });
            context.Taps.AddOrUpdate(x => x.Id, new Tap() { Id = 7, Name = "San Mig Light", Content = 10000, MaxContent = 10000, OfficeId = 5, TapState = "New" });
            context.Taps.AddOrUpdate(x => x.Id, new Tap() { Id = 7, Name = "Beer3", Content = 10000, MaxContent = 10000, OfficeId = 6, TapState = "New" });

            context.SaveChanges();
        }
    }
}

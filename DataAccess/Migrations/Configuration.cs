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

            // Populate Kegs
            context.Kegs.AddOrUpdate(x => x.Id, new Keg() { Id = 1, Name = "Draft", Content = 10000, MaxContent = 10000, TapId = 1, OfficeId = 1, UnitOfMeasurement = "ml" });
            context.Kegs.AddOrUpdate(x => x.Id, new Keg() { Id = 2, Name = "Beer", Content = 10000, MaxContent = 10000, TapId = 2, OfficeId = 2, UnitOfMeasurement = "ml" });
            context.Kegs.AddOrUpdate(x => x.Id, new Keg() { Id = 3, Name = "Beer2", Content = 10000, MaxContent = 10000, TapId = 3, OfficeId = 3, UnitOfMeasurement = "ml" });
            context.Kegs.AddOrUpdate(x => x.Id, new Keg() { Id = 4, Name = "Budweiser", Content = 10000, MaxContent = 10000, TapId = 4, OfficeId = 4, UnitOfMeasurement = "ml" });
            context.Kegs.AddOrUpdate(x => x.Id, new Keg() { Id = 5, Name = "Pale Pilsen", Content = 10000, MaxContent = 10000, TapId = 5, OfficeId = 5, UnitOfMeasurement = "ml" });
            context.Kegs.AddOrUpdate(x => x.Id, new Keg() { Id = 6, Name = "Red Horse", Content = 10000, MaxContent = 10000, TapId = 6, OfficeId = 6, UnitOfMeasurement = "ml" });
            context.Kegs.AddOrUpdate(x => x.Id, new Keg() { Id = 7, Name = "San Mig Light", Content = 10000, MaxContent = 10000, TapId = 7, OfficeId = 7, UnitOfMeasurement = "ml" });

            // Populate Offices
            context.Offices.AddOrUpdate(x => x.Id, new Office() { Id = 1, Location = "Vancouver" });
            context.Offices.AddOrUpdate(x => x.Id, new Office() { Id = 2, Location = "Regina" });
            context.Offices.AddOrUpdate(x => x.Id, new Office() { Id = 3, Location = "Winnipeg" });
            context.Offices.AddOrUpdate(x => x.Id, new Office() { Id = 4, Location = "Charlotte" });
            context.Offices.AddOrUpdate(x => x.Id, new Office() { Id = 5, Location = "Manila" });
            context.Offices.AddOrUpdate(x => x.Id, new Office() { Id = 6, Location = "Sydney" });

            // Populate Taps
            context.Taps.AddOrUpdate(x => x.Id, new Tap() { Id = 1, OfficeId = 1, Name = "Tap 1", KegId = 1, KegState = "New" });
            context.Taps.AddOrUpdate(x => x.Id, new Tap() { Id = 2, OfficeId = 2, Name = "Tap 2", KegId = 2, KegState = "New" });
            context.Taps.AddOrUpdate(x => x.Id, new Tap() { Id = 3, OfficeId = 3, Name = "Tap 3", KegId = 3, KegState = "New" });
            context.Taps.AddOrUpdate(x => x.Id, new Tap() { Id = 4, OfficeId = 4, Name = "Tap 4", KegId = 4, KegState = "New" });
            context.Taps.AddOrUpdate(x => x.Id, new Tap() { Id = 5, OfficeId = 5, Name = "Tap 5", KegId = 5, KegState = "New" });
            context.Taps.AddOrUpdate(x => x.Id, new Tap() { Id = 6, OfficeId = 5, Name = "Tap 6", KegId = 6, KegState = "New" });
            context.Taps.AddOrUpdate(x => x.Id, new Tap() { Id = 7, OfficeId = 6, Name = "Tap 7", KegId = 7, KegState = "New" });

            context.SaveChanges();
        }
    }
}

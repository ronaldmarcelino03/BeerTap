using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Entities;

namespace DataAccess
{
    public class Context : DbContext
    {
        public Context() : base("name=DataContext")
        {
        }

        public DbSet<Tap> Taps { get; set; }
        public DbSet<Office> Offices { get; set; }
        public DbSet<Keg> Kegs { get; set; }
    }
}

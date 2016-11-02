using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Entities;

namespace DataAccess.Repostitories
{
    public class KegRepository : IKegRepository
    {
        private Context _context;
        private bool _disposed;

        public KegRepository()
        {
            _context = new Context();
        }

        public IEnumerable<Keg> GetKegs()
        {
            return _context.Kegs.AsEnumerable();
        }

        public Keg GetKegById(int id)
        {
            return _context.Kegs.FirstOrDefault(c => c.Id == id);
        }
        
        public void Update(Keg keg)
        {
            _context.Kegs.AddOrUpdate(new Keg()
            {
                Id = keg.Id,
                Name = keg.Name,
                Content = keg.Content,
                MaxContent = keg.MaxContent,
                UnitOfMeasurement = keg.UnitOfMeasurement,
                KegState = keg.KegState
            });
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }

            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Entities;

namespace DataAccess.Repostitories
{
    public class OfficeRepository : IOfficeRepository, IDisposable
    {
        private Context _context;
        private bool _disposed;

        public OfficeRepository()
        {
            _context = new Context();
        }

        public IEnumerable<Office> GetOffices()
        {
            return _context.Offices.AsEnumerable();
        }

        public Office GetOfficeModelById(int id)
        {
            return _context.Offices.FirstOrDefault(_ => _.Id == id);
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

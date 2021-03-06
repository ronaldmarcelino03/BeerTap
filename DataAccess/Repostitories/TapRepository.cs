﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repostitories
{
    public class TapRepository : ITapRepository, IDisposable
    {
        private Context _context;
        private bool _disposed;

        public TapRepository()
        {
            _context = new Context();
        }

        public IEnumerable<Tap> GetTaps()
        {
            return _context.Taps.AsEnumerable();
		}

		public IEnumerable<Tap> GetTapsByOfficeId(int officeId)
		{
			return _context.Taps.Where(c => c.OfficeId == officeId).Select(c => c);
		}

		public Tap GetTapById(int id)
        {
            return _context.Taps.FirstOrDefault(c => c.Id == id);
		}

		public Tap GetTapByOfficeAndTapIds(int officeId, int tapId)
		{
			return _context.Taps.FirstOrDefault(c => c.OfficeId == officeId && c.Id == tapId);
		}

		public void Update(Tap tap)
        {
            _context.Taps.AddOrUpdate(tap);
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

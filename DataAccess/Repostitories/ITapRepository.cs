using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Entities;

namespace DataAccess.Repostitories
{
    public interface ITapRepository
    {
        IEnumerable<Tap> GetTaps();
        Tap GetTapById(int Id);
        void InsertTap(Tap keg);
        void DeleteTap(Tap keg);
        void UpdateTap(Tap keg);
        void Save();
    }
}

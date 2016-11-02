using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Entities;

namespace DataAccess.Repostitories
{
    public interface IKegRepository : IDisposable
    {
        IEnumerable<Keg> GetKegs();
        Keg GetKegById(int id);
        void Update(Keg keg);
        void Save();
    }
}

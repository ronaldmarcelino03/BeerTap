﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Entities;

namespace DataAccess.Repostitories
{
    public interface IOfficeRepository : IDisposable
    {
        Office GetOfficeModelById(int id);
        IEnumerable<Office> GetOffices();
    }
}

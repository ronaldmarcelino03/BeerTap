﻿using System;
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
        void Update(Tap tap);
        void Save();
    }
}

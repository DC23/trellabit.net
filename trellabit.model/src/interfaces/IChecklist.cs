﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace trellabit.model.interfaces
{
    interface IChecklist
    {
        string Name { get; set; }
        IList<IChecklistItem> Items { get; }
    }
}
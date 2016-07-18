using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace trellabit.model.interfaces
{
    interface IChecklistItem
    {
        string Name { get; set; }

        bool IsChecked { get; set; }
    }
}

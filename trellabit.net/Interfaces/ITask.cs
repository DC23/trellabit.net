using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace trellabit.interfaces
{
    // TODO: install ghostdoc and comment
    interface ITask
    {
        string Name { get; set; }

        string Description { get; set; }

        DateTime DueDate { get; set; }

        bool IsComplete { get; set; }
    }
}

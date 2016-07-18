using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using trellabit.model;

namespace trellabit.model.interfaces
{
    // TODO: install ghostdoc and comment
    interface ITask
    {
        string Name { get; set; }

        string Description { get; set; }

        DateTime DueDate { get; set; }

        bool IsComplete { get; set; }

        // Implement in Trello with hidden labels. Habitica has a direct implementation
        Difficulty Difficulty { get; set; }
    }
}

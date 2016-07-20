using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using trellabit.data;

namespace trellabit.data.interfaces
{
    /// <summary>
    /// A task that has a checklist.
    /// </summary>
    /// <seealso cref="trellabit.data.interfaces.ITask" />
    interface IChecklistTask : ITask
    {
        IChecklist Checklist { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trellabit.Services;

namespace Trellabit.Services.Interfaces
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using trellabit.interfaces;

namespace trellabit.Interfaces
{
    interface IChecklistTask : ITask
    {
        IChecklist Checklist { get; set; }
    }
}

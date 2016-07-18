using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using trellabit.model;

namespace trellabit.model.interfaces
{
    interface IChecklistTask : ITask
    {
        IChecklist Checklist { get; set; }
    }
}

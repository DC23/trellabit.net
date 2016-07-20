using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using trellabit.data;

namespace trellabit.data.interfaces
{
    interface IChecklistTask : ITask
    {
        IChecklist Checklist { get; set; }
    }
}

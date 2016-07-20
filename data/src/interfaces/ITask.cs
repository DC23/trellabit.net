using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using trellabit.data;

namespace trellabit.data.interfaces
{
    /// <summary>
    /// A Trellabit task.
    /// </summary>
    interface ITask
    {
        /// <summary>
        /// Gets or sets the task name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        string Name { get; set; }

        /// <summary>
        /// Gets or sets the task description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        string Description { get; set; }

        /// <summary>
        /// Gets or sets the task due date.
        /// Set to null to indicate no due date.
        /// </summary>
        /// <value>
        /// The due date, or null to clear the due date.
        /// </value>
        DateTime DueDate { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this task has been completed.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is completed; otherwise, <c>false</c>.
        /// </value>
        bool IsCompleted { get; set; }

        /// <summary>
        /// Gets or sets the task difficulty.
        /// </summary>
        /// <value>
        /// The difficulty.
        /// </value>
        Difficulty Difficulty { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace trellabit.services.interfaces
{
    /// <summary>
    /// A single checklist item.
    /// </summary>
    interface IChecklistItem
    {
        /// <summary>
        /// Gets or sets the checklist item name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        string Name { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is checked.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is checked; otherwise, <c>false</c>.
        /// </value>
        bool IsChecked { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trellabit.Services.Interfaces
{
    /// <summary>
    /// A named checklist.
    /// </summary>
    interface IChecklist
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        string Name { get; set; }

        /// <summary>
        /// The checklist items.
        /// </summary>
        /// <value>
        /// The items.
        /// </value>
        IList<IChecklistItem> Items { get; }
    }
}

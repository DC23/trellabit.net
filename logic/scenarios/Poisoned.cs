using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using trellabit.core;

namespace trellabit.logic.scenarios
{
    /// <summary>
    /// The Poisoning Scenario runner.
    /// </summary>
    public class Poisoned
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Poisoned" /> class.
        /// </summary>
        /// <param name="userOptions">The user options.</param>
        public Poisoned(UserOptions userOptions)
        {
            UserOptions = userOptions;
        }

        private UserOptions UserOptions { get; set; }
    }
}

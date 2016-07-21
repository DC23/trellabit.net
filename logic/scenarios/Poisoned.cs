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
        /// <param name="commandLineArgs">The command line arguments.</param>
        public Poisoned(UserOptions userOptions, CommandLineArgs commandLineArgs)
        {
            UserOptions = userOptions;
            ClArgs = commandLineArgs;
        }

        private UserOptions UserOptions { get; set; }
        private CommandLineArgs ClArgs { get; set; }
    }
}

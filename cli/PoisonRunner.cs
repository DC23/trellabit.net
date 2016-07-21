using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace trellabit.cli
{
    /// <summary>
    /// The Poisoning Scenario runner.
    /// TODO: this is already looking like it belongs in the logic assembly, and that I should have a common base class for "scenario runners"
    /// </summary>
    internal class PoisonRunner
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PoisonRunner" /> class.
        /// </summary>
        /// <param name="userOptions">The user options.</param>
        /// <param name="commandLineArgs">The command line arguments.</param>
        public PoisonRunner(UserOptions userOptions, CommandLineArgs commandLineArgs)
        {
            UserOptions = userOptions;
            ClArgs = commandLineArgs;
        }

        private UserOptions UserOptions { get; set; }
        private CommandLineArgs ClArgs { get; set; }
    }
}

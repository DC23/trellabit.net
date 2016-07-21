using CommandLine;
using CommandLine.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace trellabit.cli
{
    internal sealed class CommandLineArgs
    {
        /// <summary>
        /// Gets or sets the poison damage per day.
        /// </summary>
        /// <value>
        /// The poison damage per day.
        /// </value>
        [Option('p', "poison_per_day", Required = false,
            HelpText = "The amount of HP damage to be applied by poisoning in a 24 hour period")]
        public float PoisonDamagePerDay { get; set; }

        /// <summary>
        /// Gets or sets the last state of the parser.
        /// </summary>
        /// <value>
        /// The last state of the parser.
        /// </value>
        [ParserState]
        public IParserState LastParserState { get; set; }

        /// <summary>
        /// Gets the usage string.
        /// </summary>
        /// <returns>The usage (help) string.</returns>
        [HelpOption]
        public string GetUsage()
        {
            return HelpText.AutoBuild(this,
              (HelpText current) => HelpText.DefaultParsingErrorsHandler(this, current));
        }
    }
}

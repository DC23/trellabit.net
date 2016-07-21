using CommandLine;
using CommandLine.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace trellabit.core
{
    /// <summary>
    /// TODO: Can I merge the CommandLineArgs and UserOptions classes to create a single class that 
    /// represents all user options, regardless of how those options are set?
    /// </summary>
    public sealed class CommandLineArgs
    {
        /// <summary>
        /// Commandline argument factory.
        /// </summary>
        /// <param name="args">The arguments array from the CLI entry function.</param>
        /// <returns>
        /// The parsed command line arguments.
        /// </returns>
        public static CommandLineArgs Create(string[] args)
        {
            var parsedArgs = new CommandLineArgs();
            CommandLine.Parser.Default.ParseArguments(args, parsedArgs);
            return parsedArgs;
        }

        /// <summary>
        /// Prevents a default instance of the <see cref="CommandLineArgs" /> class from being created.
        /// </summary>
        private CommandLineArgs() { }

        /// <summary>
        /// Gets or sets the poison damage per day.
        /// </summary>
        /// <value>
        /// The poison damage per day.
        /// </value>
        [Option('p', "poison-per-day", Required = false, DefaultValue = 10,
            HelpText = "The amount of HP damage to be applied by poisoning in a 24 hour period")]
        public float PoisonDamagePerDay { get; set; }

        /// <summary>
        /// Gets or sets the update interval in minutes.
        /// This is the frequency at which Habitica will be polled for changes and poisoning rules applied.
        /// </summary>
        /// <value>
        /// The update interval in minutes.
        /// </value>
        [Option('i', "update-interval", Required = false, DefaultValue = 15, 
            HelpText = "The frequency at which Habitica will be polled for changes and poisoning rules applied")]
        public int UpdateIntervalMinutes { get; set; }

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

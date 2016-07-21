using NLog;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using trellabit.core;

namespace trellabit.cli
{
    /// <summary>
    /// The entry point to trellabit.cli
    /// </summary>
    class Program
    {
        static Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// trellabit command-line interface entry point
        /// </summary>
        /// <param name="args">The command-line arguments.</param>
        static void Main(string[] args)
        {
            logger.Info("Version {0} online",
                Assembly.GetExecutingAssembly().GetName().Version);

            UserOptions userOptions = new UserOptions(
                new FileInfo(Path.Combine(
                    Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                    Settings.Default.IniFileName)));

            if (!userOptions.ContainsValidTrelloApiKey)
            {
                logger.Warn("You must paste your Trello API key into the ini file.");
                Exit(2);
            }

            if (!userOptions.ContainsValidTrelloToken)
            {
                GetTrelloAuthorisationToken(userOptions.TrelloApiKey);
                Exit(1);
            }

            if (userOptions.Valid)
            {
                try
                {
                    Run(userOptions, CommandLineArgs.Create(args));
                }
                catch (System.Net.Http.HttpRequestException e)
                {
                    logger.Error("Suspected bad authorization token");
                    logger.Error(e);
                    Exit(3);
                }
                catch (Exception e)
                {
                    logger.Error(e);
                    Exit(3);
                }
            }
            else
            {
                logger.Warn("User options file '{0}' not valid. Exiting.",
                    Settings.Default.IniFileName);
            }

            Exit();
        }

        /// <summary>
        /// Template method that runs the application after all the housekeeping is completed.
        /// </summary>
        /// <param name="userOptions">The user options.</param>
        /// <param name="args">The command line arguments.</param>
        private static void Run(UserOptions userOptions, CommandLineArgs args)
        {
			logger.Debug("Running");

			logger.Debug("Done");
        }

        /// <summary>
        /// Gets the Trello authorisation token.
        /// </summary>
        /// <param name="trelloApiKey">The Trello API key.</param>
        private static void GetTrelloAuthorisationToken(string trelloApiKey)
        {
            Uri trelloAuthUri = new Uri(
                String.Format(Settings.Default.TrelloAuthUrl,
                    trelloApiKey,
                    Settings.Default.AppName,
                    "never"));
            // TODO: The expiry options are 1hour, 1day, 30days, never. Should this be user selectable?

            if (System.Windows.Forms.MessageBox.Show(
                Settings.Default.TrelloAuthRequest,
                Settings.Default.TrelloAuthRequestCaption,
                System.Windows.Forms.MessageBoxButtons.OKCancel,
                System.Windows.Forms.MessageBoxIcon.Asterisk) == System.Windows.Forms.DialogResult.OK)
            {
                // TODO: can I use a different method to make the web request directly, capture the token and write it to the ini file?
                logger.Info("Getting Trello authorisation token.");
                System.Diagnostics.Process.Start(trelloAuthUri.ToString());
            }
            else
            {
                logger.Warn("Trello authorisation cancelled.");
            }
        }

        /// <summary>
        /// Exits the application.
        /// </summary>
        /// <param name="exitCode">The exit code.</param>
        static void Exit(int exitCode = 0)
        {
            Console.WriteLine("\nPress any key to exit ...");
            Console.ReadKey();

            logger.Info("{0} {1} offline with exit code {2}",
                Assembly.GetExecutingAssembly().GetName().Name,
                Assembly.GetExecutingAssembly().GetName().Version,
                exitCode);

            Environment.Exit(exitCode);
        }
    }
}

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

            try
            {
                UserOptions userOptions = UserOptions.Create(
                    new FileInfo(Path.Combine(
                        Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                        Settings.Default.IniFileName)),
                    args);

                Run(userOptions);
            }
            catch (InvalidCredentialsException e)
            {
                logger.Error(e);
                Exit(1);
            }
            catch (InvalidUserOptionsException e)
            {
                logger.Error(e);
                Exit(2);
            }
            catch (Exception e)
            {
                logger.Error(e);
                Exit(3);
            }

            Exit();
        }

        /// <summary>
        /// Template method that runs the application after all the housekeeping is completed.
        /// </summary>
        /// <param name="userOptions">The user options.</param>
        private static void Run(UserOptions userOptions)
        {
            logger.Debug("Running");

            logger.Debug("Done");
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

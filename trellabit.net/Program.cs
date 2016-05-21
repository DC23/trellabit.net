using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using NLog;
using MadMilkman.Ini;
using System.IO;

namespace trellabit.net
{
    class Program
    {
        static Logger logger = LogManager.GetCurrentClassLogger();

        static void Main(string[] args)
        {
            logger.Info("{0} {1} online",
                Assembly.GetExecutingAssembly().GetName().Name,
                Assembly.GetExecutingAssembly().GetName().Version);

            UserOptions userOptions = new UserOptions(
                new FileInfo(Path.Combine(
                    Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), 
                    Settings.Default.IniFileName)));

            if (userOptions.Valid)
            {

                // Normal program flow here
            }
            else
            {
                logger.Warn("User options file '{0}' not valid. Exiting.",
                    Settings.Default.IniFileName);
            }

            Exit();
        }

        static void Exit()
        {
            Console.WriteLine("\nPress any key to exit ...");
            Console.ReadKey();

            logger.Info("{0} {1} offline",
                Assembly.GetExecutingAssembly().GetName().Name,
                Assembly.GetExecutingAssembly().GetName().Version);
        }
    }
}

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
        private static Logger logger = LogManager.GetCurrentClassLogger();

        static void Main(string[] args)
        {
            logger.Info("{0} {1} online",
                Assembly.GetExecutingAssembly().GetName().Name,
                Assembly.GetExecutingAssembly().GetName().Version);

            if (File.Exists(Settings.Default.IniFileName))
            {
                // Normal program flow here
                // load ini file
            }
            else
            {
                WriteDefaultIniFile(Settings.Default.IniFileName);

                logger.Warn("User settings file '{0}' not found. A default has been generated. You will need to enter your authentication keys as described in the README.",
                    Settings.Default.IniFileName);
            }

            Exit();
        }

        static void WriteDefaultIniFile(string filename)
        {
            logger.Info("Writing default ini file '{0}'", filename);

            IniFile file = new IniFile();

            // Trello section
            IniSection trelloSection = file.Sections.Add("Trello");
            trelloSection.TrailingComment.Text = "Trello Authentication";

            // Habitica section
            IniSection habiticaSection = file.Sections.Add("Habitica");
            habiticaSection.TrailingComment.Text = "Habitica Authentication";

            file.Save(filename);
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using NLog;
using MadMilkman.Ini;
using System.IO;
using TrelloNet;

namespace trellabit.net
{
    class Program
    {
        static Logger logger = LogManager.GetCurrentClassLogger();

        static void Main(string[] args)
        {
            logger.Info("Version {0} online",
                Assembly.GetExecutingAssembly().GetName().Version);

            UserOptions userOptions = new UserOptions(
                new FileInfo(Path.Combine(
                    Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                    Settings.Default.IniFileName)));

            if (userOptions.Valid)
            {
                ITrello trello = new Trello(userOptions.TrelloApiKey);

                if (String.IsNullOrEmpty(userOptions.TrelloToken))
                {
                    GetAuthorisationToken(trello);
                    Exit(1);
                }

                // what happens with an expired token? An exception I presume...
                trello.Authorize(userOptions.TrelloToken);

                Member me = trello.Members.Me();
                var myCards = trello.Cards.ForMe();
                var myBoards = trello.Boards.ForMe();
            }
            else
            {
                logger.Warn("User options file '{0}' not valid. Exiting.",
                    Settings.Default.IniFileName);
            }

            Exit();
        }

        private static void GetAuthorisationToken(ITrello trello)
        {
            var url = trello.GetAuthorizationUrl(Settings.Default.AppName, Scope.ReadWrite, Expiration.OneDay);
            if (System.Windows.Forms.MessageBox.Show(
                Settings.Default.TrelloAuthRequest,
                Settings.Default.TrelloAuthRequestCaption,
                System.Windows.Forms.MessageBoxButtons.OKCancel,
                System.Windows.Forms.MessageBoxIcon.Asterisk) == System.Windows.Forms.DialogResult.OK)
            {
                logger.Info("Getting Trello authorisation token.");
                System.Diagnostics.Process.Start(url.ToString());
            }
            else
            {
                logger.Warn("Trello authorisation cancelled.");
            }
        }

        static void Exit(int code = 0)
        {
            Console.WriteLine("\nPress any key to exit ...");
            Console.ReadKey();

            logger.Info("{0} {1} offline",
                Assembly.GetExecutingAssembly().GetName().Name,
                Assembly.GetExecutingAssembly().GetName().Version);

            Environment.Exit(code);
        }
    }
}

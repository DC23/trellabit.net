using NLog;
using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace trellabit.cli
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

            if (!userOptions.IsTrelloApiKeyValid)
            {
                logger.Warn("You must paste your Trello API key into the ini file.");
                Exit(2);
            }

            if (!userOptions.IsTrelloTokenValid)
            {
                GetAuthorisationToken(userOptions.TrelloApiKey);
                Exit(1);
            }

            if (userOptions.Valid)
            {
                try
                {
                    Run(userOptions);
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

        private static void Run(UserOptions userOptions)
        {
            // quick test of Manatee hacked directly into the housekeeping code
            //var serializer = new ManateeSerializer();
            //TrelloConfiguration.Serializer = serializer;
            //TrelloConfiguration.Deserializer = serializer;
            //TrelloConfiguration.JsonFactory = new ManateeFactory();
            //TrelloConfiguration.RestClientProvider = new WebApiClientProvider();
            //TrelloAuthorization.Default.AppKey = userOptions.TrelloApiKey;
            //TrelloAuthorization.Default.UserToken = userOptions.TrelloToken;
            //foreach (var board in Member.Me.Boards)
            //{
            //    Console.WriteLine(
            //        String.Format("{0}: Archived: {1}, Cards: {2}",
            //            board.Name,
            //            board.IsClosed,
            //            board.Cards.Count()));
            //}
        }

        // TODO: where is a good home for this method?
        private static void GetAuthorisationToken(string trelloApiKey)
        {
            Uri trelloAuthUri = new Uri(
                String.Format(Settings.Default.TrelloAuthUrl,
                    trelloApiKey,
                    Settings.Default.AppName,
                    "never"));
            // Expiry options: 1hour, 1day, 30days, never

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

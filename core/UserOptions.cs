using CommandLine;
using CommandLine.Text;
using MadMilkman.Ini;
using NLog;
using System;
using System.IO;

namespace trellabit.core
{
    /// <summary>
    /// User-editable options for trellabit.
    /// 
    /// Some options can be edited in the trellabit.ini file, while others can 
    /// also be supplied via the command line.
    ///
    /// For added security, the ini file can be encrypted once you have entered 
    /// your authentication tokens.
    /// </summary>
    public sealed class UserOptions
    {
        static Logger logger = LogManager.GetCurrentClassLogger();

        #region Creation
        /// <summary>
        /// Creates the user options class.
        /// </summary>
        /// <param name="settingsFileInfo">The settings file information.</param>
        /// <param name="args">The program entry function args array.</param>
        /// <returns>The parsed user options</returns>
        public static UserOptions Create(FileInfo settingsFileInfo, string[] args)
        {
            UserOptions uo = new UserOptions();

            // load the ini file, or create a default
            if (settingsFileInfo.Exists)
                uo.IniFile.Load(settingsFileInfo.FullName);
            else
                WriteDefaultIniFile(settingsFileInfo, uo.IniFile);

            // parse the command line
            CommandLine.Parser.Default.ParseArguments(args, uo);

            // validation
            if (!uo.ContainsValidTrelloApiKey)
                throw new InvalidCredentialsException("You must paste your Trello API key into the ini file");

            if (!uo.ContainsValidTrelloToken)
            {
                GetTrelloAuthorisationToken(uo.TrelloApiKey);
                throw new InvalidCredentialsException("You must paste the Trello authorisation token into the ini file");
            }

            if (!uo.Valid)
                throw new InvalidUserOptionsException();

            return uo;
        }

        /// <summary>
        /// Gets the Trello authorisation token.
        /// </summary>
        /// <param name="trelloApiKey">The Trello API key.</param>
        private static void GetTrelloAuthorisationToken(string trelloApiKey)
        {
            Uri trelloAuthUri = new Uri(
                string.Format(Settings.Default.TrelloAuthUrl,
                    trelloApiKey,
                    Settings.Default.AppName,
                    Settings.Default.TrelloAuthTokenExpiry));
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
        /// Writes a default ini file.
        /// </summary>
        /// <param name="settingsFileInfo">The settings file information.</param>
        /// <param name="iniFile">The ini file.</param>
        private static void WriteDefaultIniFile(FileInfo settingsFileInfo, IniFile iniFile)
        {
            logger.Warn(@"User options file '{0}' not found. A default has been generated. You will need to enter your authentication keys as described in the README.",
                settingsFileInfo.FullName);

            // Trello section
            IniSection trelloSection = iniFile.Sections.Add("Trello");
            trelloSection.TrailingComment.Text = " Trello Authentication";

            IniKey trelloApiKey = trelloSection.Keys.Add("API_Key", "    ");
            trelloApiKey.LeadingComment.Text = " Visit https://trello.com/1/appKey/generate to get your Trello API key";

            IniKey trelloAuthToken = trelloSection.Keys.Add("auth_token", "    ");
            trelloAuthToken.LeadingComment.Text = " Paste your authorisation token here";

            // Habitica section
            IniSection habiticaSection = iniFile.Sections.Add("Habitica");
            habiticaSection.TrailingComment.EmptyLinesBefore = 1;
            habiticaSection.TrailingComment.Text = " Habitica Authentication";

            iniFile.Save(settingsFileInfo.FullName);
        }

        /// <summary>
        /// Prevents a default instance of the <see cref="UserOptions"/> class from being created.
        /// </summary>
        private UserOptions() { }
        #endregion

        #region Command-line Arguments

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
        #endregion

        #region Ini file settings
        /// <summary>
        /// Gets a value indicating whether this instance contains a valid Trello API key.
        /// </summary>
        /// <value>
        /// <c>true</c> if Trello API key is valid; otherwise, <c>false</c>.
        /// </value>
        public bool ContainsValidTrelloApiKey
        {
            get
            {
                return true;
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance contains a valid Trello token.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance contains a valid Trello token; otherwise, <c>false</c>.
        /// </value>
        public bool ContainsValidTrelloToken
        {
            get
            {
                return true;
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance contains a valid Habitica token.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance contains a valid Habitica token; otherwise, <c>false</c>.
        /// </value>
        public bool ContainsValidHabiticaApiKey
        {
            get
            {
                if (!IniFile.Sections.Contains("Habitica"))
                {
                    logger.Warn("Habitica section missing");
                    return false;
                }

                return true;
            }
        }

        /// <summary>
        /// Gets a value indicating whether this <see cref="UserOptions"/> is valid.
        /// </summary>
        /// <value>
        ///   <c>true</c> if valid; otherwise, <c>false</c>.
        /// </value>
        public bool Valid
        {
            get
            {
                return ContainsValidTrelloApiKey
                    && ContainsValidTrelloToken
                    && ContainsValidHabiticaApiKey;
            }
        }

        /// <summary>
        /// Gets or sets the ini file.
        /// </summary>
        /// <value>
        /// The ini file.
        /// </value>
        private IniFile IniFile { get; set; } = new IniFile();

        /// <summary>
        /// Gets the Trello API key.
        /// </summary>
        /// <value>
        /// The Trello API key.
        /// </value>
        public string TrelloApiKey { get { return IniFile.Sections["Trello"].Keys["API_Key"].Value.Trim(); } }

        /// <summary>
        /// Gets the Trello token.
        /// </summary>
        /// <value>
        /// The Trello token.
        /// </value>
        public string TrelloToken { get { return IniFile.Sections["Trello"].Keys["auth_token"].Value.Trim(); } }
        #endregion
    }
}

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
        const int INI_VERSION = 2;

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

            // Parse the command line options first, as we need the ini password
            CommandLine.Parser.Default.ParseArguments(args, uo);

            IniOptions iniOptions = new IniOptions()
            {
                EncryptionPassword = uo.IniPassword,
            };

            // load the ini file, or create a default
            if (File.Exists(settingsFileInfo.FullName))
            {
                uo.IniFile = new IniFile(iniOptions);
                try
                {
                    uo.IniFile.Load(settingsFileInfo.FullName);

                    if (uo.DecryptIniFile)
                    {
                        logger.Warn("Decrypting ini file");

                        // create a plain text ini, copy the sections, and save
                        iniOptions.EncryptionPassword = null;
                        IniFile plainText = new IniFile(iniOptions);
                        foreach (var section in uo.IniFile.Sections)
                            plainText.Sections.Add(section.Copy(plainText));
                        plainText.Save(settingsFileInfo.FullName);
                    }
                }
                catch (System.FormatException)
                {
                    logger.Warn("Password supplied for decrypted ini file. Applying encryption.");

                    iniOptions.EncryptionPassword = null;
                    IniFile plainText = new IniFile(iniOptions);

                    // open without a password
                    plainText.Load(settingsFileInfo.FullName);

                    // copy plainText to the encrypted instance
                    foreach (var section in plainText.Sections)
                        uo.IniFile.Sections.Add(section.Copy(uo.IniFile));

                    // save the new instance
                    uo.IniFile.Save(settingsFileInfo.FullName);
                }
                catch (System.Security.Cryptography.CryptographicException e)
                {
                    logger.Error(e);
                    throw new InvalidIniPasswordException("Invalid password for encrypted ini file", e);
                }
            }
            else
            {
                // we must clear the password, as the user cannot paste their tokens into an encrypted file
                // Additionally, the password cannot be changed after creating the IniFile, thus
                // the two separate instantiations (here and above).
                iniOptions.EncryptionPassword = null;
                uo.IniFile = new IniFile(iniOptions);
                WriteDefaultIniFile(settingsFileInfo, uo.IniFile);
            }

            // validation
            if (uo.IniFile.Sections.Count == 1)
                throw new InvalidUserOptionsException("Incorrect ini file sections.");

            try
            {
                if (int.Parse(uo.IniFile.Sections["Metadata"].Keys["ini_version"].Value.Trim()) != INI_VERSION)
                    throw new InvalidUserOptionsException("Incorrect ini file version.");
            }
            catch (NullReferenceException e)
            {
                throw new InvalidUserOptionsException("Missing ini file version field.", e);
            }

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

            // metadata section
            IniSection mdSection = iniFile.Sections.Add("Metadata");
            mdSection.Keys.Add("ini_version", INI_VERSION.ToString());

            // Trello section
            IniSection trelloSection = iniFile.Sections.Add("Trello");
            trelloSection.TrailingComment.EmptyLinesBefore = 1;
            trelloSection.TrailingComment.Text = " Trello Authentication";

            IniKey trelloApiKey = trelloSection.Keys.Add("API_Key", "    ");
            trelloApiKey.LeadingComment.Text = " Visit https://trello.com/1/appKey/generate to get your Trello API key";

            IniKey trelloAuthToken = trelloSection.Keys.Add("auth_token", "    ");
            trelloAuthToken.LeadingComment.Text = " Paste your authorisation token here";

            // Habitica section
            IniSection habiticaSection = iniFile.Sections.Add("Habitica");
            habiticaSection.TrailingComment.EmptyLinesBefore = 1;
            habiticaSection.TrailingComment.Text = " Habitica Authentication";

            habiticaSection.Keys.Add("User_ID", "    ");
            trelloApiKey.LeadingComment.Text = " Visit https://habitica.com/#/options/settings/api to get your Habitica User ID";

            IniKey habiticaApiToken = habiticaSection.Keys.Add("API_Token", "    ");
            habiticaApiToken.LeadingComment.Text = " Visit https://habitica.com/#/options/settings/api to get your Habitica API token";

            iniFile.Save(settingsFileInfo.FullName);
        }

        /// <summary>
        /// Encrypts this instance.
        /// </summary>
        /// <exception cref="System.NotImplementedException"></exception>
        private void Encrypt(FileInfo iniFileInfo)
        {
        }

        /// <summary>
        /// Prevents a default instance of the <see cref="UserOptions"/> class from being created.
        /// </summary>
        private UserOptions() { }
        #endregion

        #region Command-line Arguments

        /// <summary>
        /// Gets or sets the ini password.
        /// </summary>
        /// <value>
        /// The ini password.
        /// </value>
        [Option("ini-password", Required = false, DefaultValue = null,
            HelpText = "The password used to encrypt the ini file. Once set, you need to supply the password every time or the file cannot be loaded.")]
        public string IniPassword { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to decrypt the ini file.
        /// </summary>
        /// <value>
        ///   <c>true</c> if the ini file will be decrypted; otherwise, <c>false</c>.
        /// </value>
        [Option("decrypt-ini", Required = false, DefaultValue = false,
            HelpText = "If set to true then the ini file will be written to disk as plain text")]
        public bool DecryptIniFile { get; set; }

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
        /// Gets a value indicating whether this instance contains a valid Habitica API token.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance contains a valid Habitica API token; otherwise, <c>false</c>.
        /// </value>
        public bool ContainsValidHabiticaApiToken
        {
            get
            {
                if (!IniFile.Sections.Contains("Habitica"))
                {
                    logger.Warn("Habitica section missing");
                    return false;
                }

                if (String.IsNullOrEmpty(HabiticaApiToken))
                {
                    logger.Warn("Habitica API Token missing");
                    return false;
                }

                return true;
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance contains a valid Habitica user ID.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance contains a valid Habitica user ID; otherwise, <c>false</c>.
        /// </value>
        public bool ContainsValidHabiticaUserId
        {
            get
            {
                if (!IniFile.Sections.Contains("Habitica"))
                {
                    logger.Warn("Habitica section missing");
                    return false;
                }

                if (String.IsNullOrEmpty(HabiticaUserId))
                {
                    logger.Warn("Habitica user ID missing");
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
                return IniFile.Sections.Count > 0
                    && ContainsValidTrelloApiKey
                    && ContainsValidTrelloToken
                    && ContainsValidHabiticaApiToken
                    && ContainsValidHabiticaUserId;
            }
        }

        /// <summary>
        /// Gets or sets the ini file.
        /// </summary>
        /// <value>
        /// The ini file.
        /// </value>
        private IniFile IniFile { get; set; }

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

        /// <summary>
        /// Gets the Habitica user identifier.
        /// </summary>
        /// <value>
        /// The Habitica user identifier.
        /// </value>
        public string HabiticaUserId { get { return IniFile.Sections["Habitica"].Keys["User_ID"].Value.Trim(); } }

        /// <summary>
        /// Gets the Habitica API token.
        /// </summary>
        /// <value>
        /// The Habitica API token.
        /// </value>
        public string HabiticaApiToken { get { return IniFile.Sections["Habitica"].Keys["API_Token"].Value.Trim(); } }

        #endregion
    }
}

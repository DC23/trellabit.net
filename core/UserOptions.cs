using MadMilkman.Ini;
using NLog;
using System.IO;

namespace trellabit.core
{
    /// <summary>
    /// User-editable options for trellabit.
    /// 
    /// The options can be edited in the trellabit.ini file.
    /// For added security, the ini file can be encrypted once you have entered 
    /// your authentication tokens.
    /// </summary>
    public sealed class UserOptions
	{
		static Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Initializes a new instance of the <see cref="UserOptions"/> class.
        /// </summary>
        /// <param name="settingsFileInfo">The settings file information.</param>
        public UserOptions(FileInfo settingsFileInfo)
		{
			SettingsFileInfo = settingsFileInfo;

			if (SettingsFileInfo.Exists)
			{
				IniFile.Load(SettingsFileInfo.FullName);
				logger.Debug("Trello validation removed. See the Git tag trello_usersettings_validation");
			}
			else
			{
				WriteDefaultIniFile();
			}
		}

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
        /// Writes a default ini file.
        /// </summary>
        private void WriteDefaultIniFile()
		{
			logger.Warn(@"User options file '{0}' not found. A default has been generated. You will need to enter your authentication keys as described in the README.",
				SettingsFileInfo.FullName);

			// Trello section
			IniSection trelloSection = IniFile.Sections.Add("Trello");
			trelloSection.TrailingComment.Text = " Trello Authentication";

            IniKey trelloApiKey = trelloSection.Keys.Add("API_Key", "    ");
			trelloApiKey.LeadingComment.Text = " Visit https://trello.com/1/appKey/generate to get your Trello API key";

			IniKey trelloAuthToken = trelloSection.Keys.Add("auth_token", "    ");
			trelloAuthToken.LeadingComment.Text = " Paste your authorisation token here";

			// Habitica section
			IniSection habiticaSection = IniFile.Sections.Add("Habitica");
            habiticaSection.TrailingComment.EmptyLinesBefore = 1;
			habiticaSection.TrailingComment.Text = " Habitica Authentication";

			IniFile.Save(SettingsFileInfo.FullName);
		}

        /// <summary>
        /// Gets the settings file information.
        /// </summary>
        /// <value>
        /// The settings file information.
        /// </value>
        public FileInfo SettingsFileInfo { get; private set; }

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
	}
}

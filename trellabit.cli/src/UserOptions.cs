using MadMilkman.Ini;
using NLog;
using System.IO;

namespace trellabit.cli
{
	internal class UserOptions
	{
		static Logger logger = LogManager.GetCurrentClassLogger();

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

		public bool IsTrelloApiKeyValid
		{
			get
			{
				return true;
			}
		}

		public bool IsTrelloTokenValid
		{
			get
			{ 
				return true;
			}
		}

		public bool IsHabiticaApiKeyValid
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

		public bool Valid
		{
			get
			{
				return IsTrelloApiKeyValid && IsTrelloTokenValid && IsHabiticaApiKeyValid;
			}
		}

		void WriteDefaultIniFile()
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

		public FileInfo SettingsFileInfo { get; private set; }

		private IniFile IniFile { get; set; } = new IniFile();

		public string TrelloApiKey { get { return IniFile.Sections["Trello"].Keys["API_Key"].Value.Trim(); } }

		public string TrelloToken { get { return IniFile.Sections["Trello"].Keys["auth_token"].Value.Trim(); } }
	}
}

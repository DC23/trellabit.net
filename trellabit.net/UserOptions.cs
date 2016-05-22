using NLog;
using MadMilkman.Ini;
using System.IO;

namespace trellabit.net
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
                Valid = Validate();
            }
            else
            {
                WriteDefaultIniFile();
                Valid = false;
            }
        }

        private bool Validate()
        {
            if (!IniFile.Sections.Contains("Trello"))
            {
                logger.Warn("Trello section missing");
                return false;
            }

            if (!IniFile.Sections["Trello"].Keys.Contains("API_Key"))
            { 
                logger.Warn("Trello API_Key missing");
                return false;
            }

            if (IniFile.Sections["Trello"].Keys["API_Key"].Value == "<your_api_key>")
            { 
                logger.Warn("Trello API_Key invalid");
                return false;
            }

            //if (!IniFile.Sections["Trello"].Keys.Contains("auth_token"))
            //{ 
            //    logger.Warn("Trello auth_token missing");
            //    return false;
            //}

            //if (String.IsNullOrEmpty(IniFile.Sections["Trello"].Keys["auth_token"].Value))
            //{ 
            //    logger.Warn("Trello auth_token invalid");
            //    return false;
            //}

            if (!IniFile.Sections.Contains("Habitica"))
            { 
                logger.Warn("Habitica section missing");
                return false;
            }

            return true;
        }

        void WriteDefaultIniFile()
        {
            logger.Warn(@"User options file '{0}' not found. A default has been generated. You will need to enter your authentication keys as described in the README.",
                SettingsFileInfo.FullName);

            // Trello section
            IniSection trelloSection = IniFile.Sections.Add("Trello");
            trelloSection.TrailingComment.Text = " Trello Authentication";
            IniKey trelloApiKey = trelloSection.Keys.Add("API_Key", "<your_api_key>");
            trelloApiKey.LeadingComment.Text = " Visit https://trello.com/1/appKey/generate to get your Trello API key";
            IniKey trelloAuthToken = trelloSection.Keys.Add("auth_token", "");
            trelloAuthToken.LeadingComment.Text = " Paste your authorisation token here";

            // Habitica section
            IniSection habiticaSection = IniFile.Sections.Add("Habitica");
            habiticaSection.TrailingComment.Text = " Habitica Authentication";

            IniFile.Save(SettingsFileInfo.FullName);
        }

        public bool Valid { get; private set; } = false;
        public FileInfo SettingsFileInfo { get; private set; }
        private IniFile IniFile { get; set; } = new IniFile();

        public string TrelloApiKey { get { return IniFile.Sections["Trello"].Keys["API_Key"].Value; } }
        public string TrelloToken { get { return IniFile.Sections["Trello"].Keys["auth_token"].Value; } }
    }
}

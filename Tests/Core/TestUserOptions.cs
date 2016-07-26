using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trellabit.Core;
using System.IO;
using System.Reflection;
using Xunit;

namespace Trellabit.Tests.Core
{
    public class TestUserOptions
    {
        private string GetWritableFileName(string baseName)
        {
            return Path.Combine(
                Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                baseName);
        }

        private FileInfo GetWriteableFileInfo(string baseName, bool delete = false)
        {
            var fi = new FileInfo(GetWritableFileName(baseName));
            if (delete)
                fi.Delete();

            return fi;
        }

        private FileInfo GetTestIni(
            string filename,
            string trello_key = "tkey",
            string trello_token = "ttoken",
            string habitica_id = "hab_id",
            string habitica_token = "hab_token")
        {
            var ini = GetWriteableFileInfo(filename);
            using (var writer = ini.CreateText())
            {
                writer.Write($@"
                [Metadata]
                ini_version=2

                [Trello]
                API_Key={trello_key}
                auth_token={trello_token}

                [Habitica]
                User_ID={habitica_id}
                API_Token={habitica_token}
                ");
            }

            return ini;
        }

        [Fact]
        public void DefaultIniCreatedWhenFileDoesntExist()
        {
            // we want a temp file name in a writable location
            var ini = GetWriteableFileInfo("defaultinicreated.ini", delete: true);
            Assert.False(File.Exists(ini.FullName));

            // get UserOptions to create a default file
            var options = UserOptions.Create(ini, new string[0]);

            Assert.True(File.Exists(ini.FullName));

            // check for validity by opening the newly minted file
            options = UserOptions.Create(ini, new string[0]);

            Assert.NotNull(options);
            Assert.Equal("", options.TrelloApiKey);
            Assert.Equal("", options.TrelloToken);
            Assert.Equal("", options.HabiticaUserId);
            Assert.Equal("", options.HabiticaApiToken);
        }

        [Fact]
        public void EncryptionOfPlainText()
        {
            // Create a plaintext ini file, then open it with UserOptions
            var ini = GetTestIni("testEncrypt.ini");
            UserOptions.Create(ini, new string[0]);

            // Now encrypt it
            UserOptions.Create(ini, new string[] { "--ini-password", "zzz" });

            // test that the file is now encrypted by trying to open without a password
            Assert.Throws<InvalidUserOptionsException>(() => UserOptions.Create(ini, new string[0]));
        }

        [Fact]
        public void EncryptionWrongPassword()
        {
            // Create an encrypted ini file from plain text
            var ini = GetTestIni("testEncryptedRead.ini");
            UserOptions.Create(ini, new string[] { "--ini-password", "fiffer feffer feff" });

            // Try opening with the wrong password
            Assert.Throws<InvalidIniPasswordException>(
                () => UserOptions.Create(ini, new string[] { "--ini-password", "fiffer" }));
        }

        [Fact]
        public void Decryption()
        {
            // Create encrypted ini
            var ini = GetTestIni("testDecrypt.ini");
            UserOptions.Create(ini, new string[] { "--ini-password", "zzz" });

            // test that the file is now encrypted
            Assert.Throws<InvalidUserOptionsException>(() => UserOptions.Create(ini, new string[0]));

            // decrypt it
            UserOptions.Create(ini, new string[] { "--ini-password", "zzz", "--decrypt-ini" });

            // Open without password
            var actual = UserOptions.Create(ini, new string[0]);

            Assert.Equal("tkey", actual.TrelloApiKey);
            Assert.Equal("ttoken", actual.TrelloToken);
            Assert.Equal("hab_id", actual.HabiticaUserId);
            Assert.Equal("hab_token", actual.HabiticaApiToken);
        }

        [Fact]
        public void ReadFromEncryptedIni()
        {
            // Create an encrypted ini file from plain text
            var ini = GetTestIni("testEncryptedRead.ini");
            UserOptions.Create(ini, new string[] { "--ini-password", "zzz" });

            // open it from the encrypted file
            var actual = UserOptions.Create(ini, new string[] { "--ini-password", "zzz" });

            Assert.Equal("tkey", actual.TrelloApiKey);
            Assert.Equal("ttoken", actual.TrelloToken);
            Assert.Equal("hab_id", actual.HabiticaUserId);
            Assert.Equal("hab_token", actual.HabiticaApiToken);
        }

        [Fact]
        public void MissingIniVersionKey()
        {
            var ini = GetWriteableFileInfo("missingIniVersion.ini");
            using (var writer = ini.CreateText())
            {
                writer.Write($@"
                [Trello]
                API_Key=blah
                auth_token=trello_token

                [Habitica]
                User_ID=habID
                API_Token=habAPI
                ");
            }

            Assert.Throws<InvalidUserOptionsException>(() => UserOptions.Create(ini, new string[0]));
        }

        [Fact]
        public void IncorrectIniVersionValue()
        {
            var ini = GetWriteableFileInfo("incorrectIniVersion.ini");
            using (var writer = ini.CreateText())
            {
                writer.Write($@"
                [Metadata]
                ini_version=1

                [Trello]
                API_Key=blah
                auth_token=trello_token

                [Habitica]
                User_ID=habID
                API_Token=habAPI
                ");
            }

            Assert.Throws<InvalidUserOptionsException>(() => UserOptions.Create(ini, new string[0]));
        }
    }
}

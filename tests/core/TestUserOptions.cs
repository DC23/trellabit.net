﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using trellabit.core;
using System.IO;
using System.Reflection;

namespace trellabit.tests.core
{
    [TestFixture]
    class TestUserOptions
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

        [Test]
        public void TestDefaultIniCreatedWhenFileDoesntExist()
        {
            // we want a temp file name in a writable location
            var iniPath = GetWriteableFileInfo("defaultinicreated.ini", delete: true);
            Assert.False(File.Exists(iniPath.FullName));

            // get UserOptions to create a default file
            UserOptions options = UserOptions.Create(iniPath, new string[0]);

            Assert.True(File.Exists(iniPath.FullName));

            // check for validity by opening the newly minted file
            options = UserOptions.Create(iniPath, new string[0]);

            Assert.IsNotNull(options);
        }

        [Test]
        public void TestEncryptPlainTextFile()
        {
            // we want a temp file name but no actual file
            var iniPath = GetWriteableFileInfo("testEncrypt.ini", delete: true);

            // get UserOptions to create a default file
            UserOptions options = UserOptions.Create(iniPath, new string[0]);

            // open a second UserOptions with the default file and a password
            options = UserOptions.Create(iniPath, new string[] { "--ini-password", "zzz" });

            // test that the file is now encrypted
            Assert.Throws<InvalidUserOptionsException>(() => UserOptions.Create(iniPath, new string[0]));
        }
    }
}
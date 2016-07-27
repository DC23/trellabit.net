using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trellabit.Services.Habitica.Converters;
using Xunit;

namespace Trellabit.Tests.Services.Habitica.Converters
{
    public class TestDateTimeJsonConverter
    {
        [Fact]
        public void CanConvertDateTime()
        {
            Assert.True(new DateTimeJsonConverter().CanConvert(typeof(DateTime)));
        }

        [Fact]
        public void WriteJson()
        {
        }

        [Fact]
        public void ReadJson()
        {
        }
    }
}

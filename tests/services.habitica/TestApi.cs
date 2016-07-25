using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using trellabit.data.habitica;
using Xunit;

namespace trellabit.tests.data.habitica
{
    public class TestApi
    {
        [Fact]
        public void TestGitHubApi()
        {
            var user = GitHubService.GetUser("DC23");
        }
    }
}

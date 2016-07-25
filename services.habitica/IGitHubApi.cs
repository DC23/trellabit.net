using Newtonsoft.Json;
using RestEase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace trellabit.services.habitica
{
    public class User
    {
        [JsonProperty("login")]
        public string Login { get; set; }

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }
    }

    [Header("User-Agent", "RestEase")]
    public interface IGitHubApi
    {
        [Get("/users/{userId}")]
        Task<User> GetUserAsync([Path] string userId);
    }

    public static class GitHubService 
    {
        public static User GetUser(string userId)
        {
            var api = RestClient.For<IGitHubApi>("https://api.github.com");
            return api.GetUserAsync(userId).Result;
        }
    }

}

using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace HackerNews.AzureFunctions
{
    public class SyncStoryFunction
    {
        private static readonly HttpClient _httpClient = new HttpClient();
        [FunctionName("SyncStoryFunction")]
        public static async Task Run([TimerTrigger("0 */5 * * * *")]TimerInfo myTimer, ILogger log)
        {

            // make the POST request
            var response = await _httpClient.PostAsync("https://hnapi.azurewebsites.net/Story",null);

            // use response body for further work if needed...
            var responseBody = response.Content.ReadAsStringAsync();

            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now} {responseBody}");
        }
    }
}

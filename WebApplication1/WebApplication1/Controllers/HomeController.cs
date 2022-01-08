using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private static Data res = new Data();
        private const string APP_ID = "61d6b6e412985555f8325d7c";
        private const string API_URL = "https://dummyapi.io/data/";
        private async void fetchAPI()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(API_URL);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("app-id", APP_ID);
            var response = await client.GetAsync("v1/user?limit=10");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                Data da = JsonConvert.DeserializeObject<Data>(content);
                da.data.RemoveAll(item => item.title != "mr");
                res = da;
            }
        }
        [HttpGet]
        public Data Get()
        {
            fetchAPI();
            return res;
        }
    }
}

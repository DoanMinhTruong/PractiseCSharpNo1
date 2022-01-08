using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace Jewel.api
{
    public class Category
    {
        public int id { get; set; }
        public string name { get; set; }
        public string slug { get; set; }
     
    }


    class CategoryAPI
    {
        private HttpClient client;
        private HttpResponseMessage response;

        public CategoryAPI()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://hspacep1.herokuapp.com/api/category/");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
        public List<Category> ReadAll()
        {
            response = client.GetAsync("read/").Result;
            List<Category> rs = new List<Category>();
            if (response.IsSuccessStatusCode)
            {
                rs = JsonConvert.DeserializeObject<List<Category>>(response.Content.ReadAsStringAsync().Result);
            }
            return rs;
        }
        


    }
}

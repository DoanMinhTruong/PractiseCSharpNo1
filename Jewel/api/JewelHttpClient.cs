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
    public class Jewel
    {
        public int id { get; set; }
        public string name { get; set; }
        public string slug { get; set; }
        public string price { get; set; }
        public string image { get; set; }
        public bool is_public { get; set; }
        public int category_id { get; set; }
        public int created_by_id { get; set; }
    }
    

    class JewelAPI
    {
        private HttpClient client;
        private HttpResponseMessage response;
        
        public JewelAPI()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://hspacep1.herokuapp.com/api/jewel/");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
        public List<Jewel> ReadAll()
        {
            response = client.GetAsync("read/").Result;
            List<Jewel> rs = new List<Jewel>();
            if (response.IsSuccessStatusCode)
            {
                rs = JsonConvert.DeserializeObject<List<Jewel>>(response.Content.ReadAsStringAsync().Result);
            }
            return rs;
        }
        public List<Jewel> ReadByCreated(int id)
        {
            response = client.GetAsync("read/created_by/" + id + "/").Result;
            List<Jewel> rs = new List<Jewel>();
            if (response.IsSuccessStatusCode)
            {
                rs = JsonConvert.DeserializeObject<List<Jewel>>(response.Content.ReadAsStringAsync().Result);
            }
            return rs;
        }
       
        
    }
}

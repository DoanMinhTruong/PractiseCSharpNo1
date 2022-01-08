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
    public class User
    {
        public int id { get; set; }
        public string username { get; set; }
        public string email { get; set; }
        public string password { get; set; }
    }
    

    class UserAPI
    {
        private HttpClient client;
        private HttpResponseMessage response;
        private string responseLogin; 
        public UserAPI()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://hspacep1.herokuapp.com/api/user/");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
        public bool Create(User user)
        {
            var userSerialize = JsonConvert.SerializeObject(user);
            var byteContent = new ByteArrayContent(Encoding.UTF8.GetBytes(userSerialize)); 
            byteContent.Headers.ContentType =  new MediaTypeHeaderValue("application/json");
            response = client.PostAsync("create/", byteContent).Result;
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }
        public bool Login(User user)
        {
            var userSerialize = JsonConvert.SerializeObject(user);
            var byteContent = new ByteArrayContent(Encoding.UTF8.GetBytes(userSerialize));
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            response = client.PostAsync("login/", byteContent).Result;
            responseLogin = response.Content.ReadAsStringAsync().Result;
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }
        public string getResponseLogin()
        {
            return responseLogin;
        }
        public int getUserID(string accessToken)
        {
            
            response = client.GetAsync("read/" + accessToken).Result;
            if (response.IsSuccessStatusCode)
            {
                var dict = JsonConvert.DeserializeObject<List<Dictionary<string, string>>>(response.Content.ReadAsStringAsync().Result);
                return int.Parse(dict[0]["id"]);
            }
            return -1;
        }
        public string getUsernameById(int id)
        {
            response = client.GetAsync("read/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                var dict = JsonConvert.DeserializeObject<List<Dictionary<string, string>>>(response.Content.ReadAsStringAsync().Result);
                return dict[0]["username"];
            }
            return "";
        }
        public List<User> ReadAll()
        {
            response = client.GetAsync("read/").Result;
            var dict = new List<User>();
            if (response.IsSuccessStatusCode)
            {
                dict = JsonConvert.DeserializeObject<List<User>>(response.Content.ReadAsStringAsync().Result);
            }
            return dict;
        }
    }
}
 
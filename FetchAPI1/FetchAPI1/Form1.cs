using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Http.Headers;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text.Json;
using System.Reflection;
using System.IO;
using System.Net;

namespace FetchAPI1
{

    public partial class Form1 : Form
    {
        const string APP_ID = "61d6b6e412985555f8325d7c";
        const string API_URL = "https://dummyapi.io/data/";
        public Form1()
        {
            InitializeComponent();
            
        }
        
        private async void fetchAPI(int page = 0 , int limit = 5)
        {
            HttpClient client = new HttpClient();

            // Put the following code where you want to initialize the class
            // It can be the static constructor or a one-time initializer
            client.BaseAddress = new Uri(API_URL);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            
            client.DefaultRequestHeaders.Add("app-id", APP_ID);

            var response = await client.GetAsync("v1/user?page="+page+"&limit=" + limit);
            
            var data = new List<Person>();

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                Data da = JsonConvert.DeserializeObject<Data>(content);
               
                foreach (Person person in da.data)
                {
                    DataGridViewRow row = (DataGridViewRow)dataGridView1.Rows[0].Clone();
                    
                    row.Cells[0].Value = person.id;
                    row.Cells[1].Value = person.title;
                    row.Cells[2].Value = person.firstName;
                    row.Cells[3].Value = person.lastName;

                    HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(person.picture.ToString());
                    myRequest.Method = "GET";
                    HttpWebResponse myResponse = (HttpWebResponse)myRequest.GetResponse();
                    System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(myResponse.GetResponseStream());
                    myResponse.Close();

                    row.Cells[4].Value = bmp;
                    dataGridView1.Rows.Add(row);
                }
            }
            else MessageBox.Show("GET FAIL , TRY AGAIN ");
        }
       
        private void Form1_Load(object sender, EventArgs e)
        {
            this.ActiveControl = textBox1;
            dataGridView1.Dock = DockStyle.Fill;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string limitText = textBox1.Text;
            string pageText = textBox2.Text;

            if (int.TryParse(limitText, out int value) && int.TryParse(pageText, out int val))
            {
                int lim = int.Parse(limitText);
                int p   = int.Parse(pageText);
                fetchAPI(p , lim );
            }
            else
            {
                MessageBox.Show("Missing !!");
                textBox1.Text = "";
                textBox2.Text = "";
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();
        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}

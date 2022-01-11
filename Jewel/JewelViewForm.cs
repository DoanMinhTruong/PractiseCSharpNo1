using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Jewel
{
    public partial class JewelViewForm : Form
    {
        api.Jewel je;
        Dictionary<int, string> ctDict;
        Dictionary<int, string> userDict;
        public JewelViewForm(api.Jewel j)
        {
            InitializeComponent();
            je = j;
            ctDict = MyStaticClass.ctDict;
            userDict = MyStaticClass.userDict;  

        }
        private void load(api.Jewel j)
        {
           try
           {
                string url = "https://res.cloudinary.com/dvjruizqp/image/upload/v1641616094/" + j.image + ".jpg";
                var request = (HttpWebRequest)WebRequest.Create(url);
                //request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/96.0.4664.110 Safari/537.36";
                using (var response = request.GetResponse())
                using (var responseStream = response.GetResponseStream())
                {
                    picView.BackgroundImage = Bitmap.FromStream(responseStream);
                }
                lbNameView.Text  = j.name;
                lbPriceView.Text = j.price;
                lbCreatedView.Text = userDict[j.created_by_id];
                lbCategoryView.Text = ctDict[j.category_id];


            }
            catch (Exception ex)
           {
                MessageBox.Show(ex.ToString());
                this.Close();  
           }
        }
        private void JewelViewForm_Load(object sender, EventArgs e)
        {
           load(je);
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}

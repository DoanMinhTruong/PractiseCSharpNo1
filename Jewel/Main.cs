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
using System.Drawing;
namespace Jewel
{
    public partial class mainForm : Form
    {
        private api.JewelAPI jwapi;
        private api.CategoryAPI ctapi;
        private api.UserAPI userapi;
        private Dictionary<int, string> ctDict;
        private Dictionary<int, string> userDict;
        List<api.Jewel> ljw , ljwYour;
        List<api.Category> lstCate;
        List<api.User> lstUser;

        List<Bitmap> btm;
        public mainForm()
        {
            InitializeComponent();
            jwapi = new api.JewelAPI();
            ctapi = new api.CategoryAPI();
            userapi = new api.UserAPI();
            ljwYour = new List<api.Jewel>();
            ctDict = new Dictionary<int, string>();
            userDict = new Dictionary<int, string>();
            btm = new List<Bitmap>();
        }
        public Dictionary<int, string> getCtDict()
        {
            return ctDict;
        }
        public Dictionary<int, string> getUserDict()
        {
            return userDict;
        }
        private  void loadDataGrid(List<api.Jewel> li)
        {
            DataGridViewRow row;
            int cnt = 0;
          
            foreach (api.Jewel j in li)
            {
                row = (DataGridViewRow)dataGridView1.Rows[0].Clone();
                row.Cells[0].Value = ++cnt;
                row.Cells[1].Value = j.name;
                row.Cells[2].Value = j.price;
                row.Cells[3].Value = ctDict[j.category_id];
                row.Cells[4].Value = userDict[j.created_by_id];
                row.Cells[5].Value = j.image;

                /// FAIL CLOUDINARY
               /* myRequest = (HttpWebRequest)WebRequest.Create("https://res.cloudinary.com/dvjruizqp/image/upload/v1641616094/w_250,f_auto/" + j.image );
                myRequest.Method = "GET";
                myRequest.ContentType = "application/json";
                myResponse = (HttpWebResponse)myRequest.GetResponse();
                System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(myResponse.GetResponseStream());
                myResponse.Close();*/
                ////
                dataGridView1.Rows.Add(row);
            }
        }
        private void loadComboBox()
        {
            foreach(var item in ctDict.Values) cbbCategory.Items.Add(item);
            cbbCategory.Items.Add("-- ALL --");
        }
        
        
        private void mainForm_Load(object sender, EventArgs e)
        {
            

            ljw = jwapi.ReadAll();
            foreach (api.Jewel j in ljw) if (j.created_by_id == Properties.Settings.Default.userId) ljwYour.Add(j);


            lstCate = ctapi.ReadAll();
            lstUser = userapi.ReadAll();
            
            foreach (api.Category item in lstCate) ctDict.Add(item.id, item.name);
            foreach (api.User item in lstUser) userDict.Add(item.id, item.username);

            MyStaticClass.ctDict = ctDict;
            MyStaticClass.userDict = userDict;

            dataGridView1.AutoGenerateColumns = false;
            loadDataGrid(ljw);
            loadComboBox();
            
        }
        private void loadDataSelectCbb(List<api.Jewel> l)
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();
            List<api.Jewel> tmp = new List<api.Jewel>();
            foreach (api.Jewel j in l)
            {
                if(cbbCategory.SelectedItem == null)
                {
                    tmp.Add(j);
                    continue;
                }
                if (ctDict[j.category_id] == cbbCategory.SelectedItem.ToString())
                    tmp.Add(j);
                if (cbbCategory.SelectedItem.ToString() == "-- ALL --")
                    tmp.Add(j); 
            }
            loadDataGrid(tmp);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked) loadDataSelectCbb(ljwYour);
            else loadDataSelectCbb(ljw);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;

            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0)
            {
                JewelViewForm jf;
                if (checkBox1.Checked)
                {
                    //MessageBox.Show(ljwYour[e.RowIndex].name);
                    jf = new JewelViewForm(ljwYour[e.RowIndex]);
                }
                else jf = new JewelViewForm(ljw[e.RowIndex]);
                jf.Show();

            }
        }

        private void cbbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked) loadDataSelectCbb(ljwYour);
            else loadDataSelectCbb(ljw);
        }
    }
}

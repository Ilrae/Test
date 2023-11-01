using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _Prototype_
{
    public partial class Form1 : Form
    {
        MssqlLib mMssqlLib = new MssqlLib();

        public DateTime dt1;
        public DateTime dt2;
        bool isLoaded = false;

        public Form1()
        {
            InitializeComponent();
            this.Load += Form1_Load;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            isLoaded = true;
            DateControl();
            SetCompany();
        }

        private void SetCompany()
        {
            comboBox_company.Items.Clear();
           

            //string selectedstring = comboBox_company.DisplayMember;

            try
            {
                DataSet ds_Account = mMssqlLib.GetCompanyInfo();

                if (ds_Account.Tables.Count > 0)
                {            
                    comboBox_company.DataSource = ds_Account.Tables[0];
                    comboBox_company.DisplayMember = "ACCOUNT_NAME";
                   
                    Console.WriteLine(ds_Account.Tables[0]);
                }
                else
                    return;
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }

            

        }

        private void button16_Click(object sender, EventArgs e)
        {
            Console.WriteLine(dt1);
            Console.WriteLine(dt2);
            Search();
        }

        private void Search()
        {
            DateTime _dt1 = dt1;
            DateTime _dt2 = dt2;

            try
            {
                DataSet ds = mMssqlLib.GetUserInfo(_dt1,_dt2);

                if (ds.Tables.Count > 0)
                { 
                    dataGridView1.DataSource = ds.Tables[0];
                }
                else
                    return;
            }
            catch(SqlException ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }        
        }

        public void InsertDB(int id, string name)
        {
            string connectstr = System.Configuration.ConfigurationManager.AppSettings[];
            string connectString = string.Format("Server={0};Database={1};Uid ={2};Pwd={3};", "127.0.0.1",
                                                    "Moisture", "dnct", "dnct1234");
            string sql = $"Insert Into test (id,name) values ({id},'{name}')";

            using (SqlConnection conn = new SqlConnection(connectString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
            }
        }

        private void DateControl()
        {
            //dateTimePicker1.Value = DateTime.Now;
            dateTimePicker2.Value = DateTime.Now;
        }
        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            if (this.isLoaded == false) return;
            dt1 = dateTimePicker1.Value;
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            if (this.isLoaded == false) return;
            dt2 = dateTimePicker2.Value;
        }

        private void dateTimePicker1_VisibleChanged(object sender, EventArgs e)
        {
            if (this.isLoaded == false) return;
            dt1 = dateTimePicker1.Value;
        }

        private void button14_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
        }

        private void comboBox_company_DisplayMemberChanged(object sender, EventArgs e)
        {
            if(comboBox_company.DisplayMember != "TEST")
            {
                string sql =$"declare @p3 nvarchar(1000)" +
                $"set @p3 = N''" +
                $"exec GetMeasureListProc " +
                $"@INFDS = N'COMPANY=1;DEPTCD=10;EMPID=9999;USERID=admin;DEVICE=DESKTOP-9Q3QUQV;IPNO=192.1.3.217;LANG=ko-KR'," +
                $"@RMODE = N'SL1'," +
                $"@RTNCD = @p3 output," +
                $"@COMPANY_ID = 1," +
                $"@DEPT_CODE = N'10'," +
                $"@FROM_DATE = N'({dt1})'," +
                $"@TO_DATE = N'{dt2}'," +
                $"@ACCOUNT_ID = N''," +
                $"@ITEM_TYPE = N''," +
                $"@VEHICLE_NO = N''select @p3";
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _Prototype_
{
    class MssqlLib
    {
        public bool ConnectionTest()
        {
            string connectString = string.Format("Server={0};Database={1};Uid={2};Pwd={3};","127.0.0.1", "Moisture", "dnct", "dnct1234");
            try
            {
                using (SqlConnection conn = new SqlConnection(connectString))
                {
                    conn.Open();
                }
                return true;
            }
            catch(Exception)
            {
                return false;
            }
        }

        public void SelectDB(DateTime _dt1,DateTime _dt2)
        {           
            string connectstring = string.Format("Server={0};Database={1};Uid={2};Pwd={3};", "127.0.0.1", "Moisture", "dnct", "dnct1234");
            string sql = 
                $"declare @p3 nvarchar(1000)" +
                $"set @p3 = N''" +
                $"exec GetMeasureListProc " +
                $"@INFDS = N'COMPANY=1;DEPTCD=10;EMPID=9999;USERID=admin;DEVICE=DESKTOP-9Q3QUQV;IPNO=192.1.3.217;LANG=ko-KR'," +
                $"@RMODE = N'SL1'," +
                $"@RTNCD = @p3 output," +
                $"@COMPANY_ID = 1," +
                $"@DEPT_CODE = N'10'," +
                $"@FROM_DATE = N'({_dt1})'," +
                $"@TO_DATE = N'{_dt2}'," +
                $"@ACCOUNT_ID = N''," +
                $"@ITEM_TYPE = N''," +
                $"@VEHICLE_NO = N''select @p3";

            using (SqlConnection conn = new SqlConnection(connectstring))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader dr = cmd.ExecuteReader();
                dr.Close();
            }
        }

        //public void InsertDB(int id, string name)
        //{
        //    string connectString = string.Format("Server={0};Database={1};Uid ={2};Pwd={3};", "127.0.0.1",
        //                        "Moisture", "dnct", "dnct1234");
        //    string sql = $"Insert Into test (id,name) values ({id},'{name}')";

        //    using (SqlConnection conn = new SqlConnection(connectString))
        //    {
        //        conn.Open();
        //        SqlCommand cmd = new SqlCommand(sql, conn);
        //        cmd.ExecuteNonQuery();
        //    }
        //}

        public DataSet GetUserInfo(DateTime _dt1,DateTime _dt2)
        {
            string connectstring = string.Format("Server={0};Database={1};Uid={2};Pwd={3};", "127.0.0.1", "Moisture", "dnct", "dnct1234");
            string sql = $"declare @p3 nvarchar(1000)set @p3 = N''exec GetMeasureListProc @INFDS = N'COMPANY=1;DEPTCD=10;EMPID=1000;USERID=dnct;DEVICE=LAPTOP-2LRRTI7L;IPNO=127.0.0.1;LANG=ko-KR',@RMODE=N'SL1',@RTNCD=@p3 output,@COMPANY_ID=1,@DEPT_CODE=N'10',@FROM_DATE=N'{_dt1}',@TO_DATE=N'{_dt2}',@ACCOUNT_ID = N'',@ITEM_TYPE = N'',@VEHICLE_NO = N''select @p3";
                                                                        
            
            DataSet ds = new DataSet();

            using (SqlConnection conn = new SqlConnection(connectstring))
            {
                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                da.Fill(ds);
            }
            return ds;
        }

        public DataSet GetUserInfo(DateTime _dt1, DateTime _dt2,string _AccountName)
        {
            string connectstring = string.Format("Server={0};Database={1};Uid={2};Pwd={3};", "127.0.0.1", "Moisture", "dnct", "dnct1234");
            string sql = $"declare @p3 nvarchar(1000)set @p3 = N''exec GetMeasureListProc @INFDS = N'COMPANY=1;DEPTCD=10;EMPID=1000;USERID=dnct;DEVICE=LAPTOP-2LRRTI7L;IPNO=127.0.0.1;LANG=ko-KR',@RMODE=N'SL1',@RTNCD=@p3 output,@COMPANY_ID=1,@DEPT_CODE=N'10',@FROM_DATE=N'{_dt1}',@TO_DATE=N'{_dt2}',@ACCOUNT_ID = N'',@ITEM_TYPE = N'',@VEHICLE_NO = N''select @p3";


            DataSet ds = new DataSet();

            using (SqlConnection conn = new SqlConnection(connectstring))
            {
                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                da.Fill(ds);
            }
            return ds;
        }

        public DataSet GetCompanyInfo()
        {
            string connectstring = string.Format("Server={0};Database={1};Uid={2};Pwd={3};", "127.0.0.1", "Moisture", "dnct", "dnct1234");
            string sql = "select ACCOUNT_NAME from dbo.account_master";

            DataSet ds_comp = new DataSet();

            using (SqlConnection conn = new SqlConnection(connectstring))
            {
                conn.Open();
                SqlDataAdapter da_comp = new SqlDataAdapter(sql, conn);
                Console.WriteLine(ds_comp);
                da_comp.Fill(ds_comp);
            }
            return ds_comp;
        }
    }
}

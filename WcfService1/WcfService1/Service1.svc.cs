using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Data;

namespace WcfService1
{
    // 참고: "리팩터링" 메뉴에서 "이름 바꾸기" 명령을 사용하여 코드, svc 및 config 파일에서 클래스 이름 "Service1"을 변경할 수 있습니다.
    // 참고: 이 서비스를 테스트하기 위해 WCF 테스트 클라이언트를 시작하려면 솔루션 탐색기에서Service1.svc나 Service1.svc.cs를 선택하고 디버깅을 시작하십시오.
    public class Service1 : IService1
    {
        public string Insert(InsertUser user)
        {
            string msg;
            try
            {
                SqlConnection con = new SqlConnection("Data Source=LAPTOP-2LRRTI7L;Initial Catalog=MyTestDB;Persist Security Info=True;User ID=dnct;Password=dnct1234;Pooling=False");
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                string sql = "INSERT INTO UserTab (Name,Email) values(@Name,@Email)";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@Name", user.Name);
                cmd.Parameters.AddWithValue("@Email", user.Email);

                cmd.ExecuteNonQuery();
                msg = "Successfully Inserted";

            }
            catch(Exception ex)
            {
                msg = "Failed to Insert";
            }
               
            return msg;
        }

        public gettestdata GetInfo()
        {
            gettestdata g = new gettestdata();
            SqlConnection con = new SqlConnection("Data Source=LAPTOP-2LRRTI7L;Initial Catalog=MyTestDB;Persist Security Info=True;User ID=dnct;Password=dnct1234;Pooling=False");
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM UserTab",con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            g.usertab = dt;
            return g;
        }
    }
}

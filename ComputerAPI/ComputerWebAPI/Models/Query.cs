using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;


namespace ComputerWebAPI.Models
{
    public class Query
    {
        public string StMesg;
        public void Conect(string AddQuery)

        {
         
            SqlCommand com = new SqlCommand(AddQuery);
            var RowEfected = com.ExecuteNonQuery();

            

            if (RowEfected > 0)

            {
                StMesg = "Data Saved Successfully";

            }

            else

            {

                StMesg = "ALERT : Data Not Saved !!!";

            }

        }



        public int DataReadrint(string AddQuery, string Path)

        {

            int Num = 0;

            SqlConnection con = new SqlConnection(Path);

            con.Open();

            SqlCommand com = new SqlCommand(AddQuery, con);

            SqlDataReader sdr = com.ExecuteReader();

            while (sdr.Read())

            {

                Num = sdr.GetInt32(0);

            }

            con.Close();

            return Num;

        }



        public string DataReadrstring(string AddQuery)

        {

            string Word = "a";
                   
            SqlCommand com = new SqlCommand(AddQuery);

            SqlDataReader sdr = com.ExecuteReader();

            while (sdr.Read())

            {

                Word = sdr.GetString(0).ToString();

            }

            return Word;

        }
    }
}
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using DiagnosticSystemWeb.Models;
using DiagnosticSystemWeb.Models.View_Model;

namespace DiagnosticSystemWeb.DLL
{
    public class UnPaidBillGatway
    {
        string connectionString = WebConfigurationManager.ConnectionStrings["DiagnosticCenterDB"].ConnectionString;


        public List<UnpaidBillWiseModel> UnpaidBillReport(string fromDate, string toDate)
        {

            SqlConnection con = new SqlConnection(connectionString);
            string query = @"select * from patient WHERE paymentStatus=0 ORDER BY createdAt DESC";
            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            List<UnpaidBillWiseModel> unpaBillViewModels = new List<UnpaidBillWiseModel>();
            while (reader.Read())
            {
                UnpaidBillWiseModel unpaidViewModel = new UnpaidBillWiseModel();

                unpaidViewModel.PatientName = (reader["patientName"].ToString());
                unpaidViewModel.BillNo = reader["billNo"].ToString();
                unpaidViewModel.MobileNo = reader["mobile"].ToString();
                unpaidViewModel.TotalAmount = Convert.ToDecimal(reader["dueAmount"].ToString());
               
                unpaidViewModel.paymentStatus = reader["paymentStatus"].ToString() == "False" ? "Unpaid" : "Paid";

                unpaBillViewModels.Add(unpaidViewModel);
            }
            reader.Close();
            con.Close();
            return unpaBillViewModels;
        }
    }
}
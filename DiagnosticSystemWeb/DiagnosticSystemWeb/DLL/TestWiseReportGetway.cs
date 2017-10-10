using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using DiagnosticSystemWeb.Models.View_Model;

namespace DiagnosticSystemWeb.DLL
{
    public class TestWiseReportGetway
    {
        string connectionString = WebConfigurationManager.ConnectionStrings["DiagnosticCenterDB"].ConnectionString;

        public List<TestWiseReport> GetDateWiseTestReport(string startDate, string endDate)
        {
            SqlConnection connection = new SqlConnection(connectionString);



            string query = @"SELECT ts.testname
								,count(pt.id) as TotalCount,
								 count(pt.id) * sum(DISTINCT ts.fee) as TotalFee FROM testSetup ts 
							LEFT JOIN patientTest pt on pt.testSetupID = ts.id and 
							pt.createdAt BETWEEN '" + startDate + "' AND '" + endDate + "' GROUP BY ts.testname";

            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            List<TestWiseReport> testWiseReportList = new List<TestWiseReport>();
            while (reader.Read())
            {

                TestWiseReport testWiseReport = new TestWiseReport();

                testWiseReport.TestName = reader["testname"].ToString();
                testWiseReport.TotalCount = Convert.ToInt32(reader["TotalCount"].ToString());
                testWiseReport.TotalFee = Convert.ToDecimal(reader["TotalFee"].ToString());

                testWiseReportList.Add(testWiseReport);
            }



            reader.Close();
            connection.Close();

            return testWiseReportList;

        }
    }
}
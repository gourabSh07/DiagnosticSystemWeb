using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using DiagnosticSystemWeb.Models.View_Model;

namespace DiagnosticSystemWeb.DLL
{
    public class TypeWiseReportGetway
    {
        string connectionString = WebConfigurationManager.ConnectionStrings["DiagnosticCenterDB"].ConnectionString;


        public List<TypeWiseTestReport> GetTypeWiseTestReport(string startDate, string endDate)
        {
            SqlConnection connection = new SqlConnection(connectionString);

            string query = @"SELECT TT.NAME AS TypeName, COUNT(PT.id) as TotalCount, ISNULL(SUM(DISTINCT TS.FEE) * COUNT(PT.ID), 0) as TotalFee FROM TESTTYPE TT 
                                LEFT JOIN TESTSETUP TS ON TS.typeID = TT.id
                                LEFT JOIN PATIENTTEST PT ON PT.testSetupID = TS.id
                                AND PT.createdAt BETWEEN '" + startDate + "' AND '" + endDate + "' GROUP BY TT.name";

            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            List<TypeWiseTestReport> testWiseReportList = new List<TypeWiseTestReport>();
            while (reader.Read())
            {
                TypeWiseTestReport typeWiseTestReport = new TypeWiseTestReport();

                typeWiseTestReport.TypeName = reader["TypeName"].ToString();
                typeWiseTestReport.TotalCount = Convert.ToInt32(reader["TotalCount"].ToString());
                typeWiseTestReport.TotalFee = Convert.ToDecimal(reader["TotalFee"].ToString());

                testWiseReportList.Add(typeWiseTestReport);
            }



            reader.Close();
            connection.Close();

            return testWiseReportList;

        }
    }
}
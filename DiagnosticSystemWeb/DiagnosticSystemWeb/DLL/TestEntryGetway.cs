﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using DiagnosticSystemWeb.Models;

namespace DiagnosticSystemWeb.DLL
{
    public class TestEntryGetway
    {
        string connectionString = WebConfigurationManager.ConnectionStrings["DiagnosticCenterDB"].ConnectionString;

        public List<TestSetup> GetAllTestSetup()
        {

            SqlConnection connection = new SqlConnection(connectionString);
            string query = "SELECT * FROM TestSetup";
            SqlCommand command = new SqlCommand(query, connection);

            List<TestSetup> testSetupList = new List<TestSetup>();
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    TestSetup testSetup = new TestSetup();
                    testSetup.Id = int.Parse(reader["id"].ToString());
                    testSetup.TestName = reader["testname"].ToString();
                    testSetup.Fee = Convert.ToDecimal(reader["fee"].ToString());

                    testSetupList.Add(testSetup);
                }
                reader.Close();
            }

            connection.Close();

            return testSetupList;
        }



        public int SavePatient(Patient patient)
        {

            SqlConnection connection = new SqlConnection(connectionString);
            string query = "INSERT INTO Patient (patientName,dateOfBirth,billNo,mobile,paymentStatus,createdAt, dueAmount) VALUES('" + patient.PatientName + "','" + patient.DateOfBirth + "','" + patient.BillNumber + "','" + patient.MobileNo + "', 0 , GETDATE(), '" + patient.DueAmount + "'); SELECT SCOPE_IDENTITY()";


            SqlCommand command = new SqlCommand(query, connection);
            int patientID;
            command.CommandType = CommandType.Text;
            connection.Open();

            patientID = Convert.ToInt32(command.ExecuteScalar());
            connection.Close();

            return patientID;
        }


        public TestSetup GetTestSetup(string testId)
        {
            SqlConnection connection = new SqlConnection(connectionString);

            string query = "SELECT id, testname, fee FROM TestSetup WHERE id='" + testId + "'";

            SqlCommand command = new SqlCommand(query, connection);


            connection.Open();
            TestSetup testSetup = null;
            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {

                    testSetup = new TestSetup();
                    testSetup.Id = int.Parse(reader["id"].ToString());
                    testSetup.TestName = reader["testname"].ToString();
                    testSetup.Fee = Convert.ToDecimal(reader["fee"].ToString());

                }

                reader.Close();
            }

            connection.Close();
            return testSetup;
        }


        public int SavePatientTestInformation(PatientTest patientTest)
        {
            int rowAffected;

            string connectionString = WebConfigurationManager.ConnectionStrings["DiagnosticCenterDB"].ConnectionString;

            SqlConnection connection = new SqlConnection(connectionString);
            string query = "INSERT INTO PatientTest(patientID, testSetupID, createdAt) VALUES(" + patientTest.PatientID + "," + patientTest.TestTypeID + ",GETDATE())";

            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();

            rowAffected = command.ExecuteNonQuery();
            connection.Close();

            return rowAffected;
        }
    }
}
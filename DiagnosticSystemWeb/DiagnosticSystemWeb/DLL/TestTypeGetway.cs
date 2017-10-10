using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using DiagnosticSystemWeb.Models;

namespace DiagnosticSystemWeb.DLL
{
    public class TestTypeGetway
    {
        private string connectionString = WebConfigurationManager.ConnectionStrings["DiagnosticCenterDB"].ConnectionString;

        public int SaveTestType(TestType testType)
        {
            int rowAffected;
            SqlConnection connection = new SqlConnection(connectionString);
            string query = "INSERT INTO TestType(name,createdAt) VALUES('" + testType.TestTypeName + "', GETDATE())";
            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();
            rowAffected = command.ExecuteNonQuery();
            connection.Close();
            return rowAffected;
        }

        public bool ISTestTypeExists(TestType testType)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            string query = "SELECT * FROM TestType WHERE name='" + testType.TestTypeName + "'";
            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                return true;
            }
            connection.Close();

            return false;
        }

        public List<TestType> GetAllTestTypes()
        {
            List<TestType> testTypeList = new List<TestType>();

            SqlConnection connection = new SqlConnection(connectionString);
            string query = "SELECT * FROM TestType ORDER BY name ASC";
            SqlCommand command = new SqlCommand(query, connection);

            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    TestType testType = new TestType();
                    testType.Id = int.Parse(reader["id"].ToString());
                    testType.TestTypeName = reader["name"].ToString();

                    testTypeList.Add(testType);
                }

                reader.Close();
            }

            connection.Close();
            return testTypeList;

        }

    }
}
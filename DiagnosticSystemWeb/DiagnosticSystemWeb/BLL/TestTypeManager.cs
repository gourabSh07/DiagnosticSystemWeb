using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DiagnosticSystemWeb.DLL;
using DiagnosticSystemWeb.Models;

namespace DiagnosticSystemWeb.BLL
{
    public class TestTypeManager
    {
        TestTypeGetway testTypeGetway = new TestTypeGetway();

        public string SaveTestType(TestType testType)
        {

            if (testType.TestTypeName == string.Empty)
            {
                return "empty";
            }

            
            bool isSutdentExist = testTypeGetway.ISTestTypeExists(testType);
            if (isSutdentExist)
            {
                return "exists";
            }


            int rowAffected = testTypeGetway.SaveTestType(testType);
            if (rowAffected > 0)
            {
                return "success";

            }
            else
            {
                return "failed";
            }

        }


        public List<TestType> GetAllTestType()
        {
            return testTypeGetway.GetAllTestTypes();
        }

    }
}
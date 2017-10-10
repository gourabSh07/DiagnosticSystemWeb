using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DiagnosticSystemWeb.DLL;
using DiagnosticSystemWeb.Models;

namespace DiagnosticSystemWeb.BLL
{
    public class TestSetupManager
    {
        TestSetupGateway testSetupGateway = new TestSetupGateway();
        TestTypeGetway testTypeGetway = new TestTypeGetway();

        public List<TestType> GetAllTestType()
        {
            return testTypeGetway.GetAllTestTypes();
        }

        public string SaveTestSetup(TestSetup testSetup)
        {

            if (testSetup.TestName == "" && testSetup.Fee == 0 && testSetup.TestTypeId == 0)
            {
                return "empty";
            }


            bool isTestNameExists = testSetupGateway.IsTestNameExists(testSetup);
            if (isTestNameExists)
            {

                return "exists";
            }


            int rowAffected = testSetupGateway.SaveTestSetup(testSetup);
            if (rowAffected > 0)
            {
                return "success";

            }
            else
            {
                return "failed";
            }

        }

        public List<TestSetupViewModel> GetAllTestSetup()
        {
            return testSetupGateway.GetAllTestSetup();
        }

    }
}

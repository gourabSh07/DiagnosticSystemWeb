using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DiagnosticSystemWeb.DLL;
using DiagnosticSystemWeb.Models;

namespace DiagnosticSystemWeb.BLL
{
    public class TestEntryManager
    {
        TestEntryGetway testEntryGetway = new TestEntryGetway();

        public List<TestSetup> GetAllTestSetup()
        {
            return testEntryGetway.GetAllTestSetup();
        }

        public int SavePatient(Patient patient)
        {

            return testEntryGetway.SavePatient(patient);
        }


        public TestSetup GetTestSetup(string testId)
        {
            return testEntryGetway.GetTestSetup(testId);
        }


        public string SavePatientInformation(PatientTest patientTest)
        {

            int rowAffected = testEntryGetway.SavePatientTestInformation(patientTest);
            if (rowAffected > 0)
            {
                return "success";
            }
            else
            {
                return "failed";
            }
        }

    }
}
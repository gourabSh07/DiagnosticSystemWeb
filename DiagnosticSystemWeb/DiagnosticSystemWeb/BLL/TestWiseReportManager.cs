using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DiagnosticSystemWeb.DLL;
using DiagnosticSystemWeb.Models.View_Model;

namespace DiagnosticSystemWeb.BLL
{
    public class TestWiseReportManager
    {
        TestWiseReportGetway tesWiseReportGetway = new TestWiseReportGetway();
        public List<TestWiseReport> GetAllTypeWiseReport(string fromDate, string toDate)
        {

            return tesWiseReportGetway.GetDateWiseTestReport(fromDate, toDate);
        }
    }
}
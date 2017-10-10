using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DiagnosticSystemWeb.DLL;
using DiagnosticSystemWeb.Models;
using DiagnosticSystemWeb.Models.View_Model;

namespace DiagnosticSystemWeb.BLL
{
    public class UnpaidBillWiseManager
    {
        UnPaidBillGatway unPaidBillGatway = new UnPaidBillGatway();

        public List<UnpaidBillWiseModel> UnpaidBillReport(string fromDate, string toDate)
        {
            return unPaidBillGatway.UnpaidBillReport(fromDate, toDate);
        }
    }
}
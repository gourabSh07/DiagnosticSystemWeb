using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DiagnosticSystemWeb.DLL;
using DiagnosticSystemWeb.Models;

namespace DiagnosticSystemWeb.BLL
{
    public class PaymentManager
    {
        PaymentGetway paymentGateway = new PaymentGetway();

        public TestEntry SearchByBill(string mobile)
        {
            return paymentGateway.SearchByBill(mobile);
        }

        public string UpdatePayment(string billNo, decimal dueAmount)
        {
            int rowAffected = paymentGateway.UpdatePaymentWithStatus(billNo, dueAmount, 0);
            if (rowAffected > 0)
            {
                return "success";
            }
            else
            {
                return "failed";
            }
        }

        public bool IsExistMobileNo(string mobileNo)
        {
            if (SearchByBill(mobileNo) != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }



        public string IsBillNoExists(string billNo)
        {
            if (paymentGateway.IsBillNoExists(billNo))
            {
                return "success";
            }
            else
            {
                return "failed";
            }
        }


        public Patient SearchPatientInfoByBillNo(string billNo)
        {

            return paymentGateway.GetPatientInfoUsingBillNo(billNo);
        }

        public string UpdatePaymentWithStatus(string billNo, decimal paidAmount, int status)
        {
            int rowAffected = paymentGateway.UpdatePaymentWithStatus(billNo, paidAmount, status);

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
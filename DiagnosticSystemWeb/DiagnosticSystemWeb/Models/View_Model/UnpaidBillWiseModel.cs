using System.Collections;

namespace DiagnosticSystemWeb.Models.View_Model
{
    public class UnpaidBillWiseModel
    {
        public string PatientName { get; set; }
        public string BillNo { get; set; }
        public string MobileNo { get; set; }
        public decimal TotalAmount { get; set; }
        public string paymentStatus { get; set; }
    }
}
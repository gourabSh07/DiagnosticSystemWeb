using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DiagnosticSystemWeb.BLL;
using DiagnosticSystemWeb.Models;

namespace DiagnosticSystemWeb.view
{
    public partial class PaymentUI_aspx : System.Web.UI.Page
    {


        private decimal totalAmount;
        private decimal dueAmount;
        private PaymentManager paymentManager = new PaymentManager();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (ViewState["success"] == null)
            {
                paymentButton.Enabled = false;
                amountTextBox.Enabled = false;
            }
            else
            {
                paymentButton.Enabled = true;
                amountTextBox.Enabled = true;
            }


        }

        protected void searchButton_OnClick(object sender, EventArgs e)
        {
            if (BillNoTextBox.Text == "")
            {

            }

            string billNo = BillNoTextBox.Text;
            string message = paymentManager.IsBillNoExists(billNo);

            if (message == "success")
            {
                ViewState["success"] = true;
                DisplayInfoMessage("Found Customer Information.", Color.Blue);
                ShowPaymentInformation(billNo);
            }
            else if (message == "failed")
            {

                InfoMessageLabel.Visible = true;
                InfoMessageLabel.Text = "BillNo IS Not Found.";
                InfoMessageLabel.BackColor = Color.DarkRed;
            }


        }
        private void ShowPaymentInformation(string billNo)
        {
            TestEntry testEntry = paymentManager.SearchByBill(billNo);
            Patient patient = paymentManager.SearchPatientInfoByBillNo(billNo);

            totalAmount = Convert.ToDecimal(testEntry.TotalAmount);
            dueAmount = Convert.ToDecimal(patient.DueAmount.ToString());

            billDateLabel.Text = patient.DueDate.ToString();
            totalFeeLabel.Text = totalAmount + " Taka";
            dueAmountLabel.Text = dueAmount + " Taka";
            paidAmountLabel.Text = (totalAmount - dueAmount).ToString() + " Taka";

            ViewState["DueAmount"] = dueAmount;
            ViewState["TotalAmount"] = totalAmount;
            ViewState["billNo"] = billNo;


            paymentButton.Enabled = true;
            amountTextBox.Enabled = true;
        }

        protected void paymentButton_OnClick(object sender, EventArgs e)
        {
            decimal paidAmount;
            if (amountTextBox.Text == "")
            {
                DisplayInfoMessage("Empty Amount.", Color.DarkRed);
                return;
            }


            string billNo = (string)ViewState["billNo"];
            try
            {
                paidAmount = Convert.ToDecimal(amountTextBox.Text);
            }
            catch (Exception)
            {
                DisplayInfoMessage("Please Enter A Valid Amount", Color.DarkRed);
                amountTextBox.Text = "";
                return;
            }


            dueAmount = (decimal)ViewState["DueAmount"];

            if (dueAmount == 0 && paidAmount > dueAmount)
            {
                DisplayInfoMessage("Already Paid ", Color.DarkRed);
                return;
            }

            if (paidAmount > dueAmount)
            {
                DisplayInfoMessage("Cannot Proced!! Payment Amount Greater Than Due Amount.", Color.DarkRed);

                return;
            }

            if (paidAmount == dueAmount)
            {

                decimal newDueAmount = dueAmount - paidAmount;
                string message = paymentManager.UpdatePaymentWithStatus(billNo, newDueAmount, 1);
                if (message == "success")
                {

                    ViewState["success"] = true;
                    DisplayInfoMessage("Fully Paid!! And Updated customer information.", Color.Blue);

                    ShowPaymentInformation(billNo);
                }

                return;
            }


        }
        private void DisplayInfoMessage(string text, Color color)
        {

            InfoMessageLabel.Text = text;
            InfoMessageLabel.Visible = true;
            InfoMessageLabel.BackColor = color;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using iTextSharp.text;
using iTextSharp.text.pdf;

using System.Web.UI;
using System.Web.UI.WebControls;
using DiagnosticSystemWeb.BLL;
using DiagnosticSystemWeb.Models;



namespace DiagnosticSystemWeb.view
{
    public partial class TestRequestEntryUI : System.Web.UI.Page
    {
        private string patientName;
        private string mobileNo;
        private string billNo;
        private int patientID;
        private decimal dueAmount;
        private int serialNo = 0;
        private decimal total = 0;
        TestEntryManager testEntryManager = new TestEntryManager();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                LoaddAllTestSetup();
            }

            CreateGrid();

        }
        private void CreateGrid()
        {

            if (!IsPostBack)
            {
                DataTable dataTable = new DataTable();
                dataTable.Columns.AddRange(new DataColumn[3]
                {
                   new DataColumn("ID"),
                   new DataColumn("TEST"),
                   new DataColumn("FEE")
                });

                ViewState["TestEntry"] = dataTable;
                this.BindToGridView();

            }

        }


        private void BindToGridView()
        {
            testEntryGridView.DataSource = (DataTable)ViewState["TestEntry"];
            testEntryGridView.DataBind();

        }
        private void LoaddAllTestSetup()
        {
            List<TestSetup> allTestSetupList = testEntryManager.GetAllTestSetup();

            TestNameDropDownList.DataSource = allTestSetupList;
            TestNameDropDownList.DataTextField = "TestName";
            TestNameDropDownList.DataValueField = "Id";
            TestNameDropDownList.DataBind();
            TestNameDropDownList.Items.Insert(0, "---SELECT---");

        }
        protected void testEntryGridView_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[0].Text = (serialNo += 1).ToString();
                e.Row.Cells[1].Text = DataBinder.Eval(e.Row.DataItem, "Test").ToString();
                e.Row.Cells[2].Text = DataBinder.Eval(e.Row.DataItem, "Fee").ToString();

                total += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "FEE"));

            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[0].Text = "";
                e.Row.Cells[1].Text = "Total Amount: ";
                e.Row.Cells[2].Text = total.ToString() + " TAKA";
                e.Row.Cells[3].Text = string.Empty;

            }

            ViewState["totalAmount"] = total;
        }



        protected void saveButton_OnClick(object sender, EventArgs e)
        {
            DisplayInfoMessage("Successfully Test Entry Created", Color.Blue);
            SavePatient();
            SavePatientTestInfo();
            ClearAll();
            CreatePdf();

        }

        private void SavePatientTestInfo()
        {

            foreach (GridViewRow row1 in testEntryGridView.Rows)
            {
                PatientTest patientTest = new PatientTest();
                patientTest.TestTypeID = Convert.ToInt32((row1.FindControl("testTypeIdLabel") as Label).Text);
                patientTest.PatientID = patientID;
                testEntryManager.SavePatientInformation(patientTest);



            }
        }


        protected void addButton_OnClick(object sender, EventArgs e)
        {
            if (patientNameTextBox.Text == string.Empty
              || dateOfBirthTextBox.Text == string.Empty
              || mobileNoTextBox.Text == string.Empty
              || TestNameDropDownList.SelectedIndex == 0)
            {
                DisplayInfoMessage("All Fields are required.", Color.DarkRed);
                return;
            }
            else
            {
                DisplayInfoMessage("Success! Successfully Added.", Color.Blue);
            }




            DataTable dataTable = (DataTable)ViewState["TestEntry"];
            dataTable.Rows.Add(testSetupIDHiddenrField.Value, TestNameDropDownList.SelectedItem, feeTextBox.Text.Trim());

            ViewState["TestEntry"] = dataTable;
            this.BindToGridView();



            TestNameDropDownList.SelectedIndex = 0;
            feeTextBox.Text = string.Empty;
            testSetupIDHiddenrField.Value = string.Empty;

        }
        private void DisplayInfoMessage(string text, Color color)
        {

            infoMessageLabel.Text = text;
            infoMessageLabel.Visible = true;
            infoMessageLabel.BackColor = color;
        }

        private void CreatePdf()
        {

            int columnsCount = testEntryGridView.HeaderRow.Cells.Count ;

            PdfPTable pdfTable = new PdfPTable(columnsCount);


            foreach (TableCell gridViewHeaderCell in testEntryGridView.HeaderRow.Cells)
            {


                iTextSharp.text.Font font = new iTextSharp.text.Font();
                font.Color = new BaseColor(testEntryGridView.HeaderStyle.ForeColor);
                PdfPCell pdfCell = new PdfPCell(new Phrase(gridViewHeaderCell.Text, font));
                pdfTable.AddCell(pdfCell);


            }



            foreach (GridViewRow gridViewRow in testEntryGridView.Rows)
            {
                if (gridViewRow.RowType == DataControlRowType.DataRow)
                {
                    foreach (TableCell gridViewCell in gridViewRow.Cells)
                    {


                        iTextSharp.text.Font font = new iTextSharp.text.Font();
                        font.Color = new BaseColor(testEntryGridView.RowStyle.ForeColor);
                        PdfPCell pdfCell = new PdfPCell(new Phrase(gridViewCell.Text, font));



                        pdfTable.AddCell(pdfCell);
                    }
                }
            }

            foreach (TableCell gridViewHeaderCell in testEntryGridView.FooterRow.Cells)
            {
                iTextSharp.text.Font font = new iTextSharp.text.Font();

                font.Color = new BaseColor(testEntryGridView.FooterStyle.ForeColor);
                PdfPCell pdfCell = new PdfPCell(new Phrase(gridViewHeaderCell.Text, font));



                pdfTable.AddCell(pdfCell);

            }



            Document pdfDocument = new Document(PageSize.A4, 20f, 10f, 10f, 10f);
            PdfWriter writer = PdfWriter.GetInstance(pdfDocument, Response.OutputStream);

            Patient patient = new Patient();



            string Name = "                Diagnostic Center Bill Management System";
            string moduleName = "                                                                     Patient Bill";
            var headerNameTextFont = FontFactory.GetFont("TIMES_BOLD", 19, Element.ALIGN_CENTER, BaseColor.BLUE);
            var moduleNameTextFont = FontFactory.GetFont("TIMES_BOLD", 12, Element.ALIGN_CENTER, BaseColor.BLUE);
            var dateTextFont = FontFactory.GetFont("TIMES_ROMAN", 10, BaseColor.RED);
            var footerTextFont = FontFactory.GetFont("TIMES_ROMAN", 10, Element.ALIGN_CENTER, BaseColor.RED);
            var patientInfoTextFont = FontFactory.GetFont("TIMES_BOLD", 11, Element.ALIGN_LEFT, BaseColor.BLACK);

            string pdfName = "PatientBill";



            pdfDocument.Open();

            pdfDocument.Add(new Paragraph(" \n"));
            pdfDocument.Add(new Paragraph("" + DateTime.Now.ToString(), dateTextFont));
            pdfDocument.Add(new Paragraph(Environment.NewLine));
            pdfDocument.Add(new Paragraph(Environment.NewLine));
            pdfDocument.Add(new Paragraph(Name, headerNameTextFont));
            pdfDocument.Add(new Paragraph(" \n"));

            pdfDocument.Add(new Paragraph(" \n\n"));
            pdfDocument.Add(new Paragraph("Patient Information : "));
            pdfDocument.Add(new Paragraph(" \n"));
            pdfDocument.Add(new Paragraph("Bill No : " + billNo, patientInfoTextFont));

            pdfDocument.Add(new Paragraph("Name : " + patientName, patientInfoTextFont));
            //pdfDocument.Add(new Paragraph("Date of Birth : " + d, patientInfoTextFont));
            pdfDocument.Add(new Paragraph("Mobile No : " + mobileNo, patientInfoTextFont));
            pdfDocument.Add(new Paragraph("Payment Status : Unpaid"));
            pdfDocument.Add(new Paragraph("Due Amount : " + Convert.ToDecimal(ViewState["totalAmount"]) + " Tk"));


            pdfDocument.Add(new Paragraph(Environment.NewLine));
            pdfDocument.Add(new Paragraph(Environment.NewLine));

            pdfDocument.Add(new Paragraph("\t" + moduleName, moduleNameTextFont));

            pdfDocument.Add(new Paragraph(Environment.NewLine));

            pdfDocument.Add(pdfTable);

            Paragraph footer = new Paragraph("© Copyright Dot NEt Maniac. All Rights Reserved	", footerTextFont);

            footer.Alignment = Element.ALIGN_CENTER;

            PdfPTable footerTbl = new PdfPTable(1);

            footerTbl.TotalWidth = 300;

            footerTbl.HorizontalAlignment = Element.ALIGN_CENTER;

            PdfPCell cell = new PdfPCell(footer);

            cell.Border = 0;
            cell.PaddingLeft = 10;
            footerTbl.AddCell(cell);
            footerTbl.WriteSelectedRows(0, -2, 175, 30, writer.DirectContent);

            pdfDocument.Close();
            Response.ContentType = "application/pdf";
            Response.AppendHeader("content-disposition", "attachment;filename=" + pdfName + ".pdf");

            Response.Write(pdfDocument);
            Response.Flush();
            Response.End();

        }
        private void SavePatient()
        {

            dueAmount = Convert.ToDecimal(total);

            Patient patient = new Patient();
            patient.PatientName = patientNameTextBox.Text;
            patient.DateOfBirth = Convert.ToDateTime(dateOfBirthTextBox.Text);
            patient.MobileNo = mobileNoTextBox.Text;
            patient.DueAmount = Convert.ToDecimal(ViewState["totalAmount"]);

            billNo = patient.AutoGeneratedBillNo();
            patientName = patientNameTextBox.Text;
            mobileNo = mobileNoTextBox.Text;

            patientID = testEntryManager.SavePatient(patient);

        }
        private void ClearAll()
        {
            patientNameTextBox.Text = string.Empty;
            mobileNoTextBox.Text = string.Empty;
            dateOfBirthTextBox.Text = string.Empty;
            TestNameDropDownList.SelectedIndex = 0;
            feeTextBox.Text = string.Empty;

        }

        protected void TestNameDropDownList_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            TestSetup testSetup = testEntryManager.GetTestSetup(TestNameDropDownList.SelectedValue);

            feeTextBox.Text = testSetup.Fee.ToString();
            testSetupIDHiddenrField.Value = testSetup.Id.ToString();
        }
    }
}
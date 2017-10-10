using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DiagnosticSystemWeb.BLL;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace DiagnosticSystemWeb.view
{
    public partial class TestWiseReport : System.Web.UI.Page
    {
        private decimal total = 0;
        private int serialNo = 0;
        TestWiseReportManager testWiseReportManager = new TestWiseReportManager();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadEmptyTestGridView();

            }

            pdfButton.Visible = false;
        }
        private void LoadEmptyTestGridView()
        {
            DataTable dataTable = new DataTable();
            testWiseReportGridView.DataSource = dataTable;
            testWiseReportGridView.DataBind();
        }

        protected void showButton_OnClick(object sender, EventArgs e)
        {
            try
            {
                if (formDateTextBox.Text == string.Empty || toDateTextBox.Text == string.Empty)
                {

                    DisplayInfoMessage("please Select Both Date", Color.Red);
                }
                else
                {
                    InfoMessageLabel.Visible = false;
                    string startDate = formDateTextBox.Text;
                    string endDate = toDateTextBox.Text;

                    LoadTestGridView(startDate, endDate);
                }

            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw;
            }
        }
        private void LoadTestGridView(string startDate, string endDate)
        {


            List<DiagnosticSystemWeb.Models.View_Model.TestWiseReport> testWiseReportList = testWiseReportManager.GetAllTypeWiseReport(startDate, endDate);

            if (testWiseReportList.Count != 0)
            {
                testWiseReportGridView.DataSource = testWiseReportList;
                testWiseReportGridView.DataBind();
                //display pdf button
                pdfButton.Visible = true;
                //total items
            }
            else
            {
                testWiseReportGridView.DataSource = null;
                testWiseReportGridView.DataBind();
                pdfButton.Visible = false;
            }
        }
        private void DisplayInfoMessage(string text, Color color)
        {

            InfoMessageLabel.Text = text;
            InfoMessageLabel.Visible = true;
            InfoMessageLabel.BackColor = color;
        }
        protected void pdfButton_OnClick(object sender, EventArgs e)
        {
            int columnsCount = testWiseReportGridView.HeaderRow.Cells.Count;

            PdfPTable pdfTable = new PdfPTable(columnsCount);

            foreach (TableCell gridViewHeaderCell in testWiseReportGridView.HeaderRow.Cells)
            {
                iTextSharp.text.Font font = new iTextSharp.text.Font();
                font.Color = new BaseColor(testWiseReportGridView.HeaderStyle.ForeColor);

                PdfPCell pdfCell = new PdfPCell(new Phrase(gridViewHeaderCell.Text, font));
                pdfTable.AddCell(pdfCell);
            }

            foreach (GridViewRow gridViewRow in testWiseReportGridView.Rows)
            {
                if (gridViewRow.RowType == DataControlRowType.DataRow)
                {
                    foreach (TableCell gridViewCell in gridViewRow.Cells)
                    {
                        iTextSharp.text.Font font = new iTextSharp.text.Font();
                        font.Color = new BaseColor(testWiseReportGridView.RowStyle.ForeColor);

                        PdfPCell pdfCell = new PdfPCell(new Phrase(gridViewCell.Text, font));
                        pdfTable.AddCell(pdfCell);
                    }
                }
            }

            foreach (TableCell gridViewHeaderCell in testWiseReportGridView.FooterRow.Cells)
            {
                iTextSharp.text.Font font = new iTextSharp.text.Font();

                font.Color = new BaseColor(testWiseReportGridView.FooterStyle.ForeColor);
                PdfPCell pdfCell = new PdfPCell(new Phrase(gridViewHeaderCell.Text, font));
                pdfTable.AddCell(pdfCell);
            }

            Document pdfDocument = new Document(PageSize.A4, 20f, 10f, 10f, 10f);
            PdfWriter writer = PdfWriter.GetInstance(pdfDocument, Response.OutputStream);

            string Name = "                Diagnostic Center Bill Management System";
            string moduleName = "                                                            Test Wise Report";
            var headerNameTextFont = FontFactory.GetFont("TIMES_BOLD", 19, Element.ALIGN_CENTER, BaseColor.BLUE);
            var moduleNameTextFont = FontFactory.GetFont("TIMES_BOLD", 13, Element.ALIGN_CENTER, BaseColor.BLUE);
            var dateTextFont = FontFactory.GetFont("TIMES_ROMAN", 10, BaseColor.RED);
            var footerTextFont = FontFactory.GetFont("TIMES_ROMAN", 10, Element.ALIGN_CENTER, BaseColor.RED);
            string pdfName = "TesteWiseReport";

            pdfDocument.Open();

            pdfDocument.Add(new Paragraph(" \n"));
            pdfDocument.Add(new Paragraph("" + DateTime.Now.ToString(), dateTextFont));
            pdfDocument.Add(new Paragraph(Environment.NewLine));
            pdfDocument.Add(new Paragraph(Environment.NewLine));
            pdfDocument.Add(new Paragraph(Name, headerNameTextFont));
            pdfDocument.Add(new Paragraph(" \n"));
            pdfDocument.Add(new Paragraph("\t" + moduleName, moduleNameTextFont));
            pdfDocument.Add(new Paragraph(" \n\n"));
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

        protected void testWiseReportGridView_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[0].Text = (serialNo += 1).ToString();
                e.Row.Cells[1].Text = DataBinder.Eval(e.Row.DataItem, "TestName").ToString();
                e.Row.Cells[2].Text = DataBinder.Eval(e.Row.DataItem, "TotalCount").ToString();
                e.Row.Cells[3].Text = DataBinder.Eval(e.Row.DataItem, "TotalFee").ToString();

                total = total + Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "TotalFee"));

            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[0].Text = "";
                e.Row.Cells[1].Text = "";
                e.Row.Cells[2].Text = "Total Amount: ";
                e.Row.Cells[3].Text = total.ToString();
            }
        }
    }
}
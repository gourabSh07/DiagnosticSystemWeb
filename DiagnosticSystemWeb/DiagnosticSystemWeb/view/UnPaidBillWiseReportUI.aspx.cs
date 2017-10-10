using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DiagnosticSystemWeb.BLL;
using DiagnosticSystemWeb.Models;
using DiagnosticSystemWeb.Models.View_Model;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
using System.IO;

namespace DiagnosticSystemWeb.view
{
    public partial class UnPaidBillWiseReportUI : System.Web.UI.Page
    {
        private decimal total = 0;
        private int serialNo = 0;
        UnpaidBillWiseManager unpaidBillWiseManager = new UnpaidBillWiseManager();

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
            typeWiseReportGridView.DataSource = dataTable;
            typeWiseReportGridView.DataBind();
        }
        protected void showButton_OnClick(object sender, EventArgs e)
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
        private void LoadTestGridView(string startDate, string endDate)
        {


            List<UnpaidBillWiseModel> testWiseReportList = unpaidBillWiseManager.UnpaidBillReport(startDate, endDate);

            if (testWiseReportList.Count != 0)
            {
                typeWiseReportGridView.DataSource = testWiseReportList;
                typeWiseReportGridView.DataBind();

                pdfButton.Visible = true;

            }
            else
            {
                typeWiseReportGridView.DataSource = null;
                typeWiseReportGridView.DataBind();
                pdfButton.Visible = false;
            }
        }


        protected void typeWiseReportGridView_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[0].Text = (serialNo += 1).ToString();
                e.Row.Cells[1].Text = DataBinder.Eval(e.Row.DataItem, "PatientName").ToString();
                e.Row.Cells[2].Text = DataBinder.Eval(e.Row.DataItem, "BillNo").ToString();
                e.Row.Cells[3].Text = DataBinder.Eval(e.Row.DataItem, "MobileNo").ToString();
                e.Row.Cells[4].Text = DataBinder.Eval(e.Row.DataItem, "paymentStatus").ToString();
                e.Row.Cells[5].Text = DataBinder.Eval(e.Row.DataItem, "TotalAmount").ToString();

                total = total + Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "TotalAmount"));


            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[0].Text = "";
                e.Row.Cells[1].Text = "";
                e.Row.Cells[2].Text = "";
                e.Row.Cells[3].Text = "";
                e.Row.Cells[4].Text = "Total Amount: ";
                e.Row.Cells[5].Text = total.ToString();
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
            int columnsCount = typeWiseReportGridView.HeaderRow.Cells.Count;

            PdfPTable pdfTable = new PdfPTable(columnsCount);

            foreach (TableCell gridViewHeaderCell in typeWiseReportGridView.HeaderRow.Cells)
            {
                iTextSharp.text.Font font = new iTextSharp.text.Font();
                font.Color = new BaseColor(typeWiseReportGridView.HeaderStyle.ForeColor);

                PdfPCell pdfCell = new PdfPCell(new Phrase(gridViewHeaderCell.Text, font));
                pdfTable.AddCell(pdfCell);
            }

            foreach (GridViewRow gridViewRow in typeWiseReportGridView.Rows)
            {
                if (gridViewRow.RowType == DataControlRowType.DataRow)
                {
                    foreach (TableCell gridViewCell in gridViewRow.Cells)
                    {
                        iTextSharp.text.Font font = new iTextSharp.text.Font();
                        font.Color = new BaseColor(typeWiseReportGridView.RowStyle.ForeColor);

                        PdfPCell pdfCell = new PdfPCell(new Phrase(gridViewCell.Text, font));
                        pdfTable.AddCell(pdfCell);
                    }
                }
            }

            foreach (TableCell gridViewHeaderCell in typeWiseReportGridView.FooterRow.Cells)
            {
                iTextSharp.text.Font font = new iTextSharp.text.Font();

                font.Color = new BaseColor(typeWiseReportGridView.FooterStyle.ForeColor);
                PdfPCell pdfCell = new PdfPCell(new Phrase(gridViewHeaderCell.Text, font));
                pdfTable.AddCell(pdfCell);
            }

            Document pdfDocument = new Document(PageSize.A4, 20f, 10f, 10f, 10f);
            PdfWriter writer = PdfWriter.GetInstance(pdfDocument, Response.OutputStream);

            string Name = "                Diagnostic Center Bill Management System";
            string moduleName = "                                                                 Unpaid Bill";
            var headerNameTextFont = FontFactory.GetFont("TIMES_BOLD", 19, Element.ALIGN_CENTER, BaseColor.BLUE);
            var moduleNameTextFont = FontFactory.GetFont("TIMES_BOLD", 13, Element.ALIGN_CENTER, BaseColor.BLUE);
            var dateTextFont = FontFactory.GetFont("TIMES_ROMAN", 10, BaseColor.RED);
            var footerTextFont = FontFactory.GetFont("TIMES_ROMAN", 10, Element.ALIGN_CENTER, BaseColor.RED);
            string pdfName = "Unpaid_BillReport";






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
            

            

            PdfWriter.GetInstance(pdfDocument, Response.OutputStream);


            Response.Write(pdfDocument);
            Response.Flush();
            Response.End();



        }
    }
}
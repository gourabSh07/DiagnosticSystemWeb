<%@ Page Title="" Language="C#" MasterPageFile="~/master.Master" AutoEventWireup="true" CodeBehind="UnPaidBillWiseReportUI.aspx.cs" Inherits="DiagnosticSystemWeb.view.UnPaidBillWiseReportUI" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    <title>UnpaidBillWiseReport</title>
 </asp:Content> 
<asp:Content ID="unPaidBillWiseReportUIContent" ContentPlaceHolderID="MainContent" runat="server">
    <form class="form-horizontal" id="UnPaidBillWiseReportUIForm" runat="server">
        
          <h3 class="title text-center">Unpaid Bill Report</h3>
        <asp:Label ID="InfoMessageLabel" CssClass="infoMessage" runat="server" Visible="false" Text=""></asp:Label>
        <div class="col-lg-8">
            <!-- type setup -->
            <div class="type-setup">
                <div class="form-group">
                    <asp:Label ID="testTypeLabel" runat="server" class="col-sm-3 control-label" Text="From Date"></asp:Label>
                    <div class="col-sm-9">
                        <asp:TextBox ID="formDateTextBox" TextMode="Date" class="form-control" runat="server"></asp:TextBox>
                    </div>
                </div>
                
                <div class="form-group">
                    <asp:Label ID="Label1" runat="server" class="col-sm-3 control-label" Text="To Date"></asp:Label>
                    <div class="col-sm-9">
                        <asp:TextBox ID="toDateTextBox" TextMode="Date" class="form-control" runat="server"></asp:TextBox>
                    </div>
                </div>

                <div class="form-group">
                    <div class="col-sm-offset-3 col-sm-9">
                        <asp:Button ID="showButton" runat="server" class="btn btn-primary right" Text="Show" OnClick="showButton_OnClick" />
                    </div>
                </div>
            </div>
         
        </div>
        
        <div class="row">
            <div class="col-lg-12">
                <asp:GridView 
                    ID="typeWiseReportGridView" 
                    CssClass="table table-bordered custom-table" 
                    HorizontalAlign="Center" 
                    runat="server" 
                    AutoGenerateColumns="False" 
                    EmptyDataText="No data available." 
                    BackColor="White" 
                    BorderColor="#5bc0de" 
                    BorderStyle="None" 
                    BorderWidth="1px" 
                    CellPadding="3" 

                    ShowFooter="True" OnRowDataBound="typeWiseReportGridView_OnRowDataBound">
                    
                        <Columns>
                            <asp:TemplateField HeaderText="SL">
                                <ItemTemplate><%# Container.DataItemIndex + 1 %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="PatientName" HeaderText="Patient Name" />
                        <asp:BoundField DataField="BillNo" HeaderText="Bill No" />
                        <asp:BoundField DataField="MobileNo" HeaderText="Mobile No" />
                        <asp:BoundField DataField="paymentStatus" HeaderText="payment Status" />

                        <asp:BoundField DataField="TotalAmount" HeaderText="Total" />
                        </Columns>
                        <FooterStyle BackColor="#5bc0de" Font-Bold="True" ForeColor="#000000" />
                        <HeaderStyle BackColor="#5bc0de" Font-Bold="True" ForeColor="#000000" />
                        <PagerStyle BackColor="#FFFFCC" ForeColor="#330099" HorizontalAlign="Center" />
                        <RowStyle BackColor="White" ForeColor="#330099" />
                        <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="#663399" />
                        <SortedAscendingCellStyle BackColor="#FEFCEB" />
                        <SortedAscendingHeaderStyle BackColor="#AF0101" />
                        <SortedDescendingCellStyle BackColor="#F6F0C0" />
                        <SortedDescendingHeaderStyle BackColor="#7E0000" />
                    </asp:GridView>

            </div>
        </div>
        
        
        
        <div class="row">
            <div class="col-lg-12">
                <asp:Button ID="pdfButton" runat="server" cssClass="btn btn-primary right" Text="PDF" OnClick="pdfButton_OnClick" />
                    
            </div>
        </div>

        

    </form>
    
</asp:Content>
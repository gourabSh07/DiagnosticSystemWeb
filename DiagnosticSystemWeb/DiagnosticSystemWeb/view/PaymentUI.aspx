<%@ Page Title="" Language="C#" MasterPageFile="~/master.Master" AutoEventWireup="true" CodeBehind="PaymentUI.aspx.cs" Inherits="DiagnosticSystemWeb.view.PaymentUI_aspx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    <title>PaymentUI</title>
 </asp:Content> 
<asp:Content ID="paymentContent" ContentPlaceHolderID="MainContent" runat="server">
   <form class="form-horizontal" id="PaymentUIForm" runat="server">
        <div class="row">
            <h3 class="title text-center">Pay Bill</h3>
            <asp:Label ID="InfoMessageLabel" CssClass="infoMessage" runat="server" Visible="false" Text=""></asp:Label>
            <div class="col-lg-8">
               
                <div class="type-setup">
                    <div class="row">
                        <div class="col-lg-9">
                            <div class="form-group">
                                <asp:Label ID="TestNameLabel" runat="server" CssClass="col-sm-3 control-label" Text="Bill No"></asp:Label>
                                <div class="col-sm-9">
                                    <asp:TextBox ID="BillNoTextBox" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-3">
                            <div class="form-group">
                                <div class="col-sm-offset-3 col-sm-9">
                                    <asp:Button ID="searchButton" runat="server" CssClass="btn btn-primary right" Text="Search" OnClick="searchButton_OnClick" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
              
            </div>
            <div class="col-lg-4"></div>
        </div>
        
        
        <br/>
        <hr/>
        <br/>
        
        
        <div>
            <asp:GridView ID="testEntryGridView" runat="server"
            AutoGenerateColumns = "false" Font-Names = "Arial"
            Font-Size = "11pt" AlternatingRowStyle-BackColor = "#5bc0de" 
            HeaderStyle-BackColor = "blue" AllowPaging ="true"  
            CssClass="table table-bordered tbl-none"
            EmptyDataText="No records has been added.">
                    
            </asp:GridView>
        </div>

        
        
        <div>
            <table class="table table-payment">
                <tbody>
                    <tr>
                        <td><label class="control-label">Bill Date</label></td>
                        <td><asp:Label ID="billDateLabel" runat="server" Text="N/A"></asp:Label></td>
                    </tr>
                    
                    <tr>
                        <td><label class="control-label">Total Fee</label></td>
                        <td><asp:Label ID="totalFeeLabel" runat="server" Text="N/A"></asp:Label></td>
                    </tr>
                    
                    <tr>
                        <td><label class="control-label">Paid Amount</label></td>
                        <td><asp:Label ID="paidAmountLabel" runat="server" Text="N/A"></asp:Label></td>
                    </tr>
                    
                    
                    <tr>
                        <td><label class="control-label">Due Amount</label></td>
                        <td><asp:Label ID="dueAmountLabel" runat="server" Text="N/A"></asp:Label></td>
                    </tr>
                    
                </tbody>
              </table>
        </div>

        
        <div class="row">
            <asp:Label ID="Label1" CssClass="infoMessage" runat="server" Visible="false" Text=""></asp:Label>
            <div class="col-lg-8">
               
                <div class="type-setup">
                    <div class="form-group">
                        <asp:Label ID="Label3" runat="server" CssClass="col-sm-3 control-label" Text="Amount"></asp:Label>
                        <div class="col-sm-9">
                            <asp:TextBox ID="amountTextBox" Enabled="False" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                    </div>
                

                    <div class="form-group">
                        <div class="col-sm-offset-3 col-sm-9">
                            <asp:Button ID="paymentButton" runat="server" CssClass="btn btn-success right" Text="Pay" BackColor="#5bc0de" BorderColor="#5bc0de"  OnClick="paymentButton_OnClick" />
                        </div>
                    </div>
                </div>
             
            </div>
            <div class="col-lg-4"></div>
        </div>

   </form>
     
</asp:Content>
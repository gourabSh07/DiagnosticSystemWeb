<%@ Page Title="" Language="C#" MasterPageFile="~/master.Master" AutoEventWireup="true" CodeBehind="TestSetupUI.aspx.cs" Inherits="DiagnosticSystemWeb.view.TestSetupUI" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    <title>TestSetupUI</title>
 </asp:Content> 
<asp:Content ID="testSetuContent" ContentPlaceHolderID="MainContent" runat="server">
        <form id="TestSetupform" class="form-horizontal" runat="server">
        <h3 class="title text-center">Test Setup</h3>
        <asp:Label ID="InfoMessageLabel" CssClass="infoMessage" runat="server" Visible="false" Text=""></asp:Label>
        <div class="col-lg-8">
            <!-- type setup -->
            <div class="type-setup">
                <div class="form-group">
                    <asp:Label ID="testNameLabel" runat="server" CssClass="col-sm-3 control-label" Text="Test Name"></asp:Label>
                    <div class="col-sm-9">
                        <asp:TextBox ID="testNameTextBox" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                </div>
                
                <div class="form-group">
                    <asp:Label ID="Label1" runat="server" CssClass="col-sm-3 control-label" Text="Test Fee"></asp:Label>
                    <div class="col-sm-9">
                        <asp:TextBox ID="feeTextBox" CssClass="form-control" runat="server"></asp:TextBox>      <b class="money-type">BDT</b>
                    </div>
                </div>

                <div class="form-group">
                    <asp:Label ID="Label22" runat="server" CssClass="col-sm-3 control-label" Text="Test Type"></asp:Label>
                    <div class="col-sm-9">
                        <asp:DropDownList CssClass="form-control" ID="TestTypeDropDownList" runat="server"></asp:DropDownList>
                    </div>
                </div>
                
                <div class="form-group">
                    <div class="col-sm-offset-3 col-sm-9">
                        <asp:Button ID="saveButton" runat="server" CssClass="btn btn-primary right" Text="Save" OnClick="saveButton_Click"/>
                    </div>
                </div>
            </div>
          
        </div>
        
        
        <asp:GridView ID="testSetupGridView" CssClass="table table-bordered custom-table" runat="server" AutoGenerateColumns="False" AllowPaging="True" OnPageIndexChanging="testSetupGridView_OnPageIndexChanging">
            <Columns>
                <asp:TemplateField HeaderText="SL">
                    <ItemTemplate><%# Container.DataItemIndex + 1 %></ItemTemplate>
                </asp:TemplateField>
                
                <asp:TemplateField HeaderText="Test Name">
                    <ItemTemplate><%# Eval("TestName") %></ItemTemplate>
                </asp:TemplateField>
                
                <asp:TemplateField HeaderText="Fee">
                    <ItemTemplate><%# Eval("Fee") %></ItemTemplate>
                </asp:TemplateField>
                
                <asp:TemplateField HeaderText="Type">
                    <ItemTemplate><%# Eval("Name") %></ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        
    </form>
      
</asp:Content>
<%@ Page Title="" Language="C#" MasterPageFile="~/master.Master" AutoEventWireup="true" CodeBehind="TestTypeSetupUI.aspx.cs" Inherits="DiagnosticSystemWeb.view.TestTypeSetupUI" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    <title>TestTypeSetupUI</title>
 </asp:Content> 
<asp:Content ID="testTypeSetupContent" ContentPlaceHolderID="MainContent" runat="server">
    <form class="form-horizontal" id="TestTypeSetupUIForm" runat="server">
         
        <h3 class="title text-center">Test Type Setup</h3>
        <asp:Label ID="InfoMessageLabel" CssClass="infoMessage" runat="server" Visible="false" Text=""></asp:Label>
        <div class="col-lg-8">
            
            <div class="type-setup">
                <div class="form-group">
                    <asp:Label ID="TestTypeLabel" runat="server" class="col-sm-3 control-label" Text="Type Name"></asp:Label>
                    <div class="col-sm-9">
                        <asp:TextBox ID="testTypeTextBox" name="TestTypeTextBox" class="form-control" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group">
                    <div class="col-sm-offset-3 col-sm-9">
                        <asp:Button ID="saveButton" runat="server"  type="submit" class="btn btn-primary right" Text="Save" OnClick="saveButton_Click" />
                    </div>
                </div>
            </div> 
         
        </div>
        
        
        <asp:GridView ID="testSetupGridView" CssClass="table table-bordered custom-table" runat="server" AutoGenerateColumns="False" AllowPaging="True">
            <Columns>
                <asp:TemplateField HeaderText="SL NO">
                    <ItemTemplate><%# Container.DataItemIndex + 1 %></ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Type Name">
                    <ItemTemplate><%# Eval("TestTypeName") %></ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </form>
  
   
   
     
</asp:Content>
<%@ Page Title="" Language="C#" MasterPageFile="~/master.Master" AutoEventWireup="true" CodeBehind="TestRequestEntryUI.aspx.cs" Inherits="DiagnosticSystemWeb.view.TestRequestEntryUI" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    <title>TestRequestEntryUI</title>
 </asp:Content> 
<asp:Content ID="testRequestEntryUIContent" ContentPlaceHolderID="MainContent" runat="server">
    <form class="form-horizontal" id="TestRequestEntryUIForm" runat="server">
       
        <h3 class="title text-center">Test Request Entry</h3>
        <asp:Label ID="infoMessageLabel" CssClass="infoMessage" runat="server" Visible="false" Text=""></asp:Label>
        <div class="col-lg-9">
            <!-- type setup -->
            <div class="type-setup">
                <div class="form-group">
                    <asp:Label ID="testNameLabel" runat="server" CssClass="col-sm-5 control-label" Text="Name of the Patient"></asp:Label>
                    <div class="col-sm-7">
                        <asp:TextBox ID="patientNameTextBox" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                </div>
                
                <div class="form-group">
                    <asp:Label ID="dOB" runat="server" CssClass="col-sm-5 control-label" Text="Date Of Birth"></asp:Label>
                    <div class="col-sm-7">
                        <asp:TextBox ID="dateOfBirthTextBox" TextMode="Date" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                </div>
                
                <div class="form-group">
                    <asp:Label ID="mOB" runat="server" CssClass="col-sm-5 control-label" Text="Mobile No"></asp:Label>
                    <div class="col-sm-7">
                        <asp:TextBox ID="mobileNoTextBox" CssClass="form-control" MaxLength="11" runat="server"></asp:TextBox>
                    </div>
                </div>
                
                <div class="form-group">
                    <asp:Label ID="Label3" runat="server" CssClass="col-sm-5 control-label" Text="Select Test"></asp:Label>
                    <div class="col-sm-7">
                        <asp:DropDownList ID="TestNameDropDownList" CssClass="form-control" AutoPostBack="True" runat="server" OnSelectedIndexChanged="TestNameDropDownList_OnSelectedIndexChanged"></asp:DropDownList>
                    </div>
                </div>

                
                <div class="form-group">
                    <asp:Label ID="Label4" runat="server" CssClass="col-sm-7 control-label bold-label" Text="Fee"></asp:Label>
                    <div class="col-sm-5">
                        <asp:TextBox ID="feeTextBox" Enabled="False" CssClass="form-control" runat="server"></asp:TextBox>
                        <asp:HiddenField ID="testSetupIDHiddenrField" runat="server" />
                    </div>
                </div>


                <div class="form-group">
                    <div class="col-sm-offset-3 col-sm-9">
                        <asp:Button ID="addButton" runat="server" CssClass="btn btn-primary right" Text="Add" OnClick="addButton_OnClick" />
                    </div>
                </div>
            </div>
       
        </div>
        
        
        <div>
            <asp:GridView 
                    ID="testEntryGridView" CssClass="table table-bordered custom-table" HorizontalAlign="Center" runat="server" AutoGenerateColumns="False" EmptyDataText="No data available." BackColor="White"  BorderColor="#CC9966"  BorderStyle="None" BorderWidth="1px" CellPadding="3"  ShowFooter="True" OnRowDataBound="testEntryGridView_OnRowDataBound">
                    
                    <Columns>
                        <asp:TemplateField HeaderText="SL">
                            <ItemTemplate><%# Container.DataItemIndex + 1 %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="Test Name" DataField="Test" />
                        <asp:BoundField HeaderText="Total Test" DataField="Fee" />
                        <asp:TemplateField Visible="False" ValidateRequestMode="Enabled" HeaderText=" ">
                            
                            <ItemTemplate>
                               
                                <asp:Label ID="testTypeIdLabel" runat="server" Visible="False" Text='<%# Eval("Id") %>'></asp:Label>
                            </ItemTemplate>
                           
                        </asp:TemplateField>
                    </Columns>
                    <FooterStyle BackColor="#5bc0de" Font-Bold="True" ForeColor="#663399" HorizontalAlign="Center" />
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

        
        


        <div class="form-group">
            <div class="col-sm-offset-3 col-sm-9">
                <asp:Button ID="saveButton" runat="server" CssClass="btn btn-primary right" Text="Save" OnClick="saveButton_OnClick" />
            </div>
        </div>
        
    

   </form>
     
</asp:Content>
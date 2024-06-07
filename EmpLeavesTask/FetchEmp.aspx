<%@ Page Title="" Language="C#" MasterPageFile="~/hr.Master" AutoEventWireup="true" CodeBehind="FetchEmp.aspx.cs" Inherits="EmpLeavesTask.FetchEmp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Fetch Employee by ID
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="card mt-4 w-50">
        <div class="card-header">
            <h3 class="text-center">Fetch Employee by ID</h3>
        </div>
        <div class="card-body">
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <asp:Literal ID="ltFetchEmployeeMessage" runat="server" EnableViewState="False"></asp:Literal>
                    <asp:Panel ID="PanelFetchEmployee" runat="server" CssClass="form-group">
                        <label for="employeeId">Employee ID</label>
                        <asp:TextBox ID="txtEmployeeId" runat="server" CssClass="form-control" placeholder="Enter employee ID"></asp:TextBox>
                    </asp:Panel>
                    <asp:Button ID="btnFetchEmployee" runat="server" Text="Fetch Employee" CssClass="btn btn-primary btn-block" OnClick="btnFetchEmployee_Click" />
                    <div id="employeeDetails" class="mt-4">
                        <asp:Literal ID="ltEmployeeDetails" runat="server" EnableViewState="False"></asp:Literal>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/hr.Master" AutoEventWireup="true" CodeBehind="HrFetchEmp.aspx.cs" Inherits="EmpLeavesTask.FetchEmp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Fetch Employee by ID
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="d-flex justify-content-center">
        <div class="card mt-4 w-50">
            <div class="card-header">
                <h3 class="text-center">Fetch Employee</h3>
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
                            <asp:Panel ID="PanelActionButtons" runat="server" Visible="False" CssClass="mt-4">
                                <asp:Button ID="btnGenerateOffer" runat="server" Text="Generate Offer" CssClass="btn btn-success"  OnClick="btnGenerateOffer_Click"/>
                            </asp:Panel>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

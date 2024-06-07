﻿<%@ Page Title="" Language="C#" MasterPageFile="~/hr.Master" AutoEventWireup="true" CodeBehind="addemp.aspx.cs" Inherits="EmpLeavesTask.addemp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Add Employee
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="addEmployee" class="mt-4">
        <div class="card">
            <div class="card-header">
                <h3 class="text-center">Add Employee</h3>
            </div>
            <div class="card-body">
                <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <asp:Literal ID="ltAddEmployeeMessage" runat="server" EnableViewState="False"></asp:Literal>
                        <asp:Panel ID="PanelAddEmployee" runat="server" CssClass="form-group">
                            <label for="employeeName">Employee Name</label>
                            <asp:TextBox ID="txtEmployeeName" runat="server" CssClass="form-control" placeholder="Enter employee name"></asp:TextBox>
                        </asp:Panel>
                        <asp:Panel ID="PanelEmployeeEmail" runat="server" CssClass="form-group">
                            <label for="employeeEmail">Email</label>
                            <asp:TextBox ID="txtEmployeeEmail" runat="server" CssClass="form-control" placeholder="Enter email"></asp:TextBox>
                        </asp:Panel>
                        <asp:Panel ID="PanelEmployeeContact" runat="server" CssClass="form-group">
                            <label for="employeeContact">Contact</label>
                            <asp:TextBox ID="txtEmployeeContact" runat="server" CssClass="form-control" placeholder="Enter contact number"></asp:TextBox>
                        </asp:Panel>
                        <asp:Panel ID="PanelDOJ" runat="server" CssClass="form-group">
                            <label for="doj">Date of Joining</label>
                            <asp:TextBox ID="txtDOJ" runat="server" CssClass="form-control" placeholder="Enter date of joining"></asp:TextBox>
                        </asp:Panel>
                        <asp:Button ID="btnAddEmployee" runat="server" Text="Add Employee" CssClass="btn btn-primary btn-block" OnClick="btnAddEmployee_Click" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
</asp:Content>

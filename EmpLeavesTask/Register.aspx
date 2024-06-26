﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="EmpLeavesTask.Register" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Register Employee</title>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.16.0/umd/popper.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js"></script>
    <<%--script>
        $(document).ready(function () {
            $('#btnAddEmployee').click(function (e) {
                e.preventDefault();
                $('#otpModal').modal('show');
            });
        });
    </script>--%>
</head>
<body>
    <form id="form1" runat="server">
        <div class="card" style="margin: 0 auto; max-width: 400px; padding-top: 50px;">
            <div class="card-header">
                <h3 class="text-center">Register Employee</h3>
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
                        <asp:Panel ID="PanelEmpPassword" runat="server" CssClass="form-group">
                            <label for="password">Password</label>
                            <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="form-control" placeholder="Enter password"></asp:TextBox>
                        </asp:Panel>
                        <asp:Panel ID="PanelEmployeeContact" runat="server" CssClass="form-group">
                            <label for="employeeContact">Contact</label>
                            <asp:TextBox ID="txtEmployeeContact" runat="server" CssClass="form-control" placeholder="Enter contact number"></asp:TextBox>
                        </asp:Panel>
                        <asp:Panel ID="PanelDOJ" runat="server" CssClass="form-group">
                            <label for="doj">Date of Joining</label>
                            <asp:TextBox ID="txtDOJ" runat="server" CssClass="form-control" placeholder="Enter date of joining" TextMode="Date"></asp:TextBox>
                        </asp:Panel>
                        <asp:Panel ID="PanelRole" runat="server" CssClass="form-group">
                            <label for="role">Role</label>
                            <asp:DropDownList ID="ddlRole" runat="server" CssClass="form-control">
                                <asp:ListItem Text="Hr" Value="hr"></asp:ListItem>
                                <asp:ListItem Text="Trainer" Value="trainer"></asp:ListItem>
                                <asp:ListItem Text="Trainee" Value="trainee"></asp:ListItem>
                            </asp:DropDownList>
                        </asp:Panel>
                        <asp:Button ID="btnAddEmployee" runat="server" Text="Add Employee" CssClass="btn btn-primary btn-block" OnClick="btnAddEmployee_Click" />
                        <div class="mt-3">
                            New user?
                            <asp:HyperLink ID="lnkRegister" runat="server" NavigateUrl="Login.aspx" Text="Login here" />
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>

        <!-- OTP Modal -->
        <div class="modal fade" id="otpModal" tabindex="-1" role="dialog" aria-labelledby="otpModalLabel" aria-hidden="true">
            <div class="modal-dialog" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="otpModalLabel">OTP Verification</h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <div class="form-group">
                            <label for="txtOTP">Enter OTP</label>
                            <asp:TextBox ID="txtOTP" runat="server" CssClass="form-control" placeholder="Enter OTP"></asp:TextBox>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                        <asp:Button ID="btnVerifyOTP" runat="server" Text="Verify OTP" CssClass="btn btn-primary" OnClick="btnVerifyOTP_Click" />
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>

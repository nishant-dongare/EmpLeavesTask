<%@ Page Title="" Language="C#" MasterPageFile="~/Hr.Master" AutoEventWireup="true" CodeBehind="HrPayslip.aspx.cs" Inherits="EmpLeavesTask.HrPayslip" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Employee Details
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <link href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" rel="stylesheet" />
    <style>
        .form-container {
            max-width: 600px;
        }

        .card {
            margin-top: 50px;
        }

        .card-header, .card-footer {
            background-color: #f8f9fa;
        }

        .form-control[readonly] {
            background-color: #e9ecef;
            opacity: 1;
        }
    </style>
    <asp:Panel ID="Panel1" runat="server">
        <div class="form-container" style="margin:auto">
            <div class="card">
                <div class="card-header">
                    <h3 class="card-title">Employee Details</h3>
                </div>
                <div class="card-body">
                    <div class="form-group">
                        <label for="empNo">Enter Emp No:</label>
                        <asp:TextBox ID="txtEmpNo" runat="server" CssClass="form-control" placeholder="Employee No"></asp:TextBox>
                        <asp:Button ID="btnGetDetails" runat="server" Text="Enter to get details" CssClass="btn btn-secondary mt-2" OnClick="btnGetDetails_Click" />
                    </div>
                    <div class="form-group">
                        <label for="empName">Employee Name:</label>
                        <asp:TextBox ID="txtEmpName" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <label for="bankName">Bank Name:</label>
                        <asp:TextBox ID="txtBankName" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <label for="contactNo">Contact No:</label>
                        <asp:TextBox ID="txtContactNo" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <label for="bankAccountNo">Bank Account No:</label>
                        <asp:TextBox ID="txtBankAccountNo" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <label for="email">Email:</label>
                        <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <label for="designation">Designation:</label>
                        <asp:TextBox ID="txtDesignation" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <label for="doj">Date Of Joining:</label>
                        <asp:TextBox ID="txtDOJ" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <label for="leavesDays">Leaves days(In Month):</label>
                        <asp:TextBox ID="txtLeavesDays" runat="server" CssClass="form-control" ReadOnly="true" Text="2 in Working days"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <label for="monthlySalary">Monthly Salary (₹):</label>
                        <asp:TextBox ID="txtMonthlySalary" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <label for="totalWorkingDays">Total Working Days in Month:</label>
                        <asp:TextBox ID="txtTotalWorkingDays" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <label for="leavesTaken">Leaves Taken:</label>
                        <asp:TextBox ID="txtLeavesTaken" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <label for="leavesTaken">Balance Leaves : </label>
                        <asp:TextBox ID="txtBalanceLeaves" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                    </div>
                    <asp:Button ID="btnCalculate" runat="server" Text="Calculate" CssClass="btn btn-primary" OnClick="btnCalculate_Click" />
                </div>
                <div class="card-footer">
                    <h5>Calculated Salary (₹):
                        <asp:Label ID="lblCalculatedSalary" runat="server" Text=""></asp:Label></h5>
                    <asp:Button ID="btnGeneratePDF" runat="server" Text="Generate to PDF" CssClass="btn btn-secondary" OnClick="Download_Invoice" />
                </div>
            </div>
        </div>
    </asp:Panel>
</asp:Content>

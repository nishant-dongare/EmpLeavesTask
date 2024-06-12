<%@ Page Title="" Language="C#" MasterPageFile="~/Employee.Master" AutoEventWireup="true" CodeBehind="EmpLeaveApply.aspx.cs" Inherits="EmpLeavesTask.EmpLeaveApply" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-4">
        <asp:Literal ID="ltMessage" runat="server" EnableViewState="False"></asp:Literal>

        <div class="mb-3">
            <label for="txtreason" class="form-label">Reason</label>
            <asp:TextBox ID="txtreason" runat="server" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
        </div>
        <div class="mb-3">
            <label for="tb_fromdate" class="form-label">From</label>
            <asp:TextBox ID="tb_fromdate" runat="server" TextMode="Date" CssClass="form-control"></asp:TextBox>
        </div>
        <div class="mb-3">
            <label for="tb_todate" class="form-label">To</label>
            <asp:TextBox ID="tb_todate" runat="server" TextMode="Date" CssClass="form-control"></asp:TextBox>
        </div>
        <div class="mb-3">
            <asp:Label ID="Label5" runat="server" CssClass="form-text"></asp:Label>
        </div><div class="mb-3">
            <asp:Label ID="totalleaves_lbl" runat="server" CssClass="form-text"></asp:Label>
        </div>
        <div class="mb-3">
            <asp:Button ID="Button1" runat="server" OnClick="OnSubmit" Text="Submit" CssClass="btn btn-primary" />
        </div>
    </div>


</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/Employee.Master" AutoEventWireup="true" CodeBehind="TicketSolution.aspx.cs" Inherits="EmpLeavesTask.Ticket.TicketSolution" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-5">
        <h2 class="mb-4">Enter Solution for Ticket</h2>
            <div class="form-group">
                <label for="ticketId">Ticket ID</label>
                <asp:TextBox ID="ticketIdTb" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="ticket">Ticket</label>
                <asp:TextBox ID="ticketTb" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="solution">Solution</label>
                <asp:TextBox ID="solutionTb" runat="server" TextMode="MultiLine" CssClass="form-control" Rows="5"></asp:TextBox>
            </div>
            <asp:Button ID="submitSolution" runat="server" CssClass="btn btn-primary" Text="Submit Solution" OnClick="SubmitSolution_Click" />
    </div>
</asp:Content>

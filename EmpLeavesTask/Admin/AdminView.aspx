<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="AdminView.aspx.cs" Inherits="EmpLeavesTask.Admin.AdminView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-4">
        <asp:Literal ID="ltMessage" runat="server" EnableViewState="False"></asp:Literal>

        <div class="mb-3">
            <label for="tb_fromdate" class="form-label">From</label>
            <asp:TextBox ID="tb_fromdate" runat="server" TextMode="Date" CssClass="form-control"></asp:TextBox>
        </div>
        <div class="mb-3">
            <label for="tb_todate" class="form-label">To</label>
            <asp:TextBox ID="tb_todate" runat="server" TextMode="Date" CssClass="form-control"></asp:TextBox>
        </div>
        <div class="mb-3">
            <asp:Button ID="Button1" runat="server" OnClick="OnSubmit" Text="Submit" CssClass="btn btn-primary" />
        </div>
    </div>

    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="solution_id" DataSourceID="Solution">
        <Columns>
            <asp:BoundField DataField="solution_id" HeaderText="solution_id" InsertVisible="False" ReadOnly="True" SortExpression="solution_id" />
            <asp:BoundField DataField="ticket_id" HeaderText="ticket_id" SortExpression="ticket_id" />
            <asp:BoundField DataField="raised_to" HeaderText="raised_to" SortExpression="raised_to" />
            <asp:BoundField DataField="raised_by" HeaderText="raised_by" SortExpression="raised_by" />
            <asp:BoundField DataField="ticket" HeaderText="ticket" SortExpression="ticket" />
            <asp:BoundField DataField="ticket_solution" HeaderText="ticket_solution" SortExpression="ticket_solution" />
            <asp:BoundField DataField="ticket_date" HeaderText="ticket_date" SortExpression="ticket_date" />
            <asp:BoundField DataField="solution_date" HeaderText="solution_date" SortExpression="solution_date" />
            <asp:BoundField DataField="attachment" HeaderText="attachment" SortExpression="attachment" />
        </Columns>
    </asp:GridView>

    <asp:SqlDataSource ID="Solution" runat="server" ConnectionString="<%$ ConnectionStrings:dbconn %>" SelectCommand="GetAllSolutions" SelectCommandType="StoredProcedure"></asp:SqlDataSource>

</asp:Content>

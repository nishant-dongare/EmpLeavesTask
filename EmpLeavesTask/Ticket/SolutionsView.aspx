<%@ Page Title="" Language="C#" MasterPageFile="~/Employee.Master" AutoEventWireup="true" CodeBehind="SolutionsView.aspx.cs" Inherits="EmpLeavesTask.Ticket.SolutionsView" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="solution_id" DataSourceID="Solution">
        <Columns>
            <asp:BoundField DataField="solution_id" HeaderText="solution_id" InsertVisible="False" ReadOnly="True" SortExpression="solution_id" />
            <asp:BoundField DataField="ticket_id" HeaderText="ticket_id" SortExpression="ticket_id" />
            <asp:BoundField DataField="raised_to" HeaderText="raised_to" SortExpression="raised_to" />
            <asp:BoundField DataField="raised_by" HeaderText="raised_by" SortExpression="raised_by" />
            <asp:BoundField DataField="ticket" HeaderText="ticket" SortExpression="ticket" />
            <asp:BoundField DataField="attachment" HeaderText="attachment" SortExpression="attachment" />
            <asp:BoundField DataField="ticket_date" HeaderText="ticket_date" SortExpression="ticket_date" />
            <asp:BoundField DataField="ticket_solution" HeaderText="ticket_solution" SortExpression="ticket_solution" />
            <asp:BoundField DataField="solution_date" HeaderText="solution_date" SortExpression="solution_date" />
        </Columns>
    </asp:GridView>
    <asp:SqlDataSource ID="Solution" runat="server" ConnectionString="<%$ ConnectionStrings:dbconn %>" SelectCommand="GetSolutionByEmpId" SelectCommandType="StoredProcedure">
        <SelectParameters>
            <asp:SessionParameter Name="raised_by" SessionField="user_id" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>

</asp:Content>

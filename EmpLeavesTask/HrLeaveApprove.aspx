<%@ Page Title="" Language="C#" MasterPageFile="~/Hr.Master" AutoEventWireup="true" CodeBehind="HrLeaveApprove.aspx.cs" Inherits="EmpLeavesTask.HrLeaveApprove" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" DataSourceID="db" OnRowCommand="GridView1_RowCommand">
        <Columns>
            <asp:BoundField DataField="ename" HeaderText="ename" SortExpression="ename" />
            <asp:BoundField DataField="email" HeaderText="email" SortExpression="email" />
            <asp:BoundField DataField="reason" HeaderText="reason" SortExpression="reason" />
            <asp:BoundField DataField="fromdate" HeaderText="fromdate" SortExpression="fromdate" />
            <asp:BoundField DataField="todate" HeaderText="todate" SortExpression="todate" />
            <asp:BoundField DataField="leave_status" HeaderText="leave_status" SortExpression="leave_status" />
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:Button ID="ApproveButton" runat="server" Text="Approve" CommandName="Approve" CommandArgument='<%# Eval("leave_id") %>' />
                    <asp:Button ID="RejectButton" runat="server" Text="Reject" CommandName="Reject" CommandArgument='<%# Eval("leave_id") %>' />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <asp:SqlDataSource ID="db" runat="server" ConnectionString="<%$ ConnectionStrings:dbconn %>" SelectCommand="GetEmployeeLeaveRequests" SelectCommandType="StoredProcedure"></asp:SqlDataSource>

</asp:Content>

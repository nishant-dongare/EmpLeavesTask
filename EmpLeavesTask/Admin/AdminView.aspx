<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="AdminView.aspx.cs" Inherits="EmpLeavesTask.Admin.AdminView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
      <h1 class="text-center">View Closed Tickets</h1>
  <div class="container">
        <div class="form-group">
            <asp:Label ID="lblViewBy" runat="server" Text="View by:"></asp:Label>
            <asp:DropDownList ID="ddlViewBy" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlViewBy_SelectedIndexChanged" CssClass="form-control w-50">
                <asp:ListItem Text="Daily" Value="Daily"></asp:ListItem>
                <asp:ListItem Text="Weekly" Value="Weekly"></asp:ListItem>
                <asp:ListItem Text="Monthly" Value="Monthly"></asp:ListItem>
            </asp:DropDownList>
            <asp:Button ID="btnExport" runat="server" Text="Export to CSV" OnClick="btnExport_Click" CssClass="btn btn-primary float-right" />
        </div>
      <asp:GridView ID="GridViewClosedTickets" runat="server" AutoGenerateColumns="False" DataKeyNames="ticket_id" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" Width="992px">
          <Columns>
              <asp:BoundField DataField="ticket_id" HeaderText="Ticket ID" />
              <asp:BoundField DataField="raised_by_name" HeaderText="Raised By" />
              <asp:BoundField DataField="raised_to_name" HeaderText="Raised To" />
              <asp:BoundField DataField="ticket_description" HeaderText="Ticket Description" />
              <asp:BoundField DataField="solution" HeaderText="Solution" />
              <asp:BoundField DataField="closed_date" HeaderText="Closed Date" />
          </Columns>
          <FooterStyle BackColor="White" ForeColor="#000066" />
          <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
          <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
          <RowStyle ForeColor="#000066" />
          <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
          <SortedAscendingCellStyle BackColor="#F1F1F1" />
          <SortedAscendingHeaderStyle BackColor="#007DBB" />
          <SortedDescendingCellStyle BackColor="#CAC9C9" />
          <SortedDescendingHeaderStyle BackColor="#00547E" />
      </asp:GridView>
  </div>

    <%--<asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="solution_id" DataSourceID="Solution">
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
    </asp:GridView>--%>

    <%--<asp:SqlDataSource ID="Solution" runat="server" ConnectionString="<%$ ConnectionStrings:dbconn %>" SelectCommand="GetAllSolutions" SelectCommandType="StoredProcedure"></asp:SqlDataSource>--%>

</asp:Content>

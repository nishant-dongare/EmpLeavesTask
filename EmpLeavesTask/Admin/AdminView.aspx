<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="AdminView.aspx.cs" Inherits="EmpLeavesTask.Admin.AdminView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        .gridview-header {
            background-color: #007BFF;
            color: white;
        }
        .gridview-row {
            background-color: #f8f9fa;
        }
        .gridview-row:hover {
            background-color: #e9ecef;
        }
        .gridview-selected {
            background-color: #007BFF;
            color: white;
        }
        .form-control.w-50 {
            max-width: 50%;
        }
    </style>
    <div class="container mt-5">
        <h1 class="text-center mb-4">View Solution Tickets</h1>
        <div class="form-group row">
            <label for="ddlViewBy" class="col-sm-2 col-form-label">View by:</label>
            <div class="col-sm-4">
                <asp:DropDownList ID="ddlViewBy" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlViewBy_SelectedIndexChanged" CssClass="form-control w-100">
                    <asp:ListItem Text="Daily" Value="Daily"></asp:ListItem>
                    <asp:ListItem Text="Weekly" Value="Weekly"></asp:ListItem>
                    <asp:ListItem Text="Monthly" Value="Monthly"></asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="col-sm-6 text-right">
                <asp:Button ID="btnExport" runat="server" Text="Export to CSV" OnClick="btnExport_Click" CssClass="btn btn-primary" />
            </div>
        </div>
        
        <asp:GridView ID="GridViewClosedTickets" runat="server" AutoGenerateColumns="False" DataKeyNames="ticket_id" CssClass="table table-bordered table-hover">
            <Columns>
                <asp:BoundField DataField="ticket_id" HeaderText="Ticket ID" HeaderStyle-CssClass="gridview-header" ItemStyle-CssClass="gridview-row" />
                <asp:BoundField DataField="raised_by_name" HeaderText="Raised By" HeaderStyle-CssClass="gridview-header" ItemStyle-CssClass="gridview-row" />
                <asp:BoundField DataField="raised_to_name" HeaderText="Raised To" HeaderStyle-CssClass="gridview-header" ItemStyle-CssClass="gridview-row" />
                <asp:BoundField DataField="ticket_description" HeaderText="Ticket Description" HeaderStyle-CssClass="gridview-header" ItemStyle-CssClass="gridview-row" />
                <asp:BoundField DataField="solution" HeaderText="Solution" HeaderStyle-CssClass="gridview-header" ItemStyle-CssClass="gridview-row" />
                <asp:BoundField DataField="closed_date" HeaderText="Closed Date" HeaderStyle-CssClass="gridview-header" ItemStyle-CssClass="gridview-row" />
            </Columns>
            <FooterStyle CssClass="gridview-footer" />
            <HeaderStyle CssClass="gridview-header" />
            <PagerStyle CssClass="gridview-pager" />
            <RowStyle CssClass="gridview-row" />
            <SelectedRowStyle CssClass="gridview-selected" />
            <SortedAscendingCellStyle CssClass="gridview-sorted-ascending" />
            <SortedAscendingHeaderStyle CssClass="gridview-sorted-ascending-header" />
            <SortedDescendingCellStyle CssClass="gridview-sorted-descending" />
            <SortedDescendingHeaderStyle CssClass="gridview-sorted-descending-header" />
        </asp:GridView>
    </div>
</asp:Content>


<%--<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
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
</asp:Content>--%>

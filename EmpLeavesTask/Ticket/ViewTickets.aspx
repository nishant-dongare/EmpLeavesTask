<%@ Page Title="" Language="C#" MasterPageFile="~/Employee.Master" AutoEventWireup="true" CodeBehind="ViewTickets.aspx.cs" Inherits="EmpLeavesTask.Ticket.ViewTickets" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-5">
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="ticket_id" DataSourceID="db" OnRowDataBound="DownloadAttachmentRowDataBound" CssClass="table table-striped table-bordered">
            <Columns>
                <asp:BoundField DataField="ticket_id" HeaderText="ticket_id" InsertVisible="False" ReadOnly="True" SortExpression="ticket_id" HeaderStyle-CssClass="bg-dark text-white" ItemStyle-CssClass="align-middle" />
                <asp:BoundField DataField="raised_to" HeaderText="raised_to" SortExpression="raised_to" HeaderStyle-CssClass="bg-dark text-white" ItemStyle-CssClass="align-middle" />
                <asp:BoundField DataField="raised_by" HeaderText="raised_by" SortExpression="raised_by" HeaderStyle-CssClass="bg-dark text-white" ItemStyle-CssClass="align-middle" />
                <asp:BoundField DataField="ticket" HeaderText="ticket" SortExpression="ticket" HeaderStyle-CssClass="bg-dark text-white" ItemStyle-CssClass="align-middle" />
                <asp:BoundField DataField="ticket_date" HeaderText="ticket_date" SortExpression="ticket_date" HeaderStyle-CssClass="bg-dark text-white" ItemStyle-CssClass="align-middle" />
                <asp:TemplateField HeaderText="Attachments" HeaderStyle-CssClass="bg-dark text-white" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Literal ID="litDownload" runat="server"></asp:Literal>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Solution" HeaderStyle-CssClass="bg-dark text-white" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Literal ID="litCLoseSolution" runat="server"></asp:Literal>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
    <asp:SqlDataSource ID="db" runat="server" ConnectionString="<%$ ConnectionStrings:dbconn %>" SelectCommand="SELECT * FROM [RaiseTicket] WHERE ([raised_to] = @raised_to)">
        <SelectParameters>
            <asp:SessionParameter Name="raised_to" SessionField="emp_id" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
</asp:Content>

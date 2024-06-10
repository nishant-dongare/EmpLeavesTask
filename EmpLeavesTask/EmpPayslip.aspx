<%@ Page Title="" Language="C#" MasterPageFile="~/Employee.Master" AutoEventWireup="true" CodeBehind="EmpPayslip.aspx.cs" Inherits="EmpLeavesTask.EmpPayslip" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <asp:GridView ID="GridView1" CssClass="table table-striped table-bordered" HeaderStyle-CssClass="thead-dark" runat="server" AutoGenerateColumns="False" DataKeyNames="payslip_id" DataSourceID="db" OnRowDataBound="gvPayslips_RowDataBound">
            <Columns>
                <asp:BoundField DataField="payslip_id" HeaderText="payslip_id" InsertVisible="False" ReadOnly="True" SortExpression="payslip_id" />
                <asp:BoundField DataField="month" HeaderText="month" SortExpression="month" />
                <asp:BoundField DataField="year" HeaderText="year" SortExpression="year" />
                <asp:TemplateField HeaderText="View Payslip">
                    <ItemTemplate>
                        <asp:Literal ID="litMessage" runat="server"></asp:Literal>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Downloads">
                    <ItemTemplate>
                        <asp:Literal ID="litDownload" runat="server"></asp:Literal>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <asp:SqlDataSource ID="db" runat="server" ConnectionString="<%$ ConnectionStrings:dbconn %>" SelectCommand="SELECT [payslip_id], [month], [year], [filepath] FROM [Payslips] ORDER BY [month]"></asp:SqlDataSource>
    </div>
</asp:Content>

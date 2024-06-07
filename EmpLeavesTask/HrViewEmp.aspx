<%@ Page Title="" Language="C#" MasterPageFile="~/hr.Master" AutoEventWireup="true" CodeBehind="HrViewEmp.aspx.cs" Inherits="EmpLeavesTask.viewemp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    View Emp
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="card">
        <div class="card-header">
            <h3 class="text-center">Add Employee</h3>
        </div>
        <div class="card-body">
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <contenttemplate>
                    <asp:GridView runat="server" AutoGenerateColumns="False" DataKeyNames="emp_id" DataSourceID="db">
                        <Columns>
                            <asp:BoundField DataField="emp_id" HeaderText="emp_id" InsertVisible="False" ReadOnly="True" SortExpression="emp_id" />
                            <asp:BoundField DataField="ename" HeaderText="ename" SortExpression="ename" />
                            <asp:BoundField DataField="email" HeaderText="email" SortExpression="email" />
                            <asp:BoundField DataField="contact" HeaderText="contact" SortExpression="contact" />
                            <asp:BoundField DataField="doj" HeaderText="doj" SortExpression="doj" />
                        </Columns>
                    </asp:GridView>
                    <asp:SqlDataSource ID="db" runat="server" ConnectionString="<%$ ConnectionStrings:dbconn %>" SelectCommand="SELECT * FROM [Employee]"></asp:SqlDataSource>
                </contenttemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>

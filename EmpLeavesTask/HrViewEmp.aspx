<%@ Page Title="" Language="C#" MasterPageFile="~/hr.Master" AutoEventWireup="true" CodeBehind="HrViewEmp.aspx.cs" Inherits="EmpLeavesTask.viewemp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    View Emp
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="card">
        <div class="card-header">
            <h3 class="text-center">View Employee</h3>
        </div>
        <div class="card-body">
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <asp:GridView runat="server" AutoGenerateColumns="False" DataKeyNames="emp_id" DataSourceID="db">
                        <Columns>
                            <asp:BoundField DataField="emp_id" HeaderText="emp_id" InsertVisible="False" ReadOnly="True" SortExpression="emp_id" />
                            <asp:BoundField DataField="ename" HeaderText="ename" SortExpression="ename" />
                            <asp:BoundField DataField="email" HeaderText="email" SortExpression="email" />
                            <asp:BoundField DataField="contact" HeaderText="contact" SortExpression="contact" />
                            <asp:BoundField DataField="doj" HeaderText="doj" SortExpression="doj" />
                            <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" />

                        </Columns>
                    </asp:GridView>
                    <asp:SqlDataSource ID="db" runat="server" ConnectionString="<%$ ConnectionStrings:dbconn %>" SelectCommand="GetAllEmployees" ConflictDetection="CompareAllValues" DeleteCommand="DeleteEmployee" InsertCommand="AddEmployee" OldValuesParameterFormatString="original_{0}" UpdateCommand="UpdateEmployee" DeleteCommandType="StoredProcedure" InsertCommandType="StoredProcedure" SelectCommandType="StoredProcedure" UpdateCommandType="StoredProcedure">
                        <DeleteParameters>
                            <asp:Parameter Name="EmpID" Type="Int32" />
                        </DeleteParameters>
                        <InsertParameters>
                            <asp:Parameter Name="Name" Type="String" />
                            <asp:Parameter Name="email" Type="String" />
                            <asp:Parameter Name="passkey" Type="String" />
                            <asp:Parameter Name="contact" Type="Decimal" />
                            <asp:Parameter Name="doj" Type="String" />
                        </InsertParameters>
                        <UpdateParameters>
                            <asp:Parameter Name="emp_id" Type="Int32" />
                            <asp:Parameter Name="ename" Type="String" />
                            <asp:Parameter Name="email" Type="String" />
                            <asp:Parameter Name="passkey" Type="String" />
                            <asp:Parameter Name="contact" Type="Decimal" />
                            <asp:Parameter Name="doj" Type="String" />
                        </UpdateParameters>
                    </asp:SqlDataSource>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>

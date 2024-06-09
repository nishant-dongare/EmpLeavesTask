<%@ Page Title="" Language="C#" MasterPageFile="~/hr.Master" AutoEventWireup="true" CodeBehind="HrViewEmp.aspx.cs" Inherits="EmpLeavesTask.viewemp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    View Emp
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="card">
        <div class="card-header">
            <h3 class="text-center">View Employee</h3>
        </div>
        <div class="card">
            <div class="card-body">
                <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <asp:GridView
                            runat="server"
                            AutoGenerateColumns="False"
                            DataKeyNames="emp_id"
                            DataSourceID="db"
                            CssClass="table table-striped table-bordered"
                            HeaderStyle-CssClass="thead-dark">
                            <Columns>
                                <asp:BoundField DataField="emp_id" HeaderText="Employee ID" InsertVisible="False" ReadOnly="True" SortExpression="emp_id" />
                                <asp:BoundField DataField="ename" HeaderText="Employee Name" SortExpression="ename" />
                                <asp:BoundField DataField="email" HeaderText="Email" SortExpression="email" />
                                <asp:BoundField DataField="contact" HeaderText="Contact" SortExpression="contact" />
                                <asp:BoundField DataField="doj" HeaderText="Date of Joining" SortExpression="doj" />
                                <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" />
                            </Columns>
                        </asp:GridView>
                        <asp:SqlDataSource
                            ID="db"
                            runat="server"
                            ConnectionString="<%$ ConnectionStrings:dbconn %>"
                            SelectCommand="GetAllEmployees"
                            ConflictDetection="CompareAllValues"
                            DeleteCommand="DeleteEmployee"
                            InsertCommand="AddEmployee"
                            OldValuesParameterFormatString="original_{0}"
                            UpdateCommand="UpdateEmployee"
                            DeleteCommandType="StoredProcedure"
                            InsertCommandType="StoredProcedure"
                            SelectCommandType="StoredProcedure"
                            UpdateCommandType="StoredProcedure">
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

    </div>
</asp:Content>

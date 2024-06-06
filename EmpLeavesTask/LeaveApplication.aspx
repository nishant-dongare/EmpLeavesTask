<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LeaveApplication.aspx.cs" Inherits="EmpLeavesTask.LeaveApplication" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="Label1" runat="server" Text="Reason"></asp:Label>
            <asp:TextBox ID="txtreason" runat="server" TextMode="MultiLine"></asp:TextBox>
            <br />
            <asp:Label ID="Label4" runat="server" Text="From"></asp:Label>
            &nbsp;&nbsp;&nbsp;
            <asp:TextBox ID="txtfromdate" runat="server" TextMode="Date"></asp:TextBox>
            <br />
            <asp:Label ID="Label2" runat="server" Text="To"></asp:Label>
            &nbsp;&nbsp;&nbsp;
            <asp:TextBox ID="txttodate" runat="server" TextMode="Date"></asp:TextBox>
            <br />
            <asp:Label ID="Label5" runat="server" Text="Balance"></asp:Label>
            <br />
            <asp:Button ID="Button1" runat="server" OnClick="OnSubmit" Text="Submit" />
        </div>
    </form>
</body>
</html>

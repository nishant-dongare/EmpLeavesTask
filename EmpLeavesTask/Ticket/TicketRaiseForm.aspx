<%@ Page Title="" Language="C#" MasterPageFile="~/Employee.Master" AutoEventWireup="true" CodeBehind="TicketRaiseForm.aspx.cs" Inherits="EmpLeavesTask.Ticket.TicketRaiseForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-5">
        <div class="row justify-content-center">
            <div class="col-md-6">
                <div class="card">
                    <div class="card-header bg-primary text-white">
                        <h5 class="card-title text-center">Raise Ticket</h5>
                    </div>
                    <div class="card-body">
                        <asp:Literal ID="ltMessage" runat="server" EnableViewState="False"></asp:Literal>

                        <div class="form-group">
                            <label for="lblEmail">Email address:</label>
                            <asp:Label ID="lblEmail" runat="server" CssClass="form-control" />
                        </div>
                        <div class="form-group">
                            <label for="ddlRole">Select role:</label>
                            <asp:DropDownList ID="ddlRole" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlRole_SelectedIndexChanged">
                                <asp:ListItem Text="-- Select Role --" Value="" />
                            </asp:DropDownList>
                        </div>
                        <div class="form-group">
                            <label for="ddlRaisedTo">Raised to:</label>
                            <asp:DropDownList ID="ddlRaisedTo" runat="server" CssClass="form-control" Enabled="false">
                                <asp:ListItem Text="-- Select --" Value="" />
                            </asp:DropDownList>
                        </div>
                        <div class="form-group">
                            <label for="txtDescription">Ticket:</label>
                            <asp:TextBox ID="ticket_tb" runat="server" TextMode="MultiLine" CssClass="form-control" Rows="5" />
                        </div>
                        <div class="form-group">
                            <label for="fileAttachment">Attachment:</label>
                            <asp:FileUpload ID="fileupload1" runat="server" CssClass="form-control-file" />
                        </div>
                        <asp:Button ID="btnRaiseTicket" runat="server" Text="Raise Ticket" CssClass="btn btn-primary btn-block" OnClick="btnRaiseTicket_Click" />
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

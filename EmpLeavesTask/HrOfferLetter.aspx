<%@ Page Title="" Language="C#" MasterPageFile="~/Hr.Master" AutoEventWireup="true" CodeBehind="HrOfferLetter.aspx.cs" Inherits="EmpLeavesTask.HrOfferLetter" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <div style="margin: 0 auto; max-width: 400px; padding-top: 50px;">
            <h2 class="text-center">Offer Letter</h2>
            <div class="form-group">
                <asp:TextBox ID="txtName" runat="server" CssClass="form-control" placeholder="Name"></asp:TextBox>
            </div>
            <div class="form-group">
                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" placeholder="Email"></asp:TextBox>
            </div>
            <div class="form-group">
                <asp:TextBox ID="txtContact" runat="server" CssClass="form-control" placeholder="Contact"></asp:TextBox>
            </div>
            <div class="form-group">
                <asp:TextBox ID="txtDateOfJoining" runat="server" CssClass="form-control" placeholder="Date of Joining"></asp:TextBox>
            </div>
            <div class="form-group text-center">
                <asp:Button ID="btnGenerateOfferLetter" runat="server" CssClass="btn btn-primary" Text="Generate Offer Letter" OnClick="btnGenerateOfferLetter_Click" />
            </div>
        </div>
        <asp:Panel ID="pnlOfferLetter" runat="server" Style="margin-top: 100px">
            <!-- Offer Letter Content -->
        </asp:Panel>

    </div>
</asp:Content>

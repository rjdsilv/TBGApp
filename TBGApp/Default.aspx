<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="TBGApp._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container tbg-info-display">
        <div class="col-md-12 tbg-info-display-title">
            <span>TBG Database And Data Creation Status</span>
        </div>
        <div class="row">
            <div class="col-md-12">
                <div class="col-md-5">
                    <asp:Label ID="CreateDbTextLabel" Text="The Trivia Board Game - Database Created: " runat="server" />
                </div>
                <div class="col-md-5">
                    <asp:Label ID="CreateDbStatusLabel" Text="NO" CssClass="tbg-info-no" runat="server" />
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-12">
                <div class="col-md-5">
                    <asp:Label ID="CreateTablesTextLabel" Text="The Trivia Board Game - Tables Created: " runat="server" />
                </div>
                <div class="col-md-5">
                    <asp:Label ID="CreateTablesStatusLabel" Text="NO" CssClass="tbg-info-no" runat="server" />
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-12">
                <div class="col-md-5">
                    <asp:Label ID="CreateDataTextLabel" Text="The Trivia Board Game - Application Data Created: " runat="server" />
                </div>
                <div class="col-md-5">
                    <asp:Label ID="CreateDataStatusLabel" Text="NO" CssClass="tbg-info-no" runat="server" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>

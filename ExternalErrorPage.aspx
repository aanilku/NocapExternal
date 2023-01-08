<%@ Page Title="" Language="C#" MasterPageFile="~/ExternalUser/ExternalUserMaster.master" AutoEventWireup="true" CodeFile="ExternalErrorPage.aspx.cs" Inherits="ExternalErrorPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<center>

 <asp:Label runat="server" ID="Error" ForeColor="Red" Text="Error on page please try again!"></asp:Label>

 </center>
</asp:Content>


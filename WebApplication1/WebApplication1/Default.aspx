<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebApplication1._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <br />
    <div>
        <center>
            <asp:Label ID="Label3" runat="server" Text="User Registration Form" BorderStyle="Solid" Font-Size="XX-Large" BorderColor="White" BackColor="White"></asp:Label>
        </center>
    </div>
    <br />
    <br />
    <br />
    <br />

    <asp:Label ID="Label1" runat="server" Text="Name"></asp:Label>
    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
    <br />
    <asp:Label ID="Label2" runat="server" Text="Email"></asp:Label>





<asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
<br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
<asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Insert" />
    <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="Search" />
    <br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:Label ID="lblmsg" runat="server" Font-Bold="True" ForeColor="#003300" Text="Label"></asp:Label>





    <br />
    <asp:GridView ID="GridView1" runat="server">
    </asp:GridView>





</asp:Content>

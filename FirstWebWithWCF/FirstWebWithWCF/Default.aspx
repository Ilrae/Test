<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="FirstWebWithWCF._Default" %>

<%--<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>--%>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div style="width: 54%; height: 202px;">
        <div style="background-color: blue; color: white; font-size: large; font-weight: bolder">
            My Calculator
        </div>
        <br />
        <div style="background-color: gray; color: blue; font-size: medium; font-weight: bolder">
            Number1 :
            <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
            Number2 :
            <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
        </div>
        <br />
        <div>   
            <asp:Button ID="Button1" runat="server" Text="Add" BackColor="#0099FF" Font-Bold="True" Font-Size="Medium" ForeColor="White" Width="62px" OnClick="Button1_Click"/>
            <asp:Button ID="Button2" runat="server" Text="Sub" BackColor="#0099FF" Font-Bold="True" Font-Size="Medium" ForeColor="White" Width="62px" OnClick="Button2_Click"/>
            <asp:Button ID="Button3" runat="server" Text="Mul" BackColor="#0099FF" Font-Bold="True" Font-Size="Medium" ForeColor="White" Width="62px" OnClick="Button3_Click"/>
            <asp:Button ID="Button4" runat="server" Text="Div" BackColor="#0099FF" Font-Bold="True" Font-Size="Medium" ForeColor="White" Width="62px" OnClick="Button4_Click"/>
        </div>
        <br />
        <div style="; color: blue; font-size: medium; font-weight: bolder">
            Result : <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
        </div>
    </div>

</asp:Content>

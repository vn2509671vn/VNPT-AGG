<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="VNPT_BSC.Home" %>
<%@ Register assembly="DevExpress.Web.v13.1, Version=13.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxPanel" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.v13.1, Version=13.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxRoundPanel" tagprefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="Bootstrap/bootstrap.css" rel="stylesheet" />
    <script src="Bootstrap/bootstrap.js"></script>
     <style type="text/css">
        .style1
        {
            width:200px;
            font-size: medium;
          
        }
         .style2
         {
             border: 2px solid blue;
             border-radius: 5px;
             width:100px;
         }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <p>
        <dx:ASPxRoundPanel ID="ASPxRoundPanel2" runat="server" Width="100%" Height="100%" HeaderText="Quản Lý Chức Danh">
            
      <PanelCollection>
        <dx:PanelContent runat="server" SupportsDisabledAttribute="True">
   
            <table style="width:100%;">
                <tr>
                    <td></td>
                </tr>
                <tr>
                    <td style="width:100px">

                    </td>
                    <td class="style1">
                        <asp:Label CssClass="" ID="Label1" runat="server" Text="Tên chức danh"></asp:Label>
                    </td>
                    <td >
                        <asp:TextBox class="form-control" ID="TextBox1" runat="server" Width="400px"></asp:TextBox>
                    </td>
                </tr>
                 <tr style="height:20px">
                    <td ></td>
                </tr>
                <tr>
                    <td style="width:100px">

                    </td>
                    <td class="style1">
                        <asp:Label ID="Label2" runat="server" Text="Mô tả chức danh"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="TextBox2" runat="server" class="form-control" Width="400px"></asp:TextBox>
                    </td>
                </tr>
                 <tr style="height:20px">
                    <td></td>
                </tr>
                <tr>
                    <td style="width:100px">

                    </td>
                    <td class="style1">
                        <asp:Label ID="Label3" runat="server" Text="Mã chức danh"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="TextBox3" runat="server" class="form-control" Width="400px"></asp:TextBox>
                    </td>
                </tr>
               
            </table>
   
        </dx:PanelContent>
      </PanelCollection>
            
        </dx:ASPxRoundPanel>
    </p>
</asp:Content>

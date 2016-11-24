<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="VNPT_BSC.Home" %>
<%@ Register assembly="DevExpress.Web.v13.1, Version=13.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxPanel" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.v13.1, Version=13.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxRoundPanel" tagprefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <p>
        <dx:ASPxRoundPanel ID="ASPxRoundPanel2" runat="server" Width="100%" Height="100%" HeaderText="Quản Lý Chức Danh">
            
      <PanelCollection>
        <dx:PanelContent runat="server" SupportsDisabledAttribute="True">
   
            <table style="width:100%;">
                <tr>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
               
            </table>
   
        </dx:PanelContent>
      </PanelCollection>
            
        </dx:ASPxRoundPanel>
    </p>
</asp:Content>

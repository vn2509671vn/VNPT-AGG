<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="PhanPhoiBSCDonVi.aspx.cs" Inherits="VNPT_BSC.BSC.PhanPhoiBSCDonVi" %>
<%@ Register assembly="DevExpress.Web.v13.1, Version=13.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxRoundPanel" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.v13.1, Version=13.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxPanel" tagprefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Bootstrap/bootstrap.css" rel="stylesheet" />
    <script src="../Bootstrap/bootstrap.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="margin-top:30px">
        <dx:ASPxRoundPanel ID="ASPxRoundPanel2" runat="server" Width="100%" BackColor="White" EnableTheming="True" HeaderText="PHÂN PHỐI KPI ĐƠN VỊ" Theme="Aqua">
    
        <HeaderStyle BackColor="#0080FF">
        <BorderLeft BorderStyle="None" />
        <BorderRight BorderStyle="None" />
        <BorderBottom BorderStyle="None" />
        </HeaderStyle>
        <TopLeftCorner Height="5px" Url="~/Images/ASPxRoundPanel/1531691351/TopLeftCorner.png" Width="5px">
        </TopLeftCorner>
        <NoHeaderTopLeftCorner Height="5px" Url="~/Images/ASPxRoundPanel/1531691351/NoHeaderTopLeftCorner.png" Width="5px">
        </NoHeaderTopLeftCorner>
        <TopRightCorner Height="5px" Url="~/Images/ASPxRoundPanel/1531691351/TopRightCorner.png" Width="5px">
        </TopRightCorner>
        <NoHeaderTopRightCorner Height="5px" Url="~/Images/ASPxRoundPanel/1531691351/NoHeaderTopRightCorner.png" Width="5px">
        </NoHeaderTopRightCorner>
        <BottomRightCorner Height="5px" Url="~/Images/ASPxRoundPanel/1531691351/BottomRightCorner.png" Width="5px">
        </BottomRightCorner>
        <BottomLeftCorner Height="5px" Url="~/Images/ASPxRoundPanel/1531691351/BottomLeftCorner.png" Width="5px">
        </BottomLeftCorner>
        <HeaderLeftEdge>
            <BackgroundImage HorizontalPosition="left" ImageUrl="~/Images/ASPxRoundPanel/1531691351/HeaderLeftEdge.png" Repeat="NoRepeat" VerticalPosition="bottom" />
        </HeaderLeftEdge>
        <HeaderContent>
            <BackgroundImage HorizontalPosition="left" ImageUrl="~/Images/ASPxRoundPanel/1531691351/HeaderContent.png" Repeat="RepeatX" VerticalPosition="bottom" />
        </HeaderContent>
        <HeaderRightEdge>
            <BackgroundImage HorizontalPosition="right" ImageUrl="~/Images/ASPxRoundPanel/1531691351/HeaderRightEdge.png" Repeat="NoRepeat" VerticalPosition="bottom" />
        </HeaderRightEdge>
        <PanelCollection>
        <dx:PanelContent ID="PanelContent1" runat="server" SupportsDisabledAttribute="True">
            <table style="width:100%;">
                <tr>
                    <td>&nbsp;</td>
                    <td width="20%">Đơn vị giao:</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td width="20%">Đơn vị nhận:</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                </table>
        </dx:PanelContent>
        </PanelCollection>
        <Border BorderColor="#8B8B8B" BorderStyle="Solid" BorderWidth="1px" />
    
    </dx:ASPxRoundPanel>
    </div>
    
</asp:Content>

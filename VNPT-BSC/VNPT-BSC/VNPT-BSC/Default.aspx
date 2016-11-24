<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="VNPT.Default" %>

<%@ Register assembly="DevExpress.Web.v13.1, Version=13.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxRoundPanel" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.v13.1, Version=13.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxPanel" tagprefix="dx" %>

<%@ Register assembly="DevExpress.Web.v13.1, Version=13.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>

<%--<%@ Register Assembly="DevExpress.Web.v13.1, Version=13.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.v13.1, Version=13.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxRoundPanel" TagPrefix="dx" %>

<%@ Register assembly="DevExpress.Web.v13.1, Version=13.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    namespace="DevExpress.Web.ASPxPanel" tagprefix="dx" %>--%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Portal Viễn thông An Giang</title><%--<a href="BaoCao/">BaoCao/</a>--%>
    <link rel="icon" href="~/Images/vnpt.ico" type="image/x-icon" />
    <link rel="shortcut icon" href="~/Images/vnpt.ico" type="image/x-icon" />
    <link rel="stylesheet" type="text/css" href="Styles/Site.css" media="all" />
</head>
<body style="height: 100%; background:#DCECF5">
    <form id="form1" runat="server" style="background: #DCECF5">
    <div>
        <table style="padding:5px; width:100%; height:auto; ">
            <tr>
                <td >
                </td>
                <td style="width:400px; height:100px;">
                    &nbsp;</td>
                <td>
                </td>
            </tr>
            <tr>
                <td >
                </td>
                <td style="width:400px; height:200px;" align="center" valign="top">
                    <dx:ASPxRoundPanel ID="ASPxRoundPanel1" runat="server" HeaderText="Đăng nhập - Quản Lý BSC" Width="255px" Theme="Youthful" EnableTheming="True">
                         <PanelCollection>
                             <dx:PanelContent ID="PanelContent1" runat="server" SupportsDisabledAttribute="True">
                                 <table style="width:400px; height:auto; ">
                                     <tr>
                                         <td style="height:10px">

                                         </td>
                                     </tr>
                                     <tr>
                                         <td align="right">
                                             <asp:Label ID="Label3" runat="server" Text="Tài khoản :  "></asp:Label>
                                         </td>
                                            
                                         <td  align="left">
                                             
                                             <dx:ASPxTextBox ID="ASPxTextBox1" runat="server" Width="150px" Height="27px">
                                             </dx:ASPxTextBox>
                                             
                                         </td>
                                     </tr>
                                      <tr>
                            <td style="height:10px">
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <asp:Label ID="Label2" runat="server" Text="Mật khẩu :  "></asp:Label>
                            </td>
                            <td align="left">
                                <dx:ASPxTextBox ID="txtPassword" runat="server" Width="150px" Height="27px" Password="True" >
                                </dx:ASPxTextBox>
                               <%-- <asp:TextBox ID="txtPassword" TextMode="Password" runat="server"  style="width:150px; height:22px"></asp:TextBox>--%>
                            </td>
                        </tr>
                                      <tr>
                            <td style="height:10px">
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                
                            </td>
                            <td align="left">
                              
                                <dx:ASPxButton ID="ASPxButton1" runat="server" Text="Đăng nhập" OnClick="ASPxButton1_Click" Theme="iOS">
                                </dx:ASPxButton>
                    
                            </td>
                        </tr>
                        <tr>
                            <td style="height:10px">
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" align="center">
                                <asp:Label ID="lblThongbao" runat="server" ForeColor="Red"></asp:Label>
                            </td>
                        </tr>
                                 </table>
                             </dx:PanelContent>
                         </PanelCollection>
                    </dx:ASPxRoundPanel>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td> 
                </td>
                <td>
                </td>
                <td>
                </td>
            </tr>
        </table>
                    
        <div style="width:100%; text-align:center">
            <p style="color:Blue">Điện thoại hỗ trợ: </p>
        </div>
    </div>
    </form>
</body>
</html>

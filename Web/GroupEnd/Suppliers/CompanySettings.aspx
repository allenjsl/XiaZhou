<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CompanySettings.aspx.cs"
    Inherits="Web.GroupEnd.Suppliers.CompanySettings" %>

<%@ Register Src="~/UserControl/Suppliers.ascx" TagName="Suppliers" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta content="text/html; charset=utf-8" http-equiv="Content-Type" />
    <!-- InstanceBeginEditable name="doctitle" -->
    <title>供应商系统设置</title>
    <!-- InstanceEndEditable -->
    <link type="text/css" rel="stylesheet" href="/Css/fx_style.css" />
    <link type="text/css" rel="stylesheet" href="/Css/boxynew.css" />
</head>
<body style="background: 0 none;">
    <uc1:Suppliers ID="Suppliers1" runat="server" SystemClass="default xtglicon"  />
    <!-- InstanceBeginEditable name="EditRegion3" -->
    <div class="list-main">
        <div class="list-maincontent">
            <div class="linebox-menu">
                <a href="PasswordSettings.aspx?sl=<%=EyouSoft.Common.Utils.GetQueryStringValue("sl") %>">密码修改</a> <a href="CompanySettings.aspx?sl=<%=EyouSoft.Common.Utils.GetQueryStringValue("sl") %>" class="linebox-menudefault">
                    公司信息</a>
            </div>
            <div class="hr_10">
            </div>
            <div class="listtablebox">
                <table width="100%" cellspacing="0" cellpadding="0"  border="0">
                    <tbody>
                        <tr>
                            <td width="15%" bgcolor="#DCEFF3" align="right">
                                国家：
                            </td>
                            <td width="35%" align="left">
                                <asp:Label runat="server" ID="lblCountry"></asp:Label>
                            </td>
                            <td width="15%" bgcolor="#DCEFF3" align="right">
                                省份：
                            </td>
                            <td width="35%" align="left">
                                <asp:Label runat="server" ID="lblProvice"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td width="15%" bgcolor="#DCEFF3" align="right">
                                城市：
                            </td>
                            <td width="35%" align="left">
                                <asp:Label runat="server" ID="lblCity"></asp:Label>
                            </td>
                            <td width="15%" bgcolor="#DCEFF3" align="right">
                                县区：
                            </td>
                            <td width="35%" align="left">
                                <asp:Label runat="server" ID="lblCounty"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td width="15%" bgcolor="#DCEFF3" align="right">
                                单位名称：
                            </td>
                            <td width="35%" align="left">
                                <asp:Label runat="server" ID="lblName"></asp:Label>
                            </td>
                            <td width="15%" bgcolor="#DCEFF3" align="right">
                                类型：
                            </td>
                            <td width="35%" align="left">
                                <asp:Label runat="server" ID="lblType"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td width="15%" bgcolor="#DCEFF3" align="right">
                                许可证号：
                            </td>
                            <td width="35%" align="left">
                                <asp:Label runat="server" ID="lblLicense"></asp:Label>
                            </td>
                            <td width="15%" bgcolor="#DCEFF3" align="right">
                                法人代表：
                            </td>
                            <td width="35%" align="left">
                                <asp:Label runat="server" ID="lblLegalRepresentative"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td width="15%" bgcolor="#DCEFF3" align="right">
                                电话：
                            </td>
                            <td width="35%" align="left">
                                <asp:Label ID="lblLegalRepresentativePhone" runat="server"></asp:Label>
                            </td>
                            <td width="15%" bgcolor="#DCEFF3" align="right">
                                地址：
                            </td>
                            <td width="35%" align="left">
                                <asp:Label ID="lblAddress" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td width="15%" bgcolor="#DCEFF3" align="right">
                                是否签订合同：
                            </td>
                            <td width="35%" align="left">
                                <asp:Label ID="lblIsSignContract" runat="server"></asp:Label>
                            </td>
                            <td width="15%" bgcolor="#DCEFF3" align="right">
                                合同有效期：
                            </td>
                            <td width="35%" align="left">
                                <asp:Label ID="lblEffectTime" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td width="15%" bgcolor="#DCEFF3" align="right">
                                特色线路：
                            </td>
                            <td align="left" colspan="3">
                                <asp:Label ID="lblFeatureRoute" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td width="15%" bgcolor="#DCEFF3" align="right">
                                是否签订：
                            </td>
                            <td width="35%" align="left">
                                <asp:Label ID="lblIsSign" runat="server"></asp:Label>
                            </td>
                            <td width="15%" bgcolor="#DCEFF3" align="right">
                                是否推荐：
                            </td>
                            <td width="35%" align="left">
                                <asp:Label ID="lblIsRecommend" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td width="15%" bgcolor="#DCEFF3" align="right">
                                是否返佣：
                            </td>
                            <td align="left" colspan="3">
                                <asp:Label ID="lblIsRebatePolicy" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td width="15%" bgcolor="#DCEFF3" align="right">
                                返佣政策：
                            </td>
                            <td align="left" colspan="3">
                                <asp:Label ID="lblRebatePolicy" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
            <div class="hr_10">
            </div>
        </div>
    </div>
    <!-- InstanceEndEditable -->
</body>
</html>

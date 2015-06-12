<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CompanySettings.aspx.cs"
    Inherits="Web.GroupEnd.Distribution.CompanySettings" %>

<%@ Register Src="~/UserControl/HeadDistributorControl.ascx" TagName="HeadDistributorControl"
    TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta content="text/html; charset=utf-8" http-equiv="Content-Type"/>
    <title>分销商公司信息</title>
    <link type="text/css" rel="stylesheet" href="/Css/fx_style.css"/>
    <link type="text/css" rel="stylesheet" href="/Css/boxynew.css"/>
</head>
<body style="background: 0 none;">
    <uc1:HeadDistributorControl ID="HeadDistributorControl1" runat="server" SystemClass="default xtglicon"  />
    <!-- InstanceBeginEditable name="EditRegion3" -->
    <div class="list-main">
        <div class="list-maincontent">
            <div class="linebox-menu">
                <a href="PasswordSettings.aspx?sl=<%=EyouSoft.Common.Utils.GetQueryStringValue("sl") %>">
                    密码修改</a> <a href="CompanySettings.aspx?sl=<%=EyouSoft.Common.Utils.GetQueryStringValue("sl") %>"
                        class="linebox-menudefault">公司信息</a> <a href="ConfigSettings.aspx?sl=<%=EyouSoft.Common.Utils.GetQueryStringValue("sl") %>">
                            配置管理</a>
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
                                地址：
                            </td>
                            <td width="35%" align="left">
                                <asp:Label runat="server" ID="lblAddress"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td width="15%" bgcolor="#DCEFF3" align="right">
                                机构代码：
                            </td>
                            <td width="35%" align="left">
                                <asp:Label runat="server" ID="lblOrganizationCode"></asp:Label>
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
                                法人电话：
                            </td>
                            <td width="35%" align="left">
                                <asp:Label runat="server" ID="lblLegalRepresentativePhone"></asp:Label>
                            </td>
                            <td width="15%" bgcolor="#DCEFF3" align="right">
                                法人手机：
                            </td>
                            <td width="35%" align="left">
                                <asp:Label runat="server" ID="lblLegalRepresentativeMobile"></asp:Label>
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
                                财务姓名：
                            </td>
                            <td width="35%" align="left">
                                <asp:Label runat="server" ID="lblFinancialName"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td width="15%" bgcolor="#DCEFF3" align="right">
                                财务电话：
                            </td>
                            <td align="left">
                                <asp:Label runat="server" ID="lblFinancialPhone"></asp:Label>
                            </td>
                            <td bgcolor="#DCEFF3" align="right">
                                财务手机：
                            </td>
                            <td align="left">
                                <asp:Label runat="server" ID="lblFinancialMobile"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td width="15%" bgcolor="#DCEFF3" align="right">
                                责任销售：
                            </td>
                            <td width="35%" align="left">
                                <asp:Label runat="server" ID="lblSeller"></asp:Label>
                            </td>
                            <td width="15%" bgcolor="#DCEFF3" align="right">
                                简码：
                            </td>
                            <td width="35%" align="left">
                                <asp:Label runat="server" ID="lblBrevityCode"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td width="15%" bgcolor="#DCEFF3" align="right">
                                返利政策：
                            </td>
                            <td align="left" colspan="3">
                                <asp:Label runat="server" ID="lblRebatePolicy"></asp:Label>
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

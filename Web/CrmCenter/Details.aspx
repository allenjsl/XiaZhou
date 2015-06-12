<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Details.aspx.cs" Inherits="Web.CrmCenter.Details" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>客户资料查看</title>
    <link href="/css/style.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/js/jquery-1.4.4.js"></script>
    <script type="text/javascript" src="/js/jquery.blockUI.js"></script>

    <script type="text/javascript" src="/js/table-toolbar.js"></script>

    <link href="/css/boxynew.css" rel="stylesheet" type="text/css" />


    <script type="text/javascript" src="/js/bt.min.js"></script>

    <script type="text/javascript" src="/js/jquery.boxy.js"></script>

</head>
<body style="background: 0 none;">
    <div class="alertbox-outbox">
        <div style="margin: 0 auto; width: 99%;">
            <span class="formtableT formtableT02">基本信息</span>
            <table width="100%" align="center" cellpadding="0" cellspacing="0" bgcolor="#e9f4f9" style="margin: 0 auto">
                <tr>
                    <td width="10%" height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                        国家：
                    </td>
                    <td width="23%" align="left">
                        <asp:Label ID="lblCountry" runat="server" Text=""></asp:Label>
                    </td>
                    <td width="10%" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                        省份：
                    </td>
                    <td width="23%" align="left">
                        <asp:Label ID="lblProvince" runat="server" Text=""></asp:Label>
                    </td>
                    <td width="10%" align="right" bgcolor="#B7E0F3">
                        <span class="alertboxTableT">城市：</span>
                    </td>
                    <td align="left">
                        <asp:Label ID="lblCity" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                        县区：
                    </td>
                    <td align="left" bgcolor="#e0e9ef">
                        <asp:Label ID="lblCounty" runat="server" Text=""></asp:Label>
                    </td>
                    <td align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                        单位名称：
                    </td>
                    <td align="left" bgcolor="#e0e9ef">
                        <asp:Label ID="lblName" runat="server" Text=""></asp:Label>
                    </td>
                    <td align="right" bgcolor="#B7E0F3">
                        <span class="alertboxTableT">地址：</span>
                    </td>
                    <td align="left" bgcolor="#e0e9ef">
                        <asp:Label ID="lblAddress" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                        机构代码：
                    </td>
                    <td align="left">
                        <asp:Label ID="lblOrganizationCode" runat="server" Text=""></asp:Label>
                    </td>
                    <td align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                        法人：
                    </td>
                    <td align="left">
                        <asp:Label ID="lblLegalRepresentative" runat="server" Text=""></asp:Label>
                    </td>
                    <td align="right" bgcolor="#B7E0F3">
                        <span class="alertboxTableT">法人电话：</span>
                    </td>
                    <td align="left">
                        <asp:Label ID="lblLegalRepresentativePhone" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                        法人手机：
                    </td>
                    <td align="left" bgcolor="#e0e9ef">
                        <asp:Label ID="lblLegalRepresentativeMobile" runat="server" Text=""></asp:Label>
                    </td>
                    <td align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                        许可证号：
                    </td>
                    <td align="left" bgcolor="#e0e9ef" colspan="3">
                        <asp:Label ID="lblLicense" runat="server" Text=""></asp:Label>
                    </td>                    
                </tr>
                <%--<tr>
                    <td width="9%" height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                        返利政策：
                    </td>
                    <td colspan="5" align="left" bgcolor="#e0e9ef">
                        <asp:Label ID="lblRebatePolicy" runat="server" Text=""></asp:Label>
                    </td>
                </tr>--%>
                <tr>
                    <td height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                        责任销售：
                    </td>
                    <td align="left">
                        <asp:Label ID="lblSellerId" runat="server" Text=""></asp:Label>
                    </td>
                    <td align="right" bgcolor="#B7E0F3">
                        简码：
                    </td>
                    <td colspan="3" align="left">
                        <asp:Label ID="lblBrevityCode" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
        <div class="hr_10">
        </div>
        <div style="margin: 0 auto; width: 99%;">
            <span class="formtableT formtableT02">常用设置</span>
            <table width="100%" align="center" cellpadding="0" cellspacing="0" bgcolor="#e9f4f9">
                <tr>
                    <td width="10%" height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                        欠款额度：
                    </td>
                    <td width="23%" align="left">
                        <asp:Label ID="lblAmountOwed" runat="server" Text=""></asp:Label>
                    </td>
                    <td width="10%" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                        单团账龄期限：
                    </td>
                    <td align="left">
                        <asp:Label ID="lblDeadline" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                        是否签订协议：
                    </td>
                    <td align="left" bgcolor="#e0e9ef">
                        <asp:Label ID="lblIsSignContract" runat="server" Text=""></asp:Label>
                    </td>
                    <td align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                        协议文件上传：
                    </td>
                    <td align="left" bgcolor="#e0e9ef">
                        <a target="_blank" href="<%=filepath %>">
                            <%=filename %></a>
                    </td>
                </tr>
            </table>
            <div class="hr_5">
            </div>
            <table width="100%" align="center" cellpadding="0" cellspacing="0" bgcolor="#e9f4f9"
                style="margin: 0 auto">
                <td width="10%" align="right" bgcolor="#B7E0F3" style="height:28px;">
                    <span class="alertboxTableT">财务姓名：</span>
                </td>
                <td width="23%" align="left" bgcolor="#e0e9ef">
                    <asp:Label ID="lblFinancialName" runat="server" Text=""></asp:Label>
                </td>
                <td width="10%" height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    财务电话：
                </td>
                <td width="23%" align="left">
                    <asp:Label ID="lblFinancialPhone" runat="server" Text=""></asp:Label>
                </td>
                <td width="10%" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    财务手机：
                </td>
                <td colspan="3" align="left">
                    <asp:Label ID="lblFinancialMobile" runat="server" Text=""></asp:Label>
                </td>
            </table>
            <div class="hr_5">
            </div>
            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                <tr style="background: url(../../images/y-formykinfo.gif) repeat-x center top;">
                    <td width="43%" height="28" align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                        开户行
                    </td>
                    <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                        银行账号
                    </td>
                </tr>
                <asp:Repeater ID="rptBank" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td height="28" align="center" bgcolor="#FFFFFF">
                                <%# Eval("BankName")%>
                            </td>
                            <td align="center" bgcolor="#FFFFFF">
                                <%# Eval("BankAccount")%>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>  
                <asp:PlaceHolder runat="server" ID="phYinHangZhangHuEmpty" Visible="false">   
                <tr>
                    <td colspan="2" style="height:28px; text-align:center;">暂无银行账号信息</td>
                </tr>
                </asp:PlaceHolder>
            </table>
        </div>
        <div class="hr_10">
        </div>
        <div style="margin: 0 auto; width: 99%;">
            <span class="formtableT formtableT02">联系人</span>
            <table width="100%" border="0" cellspacing="0" cellpadding="0" style="padding-bottom: 3px;">
                <tr style="background: url(../../images/y-formykinfo.gif) repeat-x center top;">
                    <td align="center" bgcolor="#B7E0F3" class="alertboxTableT" style="width:30px;">
                        编号
                    </td>
                    <td height="23" align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                        部门
                    </td>
                    <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                        地址
                    </td>
                    <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                        联系人
                    </td>
                    <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                        电话
                    </td>
                    <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                        传真
                    </td>
                    <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                        手机
                    </td>
                    <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                        生日
                    </td>
                    <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                        QQ
                    </td>
                </tr>
                <asp:Repeater ID="rptLinkMan" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td align="center" bgcolor="#FFFFFF">
                                <%#(pageIndex - 1) * pageSize + (Container.ItemIndex + 1)%>
                            </td>
                            <td height="28" align="center" bgcolor="#FFFFFF">
                                <%# Eval("Department") %>
                            </td>
                            <td align="center" bgcolor="#FFFFFF">
                                <%# Eval("Address") %>
                            </td>
                            <td align="center" bgcolor="#FFFFFF">
                                <a href="javascript:void(0);" data-class="paopao">
                                    <%# Eval("Name") %></a><span style="display: none"><%# GetAccountList(Eval("UserId").ToString())%></span>
                            </td>
                            <td align="center" bgcolor="#FFFFFF">
                                <%# Eval("Telephone")%>
                            </td>
                            <td align="center" bgcolor="#FFFFFF">
                                <%# Eval("Fax") %>
                            </td>
                            <td align="center" bgcolor="#FFFFFF">
                                <%# Eval("MobilePhone")%>
                            </td>
                            <td align="center" bgcolor="#FFFFFF">
                                <%# EyouSoft.Common.UtilsCommons.SetDateTimeFormart((DateTime?)Eval("Birthday")) %>
                            </td>
                            <td align="center" bgcolor="#FFFFFF">
                                <%# Eval("QQ") %>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
        </div>
        <div class="alertbox-btn">
            <a href="javascript:void(0);" class="chongzhi" onclick="parent.Boxy.getIframeDialog('<%=Request.QueryString["iframeId"]%>').hide();return false;" hidefocus="true"><s></s>关 闭</a>
        </div>
    </div>

    <script type="text/javascript">
        var j = 1;
        $(function() {

            $("a[data-class='paopao']").bt({
                contentSelector: function() {
                    return $(this).next().html();
                },
                positions: ['bottom'],
                fill: '#FFF2B5',
                strokeStyle: '#D59228',
                noShadowOpts: { strokeStyle: "#D59228" },
                spikeLength: 5,
                spikeGirth: 15,
                width: 475,
                overlap: 0,
                centerPointY: 4,
                cornerRadius: 4,
                shadow: true,
                shadowColor: 'rgba(0,0,0,.5)',
                cssStyles: { color: '#00387E', 'line-height': '200%' }
            });
        });
    </script>

</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FinanceList.aspx.cs" Inherits="Web.GroupEnd.Suppliers.FinanceList" %>

<%@ Register Src="~/UserControl/Suppliers.ascx" TagName="Suppliers" TagPrefix="uc1" %>
<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>
<%@ Register Src="~/UserControl/CaiWuShaiXuan.ascx" TagName="CaiWuShaiXuan" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta content="text/html; charset=utf-8" http-equiv="Content-Type" />
    <title>供应商财务管理</title>
    <link type="text/css" rel="stylesheet" href="/Css/fx_style.css" />
    <link type="text/css" rel="stylesheet" href="/Css/boxynew.css" />
</head>
<body style="background: 0 none;">
    <uc1:Suppliers ID="Suppliers1" runat="server" FinanceClass="default cawuglicon" />
    <div class="list-main">
        <div class="list-maincontent">
            <div class="hr_10">
            </div>
            <div class="listsearch">
                <form id="frm" method="get" action="FinanceList.aspx">
                <input type="hidden" name="sl" value="<%=EyouSoft.Common.Utils.GetQueryStringValue("sl") %>" />
                团号：<input type="text" class="searchInput" name="txtTourCode" value='<%=EyouSoft.Common.Utils.GetQueryStringValue("txtTourCode") %>' />
                线路区域：
                <select id="ddlArea" name="ddlArea">
                    <%=EyouSoft.Common.UtilsCommons.GetSuppliersArea(EyouSoft.Common.Utils.GetQueryStringValue("ddlArea"),SiteUserInfo.SourceCompanyInfo.CompanyId) %>
                </select>
                计调员：
                <input type="text" class="searchInput size68" name="txtPlaner" value='<%=EyouSoft.Common.Utils.GetQueryStringValue("txtPlaner") %>' />
                出团时间：
                <input type="text" class="searchInput size68" name="txtBeginLDate" onfocus="WdatePicker()"
                    value='<%=EyouSoft.Common.Utils.GetQueryStringValue("txtBeginLDate") %>' />
                -
                <input type="text" class="searchInput size68" name="txtEndLDate" onfocus="WdatePicker()"
                    value='<%=EyouSoft.Common.Utils.GetQueryStringValue("txtEndLDate") %>' />
                未收金额：
                <uc2:CaiWuShaiXuan ID="CaiWuShaiXuan1" runat="server" />
                <a href="javascript:void(0)" id="btnSearch">
                    <img src="/Images/fx-images/searchbg.gif" /></a>
                </form>
            </div>
            <div class="listtablebox">
                <table width="100%" cellspacing="0" cellpadding="0"  border="0"
                    id="liststyle">
                    <tbody>
                        <tr class="odd">
                            <th align="center">
                                编号
                            </th>
                            <th align="center">
                                团号
                            </th>
                            <th align="left">
                                线路名称
                            </th>
                            <th align="center">
                                出团时间
                            </th>
                            <th align="center">
                                人数
                            </th>
                            <th align="center">
                                团队状态
                            </th>
                            <th align="center">
                                计调员
                            </th>
                            <th align="right">
                                应收金额
                            </th>
                            <th align="right">
                                已收
                            </th>
                            <th align="right">
                                未收
                            </th>
                        </tr>
                        <asp:Repeater runat="server" ID="RpFinance">
                            <ItemTemplate>
                                <tr class='<%#Container.ItemIndex%2==0?"odd":"" %>'>
                                    <td align="center">
                                        <%#Container.ItemIndex+1+(pageIndex-1)*pageSize %>
                                    </td>
                                    <td align="center">
                                        <%#Eval("TourCode")%>
                                    </td>
                                    <td align="left">
                                        <a class="lineInfo" href="javascript:void(0)" bt-xtitle="<%#Eval("Salesman") %>_<%#Eval("Tel")%>_<%#Eval("Mobile")%>">
                                            <%#Eval("RouteName") %></a>
                                    </td>
                                    <td align="center">
                                        <%#EyouSoft.Common.UtilsCommons.GetDateString(Eval("LDate"),this.ProviderToDate) %>
                                    </td>
                                    <td align="center">
                                        <b>
                                            <%#Eval("Num")%></b>
                                    </td>
                                    <td align="center">
                                        <%#Eval("TourStatus")%>
                                    </td>
                                    <td align="center">
                                        <%#Eval("Planer")%>
                                    </td>
                                    <td align="right">
                                        <strong class="fontbsize12">
                                            <%#EyouSoft.Common.UtilsCommons.GetMoneyString( Eval("Payable"),this.ProviderToMoney)%></strong>
                                    </td>
                                    <td align="right">
                                        <strong class="font-green">
                                            <%#EyouSoft.Common.UtilsCommons.GetMoneyString(Eval("Paid"),this.ProviderToMoney) %>
                                        </strong>
                                    </td>
                                    <td align="right">
                                        <strong class="fontblue">
                                            <%#EyouSoft.Common.UtilsCommons.GetMoneyString(Eval("Unpaid"), this.ProviderToMoney)%></strong>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                        <asp:PlaceHolder ID="PhSumPage" runat="server">
                            <tr class="">
                                <td align="right" colspan="7">
                                    <strong>合计：</strong>
                                </td>
                                <td align="right">
                                    <strong class="fontbsize12">
                                        <asp:Literal ID="LtTotalPayable" runat="server"></asp:Literal></strong>
                                </td>
                                <td align="right">
                                    <strong class="font-green">
                                        <asp:Literal ID="LtTotalPaid" runat="server"></asp:Literal></strong>
                                </td>
                                <td align="right">
                                    <strong class="fontblue">
                                        <asp:Literal ID="LtTotalUnpaid" runat="server"></asp:Literal></strong>
                                </td>
                            </tr>
                            <tr class="odd">
                                <td bgcolor="#f4f4f4" align="center" colspan="18">
                                    <div class="pages">
                                        <cc1:ExporPageInfoSelect ID="ExporPageInfoSelect1" runat="server" />
                                    </div>
                                </td>
                            </tr>
                        </asp:PlaceHolder>
                        <asp:Literal ID="litMsg" Visible="false" runat="server" Text="<tr><td align='center' colspan='18'>暂无记录!</td></tr>"></asp:Literal>
                    </tbody>
                </table>
            </div>
            <div class="hr_10">
            </div>
        </div>
    </div>

    <script src="/Js/jquery-1.4.4.js" type="text/javascript"></script>

    <!--[if IE]><script src="/js/excanvas.js" type="text/javascript" charset="utf-8"></script><![endif]-->

    <script src="/Js/bt.min.js" type="text/javascript"></script>

    <script src="/Js/jquery.boxy.js" type="text/javascript"></script>

    <!--[if lte IE 6]><script src="/js/jquery.bgiframe.min.js" type="text/javascript"></script><![endif]-->

    <script src="/Js/jquery.blockUI.js" type="text/javascript"></script>

    <script src="/Js/table-toolbar.js" type="text/javascript"></script>

    <script src="/Js/datepicker/WdatePicker.js" type="text/javascript"></script>

    <script type="text/javascript">
        $(function() {
            var caiWuShaiXuan = new wuc.caiWuShaiXuan(window['<%=CaiWuShaiXuan1.ClientID%>']);
            caiWuShaiXuan.setOperator('<%=EyouSoft.Common.Utils.GetQueryStringValue(CaiWuShaiXuan1.ClientUniqueIDOperator) %>');
            caiWuShaiXuan.setOperatorNumber('<%=EyouSoft.Common.Utils.GetQueryStringValue(CaiWuShaiXuan1.ClientUniqueIDOperatorNumber) %>');

            //Enter搜索
            $("#frm").find(":text").keypress(function(e) {
                if (e.keyCode == 13) {
                    $("#frm").submit();
                    return false;
                }
            });
            
            $("#btnSearch").click(function() {
                $("#frm").submit();
            });


            //浮动
            $('.lineInfo').bt({
                contentSelector: function() {
                    var title = $(this).attr("bt-xtitle");
                    if (title) {
                        var info = title.split('_');
                        return "销售员："+info[0]+"<br />电话："+info[1]+"<br />手机："+info[2]+"";
                    }
                    
                },
                positions: ['left', 'right', 'bottom'],
                fill: '#FFF2B5',
                strokeStyle: '#D59228',
                noShadowOpts: { strokeStyle: "#D59228" },
                spikeLength: 10,
                spikeGirth: 15,
                width: 170,
                overlap: 0,
                centerPointY: 1,
                cornerRadius: 4,
                shadow: true,
                shadowColor: 'rgba(0,0,0,.5)',
                cssStyles: { color: '#00387E', 'line-height': '180%' }
            });
        });
    </script>

</body>
</html>

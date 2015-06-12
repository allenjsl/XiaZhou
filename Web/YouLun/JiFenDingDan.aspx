<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="JiFenDingDan.aspx.cs" Inherits="EyouSoft.Web.YouLun.JiFenDingDan"
    MasterPageFile="~/MasterPage/Front.Master" %>

<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>
<%@ Register Src="~/YouLun/WUC/wuc4.ascx" TagName="wuc4" TagPrefix="uc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="mainbox">
        <uc1:wuc4 runat="server" ID="wuc4" />
        <div class="searchbox fixed">
            <form id="form1" method="get">
            <input type="hidden" name="sl" value='<%= SL %>' />
            <span class="searchT">
                <p>
                    商品名称：<input type="text" size="30" class="inputtext formsize120" name="txtShangPinMingCheng"
                        value="<%=Request.QueryString["txtShangPinMingCheng"] %>" />
                    订单号：<input type="text" size="30" class="inputtext formsize120" name="txtJiaoYiHao"
                        value="<%=Request.QueryString["txtJiaoYiHao"] %>" />
                    下单时间：<input type="text" onfocus="WdatePicker()" class="inputtext formsize100" name="txtXiaDanShiJian1"
                        value="<%=Request.QueryString["txtXiaDanShiJian1"] %>" />-<input type="text" onfocus="WdatePicker()"
                            class="inputtext formsize100" name="txtXiaDanShiJian2" value="<%=Request.QueryString["txtXiaDanShiJian2"] %>" />
                    <input type="submit" value="搜索" class="search-btn" />
                </p>
            </span>
            </form>
        </div>
        <div class="tablehead" id="select_Toolbar_Paging_1">
            <ul class="fixed">
                
            </ul>
            <div class="pages">
                <cc1:ExporPageInfoSelect ID="paging" runat="server" />
            </div>
        </div>
        <!--列表表格-->
        <div class="tablelist-box">
            <table width="100%" id="liststyle">
                <tr class="odd">
                    <th class="thinputbg">
                        <input type="checkbox" id="checkbox" name="checkbox" />
                    </th>
                    <th class="th-line" style="text-align: left;">
                        订单号
                    </th>
                    <th class="th-line" style="text-align: left;">
                        商品名称
                    </th>
                    <th class="th-line" style="text-align: left;">
                        会员姓名
                    </th>
                    <th class="th-line" style="text-align: left;">
                        兑换数量
                    </th>
                    <th class="th-line" style="text-align: left;">
                        兑换方式
                    </th>
                    <th class="th-line" style="text-align: left;">
                        兑换金额
                    </th>
                    <th class="th-line" style="text-align: left;">
                        订单状态
                    </th>
                    <th class="th-line" style="text-align: left;">
                        付款状态
                    </th>
                    <th class="th-line" style="text-align: left;">
                        下单时间
                    </th>
                    <th class="th-line" style="text-align: left; width: 100px;">
                        操作
                    </th>
                </tr>
                <asp:Repeater ID="rpt" runat="server">
                    <ItemTemplate>
                        <tr class="odd" i_dingdanid="<%#Eval("DingDanId") %>">
                            <td class="thinputbg" style="text-align: center;">
                                <input type="checkbox" id="checkbox" name="checkbox" value='<%# Eval("DingDanId") %>'>
                            </td>
                            <td class="th-line">
                                <%#Eval("JiaoYiHao")%>
                            </td>
                            <td class="th-line">
                                <%#Eval("ShangPinMingCheng") %>
                            </td>
                            <td class="th-line">
                                <%#Eval("HuiYuanXingMing") %>
                            </td>
                            <td class="th-line">
                                <%#Eval("ShuLiang") %>
                            </td>
                            <td class="th-line">
                                <%#GetDuiHuanFangShi(Eval("FangShi"))%>
                            </td>
                            <td class="th-line">
                                <%#GetJinE(Eval("FangShi"),Eval("JiFen"),Eval("JinE"))%>
                            </td>
                            <td class="th-line">
                                <%#Eval("DingDanStatus") %>
                            </td>
                            <td class="th-line">
                                <%#Eval("FuKuanStatus") %>
                            </td>
                            <td class="th-line">
                                <%#Eval("IssueTime","{0:yyyy-MM-dd HH:mm}") %>
                            </td>
                            <td class="th-line">
                                <a href="javascript:void(0)" class="i_chakan">查看</a>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
                <asp:PlaceHolder runat="server" ID="phHeJi"></asp:PlaceHolder>
                <asp:PlaceHolder runat="server" ID="phEmpty" Visible="false">
                    <tr>
                        <td colspan="50" align="center">
                            暂无数据。
                        </td>
                    </tr>
                </asp:PlaceHolder>
            </table>
        </div>
        <!--列表结束-->
        <div class="tablehead" style="border-top: 0 none;" id="select_Toolbar_Paging_2">
        </div>
    </div>

    <script src="/js/datepicker/wdatepicker.js" type="text/javascript"></script>
    <script type="text/javascript">
        var iPage = {
            reload: function() {
                window.location.href = window.location.href;
            },
            chaKan: function(obj) {
                var _$tr = $(obj).closest("tr");
                Boxy.iframeDialog({ iframeUrl: "JiFenDingDanEdit.aspx?sl=<%=SL %>&editid=" + _$tr.attr("i_dingdanid"), title: "查看积分订单信息", modal: true, width: "780px", height: "500px", afterHide: function() { iPage.reload(); } });
            }
        };

    
        $(document).ready(function() {
            wuc4(1);
            tableToolbar.init();
            //clone toolbar、paging
            $("#select_Toolbar_Paging_1").children().clone(true).prependTo("#select_Toolbar_Paging_2");
            $(".i_chakan").bind("click", function() { iPage.chaKan(this); });
        });
    </script>

</asp:Content>

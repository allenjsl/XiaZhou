<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Front.Master" AutoEventWireup="true"
    CodeBehind="DingDanHaiYang.aspx.cs" Inherits="EyouSoft.Web.YouLun.DingDanHaiYang" %>

<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script src="/Js/datepicker/WdatePicker.js" type="text/javascript"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="mainbox">
        <div style="background: none #f6f6f6;" class="tablehead">
            <ul class="fixed">
                <li><s class="orderformicon"></s><a class="ztorderform " id="cjyl" hidefocus="true"
                    href="DingDan.aspx?sl=98"><span>长江游轮</span></a></li>
                <li><s class="orderformicon"></s><a class="ztorderform de-ztorderform" id="hyyl"
                    hidefocus="true" href="DingDanHaiYang.aspx?sl=98"><span>海洋游轮</span></a></li>
                <li><s class="orderformicon"></s><a class="ztorderform " id="tgdd" hidefocus="true"
                    href="DingDanTuanGou.aspx?sl=98"><span>团购订单</span></a></li>
            </ul>
        </div>
        <div class="searchbox fixed">
            <form id="form1" method="get">
            <input type="hidden" name="sl" value='<%= SL %>' />
            <input type="hidden" name="t" value="<%=Request.QueryString["t"] %>" />
            <span class="searchT">
                <p>
                    订单号<input type="text" size="30" class="inputtext formsize80" name="dingdanhao" value="<%=Request.QueryString["dingdanhao"] %>" />
                    下单时间<input type="text" size="30" class="inputtext formsize80" name="sxiadan" value="<%=Request.QueryString["sxiadan"] %>"
                        onfocus="WdatePicker()" />-<input type="text" size="30" class="inputtext formsize80"
                            name="exiadan" value="<%=Request.QueryString["exiadan"] %>" onfocus="WdatePicker()" />
                    订单状态<select name="dingdanzhuangtai" class="inputselect" style="width: 120px;">
                        <option value="">-请选择-</option>
                        <%=EyouSoft.Common.UtilsCommons.GetEnumDDL(EyouSoft.Common.EnumObj.GetList(typeof(EyouSoft.Model.EnumType.YlStructure.HangQiDingDanStatus)), EyouSoft.Common.Utils.GetQueryStringValue("dingdanzhuangtai"))%>
                    </select>
                    付款状态<select name="fukuanzhuangtai" class="inputselect" style="width: 120px;">
                        <option value="">-请选择-</option>
                        <%=EyouSoft.Common.UtilsCommons.GetEnumDDL(EyouSoft.Common.EnumObj.GetList(typeof(EyouSoft.Model.EnumType.YlStructure.FuKuanStatus)), EyouSoft.Common.Utils.GetQueryStringValue("fukuanzhuangtai"))%>
                    </select>
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
                        产品名称
                    </th>
                    <th class="th-line" style="text-align: left;">
                        订单号
                    </th>
                    <th class="th-line" style="text-align: left;">
                        供应商
                    </th>
                    <th class="th-line" style="text-align: left;">
                        游轮公司
                    </th>
                    <th class="th-line" style="text-align: left;">
                        下单人
                    </th>
                    <th class="th-line" style="text-align: left;">
                        人数
                    </th>
                    <th class="th-line" style="text-align: left;">
                        订单金额
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
                    <th class="th-line" style="text-align: left;">
                        操作
                    </th>
                </tr>
                <asp:Repeater ID="rpt" runat="server">
                    <ItemTemplate>
                        <tr class="odd">
                            <td class="thinputbg" style="text-align: center;">
                                <input type="checkbox" id="checkbox" name="checkbox">
                            </td>
                            <td class="th-line">
                                <%#Eval("MingCheng")%>
                            </td>
                            <td class="th-line">
                                <%#Eval("JiaoYiHao")%>
                            </td>
                            <td class="th-line">
                                <%#Eval("GysName")%>
                            </td>
                            <td class="th-line">
                                <%#Eval("GongSiName")%>
                            </td>
                            <td class="th-line">
                                <%#Eval("YuDingRenName")%>
                            </td>
                            <td class="th-line">
                                <%#Eval("RenShu")%>
                            </td>
                            <td class="th-line">
                                <%#Eval("JinE","{0:C2}")%>
                            </td>
                            <td class="th-line">
                                <%#Eval("DingDanStatus")%>
                            </td>
                            <td class="th-line">
                                <%#Eval("FuKuanStatus")%>
                            </td>
                            <td class="th-line">
                                <%#Eval("IssueTime","{0:yyyy-MM-dd}")%>
                            </td>
                            <td class="th-line" data-id="<%# Eval("DingDanId") %>">
                                <a class="chkInfo" href="javascript:;">[查看]</a>
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
        var pageDataJs = {
            CreateBoxy: function(data) {
                Boxy.iframeDialog({
                    iframeUrl: data.url,
                    title: data.title,
                    modal: true,
                    width: data.width,
                    height: data.height
                });
            }
        };
        $(document).ready(function() {
            $(".chkInfo").click(function() {
            var id = $(this).closest("td").attr("data-id");
            var data = { url: 'DingDanChaKan.aspx?sl=98&id=' + id, title: '查看订单', width: '900px', height: '600px' }
            pageDataJs.CreateBoxy(data);
            })
        });
    </script>

</asp:Content>

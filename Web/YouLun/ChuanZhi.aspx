<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Front.Master" AutoEventWireup="true"
    CodeBehind="ChuanZhi.aspx.cs" Inherits="EyouSoft.Web.YouLun.ChuanZhi" %>

<%@ Register Assembly="ControlLibrary" Namespace="ControlLibrary" TagPrefix="cc2" %>
<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="mainbox">
        <div style="background: none #f6f6f6;" class="tablehead">
            <ul class="fixed">
                <li><s class="orderformicon"></s><a class="ztorderform " hidefocus="true"
                    href="ZiYuan.aspx?sl=96"><span>游轮公司管理</span></a></li>
                <li><s class="orderformicon"></s><a class="ztorderform " hidefocus="true"
                    href="xilie.aspx?sl=96"><span>系列管理</span></a></li>
                <li><s class="orderformicon"></s><a class="ztorderform de-ztorderform" hidefocus="true"
                    href="ChuanZhi.aspx?sl=96"><span>船只管理</span></a></li>
                <li><s class="orderformicon"></s><a class="ztorderform " hidefocus="true"
                    href="shipin.aspx?sl=96"><span>视频管理</span></a></li>
            </ul>
        </div>
        <form id="form2" method="get">
        <div class="searchbox fixed">
            <span class="searchT">
                <p>
                    <input type="hidden" name="sl" value="<%=EyouSoft.Common.Utils.GetQueryStringValue("sl") %>" />
                    游轮公司：<input type="text" class=" inputtext formsize120" name="name" id="txtName" value="<%=EyouSoft.Common.Utils.GetQueryStringValue("name") %>" />
                    系列名称：<input type="text" class=" inputtext formsize120" name="xilie" id="txtXL" value="<%=EyouSoft.Common.Utils.GetQueryStringValue("xilie") %>" />
                    船只名称：<input type="text" class=" inputtext formsize120" name="chuanzhi" id="chuanzhi"
                        value="<%=EyouSoft.Common.Utils.GetQueryStringValue("chuanzhi") %>" />
                    <input type="submit" id="Submit1" value="搜索" class="search-btn" />
                </p>
            </span>
        </div>
        </form>
        <div class="tablehead" id="temptablehead">
            <div id="caozuo" runat="Server">
                <ul class="fixed">
                    <li><s class="addicon"></s><a class="toolbar_add" hidefocus="true" href="javascript:">
                        <span>添加</span></a></li>
                </ul>
            </div>
            <div class="pages">
                <cc1:ExporPageInfoSelect ID="ExporPageInfoSelect1" runat="server" />
            </div>
        </div>
        <!--列表表格-->
        <div class="tablelist-box">
            <table width="100%" id="liststyle">
                <tbody>
                    <tr class="odd">
                        <th width=" " align="center" class="th-line">
                            序号
                        </th>
                        <th width="" align="center" class="th-line">
                            公司名称
                        </th>
                        <th width="" align="center" class="th-line">
                            公司类型
                        </th>
                        <th width="" align="center" class="th-line">
                            系列名称
                        </th>
                        <th width="" align="center" class="th-line">
                            船只名称
                        </th>
                        <th width="" align="center" class="th-line">
                            船只设施
                        </th>
                        <th width="" align="center" class="th-line">
                            操作
                        </th>
                    </tr>
                    <cc2:CustomRepeater ID="repList" runat="server">
                        <ItemTemplate>
                            <tr class="<%#Container.ItemIndex%2==0?"":"#odd" %>">
                                <td align="center">
                                    <%#Container.ItemIndex+1%>
                                </td>
                                <td align="center">
                                    <%#Eval("GongSiMingCheng")%>
                                </td>
                                <td align="center">
                                    <%# Eval("GongSiLeiXing")%>
                                </td>
                                <td align="center">
                                    <%# Eval("XiLieMingCheng")%>
                                </td>
                                <td align="center">
                                    <%# Eval("MingCheng")%>
                                </td>
                                <td align="center">
                                    <a class="sheshi" href="javascript:;">[设施]</a> <a class="fangxing" href="javascript:;" data-type="<%# (int)Eval("GongSiLeiXing")%>">
                                        [房型]</a> <a class="meishi" href="javascript:;">[美食]</a> <a class="baseprice" href="javascript:;" data-type="<%# (int)Eval("GongSiLeiXing")%>">[基础价]</a>
                                        <a class="fangxingfujian" href="javascript:;">[房型附件]</a><a class="meishifujian" href="javascript:;">[美食附件]</a><a class="sheshifujian" href="javascript:;">[设施附件]</a>
                                    <input class="hiddenid" type="hidden" value="<%#Eval("ChuanZhiId") %>" />
                                </td>
                                <td align="center">
                                    <a class="update" href="javascript:;">修改</a> | <a class="del" href="javascript:;">删除</a>
                                    <input class="hiddenid" type="hidden" value="<%#Eval("ChuanZhiId") %>" />
                                </td>
                            </tr>
                        </ItemTemplate>
                    </cc2:CustomRepeater>
                </tbody>
            </table>
        </div>
        <!--列表结束-->
        <div class="tablehead" style="border: none;">

            <script type="text/javascript">
                document.write(document.getElementById("temptablehead").innerHTML);
            </script>

        </div>
    </div>

    <script type="text/javascript">
        var pageDataJs = {
            Params: { type: '<%= EyouSoft.Common.Utils.GetQueryStringValue("type") %>', sl: '<%= EyouSoft.Common.Utils.GetQueryStringValue("sl") %>' }
        }
        $(function() {
            $(".update").click(function() {
                var url = '/YouLun/ChuanZhiAdd.aspx?id=' + $(this).closest("td").find(".hiddenid").val() + "&" + $.param(pageDataJs.Params)
                Boxy.iframeDialog({
                    iframeUrl: url,
                    title: "修改",
                    modal: true,
                    width: "950",
                    height: "550"
                });
            })//修改
            $(".toolbar_add").click(function() {
                var url = '/YouLun/ChuanZhiAdd.aspx?' + $.param(pageDataJs.Params)
                Boxy.iframeDialog({
                    iframeUrl: url,
                    title: "添加",
                    modal: true,
                    width: "950",
                    height: "550"
                });
            })//添加
            $(".del").click(function() {
                var url = '/YouLun/ChuanZhi.aspx?del=1&sl=96&id=' + $(this).closest("td").find(".hiddenid").val()
                $.newAjax({
                    url: url,
                    dataType: "json",
                    type: "post",
                    success: function(ret) {
                        if (ret.result == "1") {
                            parent.tableToolbar._showMsg(ret.msg, function() { parent.location.href = parent.location.href; });
                        }
                        else {
                            parent.tableToolbar._showMsg(tableToolbar.errorMsg);
                        }
                    },
                    error: function() {
                        parent.tableToolbar._showMsg(tableToolbar.errorMsg);
                    }
                });
            })//删除
            $(".meishi").click(function() {
                var url = '/YouLun/ChuanZhiMeiShi.aspx?id=' + $(this).closest("td").find(".hiddenid").val() + "&" + $.param(pageDataJs.Params);
                Boxy.iframeDialog({
                    iframeUrl: url,
                    title: "美食详情",
                    modal: true,
                    width: "950",
                    height: "700"
                });
            })//房型附件
            $(".fangxingfujian").click(function() {
                var url = '/YouLun/ChuanZhiFangXingFuJian.aspx?id=' + $(this).closest("td").find(".hiddenid").val() + "&" + $.param(pageDataJs.Params);
                Boxy.iframeDialog({
                    iframeUrl: url,
                    title: "房型附件",
                    modal: true,
                    width: "950",
                    height: "700"
                });
            })//美食附件
            $(".meishifujian").click(function() {
                var url = '/YouLun/ChuanZhiFangXingFuJian.aspx?t=1&id=' + $(this).closest("td").find(".hiddenid").val() + "&" + $.param(pageDataJs.Params);
                Boxy.iframeDialog({
                    iframeUrl: url,
                    title: "美食附件",
                    modal: true,
                    width: "950",
                    height: "700"
                });
            })//设施附件
            $(".sheshifujian").click(function() {
                var url = '/YouLun/ChuanZhiFangXingFuJian.aspx?t=2&id=' + $(this).closest("td").find(".hiddenid").val() + "&" + $.param(pageDataJs.Params);
                Boxy.iframeDialog({
                    iframeUrl: url,
                    title: "设施附件",
                    modal: true,
                    width: "950",
                    height: "700"
                });
            })//美食
            $(".baseprice").click(function() {
                var url = '/YouLun/ChuanZhiBasePrice.aspx?id=' + $(this).closest("td").find(".hiddenid").val() + "&leixing=" + $(this).attr("data-type") + "&" + $.param(pageDataJs.Params);
                Boxy.iframeDialog({
                    iframeUrl: url,
                    title: "基础价设置",
                    modal: true,
                    width: "800",
                    height: "800"
                });
            })//基础价
            $(".sheshi").click(function() {
                var url = '/YouLun/ChuanZhiSheShi.aspx?id=' + $(this).closest("td").find(".hiddenid").val() + "&" + $.param(pageDataJs.Params);
                Boxy.iframeDialog({
                    iframeUrl: url,
                    title: "设施详情",
                    modal: true,
                    width: "950",
                    height: "700"
                });
            })//设施
            $(".fangxing").click(function() {
                var url = '/YouLun/ChuanZhiFangXing.aspx?id=' + $(this).closest("td").find(".hiddenid").val() + "&leixing=" + $(this).attr("data-type") + "&" + $.param(pageDataJs.Params);
                Boxy.iframeDialog({
                    iframeUrl: url,
                    title: "房型详情",
                    modal: true,
                    width: "950",
                    height: "700"
                });
            })//房型
        });
    </script>

</asp:Content>

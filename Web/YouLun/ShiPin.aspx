<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Front.Master" AutoEventWireup="true" CodeBehind="ShiPin.aspx.cs" Inherits="EyouSoft.Web.YouLun.ShiPin" %>

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
                <li><s class="orderformicon"></s><a class="ztorderform " hidefocus="true"
                    href="chuanzhi.aspx?sl=96"><span>船只管理</span></a></li>
                <li><s class="orderformicon"></s><a class="ztorderform de-ztorderform" hidefocus="true"
                    href="shipin.aspx?sl=96"><span>视频管理</span></a></li>
            </ul>
        </div>
        <form id="form2" method="get">
        <div class="searchbox fixed">
            <span class="searchT">
                <p>
                    <input type="hidden" name="sl" value="<%=EyouSoft.Common.Utils.GetQueryStringValue("sl") %>" />
                    游轮公司：<input type="text" class=" inputtext formsize120" name="gongsiname" id="txtName"
                        value="<%=EyouSoft.Common.Utils.GetQueryStringValue("gongsiname") %>" />
                    系列名称：<input type="text" class=" inputtext formsize120" name="xiliename" id="xilie" value="<%=EyouSoft.Common.Utils.GetQueryStringValue("xiliename") %>" />
                    船只名称：<input type="text" class=" inputtext formsize120" name="chuanzhiname" id="chuanzhiname"
                        value="<%=EyouSoft.Common.Utils.GetQueryStringValue("chuanzhiname") %>" />
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
                            游轮公司
                        </th>
                        <th width="" align="center" class="th-line">
                            视频名称
                        </th>
                        <th width="" align="center" class="th-line">
                            视频地址
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
                                    <%# Eval("MingCheng")%>
                                </td>
                                <td align="center">
                                      <%# Eval("Filepath")%>
                                </td>
                                <td align="center">
                                    <a class="update" href="javascript:;">修改</a> | <a class="del" href="javascript:;">删除</a>
                                    <input class="hiddenid" type="hidden" value="<%#Eval("ShiPinId") %>" />
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
                var url = '/YouLun/ShiPinAdd.aspx?id=' + $(this).closest("td").find(".hiddenid").val() + "&" + $.param(pageDataJs.Params)
                Boxy.iframeDialog({
                    iframeUrl: url,
                    title: "修改",
                    modal: true,
                    width: "600",
                    height: "350"
                });
            })//修改
            $(".toolbar_add").click(function() {
                var url = '/YouLun/ShiPinAdd.aspx?' + $.param(pageDataJs.Params)
                Boxy.iframeDialog({
                    iframeUrl: url,
                    title: "添加",
                    modal: true,
                    width: "600",
                    height: "350"
                });
            })//添加
            $(".del").click(function() {
            var url = '/YouLun/ShiPin.aspx?del=1&sl=96&id=' + $(this).closest("td").find(".hiddenid").val()
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
        });
    </script>

</asp:Content>
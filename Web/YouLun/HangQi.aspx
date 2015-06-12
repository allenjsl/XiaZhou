<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Front.Master" AutoEventWireup="true"
    CodeBehind="HangQi.aspx.cs" Inherits="EyouSoft.Web.YouLun.HangQi" %>

<%@ Register Assembly="ControlLibrary" Namespace="ControlLibrary" TagPrefix="cc2" %>
<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script src="/Js/datepicker/WdatePicker.js" type="text/javascript"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="mainbox">
        <div style="background: none #f6f6f6;" class="tablehead">
            <ul class="fixed">
                <li><s class="orderformicon"></s><a id="i_a_cjyl" class="ztorderform" hidefocus="true"
                    href="HangQi.aspx?sl=97"><span>长江游轮</span></a></li>
                <li><s class="orderformicon"></s><a id="i_a_hyyl" class="ztorderform" hidefocus="true"
                    href="HangQi.aspx?sl=97&t=1"><span>海洋游轮</span></a></li>
            </ul>
        </div>
        <form id="form2" method="get">
        <input type="hidden" name="sl" value="<%=EyouSoft.Common.Utils.GetQueryStringValue("sl") %>" />
        <input type="hidden" name="t" value="<%=EyouSoft.Common.Utils.GetQueryStringValue("t") %>" />
        <div class="searchbox fixed">
            <span class="searchT">
                <p>
                    产品编号：<input type="text" class=" inputtext formsize80" name="bianhao" id="bianhao"
                        value="<%=EyouSoft.Common.Utils.GetQueryStringValue("bianhao") %>" />
                    产品名称：<input type="text" class=" inputtext formsize80" name="mingcheng" id="mingcheng"
                        value="<%=EyouSoft.Common.Utils.GetQueryStringValue("mingcheng") %>" />
                    出港日期：
                    <input type="text" class=" inputtext formsize80" name="riqi1" id="riqi1" value="<%=EyouSoft.Common.Utils.GetQueryStringValue("riqi1") %>"
                        onfocus="WdatePicker()" />-<input type="text" class=" inputtext formsize80" name="riqi2"
                            id="riqi2" value="<%=EyouSoft.Common.Utils.GetQueryStringValue("riqi2") %>" onfocus="WdatePicker()" />
                    供应商：
                    <input type="text" class=" inputtext formsize80" name="gongyingshang" id="gongyingshang"
                        value="<%=EyouSoft.Common.Utils.GetQueryStringValue("gongyingshang") %>" />
                    游轮公司：
                    <input type="text" class=" inputtext formsize80" name="youlun" id="youlun" value="<%=EyouSoft.Common.Utils.GetQueryStringValue("youlun") %>" />
                    游轮系列：
                    <%--<input type="text" class=" inputtext formsize80" name="xilie" id="xilie" value="<%=EyouSoft.Common.Utils.GetQueryStringValue("xilie") %>" />--%>
                    <select class="inputselect" name="txtxilieid" id="txtxilieid">
                        <option value="" >请选择</option>
                        <asp:Literal runat="server" ID="ltrXiLie"></asp:Literal>
                    </select><br />
                    游轮船只：<input type="text" class=" inputtext formsize80" name="chuanzhi" id="chuanzhi"
                        value="<%=EyouSoft.Common.Utils.GetQueryStringValue("chuanzhi") %>" />
                    <input type="submit" id="Submit1" value="搜索" class="search-btn" />
                </p>
            </span>
        </div>
        </form>
        <div class="tablehead" id="temptablehead">
            <div id="caozuo" runat="Server">
                <ul class="fixed">
                    <li><s class="addicon"></s><a class="toolbar_add" hidefocus="true" href="HangQiAdd.aspx?sl=<%=sl %>&t=<% =EyouSoft.Common.Utils.GetQueryStringValue("t")%>"">
                        <span>新增</span></a></li>
                    <li><s class="updateicon"></s><a class="toolbar_update" hidefocus="true" href="javascript:">
                        <span>修改</span></a></li>
                    <li><s class="delicon"></s><a class="toolbar_delete" hidefocus="true" href="javascript:">
                        <span>删除</span></a></li>
                    <li><s class="remenicon"></s><a class="toolbar_remen" hidefocus="true" href="javascript:">
                        <span>热门</span></a></li>
                    <li><s class="tejiaicon"></s><a class="toolbar_tejia" hidefocus="true" href="javascript:">
                        <span>特价</span></a></li>
                    <li><s class="rementjicon"></s><a class="toolbar_rementuijian" hidefocus="true" href="javascript:">
                        <span>热门推荐</span></a></li>
                    <li><s class="tejiatjicon"></s><a class="toolbar_tejiatuijian" hidefocus="true" href="javascript:">
                        <span>特价推荐</span></a></li>
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
                            <input class="lischk" name="checkbox" id="checkbox1" type="checkbox" disabled="true" />
                        </th>
                        <th width=" " align="center" class="th-line">
                            产品编号
                        </th>
                        <th width=" " align="center" class="th-line">
                            产品名称
                        </th>
                        <th width=" " align="center" class="th-line">
                            供应商
                        </th>
                        <th width=" " align="center" class="th-line">
                            游轮公司
                        </th>
                        <th width=" " align="center" class="th-line">
                            游轮系列
                        </th>
                        <th width=" " align="center" class="th-line">
                            游轮船只
                        </th>
                        <th width=" " align="center" class="th-line">
                            航线
                        </th>
                        <th width=" " align="center" class="th-line">
                            出发港口
                        </th>
                        <th width=" " align="center" class="th-line">
                            抵达港口
                        </th>
                        <th width=" " align="center" class="th-line">
                            航期
                        </th>
                        <th width=" " align="center" class="th-line">
                            天数
                        </th>
                        <th width=" " align="center" class="th-line">
                            预控数量
                        </th>
                        <th width=" " align="center" class="th-line">
                            剩余数量
                        </th>
                        <th width=" " align="center" class="th-line">
                            操作
                        </th>
                    </tr>
                    <cc2:CustomRepeater ID="repList" runat="server">
                        <ItemTemplate>
                            <tr class="<%#Container.ItemIndex%2==0?"":"#odd" %>">
                                <td align="center">
                                    <input name="checkbox" id="checkbox" value="<%#Eval("HangQiId") %>" type="checkbox" />
                                </td>
                                <td align="center">
                                    <%#Eval("BianHao")%>
                                </td>
                                <td align="center"><%#Eval("MingCheng")%><%# BiaoQianInfo((System.Collections.Generic.IList<EyouSoft.Model.YlStructure.MHangQiBiaoQianInfo>)Eval("BiaoQians"))%></span>
                                </td>
                                <td align="center">
                                    <%#Eval("GysName")%>
                                </td>
                                <td align="center">
                                    <%#Eval("GongSiName")%>
                                </td>
                                <td align="center">
                                    <%#Eval("XiLieName")%>
                                </td>
                                <td align="center">
                                    <%#Eval("ChuanZhiName")%>
                                </td>
                                <td align="center">
                                    <%#Eval("HangXianMingCheng")%>
                                </td>
                                <td align="center">
                                    <%#Eval("ChuFaGangKouMingCheng")%>
                                </td>
                                <td align="center">
                                    <%#Eval("DiDaGangKouMingCheng")%>
                                </td>
                                <td align="center">
                                    <%#Eval("YouXiaoRiQi","{0:yyyy-M-d}")%>
                                </td>
                                <td align="center">
                                    <%#Eval("TianShu1")%>天<%#Eval("TianShu2")%>晚
                                </td>
                                <td align="center">
                                    <%#Eval("YuKongRenShu") %>
                                </td>
                                <td align="center">
                                    <%#(int)Eval("YuKongRenShu")-(int)Eval("YouXiaoDingDanRenShu") %>
                                </td>
                                <td align="center" data-id="<%#Eval("HangQiId") %>" data-hangxianid="<%#Eval("hangxianid") %>">
                                    <a href="javascript:;" class="hangqisheding">航期设定</a> | <a href="javascript:;" class="jiagesheding">
                                        价格设定</a> | <a href="javascript:;" class="youhuiguize">优惠规则</a> | <a href="javascript:;" class="dianping">点评</a>
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
        var pageDataJS = {
            parmars: { t: '<%=EyouSoft.Common.Utils.GetQueryStringValue("t") %>' },
            CreateBoxy: function(data) {
                Boxy.iframeDialog({
                    iframeUrl: data.url,
                    title: data.title,
                    modal: true,
                    width: data.width,
                    height: data.height
                });
            },
            setBiaoQian: function(data) {
                if ($("input[.lischk]:checked").length > 1) {
                    tableToolbar._showMsg("只能操作单条数据！");
                    return false;
                }
                if ($("input[.lischk]:checked").length == 0) {
                    tableToolbar._showMsg("请选择要修改的数据！");
                    return false;
                }
                $.newAjax({
                    url: "HangQi.aspx?" + $.param(data),
                    dataType: "json",
                    success: function(ret) {
                        if (ret.result == "1") {
                            tableToolbar._showMsg(ret.msg, function() { location.href = location.href; });
                        }
                        else {
                            tableToolbar._showMsg(tableToolbar.errorMsg);
                        }
                    },
                    error: function() {
                        tableToolbar._showMsg(tableToolbar.errorMsg);
                    }
                });


            }
        }
        $(function() {
            $(".toolbar_update").click(function() {
                if ($("input[.lischk]:checked").length > 1) {
                    tableToolbar._showMsg("只能操作单条数据！");
                    return false;
                }
                if ($("input[.lischk]:checked").length == 0) {
                    tableToolbar._showMsg("请选择要修改的数据！");
                    return false;
                }
                window.location.href = "HangQiAdd.aspx?sl=97&id=" + $("input[.lischk]:checked").val() + "&t=" + pageDataJS.parmars.t;
            })//
            $(".toolbar_delete").click(function() {
                if ($("input[.lischk]:checked").length > 1) {
                    tableToolbar._showMsg("只能操作单条数据！");
                    return false;
                }
                if ($("input[.lischk]:checked").length == 0) {
                    tableToolbar._showMsg("请选择要修改的数据！");
                    return false;
                }

                if (!confirm("你确定要删除吗？")) return false;

                $.newAjax({
                    url: "HangQi.aspx?del=1&sl=97&id=" + $("input[.lischk]:checked").val(),
                    dataType: "json",
                    type: "post",
                    success: function(ret) {
                        if (ret.result == "1") {
                            tableToolbar._showMsg(ret.msg, function() { location.href = location.href; });
                        }
                        else {
                            tableToolbar._showMsg(tableToolbar.errorMsg);
                        }
                    },
                    error: function() {
                        tableToolbar._showMsg(tableToolbar.errorMsg);
                    }
                });

            })//
            $(".hangqisheding").click(function() {
                var id = $(this).closest("td").attr("data-id");
                var data = { url: 'HangQiSheDing.aspx?sl=97&id=' + id, title: '航期设定', width: '900px', height: '450px' }
                pageDataJS.CreateBoxy(data);
            })
            $(".jiagesheding").click(function() {
                var id = $(this).closest("td").attr("data-id");
                var data = { url: 'HangQiJiaGe.aspx?sl=97&hangxianid='+$(this).closest("td").attr("data-hangxianid")+'&id=' + id + '&t=' + pageDataJS.parmars.t, title: '价格设定', width: '900px', height: '450px' }
                pageDataJS.CreateBoxy(data);
            })
            $(".dianping").click(function() {
                var id = $(this).closest("td").attr("data-id");
                var data = { url: 'HuiYuanDianPingEdit.aspx?sl=97&hangqiid=' + id, title: '点评', width: '550px', height: '280px' }
                pageDataJS.CreateBoxy(data);
            })
            $(".youhuiguize").click(function() {
                var id = $(this).closest("td").attr("data-id");
                var data = { url: 'YouHuiGuiZe.aspx?sl=97&id=' + id, title: '优惠规则', width: '900px', height: '440px' }
                pageDataJS.CreateBoxy(data);
            })//
            $(".toolbar_remen").click(function() {
                var data = {
                    sl: 97,
                    biaoqian: 1,
                    hangqi: $("input[.lischk]:checked").val(),
                    dotype: $("input[.lischk]:checked").closest("tr").find(".remen").length > 0 ? 1 : 0,
                    b: 0
                };
                pageDataJS.setBiaoQian(data);
            })//
            $(".toolbar_tejia").click(function() {
                var data = {
                    sl: 97,
                    biaoqian: 1,
                    hangqi: $("input[.lischk]:checked").val(),
                    dotype: $("input[.lischk]:checked").closest("tr").find(".tejia").length > 0 ? 1 : 0,
                    b: 1
                };
                pageDataJS.setBiaoQian(data);
            })//
            $(".toolbar_rementuijian").click(function() {
                var data = {
                    sl: 97,
                    biaoqian: 1,
                    hangqi: $("input[.lischk]:checked").val(),
                    dotype: $("input[.lischk]:checked").closest("tr").find(".rementuijian").length > 0 ? 1 : 0,
                    b: 2
                };
                pageDataJS.setBiaoQian(data);
            })//
            $(".toolbar_tejiatuijian").click(function() {
                var data = {
                    sl: 97,
                    biaoqian: 1,
                    hangqi: $("input[.lischk]:checked").val(),
                    dotype: $("input[.lischk]:checked").closest("tr").find(".tejiatuijian").length > 0 ? 1 : 0,
                    b: 3
                };
                pageDataJS.setBiaoQian(data);
            })//
            $('.ainfo').bt({
                contentSelector: function() {
                    if ($.trim($(this).next().html()) == "") {
                        return false;
                    }
                    else {
                        return $(this).next().html();
                    }
                },
                positions: ['right', 'left', 'bottom'],
                fill: '#FFF2B5',
                strokeStyle: '#D59228',
                noShadowOpts: { strokeStyle: "#D59228" },
                spikeLength: 10,
                spikeGirth: 15,
                width: 100,
                overlap: 0,
                centerPointY: 1,
                cornerRadius: 4,
                shadow: true,
                shadowColor: 'rgba(0,0,0,.5)',
                cssStyles: { color: '#00387E', 'line-height': '180%' }
            }); //

            if ('<%=EyouSoft.Common.Utils.GetQueryStringValue("t") %>' == "1") $("#i_a_hyyl").addClass("de-ztorderform");
            else $("#i_a_cjyl").addClass("de-ztorderform");
        });
    </script>

</asp:Content>

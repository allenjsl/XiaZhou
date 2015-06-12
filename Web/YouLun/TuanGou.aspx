<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TuanGou.aspx.cs" Inherits="EyouSoft.Web.YouLun.TuanGou"
    MasterPageFile="~/MasterPage/Front.Master" %>

<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="mainbox">
        <div class="searchbox fixed">
            <form id="form1" method="get">
            <input type="hidden" name="sl" value='<%= SL %>' />
            <span class="searchT">
                <p>
                    团购名称：<input type="text" size="30" class="inputtext formsize120" name="txtMingCheng"
                        value="<%=Request.QueryString["txtMingCheng"] %>" />
                    团购编号：<input type="text" size="30" class="inputtext formsize120" name="txtBianHao"
                        value="<%=Request.QueryString["txtBianHao"] %>" />
                    状态：<select id="selstat" name="stat" class="inputselect"><option value="0">请选择</option><option value="1">有效</option><option value="2">过期</option></select>
                    <input type="submit" value="搜索" class="search-btn" />
                </p>
            </span>
            </form>
        </div>
        <div class="tablehead" id="select_Toolbar_Paging_1">
            <ul class="fixed">
                <li><s class="addicon"></s><a class="toolbar_add" id="A1" hidefocus="true" href="javascript:void(0)">
                    <span>新增团购</span></a></li>
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
                        团购名称
                    </th>                    
                    <th class="th-line" style="text-align: left;">
                        团购编号
                    </th>
                    <th class="th-line" style="text-align: left;">
                        供应商
                    </th>
                    <th class="th-line" style="text-align: left;">
                        游轮公司
                    </th>
                    <th class="th-line" style="text-align: left;">
                        游轮系列
                    </th>
                    <th class="th-line" style="text-align: left;">
                        游轮船只
                    </th>
                    <th class="th-line" style="text-align: left;">
                        出港日期
                    </th>
                    <th class="th-line" style="text-align: left;">
                        团购期限
                    </th>
                    <th class="th-line" style="text-align: right;">
                        原价&nbsp;
                    </th>
                    <th class="th-line" style="text-align: right;">
                        现价&nbsp;
                    </th>
                    <th class="th-line" style="text-align: right;">
                        已预订人数&nbsp;
                    </th>
                    <th class="th-line" style="text-align: left; width: 100px;">
                        操作
                    </th>
                </tr>
                <asp:Repeater ID="rpt" runat="server">
                    <ItemTemplate>
                        <tr class="odd" i_tuangouid="<%#Eval("TuanGouId") %>">
                            <td class="thinputbg" style="text-align: center;">
                                <input type="checkbox" id="checkbox" name="checkbox" value='<%# Eval("TuanGouId") %>'>
                            </td>
                            <td class="th-line">
                                <%#Eval("MingCheng")%>
                            </td>                            
                            <td class="th-line">
                                <%#Eval("BianHao")%>
                            </td>
                            <td class="th-line">
                               <%#Eval("GysName")%>
                            </td>
                            <td class="th-line">
                                <%#Eval("GongSiName")%>
                            </td>
                            <td class="th-line">
                                <%#Eval("XiLieName")%>
                            </td>
                            <td class="th-line">
                                <%#Eval("ChuanZhiName")%>
                            </td>
                            <td class="th-line">
                                <%#Eval("RiQi","{0:yyyy-MM-dd}")%>
                            </td>
                            <td class="th-line">
                                <%#Eval("JieZhiShiJian","{0:yyyy-MM-dd}")%>
                            </td>
                            <td class="th-line" style="text-align:right;">
                                <%#Eval("YuanJia","{0:F2}")%>&nbsp;
                            </td>
                            <td class="th-line" style="text-align:right;">
                                <%#Eval("XianJia", "{0:F2}")%>&nbsp;
                            </td>
                            <td class="th-line" style="text-align:right;">
                                <%#Eval("YiYuDingRenShu")%>&nbsp;
                            </td>                            
                            <td class="th-line">
                                <a href="javascript:void(0)" class="i_xiugai">修改</a> <a href="javascript:void(0)"
                                    class="i_shanchu">删除</a>
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

    <script type="text/javascript">
        var iPage = {
            reload: function() {
                window.location.href = window.location.href;
            },
            insert: function() {
                Boxy.iframeDialog({ iframeUrl: "TuanGouEdit.aspx?sl=<%=SL %>", title: "新增团购", modal: true, width: "910px", height: "600px", afterHide: function() { iPage.reload(); } });
            },
            xiuGai: function(obj) {
                var _$tr = $(obj).closest("tr");
                Boxy.iframeDialog({ iframeUrl: "TuanGouEdit.aspx?sl=<%=SL %>&editid=" + _$tr.attr("i_tuangouid"), title: "修改团购", modal: true, width: "910px", height: "600px", afterHide: function() { iPage.reload(); } });
            },
            shanChu: function(obj) {
                if (!confirm("你确定要删除吗?")) return false;
                var _$tr = $(obj).closest("tr");
                var _data = { txtTuanGouId: _$tr.attr("i_tuangouid") };
                $.ajax({ type: "POST", url: "TuanGou.aspx?doType=shanchu&sl=<%=SL %>", data: _data, cache: false, dataType: "json", async: false,
                    success: function(response) {
                        if (response.result == "1") {
                            alert(response.msg);
                            iPage.reload();
                        } else {
                            alert(response.msg);
                        }
                    }
                });
            }
        };

        $(document).ready(function() {
        tableToolbar.init();
             $("#selstat option[value='<%=EyouSoft.Common.Utils.GetInt(EyouSoft.Common.Utils.GetQueryStringValue("stat")) %>']").attr("selected", "selected")
            //clone toolbar、paging
            $("#select_Toolbar_Paging_1").children().clone(true).prependTo("#select_Toolbar_Paging_2");
            $(".toolbar_add").bind("click", function() { iPage.insert(); });
            $(".i_xiugai").bind("click", function() { iPage.xiuGai(this); });
            $(".i_shanchu").bind("click", function() { iPage.shanChu(this); });
        });
    </script>

</asp:Content>

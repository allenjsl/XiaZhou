<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="JiFenDuiHuan.aspx.cs" Inherits="EyouSoft.Web.YouLun.JiFenDuiHuan"
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
                    商品名称：<input type="text" size="30" class="inputtext formsize120" name="txtMingCheng"
                        value="<%=Request.QueryString["txtMingCheng"] %>" />
                    商品状态：<select name="txtStatus" id="txtStatus" class="inputselect">
                        <option value="">-请选择-</option>
                        <%=EyouSoft.Common.UtilsCommons.GetEnumDDL(EyouSoft.Common.EnumObj.GetList(typeof(EyouSoft.Model.EnumType.YlStructure.JiFenShangPinStatus)),EyouSoft.Common.Utils.GetQueryStringValue("txtStatus"))%>
                    </select>
                    <input type="submit" value="搜索" class="search-btn" />
                </p>
            </span>
            </form>
        </div>
        <div class="tablehead" id="select_Toolbar_Paging_1">
            <ul class="fixed">
                <li><s class="addicon"></s><a class="toolbar_add" id="A1" hidefocus="true" href="javascript:void(0)">
                    <span>新增商品</span></a></li>
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
                        商品名称
                    </th>
                    <th class="th-line" style="text-align: left;">
                        商品金额
                    </th>
                    <th class="th-line" style="text-align: left;">
                        商品图片
                    </th>                    
                    <th class="th-line" style="text-align: left;">
                        兑换方式
                    </th>
                    <th class="th-line" style="text-align: left;">
                        商品状态
                    </th>
                    <th class="th-line" style="text-align: right;">
                        数量&nbsp;
                    </th>
                    <th class="th-line" style="text-align: right;">
                        剩余数量&nbsp;
                    </th>
                    <th class="th-line" style="text-align: left; width: 100px;">
                        操作
                    </th>
                </tr>
                <asp:Repeater ID="rpt" runat="server">
                    <ItemTemplate>
                        <tr class="odd" i_shangpinid="<%#Eval("ShangPinId") %>">
                            <td class="thinputbg" style="text-align: center;">
                                <input type="checkbox" id="checkbox" name="checkbox" value='<%# Eval("ShangPinId") %>'>
                            </td>
                            <td class="th-line">
                                <%#Eval("MingCheng")%>
                            </td>
                            <td class="th-line">
                                <%#Eval("ShangPinJinE","{0:F2}")%>
                            </td>
                            <td class="th-line">
                                <%#GetShangPinFuJian(Eval("FuJians"))%>
                            </td>
                            <td class="th-line">
                                <%#GetDuiHuanFangShi(Eval("FangShis"))%>
                            </td>
                            <td class="th-line">
                                <%#Eval("Status") %>
                            </td>
                            <td class="th-line" style="text-align:right;">
                                <%#Eval("ShuLiang") %>&nbsp;
                            </td>
                            <td class="th-line" style="text-align:right;">
                                <%#Eval("ShengYuShuLiang")%>&nbsp;
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
                Boxy.iframeDialog({ iframeUrl: "JiFenShangPinEdit.aspx?sl=<%=SL %>", title: "新增商品", modal: true, width: "910px", height: "600px", afterHide: function() { iPage.reload(); } });
            },
            xiuGai: function(obj) {
                var _$tr = $(obj).closest("tr");
                Boxy.iframeDialog({ iframeUrl: "JiFenShangPinEdit.aspx?sl=<%=SL %>&editid=" + _$tr.attr("i_shangpinid"), title: "修改商品", modal: true, width: "910px", height: "600px", afterHide: function() { iPage.reload(); } });
            },
            shanChu: function(obj) {
                if (!confirm("你确定要删除吗?")) return false;
                var _$tr = $(obj).closest("tr");
                var _data = { txtShangPinId: _$tr.attr("i_shangpinid") };
                $.ajax({ type: "POST", url: "JiFenDuiHuan.aspx?doType=shanchu&sl=<%=SL %>", data: _data, cache: false, dataType: "json", async: false,
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
            wuc4(0);
            tableToolbar.init();
            //clone toolbar、paging
            $("#select_Toolbar_Paging_1").children().clone(true).prependTo("#select_Toolbar_Paging_2");
            $(".toolbar_add").bind("click", function() { iPage.insert(); });
            $(".i_xiugai").bind("click", function() { iPage.xiuGai(this); });
            $(".i_shanchu").bind("click", function() { iPage.shanChu(this); });
        });
    </script>

</asp:Content>

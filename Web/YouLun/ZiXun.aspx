<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ZiXun.aspx.cs" Inherits="EyouSoft.Web.YouLun.ZiXun"
    MasterPageFile="~/MasterPage/Front.Master" %>

<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="mainbox">
        <div class="searchbox fixed">
            <form id="form1" method="get">
            <input type="hidden" name="sl" value='<%= SL %>' />
            <span class="searchT">
                <p>
                    类型：<select name="txtLeiXing" class="inputselect">
                        <option value="">-请选择-</option>
                        <%=EyouSoft.Common.UtilsCommons.GetEnumDDL(EyouSoft.Common.EnumObj.GetList(typeof(EyouSoft.Model.EnumType.YlStructure.WzZiXunLeiXing), new string[] { "0" }), EyouSoft.Common.Utils.GetQueryStringValue("txtLeiXing"))%>
                    </select>
                    标题：<input type="text" size="30" class="inputtext formsize120" name="txtBiaoTi" value="<%=Request.QueryString["txtBiaoTi"] %>" />
                    <input type="submit" value="搜索" class="search-btn" />
                </p>
            </span>
            </form>
        </div>
        <div class="tablehead" id="select_Toolbar_Paging_1">
            <ul class="fixed">
                <li><s class="addicon"></s><a class="toolbar_add" id="A1" hidefocus="true" href="javascript:void(0)">
                    <span>新增资讯</span></a></li>
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
                        资讯类型
                    </th>
                    <th class="th-line" style="text-align: left;">
                        资讯标题
                    </th>
                    <th class="th-line" style="text-align: left;">
                        发布时间
                    </th>
                    <th class="th-line" style="text-align: left; width: 100px;">
                        操作
                    </th>
                </tr>
                <asp:Repeater ID="rpt" runat="server">
                    <ItemTemplate>
                        <tr class="odd" i_zixunid="<%#Eval("ZiXunId") %>">
                            <td class="thinputbg" style="text-align: center;">
                                <input type="checkbox" id="checkbox" name="checkbox" value='<%# Eval("ZiXunId") %>'>
                            </td>
                            <td class="th-line">
                                <%#Eval("LeiXing")%>
                            </td>
                            <td class="th-line">
                                <%#Eval("BiaoTi")%>
                            </td>
                            <td class="th-line">
                                <%#Eval("IssueTime","{0:yyyy-MM-dd}")%>
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
                Boxy.iframeDialog({ iframeUrl: "ZiXunEdit.aspx?sl=<%=SL %>", title: "新增资讯", modal: true, width: "780px", height: "500px", afterHide: function() { iPage.reload(); } });
            },
            xiuGai: function(obj) {
                var _$tr = $(obj).closest("tr");
                Boxy.iframeDialog({ iframeUrl: "ZiXunEdit.aspx?sl=<%=SL %>&editid=" + _$tr.attr("i_zixunid"), title: "修改资讯", modal: true, width: "780px", height: "500px", afterHide: function() { iPage.reload(); } });
            },
            shanChu: function(obj) {
                if (!confirm("你确定要删除吗?")) return false;
                var _$tr = $(obj).closest("tr");
                var _data = { txtZiXunId: _$tr.attr("i_zixunid") };
                $.ajax({ type: "POST", url: "ZiXun.aspx?doType=shanchu&sl=<%=SL %>", data: _data, cache: false, dataType: "json", async: false,
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
            //clone toolbar、paging
            $("#select_Toolbar_Paging_1").children().clone(true).prependTo("#select_Toolbar_Paging_2");
            $(".toolbar_add").bind("click", function() { iPage.insert(); });
            $(".i_xiugai").bind("click", function() { iPage.xiuGai(this); });
            $(".i_shanchu").bind("click", function() { iPage.shanChu(this); });
        });
    </script>

</asp:Content>

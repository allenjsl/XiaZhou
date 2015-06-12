<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GuangGao.aspx.cs" Inherits="EyouSoft.Web.YouLun.GuangGao"
    MasterPageFile="~/MasterPage/Front.Master" %>

<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="mainbox">
        <div class="searchbox fixed">
            <form id="form1" method="get">
            <input type="hidden" name="sl" value='<%= SL %>' />
            <span class="searchT">
                <p>
                    广告位置：<select name="txtWeiZhi" class="inputselect">
                        <option value="">-请选择-</option>
                        <%=EyouSoft.Common.UtilsCommons.GetEnumDDL(EyouSoft.Common.EnumObj.GetList(typeof(EyouSoft.Model.EnumType.YlStructure.WzGuangGaoWeiZhi), new string[] { "0" }), EyouSoft.Common.Utils.GetQueryStringValue("txtWeiZhi"))%>
                    </select>
                    <input type="submit" value="搜索" class="search-btn" />
                </p>
            </span>
            </form>
        </div>
        <div class="tablehead" id="select_Toolbar_Paging_1">
            <ul class="fixed">
                <li><s class="addicon"></s><a class="toolbar_add" id="A1" hidefocus="true" href="javascript:void(0)">
                    <span>新增广告</span></a></li>
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
                        广告名称
                    </th>
                    <th class="th-line" style="text-align: left;">
                        广告位置
                    </th>
                    <th class="th-line" style="text-align: left;">
                        广告图片
                    </th>
                    <th class="th-line" style="text-align: left;">
                        排序值
                    </th>
                    <th class="th-line" style="text-align: left; width: 100px;">
                        操作
                    </th>
                </tr>
                <asp:Repeater ID="rpt" runat="server">
                    <ItemTemplate>
                        <tr class="odd" i_guanggaoid="<%#Eval("GuangGaoId") %>">
                            <td class="thinputbg" style="text-align: center;">
                                <input type="checkbox" id="checkbox" name="checkbox" value='<%# Eval("GuangGaoId") %>'>
                            </td>
                            <td class="th-line">
                                <%#Eval("MingCheng")%>
                            </td>
                            <td class="th-line">
                                <%#Eval("WeiZhi")%>
                            </td>
                            <td class="th-line">
                                <%#GetLianJie(Eval("Filepath"),Eval("Url"),Eval("MingCheng")) %>
                            </td>
                            <td class="th-line">
                                <%#Eval("PaiXuId")%>
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
                Boxy.iframeDialog({ iframeUrl: "GuangGaoEdit.aspx?sl=<%=SL %>", title: "新增广告", modal: true, width: "680px", height: "330px", afterHide: function() { iPage.reload(); } });
            },
            xiuGai: function(obj) {
                var _$tr = $(obj).closest("tr");
                Boxy.iframeDialog({ iframeUrl: "GuangGaoEdit.aspx?sl=<%=SL %>&editid=" + _$tr.attr("i_guanggaoid"), title: "修改广告", modal: true, width: "680px", height: "330px", afterHide: function() { iPage.reload(); } });
            },
            shanChu: function(obj) {
                if (!confirm("你确定要删除吗?")) return false;
                var _$tr = $(obj).closest("tr");
                var _data = { txtGuangGaoId: _$tr.attr("i_guanggaoid") };
                $.ajax({ type: "POST", url: "GuangGao.aspx?doType=shanchu&sl=<%=SL %>", data: _data, cache: false, dataType: "json", async: false,
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

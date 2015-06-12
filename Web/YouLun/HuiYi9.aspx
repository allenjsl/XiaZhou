<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HuiYi9.aspx.cs" Inherits="EyouSoft.Web.YouLun.HuiYi9"
    MasterPageFile="~/MasterPage/Front.Master" %>

<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>
<%@ Register Src="~/YouLun/WUC/wuc3.ascx" TagName="wuc3" TagPrefix="uc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="mainbox">
        <uc1:wuc3 runat="server" ID="wuc3" />
        <div class="searchbox fixed">
            <form id="form1" method="get">
            <input type="hidden" name="sl" value='<%= SL %>' />
            <span class="searchT">
                <p>
                    游轮类型：<select name="txtLeiXing" id="txtLeiXing" class="inputselect">
                        <option value="">-请选择-</option>
                        <%=EyouSoft.Common.UtilsCommons.GetEnumDDL(EyouSoft.Common.EnumObj.GetList(typeof(EyouSoft.Model.EnumType.YlStructure.YouLunLeiXing)),EyouSoft.Common.Utils.GetQueryStringValue("txtLeiXing"))%>
                    </select>
                    会议名称：<input type="text" size="30" class="inputtext formsize120" name="txtMingCheng"
                        value="<%=Request.QueryString["txtMingCheng"] %>" />                    
                    会议时间：<input type="text" onfocus="WdatePicker()" class="inputtext formsize100" name="txtShiJian1"
                        value="<%=Request.QueryString["txtShiJian1"] %>" />-<input type="text" onfocus="WdatePicker()"
                            class="inputtext formsize100" name="txtShiJian2" value="<%=Request.QueryString["txtShiJian2"] %>" />
                    <input type="submit" value="搜索" class="search-btn" />
                </p>
            </span>
            </form>
        </div>
        <div class="tablehead" id="select_Toolbar_Paging_1">
            <ul class="fixed">
                <li><s class="addicon"></s><a class="toolbar_add" id="A1" hidefocus="true" href="javascript:void(0)">
                    <span>新增案例</span></a></li>
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
                        会议名称
                    </th>
                    <th class="th-line" style="text-align: left;">
                        开始时间
                    </th>
                    <th class="th-line" style="text-align: left;">
                        结束时间
                    </th>
                    <th class="th-line" style="text-align: left;">
                        会议图片
                    </th>
                    <th class="th-line" style="text-align: left; width: 100px;">
                        操作
                    </th>
                </tr>
                <asp:Repeater ID="rpt" runat="server">
                    <ItemTemplate>
                        <tr class="odd" i_anliid="<%#Eval("AnLiId") %>">
                            <td class="thinputbg" style="text-align: center;">
                                <input type="checkbox" id="checkbox" name="checkbox" value='<%# Eval("AnLiId") %>'>
                            </td>
                            <td class="th-line">
                                <%#Eval("MingCheng")%>
                            </td>
                            <td class="th-line">
                                <%#Eval("ShiJian1","{0:yyyy-MM-dd}")%>
                            </td>
                            <td class="th-line">
                                <%#Eval("ShiJian2","{0:yyyy-MM-dd}")%>
                            </td>
                            <td class="th-line">
                                <img src="<%#Eval("Filepath") %>" style="width: 124px; height: 80px; margin: 4px;" />
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

    <script src="/js/datepicker/wdatepicker.js" type="text/javascript"></script>
    <script type="text/javascript">
        var iPage = {
            reload: function() {
                window.location.href = window.location.href;
            },
            insert: function() {
                Boxy.iframeDialog({ iframeUrl: "HuiYi901.aspx?sl=<%=SL %>", title: "新增案例", modal: true, width: "680px", height: "300px", afterHide: function() { iPage.reload(); } });
            },
            xiuGai: function(obj) {
                var _$tr = $(obj).closest("tr");
                Boxy.iframeDialog({ iframeUrl: "HuiYi901.aspx?sl=<%=SL %>&editid=" + _$tr.attr("i_anliid"), title: "修改案例", modal: true, width: "680px", height: "300px", afterHide: function() { iPage.reload(); } });
            },
            shanChu: function(obj) {
                if (!confirm("你确定要删除吗?")) return false;
                var _$tr = $(obj).closest("tr");
                var _data = { txtAnLiId: _$tr.attr("i_anliid") };
                $.ajax({ type: "POST", url: "HuiYi9.aspx?doType=shanchu&sl=<%=SL %>", data: _data, cache: false, dataType: "json", async: false,
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
            wuc3(9);
            tableToolbar.init();
            //clone toolbar、paging
            $("#select_Toolbar_Paging_1").children().clone(true).prependTo("#select_Toolbar_Paging_2");
            $(".toolbar_add").bind("click", function() { iPage.insert(); });
            $(".i_xiugai").bind("click", function() { iPage.xiuGai(this); });
            $(".i_shanchu").bind("click", function() { iPage.shanChu(this); });
        });
    </script>

</asp:Content>

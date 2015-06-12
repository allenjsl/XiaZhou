<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HuiYi8.aspx.cs" Inherits="EyouSoft.Web.YouLun.HuiYi8"
    MasterPageFile="~/MasterPage/Front.Master" ValidateRequest="false" %>

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
                    游轮类型：<select name="txtLeiXing" class="inputselect">
                        <option value="">-请选择-</option>
                        <%=EyouSoft.Common.UtilsCommons.GetEnumDDL(EyouSoft.Common.EnumObj.GetList(typeof(EyouSoft.Model.EnumType.YlStructure.YouLunLeiXing)), EyouSoft.Common.Utils.GetQueryStringValue("txtLeiXing"))%>
                    </select>
                    申请时间：<input type="text" onfocus="WdatePicker()" class="inputtext formsize100" name="txtShenQingShiJian1"
                        value="<%=Request.QueryString["txtShenQingShiJian1"] %>" />-<input type="text" onfocus="WdatePicker()"
                            class="inputtext formsize100" name="txtShenQingShiJian2" value="<%=Request.QueryString["txtShenQingShiJian2"] %>" />
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
                        游轮类型
                    </th>
                    <th class="th-line" style="text-align: left;">
                        会议规模
                    </th>
                    <th class="th-line" style="text-align: left;">
                        预计时间
                    </th>
                    <th class="th-line" style="text-align: left;">
                        联系人姓名
                    </th>
                    <th class="th-line" style="text-align: left;">
                        联系人手机
                    </th>
                    <th class="th-line" style="text-align: left;">
                        申请时间
                    </th>
                    <th class="th-line" style="text-align: left;">
                        处理时间
                    </th>
                    <th class="th-line" style="text-align: left; width: 100px;">
                        操作
                    </th>
                </tr>
                <asp:Repeater ID="rpt" runat="server">
                    <ItemTemplate>
                        <tr class="odd" i_shenqingid="<%#Eval("ShenQingId") %>">
                            <td class="thinputbg" style="text-align: center;">
                                <input type="checkbox" id="checkbox" name="checkbox" value='<%# Eval("ShenQingId") %>'>
                            </td>
                            <td class="th-line">
                                <%#Eval("LeiXing")%>
                            </td>
                            <td class="th-line">
                                <%#Eval("GuiMo")%>
                            </td>
                            <td class="th-line">
                                <%#Eval("YuJiShiJian")%>
                            </td>
                            <td class="th-line">
                                <%#Eval("LxrXingMing")%>
                            </td>
                            <td class="th-line">
                                <%#Eval("LxrShouJi")%>
                            </td>
                            <td class="th-line">
                                <%#Eval("IssueTime","{0:yyyy-MM-dd}")%>
                            </td>
                            <td class="th-line">
                                <%#Eval("ChuLiShiJian","{0:yyyy-MM-dd}")%>
                            </td>
                            <td class="th-line">
                                <a href="javascript:void(0)" class="i_chakan">查看</a> <a href="javascript:void(0)"
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
    <script src="/js/kindeditor-4.1/kindeditor-min.js" type="text/javascript"></script>

    <script type="text/javascript">
        var iPage = {
            reload: function() {
                window.location.href = window.location.href;
            },
            chaKan: function(obj) {
                var _$tr = $(obj).closest("tr");
                Boxy.iframeDialog({ iframeUrl: "HuiYi801.aspx?sl=<%=SL %>&editid=" + _$tr.attr("i_shenqingid"), title: "查看会议申请信息", modal: true, width: "780px", height: "500px", afterHide: function() { iPage.reload(); } });
            },
            shanChu: function(obj) {
                if (!confirm("你确定要删除吗?")) return false;
                var _$tr = $(obj).closest("tr");
                var _data = { txtShenQingId: _$tr.attr("i_shenqingid") };
                $.ajax({ type: "POST", url: "HuiYi8.aspx?doType=shanchu&sl=<%=SL %>", data: _data, cache: false, dataType: "json", async: false,
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
            wuc3(8);
            tableToolbar.init();
            //clone toolbar、paging
            $("#select_Toolbar_Paging_1").children().clone(true).prependTo("#select_Toolbar_Paging_2");
            $(".i_chakan").bind("click", function() { iPage.chaKan(this); });
            $(".i_shanchu").bind("click", function() { iPage.shanChu(this); });
        });
    </script>

</asp:Content>

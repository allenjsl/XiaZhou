<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="YiJianFanKui.aspx.cs" Inherits="EyouSoft.Web.YouLun.YiJianFanKui" MasterPageFile="~/MasterPage/Front.Master" %>
<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>
<%@ Register Src="~/YouLun/WUC/wuc2.ascx" TagName="wuc2" TagPrefix="uc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="mainbox">
        <uc1:wuc2 runat="server" ID="wuc2" />
        <div class="searchbox fixed">
            <form id="form1" method="get">
            </form>
        </div>
        <div class="tablehead" id="select_Toolbar_Paging_1">
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
                        问题类型
                    </th>
                    <th class="th-line" style="text-align: left;">
                        问题描述
                    </th>
                    <th class="th-line" style="text-align: left;">
                        IP地址
                    </th>
                    <th class="th-line" style="text-align: left;">
                        操作时间
                    </th>
                    <th class="th-line" style="text-align: left;">
                        会员姓名
                    </th>
                    <th class="th-line" style="text-align: left; width: 100px;">
                        操作
                    </th>
                </tr>
                <asp:Repeater ID="rpt" runat="server">
                    <ItemTemplate>
                        <tr class="odd" i_lianjieid="<%#Eval("YiJianId") %>">
                            <td class="thinputbg" style="text-align: center;">
                                <input type="checkbox" id="checkbox" name="checkbox" value='<%# Eval("YiJianId") %>'>
                            </td>
                            <td class="th-line">
                                <%#Eval("LeiXing")%>
                            </td>
                            <td class="th-line">
                                <%#Eval("MiaoShu")%>
                            </td>
                            <td class="th-line">
                                <%#Eval("RemoteIP")%>
                            </td>
                            <td class="th-line">
                                <%#Eval("IssueTime")%>
                            </td>
                            <td class="th-line">
                                <%#GetHuiYuan(Eval("OperatorId"))%>
                            </td>
                            <td class="th-line">
                                <a href="javascript:void(0)" class="i_xiugai">查看</a> <a href="javascript:void(0)"
                                    class="i_shanchu">删除</a>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
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
            url:"YiJianFanKuiEdit.aspx?sl=<%=SL %>",
            reload: function() {
                window.location.href = window.location.href;
            },
            xiuGai: function(obj) {
                var _$tr = $(obj).closest("tr");
                Boxy.iframeDialog({ iframeUrl: iPage.url+"&editid=" + _$tr.attr("i_lianjieid"), title: "意见反馈", modal: true, width: "680px", height: "600px", afterHide: function() { iPage.reload(); } });
            },
            shanChu: function(obj) {
                if (!confirm("你确定要删除吗?")) return false;
                var _$tr = $(obj).closest("tr");
                var _data = { txtLianJieId: _$tr.attr("i_lianjieid") };
                $.ajax({ type: "POST", url: "YiJianFanKui.aspx?doType=shanchu&sl=<%=SL %>", data: _data, cache: false, dataType: "json", async: false,
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
            wuc2(14);
            tableToolbar.init();
            //clone toolbar、paging
            $("#select_Toolbar_Paging_1").children().clone(true).prependTo("#select_Toolbar_Paging_2");
            $(".i_xiugai").bind("click", function() { iPage.xiuGai(this); });
            $(".i_shanchu").bind("click", function() { iPage.shanChu(this); });
        });
    </script>
</asp:Content>

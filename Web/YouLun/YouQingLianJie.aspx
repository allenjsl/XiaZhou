﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="YouQingLianJie.aspx.cs"
    Inherits="EyouSoft.Web.YouLun.YouQingLianJie" MasterPageFile="~/MasterPage/Front.Master" %>

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
            <ul class="fixed">
                <li><s class="addicon"></s><a class="toolbar_add" id="A1" hidefocus="true" href="javascript:void(0)">
                    <span>新增链接</span></a></li>
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
                        链接名称
                    </th>
                    <th class="th-line" style="text-align: left;">
                        链接地址
                    </th>
                <%if (this.YouQingLianJieLeiXing != EyouSoft.Model.EnumType.YlStructure.WzYouQingLianJieLeiXing.长江优惠信息 && this.YouQingLianJieLeiXing != EyouSoft.Model.EnumType.YlStructure.WzYouQingLianJieLeiXing.海洋优惠信息) %>
                <%{ %>
                    <th class="th-line" style="text-align: left;">
                        链接图片
                    </th>
                    <%} %>
                    <th class="th-line" style="text-align: left; width: 100px;">
                        操作
                    </th>
                </tr>
                <asp:Repeater ID="rpt" runat="server">
                    <ItemTemplate>
                        <tr class="odd" i_lianjieid="<%#Eval("LianJieId") %>">
                            <td class="thinputbg" style="text-align: center;">
                                <input type="checkbox" id="checkbox" name="checkbox" value='<%# Eval("LianJieId") %>'>
                            </td>
                            <td class="th-line">
                                <%#Eval("MingCheng")%>
                            </td>
                            <td class="th-line">
                                <%#Eval("Url")%>
                            </td>
                <%if (this.YouQingLianJieLeiXing != EyouSoft.Model.EnumType.YlStructure.WzYouQingLianJieLeiXing.长江优惠信息 && this.YouQingLianJieLeiXing != EyouSoft.Model.EnumType.YlStructure.WzYouQingLianJieLeiXing.海洋优惠信息) %>
                <%{ %>
                            <td class="th-line">
                                <%#GetLianJie(Eval("Filepath"),Eval("Url"),Eval("MingCheng")) %>
                            </td>
                            <%} %>
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
            url:"YouQingLianJieEdit.aspx?sl=<%=SL %>"+'<%=this.YouQingLianJieLeiXing==EyouSoft.Model.EnumType.YlStructure.WzYouQingLianJieLeiXing.长江优惠信息||this.YouQingLianJieLeiXing == EyouSoft.Model.EnumType.YlStructure.WzYouQingLianJieLeiXing.海洋优惠信息?"&leixing="+(int)this.YouQingLianJieLeiXing:"" %>',
            reload: function() {
                window.location.href = window.location.href;
            },
            insert: function() {
                Boxy.iframeDialog({ iframeUrl: iPage.url, title: "新增友情链接", modal: true, width: "680px", height: "300px", afterHide: function() { iPage.reload(); } });
            },
            xiuGai: function(obj) {
                var _$tr = $(obj).closest("tr");
                Boxy.iframeDialog({ iframeUrl: iPage.url+"&editid=" + _$tr.attr("i_lianjieid"), title: "修改友情链接", modal: true, width: "680px", height: "300px", afterHide: function() { iPage.reload(); } });
            },
            shanChu: function(obj) {
                if (!confirm("你确定要删除吗?")) return false;
                var _$tr = $(obj).closest("tr");
                var _data = { txtLianJieId: _$tr.attr("i_lianjieid") };
                $.ajax({ type: "POST", url: "YouQingLianJie.aspx?doType=shanchu&sl=<%=SL %>", data: _data, cache: false, dataType: "json", async: false,
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
            wuc2("<%=this.YouQingLianJieLeiXing==EyouSoft.Model.EnumType.YlStructure.WzYouQingLianJieLeiXing.长江优惠信息?12:(this.YouQingLianJieLeiXing==EyouSoft.Model.EnumType.YlStructure.WzYouQingLianJieLeiXing.海洋优惠信息?13:4) %>");
            tableToolbar.init();
            //clone toolbar、paging
            $("#select_Toolbar_Paging_1").children().clone(true).prependTo("#select_Toolbar_Paging_2");
            $(".toolbar_add").bind("click", function() { iPage.insert(); });
            $(".i_xiugai").bind("click", function() { iPage.xiuGai(this); });
            $(".i_shanchu").bind("click", function() { iPage.shanChu(this); });
        });
    </script>

</asp:Content>

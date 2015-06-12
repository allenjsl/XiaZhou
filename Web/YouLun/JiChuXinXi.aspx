<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Front.Master" AutoEventWireup="true"
    CodeBehind="JiChuXinXi.aspx.cs" Inherits="EyouSoft.Web.YouLun.JiChuXinXi" %>

<%@ Register Assembly="ControlLibrary" Namespace="ControlLibrary" TagPrefix="cc2" %>
<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>
<%@ Register Src="/UserControl/BasciSetBar.ascx" TagName="BasciSetBar" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form id="form1" runat="server">
    <div class="mainbox">
        <div style="background: none #f6f6f6;" class="tablehead">
            <ul class="fixed">
                <li><s class="orderformicon"></s><a <%= EyouSoft.Common.Utils.GetQueryStringValue("type") == "" ? "class='ztorderform de-ztorderform'" : "class='ztorderform'"%>
                    hidefocus="true" href="JiChuXinXi.aspx?sl=95"><span>航线管理</span></a></li>
                <li><s class="orderformicon"></s><a <%= EyouSoft.Common.Utils.GetQueryStringValue("type") == "1" ? "class='ztorderform de-ztorderform'" : "class='ztorderform'"%>
                    hidefocus="true" href="JiChuXinXi.aspx?sl=95&type=1"><span>港口管理</span></a></li>
                <li><s class="orderformicon"></s><a <%= EyouSoft.Common.Utils.GetQueryStringValue("type") == "2" ? "class='ztorderform de-ztorderform'" : "class='ztorderform'"%>
                    hidefocus="true" href="JiChuXinXi.aspx?sl=95&type=2"><span>国籍管理</span></a></li>
                <li><s class="orderformicon"></s><a <%= EyouSoft.Common.Utils.GetQueryStringValue("type") == "3" ? "class='ztorderform de-ztorderform'" : "class='ztorderform'"%>
                    hidefocus="true" href="JiChuXinXi.aspx?sl=95&type=3"><span>房型管理</span></a></li>
                <li><s class="orderformicon"></s><a <%= EyouSoft.Common.Utils.GetQueryStringValue("type") == "4" ? "class='ztorderform de-ztorderform'" : "class='ztorderform'"%>
                    hidefocus="true" href="JiChuXinXi.aspx?sl=95&type=4"><span>价格人员类型管理</span></a></li>
                <li><s class="orderformicon"></s><a <%= EyouSoft.Common.Utils.GetQueryStringValue("type") == "5" ? "class='ztorderform de-ztorderform'" : "class='ztorderform'"%>
                    hidefocus="true" href="JiChuXinXi.aspx?sl=95&type=5"><span>附加产品类型</span></a></li>
            </ul>
        </div>
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
                            名称
                        </th>
                        <th width="" align="center" class="th-line">
                            类型
                        </th>
                         <%if(PaiXuColumn){ %>
                        <th width="" align="center" class="th-line">
                            排序值
                        </th>
                        <%} %>
                        <th width="" align="center" class="th-line">
                            操作
                        </th>
                    </tr>
                    <cc2:CustomRepeater ID="repList" runat="server">
                        <ItemTemplate>
                            <tr class="<%#Container.ItemIndex%2==0?"":"#odd" %>">
                                <td width="50" align="center">
                                    <%#Container.ItemIndex+1%>
                                </td>
                                <td align="center">
                                    <%#Eval("MingCheng")%>
                                </td>
                                <td align="center">
                                    <%# Eval("ChangJingLeiXing")%>
                                </td>
                                <%if(PaiXuColumn){ %>
                                <td align="center">
                                    <%# Eval("PaiXuId")%>
                                </td>
                                <%} %>
                                <td align="center">
                                    <a class="update" href="javascript:;">修改</a> | <a class="del" href="javascript:;">删除</a>
                                    <input class="hiddenid" type="hidden" value="<%#Eval("XinXiId") %>" />
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
    </form>

    <script type="text/javascript">
        var pageDataJs = {
            Params: { type: '<%= EyouSoft.Common.Utils.GetQueryStringValue("type") %>', sl: '<%= EyouSoft.Common.Utils.GetQueryStringValue("sl") %>' }
        }
        $(function() {
            $(".update").click(function() {
                var url = '/YouLun/JiChuXinXiAdd.aspx?id=' + $(this).closest("td").find(".hiddenid").val() + "&" + $.param(pageDataJs.Params)
                Boxy.iframeDialog({
                    iframeUrl: url,
                    title: "修改",
                    modal: true,
                    width: "450px",
                    height: "180px"
                });
            })//修改
            $(".toolbar_add").click(function() {
                var url = '/YouLun/JiChuXinXiAdd.aspx?' + $.param(pageDataJs.Params)
                Boxy.iframeDialog({
                    iframeUrl: url,
                    title: "添加",
                    modal: true,
                    width: "450px",
                    height: "180px"
                });
            })//添加
            $(".del").click(function() {
                var url = '/YouLun/JiChuXinXi.aspx?del=1&sl=95&id=' + $(this).closest("td").find(".hiddenid").val()
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

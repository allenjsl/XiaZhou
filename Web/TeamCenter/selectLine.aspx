<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="selectLine.aspx.cs" Inherits="Web.TeamCenter.selectLine" %>

<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <title>线路选用</title>
    <link href="/css/style.css" rel="stylesheet" type="text/css" />
    <script src="/Js/jquery-1.4.4.js" type="text/javascript"></script>

    <style type="text/css">
        .bumenbox
        {
            line-height: 200%;
        }
        .bumenbox a
        {
            color: #000000;
        }
        .bumenbox a:hover
        {
            color: #FF0000;
        }
        .bottomline
        {
            border-bottom: 1px #FFFFFF solid;
            border-collapse: collapse;
        }
    </style>
</head>
<body style="background: 0 none;">
    <div class="alertbox-outbox02">
        <table width="99%" border="0" cellpadding="0" cellspacing="0" class="alertboxbk2"
            style="margin: 0 auto; border-collapse: collapse;">
            <tr>
                <td align="center">
                    <span style="font-size: 14px;">【选择线路】</span>
                </td>
            </tr>
            <tr>
                <td bgcolor="#B7E0F3" class="bumenbox">
                    <b><a href="/TeamCenter/selectLine.aspx?aid=<%=Request.QueryString["aid"]%>&sl=<%=Request.QueryString["sl"]%>&callBackFun=<%=Request.QueryString["callBackFun"]%>&iframeId=<%=Request.QueryString["iframeId"]%>">
                        所有线路区域</a></b>：<asp:Repeater ID="rptAreaList" runat="server">
                            <ItemTemplate>
                                <a href="/TeamCenter/selectLine.aspx?areaID=<%#Eval("AreaId") %>&aid=<%=Request.QueryString["aid"]%>&sl=<%=Request.QueryString["sl"]%>&callBackFun=<%=Request.QueryString["callBackFun"]%>&iframeId=<%=Request.QueryString["iframeId"]%>">
                                    <%#Eval("AreaName")%></a> |
                            </ItemTemplate>
                        </asp:Repeater>
                </td>
            </tr>
            <tr>
                <td>
                    <div class="fixed" style="width: 98%;" id="divSearch">
                        <form method="get" action="/TeamCenter/selectLine.aspx">
                        线路名称：<input type="text" name="txtRouteName" class="formsize80 bk" value="<%=Request.QueryString["txtRouteName"]%>" />
                        <button type="submit" style="width: 64px; height: 24px; background: url(/images/cx.gif) no-repeat center center;
                            border: 0 none; margin-left: 5px;">
                            查 询</button>
                        <input type="hidden" name="aid" value="<%=Request.QueryString["aid"]%>" />
                        <input type="hidden" name="areaId" value="<%=Request.QueryString["areaId"]%>" />
                        <input type="hidden" name="sl" value="<%=Request.QueryString["sl"]%>" />
                        <input type="hidden" name="callBackFun" value="<%=Request.QueryString["callBackFun"]%>" />
                        </form>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <table id="tblList" width="100%" border="0" cellpadding="0" cellspacing="0" class="alertboxbk1"
                        bgcolor="#FFFFFF" style="border-collapse: collapse; margin: 5px 0;">
                        <tr>
                            <asp:Repeater ID="rpt_List" runat="server">
                                <ItemTemplate>
                                    <td align="left">
                                        <label><input type="checkbox" name="checkbox" value="<%#Eval("RouteId") %>" />
                                        <span><%#Eval("RouteName")%></span></label>
                                    </td>
                                    <%#EyouSoft.Common.Utils.IsOutTrOrTd(Container.ItemIndex, recordCount, 4)%>
                                </ItemTemplate>
                            </asp:Repeater>
                    </table>
                </td>
            </tr>
        </table>
        <div style="position: relative; height: 20px;">
            <div class="pages">
                <cc1:ExporPageInfoSelect ID="ExporPageInfoSelect1" runat="server" />
            </div>
        </div>
        <div class="alertbox-btn">
            <span id="spanLoading" style="display:none;color:#ff0000">正在生成线路信息，请稍候....&nbsp;&nbsp;</span>
            <a href="javascript:void(0)" id="heBing" hidefocus="true"><s class="xuanzhe"></s>合并线路</a>
            <a href="javascript:void(0)" id="xuanzhe" hidefocus="true"><s class="xuanzhe"></s>选择线路</a></div>
    </div>

    <script type="text/javascript">
        var SelectLine = {
            Data: {
                aid: $("#divSearch").find("input[name='aid']").val(),
                callBackFun: $("#divSearch").find("input[name='callBackFun']").val(),
                iframeid: '<%=Request.QueryString["iframeId"]%>'
            },
            getXianZhongValues: function() {
                var items = [];
                $("#tblList").find("input[type='checkbox']:checked").each(function() {
                    var _$chk = $(this);
                    var item = { id: _$chk.val(), name: _$chk.next().html(), areaId: $("#areaId").val() };
                    items.push(item);
                });
                return items;
            },
            loading: function() {
                $("#xuanzhe").css({ color: "#999" }).unbind("click");
                $("#heBing").css({ color: "#999" }).unbind("click");
                $("#spanLoading").show();
            },
            xuanZe_Click: function(obj) {
                var items = this.getXianZhongValues();

                if (items && items.length > 3) {
                    parent.tableToolbar._showMsg("一次最多只能选择三条线路");
                    return;
                }

                if (items && items.length == 0) {
                    parent.tableToolbar._showMsg("未选择任何线路信息");
                    return;
                }

                if (items && items.length > 0) {
                    this.loading();
                    parent.window[SelectLine.Data.callBackFun](items, "1");
                }

                parent.Boxy.getIframeDialog(this.Data.iframeid).hide();
            },
            heBing_Click: function(obj) {
                var items = this.getXianZhongValues();

                if (items && items.length > 3) {
                    parent.tableToolbar._showMsg("一次最多只能选择三条线路");
                    return;
                }

                if (items && items.length == 0) {
                    parent.tableToolbar._showMsg("未选择任何线路信息");
                    return;
                }

                if (items && items.length > 0) {
                    this.loading();
                    parent.window[SelectLine.Data.callBackFun](items, "2");
                }

                parent.Boxy.getIframeDialog(this.Data.iframeid).hide();
            }
        }
        $(document).ready(function() {
            $("#xuanzhe").click(function() { SelectLine.xuanZe_Click(this); });
            $("#heBing").click(function() { SelectLine.heBing_Click(this); });
        });
    </script>

</body>
</html>

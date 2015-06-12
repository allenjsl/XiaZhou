<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TourListBoxy.aspx.cs" Inherits="EyouSoft.Web.CommonPage.TourListBoxy" %>

<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>团号选用</title>
    <link href="/css/style.css" rel="stylesheet" type="text/css" />
    <link href="/css/boxynew.css" rel="stylesheet" type="text/css" />
    <link href="/css/jquery.autocomplete.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="/js/jquery-1.4.4.js"></script>

    <script type="text/javascript" src="/js/jquery.boxy.js"></script>

    <script src="/Js/jquery.blockUI.js" type="text/javascript"></script>

    <script src="/Js/table-toolbar.js" type="text/javascript"></script>

    <script src="/Js/Newjquery.autocomplete.js" type="text/javascript"></script>

    <script src="/Js/jquery.boxy.js" type="text/javascript"></script>

    <script src="/Js/datepicker/WdatePicker.js" type="text/javascript"></script>

</head>
<body style="background: #e9f4f9;">
    <div class="alertbox-outbox">
        <div style="margin: 0 auto; width: 99%; padding-bottom: 5px;">
            <form id="SelectFrom" action="TourListBoxy.aspx" method="get">
            <table align="center" cellspacing="0" cellpadding="0" bgcolor="#e9f4f9" width="99%">
                <tbody>
                    <tr>
                        <td align="left" bgcolor="#C1E5F5" height="28" width="11%" class="alertboxTableT">
                            团队类型：
                            <label for="select">
                            </label>
                            <select name="tourType" class="inputselect ">
                                <%=EyouSoft.Common.UtilsCommons.GetEnumDDL(EyouSoft.Common.EnumObj.GetList(typeof(EyouSoft.Model.EnumType.TourStructure.TourType)), Request.QueryString["tourType"]??"-1", true)%>
                            </select>
                            团号：
                            <input type="text" class="inputtext formsize140" name="tourCode" value="<%=Request.QueryString["tourCode"]  %>">
                            出团时间：
                            <input type="text" class="inputtext formsize80" onfocus="WdatePicker({maxDate:'#F{$dp.$D(\'lDateE\')}'});"
                                name="lDateS" id="lDateS" value="<%=Request.QueryString["lDateS"]  %>">
                            -
                            <input type="text" class="inputtext formsize80" onfocus="WdatePicker({minDate:'#F{$dp.$D(\'lDateS\')}'});"
                                id="lDateE" name="lDateE" value="<%=Request.QueryString["lDateE"]  %>">
                            <input type="hidden" name="isRadio" value="<%=Request.QueryString["isRadio"] %>" />
                            <input type="submit" style="width: 64px; height: 24px; background: url(/images/cx.gif) no-repeat center center;
                                border: 0 none; margin-left: 5px;" value="查 询" />
                        </td>
                    </tr>
                </tbody>
            </table>
            <input type="hidden" name="pIframeID" value="<%=Request.QueryString["pIframeID"] %>" />
            <input type="hidden" name="iframeId" value="<%=Request.QueryString["iframeId"] %>" />
            <input type="hidden" name="callBackFun" value="<%=Request.QueryString["callBackFun"] %>" />
            </form>
            <div class="hr_10">
            </div>
            <table align="center" cellspacing="0" cellpadding="0" bordercolor="#b8c5ce" width="99%">
                <tr>
                    <asp:Repeater ID="rpt_list" runat="server">
                        <ItemTemplate>
                            <td align="left" style="padding: 3px 3px">
                                <input id="select_<%#Container.ItemIndex+1 %>" type="radio" name="rad_tour" data-tourid="<%#Eval("TourId") %>" />
                                <input id="select_<%#Container.ItemIndex+1 %>" type="checkbox" data-tourid="<%#Eval("TourId") %>" />
                                <label for="select_<%#Container.ItemIndex+1 %>">
                                    <%#Eval("TourCode")%></label>
                                <%#EyouSoft.Common.Utils.IsOutTrOrTd(Container.ItemIndex, recordCount, 4)%>
                        </ItemTemplate>
                    </asp:Repeater>
                    <asp:Panel ID="pan_msg" runat="server">
                        <tr align="center">
                            <td colspan="50">
                                暂无团号数据!
                            </td>
                        </tr>
                    </asp:Panel>
                    <tr>
                        <td height="30" colspan="5" align="right">
                            <div style="position: relative; height: 20px;">
                                <cc1:ExporPageInfoSelect ID="ExporPageInfoSelect1" runat="server" />
                            </div>
                        </td>
                    </tr>
            </table>
            <div class="alertbox-btn">
                <a href="javascript:void(0);" hidefocus="true" id="a_Save"><s class="xuanzhe"></s>选
                    择</a><a href="javascript:parent.Boxy.getIframeDialog(Boxy.queryString('iframeId')).hide();"
                        hidefocus="true"><s class="chongzhi"></s>关 闭</a>
            </div>
        </div>
    </div>

    <script type="text/javascript">
        var TourListBoxy = {
            _parentWindow: null,
            PageInit: function() {
                var that = this;

                that._parentWindow = Boxy.queryString("pIframeID") ? window.parent.Boxy.getIframeWindow(Boxy.queryString("pIframeID")) : parent;
                //控制单选多选
                var isRadio = Boxy.queryString("isRadio");
                if (isRadio) {
                    $(":checkbox").remove();
                }
                else {
                    $(":radio").remove();
                }

                $("#a_Save").click(function() {
                    var arrData = [];
                    $(":radio:checked,:checkbox:checked").each(function() {
                        arrData.push($.trim($(this).parent().text()) + "," + $.trim($(this).attr("data-tourid")))
                    })
                    var parents = that._parentWindow;
                    var callBackFun = Boxy.queryString("callBackFun");

                    //判断自定义回调,若没有则返回null
                    var callBackFunArr = callBackFun ? callBackFun.split('.') : null;
                    //存在回调函数
                    if (callBackFunArr) {
                        for (var item in callBackFunArr) {
                            if (callBackFunArr.hasOwnProperty(item)) {/*筛选掉原型链属性*/
                                parents = parents[callBackFunArr[item]];
                            }
                        }
                        parents(arrData);
                    }
                    parent.Boxy.getIframeDialog(Boxy.queryString("iframeId")).hide();
                })
            }
        }
        $(function() {
            TourListBoxy.PageInit();
        })
    </script>

</body>
</html>

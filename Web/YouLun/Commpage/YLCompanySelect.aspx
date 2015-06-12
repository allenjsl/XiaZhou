<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="YLCompanySelect.aspx.cs"
    Inherits="EyouSoft.Web.CommonPage.YLCompanySelect" %>

<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="/Css/style.css" rel="stylesheet" type="text/css" />

    <script src="/Js/jquery-1.4.4.js" type="text/javascript"></script>

</head>
<body style="background: #e9f4f9;">
    <div class="alertbox-outbox02">
        <table width="99%" cellspacing="0" cellpadding="0" class="alertboxbk1" border="0"
            style="margin: 0 auto; border-collapse: collapse;">
            <tbody>
                <tr>
                    <td height="30">
                        <form method="get">
                        公司名称：
                        <input type="hidden" id="t" name="t" class="inputtext formsize80" value='<%=Request.QueryString["t"] %>' />
                        <input type="hidden" id="id" name="id" class="inputtext formsize80" value='<%=Request.QueryString["id"] %>' />
                        <input type="hidden" id="pIframeId" name="pIframeId" class="inputtext formsize80"
                            value='<%=Request.QueryString["pIframeId"] %>' />
                        <input type="hidden" id="CallBackFun" name="CallBackFun" class="inputtext formsize80"
                            value='<%=Request.QueryString["CallBackFun"] %>' />
                        <input type="hidden" id="iframeId" name="iframeId" class="inputtext formsize80" value='<%=Request.QueryString["iframeId"] %>' />
                        <input type="text" id="comName" name="comName" class="inputtext formsize80" value='<%=Request.QueryString["comName"] %>' />
                        <input type="submit" value="查询" style="width: 64px; height: 24px; background: url(/images/cx.gif) no-repeat center center;
                            border: 0 none; margin-left: 5px;" id="btnSearch" />
                        </form>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table id="tblList" width="100%" cellspacing="0" cellpadding="0" class="alertboxbk1"
                            border="0" bgcolor="#FFFFFF" style="border-collapse: collapse; margin: 5px 0;">
                            <tbody>
                                <tr>
                                    <asp:Repeater ID="rptList" runat="server">
                                        <ItemTemplate>
                                            <td align="left">
                                                <label>
                                                    <input type="radio" name="contactID" value="<%#Eval("GongSiId")%>" data-name="<%#Eval("MingCheng")%>"
                                                        data-xilie="<%# (int)Eval("LeiXing")%>" />
                                                    <span>
                                                        <%#Eval("MingCheng")%></span>
                                                </label>
                                                <%#EyouSoft.Common.Utils.IsOutTrOrTd(Container.ItemIndex, listCount, 4)%>
                                        </ItemTemplate>
                                    </asp:Repeater>
                            </tbody>
                        </table>
                        <div style="width: 100%; text-align: center; background-color: #ffffff">
                            <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                        </div>
                    </td>
                </tr>
            </tbody>
        </table>
        <div style="position: relative; height: 25px;">
            <div class="pages" id="divPages">
                <cc1:ExporPageInfoSelect ID="ExporPageInfoSelect1" runat="server" />
            </div>
        </div>
        <div class="alertbox-btn">
            <a id="a_btn" hidefocus="true" href="javascript:void(0);"><s class="xuanzhe"></s>选 择</a></div>
    </div>

    <script type="text/javascript">
        var YLSelectPage = {

            aid: '<%=Request.QueryString["id"] %>',
            parentWindow: null, //要赋值的页面的window对象
            iframeID: '<%=Request.QueryString["iframeId"]%>', //当前弹窗ID
            pIframeID: '<%=Request.QueryString["pIframeId"]%>', //父级弹窗ID
            BackFun: '<%=  Request.QueryString["CallBackFun"]%>'
        }
        $(function() {
            //获得需要赋值页面的window 对象
            if (YLSelectPage.pIframeID) {
                YLSelectPage.parentWindow = window.parent.Boxy.getIframeWindow(YLSelectPage.pIframeID) || window.parent.Boxy.getIframeWindowByID(YLSelectPage.pIframeID);
            }
            else {
                YLSelectPage.parentWindow = parent.window;
            }
            $("#a_btn").click(function() {
                if (YLSelectPage.pIframeID) {
                    $("#" + YLSelectPage.aid, window.top.frames[YLSelectPage.pIframeID].document).closest("span").find("input[type=text]").val($("input:checked").attr("data-name"));
                    $("#" + YLSelectPage.aid, window.top.frames[YLSelectPage.pIframeID].document).closest("span").find("input[type=hidden]").val($("input:checked").val());
                }
                else {
                    $("#" + YLSelectPage.aid, window.top.document).closest("span").find("input[type=text]").val($("input:checked").attr("data-name"));
                    $("#" + YLSelectPage.aid, window.top.document).closest("span").find("input[type=hidden]").val($("input:checked").val());
                }
                var data = {
                    gongsi: $("input:checked").val(),
                    hangxian: $("input:checked").attr("data-xilie")
                };

                if (YLSelectPage.BackFun) {
                    if (YLSelectPage.BackFun.indexOf('.') == -1) {
                        YLSelectPage.parentWindow[YLSelectPage.BackFun](data);
                    } else {
                        YLSelectPage.parentWindow[YLSelectPage.BackFun.split('.')[0]][YLSelectPage.BackFun.split('.')[1]](data);
                    }
                }


                parent.Boxy.getIframeDialog(YLSelectPage.iframeID).hide()
            })
        }) 
    </script>

</body>
</html>

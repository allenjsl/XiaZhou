<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PayStyle.aspx.cs" Inherits="EyouSoft.Web.FinanceManage.KingdeeSubject.PayStyle" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>支付方式-选用</title>
    <link href="/css/style.css" rel="stylesheet" type="text/css" />
    <link href="/css/boxynew.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="/js/jquery-1.4.4.js"></script>

    <script src="/Js/jquery.boxy.js" type="text/javascript"></script>

    <script type="text/javascript" src="/Js/jquery.blockUI.js"></script>

    <script type="text/javascript" src="/Js/table-toolbar.js"></script>

</head>
<body style="background: 0 none;">
    <div class="alertbox-outbox">
        <div style="text-align: center; padding: 10px 10px 10px 10px;">
            <table width="99%" border="0" cellspacing="0" cellpadding="0" class="noborderInput">
                <tr>
                    <td colspan="4" style="padding: 5px 5px">
                        <b>支付方式选用</b>
                    </td>
                </tr>
                <tr>
                    <td width="30" height="23" align="center" bgcolor="#b7e0f3" class="alertboxTableT">
                        选择
                    </td>
                    <td align="center" bgcolor="#b7e0f3" class="alertboxTableT">
                        支付方式
                    </td>
                    <td align="center" bgcolor="#b7e0f3" class="alertboxTableT">
                        支付类型
                    </td>
                    <td align="center" bgcolor="#b7e0f3" class="alertboxTableT">
                        款项来源
                    </td>
                </tr>
                <asp:Repeater ID="rpt_list" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td align="center" style="padding: 3px 3px">
                                <input type="radio" name="radio" data-paymentid="<%#Eval("PaymentId") %>" data-name="<%#Eval("Name")%>"/>
                            </td>
                            <td>
                                <%#Eval("Name")%>
                            </td>
                            <td>
                                <%#Eval("ItemType")%>
                            </td>
                            <td>
                                <%#Eval("SourceType")%>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
                <tr>
                    <td height="30" colspan="5" align="right">
                        <div style="position: relative; height: 20px;">
                        </div>
                    </td>
                </tr>
                <asp:Panel ID="pan_msg" runat="server">
                    <tr>
                        <td colspan="4">
                            暂无数据!
                        </td>
                    </tr>
                </asp:Panel>
            </table>
        </div>
        <div class="alertbox-btn">
            <a href="javascript:void(0);" hidefocus="true" id="a_btn"><s class="xuanzhe"></s>选 择</a></div>
    </div>

    <script type="text/javascript">
        var PayStyle = {
            _parentWindow: null,
            PageInit: function() {
                var that = this;
                that._parentWindow = Boxy.queryString("pIframeID") ? window.parent.Boxy.getIframeWindow(Boxy.queryString("pIframeID")) : parent;
                $("#a_btn").click(function() {
                    var arrData = [];
                    $(":checkbox:checked,:radio:checked").each(function() {
                        var obj = $(this)
                        arrData.push($.trim(obj.attr("data-name")) + "," + obj.attr("data-paymentid"))
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
            PayStyle.PageInit();
        })
    </script>

</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChangeInfo.aspx.cs" Inherits="Web.OperaterCenter.ChangeInfo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="/css/style.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="/Js/jquery-1.4.4.js"></script>

    <script type="text/javascript" src="/Js/ValiDatorForm.js"></script>

    <script type="text/javascript" src="/Js/datepicker/WdatePicker.js"></script>

    <script type="text/javascript" src="/Js/table-toolbar.js"></script>

    <script type="text/javascript" src="/Js/jquery.blockUI.js"></script>

</head>
<body style="background: 0 none;">
    <form id="form1" runat="server">
    <div class="alertbox-outbox">
        <div style="height: 30px; line-height: 30px; width: 98%; margin: auto; font-size: 14px;">
            <strong>当前变更</strong></div>
        <table width="98%" align="center" cellpadding="0" cellspacing="0" bgcolor="#e9f4f9"
            style="margin: 0 auto">
            <asp:PlaceHolder ID="changeTitleView" runat="server">
                <tr>
                    <td width="15%" height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                        变更标题：
                    </td>
                    <td colspan="3" align="left">
                        <asp:Literal ID="litChangeTitle" runat="server"></asp:Literal>
                    </td>
                </tr>
            </asp:PlaceHolder>
            <tr>
                <td width="15%" height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    变更人：
                </td>
                <td width="35%" align="left" bgcolor="#e0e9ef">
                    <asp:Literal ID="litChangeName" runat="server"></asp:Literal>
                </td>
                <td width="15%" height="28" align="right" bgcolor="#B7E0F3" class="alertboxTableT">
                    变更时间：
                </td>
                <td width="35%" height="28" bgcolor="#e0e9ef">
                    <asp:Literal ID="litChangeTime" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td width="15%" height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    变更内容：
                </td>
                <td colspan="3" style="padding: 6px 4px;">
                    <asp:Literal ID="litChangeContect" runat="server"></asp:Literal>
                </td>
            </tr>
        </table>
        <div class="alertbox-btn">
            <asp:PlaceHolder ID="btnCheckView" runat="server"><a href="javascript:" hidefocus="true"
                data-class="confirmChange">确认变更</a> </asp:PlaceHolder>
            <a href="javascript:" hidefocus="true" onclick="window.parent.Boxy.getIframeDialog('<%=Request.QueryString["iframeId"] %>').hide();">
                <s class="chongzhi"></s>关闭</a>
        </div>
    </div>
    </form>

    <script type="text/javascript">
        $(document).ready(function() {
            var _sl = '<%=SL %>';
            var id = '<%=EyouSoft.Common.Utils.GetQueryStringValue("Id") %>';
            var type = '<%=EyouSoft.Common.Utils.GetQueryStringValue("type") %>';
            $(".alertbox-btn").find("[data-class='confirmChange']").click(function() {
                $.newAjax({
                    type: "POST",
                    url: '/OperaterCenter/ChangeInfo.aspx?action=save&sl=' + _sl + '&changeId=' + id + "&type=" + type,
                    cache: false,
                    async: false,
                    dataType: "json",
                    success: function(ret) {
                        if (ret.result == "1") {
                            parent.tableToolbar._showMsg(ret.msg, function() {
                                window.parent.Boxy.getIframeDialog('<%=Request.QueryString["iframeId"] %>').hide();
                                window.parent.location.href = window.parent.location.href;
                            });
                        } else {
                            parent.tableToolbar._showMsg(ret.msg);
                        }
                    },
                    error: function() {
                        parent.tableToolbar._showMsg(tableToolbar.errorMsg, function() {
                            return false;
                        });
                    }
                });
            });
        });
    </script>

</body>
</html>

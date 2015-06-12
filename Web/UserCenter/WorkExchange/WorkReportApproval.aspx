<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WorkReportApproval.aspx.cs"
    Inherits="Web.UserCenter.WorkExchange.WorkReportApproval" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>个人中心-工作汇报-审批</title>
    <link href="/css/style.css" rel="stylesheet" type="text/css" />
    <link href="/css/boxynew.css" rel="stylesheet" type="text/css" />

    <script src="/Js/jquery-1.4.4.js" type="text/javascript"></script>

    <script src="/Js/datepicker/WdatePicker.js" type="text/javascript"></script>

    <script type="text/javascript" src="/js/table-toolbar.js"></script>

    <script src="/Js/jquery.blockUI.js" type="text/javascript"></script>

</head>
<body style="background: 0 none;">
    <form id="form1" runat="server">
    <div class="alertbox-outbox">
        <table width="99%" align="center" cellpadding="0" cellspacing="0" bgcolor="#e9f4f9"
            style="margin: 0 auto">
            <tr>
                <td width="12%" height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    汇报标题：
                </td>
                <td width="45%" height="28" align="left">
                    <asp:Label ID="lblTitle" runat="server" Text=""></asp:Label>
                </td>
                <td width="12%" align="right" bgcolor="#B7E0F3" class="alertboxTableT">
                    汇报人：
                </td>
                <td width="31%" align="left">
                    <asp:Label ID="lblOperatorName" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    汇报部门：
                </td>
                <td align="left">
                    <asp:Label ID="lblDepartment" runat="server" Text=""></asp:Label>
                </td>
                <td align="right" bgcolor="#B7E0F3" class="alertboxTableT">
                    汇报时间：
                </td>
                <td align="left">
                    <asp:Label ID="lblIssueTime" runat="server" Text=""></asp:Label>
                </td>
            </tr>
        </table>
        <div class="hr_10">
        </div>
        <div class="tablelist-box">
            <asp:Literal ID="litList" runat="server"></asp:Literal>
        </div>
        <div class="alertbox-btn">
            <asp:PlaceHolder ID="phdPrint" runat="server"><a href="javascript:window.print();"
                hidefocus="true">打 印</a> </asp:PlaceHolder>
            <a href="javascript:void(0)" hidefocus="true" onclick="parent.Boxy.getIframeDialog('<%=Request.QueryString["iframeId"]%>').hide();return false;">
                <s class="chongzhi"></s>关 闭</a>
        </div>
    </div>
    <%--存储工作汇报ID--%>
    <asp:HiddenField ID="hidID" runat="server" />
    </form>

    <script type="text/javascript">

        var parames = { WorkId: "", ApproveTime: "", listId: "", Comment: "", Status: -1 }
        var WorkReportApproval = {
            data: {
                sl: '<%=EyouSoft.Common.Utils.GetQueryStringValue("sl") %>'
            },
            SetWorkReportStatus: function() {
                $.newAjax({
                    type: "post",
                    cache: false,
                    url: "WorkReportApproval.aspx?doType=AjaxSetStatus&sl=" + WorkReportApproval.data.sl,
                    data: parames,
                    dataType: "json",
                    success: function(ret) {
                        if (ret.result == "true") {
                            tableToolbar._showMsg(ret.msg, function() {
                                if (ret.result == "true")
                                    window.parent.frames.location.reload();
                            });
                        } else {
                            tableToolbar._showMsg(ret.msg);
                        }

                    },
                    error: function() {
                        tableToolbar._showMsg(tableToolbar.errorMsg);
                    }
                });
            },
            IntiData: function() {
                parames.WorkId = $("#hidID").val();
                parames.ApproveTime = $("#txtApproveTime").val();
                parames.listId = $("#chbStatus").val();
                parames.Comment = $("#txtComment").val();
                if ($("#chbStatus").attr("checked"))
                    parames.Status = 1;
                else
                    parames.Status = 0;

                if (parames.Status == 0)
                { tableToolbar._showMsg("请选择状态"); return }
                WorkReportApproval.SetWorkReportStatus();
            }
        }
    
    </script>

</body>
</html>

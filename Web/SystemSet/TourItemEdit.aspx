<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeBehind="TourItemEdit.aspx.cs"
    Inherits="Web.SystemSet.TourItemEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <title>无标题文档</title>
    <link href="/css/style.css" rel="stylesheet" type="text/css" />  
    <script src="/Js/jquery-1.4.4.js" type="text/javascript"></script>

    <script src="/Js/table-toolbar.js" type="text/javascript"></script>

    <script src="/Js/jquery.blockUI.js" type="text/javascript"></script>
</head>
<body style="background: 0 none;">
    <form id="form1" runat="server">
    <div class="alertbox-outbox02">
        <table width="99%" cellspacing="0" cellpadding="0" bgcolor="#e9f4f9" align="center"
            style="margin: 0 auto">
            <tbody>
                <tr>
                    <td align="center">
                        <asp:TextBox ID="txtContent" Style="height: 80px;" runat="server" TextMode="MultiLine"
                           CssClass=" inputtext formsize600"></asp:TextBox>
                    </td>
                </tr>
            </tbody>
        </table>
        <div class="alertbox-btn">
            <asp:LinkButton ID="btnSave" runat="server" OnClick="btnSave_Click" OnClientClick=" return TourItemEdit.Save();"><s class="xiugai"></s>修 改</asp:LinkButton><a
                hidefocus="true" href="javascript:" onclick="parent.Boxy.getIframeDialog('<%=Request.QueryString["iframeId"]%>').hide(); return false;"><s
                    class="chongzhi"></s>取 消</a>
        </div>
    </div>
    </form>

    <script type="text/javascript">
        var TourItemEdit = {
            Save: function() {
                var msg = "";
                var txtContent = document.getElementById("<%=txtContent.ClientID%>").value;
                if (txtContent == "") {
                    msg += "项目名称不能为空!<br/>";
                }

                if (msg != "") {
                    parent.tableToolbar._showMsg(msg);
                    return false;
                }
                return true;
            }
        };
    </script>

</body>
</html>
87217225
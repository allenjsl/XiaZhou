<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserAuthorize.aspx.cs"
    Inherits="Web.SystemSet.UserAuthorize" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />

    <script src="/Js/jquery-1.4.4.js" type="text/javascript"></script>

    <script src="/Js/table-toolbar.js" type="text/javascript"></script>

    <script src="/Js/jquery.blockUI.js" type="text/javascript"></script>

    <link href="/Css/style.css" rel="stylesheet" type="text/css" />
    <title>ϵͳ����-Ȩ�޹���-���</title>
</head>
<body style="background: #fff;">
    <form id="form1" runat="server">
    <table width="99%" cellspacing="0" cellpadding="0" border="0" align="center">
        <tbody>
            <tr>
                <td height="40" align="center" style="font-size: 14px">
                    <strong>Ȩ����ѡ��</strong>
                    <asp:DropDownList ID="ddlRole" runat="server">
                    </asp:DropDownList>
                    <asp:HiddenField ID="hidPower" runat="server" />
                </td>
            </tr>
        </tbody>
    </table>
    <%=PowerStr %>
    <div class="mainbox cunline fixed">
        <ul>
            <li class="cun-cy">
                <asp:LinkButton ID="btnSave" runat="server" OnClick="btnSave_Click">����</asp:LinkButton></li>
            <li class="quxiao-cy"><a href="javascript:" onclick="parent.Boxy.getIframeDialog('<%=Request.QueryString["iframeId"]%>').hide();">
                ȡ ��</a></li>
        </ul>
        <div class="hr_10">
        </div>
    </div>
    </form>

    <script type="text/javascript">
        $("[name='chkAll']").click(function() {
            var MenuId = $(this).attr("value");
            var Checked = $(this).attr("checked");
            $("[MenuId='" + MenuId + "']").each(function() {
                $(this).attr("checked", Checked);
                var Menu2Id = $(this).attr("Menu2Id");
                $("[Menu2Id=" + Menu2Id + "]").attr("checked", Checked);
            });
        });

        $("[name='chkMenu']").click(function() {
            var Menu2Id = $(this).attr("Menu2Id");
            $("[Menu2Id=" + Menu2Id + "]").attr("checked", $(this).attr("checked"));
        });

        $("#<%=ddlRole.ClientID%>").change(function() {
            //����Ȩ�޶�ȡ��ѡ�С�
            $("[name='chkPower']").attr("checked", false);
            //��ǰѡ�еĽ�ɫ���
            var selectValue = this.value;
            //0����δѡ��
            if (selectValue != "0") {
                var date = eval($("#<%=hidPower.ClientID%>").val());
                for (var i = 0; i < date.length; i++) {
                    if (date[i].RoleId == selectValue) {
                        //��ɫ������Ȩ��ID,ȫ��ѡ�С�
                        var ids = date[i].PowerIds.split(",");
                        for (var j = 0; j < ids.length; j++) {
                            $("[name='chkPower'][value='" + ids[j] + "']").attr("checked", true);
                        }
                    }
                }
            }
        });
        
    </script>

</body>
</html>

<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeBehind="PayStyleEdit.aspx.cs"
    Inherits="Web.SystemSet.PayStyleEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <title>�ޱ����ĵ�</title>

    <script src="/Js/jquery-1.4.4.js" type="text/javascript"></script>

    <script src="/Js/table-toolbar.js" type="text/javascript"></script>

    <script src="/Js/jquery.blockUI.js" type="text/javascript"></script>

    <link href="/css/style.css" rel="stylesheet" type="text/css" />
</head>
<body style="background: 0 none;">
    <form id="form1" runat="server">
    <div class="alertbox-outbox02">
        <table width="99%" cellspacing="0" cellpadding="0" bgcolor="#e9f4f9" align="center"
            style="margin: 0 auto">
            <tbody>
                <tr>
                    <td width="9%" height="28" align="right" class="alertboxTableT">
                        ֧����ʽ��
                    </td>
                    <td width="35%" height="28" align="left">
                        <asp:TextBox ID="txtPayStyleName" CssClass=" inputtext formsize180" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td height="28" align="right" class="alertboxTableT">
                        ֧�����
                    </td>
                    <td height="28" align="left">
                        <asp:DropDownList ID="ddlPayType" runat="server" CssClass="inputselect">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td height="28" align="right" class="alertboxTableT">
                        &nbsp;
                    </td>
                    <td height="28" align="left">
                        <table>
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="RbtnSourceType" runat="server" RepeatDirection="Horizontal">
                                    </asp:RadioButtonList>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlBankList" runat="server" CssClass="inputselect">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </tbody>
        </table>
        <div style="text-align: center;" class="alertbox-btn">
            <a href="##" id="btnSave" onclick="return PayStyleEdit.Save();"><s class="baochun"></s>
                �� ��</a> <a hidefocus="true" href="javascript:" onclick="parent.Boxy.getIframeDialog('<%=Request.QueryString["iframeId"]%>').hide(); return false;">
                    <s class="chongzhi"></s>�� ��</a>
        </div>
    </div>
    </form>

    <script type="text/javascript">
        var PayStyleEdit = {
            Params: { memuid: querystring(location.href, "memuid"), sl: querystring(location.href, "sl"), id: '<%=Request.QueryString["id"] %>' },

            Init: function() {
            //�ֽ�
            if ($("#<%=RbtnSourceType.ClientID %>").find(":checked").val() == "<%=(int)EyouSoft.Model.EnumType.ComStructure.SourceType.�ֽ� %>" || PayStyleEdit.Params.id=="") {
                    $("#<%=ddlBankList.ClientID%>").css("display", "none");
                }
                $("[name=RbtnSourceType]").eq(0).click(function() {
                    $("#<%=ddlBankList.ClientID%>").css("display", "none");
                });
                $("[name=RbtnSourceType]").eq(1).click(function() {
                    $("#<%=ddlBankList.ClientID%>").css("display", "block");
                });
            },
            Save: function() {
                var txtPayStyleName = $("#<%=txtPayStyleName.ClientID %>").val();
                var ddlBankList = $("#<%=ddlBankList.ClientID %>").val();
                var RbtnSourceType = $("#<%=RbtnSourceType.ClientID %>").find(":checked").val();
                var msg = "";
                if (txtPayStyleName == "") {
                    msg += "������֧����ʽ����!<br/>";
                }
                if (RbtnSourceType != "" && RbtnSourceType != undefined) {
                    if (RbtnSourceType == "<%=((int)EyouSoft.Model.EnumType.ComStructure.SourceType.����).ToString()%>") {
                        if (ddlBankList == "") {
                            msg += "��ѡ������!<br/>";
                        }
                    }
                } else {
                    msg += "��ѡ��֧����ʽ!<br/>";
                }
                if (msg != "") {
                    parent.tableToolbar._showMsg(msg);
                    return false;
                }
                $.newAjax({
                    type: "post",
                    cache: false,
                    url: "/SystemSet/PayStyleEdit.aspx?dotype=save&" + $.param(PayStyleEdit.Params),
                    data: $("#btnSave").closest("form").serialize(),
                    dataType: "json",
                    success: function(ret) {
                        //ajax�ط���ʾ
                        if (ret.result == "1") {
                            parent.tableToolbar._showMsg(ret.msg, function() { parent.window.location.href = '/SystemSet/PayStyleList.aspx?sl=' + querystring(location.href, "sl") + "&memuid=" + querystring(location.href, "memuid") });
                        } else {
                            parent.tableToolbar._showMsg(ret.msg);
                        }
                    },
                    error: function() {
                        parent.tableToolbar._showMsg("����ʧ�ܣ����Ժ�����!");
                    }
                });
            }
        };
        $(function() {
            PayStyleEdit.Init();
        });
    </script>

</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InsuranceEdit.aspx.cs"
    Inherits="EyouSoft.Web.SystemSet.InsuranceEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta content="text/html; charset=gb2312" http-equiv="Content-Type">
    <title>��������-�ͻ��ȼ�-�޸�</title>

    <script src="/Js/jquery-1.4.4.js" type="text/javascript"></script>

    <script src="/Js/table-toolbar.js" type="text/javascript"></script>

    <script src="/Js/jquery.blockUI.js" type="text/javascript"></script>

    <link href="/css/style.css" rel="stylesheet" type="text/css" />
    <link type="text/css" rel="stylesheet" href="/css/boxynew.css" />
</head>
<body>
    <form id="form1" runat="server">
    <div class="alertbox-outbox02">
        <table width="99%" cellspacing="0" cellpadding="0" bgcolor="#e9f4f9" align="center">
            <tbody>
                <tr>
                    <td width="36%" height="28" align="right" class="alertboxTableT">
                        �������ƣ�
                    </td>
                    <td width="64%" height="28" align="left">
                        <asp:TextBox ID="txtInsName" runat="server" CssClass=" inputtext formsize180" MaxLength="30"
                            valid="request" errmsg="�����뱣�����ƣ�"></asp:TextBox>
                </tr>
                <tr>
                    <td height="28" align="right" class="alertboxTableT">
                        ���ۣ�
                    </td>
                    <td height="28" align="left">
                        <asp:TextBox ID="txtInsPrice" runat="server" CssClass=" inputtext formsize40" MaxLength="5"
                            valid="request|isMoney" errmsg="�����뵥�ۣ�|��������ȷ���۸�ʽ��"></asp:TextBox>
                    </td>
                </tr>
            </tbody>
        </table>
        <div class="alertbox-btn" style="text-align: center;">
            <a hidefocus="true" href="javascript:" id="btnSave" onclick="return InsuranceEdit.Save();">
                <s class="baochun"></s>�� ��</a> <a hidefocus="true" href="javascript:" onclick="parent.Boxy.getIframeDialog('<%=Request.QueryString["iframeId"]%>').hide();return false;">
                    <s class="chongzhi"></s>�� ��</a>
        </div>
    </div>

    <script type="text/javascript">
        var InsuranceEdit = {
            Params:{sl:"<%=EyouSoft.Common.Utils.GetQueryStringValue("sl") %>",memuid:"<%=EyouSoft.Common.Utils.GetQueryStringValue("memuid") %>",id:"<%=EyouSoft.Common.Utils.GetQueryStringValue("id") %>"
            },
            UnBindBtn: function() {
                $("#btnSave").html("<s class='baochun'></s>�ύ��...").unbind("click").css("background-position", "0-57px");
            },
            //��ť���¼�
            BindBtn: function() {
                $("#btnSave").bind("click").css("background-position", "0-28px").html("<s class='baochun'></s>�� ��");
                $("#btnSave").click(function() {
                    InsuranceEdit.Save();
                    return false;
                });
            },
            CheckDate:function(){
                var msg = "";
                var InsName = document.getElementById("<%=txtInsName.ClientID%>").value;
                var InsPrice = document.getElementById("<%=txtInsPrice.ClientID%>").value;
                if (InsName == "") {
                    msg += "�������Ʋ���Ϊ��!<br/>";
                }
                if (InsPrice == "") {
                    msg += "���۲���Ϊ��!<br/>"
                }
                else {
                    if (!/^\d+(\.\d+)?$/.test(InsPrice)) {
                        msg += "���۸�ʽ��������!";
                    }
                }
                if(msg!=""){
                    parent.tableToolbar._showMsg(msg);
                    return false;
                }
                return true;
            },
            Save: function() {
                InsuranceEdit.CheckDate();
                InsuranceEdit.UnBindBtn();
                $.newAjax({
                    type: "post",
                    cache: false,
                    url: "/SystemSet/InsuranceEdit.aspx?dotype=save&"+$.param(InsuranceEdit.Params),
                    data: $("#btnSave").closest("form").serialize(),
                    dataType: "json",
                    success: function(ret) {
                        //ajax�ط���ʾ
                        if (ret.result == "1") {
                            parent.tableToolbar._showMsg(ret.msg,function(){parent.window.location.href="/SystemSet/InsuranceList.aspx?&"+$.param(InsuranceEdit.Params);});
                        } else {
                            parent.tableToolbar._showMsg(ret.msg);
                        }
                        InsuranceEdit.BindBtn();
                    },
                    error: function() {
                        parent.tableToolbar._showMsg("����ʧ�ܣ����Ժ�����!");
                        InsuranceEdit.BindBtn();
                    }
                });
            }
        };
    </script>

    </form>
</body>
</html>

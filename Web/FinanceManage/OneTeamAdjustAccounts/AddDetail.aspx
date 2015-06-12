<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddDetail.aspx.cs" Inherits="Web.FinanceManage.OneTeamAdjustAccounts.AddDetail" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>其他</title>
    <link href="/css/style.css" rel="stylesheet" type="text/css" />
    <link href="/css/boxynew.css" rel="stylesheet" type="text/css" />

    <script src="/Js/jquery-1.4.4.js" type="text/javascript"></script>

    <script src="/Js/jquery.blockUI.js" type="text/javascript"></script>

    <script src="/Js/table-toolbar.js" type="text/javascript"></script>

    <script src="/Js/ValiDatorForm.js" type="text/javascript"></script>

</head>
<body style="background: 0 none;">
    <form id="form1" runat="server">
    <div class="alertbox-outbox02">
        <table width="99%" border="0" cellspacing="0" cellpadding="0" style="margin: 0 auto;">
            <tr>
                <td width="18%" height="34" align="right">
                    <%= typeTitle %>类别：
                </td>
                <td width="82%">
                    <asp:TextBox ID="txt_getsetType" runat="server" class="formsize180" valid="required"
                        errmsg="类别不能为空！"></asp:TextBox>
                    <span id="errMsg_txt_getsetType" class="fontred"></span>
                </td>
            </tr>
            <tr>
                <td height="34" align="right">
                    <%= unitTitle %>单位：
                </td>
                <td>
                    <asp:TextBox ID="txt_getsetUnit" runat="server" class="formsize180" valid="required"
                        errmsg="单位不能为空！"></asp:TextBox>
                    <span id="errMsg_txt_getsetUnit" class="fontred"></span>
                </td>
            </tr>
            <tr>
                <td height="34" align="right">
                    金额：
                </td>
                <td>
                    <asp:TextBox ID="txt_Money" runat="server" class="formsize80" valid="required" errmsg="金额不能为空！"></asp:TextBox>
                    <span id="errMsg_txt_Money" class="fontred"></span>
                </td>
            </tr>
            <tr>
                <td height="34" align="right">
                    备注：
                </td>
                <td>
                    <textarea name="textarea" id="textarea" style="width: 240px; height: 60px;"></textarea>
                </td>
            </tr>
        </table>
        <div class="alertbox-btn">
            <asp:LinkButton ID="lbtn_Save" runat="server" OnClick="lbtn_Save_Click" OnClientClick="return Tset() "><s class="baochun"></s>保 存</asp:LinkButton><a
                href="#" hidefocus="true"><s class="chongzhi"></s>重 置</a>
        </div>
    </div>
    </form>

    <script type="text/javascript">
        $(function() {
            FV_onBlur.initValid($("#lbtn_Save").closest("form").get(0));
        })
        function Tset() {
            var form = $("#lbtn_Save").closest("form").get(0);
            return ValiDatorForm.validator(form, "span");
        }
        
    </script>

</body>
</html>

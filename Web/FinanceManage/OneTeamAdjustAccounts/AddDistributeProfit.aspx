<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddDistributeProfit.aspx.cs"
    Inherits="Web.FinanceManage.OneTeamAdjustAccounts.AddDistributeProfit" %>

<%@ Register Src="/UserControl/SelectSection.ascx" TagName="SelectSection" TagPrefix="uc1" %>
<%@ Register Src="/UserControl/SellsSelect.ascx" TagName="SellsSelect" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>利润分配</title>
    <link href="/css/style.css" rel="stylesheet" type="text/css" />
    <link href="/css/boxynew.css" rel="stylesheet" type="text/css" />

    <script src="/Js/jquery-1.4.4.js" type="text/javascript"></script>

    <script src="/Js/jquery.boxy.js" type="text/javascript"></script>

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
                    分配类别：
                </td>
                <td width="82%">
                    <asp:TextBox ID="txt_distributeType" runat="server" class="formsize180" valid="required"
                        errmsg="类别不能为空！"></asp:TextBox>
                    <span id="errMsg_txt_distributeType" class="fontred"></span>
                </td>
            </tr>
            <tr>
                <td height="34" align="right">
                    金额：
                </td>
                <td>
                    <asp:TextBox ID="txt_Money" runat="server" class="formsize40" valid="required" errmsg="金额不能为空！"></asp:TextBox>
                    <span id="errMsg_txt_Money" class="fontred"></span>
                </td>
            </tr>
            <tr>
                <td height="34" align="right">
                    部门：
                </td>
                <td>
                    <uc1:SelectSection ID="SelectSection1" runat="server" />
                </td>
            </tr>
            <tr>
                <td height="34" align="right">
                    人员：
                </td>
                <td>
                    <uc2:SellsSelect ID="SellsSelect1" runat="server" />
                </td>
            </tr>
            <tr>
                <td height="34" align="right">
                    备注：
                </td>
                <td>
                    <input name="textfield3" type="text" class="formsize180" id="textfield3" />
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

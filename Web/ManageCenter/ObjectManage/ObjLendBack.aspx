<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ObjLendBack.aspx.cs" Inherits="Web.ManageCenter.ObjectManage.ObjLendBack" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="/css/style.css" rel="stylesheet" type="text/css" />
    <link href="/css/boxynew.css" rel="stylesheet" type="text/css" />   
    <script type="text/javascript" src="/js/jquery-1.4.4.js"></script>
    <script src="/Js/table-toolbar.js" type="text/javascript"></script>
    <style type="text/css">
        .errmsg{color:#f00;font-size:12px;}
    </style>
</head>
<body style="background: 0 none;">
<form id="form1" method="post" runat="server">
    <div class="alertbox-outbox">
        <table width="99%" align="center" cellpadding="0" cellspacing="0" bgcolor="#e9f4f9" class="tableForm"
            style="margin: 0 auto">
            <tr>
                <td width="9%" height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    物品名称：
                </td>
                <td width="35%" height="28" align="left" bgcolor="#e0e9ef" class="objInfo">
                     <asp:Label runat="server" ID="lbName"></asp:Label>
                </td>
            </tr>
            <tr>
                <td height="28" rowspan="2" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    借阅时间：
                </td>
                <td height="28" align="left" bgcolor="#e0e9ef">
                    借出时间
                    <asp:Label runat="server" ID="lbOutTime"></asp:Label>
                </td>
            </tr>
            <tr>
                <td height="28" align="left" bgcolor="#e0e9ef">
                    <span class="errmsg" id="spanInTime">*</span>归还时间
                    <input name="txtInTime" type="text" class="inputtext formsize120" id="txtInTime" valid="required" errmsg="归还时间不能为空！"  onfocus="WdatePicker({dateFmt:'yyyy-MM-dd HH:mm'})"  runat="server"/>
                    <span id="errMsg_txtInTime" class="errmsg"></span>
                </td>
            </tr>
            <tr>
                <td height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    借阅部门：
                </td>
                <td height="28" align="left" bgcolor="#e0e9ef">
                     <asp:Label runat="server" ID="lbPart"></asp:Label>
                </td>
            </tr>
            <tr>
                <td width="9%" height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    借 阅 人：
                </td>
                <td width="35%" height="28" align="left" bgcolor="#e0e9ef">
                     <asp:Label runat="server" ID="lbGetor"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    借阅数量：
                </td>
                <td height="28" align="left" bgcolor="#e0e9ef">
                     <asp:Label runat="server" ID="lbCount"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    用&nbsp;&nbsp;&nbsp; 途：
                </td>
                <td height="46" align="left" bgcolor="#e0e9ef">
                     <asp:Label runat="server" ID="lbUse"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    经 办 人：
                </td>
                <td height="28" align="left" bgcolor="#e0e9ef">
                    <asp:Label runat="server" ID="lbHandler"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    状&nbsp;&nbsp;&nbsp; 态：
                </td>
                <td height="28" align="left" bgcolor="#e0e9ef">
                     <asp:Label runat="server" ID="lbState"></asp:Label>
                </td>
            </tr>
        </table>
        <div class="alertbox-btn" style="text-align: center;">
            <asp:LinkButton ID="btnSubmit" runat="server" hidefocus="true"  OnClick="btnClick"><s class="baochun"></s>保 存</asp:LinkButton>
            <a href="javascript:resetForm();" hidefocus="true"><s class="chongzhi"></s>重 置</a>
        </div>
    </div>
</form>
    <script src="/Js/jquery.boxy.js" type="text/javascript"></script>
    <script src="/Js/jquery.blockUI.js" type="text/javascript"></script>
    <script src="/Js/datepicker/WdatePicker.js" type="text/javascript"></script>
    <script src="/Js/ValiDatorForm.js" type="text/javascript"></script>
        <link href="/Css/jquery.autocomplete.css" rel="stylesheet" type="text/css" />
    <script src="/Js/Newjquery.autocomplete.js" type="text/javascript"></script>
        <script type="text/javascript">
        $(function() {
            $("#btnSubmit").click(function() {
                var form = $(this).closest("form").get(0);
                //点击按纽触发执行的验证函数
                return ValiDatorForm.validator(form, "span");
            });
            //初始化表单元素失去焦点时的行为，当需验证的表单元素失去焦点时，验证其有效性。
            FV_onBlur.initValid($("#btnSubmit").closest("form").get(0));
        });
        function resetForm() {
            document.getElementById('form1').reset();
        }
        </script>    
</body>
</html>
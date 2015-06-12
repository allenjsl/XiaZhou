<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ContractNumDestroy.aspx.cs" Inherits="Web.ContractManage.ContractNumDestroy" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>合同号销号</title>
    <link href="/css/style.css" rel="stylesheet" type="text/css" />

    <script src="/Js/jquery-1.4.4.js" type="text/javascript"></script>

    <script src="/Js/jquery.blockUI.js" type="text/javascript"></script>

    <script src="/Js/jquery.boxy.js" type="text/javascript"></script>

    <script src="/Js/ValiDatorForm.js" type="text/javascript"></script>

    <script src="/Js/table-toolbar.js" type="text/javascript"></script>

    <style type="text/css">
        .errmsg
        {
            font-size: 12px;
            color: Red;
        }
    </style>
    <link href="/css/style.css" rel="stylesheet" type="text/css" />
</head>
<body style="background:0 none;">
    <form id="form1" runat="server">
    <div class="alertbox-outbox02">
    <table width="100%" height="32" border="0" align="center" cellpadding="1" cellspacing="1">
          <tr>
                <td width="30%" height="32" align="right" bgcolor="#b7e0f3">
                    <span class="alertboxTableT">合同类型：</span>
                </td>
                <td width="78%" height="32" align="left">
                    <span class="kuang2">
                    <asp:DropDownList ID="ddlConType" runat="server" valid="required|RegInteger" errmsg="请选择合同号类型|请选择合同号类型"></asp:DropDownList>
                    </span>
                </td>
            </tr>
            <tr>
                <td width="22%" height="22" align="right" bgcolor="#b7e0f3"  rowspan="2">
                    <span class="alertboxTableT">合同号：</span>
                </td>
                <td width="78%" height="22" align="left">
                    &nbsp&nbsp&nbsp&nbsp&nbsp 合同编码 &nbsp&nbsp&nbsp - &nbsp&nbsp&nbsp&nbsp&nbsp 合同序号
                </td>
            </tr>
            <tr>
                <td width="78%" height="22" align="left">
                    <asp:TextBox ID="txtCode" runat="server" Class="formsize80" valid="required" errmsg="请输入合同编码!"></asp:TextBox>-
                    <asp:TextBox ID="txtNumber" runat="server" Class="formsize80" valid="isNumber|required" errmsg="*必须是数字!|请输入合同序号" ></asp:TextBox>
                </td>
            </tr>
      </table>
      <div class="alertbox-btn" style="text-align:center;">
            <asp:LinkButton ID="btnSubmit" runat="server" hidefocus="true" OnClick="btnClick"><s class="baochun"></s>保 存</asp:LinkButton>
            <a hidefocus="true" href="javascript:void(0);"><s class="chongzhi"></s>关 闭</a>
      </div>
    </div>
    </form>
</body>
</html>
<script type="text/javascript">
    $(function() {

        $("#btnSubmit").click(function() {
            var form = $(this).closest("form").get(0);
            //点击按纽触发执行的验证函数
            return ValiDatorForm.validator(form, "parent");
        });
        //初始化表单元素失去焦点时的行为，当需验证的表单元素失去焦点时，验证其有效性。
        FV_onBlur.initValid($("#btnSubmit").closest("form").get(0));
    });
</script>

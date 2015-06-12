<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ContractNumCollar.aspx.cs" Inherits="Web.ContractManage.ContractNumCollar" %>
<%@ Register Src="~/UserControl/SellsSelect.ascx" TagName="SellsSelect" TagPrefix="uc1" %>
<%@ Register Src="~/UserControl/SelectSection.ascx" TagName="SelectSection1" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
<title>合同号领用</title>
<link href="/css/style.css" rel="stylesheet" type="text/css" />
<link href="/css/style.css" rel="stylesheet" type="text/css" />
    <link href="/css/boxynew.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/js/jquery-1.4.4.js"></script>
    <script type="text/javascript" src="/js/table-toolbar.js"></script>
    <style type="text/css">
        .errmsg{color:#f00;font-size:12px;}
    </style>
</head>
<body style="background:0 none;">
    <form id="form1" runat="server">
    <div class="alertbox-outbox02">
    <table width="100%" height="128" border="0" align="center" cellpadding="0" cellspacing="0">
          <tr>
            <td width="30%" height="32" align="right"><span class="alertboxTableT">领用部门：</span></td>
            <td width="70%" height="32" align="left"><span class="kuang2">
              <%--<select name="select">
                <asp:DropDownList ID="ddlDept" runat="server" valid="required|RegInteger" errmsg="请选择部门|请选择部门"></asp:DropDownList>
              </select>--%>
              <uc2:SelectSection1 ID="SelectSection1" runat="server" SModel="1" valid="required" errmsg="请选择部门!"/>
            </span></td>
          </tr>
          <tr>
            <td align="right"><label><span class="alertboxTableT">领 用 人：</span></label></td>
            <td height="32" align="left">
                <uc1:SellsSelect ID="SellsSelect1" runat="server" SMode="1" SetTitle="领用人"/>
            </td>
          </tr>
          <tr>
            <td height="32" align="right">领用时间：</td>
            <td height="32" align="left">
                 <input name="txtCollarTime" id="txtCollarTime" type="text" class="formsize120" valid="required" errmsg="领用时间不能为空"  onfocus="WdatePicker({dateFmt:'yyyy-MM-dd HH:mm'})"  runat="server"/>
                 <a href="javascript:document.getElementById('txtCollarTime').focus();" class="timesicon">领用时间</a>
            </td>
          </tr>
          <tr>
            <td height="32" align="right">号&nbsp;&nbsp;&nbsp; 段：</td>
            <td height="32" align="left">
            <asp:TextBox ID="txtConStart" runat="server" Class="formsize120" valid="isNumber|required" errmsg="*开始数必须是数字!|请输入开始数"></asp:TextBox>
            至
            <asp:TextBox ID="txtConEnd" runat="server" Class="formsize120" valid="isNumber|required" errmsg="*开始数必须是数字!|请输入开始数"></asp:TextBox>
            </td>
          </tr>
      </table>
      <div class="alertbox-btn" style="text-align:center;">
        <asp:LinkButton ID="btnSubmit" runat="server" hidefocus="true"  OnClick="btnClick"><s class="baochun"></s>保 存</asp:LinkButton>
            <a href="javascript:resetForm();" hidefocus="true"><s class="chongzhi"></s>重 置</a>
      </div>
    </div>
    </form>
    <script type="text/javascript" src="/js/jquery.boxy.js"></script>
    <script type="text/javascript" src="/js/jquery.blockUI.js"></script>    
    <script src="/Js/ValiDatorForm.js" type="text/javascript"></script>
    <script src="/Js/datepicker/WdatePicker.js" type="text/javascript"></script>
    <link href="/Css/jquery.autocomplete.css" rel="stylesheet" type="text/css" />
    <script src="/Js/Newjquery.autocomplete.js" type="text/javascript"></script>    
    <script type="text/javascript">
        $(function() {
            $("#btnSubmit").click(function() {
                var form = $(this).closest("form").get(0);
                //点击按纽触发执行的验证函数
                return ValiDatorForm.validator(form, "parent");
            });
            //初始化表单元素失去焦点时的行为，当需验证的表单元素失去焦点时，验证其有效性。
            FV_onBlur.initValid($("#btnSubmit").closest("form").get(0));
        })
        function resetForm() {
            document.getElementById('form1').reset();
        }      
    </script>    
</body>
</html>

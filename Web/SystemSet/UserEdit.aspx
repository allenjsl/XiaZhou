<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeBehind="UserEdit.aspx.cs"
    Inherits="Web.SystemSet.UserEdit" %>

<%@ Register Src="../UserControl/HrSelect.ascx" TagName="HrSelect" TagPrefix="uc1" %>
<%@ Register Src="~/UserControl/SelectSection.ascx" TagName="SelectSection" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <title>无标题文档</title>
    <link href="/css/style.css" rel="stylesheet" type="text/css" />
    <link type="text/css" rel="stylesheet" href="/css/boxynew.css" />

    <script src="/Js/jquery-1.4.4.js" type="text/javascript"></script>

    <script src="/Js/ValiDatorForm.js" type="text/javascript"></script>

    <script src="/Js/table-toolbar.js" type="text/javascript"></script>

    <script src="/Js/jquery.blockUI.js" type="text/javascript"></script>

</head>
<body style="background: 0 none;">
    <form id="form1" runat="server">
    <div class="alertbox-outbox">
        <table width="99%" cellspacing="0" cellpadding="0" bgcolor="#e9f4f9" align="center"
            style="margin: 0 auto">
            <tbody>
                <tr>
                    <td width="12%" height="28" bgcolor="#b7e0f3" align="right" class="alertboxTableT">
                        用户名：
                    </td>
                    <td width="32%" height="28" bgcolor="#e0e9ef" align="left">
                        <asp:TextBox ID="txtUserName" class=" inputtext formsize140" runat="server" errmsg="请填写用户名!"
                            valid="required" MaxLength="15"></asp:TextBox>
                        <span class="fontred">*</span>
                    </td>
                    <td width="13%" bgcolor="#B7E0F3" align="right">
                        密 码：
                    </td>
                    <td width="43%" bgcolor="#e0e9ef" align="left">
                        <input type="password" class="inputtext formsize140" id="txtPwd" name="txtPwd" errmsg="请填写密码!"
                            valid="required" maxlength="15" value="<%=Pwd%>" />
                        <span class="fontred">*</span>
                    </td>
                </tr>
                <tr>
                    <td height="28" bgcolor="#b7e0f3" align="right" class="alertboxTableT">
                        姓&nbsp; 名：
                    </td>
                    <td height="28" bgcolor="#e0e9ef" align="left">
                        <uc1:HrSelect ID="HrSelect1" runat="server" />
                        &nbsp;<span class="fontred"> *</span>
                    </td>
                    <td height="28" bgcolor="#B7E0F3" align="right">
                        性 别：
                    </td>
                    <td height="28" bgcolor="#e0e9ef" align="left">
                        <asp:DropDownList ID="ddlSex" runat="server" errmsg="请选择性别!" valid="required" CssClass="inputselect">
                        </asp:DropDownList>
                        <span class="fontred">*</span>
                    </td>
                </tr>
                <tr>
                    <td height="28" bgcolor="#b7e0f3" align="right" class="alertboxTableT">
                        所属部门：
                    </td>
                    <td height="28" bgcolor="#e0e9ef" align="left">
                        <uc1:SelectSection ID="BelongDepart" runat="server" ReadOnly="true" SetTitle="所属部门"
                            SModel="1" />
                        <span class="fontred">*</span>
                    </td>
                    <td height="28" bgcolor="#B7E0F3" align="right">
                        监管部门：
                    </td>
                    <td height="28" bgcolor="#e0e9ef" align="left">
                        <uc1:SelectSection ID="ManageDepart" runat="server" ReadOnly="true" SetTitle="监管部门"
                            SModel="1" />
                    </td>
                </tr>
                <tr>
                    <td width="12%" height="28" bgcolor="#b7e0f3" align="right" class="alertboxTableT">
                        欠款额度：
                    </td>
                    <td height="28" bgcolor="#e0e9ef" align="left">
                        <input type="text" id="txtDebt" class=" inputtext formsize120" valid="range" max="100000000"
                            errmsg="欠款额度不能超过100000000" runat="server" />
                    </td>
                    <%-- <td width="12%" height="28" bgcolor="#b7e0f3" align="right" class="alertboxTableT">
                        是否启用：
                    </td>
                    <td height="28" bgcolor="#e0e9ef" align="left">
                        <asp:CheckBox ID="chkState" runat="server"  />
                    </td>--%>
                    <td width="12%" height="28" bgcolor="#b7e0f3" align="right" class="alertboxTableT">
                        角色：
                    </td>
                    <td height="28" bgcolor="#e0e9ef" align="left">
                        <asp:DropDownList ID="ddlRoleList" runat="server" errmsg="请选择角色!" valid="required"
                            CssClass="inputselect">
                        </asp:DropDownList>
                        <span class="fontred">*</span>
                    </td>
                </tr>
                <tr>
                    <td width="12%" height="28" bgcolor="#b7e0f3" align="right" class="alertboxTableT">
                        电&nbsp;&nbsp; 话：
                    </td>
                    <td width="32%" height="28" bgcolor="#e0e9ef" align="left">
                        <input type="text" id="txtPhone" errmsg="电话格式错误!" valid="isPhone" class=" inputtext formsize120" name="textfield422"
                            runat="server" />
                    </td>
                    <td width="13%" bgcolor="#B7E0F3" align="right">
                        &nbsp;&nbsp;&nbsp; 传&nbsp; 真：
                    </td>
                    <td width="43%" bgcolor="#e0e9ef" align="left">
                        <input type="text" id="txtFax" class="inputtext formsize140" name="textfield43" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td width="12%" height="28" bgcolor="#b7e0f3" align="right" class="alertboxTableT">
                        手&nbsp;&nbsp; 机：
                    </td>
                    <td width="32%" height="28" bgcolor="#e0e9ef" align="left">
                        <input type="text" id="txtMobile" errmsg="手机格式错误!" valid="isMobile" class="inputtext formsize120" name="textfield422"
                            runat="server" />
                    </td>
                    <td width="13%" bgcolor="#B7E0F3" align="right">
                        &nbsp;&nbsp;&nbsp; QQ：
                    </td>
                    <td width="43%" bgcolor="#e0e9ef" align="left">
                        <input type="text" id="txtQQ" errmsg="QQ格式错误!" valid="isQQ" class="inputtext formsize140" name="textfield43" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td width="12%" height="28" bgcolor="#b7e0f3" align="right" class="alertboxTableT">
                        MSN：
                    </td>
                    <td width="32%" height="28" bgcolor="#e0e9ef" align="left">
                        <input type="text" id="txtMSN" class=" inputtext formsize120" name="textfield422"
                            runat="server" />
                    </td>
                    <td width="13%" bgcolor="#B7E0F3" align="right">
                        &nbsp;&nbsp;&nbsp; E-mail：
                    </td>
                    <td width="43%" bgcolor="#e0e9ef" align="left">
                        <input type="text" id="txtEmail" errmsg="邮箱格式错误!" valid="isEmail" class="inputtext formsize140" name="textfield43"
                            runat="server" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#b7e0f3" align="right" class="alertboxTableT">
                        简介：
                    </td>
                    <td height="68" bgcolor="#e0e9ef" align="left" colspan="3">
                        <asp:TextBox ID="txtIntroduction" Style="height: 63px;" class="inputtext formsize600"
                            Height="80px" runat="server" TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#b7e0f3" align="right" class="alertboxTableT">
                        备注：
                    </td>
                    <td height="68" bgcolor="#e0e9ef" align="left" colspan="3">
                        <asp:TextBox ID="txtRemark" Style="height: 63px;" class="inputtext formsize600" Height="80px"
                            runat="server" TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
            </tbody>
        </table>
        <div style="text-align: center;" class="alertbox-btn">
            <asp:PlaceHolder ID="placeSave" runat="server"><a href="javascript:" id="btnSave"><s
                class="baochun"></s>保 存</a></asp:PlaceHolder>
            <a hidefocus="true" href="javascript:" id="reset" onclick="parent.Boxy.getIframeDialog('<%=Request.QueryString["iframeId"]%>').hide();return false;">
                <s class="chongzhi"></s>关 闭</a>
        </div>
    </div>
    </form>

    <script type="text/javascript">
        var UserEdit = {
            SL: '<%=EyouSoft.Common.Utils.GetQueryStringValue("sl") %>',
            id: '<%=EyouSoft.Common.Utils.GetQueryStringValue("id") %>',
            UnBindBtn: function() {
                $("#btnSave").unbind("click");
                $("#btnSave").text("提交中...");
                $("#btnSave").css("background-position", "0 -57px");
            },
            BindBtn: function() {
                $("#btnSave").unbind("click");
                $("#btnSave").text("保 存");
                $("#btnSave").css("background-position", "0 0");
                $("#btnSave").bind("click", function() {
                    var form = $(this).closest("form").get(0);
                    if (ValiDatorForm.validator(form, "alert")) {
                        UserEdit.Save();
                    }
                    else {
                        return false;
                    }
                });
            },
            BindGovFile: function(data1) {
                $("#<%=HrSelect1.HrSelectNameClient %>").val(data1.text);
                $("#<%=HrSelect1.HrSelectIDClient %>").val(data1.value);
                $.newAjax({
                    type: "get",
                    cache: false,
                    url: "/SystemSet/UserEdit.aspx?dotype=getGovInfo&id=" + data1.value,
                    dataType: "json",
                    success: function(data) {
                        data = (data);
                        if (data.ID) {
                            $("#<%=ddlSex.ClientID %>").val(data.Sex);
                            $("#<%=txtPhone.ClientID %>").val(data.Contact);
                            $("#<%=txtMobile.ClientID %>").val(data.Mobile);
                            $("#<%=txtQQ.ClientID %>").val(data.qq);
                            $("#<%=txtMSN.ClientID %>").val(data.Msn);
                            $("#<%=txtEmail.ClientID %>").val(data.Email);
                            $("#<%=BelongDepart.SelectNameClient%>").val(data.DepartName);
                            $("#<%=BelongDepart.SelectIDClient%>").val(data.DepartId);
                        }
                    }
                });
            },
            Save: function() {
                $.newAjax({
                    type: "post",
                    cache: false,
                    url: "/SystemSet/UserEdit.aspx?dotype=save&sl=" + UserEdit.SL + "&id=" + UserEdit.id,
                    data: $("#btnSave").closest("form").serialize(),
                    dataType: "json",
                    success: function(ret) {
                        if (ret.result == "1") {
                            parent.tableToolbar._showMsg(ret.msg, function() { parent.window.location.href = "/SystemSet/UserList.aspx?sl=" + UserEdit.SL; });
                        } else {
                            parent.tableToolbar._showMsg(ret.msg);
                        }
                    },
                    error: function() {
                        parent.tableToolbar._showMsg("服务器忙，请稍后再试!");
                    }
                });
            }
        };
        $(function() {
            FV_onBlur.initValid($("#btnSave").closest("form").get(0));
            $("#btnSave").click(function() {
                var form = $(this).closest("form").get(0);
                if (ValiDatorForm.validator(form, "alert")) {
                    UserEdit.Save();
                }
                else {
                    return false;
                }
            });
        })
    </script>

</body>
</html>

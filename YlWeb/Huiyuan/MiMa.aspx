<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MiMa.aspx.cs" Inherits="EyouSoft.YlWeb.Huiyuan.MiMa" MasterPageFile="~/MasterPage/HuiYuan.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HuiYuanHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HuiYuanBody" runat="server">
    <form id="form1" runat="server">
        <div class="user-title">更改密码 <em>*必须填写原密码才能修改下面的资料</em></div>
        <div class="user_Rbox">
          <ul class="login_form fixed">
	          <li><label>旧密码</label><input type="password" id="txtOld" runat="server" class="formsize270 input_style" /></li>
	          <li><label>新密码</label><input type="password" id="txtNew" runat="server" class="formsize270 input_style" /></li>
	          <li><label>确认密码</label><input type="password" id="txtConfirm" runat="server" class="formsize270 input_style" /></li>
	          <%--<li><label>电子邮箱</label><input id="txtEmail" runat="server" class="formsize270 input_style" /></li>--%>
          </ul>			     
        </div>

        <div class="user_Rbox" style="text-align:center;">
        <a href="javascript:void(0)" class="user_add_btn user_btn02" onclick="Save();">保存信息</a>
        </div>
    </form>
<script type="text/javascript">
    function Save(){
        $.newAjax({
            type: "post",
            cache: false,
            url: '/HuiYuan/MiMa.aspx?doType=save',
            data: $("form").serialize(),
            dataType: "json",
            success: function(ret) {
                if (ret.result == "1") {
                    tableToolbar._showMsg(ret.msg)
                    window.location.href = window.location.href;

                } else {
                    tableToolbar._showMsg(ret.msg);
                }
            }
        });
    }
</script></asp:Content>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Web.Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="/Css/style.css" rel="stylesheet" type="text/css" />

    <script src="/Js/jquery-1.4.4.js" type="text/javascript"></script>

    <style type="text/css">
        .login_logo, .login_logo img
        {
            width: 458px;
            height: 80px;
        }
        body
        {
            background: #0F67A5;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <asp:Literal ID="litWelcome" runat="server"></asp:Literal>
    <div class="login-wrap">
        <div class="login-top" style="text-align: center;">
            <asp:Literal ID="ltrTiShi" runat="server"></asp:Literal>
        </div>
        <div class="login fixed">
            <div class="login-l">
            </div>
            <div class="login-c">
                <div>
                    <img src="/images/login_04.gif"></div>
                <div class="login_logo">
                    <asp:Literal ID="litLogo" runat="server"></asp:Literal>
                </div>
                <div class="login-c-00 fixed">
                    <div class="login-c-01" style="text-align: center;">
                        <asp:Literal ID="litLeft" runat="server"></asp:Literal>
                    </div>
                    <div class="login-c-02 fixed">
                        <ul>
                            <li><b>用户名：</b>
                                <input type="text" class="inputtext formsize140" style="width: 165px;" name="u" id="u"
                                    tabindex="1"></li>
                            <li><b>密<span style="padding-left: 12px;"></span>码：</b>
                                <input type="password" class="inputtext formsize140" style="width: 165px;" name="p"
                                    id="p" tabindex="2"></li>
                            <li><b>验证码：</b>
                                <input onfocus="this.select();" class="inputtext formsize100" tabindex="3" type="text"
                                    name="vc" id="vc" />
                                <img style="cursor: pointer; margin-top: 4px" title="点击更换验证码" onclick="this.src='/ashx/ValidateCode.ashx?ValidateCodeName=SYS_VC&id='+Math.random();return false;"
                                    align="middle" width="60" height="20" id="imgCheckCode" src="/ashx/ValidateCode.ashx?ValidateCodeName=SYS_VC&t=<%=DateTime.Now.ToString("HHmmssffff") %>" />
                            </li>
                            <li style="display: none;"><b>&nbsp;</b><input type="checkbox" value="" name="">
                                下次自动登录 &nbsp;&nbsp;<span class="fontred"><a href="#">忘记密码？</a></span></li>
                        </ul>
                        <div class="login-anniu">
                            <ul>
                                <li><a href="javascript:void(0);" id="linkLogin" tabindex="4">登录</a></li>
                                <li><a href="javascript:void(0);" onclick="reset()">重置</a></li></ul>
                        </div>
                    </div>
                    <div class="login-c-03" style="text-align: center; width: 70px;">
                        <asp:Literal ID="litRight" runat="server"></asp:Literal>
                    </div>
                </div>
                <div>
                    <img src="/images/login_08.gif"></div>
            </div>
            <div class="login-r">
            </div>
        </div>
        <div class="login-bottom">
            <img src="/images/login_09.gif"><img src="/images/login_10.gif"><img src="/images/login_11.gif">
        </div>
    </div>
    <div class="login-footer">
        版权所有：杭州易诺科技有限公司<span style="padding-left: 30px;">系统使用环境：1024*768以上分辨率、IE 7及以上版本浏览器</span>
    </div>
    </form>

    <script src="/Js/slogin.js" type="text/javascript"></script>

    <script src="/Js/jquery.blockUI.js" type="text/javascript"></script>

    <script src="/Js/table-toolbar.js" type="text/javascript"></script>

    <script type="text/javascript">
        function getCheckCode() {
            var c = document.cookie, ckcode = "", tenName = "";
            for (var i = 0; i < c.split(";").length; i++) {
                tenName = c.split(";")[i].split("=")[0];
                ckcode = c.split(";")[i].split("=")[1];
                if ($.trim(tenName) == "SYS_VC") {
                    break;
                } else {
                    ckcode = "";
                }
            }
            return $.trim(ckcode);
        };


        function reset() {
            $("#u").val(""); $("#p").val(""); $("#vc").val("");
        }

        function login() {
            var u = $.trim($("#u").val()), p = $.trim($("#p").val()), ckcode = $.trim($("#vc").val());
            if (u == "") {
                tableToolbar._showMsg("请输入用户名!");
                $("#u").focus();
                return;
            }
            if (p == "") {
                tableToolbar._showMsg("请输入密码");
                return;
            }
            if (ckcode == "" || ckcode != getCheckCode()) {
                tableToolbar._showMsg("请输入正确的验证码");
                return;
            }

            //显示登录状态
            tableToolbar._showMsg("正在登录中....");
            //防止重复登陆
            $("#linkLogin").unbind().css("cursor", "default");

            blogin5($("form").get(0)
                , function(h) {//login success callback
                    tableToolbar._showMsg("登录成功，正进入系统....");
                    var s = '<%=Request.QueryString["returnurl"] %>';
                    if (s == "") {
                        if (h == "1") {
                            s = "/GroupEnd/Distribution/AcceptPlan.aspx?sl=<%=(int)EyouSoft.Model.EnumType.PrivsStructure.Menu2.None%>";
                        } else if (h == "2") {
                            s = "/Default.aspx?sl=<%=(int)EyouSoft.Model.EnumType.PrivsStructure.Menu2.None%>";
                        } else if (h == "3") {
                            s = "/GroupEnd/Suppliers/ProductList.aspx?sl=<%=(int)EyouSoft.Model.EnumType.PrivsStructure.Menu2.None%>";
                        }
                    }
                    window.location.href = s;
                }
                , function(m) {//login error callback
                    tableToolbar._showMsg(m);
                    $("#linkLogin").click(function() { login(); return false; }).css("cursor", "pointer");
                });
        }

        $(function() {
            $("#u").focus();
            $("#linkLogin").click(function() { login(); return false; });
            $("#u,#p,#vc").keypress(function(e) { if (e.keyCode == 13) { login(); return false; } });
        });

    </script>

</body>
</html>

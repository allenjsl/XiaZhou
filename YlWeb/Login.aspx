<%@ Page Title="会员登录" Language="C#" MasterPageFile="~/MasterPage/Boxy.Master" AutoEventWireup="true"
    CodeBehind="Login.aspx.cs" Inherits="EyouSoft.YlWeb.Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PageHead" runat="server">    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageBody" runat="server">
    <div class="mainbox">
        <form runat="server" id="form1">
        <div class="basicT">
            您的位置：维诗达游轮 &gt; 会员中心</div>
        <div class="user_box fixed">
            <div class="login_L"><asp:Literal runat="server" ID="ltrGuangGao"></asp:Literal></div>
            <div class="login_R login001">
            
            <div class="login_boxyL">
				<h3>会员登录</h3>
				<div class="loginbar margin_T16">
	                     <div class="login_select"><label><input type="radio" checked="checked" value="" id="rdo1" name="rdo">普通登录</label><label><input type="radio" value="" id="rdo2" name="rdo">手机动态密码登录</label></div>
	                     <ul class="login_form" id="ul1">
	                        <li><label>登录名</label><input value="" class="formsize200 input_style" id="i_u_0"/>
	                        </li>
	                        <li><label>密&#12288;码</label><input class="formsize200 input_style" type="password" id="i_p_0"/><a class="forget_pwd" href="/reset.aspx" target="_blank">忘记密码?</a></li>
	                        <li class="login_txt"> <label class="no_style"><input type="checkbox" checked="checked" value="" id="c_1">30天内自动登录</label></li>
	                        <li class="login_txt"><a class="loginbtn" href="javascript:void(0)" id="i_btn_0">登录</a></li>
	                     </ul>
	                     <ul style="display:none;" class="login_form" id="ul2">
	                        <li><label>手机号</label><input value="" class="formsize200 input_style" id="i_sj_0"/>
	                        </li>
	                        <li><label>验证码</label><input class="formsize100 input_style" id="txtYanZhengMa"/><a href="javascript:void(0)">
                        <img style="cursor: pointer; margin-top: 0px" title="点击更换验证码" 
                                    align="middle" width="88" height="30" id="imgCheckCode" src="/ashx/ValidateCode.ashx?ValidateCodeName=SYS_VC&t=<%=DateTime.Now.ToString("HHmmssffff") %>" />
</a></li>
	                        <li><label>密&#12288;码</label><input value="" class="formsize100 input_style" type="password" id="i_p_1"/> <a class="pwd_bg" href="javascript:void(0)" id="i_p">发送动态密码</a></li>
	                        <li class="login_txt"> <label class="no_style"><input type="checkbox" checked="checked" value="" id="c_2">30天内自动登录</label></li>
	                        <li class="login_txt"><a class="loginbtn" href="javascript:void(0)" id="i_btn_1">登录</a></li>
	                     </ul>
	         </div>
		   </div>
            
                <div class="zhuce-btn">
                    <a href="Register.aspx">新用户注册</a></div>
                <div class="login_Rimg">
                    <asp:Literal runat="server" ID="Literal1"></asp:Literal></div>
            </div>
        </div>
        </form>
    </div>

    <script type="text/javascript">
        //获取验证码
        function _getcode() {
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
        }

        function __login() {
            var u = $.trim($("#i_u_0").val()), p = $.trim($("#i_p_0").val()), ckcode = '', is = $("#c_1").attr("checked");
            if (u == "") { tableToolbar._showMsg("请输入用户名!"); $("#i_u_0").focus(); return; }
            if (p == "") { tableToolbar._showMsg("请输入密码"); return; }

            tableToolbar._showMsg("正在登录....");
            $("#i_btn_0").unbind().css("cursor", "default");

            blogin5({ u: u, p: p, vc: ckcode, is: is }
                        , function(h) { /*tableToolbar._showMsg("登录成功", $("#btnSava").unbind().bind("click", function() { PageSet.sava(); }));*/; tableToolbar._showMsg("登录成功"); window.location.href = "/default.aspx"; return; }
                        , function(m) { tableToolbar._showMsg(m); $("#i_btn_0").click(function() { __login(); return false; }).css("cursor", "pointer"); }
                    );
        }

        function __login1() {
            var u = $.trim($("#i_sj_0").val()), p = $.trim($("#i_p_1").val()), ckcode = $.trim($("#txtYanZhengMa").val()), is = $("#c_2").attr("checked");
            if (u == "") { tableToolbar._showMsg("请输入手机!"); $("#i_u_0").focus(); return; }
            if (p == "") { tableToolbar._showMsg("请输入密码"); $("#i_p_1").focus(); return; }
            if (ckcode == "") { tableToolbar._showMsg("请输入验证码"); $("#txtYanZhengMa").focus(); return; }

            tableToolbar._showMsg("正在登录....");
            $("#i_btn_1").unbind().css("cursor", "default");

            blogin5({ u: u, p: p, vc: ckcode, is: is }
                        , function(h) { /*tableToolbar._showMsg("登录成功", $("#btnSava").unbind().bind("click", function() { PageSet.sava(); }));*/; tableToolbar._showMsg("登录成功"); window.location.href = "/default.aspx"; return; }
                        , function(m) { tableToolbar._showMsg(m); $("#i_btn_1").click(function() { __login1(); return false; }).css("cursor", "pointer"); }
                    );
                }
                $(function() {
                    $('#rdo1').click(function() { $('#ul1').css('display', ''); $('#ul2').css('display', 'none'); });
                    $('#rdo2').click(function() { $('#ul1').css('display', 'none'); $('#ul2').css('display', ''); });
                    $('#imgCheckCode').click(function() { $(this).attr("src", '/ashx/ValidateCode.ashx?ValidateCodeName=SYS_VC&id=' + Math.random()) });
                    $("#i_btn_0").click(function() { __login(); });
                    $("#i_btn_1").click(function() { __login1(); });
                    $('#i_u_0').keydown(function(e) { if (e.keyCode == 13) { $("#i_btn_0").click(); } });
                    $('#i_p_0').keydown(function(e) { if (e.keyCode == 13) { $("#i_btn_0").click(); } });
                    $('#i_xm_0').keydown(function(e) { if (e.keyCode == 13) { $("#i_btn_1").click(); } });
                    $('#i_sj_0').keydown(function(e) { if (e.keyCode == 13) { $("#i_btn_1").click(); } });
                    $("#i_p").click(function() {
                        if ($.trim($("#txtYanZhengMa").val()) != _getcode()) { tableToolbar._showMsg("请输入正确的验证码！"); return false; }
                        if (!RegExps.isMobile.test($.trim($("#i_sj_0").val()))) { tableToolbar._showMsg("请输入正确的手机号码！"); return false; }
                        $(this).unbind("click").css("cursor", "default");
                        $.newAjax({
                            type: "post",
                            cache: false,
                            url: "/Ashx/Handler.ashx?dotype=fasongmima&shouji=" + $.trim($("#i_sj_0").val()),
                            dataType: "json",
                            success: function(ret) {
                                tableToolbar._showMsg(ret.msg);
                            },
                            error: function() {
                                tableToolbar._showMsg(tableToolbar.errorMsg);
                            }
                        })
                        //$(this).bind("click").css("cursor", "pointer");
                    });
                });
    </script>

</asp:Content>

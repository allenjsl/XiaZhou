<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Web.GroupEnd.Suppliers.Login" %>

<%@ Register Src="~/UserControl/DistributorNotice.ascx" TagName="Notice" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta content="text/html; charset=utf-8" http-equiv="Content-Type" />
    <title>供应商平台</title>
    <link type="text/css" rel="stylesheet" href="/Css/fx_style复件.css" />
</head>
<body>
    <!--banner-->
    <div class="banner">
        <div class="logo">
        </div>
    </div>
    <!--main-->
    <div class="main">
        <div class="jianbian">
            <uc1:Notice runat="server" ID="Notice1" />
        </div>
        <div class="content fixed">
            <div class="fxsline-box22">
                <div class="dengluboxT">
                    <img width="595" height="59" alt=" " src="/images/fx-images/fxptlo_09.jpg"></div>
                <div class="dengluk">
                    <form>
                    <ul>
                        <li>用户名：<input type="text" id="u" class="loginInputText" /></li>
                        <li>密 码：<input type="password" id="p" class="loginInputText" name="" /></li>
                        <li>验证码：<input type="text" class="loginInputText yzm" id="vc" name="" />
                            <img style="cursor: pointer; margin-top: 4px" title="点击更换验证码" onclick="this.src='/ashx/ValidateCode.ashx?ValidateCodeName=gycode&id='+Math.random();return false;"
                                align="middle" width="60" height="20" id="validateImg" src="/ashx/ValidateCode.ashx?ValidateCodeName=SCode&t=<%=DateTime.Now.ToString("HHmmssffff") %>" />
                        </li>
                        <li style="padding-left: 50px;">
                            <input type="checkbox" value="" name="" />
                            下次自动登录 <a href="#">忘记密码？</a></li>
                        <li class="loginbtnbox">
                            <button class="loginbtn" type="button" id="btnLogin">
                                立即登录</button></li>
                    </ul>
                    </form>
                </div>
            </div>
            <div class="contact2">
                <h3>
                    <img width="304" height="56" alt=" " src="/images/fx-images/fxptlo_10.jpg"></h3>
                <div class="contact2box">
                    <ul>
                        <li>联系人：胡晓明</li>
                        <li>电 &nbsp;话：0571-88888888</li>
                        <li>手 &nbsp;机：13688888888</li>
                        <li>传 &nbsp;真：0571-666666</li>
                        <li>Q &nbsp;&nbsp; Q：305714997</li>
                    </ul>
                </div>
            </div>
        </div>
    </div>
    <div class="hr_10">
    </div>
    <!--footer-->
    <div class="footer">
        版权所有 湖北峡州国际旅行社有限公司 技术支持 易诺科技</div>

    <script type="text/javascript" src="/Js/jquery-1.4.1-vsdoc.js"></script>

    <script src="/Js/jquery.blockUI.js" type="text/javascript"></script>

    <script src="/Js/table-toolbar.js" type="text/javascript"></script>

    <script src="/Js/slogin.js" type="text/javascript"></script>

    <script type="text/javascript">
        function login() {
            var u = $.trim($("#u").val()), p = $.trim($("#p").val()), ckcode = $.trim($("#vc").val());
            if (u == "") {
                tableToolbar._showMsg("请输入用户名!");
                $("#u").focus();
                return;
            }
            else if (p == "") {
                tableToolbar._showMsg("请输入密码");
                return;
            }
            else if(ckcode==""){
             tableToolbar._showMsg("请输入正确的验证码");
                return;
            }
            else if (ckcode != GetCodeByCookie()) {
                tableToolbar._showMsg("请输入正确的验证码");
                return;
            }
            else{
            
            tableToolbar._showMsg("正在登录中....");
            //防止重复登陆
            $(".loginbtn").unbind().css("cursor", "default");
            
             blogin5($("form").get(0), function(status) {
                 tableToolbar._showMsg("登录成功，正进入供应商平台....");
                 var url = "<%=Request.QueryString["returnurl"] %>";
                 if(url==""){
                    if(status=="3"){
                        url="/GroupEnd/Suppliers/ProductList.aspx?sl=<%=(int)EyouSoft.Model.EnumType.PrivsStructure.Menu2.None%>";
                    }
                 }
                  window.location.href=url;
             },function(){
                tableToolbar._showMsg(error);
                 $(".loginbtn").css("cursor","pointer").click(function(){
                     DoLogin();
                     return false;
                 });
             });
             
            }

        }


        //Cookie 中获取当前验证码
        function GetCodeByCookie() {
            var c = document.cookie;
            var dic = c.split(';');
            for (var i = 0; i < dic.length; i++) {
                if ($.trim(dic[i].split('=')[0]) == "SCode") {
                    return $.trim(dic[i].split('=')[1]);
                }
            }
            return "";
        }
        
        
        $(function(){
            $("#u").focus();
            $("#btnLogin").click(function() {
                login();
                return false;
            });
            $("#u,#p,#vc").keypress(function(e) {
                if (e.keyCode == 13) {
                    login();
                    return false;
                }
            });
        });
    
    </script>

</body>
</html>

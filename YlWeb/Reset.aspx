<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Reset.aspx.cs" Inherits="EyouSoft.YlWeb.Reset" MasterPageFile="~/MasterPage/Boxy.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PageHead" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageBody" runat="server">
    <form id="form1" runat="server">
    <div class="mainbox">
       <div class="basicT">您的位置：维诗达游轮 &gt; 会员中心</div>

        <div class="user_box">
           
		   <div class="reg_box forget_box fixed">
			  <div class="reg_L">
			      <div class="forget_t">重置登录密码</div>
                     <ul class="login_form">
                        <li><label>用户名</label><input value="用户名/邮箱" class="formsize200 input_style" id="txtYongHuMing" name="txtYongHuMing" valid="required" errmsg="请填写用户名/邮箱"/>
                        </li>
                        <li><label>验证码</label><input value="不区分大小写" class="formsize100 input_style" id="txtYanZhengMa" valid="required" errmsg="请填写验证码"/> <a href="javascript:void(0)">
                        <img style="cursor: pointer; margin-top: 0px" title="点击更换验证码" onclick="this.src='/ashx/ValidateCode.ashx?ValidateCodeName=SYS_VC&id='+Math.random();return false;"
                                    align="middle" width="88" height="30" id="imgCheckCode" src="/ashx/ValidateCode.ashx?ValidateCodeName=SYS_VC&t=<%=DateTime.Now.ToString("HHmmssffff") %>" />
</a></li>
                        <li class="password_txt">看不清，<a href="javascript:void(0)" onclick="$('#imgCheckCode').trigger('click')">换一张</a></li>
                        <li class="login_txt"><a class="loginbtn" href="javascript:void(0)">提 交</a></li>
                     </ul>
			  </div>
			  <div class="reg_Rimg"><asp:Literal runat="server" ID="ltrGuangGao"></asp:Literal></div>
		   </div>
		   
           
       </div>
        
        
        
     </div>
    </form>
<script language="javascript" type="text/javascript">
var iPage={
    GetCode:function(){
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
    },
    Check:function(){
        var un=$.trim($("#txtYongHuMing").val());
        var ckcode=$.trim($("#txtYanZhengMa").val());
        var isok=false;
        if(un=="用户名/邮箱"){
            tableToolbar._showMsg("请输入正确的用户名或邮箱");
            isok=false;
        }
        else{
            isok=true;
        }
        if (ckcode != this.GetCode()) {
            tableToolbar._showMsg("请输入正确的验证码");
            isok= false;
        }
        else{
            isok= true;
        }
        return isok;
    },
    BindBtn: function() {
        $(".loginbtn").val("提 交").unbind("click").click(function() {
            iPage.Submit();
        });
        $("#txtYongHuMing").unbind("focus").focus(function(){
            if($.trim($(this).val())=="用户名/邮箱"){
                $(this).val("");
            }
        });
        $("#txtYongHuMing").unbind("blur").blur(function(){
            if($.trim($(this).val())==""){
                $(this).val("用户名/邮箱");
            }
        });
        $("#txtYanZhengMa").unbind("focus").focus(function(){
            if($.trim($(this).val())=="不区分大小写"){
                $(this).val("");
            }
        });
        $("#txtYanZhengMa").unbind("blur").blur(function(){
            if($.trim($(this).val())==""){
                $(this).val("不区分大小写");
            }
        });
    },
    Submit:function(){
        if(ValiDatorForm.validator($(".loginbtn").closest("form").get(0), "alert")&&this.Check()){
            $(".loginbtn").val("提交中...").unbind("click");
            $.newAjax({
                type: "post",
                cache: false,
                url: "/Reset.aspx",
                dataType: "json",
                data: $(".loginbtn").closest("form").serialize(),
                success: function(ret) {
                    switch(ret.result)
                    {
                        case "0":
                            tableToolbar._showMsg(ret.msg);
                            iPage.BindBtn();
                            break;
                        case "1":
                            tableToolbar._showMsg(ret.msg, function() {
                                location.href = location.href;
                            });
                            break;
                        case "2":
//                            $.getJSON(ret.msg,function(data){alert(data.msg);});
                            $.ajax({
                                type: "get",
                                async:false,
                                cache : false, 
                                url: ret.msg,
                                dataType: "jsonp",
                                jsonp:"callback",
                                success: function(r) {
                                    if(r=="0"){
                                        tableToolbar._showMsg("提交成功！", function() {
                                            location.href = location.href;
                                        });
                                    }else{
                                        tableToolbar._showMsg("提交失败！");
                                        iPage.BindBtn();
                                    }
                                },
                                error: function() {
                                    tableToolbar._showMsg(tableToolbar.errorMsg);
                                    iPage.BindBtn();
                                }
                            });
                            break;
                    }
                },
                error: function() {
                    tableToolbar._showMsg(tableToolbar.errorMsg);
                    iPage.BindBtn();
                }
            });
        }
    }
}
$(function(){
    iPage.BindBtn();
})
</script>
</asp:Content>

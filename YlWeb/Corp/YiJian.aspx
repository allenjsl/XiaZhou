<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="YiJian.aspx.cs" Inherits="EyouSoft.YlWeb.Corp.YiJian" Title="意见反馈" MasterPageFile="~/MasterPage/Boxy.Master" ValidateRequest="false" %>
<%@ Register Src="../UserControl/UploadControl.ascx" TagName="UploadControl" TagPrefix="uc1" %>
<%@ MasterType VirtualPath="~/MasterPage/Boxy.Master" %>

<asp:Content ID="ctHead" ContentPlaceHolderID="PageHead" runat="server">
    <link href="/Css/swfupload/default.css" rel="stylesheet" type="text/css" />

    <script src="/Js/swfupload/swfupload.js" type="text/javascript"></script>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="PageIndex" runat="server">
<div class="step_mainbox fixed">
         
            <div class="basicT"> 您的位置：维诗达游轮 &gt; 意见反馈</div>
            
            <div class="map_bar">
               <div class="map_barT">意见反馈</div>
               <div class="yijian_box">
                  <h3>欢迎提交任何关于维诗达游轮网的问题和建议，我们将尽快回复您。感谢您对维诗达游轮网的帮助。</h3>
                  <h4>您喜欢新版维诗达吗？我们倾听您的建议<br>
如果提交有关浏览速度、系统BUG、视觉显示等问题，请注明您使用的操作系统、浏览器，以便我们尽快对应查找问题并解决。 </h4>
                    <form id="form1" runat="server">
                    <table width="100%" cellspacing="0" cellpadding="0" border="0">
                      <tbody><tr>
                        <td width="9%" height="60">问题类型</td>
                        <td width="91%"><label><input type="radio" value="0" id="radio" name="radio" checked="checked"/>
                        网站改版建议</label>      
                          <label><input type="radio" value="1" id="radio2" name="radio"/>
                        订购流程</label> </td>
                      </tr>
                      <tr>
                        <td height="60">上传文件</td>
                        <td><uc1:UploadControl runat="server" ID="upFiles" IsUploadMore="false" IsUploadSelf="true" /></td>
                      </tr>
                      <tr>
                        <td valign="top">问题描述</td>
                        <td><textarea class="yijian_txt" id="textfield" name="textfield" valid="required" errmsg="请填写问题描述"></textarea></td>
                      </tr>
                      <tr>
                        <td height="60">验证码</td>
                        <td><input class="formsize100" style="margin-right:4px; height:28px; line-height:28px; border:#bbb solid 1px;" id="txtYanZhengMa" valid="required" errmsg="请填写验证码" /><a href="javascript:void(0)"><img style="cursor: pointer; margin-top: 0px" title="点击更换验证码" align="middle" width="88" height="30" id="imgCheckCode" src="/ashx/ValidateCode.ashx?ValidateCodeName=SYS_VC&t=<%=DateTime.Now.ToString("HHmmssffff") %>" /></a></td>
                      </tr>
                      <tr>
                        <td height="60">&nbsp;</td>
                        <td><a href="javascript:;" id="a_submit"><img src="../images/yj_tj.gif"></a></td>
                      </tr>
                    </tbody></table>
                    </form>
               </div>
            </div>            
            
         </div>
<script type="text/javascript">
var ipage={
    getcode:function(){
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
    changecode:function(o){
        $(o).attr("src", '/ashx/ValidateCode.ashx?ValidateCodeName=SYS_VC&id=' + Math.random());
    },
    check:function(){
        var _ok=ValiDatorForm.validator($("form").get(0), "alert");
        if(!_ok)return _ok;
        if ($.trim($("#txtYanZhengMa").val()) != ipage.getcode()) { tableToolbar._showMsg("请填写正确的验证码"); return false; }
        return true;
    },
    save:function(o){
        $(o).unbind().css("cursor", "default");
        $.newAjax({
            type: "post",
            cache: false,
            url: "/corp/yijian.aspx?dotype=submit",
            dataType: "json",
            data: $(o).closest("form").serialize(),
            success: function(ret) {
                tableToolbar._showMsg(ret.msg);
            },
            error: function() {
                tableToolbar._showMsg(tableToolbar.errorMsg);
            }
        })
        $(o).click(function() {if(ipage.check()){ipage.save(this);ipage.changecode($('#imgCheckCode'));}}).css("cursor", "pointer");
    }
}
$(function(){
    $('#imgCheckCode').click(function() { ipage.changecode(this); });
    
    $("#a_submit").click(function(){
        if(ipage.check()){ipage.save(this);$('#imgCheckCode').trigger("click");}
    });
})
</script>
</asp:Content>

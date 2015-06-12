<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TouXiang.aspx.cs" Inherits="EyouSoft.YlWeb.Huiyuan.TouXiang" MasterPageFile="~/MasterPage/HuiYuan.Master" %>
<%@ Register Src="../UserControl/UploadControl.ascx" TagName="UploadControl" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HuiYuanHead" runat="server">
    <link href="/Css/swfupload/default.css" rel="stylesheet" type="text/css" />

    <script src="/Js/swfupload/swfupload.js" type="text/javascript"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HuiYuanBody" runat="server">
    <form id="form1" runat="server">
        <div class="user-title">修改头像</div>
        <div class="user_Rbox"><div class="txt">如果您还没有设置自己的头像，系统会显示为默认头像。您需要上传一张235*235图片作为自己的头像。</div></div>
        <div class="user_Rbox">
          <ul class="headbox fixed">
	          <li  style="margin-right:130px;">
	             <p class="info">当前我的头像</p>
		         <p class="head-img"><img src="<%=TuXiang %>" width="235px" height="235px" /></p>
	          </li>
	          <li>
	             <p class="info">设置我的新头像</p>
		         <div class="head-img"><img src="../images/head_bg.jpg"/>
		         <div class="head_upload">
		         <uc1:UploadControl runat="server" ID="upFiles" IsUploadMore="false" IsUploadSelf="true" FileTypes="*.jpg;*.gif;*.jpeg;*.png" />
                </div>
                </div>
	          </li>
          </ul>			     
        </div>

        <div class="user_Rbox" style="text-align:center;">
        <a href="javascript:void(0);" class="user_add_btn user_btn02" onclick="Save();">保存头像</a>
        </div>
    </form>
<script type="text/javascript">
    function Save(){
        $.newAjax({
            type: "post",
            cache: false,
            url: '/HuiYuan/TouXiang.aspx?doType=save',
            data: $("form").serialize(),
            dataType: "json",
            success: function(ret) {
                if (ret.result == "1") {
                    tableToolbar._showMsg(ret.msg,function(){window.location.href = window.location.href;})

                } else {
                    tableToolbar._showMsg(ret.msg);
                }
            },
            error: function() {
                tableToolbar._showMsg("操作失败，请稍后重试！");
            }
        });
    }
</script>
</asp:Content>

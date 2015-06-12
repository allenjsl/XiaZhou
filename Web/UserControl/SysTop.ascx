<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SysTop.ascx.cs" Inherits="Web.UserControl.SysTop" %>
<div class="list_btn">
    <ul class="fixed" sign="fixed">
        <li><a id="sendmessage" href="/SmsCenter/SendMessage.aspx?sl=88">发送短信</a></li>
        <li><a id="sendhistory" href="/SmsCenter/SendHistory.aspx?sl=88">发送历史</a></li>
        <li><a id="smslist" href="/SmsCenter/SmsList.aspx?sl=88">常用短信</a></li>
        <li><a id="smssetting" href="/SmsCenter/SmsSetting.aspx?sl=88">短信设置</a></li>
        <li><a id="accountinfo" href="/SmsCenter/AccountInfo.aspx?sl=88">帐户信息</a></li>
    </ul>

    <script type="text/javascript">
        $("[sign=fixed]").find("li").each(function() {
            if ($(this).find("a").attr("id") == '<%=str%>')
                $(this).find("a").addClass("warmdefault");
            else
                $(this).find("a").removeClass("warmdefault");
        });
    </script>

</div>

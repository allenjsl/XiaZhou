<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Account.aspx.cs" Inherits="Web.ResourceManage.Ground.Account" %>

<%@ Register Src="~/UserControl/LineAreaSelect.ascx" TagName="LineAreaSelect" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>资源管理-地接社-分配帐号</title>
    <link href="/Css/style.css" rel="stylesheet" type="text/css" />

    <script src="/Js/jquery-1.4.4.js" type="text/javascript"></script>

    <script src="/Js/jquery.blockUI.js" type="text/javascript"></script>

    <script src="/Js/jquery.boxy.js" type="text/javascript"></script>

    <script src="/Js/ValiDatorForm.js" type="text/javascript"></script>

    <script src="/Js/table-toolbar.js" type="text/javascript"></script>

</head>
<body style="background: 0 none;">
    <form id="form1" runat="server">
    <input type="hidden" runat="server" id="txtEdit" value="0" />
    <div class="alertbox-outbox02">
        <table width="99%" align="center" cellpadding="0" cellspacing="0" bgcolor="#e9f4f9"
            style="margin: 0 auto">
            <tr>
                <td width="39%" height="28" align="right" class="alertboxTableT">
                    账 号：
                </td>
                <td width="61%" height="28" align="left">
                    <asp:TextBox ID="txtAccount" runat="server" name="txtAccount" class="inputtext formsize120"
                        size="30"></asp:TextBox><span id="AccountTip" style="color: Red; display: none">*账号不能为空!</span>
                </td>
            </tr>
            <tr>
                <td height="28" align="right" class="alertboxTableT">
                    密 码：
                </td>
                <td height="28" align="left">
                    <asp:TextBox ID="txtPwd" TextMode="Password" runat="server" name="txtPwd" class="inputtext formsize120"
                        size="30"></asp:TextBox><span id="PwdTip" style="color: Red; display: none">*密码不能为空!</span>
                </td>
            </tr>
            <tr>
                <td height="28" align="right" class="alertboxTableT">
                    线路区域：
                </td>
                <td height="28" align="left">
                    <uc1:LineAreaSelect ID="LineAreaSelect1" runat="server" SModel="2" SetTitle=" 线路区域选择" />
                </td>
            </tr>
        </table>
        <div class="alertbox-btn">
            <a href="javascript:void(0)" id="btn" hidefocus="true"><s class="baochun"></s>保 存</a><a
                href="javascript:void(0)" onclick="Account.CloseForm()" hidefocus="true"><s class="chongzhi"></s>关
                闭</a>
        </div>
    </div>
    </form>
</body>
</html>

<script type="text/javascript">
    $(function() {
        Account.PageInit();
    })
    var Account = {
        GoAjax: function(url) {
         $("#btn").html("提交中...");
         $("#btn").unbind("click");
            $.newAjax({
                type: "post",
                cache: false,
                url: url,
                dataType: "json",
                data: $("#btn").closest("form").serialize(),
                success: function(ret) {
                    if (ret.result == "1") {
                       parent.tableToolbar._showMsg(ret.msg, function() { parent.location.href = parent.location.href; });
                    }
                    else {
                       parent.tableToolbar._showMsg(ret.msg,function(){Account.BindBtn()});
                    }
                },
                error: function() {
                  tableToolbar._showMsg(tableToolbar.errorMsg,function(){Account.BindBtn()});
                }
            });
        },
        PageInit: function() {
            //页面初始化
            Account.BindBtn();
        },
        BindBtn: function() {
            //绑定Add事件
            $("#btn").click(function() {
               if(Account.CheckForm()){
                    var ajaxUrl = "/ResourceManage/Ground/Account.aspx?type=save&sl=" + '<%=Request.QueryString["sl"]%>' + "&sourceid=" + '<%=Request.QueryString["sourceid"]%>&account=<%=Request.QueryString["account"] %>';
                    Account.GoAjax(ajaxUrl);
                }
                return false;
            })
            $("#btn").html("<s class='baochun'></s>保存");
            $("#btn").attr("class","");
        },
        CloseForm: function() {
            parent.Boxy.getIframeDialog('<%=Request.QueryString["iframeId"] %>').hide();
        },
        CheckForm:function(){
            if($("#<%=txtAccount.ClientID %>").val()==""){
                $("#AccountTip").show();
                $("#PwdTip").hide();
                return false;
            }
            else if($("#<%=txtPwd.ClientID %>").val()==""){
                    $("#AccountTip").hide();
                    $("#PwdTip").show();
                    return false;
            }
            else{
               return true;
            }
           
        }

    }
   
</script>


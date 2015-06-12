<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HuiYuan01.aspx.cs" Inherits="EyouSoft.Web.YouLun.HuiYuan01"
    MasterPageFile="~/MasterPage/Boxy.Master" %>

<%@ Register Src="~/YouLun/WUC/wucupload.ascx" TagName="wucupload" TagPrefix="uc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="PageBody">
    <div class="alertbox-outbox">
        <form id="i_form">
        <div style="width: 99%; margin: 0px auto;">
            <table cellspacing="0" cellpadding="0" style="width: 100%; margin: 0 auto" class="firsttable">
                <tr>
                    <td style="background: #b7e0f3; height: 30px; text-align: left; width: 80px;" class="addtableT">
                        会员账号
                    </td>
                    <td>
                        <input type="text" class="inputtext" style="width: 280px;" runat="server" id="txtYongHuMing"
                            valid="required" errmsg="请填写会员账号" maxlength="50" />
                    </td>
                </tr>
                <tr>
                    <td style="background: #b7e0f3; height: 30px; text-align: left; width: 80px;" class="addtableT">
                        登录密码
                    </td>
                    <td>
                        <input type="text" class="inputtext" style="width:280px;" runat="server" id="txtMiMa" maxlength="50" />
                    </td>
                </tr>
            </table>
            <div style="color:#666">
                说明：登录密码为空时保留原密码</div>
        </div>
        <div style="text-align: right;" class="alertbox-btn">
            <a href="javascript:void(0)" id="i_baocun"><s class="baochun"></s>保 存</a>
        </div>
        </form>
    </div>

    <script type="text/javascript">
        var iPage = {
            reload: function() {
                window.location.href = window.location.href;
            },
            close: function() {
                var _win = top || window;
                _win.Boxy.getIframeDialog('<%=EyouSoft.Common.Utils.GetQueryStringValue("iframeId") %>').hide();
                return false;
            },
            baoCun: function(obj) {
                var validatorResult = ValiDatorForm.validator($("#i_form").get(0), "parent");
                if (!validatorResult) return;
                $(obj).unbind("click").css({ "color": "#999999" });
                if (!confirm("你确定要修改会员账号信息吗？")) return false;
                $.newAjax({
                    type: "POST",
                    url: window.location.href + "&doType=baocun",
                    data: $("#i_baocun").closest("form").serialize(),
                    cache: false,
                    dataType: "json",
                    async: false,
                    success: function(response) {
                        if (response.result == "1") {
                            alert(response.msg);
                            iPage.close();
                        } else {
                            alert(response.msg);
                            $(obj).bind("click", function() { iPage.baoCun(obj); }).css({ "color": "" });
                        }
                    }
                });
            }
        };

        $(document).ready(function() {
            $("#i_baocun").bind("click", function() { iPage.baoCun(this); });
        });
    </script>

</asp:Content>

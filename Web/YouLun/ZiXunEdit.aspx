<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ZiXunEdit.aspx.cs" Inherits="EyouSoft.Web.YouLun.ZiXunEdit"
    MasterPageFile="~/MasterPage/Boxy.Master" ValidateRequest="false" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="PageBody">
    <div class="alertbox-outbox">
        <form id="i_form">
        <div style="width: 99%; margin: 0px auto;">
            <table cellspacing="0" cellpadding="0" style="width: 100%; margin: 0 auto" class="firsttable">
                <tr>
                    <td style="background: #b7e0f3; height: 30px; text-align: left; width: 80px;" class="addtableT">
                        资讯标题
                    </td>
                    <td>
                        <input type="text" runat="server" id="txtBiaoTi" class="inputtext" style="width: 380px;"
                            valid="required" errmsg="请填写资讯标题" />
                    </td>
                </tr>
                <tr>
                    <td style="background: #b7e0f3; height: 30px; text-align: left; width: 80px;" class="addtableT">
                        资讯类型
                    </td>
                    <td>
                        <select name="txtLeiXing" id="txtLeiXing" class="inputselect" valid="required" errmsg="请选择资讯类型">
                            <option value="">-请选择-</option>
                            <%=EyouSoft.Common.UtilsCommons.GetEnumDDL(EyouSoft.Common.EnumObj.GetList(typeof(EyouSoft.Model.EnumType.YlStructure.WzZiXunLeiXing),new string[]{"0"}))%>
                        </select>
                    </td>
                </tr>
                <tr>
                    <td style="background: #b7e0f3; height: 30px; text-align: left;" class="addtableT">
                        资讯内容
                    </td>
                    <td>
                        <textarea runat="server" id="txtNeiRong"></textarea>
                    </td>
                </tr>
            </table>
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
                KEditer.sync();
                $(obj).unbind("click").css({ "color": "#999999" });

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
                    },
                    error: function() {
                        $(obj).bind("click", function() { iPage.baoCun(obj); }).css({ "color": "" });
                    }
                });
            }
        };

        $(document).ready(function() {
            KEditer["UPLOAD_P1"] = "YL";
            var _keoptions = { items: keSimpleYL, height: "350px", width: "680px" }
            KEditer.init('<%=txtNeiRong.ClientID %>', _keoptions);
            $("#i_baocun").bind("click", function() { iPage.baoCun(this); });
            $("#txtLeiXing").val("<%=LeiXing %>");

        });
    </script>

</asp:Content>

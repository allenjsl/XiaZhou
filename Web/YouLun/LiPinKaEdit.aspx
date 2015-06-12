<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LiPinKaEdit.aspx.cs" Inherits="EyouSoft.Web.YouLun.LiPinKaEdit"
    MasterPageFile="~/MasterPage/Boxy.Master" ValidateRequest="false" %>

<%@ Register Src="~/YouLun/WUC/wucupload.ascx" TagName="wucupload" TagPrefix="uc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="PageBody">
    <div class="alertbox-outbox">
        <form id="i_form">
        <div style="width: 99%; margin: 0px auto;">
            <table cellspacing="0" cellpadding="0" style="width: 100%; margin: 0 auto" class="firsttable">
                <tr>
                    <td style="background: #b7e0f3; height: 30px; text-align: left; width: 80px;" class="addtableT">
                        礼品卡名称
                    </td>
                    <td>
                        <input type="text" runat="server" id="txtMingCheng" class="inputtext" style="width: 380px;"
                            valid="required" errmsg="请填写礼品卡名称" />
                    </td>
                </tr>
                <tr>
                    <td style="background: #b7e0f3; height: 30px; text-align: left; width: 80px;" class="addtableT">
                        礼品卡类型
                    </td>
                    <td>
                        <select name="txtLeiXing" id="txtLeiXing" class="inputselect" valid="required" errmsg="请选择类型">
                            <option value="">-请选择-</option>
                            <%=EyouSoft.Common.UtilsCommons.GetEnumDDL(EyouSoft.Common.EnumObj.GetList(typeof(EyouSoft.Model.EnumType.YlStructure.LiPinKaLeiXing)))%>
                        </select>
                    </td>
                </tr>
                <tr>
                    <td style="background: #b7e0f3; height: 30px; text-align: left;" class="addtableT">
                        礼品卡图片
                    </td>
                    <td>
                        <uc1:wucupload runat="server" ID="upload1" IsUploadMore="false" IsUploadSelf="true"
                            FileTypes="*.jpg;*.gif;*.jpeg;*.png" />
                    </td>
                </tr>
                <tr>
                    <td style="background: #b7e0f3; height: 30px; text-align: left; width: 80px;" class="addtableT">
                        卡片金额
                    </td>
                    <td>
                        <input type="text" runat="server" id="txtJinE" class="inputtext" style="width: 380px;" />
                    </td>
                </tr>
                <tr>
                    <td style="background: #b7e0f3; height: 30px; text-align: left; width: 80px;" class="addtableT">
                        购买金额
                    </td>
                    <td>
                        <input type="text" runat="server" id="txtJinE1" class="inputtext" style="width: 380px;" />
                    </td>
                </tr>
                <tr>
                    <td style="background: #b7e0f3; height: 30px; text-align: left; width: 80px;" class="addtableT">
                        发票快递费用
                    </td>
                    <td>
                        <input type="text" runat="server" id="txtFaPiaoKuaiDiJinE" class="inputtext" style="width: 380px;" />
                    </td>
                </tr>
                <tr>
                    <td style="background: #b7e0f3; height: 30px; text-align: left;" class="addtableT">
                        礼品卡描述
                    </td>
                    <td>
                        <textarea runat="server" id="txtMiaoShu"></textarea>
                    </td>
                </tr>
                <tr>
                    <td style="background: #b7e0f3; height: 30px; text-align: left;" class="addtableT">
                        购买须知
                    </td>
                    <td>
                        <textarea runat="server" id="txtXuZhi"></textarea>
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

                $(obj).unbind("click").css({ "color": "#999999" });
                KEditer.sync();
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
            $("#i_baocun").bind("click", function() { iPage.baoCun(this); });
            var _keoptions = { items: keSimple, height: "350px", width: "700px" }
            KEditer.init('<%=txtMiaoShu.ClientID %>', _keoptions);
            KEditer.init('<%=txtXuZhi.ClientID %>', _keoptions);
            $("#txtLeiXing").val("<%=LeiXing %>")
        });
    </script>

</asp:Content>

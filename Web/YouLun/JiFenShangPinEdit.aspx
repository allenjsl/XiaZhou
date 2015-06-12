<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="JiFenShangPinEdit.aspx.cs"
    Inherits="EyouSoft.Web.YouLun.JiFenShangPinEdit" MasterPageFile="~/MasterPage/Boxy.Master" ValidateRequest="false" %>

<%@ Register Src="~/YouLun/WUC/wucupload.ascx" TagName="wucupload" TagPrefix="uc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="PageBody">
    <div class="alertbox-outbox">
        <form id="i_form">
        <div style="width: 99%; margin: 0px auto;">
            <table cellspacing="0" cellpadding="0" style="width: 100%; margin: 0 auto" class="firsttable">
                <tr>
                    <td style="background: #b7e0f3; height: 30px; text-align: left; width: 80px;" class="addtableT">
                        商品名称
                    </td>
                    <td>
                        <input type="text" runat="server" id="txtMingCheng" class="inputtext" style="width: 380px;"
                            valid="required" errmsg="请填写商品名称" />
                    </td>
                </tr>
                <tr>
                    <td style="background: #b7e0f3; height: 30px; text-align: left; width: 80px;" class="addtableT">
                        商品金额
                    </td>
                    <td>
                        <input type="text" runat="server" id="txtShangPinJinE" class="inputtext" style="width: 380px;"
                            valid="required" errmsg="请填写商品金额" />
                    </td>
                </tr>
                <tr>
                    <td style="background: #b7e0f3; height: 30px; text-align: left; width: 80px;" class="addtableT">
                        商品数量
                    </td>
                    <td>
                        <input type="text" runat="server" id="txtShuLiang" class="inputtext" style="width: 380px;"
                            valid="required" errmsg="请填写商品数量" />
                    </td>
                </tr>
                <tr>
                    <td style="background: #b7e0f3; height: 30px; text-align: left; width: 80px;" class="addtableT">
                        状态
                    </td>
                    <td>
                        <select name="txtStatus" id="txtStatus" class="inputselect" valid="required" errmsg="请选择状态">
                            <option value="">-请选择-</option>
                            <%=EyouSoft.Common.UtilsCommons.GetEnumDDL(EyouSoft.Common.EnumObj.GetList(typeof(EyouSoft.Model.EnumType.YlStructure.JiFenShangPinStatus)))%>
                        </select>
                    </td>
                </tr>
                <tr>
                    <td style="background: #b7e0f3; height: 30px; text-align: left;" class="addtableT">
                        商品图片
                    </td>
                    <td>
                        <uc1:wucupload runat="server" ID="upload1" IsUploadMore="true" IsUploadSelf="true"
                            FileTypes="*.jpg;*.gif;*.jpeg;*.png" YangShi="1" />
                    </td>
                </tr>
                <tr>
                    <td style="background: #b7e0f3; height: 30px; text-align: left;" class="addtableT">
                        兑换方式
                    </td>
                    <td style="line-height:25px;">
                        积分：<input runat="server" id="txtJiFen1" class="inputtext" style="width: 100px" />积分<br />
                        <%--积分+礼品卡：<input runat="server" id="txtJiFen2" class="inputtext" style="width: 100px" />积分+<input runat="server" id="txtJinE2" class="inputtext" style="width: 100px" />元<br />--%>
                        积分+现金：<input runat="server" id="txtJiFen3" class="inputtext" style="width: 100px" />积分+<input runat="server" id="txtJinE3" class="inputtext" style="width: 100px" />元
                    </td>
                </tr>
                <tr>
                    <td style="background: #b7e0f3; height: 30px; text-align: left; width: 80px;" class="addtableT">
                        配送方式
                    </td>
                    <td>
                        <input type="text" runat="server" id="txtPeiSongFangShi" class="inputtext" style="width: 380px;" />
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
                        商品说明
                    </td>
                    <td>
                        <textarea runat="server" id="txtShuoMing"></textarea>
                    </td>
                </tr>
                <tr>
                    <td style="background: #b7e0f3; height: 30px; text-align: left;" class="addtableT">
                        兑换须知
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
            KEditer["UPLOAD_P1"] = "YL";
            $("#i_baocun").bind("click", function() { iPage.baoCun(this); });
            var _keoptions = { items: keSimpleYL, height: "350px", width: "700px" }
            KEditer.init('<%=txtShuoMing.ClientID %>', _keoptions);
            KEditer.init('<%=txtXuZhi.ClientID %>', _keoptions);
            $("#txtStatus").val("<%=Status %>")
        });
    </script>

</asp:Content>

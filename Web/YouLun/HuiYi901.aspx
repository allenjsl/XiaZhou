<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HuiYi901.aspx.cs" Inherits="EyouSoft.Web.YouLun.HuiYi901"
    MasterPageFile="~/MasterPage/Boxy.Master" %>

<%@ Register Src="~/YouLun/WUC/wucupload.ascx" TagName="wucupload" TagPrefix="uc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="PageBody">
    <div class="alertbox-outbox">
        <form id="i_form">
        <div style="width: 99%; margin: 0px auto;">
            <table cellspacing="0" cellpadding="0" style="width: 100%; margin: 0 auto" class="firsttable">
                <tr>
                    <td style="background: #b7e0f3; height: 30px; text-align: left; width: 80px;" class="addtableT">
                        会议名称
                    </td>
                    <td>
                        <input type="text" runat="server" id="txtMingCheng" class="inputtext" style="width: 380px;"
                            valid="required" errmsg="请填写会议名称" />
                    </td>
                </tr>
                <tr>
                    <td style="background: #b7e0f3; height: 30px; text-align: left; " class="addtableT">
                        游轮类型
                    </td>
                    <td>
                        <select name="txtLeiXing" id="txtLeiXing" class="inputselect" valid="required" errmsg="请选择游轮类型">
                            <option value="">-请选择-</option>
                            <%=EyouSoft.Common.UtilsCommons.GetEnumDDL(EyouSoft.Common.EnumObj.GetList(typeof(EyouSoft.Model.EnumType.YlStructure.YouLunLeiXing)))%>
                        </select>
                    </td>
                </tr>
                <tr>
                    <td style="background: #b7e0f3; height: 30px; text-align: left; " class="addtableT">
                        会议时间
                    </td>
                    <td>
                        <input type="text" runat="server" id="txtShiJian1" class="inputtext" style="width: 100px;"
                            onfocus="WdatePicker()" valid="required" errmsg="请填写开始时间" />-<input type="text" runat="server"
                                id="txtShiJian2" class="inputtext" style="width: 100px;" onfocus="WdatePicker()"
                                valid="required" errmsg="请填写结束时间" />
                    </td>
                </tr>
                <tr>
                    <td style="background: #b7e0f3; height: 30px; text-align: left;" class="addtableT">
                        案例图片
                    </td>
                    <td>
                        <uc1:wucupload runat="server" ID="upload1" IsUploadMore="false" IsUploadSelf="true"
                            FileTypes="*.jpg;*.gif;*.jpeg;*.png" YangShi="1" />
                    </td>
                </tr>
            </table>
            <div style="height: 30px; line-height: 30px; color: #999">
                上传图片大小：宽248px高120px;</div>
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

            $("#txtLeiXing").val("<%=LeiXing %>")
        });
    </script>

</asp:Content>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="YiJianFanKuiEdit.aspx.cs" Inherits="EyouSoft.Web.YouLun.YiJianFanKuiEdit" MasterPageFile="~/MasterPage/Boxy.Master" %>

<%@ Register Src="~/YouLun/WUC/wucupload.ascx" TagName="wucupload" TagPrefix="uc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="PageBody">
    <div class="alertbox-outbox">
        <form id="i_form">
        <div style="width: 99%; margin: 0px auto;">
            <table cellspacing="0" cellpadding="0" style="width: 100%; margin: 0 auto" class="firsttable">
                <tr>
                    <td style="background: #b7e0f3; height: 30px; text-align: left; width: 80px;" class="addtableT">
                        问题类型
                    </td>
                    <td>
                        <%foreach (var e in Enum.GetValues(typeof(EyouSoft.Model.EnumType.YlStructure.YiJianFanKuiLeiXing))) %>
                        <%{ %>
                            <label><input type="radio" value="<%=(int)e %>" name="leixing" <%=(int)e==(int)this.LeiXing?"checked":"" %> /><%=Enum.GetName(typeof(EyouSoft.Model.EnumType.YlStructure.YiJianFanKuiLeiXing), e)%></label>
                        <%} %>
                    </td>
                </tr>
                <tr>
                    <td style="background: #b7e0f3; height: 30px; text-align: left;" class="addtableT">
                        上传文件
                    </td>
                    <td>
                        <uc1:wucupload runat="server" ID="upload1" IsUploadMore="false" IsUploadSelf="true" YangShi="1" />
                    </td>
                </tr>
                <tr>
                    <td style="background: #b7e0f3; height: 30px; text-align: left; width: 80px;" class="addtableT">
                        问题描述
                    </td>
                    <td>
                        <textarea id="txtMiaoShu" rows="10" cols="80" runat="server"></textarea>
                    </td>
                </tr>
                <tr>
                    <td style="background: #b7e0f3; height: 30px; text-align: left; width: 80px;" class="addtableT">
                        IP地址
                    </td>
                    <td>
                        <asp:Literal ID="litRemoteIP" runat="server"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td style="background: #b7e0f3; height: 30px; text-align: left; width: 80px;" class="addtableT">
                        操作时间
                    </td>
                    <td>
                        <asp:Literal ID="litIssueTime" runat="server"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td style="background: #b7e0f3; height: 30px; text-align: left; width: 80px;" class="addtableT">
                        操作人员
                    </td>
                    <td>
                        <asp:Literal ID="litOperator" runat="server"></asp:Literal>
                    </td>
                </tr>
            </table>
        </div>
        <%--<div style="text-align: right;" class="alertbox-btn">
            <a href="javascript:void(0)" id="i_baocun"><s class="baochun"></s>保 存</a>
        </div>--%>
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
        });
    </script>

</asp:Content>

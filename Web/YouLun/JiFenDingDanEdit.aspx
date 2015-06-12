<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="JiFenDingDanEdit.aspx.cs"
    Inherits="EyouSoft.Web.YouLun.JiFenDingDanEdit" MasterPageFile="~/MasterPage/Boxy.Master" %>

<%@ Register Src="~/YouLun/WUC/wucupload.ascx" TagName="wucupload" TagPrefix="uc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="PageBody">
    <div class="alertbox-outbox">
        <form id="i_form">
        <div style="width: 99%; margin: 0px auto;">
            <table cellspacing="0" cellpadding="0" style="width: 100%; margin: 0 auto" class="firsttable">
                <tr>
                    <td style="background: #b7e0f3; height: 30px; text-align: left; width: 80px;" class="addtableT">
                        订单号
                    </td>
                    <td>
                        <asp:Literal runat="server" ID="ltrDingDanHao"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td style="background: #b7e0f3; height: 30px; text-align: left; width: 80px;" class="addtableT">
                        商品名称
                    </td>
                    <td>
                        <asp:Literal runat="server" ID="ltrShangPinMingCheng"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td style="background: #b7e0f3; height: 30px; text-align: left; width: 80px;" class="addtableT">
                        会员帐号
                    </td>
                    <td>
                        <asp:Literal runat="server" ID="ltrHuiYuanXingMing"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td style="background: #b7e0f3; height: 30px; text-align: left; width: 80px;" class="addtableT">
                        兑换数量
                    </td>
                    <td>
                        <asp:Literal runat="server" ID="ltrShuLiang"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td style="background: #b7e0f3; height: 30px; text-align: left; width: 80px;" class="addtableT">
                        兑换方式
                    </td>
                    <td>
                        <asp:Literal runat="server" ID="ltrFangShi"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td style="background: #b7e0f3; height: 30px; text-align: left; width: 80px;" class="addtableT">
                        兑换金额
                    </td>
                    <td>
                        <asp:Literal runat="server" ID="ltrJinE"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td style="background: #b7e0f3; height: 30px; text-align: left; width: 80px;" class="addtableT">
                        订单状态
                    </td>
                    <td>
                        <asp:Literal runat="server" ID="ltrDingDanStatus"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td style="background: #b7e0f3; height: 30px; text-align: left; width: 80px;" class="addtableT">
                        付款状态
                    </td>
                    <td>
                        <asp:Literal runat="server" ID="ltrFuKuanStatus"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td style="background: #b7e0f3; height: 30px; text-align: left; width: 80px;" class="addtableT">
                        发票信息
                    </td>
                    <td style="line-height:25px;">
                        <asp:Literal runat="server" ID="ltrFaPiao"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td style="background: #b7e0f3; height: 30px; text-align: left; width: 80px;" class="addtableT">
                        发票配送方式
                    </td>
                    <td style="line-height:24px;">
                        <asp:Literal runat="server" ID="ltrPeiSongFangShi"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td style="background: #b7e0f3; height: 30px; text-align: left; width: 80px;" class="addtableT">
                        下单时间
                    </td>
                    <td>
                        <asp:Literal runat="server" ID="ltrXiaDanShiJian"></asp:Literal>
                    </td>
                </tr>
            </table>
        </div>
        <div style="text-align: right;" class="alertbox-btn">
            <asp:PlaceHolder runat="server" ID="phQuXiao" Visible="false">
            <a href="javascript:void(0)" id="i_quxiao">取消订单</a>
            </asp:PlaceHolder>
            <%--<a href="javascript:void(0)" id="i_baocun"><s class="baochun"></s>保 存</a>--%>
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
            },
            quXiao: function(obj) {
                if (!confirm("你确定要取消订单吗？")) return false;
                $(obj).unbind("click").css({ "color": "#999999" });
                $.newAjax({
                    type: "POST",
                    url: window.location.href + "&doType=quxiao",
                    cache: false,
                    dataType: "json",
                    async: false,
                    success: function(response) {
                        if (response.result == "1") {
                            alert(response.msg);
                            iPage.close();
                        } else {
                            alert(response.msg);
                            $(obj).bind("click", function() { iPage.quXiao(obj); }).css({ "color": "" });
                        }
                    },
                    error: function() {
                        $(obj).bind("click", function() { iPage.quXiao(obj); }).css({ "color": "" });
                    }
                });
            }
        };

        $(document).ready(function() {
            $("#i_baocun").bind("click", function() { iPage.baoCun(this); });
            $("#i_quxiao").bind("click", function() { iPage.quXiao(this); });
        });
    </script>

</asp:Content>

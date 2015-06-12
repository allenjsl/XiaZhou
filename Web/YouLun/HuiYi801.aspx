<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HuiYi801.aspx.cs" Inherits="EyouSoft.Web.YouLun.HuiYi801"
    MasterPageFile="~/MasterPage/Boxy.Master" %>

<%@ Register Src="~/YouLun/WUC/wucupload.ascx" TagName="wucupload" TagPrefix="uc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="PageBody">
    <div class="alertbox-outbox">
        <form id="i_form">
        <div style="width: 99%; margin: 0px auto;">
            <table cellspacing="0" cellpadding="0" style="width: 100%; margin: 0 auto" class="firsttable">
                <tr>
                    <td style="background: #b7e0f3; height: 30px; text-align: left; width: 80px;" class="addtableT">
                        会议规模
                    </td>
                    <td>
                        <asp:Literal runat="server" ID="ltrGuiMo"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td style="background: #b7e0f3; height: 30px; text-align: left; width: 80px;" class="addtableT">
                        会议预计时间
                    </td>
                    <td>
                        <asp:Literal runat="server" ID="ltrYuJiShiJian"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td style="background: #b7e0f3; height: 30px; text-align: left; width: 80px;" class="addtableT">
                        游轮类型
                    </td>
                    <td>
                        <asp:Literal runat="server" ID="ltrLeiXing"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td style="background: #b7e0f3; height: 30px; text-align: left; width: 80px;" class="addtableT">
                        联系人姓名
                    </td>
                    <td>
                        <asp:Literal runat="server" ID="ltrLxrXingMing"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td style="background: #b7e0f3; height: 30px; text-align: left; width: 80px;" class="addtableT">
                        联系人手机
                    </td>
                    <td>
                        <asp:Literal runat="server" ID="ltrLxrShouJi"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td style="background: #b7e0f3; height: 30px; text-align: left; width: 80px;" class="addtableT">
                        联系人邮箱
                    </td>
                    <td>
                        <asp:Literal runat="server" ID="ltrLxrYouXiang"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td style="background: #b7e0f3; height: 30px; text-align: left; width: 80px;" class="addtableT">
                        联系人地址
                    </td>
                    <td>
                        <asp:Literal runat="server" ID="ltrLxrDiZhi"></asp:Literal>
                    </td>
                </tr> 
                <tr>
                    <td style="background: #b7e0f3; height: 30px; text-align: left; width: 80px;" class="addtableT">
                        行业名称
                    </td>
                    <td>
                        <asp:Literal runat="server" ID="ltrHangYeMingCheng"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td style="background: #b7e0f3; height: 30px; text-align: left; width: 80px;" class="addtableT">
                        行业联系方式
                    </td>
                    <td>
                        <asp:Literal runat="server" ID="ltrHangYeLianXiFangShi"></asp:Literal>
                    </td>
                </tr> 
                <tr>
                    <td style="background: #b7e0f3; height: 30px; text-align: left; width: 80px;" class="addtableT">
                        会议申请时间
                    </td>
                    <td>
                        <asp:Literal runat="server" ID="ltrShenQingShiJian"></asp:Literal>
                    </td>
                </tr>                    
                <tr>
                    <td style="background: #b7e0f3; height: 30px; text-align: left; width: 80px;" class="addtableT">
                        处理结果
                    </td>
                    <td>
                        <textarea runat="server" id="txtChuLiBeiZhu" cols="100" rows="5"></textarea>
                    </td>
                </tr>        
                <tr>
                    <td style="background: #b7e0f3; height: 30px; text-align: left; width: 80px;" class="addtableT">
                        处理时间
                    </td>
                    <td>
                        <asp:Literal runat="server" ID="ltrChuLiShiJian"></asp:Literal>
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

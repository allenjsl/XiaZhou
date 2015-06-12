<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="JiDiaoAnPaiBianGengEdit.aspx.cs"
    Inherits="EyouSoft.Web.CommonPage.JiDiaoAnPaiBianGengEdit" MasterPageFile="~/MasterPage/Boxy.Master" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="PageBody">
    <form id="form1">
    <div class="alertbox-outbox">
        <div style="width: 99%; margin: 0px auto;">
            <table cellspacing="0" cellpadding="0" style="width: 100%; margin: 0 auto" class="firsttable">
                <tr>
                    <td class="addtableT" style="width:70px;">安排类型：</td>
                    <td class="kuang2"><asp:Literal runat="server" ID="ltrAnPaiLeiXing"></asp:Literal><input type="hidden" id="txtAnPaiLeiXing" runat="server" /></td>
                </tr>
                <tr>
                    <td class="addtableT" style="width:70px;"><asp:Literal runat="server" ID="ltrGysTitle" >供应商</asp:Literal>：</td>
                    <td class="kuang2"><asp:Literal runat="server" ID="ltrGysName"></asp:Literal></td>
                </tr>
                <tr>
                    <td class="addtableT" style="width:70px;"><%=JiaJianLeiXing1%>人数：</td>
                    <td class="kuang2"><input type="text" class="inputtext formsize100" maxlength="4" id="txtRenShu" runat="server" /></td>
                </tr>
                <tr>
                    <td class="addtableT"><%=JiaJianLeiXing1%>费用：</td>
                    <td class="kuang2"><input type="text" class="inputtext formsize100" maxlength="11" id="txtJinE" runat="server" /></td>
                </tr>
                <tr>
                    <td class="addtableT"><%=JiaJianLeiXing1%>备注：</td>
                    <td class="kuang2"><textarea id="txtBeiZhu" runat="server" class="inputarea" cols="80" rows="5"></textarea></td>
                </tr>
                <tr>
                    <td class="addtableT">费用明细：</td>
                    <td class="kuang2"><textarea id="txtFeiYongMingXi" runat="server" class="inputarea" cols="80" rows="5"></textarea></td>
                </tr>
            </table>
        </div>
        
        <div class="alertbox-btn">
            <asp:Literal runat="server" ID="ltrOperatorHtml"></asp:Literal>&nbsp;&nbsp;
        </div>
    </div>
    </form>

    <script type="text/javascript">
        var iPage = {
            close: function() {
                top.Boxy.getIframeDialog('<%=EyouSoft.Common.Utils.GetQueryStringValue("iframeId") %>').hide();
                return false;
            },
            submit: function(obj) {
                if (tableToolbar.getFloat($("#<%=txtRenShu.ClientID %>").val()) < 0) { parent.tableToolbar._showMsg("<%=JiaJianLeiXing1 %>人数必须大于等于0"); return; }
                if (tableToolbar.getFloat($("#<%=txtJinE.ClientID %>").val()) < 0) { parent.tableToolbar._showMsg("<%=JiaJianLeiXing1 %>费用必须大于等于0"); return; }

                var validatorResult = ValiDatorForm.validator($("form").get(0), "parent");
                if (!validatorResult) return false;

                $(obj).unbind("click").text("处理中....");

                $.ajax({
                    type: "POST", url: window.location.href + "&doType=submit",
                    data: $("form").serialize(), cache: false, dataType: "json", async: false,
                    success: function(response) {
                        if (response.result == "1") {
                            alert(response.msg);
                            parent.location.href = parent.location.href;
                            iPage.close();
                        } else {
                            alert(response.msg);
                            $(obj).bind("click", function() { iPage.submit(obj); }).text("保存");
                        }
                    },
                    error: function() {
                        $(obj).bind("click", function() { iPage.submit(obj); }).text("保存");
                    }
                });
            }
        };

        $(document).ready(function() {
            $("#<%=txtRenShu.ClientID %>").attr("valid", "required|isInt").attr("errmsg", "请填写<%=JiaJianLeiXing1 %>人数|请填写正确的<%=JiaJianLeiXing1 %>人数");
            $("#<%=txtJinE.ClientID %>").attr("valid", "required|isNumber").attr("errmsg", "请填写<%=JiaJianLeiXing1 %>费用|请填写正确的<%=JiaJianLeiXing1 %>费用");
            $("#i_a_submit").click(function() { iPage.submit(this); });

            var anPaiLeiXing = $("#<%=txtAnPaiLeiXing.ClientID %>").val();
            if (anPaiLeiXing == "<%=(int)EyouSoft.Model.EnumType.PlanStructure.PlanProject.国内游轮 %>" || anPaiLeiXing == "<%=(int)EyouSoft.Model.EnumType.PlanStructure.PlanProject.涉外游轮 %>") {
                $("#<%=txtRenShu.ClientID %>").attr("valid", "isMoney").attr("errmsg", "请填写正确的人数").attr("maxlength", "6");
            } else {
                $("#<%=txtRenShu.ClientID %>").attr("valid", "RegInteger").attr("errmsg", "请填写正确的人数");
            }
        });
    </script>

</asp:Content>

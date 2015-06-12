<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Edit.aspx.cs" Inherits="EyouSoft.Web.FinanceManage.DaiShou.Edit"
    MasterPageFile="~/MasterPage/Boxy.Master" %>

<asp:Content runat="server" ContentPlaceHolderID="PageBody">
    <form runat="server">
    <input type="hidden" id="txtOrderId" runat="server" />
    <input type="hidden" id="txtAnPaiId" runat="server" />
    <div class="alertbox-outbox">
        <div style="width:99%; margin:0px auto;">
            <table cellspacing="0" cellpadding="0" style="width: 100%; margin: 0 auto" class="firsttable">
                <tr>
                    <td colspan="6" style="background: #b7e0f3; height: 30px; text-align:left;" class="addtableT">请选择需要代收的订单</td>
                </tr>
                <tr>
                    <td style="height:30px; width:50px;">&nbsp;</td>
                    <td style="width:200px;">订单号</td>
                    <td>客户单位</td>
                    <td>销售员</td>
                    <td>下单人</td>
                    <td style="text-align:right; width:100px;">合同金额</td>
                </tr>
                <asp:Repeater runat="server" ID="rptOrder">
                <ItemTemplate>
                <tr>
                    <td style="height:30px; text-align:center;"><input type="radio" name="radioOrder" value="<%#Eval("OrderId") %>" /></td>
                    <td><%#Eval("OrderCode") %></td>
                    <td><%#Eval("CrmName") %></td>
                    <td><%#Eval("SellerName") %></td>
                    <td><%#Eval("XiaDanRenName") %></td>
                    <td style="text-align:right;"><%#Eval("JinE","{0:C2}") %></td>
                </tr>
                </ItemTemplate>                
                </asp:Repeater>
                <asp:PlaceHolder runat="server" ID="phEmptyOrder" Visible="false">
                <tr>
                    <td colspan="6" style="height: 30px;">无订单供选择</td>
                </tr>
                </asp:PlaceHolder>
            </table>
            
            <table cellspacing="0" cellpadding="0" style="width: 100%; margin: 0 auto; margin-top: 10px;"
                class="firsttable">
                <tr>
                    <td colspan="4" style="background: #b7e0f3; height: 30px; text-align: left;" class="addtableT">请选择代收的供应商</td>
                </tr>
                <tr>
                    <td style="height:30px;width:50px;">&nbsp;</td>
                    <td>安排类型</td>
                    <td>供应商</td>
                    <td style="text-align:right; width:100px;"> 结算金额</td>
                </tr>
                <asp:Repeater runat="server" ID="rptAnPai">
                <ItemTemplate>
                <tr>
                    <td style="height:30px; text-align:center;"><input type="radio" name="radioAnPai" value="<%#Eval("AnPaiId") %>" /></td>
                    <td><%#Eval("AnPaiLeiXing") %></td>
                    <td><%#Eval("GysName") %></td>
                    <td style="text-align:right;"><%#Eval("JinE","{0:C2}") %></td>
                </tr>
                </ItemTemplate>                
                </asp:Repeater>
                <asp:PlaceHolder runat="server" ID="phEmptyAnPai" Visible="false">
                <tr>
                    <td colspan="4" style="height: 30px;">无供应商供选择</td>
                </tr>
                </asp:PlaceHolder>
            </table>
            
            <table cellspacing="0" cellpadding="0" style="width: 100%; margin: 0 auto; margin-top: 10px;"
                class="firsttable">
                <tr>
                    <td class="addtableT" style="width:100px;">代收时间：</td>
                    <td class="kuang2"><input type="text" id="txtTime" runat="server" class="inputtext formsize100" onfocus="WdatePicker()" valid="required" errmsg="请填写代收时间" /></td>
                </tr>
                <tr>
                    <td class="addtableT">代收金额：</td>
                    <td class="kuang2"><input type="text" id="txtJinE" runat="server" class="inputtext formsize100" valid="required|isNumber"
                            errmsg="请填写代收金额|请填写正确的代收金额" /></td>
                </tr>
                <tr>
                    <td class="addtableT">代收备注：</td>
                    <td class="kuang2"><textarea id="txtBeiZhu" runat="server" class="inputarea" cols="80" rows="5"></textarea></td>
                </tr>
                <tr>
                    <td class="addtableT">登记人：</td>
                    <td class="kuang2"><asp:Literal runat="server" ID="ltrDengJiRen"></asp:Literal></td>
                </tr>
                <asp:PlaceHolder runat="server" ID="phShenPiRen" Visible="false">
                <tr>
                    <td class="addtableT">审批人：</td>
                    <td class="kuang2"><asp:Literal runat="server" ID="ltrShenPiRen"></asp:Literal></td>
                </tr>
                </asp:PlaceHolder>
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
            init: function() {
                var _orderId = $.trim($("#<%=txtOrderId.ClientID %>").val());
                var _anPaiId = $.trim($("#<%=txtAnPaiId.ClientID %>").val());

                if (_orderId.length > 0) $("input[name='radioOrder'][value='" + _orderId + "']").attr("checked", "checked");
                if (_anPaiId.length > 0) $("input[name='radioAnPai'][value='" + _anPaiId + "']").attr("checked", "checked");
            },
            submit: function(obj) {
                if ($.trim($('input[name="radioOrder"]:checked').val()).length == 0) { parent.tableToolbar._showMsg("请选择需要代收的订单"); return; }
                if ($.trim($('input[name="radioAnPai"]:checked').val()).length == 0) { parent.tableToolbar._showMsg("请选择代收的供应商"); return; }
                if (tableToolbar.getFloat($("#<%=txtJinE.ClientID %>").val()) <= 0) { parent.tableToolbar._showMsg("代收金额必须大于0"); return; }

                var validatorResult = ValiDatorForm.validator($("form").get(0), "parent");
                if (!validatorResult) return false;

                $(obj).unbind("click").text("处理中....");

                $.ajax({
                    type: "POST", url: window.location.href + "&doType=submit",
                    data: $("form").serialize(), cache: false, dataType: "json", async: false,
                    success: function(response) {
                        if (response.result == "1") {
                            alert(response.msg);
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
            },
            shenPi: function(obj) {
                if (!confirm("审批操作不可逆，你确定要审批吗？")) return;
                $(obj).unbind("click").text("处理中....");
                $.ajax({
                    type: "POST", url: window.location.href + "&doType=shenpi",
                    cache: false, dataType: "json", async: false,
                    success: function(response) {
                        if (response.result == "1") {
                            alert(response.msg);
                            iPage.close();
                        } else {
                            alert(response.msg);
                            $(obj).bind("click", function() { iPage.submit(obj); }).text("审批");
                        }
                    },
                    error: function() {
                        $(obj).bind("click", function() { iPage.submit(obj); }).text("审批");
                    }
                });
            }
        };

        $(document).ready(function() {
            iPage.init();
            $("#i_a_submit").click(function() { iPage.submit(this); });
            $("#i_a_shenpi").click(function() { iPage.shenPi(this); });
        });
    </script>
</asp:Content>
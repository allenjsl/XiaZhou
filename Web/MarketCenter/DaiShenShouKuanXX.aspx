<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DaiShenShouKuanXX.aspx.cs"
    Inherits="EyouSoft.Web.MarketCenter.DaiShenShouKuanXX" MasterPageFile="~/MasterPage/Boxy.Master"
    ValidateRequest="false" %>
<%--待审核收款明细--%>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="PageBody">
    <div class="alertbox-outbox">
        <form method="post" id="form1">
        <input type="hidden" name="istoxls" value="1" />
        <table width="99%" border="0" align="center" cellpadding="0" cellspacing="0"
            style="margin: 0 auto;">
            <tr>
                <td style="height: 28px;" align="left" bgcolor="#B7E0F3">
                    <span class="alertboxTableT">序号</span>
                </td>
                <td align="center" bgcolor="#B7E0F3">
                    订单号
                </td>
                <td align="center" bgcolor="#B7E0F3">
                    收款日期
                </td>
                <td align="center" bgcolor="#B7E0F3">
                    收款人
                </td>
                <td bgcolor="#B7E0F3" style="text-align: right;">
                    收款金额&nbsp;
                </td>
                <td align="center" bgcolor="#B7E0F3">
                    收款方式
                </td>
                <td align="left" bgcolor="#B7E0F3">
                    备注
                </td>
            </tr>
            <asp:Repeater ID="rpt" runat="server">
                <ItemTemplate>
                    <tr>
                        <td height="28" align="left">
                            <input type="checkbox" name="chk" value="<%#Eval("Id") %>" />
                            <%#Container.ItemIndex+1 %>
                        </td>
                        <td align="center">
                            <%#Eval("OrderCode") %>
                        </td>
                        <td align="center">
                            <%#EyouSoft.Common.UtilsCommons.GetDateString(Eval("CollectionRefundDate"),ProviderToDate) %>
                        </td>
                        <td align="center">
                            <%#Eval("CollectionRefundOperator") %>
                        </td>
                        <td style="text-align: right;">
                            <%#EyouSoft.Common.UtilsCommons.GetMoneyString(Eval("CollectionRefundAmount"),ProviderToMoney)%>&nbsp;
                        </td>
                        <td align="center">
                            <%#Eval("CollectionRefundModeName")%>
                        </td>
                        <td align="center">
                            <%#Eval("Memo")%>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
            <asp:PlaceHolder runat="server" ID="phEmpty">
                <tr>
                    <td colspan="7" style="height: 28px; text-align: center;">
                        选中的订单暂无需要审批的收款登记信息。
                    </td>
                </tr>
            </asp:PlaceHolder>
            <asp:PlaceHolder runat="server" ID="phHeJiJinE">
                <tr>
                    <td style="height: 28px; " colspan="3">
                        <a href="javascript:void(0)" id="i_a_quanxuan">全选</a>&nbsp;&nbsp; 
                        <a href="javascript:void(0)" id="i_a_fanxuan">反选</a>
                    </td>
                    <td style="text-align: right;">
                        合计金额：
                    </td>
                    <td style="text-align: right;">
                        <b><asp:Literal runat="server" ID="ltrHeJiJinE"></asp:Literal></b>&nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
            </asp:PlaceHolder>
        </table>
        </form>
        <div class="alertbox-btn">
            <a id="i_a_daochu" href="javascript:void(0);" style="text-indent: 0px;">导出</a>
        </div>
    </div>
    
    <script type="text/javascript">
        var iPage = {
            close: function() {
                var _params = utilsUri.getUrlParams([]);
                parent.Boxy.getIframeDialog(_params["iframeId"]).hide();
            },
            daoChu: function() {
                var _s = [];
                var _$chks = $("input[type='checkbox']:checked");
                if (_$chks.length == 0) { alert("请选择需要导出的数据"); return; }

                $("#form1").submit();

            },
            quanXuan: function() {
                $("input[type='checkbox']").each(function() {
                    if (!this.checked) $(this).attr("checked", "checked");
                });
            },
            fanXuan: function() {
                $("input[type='checkbox']").each(function() {
                    if (this.checked) $(this).removeAttr("checked");
                    else $(this).attr("checked", "checked");
                });
            }
        };

        $(document).ready(function() {
            $("#i_a_daochu").click(function() { iPage.daoChu(); });
            $("#i_a_quanxuan").click(function() { iPage.quanXuan(); });
            $("#i_a_fanxuan").click(function() { iPage.fanXuan(); });
        });
    </script>
</asp:Content>

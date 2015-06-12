<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="YuSuanBiaoMaoLi.aspx.cs"
    Inherits="EyouSoft.Web.TongJi.YuSuanBiaoMaoLi" MasterPageFile="~/MasterPage/boxy.Master" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="PageBody">
    <div class="alertbox-outbox" id="alertbox" style="padding-top: 5px;">
        <div style="width: 98%; margin: 0px auto; line-height: 25px;">
            团队信息&nbsp;&nbsp; <span style="float: right;"><a href="javascript:void(0)" id="i_a_dayin">
                打印窗口</a></span>
        </div>
        <table style="width: 98%; margin: 0px auto;" cellspacing="0" cellpadding="0" bgcolor="#f3f3f3">
            <tr>
                <td style="height: 25px; width: 60px;">
                    线路名称
                </td>
                <td style="width: 27%;">
                    <asp:Literal runat="server" ID="ltrRouteName"></asp:Literal>
                </td>
                <td style="width: 60px;">
                    团号
                </td>
                <td style="width: 27%;">
                    <asp:Literal runat="server" ID="ltrTourCode"></asp:Literal>
                </td>
                <td style="width: 60px;">
                    行程天数
                </td>
                <td>
                    <asp:Literal runat="server" ID="ltrTianShu"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td style="height: 25px;">
                    出团日期
                </td>
                <td>
                    <asp:Literal runat="server" ID="ltrChuTuanRiQi"></asp:Literal>
                </td>
                <td>
                    回团日期
                </td>
                <td>
                    <asp:Literal runat="server" ID="ltrHuiTuanRiQi"></asp:Literal>
                </td>
                <td>
                    团队状态
                </td>
                <td>
                    <asp:Literal runat="server" ID="ltrTourStatus"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td style="height: 25px;">
                    销售员
                </td>
                <td>
                    <asp:Literal runat="server" ID="ltrXiaoShouYuan"></asp:Literal>
                </td>
                <td>
                    计调员
                </td>
                <td>
                    <asp:Literal runat="server" ID="ltrJiDiaoYuan"></asp:Literal>
                </td>
                <td>
                    实收人数
                </td>
                <td>
                    <asp:Literal runat="server" ID="ltrShiShouRenShu0"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td style="height: 25px;">
                    收入金额
                </td>
                <td>
                    <asp:Literal runat="server" ID="ltrShouRuJinE0"></asp:Literal>
                </td>
                <td>
                    支出金额
                </td>
                <td>
                    <asp:Literal runat="server" ID="ltrZhiChuJinE0"></asp:Literal>
                </td>
                <td>
                    毛利
                </td>
                <td>
                    <asp:Literal runat="server" ID="ltrMaoLi"></asp:Literal>
                </td>
            </tr>
        </table>
        <div style="width: 98%; margin: 0px auto; margin-top: 5px; line-height: 25px;">
            收入明细</div>
        <table width="98%" cellspacing="0" cellpadding="0" bgcolor="#FFFFFF" align="center"
            id="liststyle_shouru" style="margin: 0 auto">
            <tr style="background: url(/images/y-formykinfo.gif) repeat-x center top;" class="odd">
                <td height="28" bgcolor="#b7e0f3" align="right" class="alertboxTableT" style="width: 40px;">
                    序号&nbsp;
                </td>
                <td bgcolor="#b7e0f3" class="alertboxTableT">
                    &nbsp;订单号
                </td>
                <td bgcolor="#b7e0f3" class="alertboxTableT">
                    &nbsp;客户名称
                </td>
                <td bgcolor="#b7e0f3" class="alertboxTableT" style="width: 70px;">
                    &nbsp;销售员
                </td>
                <td bgcolor="#b7e0f3" class="alertboxTableT">
                    &nbsp;下单时间
                </td>
                <td bgcolor="#b7e0f3" align="right" class="alertboxTableT" style="width: 80px;">
                    人数&nbsp;
                </td>
                <td bgcolor="#b7e0f3" align="right" class="alertboxTableT" style="width: 90px;">
                    合同金额&nbsp;
                </td>
            </tr>
            <asp:Repeater ID="rptShouRu" runat="server">
                <ItemTemplate>
                    <tr bgcolor="F3F3F3">
                        <td align="right" style="height: 28px;">
                            <%#Container.ItemIndex + 1 %>&nbsp;
                        </td>
                        <td>
                            &nbsp;<%#Eval("OrderCode")%>
                        </td>
                        <td>
                            &nbsp;<%#Eval("KeHuName")%>
                        </td>
                        <td>
                            &nbsp;<%#Eval("XiaoShouYuanName")%>
                        </td>
                        <td>
                            &nbsp;<%#Eval("XiaDanTime","{0:yyyy-MM-dd HH:mm}")%>
                        </td>
                        <td align="right">
                            <%#Eval("ShiShouRenShu")%>&nbsp;
                        </td>
                        <td align="right">
                            <%#Eval("JinE","{0:F2}")%>&nbsp;
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
            <asp:PlaceHolder runat="server" ID="phEmptyShouRu" Visible="false">
                <tr>
                    <td colspan="11" style="height: 28px;">
                        暂无收入明细
                    </td>
                </tr>
            </asp:PlaceHolder>
            <asp:PlaceHolder runat="server" ID="phHeJiShuRu">
                <tr bgcolor="#e9f4f9" class="odd">
                    <td style="height: 28px" align="right" class="alertboxTableT" colspan="5">
                        合计：
                    </td>
                    <td style="text-align: right" class="alertboxTableT">
                        <asp:Literal runat="server" ID="ltrShiShouRenShu1"></asp:Literal>&nbsp;
                    </td>
                    <td style="text-align: right" class="alertboxTableT">
                        <asp:Literal runat="server" ID="ltrShouRuJinE1"></asp:Literal>&nbsp;
                    </td>
                </tr>
            </asp:PlaceHolder>
        </table>
        <div style="width: 98%; margin: 0px auto; margin-top: 5px; line-height: 25px;">
            支出明细</div>
        <table width="98%" cellspacing="0" cellpadding="0" bgcolor="#FFFFFF" align="center"
            id="liststyle_zhichu" style="margin: 0 auto">
            <tr style="background: url(/images/y-formykinfo.gif) repeat-x center top;" class="odd">
                <td height="28" bgcolor="#b7e0f3" align="right" class="alertboxTableT" style="width: 40px;">
                    序号&nbsp;
                </td>
                <td bgcolor="#b7e0f3" class="alertboxTableT" style="width:70px;">
                    &nbsp;类型
                </td>
                <td bgcolor="#b7e0f3" class="alertboxTableT">
                    &nbsp;供应商名称
                </td>
                <td bgcolor="#b7e0f3" class="alertboxTableT" style="width: 70px;">
                    &nbsp;计调员
                </td>
                <td bgcolor="#b7e0f3" class="alertboxTableT">
                    &nbsp;费用明细
                </td>
                <td bgcolor="#b7e0f3" class="alertboxTableT" style="width: 80px">
                    &nbsp;支付方式
                </td>
                <td bgcolor="#b7e0f3" align="right" class="alertboxTableT" style="width: 90px">
                    金额&nbsp;
                </td>
            </tr>
            <asp:Repeater ID="rptZhiChu" runat="server">
                <ItemTemplate>
                    <tr bgcolor="F3F3F3">
                        <td align="right" style="height: 28px;">
                            <%#Container.ItemIndex + 1 %>&nbsp;
                        </td>
                        <td>
                            &nbsp;<%#Eval("LeiXing")%>
                        </td>
                        <td>
                            &nbsp;<%#Eval("GysName")%>
                        </td>
                        <td>
                            &nbsp;<%#Eval("JiDiaoYuanName")%>
                        </td>
                        <td>
                            &nbsp;<%#Eval("FeiYongMingXi")%>
                        </td>
                        <td>
                            &nbsp;<%#Eval("ZhiFuFangShi")%>
                        </td>
                        <td align="right">
                            <%#Eval("JinE","{0:F2}")%>&nbsp;
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
            <asp:PlaceHolder runat="server" ID="phEmptyZhiChu" Visible="false">
                <tr>
                    <td colspan="11" style="height: 28px;">
                        暂无支出明细
                    </td>
                </tr>
            </asp:PlaceHolder>
            <asp:PlaceHolder runat="server" ID="phHeJiZhiChu">
                <tr bgcolor="#e9f4f9" class="odd">
                    <td style="height: 28px" align="right" class="alertboxTableT" colspan="6">
                        合计：
                    </td>
                    <td style="text-align: right" class="alertboxTableT">
                        <asp:Literal runat="server" ID="ltrZhiChuJinE1"></asp:Literal>&nbsp;
                    </td>
                </tr>
            </asp:PlaceHolder>
        </table>
        <div style="width: 98%; margin: 0px auto; margin-top: 5px; line-height: 25px; text-align: right;">
            <%=DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") %></div>
    </div>

    <script type="text/javascript">
        $(document).ready(function() {
            $("#i_a_dayin").click(function() {
                if (window.print == null) { alert("没有安装打印机"); return false; }
                var _$obj = $(this);
                _$obj.hide();
                window.print();
                setTimeout(function() { _$obj.show(); }, 1000);
            });
        });
    </script>

</asp:Content>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="YuSuanBiaoZhiChu.aspx.cs"
    Inherits="EyouSoft.Web.TongJi.YuSuanBiaoZhiChu" MasterPageFile="~/MasterPage/boxy.Master" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="PageBody">
    <div class="alertbox-outbox" id="alertbox" style="padding-top: 5px;">
        <table width="98%" cellspacing="0" cellpadding="0" bgcolor="#FFFFFF" align="center"
            id="liststyle" style="margin: 0 auto">
            <tr style="background: url(/images/y-formykinfo.gif) repeat-x center top;" class="odd">
                <td height="28" bgcolor="#b7e0f3" align="right" class="alertboxTableT" style="width: 40px;">
                    序号
                </td>
                <td bgcolor="#b7e0f3" class="alertboxTableT">
                    &nbsp;类型
                </td>
                <td bgcolor="#b7e0f3" class="alertboxTableT">
                    &nbsp;供应商名称
                </td>
                <td bgcolor="#b7e0f3" class="alertboxTableT">
                    &nbsp;计调员
                </td>
                <td bgcolor="#b7e0f3" class="alertboxTableT">
                    &nbsp;费用明细
                </td>
                <td bgcolor="#b7e0f3" class="alertboxTableT">
                    &nbsp;支付方式
                </td>
                <td bgcolor="#b7e0f3" align="right" class="alertboxTableT">
                    金额&nbsp;
                </td>
            </tr>
            <asp:Repeater ID="rpt" runat="server">
                <ItemTemplate>
                    <tr bgcolor="F3F3F3">
                        <td align="right" style="height: 28px;">
                            <%#Container.ItemIndex + 1 %>
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
            <asp:PlaceHolder runat="server" ID="phEmpty" Visible="false">
                <tr>
                    <td colspan="11" style="height: 28px;">
                        暂无明细信息
                    </td>
                </tr>
            </asp:PlaceHolder>
            <asp:PlaceHolder runat="server" ID="phHeJi">
                <tr bgcolor="#e9f4f9" class="odd">
                    <td style="height: 28px" align="right" class="alertboxTableT" colspan="6">
                        合计：
                    </td>
                    <td style="text-align: right" class="alertboxTableT">
                        <asp:Literal runat="server" ID="ltrJinE"></asp:Literal>&nbsp;
                    </td>
                </tr>
            </asp:PlaceHolder>
        </table>
    </div>

    <script type="text/javascript">
        $(document).ready(function() {
        });
    </script>

</asp:Content>

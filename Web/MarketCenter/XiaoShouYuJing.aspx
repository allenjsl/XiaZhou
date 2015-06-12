<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="XiaoShouYuJing.aspx.cs"
    Inherits="Web.MarketCenter.XiaoShouYuJing" MasterPageFile="~/MasterPage/Front.Master" %>

<%--销售中心-销售预警--%>
<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>
<%@ Register Src="~/UserControl/SellsSelect.ascx" TagName="SellsSelect" TagPrefix="uc1" %>
<%@ Register Src="../UserControl/CaiWuShaiXuan.ascx" TagName="CaiWuShaiXuan" TagPrefix="uc2" %>
<asp:Content ID="MainBodyContent" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="mainbox">
        <div class="tablehead" style="background: none #f6f6f6;">
            <ul class="fixed">
                <li><s class="orderformicon"></s><a href="KeHuYuJing.aspx?sl=<%=EyouSoft.Common.Utils.GetQueryStringValue("sl") %>"
                    hidefocus="true" class="ztorderform"><span>客户预警</span></a></li>
                <li><s class="orderformicon"></s><a href="XiaoShouYuJing.aspx?sl=<%=EyouSoft.Common.Utils.GetQueryStringValue("sl") %>" hidefocus="true" class="ztorderform de-ztorderform">
                    <span>销售预警</span></a></li>
                <li><s class="orderformicon"></s><a href="ChaoXianShenPi.aspx?sl=<%=EyouSoft.Common.Utils.GetQueryStringValue("sl") %>"
                    hidefocus="true" class="ztorderform"><span>超限审批</span></a></li>
            </ul>
            <div class="pages">
                <cc1:ExporPageInfoSelect ID="ExporPageInfoSelect2" runat="server" />
            </div>
        </div>
        <div class="searchbox fixed">
            <form method="get" action="XiaoShouYuJing.aspx">
            <input type="hidden" size="8" name="sl" value="<%=EyouSoft.Common.Utils.GetQueryStringValue("sl") %>" />
            <span class="searchT">
                <p>
                    部门：
                    <select name="sltDept" name="sltDept" class="inputselect">
                        <%=GetDepartLineForSelect(EyouSoft.Common.Utils.GetInt(EyouSoft.Common.Utils.GetQueryStringValue("sltDept")), SiteUserInfo.CompanyId)%>
                    </select>
                    销售员：
                    <uc1:SellsSelect ID="txtXiaoShouYuan" runat="server" SetTitle = "选择 销售员" SelectFrist="false" />                     
                    &nbsp;<uc2:CaiWuShaiXuan ID="CaiWuShaiXuan1" runat="server" ShaiXuanBiaoTi="拖欠金额：" />
                    <uc2:CaiWuShaiXuan ID="CaiWuShaiXuan2" runat="server" ShaiXuanBiaoTi="超限金额：" />                    
                    <input type="submit" id="btnSubmit" class="search-btn" /></p>
                    
                    <script type="text/javascript">
                        var obj1 = new wuc.caiWuShaiXuan(window["<%= CaiWuShaiXuan1.ClientUniqueID%>"]);
                        obj1.setOperator("<%=EyouSoft.Common.Utils.GetQueryStringValue(CaiWuShaiXuan1.ClientUniqueIDOperator) %>");
                        obj1.setOperatorNumber('<%=EyouSoft.Common.Utils.GetQueryStringValue(CaiWuShaiXuan1.ClientUniqueIDOperatorNumber) %>');
                        var obj2 = new wuc.caiWuShaiXuan(window['<%=CaiWuShaiXuan2.ClientID%>']);
                        obj2.setOperator('<%=EyouSoft.Common.Utils.GetQueryStringValue(CaiWuShaiXuan2.ClientUniqueIDOperator) %>');
                        obj2.setOperatorNumber('<%=EyouSoft.Common.Utils.GetQueryStringValue(CaiWuShaiXuan2.ClientUniqueIDOperatorNumber) %>');
                    </script>
            </span>
            </form>
        </div>
        <!--列表表格-->
        <div class="tablelist-box">
            <table width="100%" id="liststyle">
                <tr>
                    <th align="center" class="th-line">
                        销售员
                    </th>
                    <th align="right" class="th-line">
                        欠款额度
                    </th>
                    <th align="right" class="th-line">
                        已确认垫款
                    </th>
                    <th align="right" class="th-line">
                        团队预收
                    </th>
                    <th align="right" class="th-line">
                        支出
                    </th>
                    <th align="right" class="th-line">
                        拖欠金额
                    </th>
                    <th align="right" class="th-line">
                        超限金额
                    </th>
                    <th align="center" class="th-line">
                        超限时间
                    </th>
                </tr>
                <asp:Repeater runat="server" ID="rptList">
                    <ItemTemplate>
                        <tr>
                            <td align="center">
                                <%#Eval("SellerName")%>
                            </td>
                            <td align="right">
                                <strong>
                                    <%#UtilsCommons.GetMoneyString(Eval("AmountOwed"), ProviderToMoney)%></strong>
                            </td>
                            <td align="right">
                                <a href="###" class="toolbar_adv" data-sid="<%#Eval("SellerId")%>"><strong>
                                    <%#UtilsCommons.GetMoneyString(Eval("ConfirmAdvances"), ProviderToMoney)%></strong>
                                </a>
                            </td>
                            <td align="right">
                                <strong>
                                    <%#UtilsCommons.GetMoneyString(Eval("PreIncome"), ProviderToMoney)%></strong>
                            </td>
                            <td align="right">
                                <b class="fontblue">
                                    <%#UtilsCommons.GetMoneyString(Eval("SumPay"), ProviderToMoney)%></b>
                            </td>
                            <td align="right">
                                <b class="fontbsize12">
                                    <%#UtilsCommons.GetMoneyString(Eval("Arrear"), ProviderToMoney)%></b>
                            </td>
                            <td align="right">
                                <b class="fontgreen">
                                    <%#UtilsCommons.GetMoneyString(Eval("Transfinite"), ProviderToMoney)%></b>
                            </td>
                            <td align="center">
                                <%#Eval("TransfiniteTime")%>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
        </div>
        <!--列表结束-->
        <div class="tablehead" style="border-top: 0 none;">
            <div class="pages">
                <cc1:ExporPageInfoSelect ID="ExporPageInfoSelect1" runat="server" />
            </div>
        </div>
    </div>

    <script type="text/javascript">
        var XiaoShouYuJing = {
            sl: '<%=Request.QueryString["sl"] %>',

            ShowBoxy: function(data) {/*显示弹窗*/
                Boxy.iframeDialog({
                    iframeUrl: data.url,
                    title: data.title,
                    modal: true,
                    width: data.width,
                    height: data.height
                });
            },
            DataBoxy: function() {/*弹窗参数*/
                return {
                    url: "",
                    title: "",
                    width: "",
                    height: ""
                }
            },
            Advances: function(obj) {/*垫款查看弹窗*/
                var data = this.DataBoxy();
                data.title += "销售预警-垫款查看";
                data.url = "/MarketCenter/AdvancesList.aspx?" + $.param({ sl: Boxy.queryString("sl"), sId: $(obj).attr("data-sid") });
                data.width = "800px";
                data.height = "452px";
                this.ShowBoxy(data);
                return false;
            },
            BindBtn: function() {
                var that = this;
                $(".toolbar_adv").click(function() {
                    that.Advances(this);
                    return false;
                })
            },
            PageInit: function() {/*页面初始化*/
                /*绑定功能按钮*/
                this.BindBtn();
            }
        }

        $(function() {
            //初始化页面
            XiaoShouYuJing.PageInit();
        })
    </script>

</asp:Content>

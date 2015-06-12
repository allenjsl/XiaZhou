<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Front.Master" AutoEventWireup="true"
    CodeBehind="SellList.aspx.cs" Inherits="Web.FinanceManage.Arrearage.SellList" %>

<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>
<%@ Register Src="/UserControl/SellsSelect.ascx" TagName="SellsSelect" TagPrefix="uc1" %>
<%@ Register Src="/UserControl/CaiWuShaiXuan.ascx" TagName="CaiWuShaiXuan" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="mainbox">
        <div class="tablehead" style="background: none #f6f6f6;">
            <ul class="fixed">
                <li><s class="orderformicon"></s><a href="ClientList.aspx?sl=<%=Request.QueryString["sl"] %>"
                    hidefocus="true" class="ztorderform"><span>客户预警</span></a></li>
                <li><s class="orderformicon"></s><a href="javascript:void(0);" hidefocus="true" class="ztorderform de-ztorderform">
                    <span>销售预警</span></a></li>
                <li><s class="orderformicon"></s><a href="OverrunList.aspx?sl=<%=Request.QueryString["sl"] %>"
                    hidefocus="true" class="ztorderform"><span>超限审批</span></a></li>
            </ul>
        </div>
        <form id="SelectFrom" action="SellList.aspx" method="get">
        <div class="searchbox fixed">
            <span class="searchT">
                <p>
                    部门：
                    <select name="sel_department" class="inputselect">
                        <option value="-1">-请选择-</option>
                        <%=DepartmentOptionStr %>
                    </select>
                    销售员：
                    <uc1:SellsSelect ID="txt_SellsSelect" selectfrist="false" runat="server" />
                    拖欠金额：
                    <uc1:CaiWuShaiXuan ID="CaiWuShaiXuan1" runat="server" />
                    超限金额：
                    <uc1:CaiWuShaiXuan ID="CaiWuShaiXuan2" runat="server" />
                    <button type="submit" class="search-btn">
                        搜索</button></p>
            </span>
        </div>
        <input type="hidden" name="sl" value="<%=Request.QueryString["sl"] %>" />
        </form>
        <div class="tablehead" id="tablehead">
            <div class="pages">
                <cc1:ExporPageInfoSelect ID="ExporPageInfoSelect2" runat="server" />
            </div>
        </div>
        <!--列表表格-->
        <div class="tablelist-box">
            <table width="100%" id="liststyle">
                <tr>
                    <th class="thinputbg">
                        <input type="checkbox" name="checkbox" />
                    </th>
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
                <asp:Repeater ID="rpt_list" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td align="center">
                                <input type="checkbox" name="checkbox" />
                            </td>
                            <td align="center">
                                <%#Eval("SellerName")%>
                            </td>
                            <td align="right">
                                <strong>
                                    <%#EyouSoft.Common.UtilsCommons.GetMoneyString(Eval("AmountOwed")??0,ProviderToMoney)%></strong>
                            </td>
                            <td align="right">
                                <strong>
                                    <%#EyouSoft.Common.UtilsCommons.GetMoneyString(Eval("ConfirmAdvances") ?? 0, ProviderToMoney)%></strong>
                            </td>
                            <td align="right">
                                <strong>
                                    <%#EyouSoft.Common.UtilsCommons.GetMoneyString(Eval("PreIncome") ?? 0, ProviderToMoney)%></strong>
                            </td>
                            <td align="right">
                                <b class="fontblue">
                                    <%#EyouSoft.Common.UtilsCommons.GetMoneyString(Eval("SumPay") ?? 0, ProviderToMoney)%></b>
                            </td>
                            <td align="right">
                                <b class="fontbsize12">
                                    <%#EyouSoft.Common.UtilsCommons.GetMoneyString(Eval("Arrear") ?? 0, ProviderToMoney)%></b>
                            </td>
                            <td align="right">
                                <b class="fontgreen">
                                    <%#EyouSoft.Common.UtilsCommons.GetMoneyString(Eval("Transfinite")??0, ProviderToMoney)%></b>
                            </td>
                            <td align="center">
                                <%#EyouSoft.Common.UtilsCommons.GetDateString(Eval("TransfiniteTime"), ProviderToDate)%>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
                <asp:Panel ID="pan_Msg" runat="server">
                    <tr align="center">
                        <td colspan="9">
                            暂无数据!
                        </td>
                    </tr>
                </asp:Panel>
            </table>
        </div>
        <!--列表结束-->
        <div class="tablehead" style="border-top: 0 none;">

            <script type="text/javascript">
                document.write(document.getElementById("tablehead").innerHTML);
            </script>

        </div>
    </div>

    <script type="text/javascript">
        var ClientList = {
            ShowBoxy: function(data) {/*显示弹窗*/
                Boxy.iframeDialog({
                    iframeUrl: data.url,
                    title: data.title,
                    modal: true,
                    width: data.width,
                    height: data.height
                });
            },
            PageInit: function() {
                tableToolbar.init({});
                //已付金额
                var paidwuc = new wuc.caiWuShaiXuan(window['<%=CaiWuShaiXuan1.ClientUniqueID %>']);
                paidwuc.setOperator('<%=EyouSoft.Common.Utils.GetQueryStringValue(CaiWuShaiXuan1.ClientUniqueIDOperator)%>')
                paidwuc.setOperatorNumber('<%=EyouSoft.Common.Utils.GetQueryStringValue(CaiWuShaiXuan1.ClientUniqueIDOperatorNumber)%>')
                //未付金额
                paidwuc = new wuc.caiWuShaiXuan(window['<%=CaiWuShaiXuan2.ClientUniqueID  %>']);
                paidwuc.setOperator('<%=EyouSoft.Common.Utils.GetQueryStringValue(CaiWuShaiXuan2.ClientUniqueIDOperator)%>')
                paidwuc.setOperatorNumber('<%=EyouSoft.Common.Utils.GetQueryStringValue(CaiWuShaiXuan2.ClientUniqueIDOperatorNumber)%>')
            }
        }
        $(function() {
            ClientList.PageInit();
        })
        
    </script>

</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Front.Master" AutoEventWireup="true"
    CodeBehind="ClientList.aspx.cs" Inherits="Web.FinanceManage.Arrearage.ClientList" %>

<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>
<%@ Register Src="/UserControl/SellsSelect.ascx" TagName="SellsSelect" TagPrefix="uc1" %>
<%@ Register Src="/UserControl/CaiWuShaiXuan.ascx" TagName="CaiWuShaiXuan" TagPrefix="uc1" %>
<%@ Register Src="/UserControl/CustomerUnitSelect.ascx" TagName="CustomerUnitSelect"
    TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="mainbox">
        <div class="tablehead" style="background: none #f6f6f6;">
            <ul class="fixed">
                <li><s class="orderformicon"></s><a href="javascript:void(0);" hidefocus="true" class="ztorderform de-ztorderform">
                    <span>客户预警</span></a></li>
                <li><s class="orderformicon"></s><a href="SellList.aspx?sl=<%=Request.QueryString["sl"] %>"
                    hidefocus="true" class="ztorderform "><span>销售预警</span></a></li>
                <li><s class="orderformicon"></s><a href="OverrunList.aspx?sl=<%=Request.QueryString["sl"] %>"
                    hidefocus="true" class="ztorderform"><span>超限审批</span></a></li>
            </ul>
        </div>
        <div class="searchbox fixed">
            <form id="SelectFrom" action="ClientList.aspx" method="get">
            <span class="searchT">
                <p>
                    客户单位：
                    <uc2:CustomerUnitSelect ID="CustomerUnitSelect1" selectfrist="false" runat="server" />
                    销售员：
                    <uc1:SellsSelect ID="txt_SellsSelect" runat="server" SelectFrist="false" />
                    拖欠金额：
                    <uc1:CaiWuShaiXuan ID="CaiWuShaiXuan1" runat="server" />
                    超限金额：
                    <uc1:CaiWuShaiXuan ID="CaiWuShaiXuan2" runat="server" />
                    <button type="submit" class="search-btn">
                        搜索</button></p>
            </span>
            <input type="hidden" name="sl" value="<%=Request.QueryString["sl"] %>" />
            </form>
        </div>
        <!--列表表格-->
        <div class="tablelist-box">
            <table width="100%" id="liststyle">
                <tr>
                    <th class="thinputbg">
                        <input type="checkbox" name="checkbox" />
                    </th>
                    <th align="center" class="th-line">
                        客户单位
                    </th>
                    <th align="center" class="th-line">
                        销售员
                    </th>
                    <th align="right" class="th-line">
                        欠款额度
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
                    <th align="center" class="th-line">
                        单团账龄期限
                    </th>
                    <th align="center" class="th-line">
                        最长超时天数
                    </th>
                    <th align="center" class="th-line">
                        超时团数
                    </th>
                </tr>
                <asp:Repeater ID="rpt_list" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td align="center">
                                <input type="checkbox" name="checkbox" />
                            </td>
                            <td align="left">
                                <a href="javascript:void(0);" data-class="a_popo">
                                    <%#Eval("Customer")%></a><span style="display: none"><b><%#Eval("Customer")%></b><br />
                                        <%#LinkMan(Eval("Contact"))%></span>
                            </td>
                            <td align="center">
                                <%#Eval("SellerName")%>
                            </td>
                            <td align="right">
                                <b class="fontbsize12">
                                    <%#EyouSoft.Common.UtilsCommons.GetMoneyString(Eval("AmountOwed")??0,ProviderToMoney)%></b>
                            </td>
                            <td align="right">
                                <a href="javascript:void(0);" data-crmid="<%#Eval("CrmId") %>" data-crmname="<%#Eval("Customer") %>">
                                    <strong>
                                        <%#EyouSoft.Common.UtilsCommons.GetMoneyString(Eval("Arrear") ?? 0, ProviderToMoney)%></strong></a>
                            </td>
                            <td align="right">
                                <b class="fontgreen">
                                    <%#EyouSoft.Common.UtilsCommons.GetMoneyString(Eval("Transfinite")??0, ProviderToMoney)%></b>
                            </td>
                            <td align="center">
                                <%#EyouSoft.Common.UtilsCommons.GetDateString(Eval("TransfiniteTime"), ProviderToDate)%>
                            </td>
                            <td align="right">
                                <%#Eval("Deadline")%>
                            </td>
                            <td align="right">
                                <%#EyouSoft.Common.Utils.GetInt(Eval("DeadDay")!=null?Eval("DeadDay").ToString():"0")%>
                            </td>
                            <td align="center">
                                <%#Eval("TourCount")%>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
                <asp:Panel ID="pan_Msg" runat="server">
                    <tr align="center">
                        <td colspan="10">
                            暂无数据!
                        </td>
                    </tr>
                </asp:Panel>
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
            Bt: function(obj) {/*泡泡提示*/
                $(".bt-wrapper").html("");
                $(obj).each(function() {
                    $(this).bt({
                        contentSelector: function() {
                            return $(this).next("span").html();
                        },
                        positions: ['left', 'right', 'bottom'],
                        fill: '#FFF2B5',
                        strokeStyle: '#D59228',
                        noShadowOpts: { strokeStyle: "#D59228" },
                        spikeLength: 10,
                        spikeGirth: 15,
                        width: 200,
                        overlap: 0,
                        centerPointY: 1,
                        cornerRadius: 4,
                        shadow: true,
                        shadowColor: 'rgba(0,0,0,.5)',
                        cssStyles: { color: '#00387E', 'line-height': '180%' }
                    });
                });

            },
            Arrear: function(obj) {/*拖欠金额*/
                var obj = $(obj);
                this.ShowBoxy({
                    url: "/FinanceManage/Arrearage/ArrearList.aspx?" + $.param({
                        crmId: obj.attr("data-crmid"),
                        crmName: obj.attr("data-crmname")
                    }),
                    title: "财务管理-客户预警-拖欠金额",
                    width: "890px",
                    height: "500px"
                })
            },
            PageInit: function() {
                tableToolbar.init({});
                var that = this;
                that.Bt($("#liststyle a[data-class='a_popo']"))
                $("#liststyle a[data-crmid]").click(function() {
                    that.Arrear(this);
                    return false;
                })
                //已付金额
                var paidwuc = new wuc.caiWuShaiXuan(window['<%=CaiWuShaiXuan1.ClientUniqueID %>']);
                paidwuc.setOperator('<%=EyouSoft.Common.Utils.GetQueryStringValue(CaiWuShaiXuan1.ClientUniqueIDOperator)%>')
                paidwuc.setOperatorNumber('<%=EyouSoft.Common.Utils.GetQueryStringValue(CaiWuShaiXuan1.ClientUniqueIDOperatorNumber)%>')
                //未付金额
                paidwuc = new wuc.caiWuShaiXuan(window['<%=CaiWuShaiXuan2.ClientUniqueID  %>']);
                paidwuc.setOperator('<%=EyouSoft.Common.Utils.GetQueryStringValue(CaiWuShaiXuan2.ClientUniqueIDOperator)%>')
                paidwuc.setOperatorNumber('<%=EyouSoft.Common.Utils.GetQueryStringValue(CaiWuShaiXuan2.ClientUniqueIDOperatorNumber)%>')
                $("#div_clone").append($(".pages").clone(true));
            }
        }
        $(function() {
            ClientList.PageInit();
        })
        
    </script>

</asp:Content>

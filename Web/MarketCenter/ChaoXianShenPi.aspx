<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChaoXianShenPi.aspx.cs"
    Inherits="Web.MarketCenter.ChaoXianShenPi" MasterPageFile="~/MasterPage/Front.Master" %>

<%--销售中心-超限审批--%>
<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>
<%@ Register Src="/UserControl/SellsSelect.ascx" TagName="SellsSelect" TagPrefix="uc1" %>
<%@ Register Src="/UserControl/CustomerUnitSelect.ascx" TagName="CustomerUnitSelect"
    TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script src="/Js/datepicker/WdatePicker.js" type="text/javascript"></script>

</asp:Content>
<asp:Content ID="MainBodyContent" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="mainbox">
        <div class="tablehead" style="background: none #f6f6f6;">
            <ul class="fixed">
                <li><s class="orderformicon"></s><a href="KeHuYuJing.aspx?sl=<%=EyouSoft.Common.Utils.GetQueryStringValue("sl") %>"
                    hidefocus="true" class="ztorderform"><span>客户预警</span></a></li>
                <li><s class="orderformicon"></s><a href="XiaoShouYuJing.aspx?sl=<%=EyouSoft.Common.Utils.GetQueryStringValue("sl") %>"
                    hidefocus="true" class="ztorderform"><span>销售预警</span></a></li>
                <li><s class="orderformicon"></s><a href="ChaoXianShenPi.aspx?sl=<%=EyouSoft.Common.Utils.GetQueryStringValue("sl") %>"
                    hidefocus="true" class="ztorderform de-ztorderform"><span>超限审批</span></a></li>
            </ul>
        </div>
        <form id="SelectFrom" action="ChaoXianShenPi.aspx" method="get">
        <div class="searchbox fixed">
            <span class="searchT">
                <p>
                    线路名称：<input type="text" name="txt_lineName" class="inputtext formsize120" value="<%=Request.QueryString["txt_lineName"]  %>" />
                    <uc2:CustomerUnitSelect ID="CustomerUnitSelect1" runat="server" />
                    申请人：<uc1:SellsSelect ID="SellsSelect1" runat="server" />
                    申请时间：
                    <input type="text" name="ApplyTimeS" class="inputtext formsize80" onclick="WdatePicker()"
                        value="<%=Request.QueryString["ApplyTimeS"] %>" />
                    -
                    <input type="text" name="ApplyTimeE" class="inputtext formsize80" onclick="WdatePicker()"
                        value="<%=Request.QueryString["ApplyTimeE"] %>" />
                    <input type="submit" class="search-btn" /></p>
            </span>
        </div>
        <input type="hidden" name="sl" value="<%=Request.QueryString["sl"] %>" />
        </form>
        <!--列表表格-->
        <div class="tablelist-box">
            <table width="100%" id="liststyle">
                <tr>
                    <th class="thinputbg">
                        <input type="checkbox" name="checkbox" id="checkbox" />
                    </th>
                    <th align="center" class="th-line">
                        线路名称
                    </th>
                    <th align="center" class="th-line">
                        客户单位
                    </th>
                    <th align="center" class="th-line">
                        申请人
                    </th>
                    <th align="right" class="th-line">
                        申请金额
                    </th>
                    <th align="center" class="th-line">
                        操作
                    </th>
                </tr>
                <asp:Repeater ID="rpt_list" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td align="center">
                                <input type="checkbox" name="checkbox" id="checkbox" />
                            </td>
                            <td align="center">
                                <%#Eval("RouteName")%>
                            </td>
                            <td align="center">
                                <%#Eval("Crm")%>
                            </td>
                            <td align="center">
                                <%#Eval("Applier")%>
                            </td>
                            <td align="right">
                                <b class="fontbsize12">
                                    <%#EyouSoft.Common.UtilsCommons.GetMoneyString(Eval("DisburseAmount") ?? 0, ProviderToMoney)%></b>
                            </td>
                            <td align="center">
                                <%--<a href="javascript:void(0);" data-itemtype="<%#(int)Eval("ItemType") %>" data-itemid="<%#Eval("ItemId") %>"
                                    disburseid="<%#Eval("DisburseId") %>" data-crmid="<%#Eval("CrmId") %>" data-approverid="<%#Eval("ApproverId") %>"
                                    data-approvername="<%#Eval("Applier") %>" data-disburseamount="<%#Eval("DisburseAmount") %>"
                                    data-applytime="<%#Eval("ApplyTime") %>" data-remark="<%#Eval("Remark") %>">审批</a>--%><%#Eval("IsApprove")%>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
                <asp:Panel ID="pan_msg" runat="server">
                    <tr align="center">
                        <td colspan="6">
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
        var ChaoXianShenPi = {
            ShowBoxy: function(data) {/*显示弹窗*/
                Boxy.iframeDialog({
                    iframeUrl: data.url,
                    title: data.title,
                    modal: true,
                    width: data.width,
                    height: data.height
                });
            },
            ExamineA: function(obj) {/*审批*/
                var obj = $(obj);
                this.ShowBoxy({
                    url: "/FinanceManage/Arrearage/ExamineA.aspx?" + $.param({
                        disburseid: obj.attr("data-disburseid"), //垫付id
                        itemtype: obj.attr("data-itemtype"), //0：报价成功/1：成团/2：报名
                        itemid: obj.attr("data-itemid"), //报价编号/团队编号/订单编号
                        crmId: obj.attr("data-crmid"), //客户单位ID
                        approverId: obj.attr("data-approverid"), //销售员id=申请人id
                        approvername: obj.attr("data-approvername"), //销售员name=申请人name
                        disburseamount: obj.attr("data-disburseamount"), //垫付金额
                        applytime: obj.attr("data-applytime"), //申请时间
                        remark: obj.attr("data-remark")//备注
                    }),
                    title: "超限审批-" + EnglishToChanges.Ping("ExamineA"),
                    width: "750px",
                    height: "500px"
                })
            },
            Bt: function(obj) {/*泡泡提示*/
                var obj = $(obj);
                $(".bt-wrapper").html("");
                //公司名称
                var firmName = obj.text();
                //联系人姓名
                var linkName = obj.attr("linkName");
                //联系电话
                var linkTel = obj.attr("linkTel");
                //拼接提示文本
                var cont = "<b>" + firmName + "</b><br/>";
                cont += "联系人：" + linkName + "<br/>";
                cont += "联系方式：" + linkTel;
                obj.bt({
                    contentSelector: function() {
                        return cont;
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
            },
            PageInit: function() {/*页面初始化*/
                var that = this;
                $("a[data-itemid][data-itemtype][data-crmid][data-approverid]").click(function() {
                    that.ExamineA(this);
                    return false;
                })

            }
        }
        $(function() {
            ChaoXianShenPi.PageInit();
        })



       
    </script>

</asp:Content>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TeamOperaterList.aspx.cs"
    Inherits="Web.OperaterCenter.TourOperaterList" MasterPageFile="~/MasterPage/Front.Master" %>

<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Src="~/UserControl/SellsSelect.ascx" TagName="Seller" TagPrefix="Uc1" %>
<%@ Register Src="~/UserControl/CustomerUnitSelect.ascx" TagName="Customer" TagPrefix="uc2" %>
<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>
<asp:Content ContentPlaceHolderID="head" ID="Content2" runat="server">
</asp:Content>
<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" ID="Content1" runat="server">

    <script src="/Js/datepicker/WdatePicker.js" type="text/javascript"></script>

    <div class="mainbox">
        <form id="searchform" action="/OperaterCenter/TeamOperaterList.aspx" method="get">
        <div class="searchbox fixed">
            <span class="searchT">
                <p>
                    <input type="hidden" name="sl" id="sl" value="<%=SL %>" />
                    �� &nbsp;&nbsp;&nbsp;�ţ�
                    <input type="text" class="inputtext formsize120" name="txtTourCode" value="<%=EyouSoft.Common.Utils.GetQueryStringValue("txtTourCode") %>" />
                    �������ڣ�
                    <input type="text" class="inputtext" style="width: 63px; padding-left: 2px;" id="txtStartTime"
                        name="txtStartTime" onfocus="WdatePicker();" value="<%=EyouSoft.Common.Utils.GetQueryStringValue("txtStartTime") %>" />
                    -
                    <input type="text" class="inputtext" style="width: 63px; padding-left: 2px;" id="txtEndTime"
                        name="txtEndTime" onfocus="WdatePicker();" value="<%=EyouSoft.Common.Utils.GetQueryStringValue("txtEndTime") %>" />
                    �Ŷ�״̬��<%=TourStatusHtml.ToString()%>
                    <br />
                    <br />
                    ��Դ��λ��
                    <uc2:Customer ID="Customer1" runat="server" BoxyTitle="��Դ��λ" SelectFrist="false" />
                    ����Ա��
                    <Uc1:Seller ID="Seller1" runat="server" CompanyID="<%=this.SiteUserInfo.CompanyId %>" SelectFrist="false" />
                    �Ƶ�Ա��
                    <Uc1:Seller ID="seller2" runat="server" CompanyID="<%=this.SiteUserInfo.CompanyId %>" SelectFrist="false"  SetTitle="�Ƶ�Ա"/>
                    <input type="submit" id="search" class="search-btn" value="" /></p>
            </span>
        </div>
        </form>
        <div class="tablehead">
            <div class="pages">
                <cc1:ExporPageInfoSelect ID="ExporPageInfoSelect1" runat="server" />
            </div>
        </div>
        <div class="tablelist-box">
            <table width="100%" id="liststyle">
                <tbody>
                    <tr>
                        <th class="thinputbg">
                            <input type="checkbox" name="checkbox" id="checkbox" />
                        </th>
                        <th align="center" class="th-line">
                            �ź�
                        </th>
                        <th align="left" class="th-line">
                            ��·����
                        </th>
                        <th align="center" class="th-line">
                            ����ʱ��
                        </th>
                        <th align="center" class="th-line">
                            ����
                        </th>
                        <th align="center" class="th-line">
                            �ͻ���λ
                        </th>
                        <th align="center" class="th-line">
                            ����Ա
                        </th>
                        <th align="center" class="th-line">
                            �Ƶ�Ա
                        </th>
                        <th align="center" class="th-line">
                            ����
                        </th>
                        <th align="center" class="th-line">
                            �鿴����
                        </th>
                        <th align="center" class="th-line">
                            �鿴�Ƶ�
                        </th>
                        <th align="center" class="th-line">
                            ״̬
                        </th>
                        <th align="center" class="th-line">
                            ����
                        </th>
                    </tr>
                    <asp:Repeater ID="TeamOperaterList" runat="server">
                        <ItemTemplate>
                            <tr <%# Container.ItemIndex%2!=0? "class=\"odd\"":"" %>>
                                <td align="center">
                                    <input type="checkbox" name="checkbox" id="checkbox" value="<%# Eval("TourId") %>" />
                                </td>
                                <td align="center">
                                    <a href='<%# teamPrintUrl %>?tourid=<%# Eval("TourId") %>' target="_blank" style="text-decoration:none" data-class="TravelagencyShow">
                                        <%# Eval("TourCode")%>
                                    </a>
                                    <%#GetTourPlanIschange(Convert.ToBoolean(Eval("IsChange")), Convert.ToBoolean(Eval("IsSure")),Eval("TourId").ToString())%>
                                    <div style="display: none">
                                        <%# GetOperaterInfo(Eval("TourId").ToString())%>
                                    </div>
                                </td>
                                <td align="left">
                                    <a href='<%# teamPrintUrl %>?tourid=<%# Eval("TourId") %>' target="_blank">
                                        <%# Eval("RouteName") %>
                                    </a>
                                </td>
                                <td align="center">
                                    <%# EyouSoft.Common.UtilsCommons.GetDateString(Convert.ToDateTime(Eval("LDate")), ProviderToDate)%>
                                </td>
                                <td align="center">
                                    <%# Eval("TourDays")%>
                                </td>
                                <td align="center">
                                    <asp:PlaceHolder runat="server" Visible='<%# GetTourType((EyouSoft.Model.EnumType.TourStructure.TourType)Enum.Parse(typeof(EyouSoft.Model.EnumType.TourStructure.TourType),Eval("TourType").ToString())) %>'>
                                        <a href="javascript:void(0);" data-class="customershow">
                                            <%#GetCustomerInfo(Eval("CompanyInfo"), "single")%>
                                        </a>
                                        <div style="display: none">
                                            <%# GetCustomerInfo(Eval("CompanyInfo"), "info")%>
                                        </div>
                                    </asp:PlaceHolder>
                                </td>
                                <td align="center">
                                    <%# GetSellerInfo(Eval("SaleInfo"))%>
                                </td>
                                <td align="center">
                                    <%# GetOperaList(Eval("TourPlaner"))%>
                                </td>
                                <td align="center">
                                    <b class="fontblue">
                                        <%# Eval("Adults")%></b><sup>+<b class="fontred"><%# Eval("Childs")%></b></sup>
                                </td>
                                <td align="center">
                                    <a href='/SellCenter/CustomerPlan/OrderPaid.aspx?tourID=<%# Eval("TourId") %>&sl=<%#EyouSoft.Common.Utils.GetQueryStringValue("sl")  %>&plan=1'>��<b class="fontred"><%# Eval("OrderNum")%></b>��</a>
                                </td>
                                <td align="center" data-class="GetJiDiaoIcon" data-tourid="<%# Eval("TourId") %>">
                                    <%# EyouSoft.Common.UtilsCommons.GetJiDiaoIcon((EyouSoft.Model.TourStructure.MTourPlanStatus)Eval("TourPlanStatus"))%>
                                </td>
                                <td align="center" <%# (EyouSoft.Model.EnumType.TourStructure.TourStatus)Eval("TourStatus") == EyouSoft.Model.EnumType.TourStructure.TourStatus.���� ? "class='fontgray'" : ""%>>
                                    <%# Eval("TourStatus").ToString()%>
                                </td>
                                <td align="center">
                                    <%# GetOperate((EyouSoft.Model.EnumType.TourStructure.TourStatus)Enum.Parse(typeof(EyouSoft.Model.EnumType.TourStructure.TourStatus), Eval("TourStatus").ToString()), Eval("TourId").ToString(), (EyouSoft.Model.EnumType.TourStructure.TourType)Enum.Parse(typeof(EyouSoft.Model.EnumType.TourStructure.TourType),Eval("TourType").ToString()), (EyouSoft.Model.EnumType.TourStructure.TourQuoteType)Enum.Parse(typeof(EyouSoft.Model.EnumType.TourStructure.TourQuoteType), Eval("OutQuoteType").ToString()))%>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>
        </div>
        <div class="tablehead">
            <div class="pages">
                <asp:Label ID="lab_Text" runat="server"></asp:Label>
                <cc1:ExporPageInfoSelect ID="ExporPageInfoSelect2" runat="server" />
            </div>
        </div>
    </div>

    <script type="text/javascript">
        var TeamOperaterPage = {
            //�Ƶ��������
            _OperaterReceive: function(comID, tourId) {
                $.newAjax({
                    type: "POST",
                    url: '/Ashx/ReceiveJob.ashx?sl=<%=Utils.GetQueryStringValue("sl") %>&type=receive&com=' + comID + "&Operator=<%=this.SiteUserInfo.Username %>" + "&OperatorID=<%=this.SiteUserInfo.UserId %>" + "&OperatDepID=<%=this.SiteUserInfo.DeptId  %>&tourId=" + tourId,
                    async: false,
                    dataType: "json",
                    success: function(ret) {
                        if (ret.result) {
                            tableToolbar._showMsg(ret.msg, function() {
                                window.location.href = window.location.href;
                            });
                            return false;
                        }
                    },
                    error: function() {
                        tableToolbar._showMsg(tableToolbar.errorMsg);
                    }
                });
            },
            //click�¼�
            _BindBtn: function() {
                $("#liststyle").find("[data-class='receiveOp']").unbind("click").click(function() {
                    var ac = $(this).attr("data-teamPlaner");
                    if (ac.toUpperCase() == "FALSE") {
                        tableToolbar._showMsg("���Ǹüƻ��ļƵ�Ա,û�н��������Ȩ��!");
                    } else {
                        var companyID = "<%=this.SiteUserInfo.CompanyId %>";
                        var tourId = $(this).attr("data-tourid");
                        TeamOperaterPage._OperaterReceive(companyID, tourId);
                    }
                    return false;
                });

                //������������ʾ
                $("#liststyle").find("[data-class='TravelagencyShow']").bt({
                    contentSelector: function() {
                        return $(this).siblings("div").html();
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

                //�ͻ���λ����
                $("#liststyle").find("[data-class='customershow']").bt({
                    contentSelector: function() {
                        return $(this).siblings("div").html();
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

                //��Ӧ������
                BtFun.InitBindBt("GetJiDiaoIcon");
            },
            _DataInit: function() {
                TeamOperaterPage._BindBtn();
            }
        };


        $(document).ready(function() {
            //��ʼ��
            TeamOperaterPage._DataInit();
            tableToolbar.init({
                tableContainerSelector: "#liststyle"
            });
        });
        
    </script>

</asp:Content>

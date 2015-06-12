<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Front.Master" AutoEventWireup="true"
    CodeBehind="OrderList.aspx.cs" Inherits="Web.SellCenter.Order.OrderList" %>

<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>
<%@ Register Src="../../UserControl/SellsSelect.ascx" TagName="SellsSelect" TagPrefix="uc1" %>
<%@ Register Src="/UserControl/DuoXuanOrderStatus.ascx" TagName="DuoXuanOrderStatus"
    TagPrefix="uc1" %>
<%@ Register Src="~/UserControl/CustomerUnitSelect.ascx" TagName="CustomerUnitSelect"
    TagPrefix="uc1" %>
<%@ Register Src="~/UserControl/SelectSection.ascx" TagName="SelectSection" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="mainbox">
        <form id="form1" method="get">
        <div class="searchbox fixed">
            <span class="searchT">
                <p>
                    订单号：<input type="text" class="inputtext formsize120" id="txtOrderCode" name="txtOrderCode"
                        value="<%=EyouSoft.Common.Utils.GetQueryStringValue("txtOrderCode")%>" />
                    线路名称：<input type="text" class="inputtext formsize120" id="txtRouteName" name="txtRouteName"
                        value="<%=EyouSoft.Common.Utils.GetQueryStringValue("txtRouteName")%>" />
                    下单时间：<input type="text" onfocus="WdatePicker()" class="inputtext formsize80" id="txtOrderIssueBeginTime"
                        name="txtOrderIssueBeginTime" value="<%=EyouSoft.Common.Utils.GetQueryStringValue("txtOrderIssueBeginTime")%>" />
                    至
                    <input type="text" onfocus="WdatePicker()" class="inputtext formsize80" id="txtOrderIssueEndTime"
                        name="txtOrderIssueEndTime" value="<%=EyouSoft.Common.Utils.GetQueryStringValue("txtOrderIssueEndTime")%>" />
                    出团时间：<input type="text" onfocus="WdatePicker()" class="inputtext formsize80" id="txtLeaveBeginTime"
                        name="txtLeaveBeginTime" value="<%=EyouSoft.Common.Utils.GetQueryStringValue("txtLeaveBeginTime")%>" />
                    至
                    <input type="text" onfocus="WdatePicker()" class="inputtext formsize80" id="txtLeaveEndTime"
                        name="txtLeaveEndTime" value="<%=EyouSoft.Common.Utils.GetQueryStringValue("txtLeaveEndTime")%>" />
                    <br />
                    团 号：<input type="text" class="inputtext formsize120" id="txtTourCode" name="txtTourCode"
                        value="<%=EyouSoft.Common.Utils.GetQueryStringValue("txtTourCode")%>" />
                    销售员：<uc1:SellsSelect ID="SellsSelect1" runat="server" SetTitle="销售员" SelectFrist="false" />
                    下单人：<uc1:sellsselect id="txtXiaDanRen" runat="server" settitle="下单人" selectfrist="false" />
                    订单状态：<uc1:DuoXuanOrderStatus runat="server" id="txtOrderStatus"></uc1:DuoXuanOrderStatus>
                    <br />
                    客户单位：<uc1:CustomerUnitSelect ID="txtKeHuDanWei" runat="server" BoxyTitle="客户单位" SelectFrist="false" />
                    订单销售员部门：<uc1:SelectSection ID="txtXiaoShouYuanDept" runat="server" ReadOnly="true" SetTitle="部门"
                        SModel="2" />
                    <button type="submit" class="search-btn">
                        搜索</button>
                </p>
                <input type="hidden" name="sl" value="<%=EyouSoft.Common.Utils.GetQueryStringValue("sl") %>" />
                <input type="hidden" name="OrderTypeBySearch" value="<%=EyouSoft.Common.Utils.GetQueryStringValue("OrderTypeBySearch") %>" />
            </span>
        </div>
        </form>
        
        <div style="background: none #f6f6f6;" class="tablehead i_lanmu">            
            <ul class="fixed">
                <li><s class="orderformicon"></s><a id="i_a_lm_0" href="javascript:void(0);" data-href="OrderList.aspx?OrderTypeBySearch=<%=(int)EyouSoft.Model.EnumType.TourStructure.OrderTypeBySearch.全部订单%>&sl=<%=(int)EyouSoft.Model.EnumType.PrivsStructure.Menu2.同业分销_订单中心 %>"
                    hidefocus="true" class="ztorderform">
                    <span>全部订单</span> </a></li>
                <li><s class="orderformicon"></s><a id="i_a_lm_1" href="javascript:void(0);" data-href="OrderList.aspx?OrderTypeBySearch=<%=(int)EyouSoft.Model.EnumType.TourStructure.OrderTypeBySearch.我销售的订单%>&sl=<%=(int)EyouSoft.Model.EnumType.PrivsStructure.Menu2.同业分销_订单中心 %>"
                    hidefocus="true" class="ztorderform">
                    <span>我销售的订单</span></a></li>
                <li><s class="orderformicon"></s><a id="i_a_lm_2" href="javascript:void(0);" data-href="OrderList.aspx?OrderTypeBySearch=<%=(int)EyouSoft.Model.EnumType.TourStructure.OrderTypeBySearch.我操作的订单%>&sl=<%=(int)EyouSoft.Model.EnumType.PrivsStructure.Menu2.同业分销_订单中心 %>"
                    hidefocus="true" class="ztorderform">
                    <span>我操作的订单</span></a></li>
            </ul>
        </div>
        
        <div class="tablehead">
            <ul class="fixed">
                <li><s class="daochu"></s><a class="toolbar_daochu" id="i_a_toxls" hidefocus="true" href="javascript:void(0)"><span>导&nbsp;&nbsp;出</span></a></li>
                <li><s class="daochu"></s><a class="toolbar_Contract" id="i_a_SetContract" hidefocus="true" href="javascript:void(0)"><span>设定合同号</span></a></li>
            </ul>
            <div class="pages">
                <cc1:exporpageinfoselect id="ExporPageInfoSelect1" runat="server" />
            </div>
        </div>
        <!--列表表格-->
        <div class="tablelist-box">
            <table width="100%" id="liststyle">
                <tr>
                    <th width="30" class="thinputbg">
                        <input type="checkbox" name="checkbox" id="checkbox" />
                    </th>
                    <th width="177" align="left" class="th-line">
                        线路名称
                    </th>
                    <th width="101" align="center" class="th-line">
                        合同号
                    </th> 
                    <th width="101" align="center" class="th-line">
                        订单号
                    </th>                    
                    <th width="101" align="center" class="th-line">
                        下单人
                    </th>
                    <th width="101" align="center" class="th-line">
                        销售员
                    </th>
                    <th  align="center" class="th-line">
                        客源单位
                    </th>     
                    <%--<th class="th-line" style="text-align: center; ">
                        标准/等级
                    </th>
                    <th width="100" align="center" class="th-line">
                        销售价
                    </th>
                    <th width="100" align="center" class="th-line">
                        结算价
                    </th>--%>
                    <th width="59" align="center" class="th-line">
                        人数
                    </th>
                    <th width="68" align="center" class="th-line">
                        合计金额
                    </th>                    
                    <th width="86" align="center" class="th-line">
                        状态
                    </th>
                    <th width="110" align="center" class="th-line">
                        下单时间
                    </th>
                    <th width="60" align="center" class="th-line">
                        查看
                    </th>
                </tr>
                <asp:Repeater ID="rptList" runat="server">
                    <ItemTemplate>
                        <tr class="<%#Container.ItemIndex%2==0?"":"odd" %>">
                            <td align="center">
                                <input type="hidden" name="ItemUserID" data-userid='<%#Eval("OperatorId") %>' data-saleid='<%#Eval("SellerId") %>' />
                                <input type="checkbox" name="checkbox" id="checkbox" value="<%#Eval("OrderId") %>" />
                            </td>
                            <td align="left">
                                <a href="<%#GetPrintXingChengDan(Eval("TourId"),Eval("TourType")) %>" target="_blank"><%#Eval("RouteName")%></a>
                            </td>
                            <td align="center">
                                <%#Eval("ContractCode")%>
                            </td>
                            <td align="center">
                                <a href="<%# GetPrintJieSuanDan( Eval("OrderId"),Eval("TourType"))%>" target="_blank"
                                    title="<%#Eval("OrderCode")%>">
                                    <%#Eval("OrderCode")%></a>
                            </td>
                            <td align="center">
                                <%#Eval("Operator")%>
                            </td>
                            <td align="center">
                                <%# Eval("SellerName")%>
                            </td>
                            <td align="center">
                                <a href="<%#GetPrintYouKeQueRenDan( Eval("OrderId"),Eval("TourType")) %>" target="_blank"><%#Eval("BuyCompanyName")%></a>
                            </td>         
                            <%--<td style="text-align: center;">
                                <%#Eval("BaoJiaBiaoZhunName")%>/<%#Eval("KeHuLevName")%>                                                             
                            </td>                  
                            <td align="center">
                                <b class="fontgreen">
                                    <%#EyouSoft.Common.UtilsCommons.GetMoneyString(Eval("AdultPrice"), this.ProviderToMoney)%>
                                </b>
                            </td>
                            <td align="center">
                                <b class="fontblue">
                                    <%#EyouSoft.Common.UtilsCommons.GetMoneyString(Eval("PeerAdultPrice"), this.ProviderToMoney)%></b>
                            </td>--%>
                            <td align="center">
                                <b><a href="<%# GetPrintYouKeMingDan(Eval("OrderId"))%>" target="_blank">
                                    <%#Eval("Adults")%></a><sup class="fontred">+<%#Eval("Childs")%></sup></b>
                            </td>
                            <td align="center" >
                                <b class="<%#(bool)Eval("IsQueRenHeTongJinE")?"":"fontred" %>" title="<%#(bool)Eval("IsQueRenHeTongJinE")?"合同金额已确认":"合同金额未确认" %>">
                                    <%#EyouSoft.Common.UtilsCommons.GetMoneyString(Eval("ConfirmMoney"), this.ProviderToMoney)%></b>
                            </td>                            
                            <td align="center" class="fontblue" date-class="orderstate">
                                <input type="hidden" name="hideTourType" value='<%#(int)Eval("tourType") %>' />
                                <%# EyouSoft.Common.UtilsCommons.GetOrderStateForHtml(((int)Eval("OrderStatus")).ToString())%>
                            </td>
                            <td align="center">
                                <%#EyouSoft.Common.UtilsCommons.GetDateString(Eval("IssueTime"), "yyyy-MM-dd HH:mm")%>
                            </td>
                            <td align="center">
                                <a onclick="OrderInfo.ToOrderInfoByType('<%#Eval("TourType")%>','<%#Eval("OrderId")%>');return false;"
                                    href="javascript:void(0)" class="check-btn" title="查看"></a>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
            <div style="width: 100%; text-align: center; background-color: #ffffff">
                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
            </div>
            <div style="border-top: 0 none;" class="tablehead">
                <asp:PlaceHolder runat="server" ID="phHeJi">
                <div style="float:left; margin-top:8px; font-weight:bold;">
                &nbsp;合计信息：共计<asp:Literal runat="server" ID="ltrHeJi0"></asp:Literal>单；人数：<asp:Literal
                        runat="server" ID="ltrHeJi1"></asp:Literal>+<asp:Literal runat="server" ID="ltrHeJi2"></asp:Literal>；合计金额：<asp:Literal
                            runat="server" ID="ltrHeJi3"></asp:Literal>
                </div>
                </asp:PlaceHolder>
                <div class="pages">
                    <cc1:exporpageinfoselect id="ExporPageInfoSelect2" runat="server" />
                </div>
            </div>
        </div>
    </div>

    <script src="/Js/datepicker/WdatePicker.js" type="text/javascript"></script>

    <script type="text/javascript">
        var OrderInfo = {
            ToOrderInfoByType: function(tourtype, orderid) {
                var sl = '<%=SL %>';
                if (tourtype != null) {
                    if (tourtype == '<%=EyouSoft.Model.EnumType.TourStructure.TourType.出境散拼 %>' ||
                   tourtype == '<%=EyouSoft.Model.EnumType.TourStructure.TourType.地接散拼 %>' ||
                   tourtype == '<%=EyouSoft.Model.EnumType.TourStructure.TourType.组团散拼 %>') {
                        window.location.href = "/TeamCenter/SanKeBaoMing.aspx?OrderId=" + orderid + "&sl=" + sl + "&url=<%=Server.UrlEncode(Request.Url.ToString()) %>";
                    }
                    else if (tourtype == '<%=EyouSoft.Model.EnumType.TourStructure.TourType.出境团队 %>' ||
                        tourtype == '<%=EyouSoft.Model.EnumType.TourStructure.TourType.单项服务 %>' ||
                        tourtype == '<%=EyouSoft.Model.EnumType.TourStructure.TourType.地接团队 %>' ||
                        tourtype == '<%=EyouSoft.Model.EnumType.TourStructure.TourType.组团团队 %>') {
                        window.location.href = "/SellCenter/CustomerPlan/OrderInfo.aspx?OrderId=" + orderid + "&sl=" + sl;
                    } else if (tourtype == "<%=EyouSoft.Model.EnumType.TourStructure.TourType.组团散拼短线 %>") {
                        window.location.href = "/TeamCenter/ShortSanKeBaoMing.aspx?OrderId=" + orderid + "&sl=" + sl + "&url=<%=Server.UrlEncode(Request.Url.ToString()) %>";
                    }
                    else {
                        tableToolbar._showMsg("数据有误");
                    }
                }
            }
        };

        $(function() {
            tableToolbar.init({});

            $(".i_lanmu ul li").each(function() { $(this).find('a').click(function() { var url = $(this).attr("data-href"); window.location.href = url; return false; }); });

            $("#liststyle").find("a[data-class='chaoxian']").each(function() {
                var _self = $(this);
                var td = _self.closest("td");
                var type = td.find("input[name='hideTourType']").val();
                if (type == "<%=(int)EyouSoft.Model.EnumType.TourStructure.TourType.组团团队 %>" || type == "<%=(int)EyouSoft.Model.EnumType.TourStructure.TourType.地接团队 %>" || type == "<%=(int)EyouSoft.Model.EnumType.TourStructure.TourType.出境团队 %>") {
                    td.html('<font class="fontred">' + _self.html() + '</font>');
                } else {
                    _self.click(function() {
                        if (tableToolbar.IsHandleElse == "false") {
                            var sl = '<%=Request.QueryString["sl"] %>';
                            var id = $(this).closest("tr").find("input[type='checkbox']").val();
                            var msgList = [];
                            var thisObj = $(this).closest("tr").find("input[name='ItemUserID']");
                            if (thisObj.attr("data-userid") != tableToolbar.UserID && thisObj.attr("data-saleid") != tableToolbar.UserID) {
                                msgList.push("你不是该计划的销售员,无法垫付申请!");
                            }
                            if (msgList.length > 0) {
                                tableToolbar._showMsg(msgList.join("<br />"));
                                return false;
                            }
                        }
                        Boxy.iframeDialog({
                            iframeUrl: "/TeamCenter/ChaoXianShenQing.aspx?orderId=" + id + "&sl=" + sl,
                            title: "垫付申请",
                            modal: true,
                            width: "600px",
                            height: "300px"
                        });
                    })
                }
            });

            window["<%=txtOrderStatus.ClientID %>"].setValue({ value: "<%=EyouSoft.Common.Utils.GetQueryStringValue(txtOrderStatus.ZTIdClientID) %>", text: "<%=EyouSoft.Common.Utils.GetQueryStringValue(txtOrderStatus.ZTNameClientID) %>" });
            toXls.init({ "selector": "#i_a_toxls" });

            var _lm = '<%= (int)EyouSoft.Common.Utils.GetEnumValue < EyouSoft.Model.EnumType.TourStructure.OrderTypeBySearch > (EyouSoft.Common.Utils.GetQueryStringValue("OrderTypeBySearch"), EyouSoft.Model.EnumType.TourStructure.OrderTypeBySearch.我操作的订单) %>'
            $("#i_a_lm_" + _lm).addClass("de-ztorderform");

            //合同号设定 事件
            $("#i_a_SetContract").click(function() {
                var sl = '<%=Request.QueryString["sl"] %>';
                var ids = new Array();
                $("#liststyle").find("input[type='checkbox']:checked").each(function() {
                    if (this.value && this.value != "on") {
                        ids.push(this.value);
                    }
                })
                if (ids.length ==0) {
                    tableToolbar._showMsg("未选中任何订单!");
                    return false;
                }
                if (ids.length > 1) {
                    tableToolbar._showMsg("只能选择一个订单!");
                    return false;
                }
                Boxy.iframeDialog({
                iframeUrl: "/SellCenter/Order/SetContract.aspx?orderId=" + ids.join(',') + "&sl=" + sl,
                    title: "合同号设置",
                    modal: true,
                    width: "600px",
                    height: "300px"
                });
                return false;
            })
        });
    </script>
</asp:Content>

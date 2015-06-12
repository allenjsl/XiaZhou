<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OperaterLargeList.aspx.cs"
    Inherits="Web.OperaterCenter.OperaterLargeList" %>

<%@ Import Namespace=" EyouSoft.Common" %>
<%@ Register TagName="supplierControl" TagPrefix="uc6" Src="~/UserControl/SupplierControl.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>

    <script type="text/javascript" src="/Js/jquery-1.4.4.js"></script>

    <script src="/Js/datepicker/WdatePicker.js" type="text/javascript"></script>

    <!--[if IE]><script src="/js/excanvas.js" type="text/javascript" charset="utf-8"></script><![endif]-->

    <script src="/Js/ValiDatorForm.js" type="text/javascript"></script>

    <link href="/css/style.css" rel="stylesheet" type="text/css" />
    <!--[if lte IE 6]><script src="/js/jquery.bgiframe.min.js" type="text/javascript"></script><![endif]-->

    <script type="text/javascript" src="/Js/table-toolbar.js"></script>

    <script type="text/javascript" src="/Js/jquery.blockUI.js"></script>

    <script src="/Js/bt.min.js" type="text/javascript"></script>

    <style type="text/css">
        /*样式重写*/body, html
        {
            overflow: visible;
            width: 100%;
        }
        .jidiao-r
        {
            border-right: 0px;
            border-top: 0px;
        }
    </style>
</head>
<body style="background-color: #fff">
    <form id="largeform1" runat="server">
    <div class="jidiao-r">
        <div id="con_faq_5">
            <div>
                <table width="100%" cellpadding="0" cellspacing="0" class="jd-table01" style="border-bottom: 1px solid #A9D7EC;
                    margin-bottom: 1px;">
                    <tr>
                        <th width="15%" align="right" class="border-l">
                            大交通类型：
                        </th>
                        <td width="85%" colspan="3">
                            <label for="r1">
                                <input name="radioLartype" id="r1" type="radio" value="1" checked="checked" />
                                飞机</label>
                            <input name="radioLartype" id="r2" type="radio" value="2" />
                            <label for="r2">
                                火车</label>
                            <input name="radioLartype" id="r3" type="radio" value="3" />
                            <label for="r3">
                                汽车</label>
                        </td>
                    </tr>
                </table>
            </div>
            <div id="g" style="display: block">
                <table width="100%" cellpadding="0" cellspacing="0" class="jd-table01 line-b">
                    <tr>
                        <th width="15%" align="right" class="border-l">
                            <span class="fontred">*</span>出票点：
                        </th>
                        <td width="40%">
                            <uc6:supplierControl ID="supplierControl1" IsMust="false" runat="server" CallBack="LargePage._AjaxContectInfo"
                                SupplierType="飞机" />
                        </td>
                        <th width="15%" align="right">
                            联系人：
                        </th>
                        <td width="30%">
                            <asp:TextBox ID="txtContentName" runat="server" CssClass="inputtext formsize140"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th align="right" class="border-l">
                            联系电话：
                        </th>
                        <td>
                            <asp:TextBox ID="txtContentPhone" runat="server" CssClass=" inputtext formsize140"></asp:TextBox>
                        </td>
                        <th align="right">
                            联系传真：
                        </th>
                        <td>
                            <asp:TextBox ID="txtContentFax" runat="server" CssClass="inputtext formsize140"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th align="right" class="border-l">
                            <span class="fontred">*</span>航班：
                        </th>
                        <td colspan="3">
                            <table id="tabAirFilght" width="100%" border="0" cellspacing="0" cellpadding="0"
                                class="noborder">
                                <asp:PlaceHolder ID="tabHolderView" runat="server">
                                    <tr class="tempRow">
                                        <td>
                                            <input type="text" name="txtstartTime" id="txtstartTime" class="inputtext"
                                                onfocus="WdatePicker();" valid="required" errmsg="*请选择出发日期!"
                                                isvalid="1" data-title="出发日期" data-class="show" style="width:65px" />                                                                                       
                                            <input type="text" name="txtstartPlace" class="inputtext formsize40" isvalid="1"
                                                data-title="出发地" data-class="show" />
                                            <input type="text" class="inputtext formsize40" name="txtendPlace" isvalid="1" data-title="目的地"
                                                data-class="show" />
                                            <input type="text" class="inputtext formsize50" name="txtFilghtnumbers" isvalid="1"
                                                data-title="航班号" data-class="show" />
                                            <%=seleSpaceTypeHtml("")%>&nbsp&nbsp<%=PassengerstypeHtml("") %>
                                            人数：<input type="text" class="inputtext formsize40" data-class="show" data-title="人数"
                                                 name="txtpeopleNums" valid="required|RegInteger" errmsg="*请输入人数!|*人数必须是正整数!"
                                                isvalid="1" />
                                            单价：<input type="text" class="inputtext formsize40" data-class="show" data-title="单价"
                                                name="txtprices" valid="required|isMoney" errmsg="*请输入单价!|*单价有误!"
                                                isvalid="1" />                                            
                                            备注：<input type="text" name="txtFeiJiHangBanBeiZhu" class="inputtext" maxlength="255" />
                                            小计：<input type="text" class="inputtext formsize40" data-class="show" data-title="小计"
                                                name="txtXiaoJi" valid="required|isMoney" errmsg="*请输入小计费用!|*小计费有误!"
                                                isvalid="1" />
                                            元
                                        </td>
                                        <td width="110" align="right">
                                            <a href="javascript:void(0)" class="addbtn">
                                                <img height="20" width="48" src="/images/addimg.gif" alt=""></a>&nbsp; <a href="javascript:void(0)"
                                                    class="delbtn">
                                                    <img height="20" width="48" src="/images/delimg.gif" alt=""></a>
                                        </td>
                                    </tr>
                                </asp:PlaceHolder>
                                <asp:Repeater ID="repFilght" runat="server">
                                    <ItemTemplate>
                                        <tr class="tempRow">
                                            <td>
                                                <input type="text" isvalid="1" name="txtstartTime" id="txtstartTime" class="inputtext"
                                                    valid="required" errmsg="*请输入出发时间!" value="<%#EyouSoft.Common.UtilsCommons.GetDateString(Eval("DepartureTime"),ProviderToDate) %>"
                                                    onfocus="WdatePicker();" data-title="出发日期" data-class="show" style="width:65[x" />                                                
                                                <input type="text" isvalid="1" name="txtstartPlace" class="inputtext formsize40"
                                                    value="<%# Eval("Departure") %>" data-title="出发地" data-class="show" />
                                                <input type="text" isvalid="1" class="inputtext formsize40" name="txtendPlace" value="<%# Eval("Destination") %>"
                                                    data-title="目的地" data-class="show" />
                                                <input type="text" isvalid="1" class="inputtext formsize50" name="txtFilghtnumbers"
                                                    value="<%# Eval("Numbers") %>" data-title="航班号" data-class="show" />
                                                <%#seleSpaceTypeHtml(((int)Eval("SeatType")).ToString())%>&nbsp&nbsp
                                                <%#PassengerstypeHtml(((int)Eval("AdultsType")).ToString())%>
                                                人数：<input type="text" class="inputtext formsize40" name="txtpeopleNums" valid="required|RegInteger"
                                                    errmsg="*请输入人数!|*人数必须是正整数!" min="1" data-class="show" data-title="人数"
                                                    value="<%# EyouSoft.Common.Utils.FilterEndOfTheZeroDecimal(Convert.ToDecimal(Eval("PayNumber"))) %>"
                                                    isvalid="1" />
                                                单价：<input type="text" class="inputtext formsize40" name="txtprices" valid="required|isMoney"
                                                    errmsg="*请输入单价!|*单价有误!" data-class="show" data-title="单价" value="<%# EyouSoft.Common.Utils.FilterEndOfTheZeroDecimal(Convert.ToDecimal(Eval("FarePrice"))) %>"
                                                    isvalid="1" />
                                                备注：<input type="text" name="txtFeiJiHangBanBeiZhu" value="<%#Eval("BeiZhu") %>" class="inputtext" maxlength="255" />
                                                小计：<input type="text" class="inputtext formsize40" name="txtXiaoJi" valid="required|isMoney"
                                                    errmsg="*请输入小计费用!|*小计费有误!" data-class="show" data-title="小计"
                                                    value="<%#EyouSoft.Common.Utils.FilterEndOfTheZeroDecimal(Convert.ToDecimal(Eval("SumPrice"))) %>"
                                                    isvalid="1" />
                                                元
                                            </td>
                                            <td align="right">
                                                <a href="javascript:void(0)" class="addbtn">
                                                    <img height="20" width="48" alt="" src="/images/addimg.gif"></a>&nbsp; <a href="javascript:void(0)"
                                                        class="delbtn">
                                                        <img height="20" alt="" width="48" src="/images/delimg.gif"></a>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <th align="right" class="border-l">
                            <span class="fontred">*</span>结算费用：
                        </th>
                        <td colspan="3" align="left">
                            <asp:TextBox ID="txtCostAccount" runat="server" CssClass="inputtext formsize50" valid="required|isMoney"
                                errmsg="*请输入结算费用!|*结算费用输入有误！" isvalid="1"></asp:TextBox>
                            元
                        </td>
                    </tr>
                    <tr>
                        <th align="right" class="border-l">
                            费用明细：
                        </th>
                        <td colspan="3">
                            <asp:TextBox ID="txtCostParticu" TextMode="MultiLine" runat="server" CssClass="inputtext formsize600"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th align="right" class="border-l">
                            导游需知：
                        </th>
                        <td colspan="3">
                            <asp:TextBox ID="txtGuidNotes" runat="server" TextMode="MultiLine" CssClass="inputtext formsize600">
出发时间:
航班号:
出发地:
目的地:
请提前90分钟抵达机场
                            </asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th align="right" class="border-l">
                            其它备注：
                        </th>
                        <td colspan="3">
                            <asp:TextBox ID="txtOtherMark" runat="server" TextMode="MultiLine" CssClass="inputtext formsize600"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th align="right" class="border-l">
                            <span class="fontred">*</span>支付方式：
                        </th>
                        <td align="left">
                            <select name="SelPamyMent" class="inputselect">
                                <%=UtilsCommons.GetOperaterPanyMentList(PantMent)%>
                            </select>
                        </td>
                        <th align="right">
                            <span class="fontred">*</span>状态：
                        </th>
                        <td align="left">
                            <select name="SelStatus" class="inputselect">
                                <%=UtilsCommons.GetOperaterStatusList(Status)%>
                            </select>
                        </td>
                    </tr>
                </table>
                <asp:PlaceHolder ID="panShowListFrist" runat="server">
                    <h2>
                        <p>
                            已安排机票</p>
                    </h2>
                    <table width="100%" border="0" cellspacing="0" cellpadding="0" class="jd-table01"
                        style="border-bottom: 1px solid #A9D7EC;" id="Airlist">
                        <tr>
                            <th align="" class="border-l">
                                出票点
                            </th>
                            <th align="center" class="addtableT">
                                出票数
                            </th>
                            <th align="center" class="addtableT">
                                费用明细
                            </th>
                            <th align="right" class="addtableT">
                                结算费用
                            </th>
                            <th align="center" class="addtableT">
                                支付方式
                            </th>
                            <th align="center" class="addtableT">
                                状态
                            </th>
                            <th align="center" class="addtableT">
                                确认单
                            </th>
                            <th align="center" class="addtableT">
                                操作
                            </th>
                        </tr>
                        <asp:Repeater ID="repAirlist" runat="server">
                            <ItemTemplate>
                                <tr>
                                    <td align="center" class="border-l">
                                        <%# Eval("SourceName")%>
                                    </td>
                                    <td align="center">
                                        <%# Eval("Num")%>
                                    </td>
                                    <td align="center">
                                        <span title="<%#Eval("CostDetail") %>">
                                            <%# EyouSoft.Common.Utils.GetText(Eval("CostDetail").ToString(),30,true)%></span>
                                    </td>
                                    <td align="right" class="red">
                                        <%#UtilsCommons.GetMoneyString(Convert.ToDecimal(Eval("Confirmation")),ProviderToMoney)%><a
                                            href="javascript:" data-class="Prepaid" title="预付申请" data-id="<%# Eval("PlanId") %>"
                                            data-soucesname="<%# Eval("SourceName")%>">
                                            <img src="/images/yufu.gif" /></a>
                                    </td>
                                    <td align="center">
                                        <%# Eval("PaymentType").ToString()%>
                                    </td>
                                    <td align="center">
                                        <%# Eval("Status").ToString()%>
                                    </td>
                                    <td align="center">
                                        <a href='<%# querenAirUrl %>?planId=<%#Eval("PlanId")%>' target="_blank">
                                            <img src="/images/y-kehuqueding.gif" /></a>
                                    </td>
                                    <td align="center">
                                        <%if (ListPower)
                                          { %>
                                        <a href="javascript:" data-class="updateAir">
                                            <img src="/images/y-delupdateicon.gif" data-id="<%# Eval("PlanId") %>" alt="" />修改</a>
                                        <a href="javascript:" data-class="deleteAir">
                                            <img src="/images/y-delicon.gif" data-id="<%# Eval("PlanId") %>" alt="" />删除</a>
                                        <%}
                                          else
                                          { %>
                                        <a href="javascript:" data-class="showAir">
                                            <img src="/images/y-delupdateicon.gif" data-id="<%# Eval("PlanId") %>" alt="" />查看</a>
                                        <%} %>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </table>
                    <div class="hr_5">
                    </div>
                </asp:PlaceHolder>
                <asp:PlaceHolder ID="panView" runat="server">
                    <div class="mainbox cunline fixed">
                        <ul id="ul_Btn_listAir">
                            <li class="cun-cy"><a href="javascript:" id="btnSave">保存</a> </li>
                        </ul>
                    </div>
                </asp:PlaceHolder>
            </div>
            <div id="h" style="display: none">
                <table width="100%" cellpadding="0" cellspacing="0" class="jd-table01 line-b">
                    <tr>
                        <th width="15%" align="right" class="border-l">
                            <span class="fontred">*</span>出票点：
                        </th>
                        <td width="40%">
                            <uc6:supplierControl ID="supplierControl2" IsMust="false" runat="server" SupplierType="火车"
                                CallBack="LargePage._AjaxContectInfoT" />
                        </td>
                        <th width="15%" align="right">
                            联系人：
                        </th>
                        <td width="30%">
                            <asp:TextBox ID="txtContentName1" runat="server" CssClass="inputtext formsize140"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th align="right" class="border-l">
                            联系电话：
                        </th>
                        <td>
                            <asp:TextBox ID="txtContentPhone1" runat="server" CssClass="inputtext formsize140"></asp:TextBox>
                        </td>
                        <th align="right">
                            联系传真：
                        </th>
                        <td>
                            <asp:TextBox ID="txtContentFax1" runat="server" CssClass="inputtext formsize140"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th align="right" class="border-l">
                            <span class="fontred">*</span>费用明细：
                        </th>
                        <td colspan="3" align="left">
                            <asp:TextBox ID="txtCostDetail" runat="server" TextMode="MultiLine" CssClass="inputtext formsize600"
                                valid="required" errmsg="*请输入费用明细!" isvalid="1"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th align="right" class="border-l">
                            <span class="fontred">*</span>班次：
                        </th>
                        <td colspan="3">
                            <table width="100%" id="tabcoachTrain" border="0" cellspacing="0" cellpadding="0"
                                class="noborder">
                                <asp:PlaceHolder ID="tabHolderView1" runat="server">
                                    <tr class="tempRow">
                                        <td>
                                            <input type="text" id="txtStartTimeTrain" class="inputtext formsize80" value="" name="txtStartTimeTrain"
                                                valid="required" errmsg="*请输入出发日期!" onfocus="WdatePicker();"
                                                isvalid="1" data-title="出发日期" data-class="show" />
                                            <input type="text" class="inputtext formsize40"  name="txtStartHoursTrain"
                                                isvalid="1" data-title="点时间" data-class="show" />
                                            <input type="text" class="inputtext formsize50"  name="txtStartPlaceTrain"
                                                isvalid="1" data-title="出发地" data-class="show" />
                                            <input type="text" class="inputtext formsize50"  name="txtendPlaceTrain"
                                                isvalid="1" data-title="目的地" data-class="show" />
                                            <input type="text" class="inputtext formsize50"  name="txtcoachNumTrain"
                                                isvalid="1" data-title="车次" data-class="show" />
                                            <input type="text" class="inputtext formsize50"  name="txtseatStandTrain"
                                                isvalid="1" data-title="坐位标准" data-class="show" />
                                            张数：<input type="text" class="inputtext formsize20"  name="txtpayNumTrain"
                                                valid="required|RegInteger" errmsg="*请输入张数!|*张数有误！" isvalid="1" data-title="张数"
                                                data-class="show" />
                                            单价：<input type="text" class="inputtext formsize40" name="txtHuoCheDanJia" valid="required|isMoney"
                                                isvalid="1" errmsg="请输入单价|请输入正确的单价" data-title="单价" data-class="show" />
                                            小计：<input type="text" class="inputtext formsize40" name="txtHuoCheXiaoJi" valid="required|isMoney"
                                                isvalid="1" errmsg="请输入小计|请输入正确的小计" data-title="小计" data-class="show" />
                                            备注：<input type="text" name="txtHuoCheBeiZhu" class="inputtext"
                                                maxlength="255" />
                                        </td>
                                        <td width="120" align="right">
                                            <a href="javascript:void(0)" class="addbtn">
                                                <img height="20" width="48" src="/images/addimg.gif"></a>&nbsp; <a href="javascript:void(0)"
                                                    class="delbtn">
                                                    <img height="20" width="48" src="/images/delimg.gif"></a>
                                        </td>
                                    </tr>
                                </asp:PlaceHolder>
                                <asp:Repeater ID="repcoachlist" runat="server">
                                    <ItemTemplate>
                                        <tr class="tempRow">
                                            <td>
                                                <input type="text" id="txtStartTimeTrain" class="inputtext formsize80" name="txtStartTimeTrain"
                                                    valid="required" errmsg="*请输入出发日期!" value="<%#EyouSoft.Common.UtilsCommons.GetDateString(Eval("DepartureTime"),ProviderToDate) %>"
                                                    isvalid="1" onfocus="WdatePicker();" data-title="出发日期" data-class="show" />
                                                <input type="text" class="inputtext formsize40" name="txtStartHoursTrain" value="<%# Eval("Time") %>" isvalid="1" data-title="点时间" data-class="show" />
                                                <input type="text" class="inputtext formsize50" name="txtStartPlaceTrain"  value="<%# Eval("Departure") %>" isvalid="1" data-title="出发地"
                                                    data-class="show" />
                                                <input type="text" class="inputtext formsize50" name="txtendPlaceTrain" value="<%# Eval("Destination") %>" isvalid="1" data-title="目的地"
                                                    data-class="show" />
                                                <input type="text" class="inputtext formsize50" name="txtcoachNumTrain"  value="<%# Eval("Numbers") %>" isvalid="1" data-title="车次" data-class="show" />
                                                <input type="text" class="inputtext formsize50" name="txtseatStandTrain"  value="<%# Eval("SeatStandard") %>" isvalid="1" data-title="坐位标准"
                                                    data-class="show" />
                                                张数：<input type="text" class="inputtext formsize20" name="txtpayNumTrain" valid="required|RegInteger"
                                                    errmsg="*请输入张数!|*张数输入不正确!" value="<%# Eval("PayNumber") %>" isvalid="1" data-title="张数"
                                                    data-class="show" />
                                                单价：<input type="text" class="inputtext formsize40" name="txtHuoCheDanJia" valid="required|isMoney"
                                                    isvalid="1" errmsg="请输入单价|请输入正确的单价" value="<%#Eval("FarePrice","{0:F2}") %>"
                                                    data-title="单价" data-class="show" />
                                                小计：<input type="text" class="inputtext formsize40" name="txtHuoCheXiaoJi" valid="required|isMoney"
                                                    isvalid="1" errmsg="请输入小计|请输入正确的小计" value="<%#Eval("SumPrice","{0:F2}") %>" data-title="小计"
                                                    data-class="show" />
                                                备注：<input type="text" name="txtHuoCheBeiZhu" value="<%#Eval("BeiZhu") %>" class="inputtext"
                                                    maxlength="255" />
                                            </td>
                                            <td width="120" align="right">
                                                <a href="javascript:void(0)" class="addbtn">
                                                    <img height="20" width="48" src="/images/addimg.gif"></a>&nbsp; <a href="javascript:void(0)"
                                                        class="delbtn">
                                                        <img height="20" width="48" src="/images/delimg.gif"></a>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <th align="right" class="border-l">
                            <span class="fontred">*</span>结算费用：
                        </th>
                        <td colspan="3" align="left">
                            <asp:TextBox ID="txtTotalPrices1" runat="server" CssClass="inputtext formsize80"
                                valid="required|isMoney" errmsg="*请输入结算费用!|*结算费用输入有误!"
                                isvalid="1"></asp:TextBox>
                            元
                        </td>
                    </tr>
                    <tr>
                        <th align="right" class="border-l">
                            导游需知：
                        </th>
                        <td colspan="3">
                            <asp:TextBox ID="txtGuidNotes1" TextMode="MultiLine" runat="server" CssClass="inputtext formsize600">
出发时间:
出发地:
目的地:
车次:
请提前一小时抵达车站。
                            </asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th align="right" class="border-l">
                            其它备注：
                        </th>
                        <td colspan="3">
                            <asp:TextBox ID="txtOtherRemark1" TextMode="MultiLine" runat="server" CssClass="inputtext formsize600"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th align="right">
                            <span class="fontred">*</span>支付方式：
                        </th>
                        <td align="left">
                            <select name="SelPanyMentT" class="inputselect">
                                <%=UtilsCommons.GetOperaterPanyMentList(PanyMentT)%>
                            </select>
                        </td>
                        <th align="right">
                            <span class="fontred">*</span>状态：
                        </th>
                        <td align="left">
                            <select name="SelStatusT" class="inputselect">
                                <%=UtilsCommons.GetOperaterStatusList(StatusT)%>
                            </select>
                        </td>
                    </tr>
                </table>
                <asp:PlaceHolder ID="panShowListSecond" runat="server">
                    <h2>
                        <p>
                            已安排火车票</p>
                    </h2>
                    <table width="100%" id="tabTrainlist" border="0" cellspacing="0" cellpadding="0"
                        class="jd-table01" style="border-bottom: 1px solid #A9D7EC;">
                        <tr>
                            <th align="center" class="border-l">
                                出票点
                            </th>
                            <th align="center">
                                张数
                            </th>
                            <!--<th align="center">
                                免费数量
                            </th>-->
                            <th align="center">
                                费用明细
                            </th>
                            <th align="center">
                                支付方式
                            </th>
                            <th align="right">
                                结算费用
                            </th>
                            <th align="center">
                                状态
                            </th>
                            <th align="center" class="addtableT">
                                确认单
                            </th>
                            <th align="center">
                                操作
                            </th>
                        </tr>
                        <asp:Repeater ID="reptrainlist" runat="server">
                            <ItemTemplate>
                                <tr>
                                    <td align="center" class="border-l">
                                        <%# Eval("SourceName")%>
                                    </td>
                                    <td align="center">
                                        <%# Eval("Num") %>
                                    </td>
                                    <!--<td align="center">
                                        <%# Eval("FreeNumber")%>
                                    </td>-->
                                    <td align="center">
                                        <span title="<%#Eval("CostDetail") %>">
                                            <%# EyouSoft.Common.Utils.GetText(Eval("CostDetail").ToString(),30,true)%></span>
                                    </td>
                                    <td align="center">
                                        <%# Eval("PaymentType").ToString()%>
                                    </td>
                                    <td align="right">
                                        <%#UtilsCommons.GetMoneyString(Convert.ToDecimal(Eval("Confirmation")),ProviderToMoney)%>
                                        <a href="javascript:" data-class="Prepaid" title="预付申请" data-id="<%# Eval("PlanId") %>"
                                            data-soucesname="<%# Eval("SourceName")%>">
                                            <img src="/images/yufu.gif" alt="" data-id="<%# Eval("PlanId") %>" /></a>
                                    </td>
                                    <td align="center">
                                        <%# Eval("Status").ToString()%>
                                    </td>
                                    <td align="center">
                                        <a href='<%# querenTrainUrl %>?planId=<%#Eval("PlanId") %>' target="_blank">
                                            <img src="/images/y-kehuqueding.gif" alt="" /></a>
                                    </td>
                                    <td align="center">
                                        <%if (ListPower)
                                          { %>
                                        <a href="javascript:" data-class="updateTrain">
                                            <img src="/images/y-delupdateicon.gif" alt="" data-id="<%# Eval("PlanId") %>" />
                                            修改</a> <a href="javascript:" data-class="deleteTrain">
                                                <img src="/images/y-delicon.gif" alt="" data-id="<%# Eval("PlanId") %>" />
                                                删除</a>
                                        <%}
                                          else
                                          { %>
                                        <a href="javascript:" data-class="showTrain">
                                            <img src="/images/y-delupdateicon.gif" alt="" data-id="<%# Eval("PlanId") %>" />
                                            查看</a>
                                        <%} %>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </table>
                    <div class="hr_5">
                    </div>
                </asp:PlaceHolder>
                <asp:PlaceHolder ID="panView1" runat="server">
                    <div class="mainbox cunline fixed">
                        <ul id="ul_Btn_listTrain">
                            <li class="cun-cy"><a href="javascript:" id="btnSave1">保存</a> </li>
                        </ul>
                    </div>
                </asp:PlaceHolder>
            </div>
            <div id="j" style="display: none">
                <table width="100%" cellpadding="0" cellspacing="0" class="jd-table01 line-b">
                    <tr>
                        <th width="15%" align="right" class="border-l">
                            <span class="fontred">*</span>出票点：
                        </th>
                        <td width="40%">
                            <uc6:supplierControl ID="supplierControl3" IsMust="false" runat="server" CallBack="LargePage._AjaxContectInfoB"
                                SupplierType="汽车" />
                        </td>
                        <th width="15%" align="right">
                            联系人：
                        </th>
                        <td width="30%">
                            <asp:TextBox ID="txtContentName2" runat="server" CssClass="inputtext formsize140"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th align="right" class="border-l">
                            联系电话：
                        </th>
                        <td>
                            <asp:TextBox ID="txtContentPhone2" runat="server" CssClass="inputtext formsize140"></asp:TextBox>
                        </td>
                        <th align="right">
                            联系传真：
                        </th>
                        <td>
                            <asp:TextBox ID="txtContentFax2" runat="server" CssClass="inputtext formsize140"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th width="15%" align="right" class="border-l">
                            <span class="fontred">*</span>班次：
                        </th>
                        <td colspan="3">
                            <table width="100%" id="tabCoachBus" border="0" cellspacing="0" cellpadding="0" class="noborder">
                                <asp:PlaceHolder ID="tabHolderView2" runat="server">
                                    <tr class="tempRow">
                                        <td width="84%">
                                            <input type="text" id="txtStartTimeBus" class="inputtext formsize80" value="" name="txtStartTimeBus"
                                                onfocus="WdatePicker();" valid="required" errmsg="*请输入出发日期!" isvalid="1" data-title="出发日期"
                                                data-class="show" />
                                            <!--<input type="text" class="inputtext formsize40" name="txtStartHoursBus"
                                                isvalid="1" data-title="点时间" data-class="show" />
                                            <input type="text" class="inputtext formsize80"  name="txtCarNumBus"  isvalid="1" data-title="车牌号" data-class="show" />-->
                                            <input type="text" class="inputtext formsize50" name="txtStartPlaceBus"
                                                 isvalid="1" data-title="出发地" data-class="show" />
                                            <input type="text" class="inputtext formsize50" name="txtUnderPlaceBus"
                                                isvalid="1" data-title="目的地" data-class="show" />
                                            单价：<input type="text" class="inputtext formsize40" name="txtPricesBus" valid="required|isMoney"
                                                errmsg="*请输入价格!|*价格有误!" isvalid="1"  data-title="价格" data-class="show" />
                                            张数：<input type="text" class="inputtext formsize40" name="txtTicketNumBus"
                                                valid="required|RegInteger" errmsg="*请输入张数!|*张数有误!" isvalid="1"
                                                data-title="张数" data-class="show" />
                                            小计：<input type="text" class="inputtext formsize40" name="txtQiCheXiaoJi" valid="required|isMoney"
                                                isvalid="1" errmsg="请输入小计|请输入正确的小计"  data-title="小计"
                                                data-class="show" />
                                            备注：<input type="text" name="txtQiCheBeiZhu" class="inputtext"
                                                maxlength="255" />
                                        </td>
                                        <td width="16%" align="right">
                                            <a href="javascript:void(0)" class="addbtn">
                                                <img height="20" width="48" src="/images/addimg.gif" alt=""></a>&nbsp; <a href="javascript:void(0)"
                                                    class="delbtn">
                                                    <img height="20" width="48" src="/images/delimg.gif" alt=""></a>
                                        </td>
                                    </tr>
                                </asp:PlaceHolder>
                                <asp:Repeater ID="repcoachlist1" runat="server">
                                    <ItemTemplate>
                                        <tr class="tempRow">
                                            <td width="84%">
                                                <input type="text" class="inputtext formsize80" name="txtStartTimeBus" id="txtStartTimeBus"
                                                    onfocus="WdatePicker();" valid="required" errmsg="*请输入出发日期!" value="<%# EyouSoft.Common.UtilsCommons.GetDateString(Eval("DepartureTime"), ProviderToDate) %>"
                                                    isvalid="1" data-title="出发日期" data-class="show" />
                                                <!--<input type="text" class="inputtext formsize40" name="txtStartHoursBus" value="<%# Eval("Time") %>" isvalid="1" data-title="点时间" data-class="show" />
                                                <input type="text" class="inputtext formsize80" name="txtCarNumBus"  value="<%# Eval("Numbers") %>" isvalid="1" data-title="车牌号"
                                                    data-class="show" />-->
                                                <input type="text" class="inputtext formsize50" name="txtStartPlaceBus" value="<%# Eval("Departure") %>" isvalid="1" data-title="出发地"
                                                    data-class="show" />
                                                <input type="text" class="inputtext formsize50" name="txtUnderPlaceBus" value="<%# Eval("Destination") %>" isvalid="1" data-title="目的地"
                                                    data-class="show" />
                                                单价：<input type="text" class="inputtext formsize40" name="txtPricesBus" valid="required|isMoney"
                                                    errmsg="*请输入价格!|*价格有误!" value="<%# EyouSoft.Common.Utils.FilterEndOfTheZeroString(Eval("FarePrice").ToString())  %>"
                                                    isvalid="1" data-title="价格" data-class="show" />
                                                张数：<input type="text" class="inputtext formsize40" name="txtTicketNumBus" valid="required|RegInteger"
                                                     errmsg="*请输入张数!|*张数有误!" value="<%# Eval("PayNumber") %>" isvalid="1"
                                                    data-title="张数" data-class="show" />
                                                小计：<input type="text" class="inputtext formsize40" name="txtQiCheXiaoJi" valid="required|isMoney"
                                                    isvalid="1" errmsg="请输入小计|请输入正确的小计" value="<%#Eval("SumPrice","{0:F2}") %>" data-title="小计"
                                                    data-class="show" />
                                                备注：<input type="text" name="txtQiCheBeiZhu" value="<%#Eval("BeiZhu") %>" class="inputtext"
                                                    maxlength="255" />
                                            </td>
                                            <td width="16%" align="right">
                                                <a href="javascript:void(0)" class="addbtn">
                                                    <img height="20" width="48" src="/images/addimg.gif"></a>&nbsp; <a href="javascript:void(0)"
                                                        class="delbtn">
                                                        <img height="20" width="48" src="/images/delimg.gif"></a>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <th align="right" class="border-l">
                            <span class="fontred">*</span>结算费用：
                        </th>
                        <td colspan="3" align="left">
                            <asp:TextBox ID="txtTotalPricesBus" runat="server" CssClass="inputtext formsize80"
                                valid="required|isMoney" errmsg="*请输入结算费用!|*结算费用输入有误!"
                                isvalid="1"></asp:TextBox>
                            元
                        </td>
                    </tr>
                    <tr>
                        <th align="right" class="border-l">
                            费用明细：
                        </th>
                        <td colspan="3">
                            <asp:TextBox ID="txtcostDetailbus" runat="server" CssClass="inputtext formsize600"
                                 TextMode="MultiLine"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th align="right" class="border-l">
                            导游需知：
                        </th>
                        <td colspan="3">
                            <asp:TextBox ID="txtGuidNotes2" runat="server" CssClass="inputtext formsize600"
                                TextMode="MultiLine">
出发时间：
出发地：
目的地：
                            </asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th align="right" class="border-l">
                            其它备注：
                        </th>
                        <td colspan="3">
                            <asp:TextBox ID="txtOtherRemark2" runat="server" CssClass="inputtext formsize600"
                                TextMode="MultiLine"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th align="right" class="border-l">
                            <span class="fontred">*</span>支付方式：
                        </th>
                        <td align="left">
                            <select name="SelPanyMentB" class="inputselect">
                                <%=UtilsCommons.GetOperaterPanyMentList(panyMentB)%>
                            </select>
                        </td>
                        <th align="right">
                            <span class="fontred">*</span>状态：
                        </th>
                        <td align="left">
                            <select name="selStatusB" class="inputselect">
                                <%=UtilsCommons.GetOperaterStatusList(StatusB)%>
                            </select>
                        </td>
                    </tr>
                </table>
                <asp:PlaceHolder ID="panShowListThird" runat="server">
                    <h2>
                        <p>
                            已安排汽车票</p>
                    </h2>
                    <table width="100%" border="0" cellspacing="0" cellpadding="0" class="jd-table01"
                        style="border-bottom: 1px solid #A9D7EC;" id="tabPlanBus">
                        <tr>
                            <th align="center" class="border-l">
                                出票点
                            </th>
                            <th align="center">
                                张数
                            </th>
                            <th align="center" class="addtableT">
                                费用明细
                            </th>
                            <th align="right">
                                结算费用
                            </th>
                            <th align="center" class="addtableT">
                                支付方式
                            </th>
                            <th align="center">
                                状态
                            </th>
                            <th align="center" class="addtableT">
                                确认单
                            </th>
                            <th align="center">
                                操作
                            </th>
                        </tr>
                        <asp:Repeater ID="repBuslist" runat="server">
                            <ItemTemplate>
                                <tr>
                                    <td align="center" class="border-l">
                                        <%# Eval("SourceName")%>
                                    </td>
                                    <td align="center">
                                        <%# Eval("Num")%>
                                    </td>
                                    <td align="center">
                                        <span title="<%#Eval("CostDetail") %>">
                                            <%# EyouSoft.Common.Utils.GetText(Eval("CostDetail").ToString(),30,true)%></span>
                                    </td>
                                    <td align="right">
                                        <%# UtilsCommons.GetMoneyString(Convert.ToDecimal(Eval("Confirmation")), ProviderToMoney)%>
                                        <a href="javascript:" data-class="Prepaid" title="预付申请" data-id="<%# Eval("PlanId") %>"
                                            data-soucesname="<%# Eval("SourceName")%>">
                                            <img src="/images/yufu.gif" alt="" data-id="<%# Eval("PlanId") %>" /></a>
                                    </td>
                                    <td align="center">
                                        <%# Eval("PaymentType").ToString()%>
                                    </td>
                                    <td align="center">
                                        <%# Eval("Status").ToString() %>
                                    </td>
                                    <td align="center">
                                        <a href='<%#querenBusUrl %>?planId=<%#Eval("PlanId") %>' target="_blank">
                                            <img src="/images/y-kehuqueding.gif" alt="" /></a>
                                    </td>
                                    <td align="center">
                                        <%if (ListPower)
                                          { %>
                                        <a href="javascript:" data-class="updatebus">
                                            <img src="/images/y-delupdateicon.gif" data-id="<%# Eval("PlanId") %>" />
                                            修改</a> <a href="javascript:" data-class="deletebus">
                                                <img src="/images/y-delicon.gif" data-id="<%# Eval("PlanId") %>" />
                                                删除</a>
                                        <%}
                                          else
                                          { %>
                                        <a href="javascript:" data-class="showbus">
                                            <img src="/images/y-delupdateicon.gif" data-id="<%# Eval("PlanId") %>" />
                                            查看</a>
                                        <%} %>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </table>
                    <div class="hr_5">
                    </div>
                </asp:PlaceHolder>
                <asp:PlaceHolder ID="panView2" runat="server">
                    <div class="mainbox cunline fixed">
                        <ul id="ul_Btn_listBus">
                            <li class="cun-cy"><a href="javascript:" id="btnSave3">保存</a> </li>
                        </ul>
                    </div>
                </asp:PlaceHolder>
            </div>
        </div>
    </div>
    </form>

    <script type="text/javascript">
        var LargePage = {
            sl: '<%=SL %>',
            type: '<%=Utils.GetQueryStringValue("Type") %>',
            tourId: '<%=Utils.GetQueryStringValue("tourId") %>',
            iframeId: '<%=Utils.GetQueryStringValue("iframeId") %>',
            _InitSupAttr: function() {
                $("#<%=supplierControl1.ClientText %>").attr("isvalid", "1").attr("valid", "required").attr("errmsg", "*请选择出票点!");
                $("#<%=supplierControl2.ClientText %>").attr("isvalid", "1").attr("valid", "required").attr("errmsg", "*请选择出票点!");
                $("#<%=supplierControl3.ClientText %>").attr("isvalid", "1").attr("valid", "required").attr("errmsg", "*请选择出票点!");
            },
            _RemoveAttr: function(ct) {
                $(ct).find("[isvalid='1']").attr("data-valid", $(ct).find("[isvalid='1']").attr("valid"));
                $(ct).find("[isvalid='1']").removeAttr("valid");
            },
            _AddAttr: function(ct) {
                $(ct).find("[isvalid='1']").attr("valid", $(ct).find("[isvalid='1']").attr("data-valid"));
            },
            _DeleteLarg: function(_ObjID) { //删除飞机
                var _Url = '/OperaterCenter/OperaterLargeList.aspx?sl=' + LargePage.sl + '&type=' + LargePage.type + '&tourId=' + LargePage.tourId + "&typeId=9_1" + "&iframeId=" + LargePage.iframeId + "&m=" + new Date().getTime();
                $.newAjax({
                    type: "get",
                    url: '/OperaterCenter/OperaterLargeList.aspx?sl=' + LargePage.sl + '&action=deleteAir&planIdAir=' + _ObjID + '&tourid=' + LargePage.tourId,
                    cache: false,
                    dataType: 'json',
                    success: function(ret) {
                        if (ret.result == "1") {
                            parent.tableToolbar._showMsg(ret.msg, function() {
                                window.location.href = _Url;
                            });
                            return false;
                        }
                        else {
                            parent.tableToolbar._showMsg(ret.msg, function() {
                                window.location.href = _Url;
                            });
                            return false;
                        }
                    },
                    error: function() {
                        parent.tableToolbar._showMsg(tableToolbar.errorMsg);
                        return false;
                    }
                });
            },
            _DeleteTrain: function(_ObjID) { //删除火车
                var _Url = '/OperaterCenter/OperaterLargeList.aspx?sl=' + LargePage.sl + '&type=' + LargePage.type + '&tourId=' + LargePage.tourId + "&typeId=9_2" + "&iframeId=" + LargePage.iframeId + "&m=" + new Date().getTime();
                $.newAjax({
                    type: "get",
                    url: '/OperaterCenter/OperaterLargeList.aspx?sl=' + LargePage.sl + '&action=deleteTrain&planIdTrainId=' + _ObjID + '&tourid=' + LargePage.tourId,
                    cache: false,
                    dataType: 'json',
                    success: function(ret) {
                        if (ret.result == "1") {
                            parent.tableToolbar._showMsg(ret.msg, function() {
                                window.location.href = _Url;
                            });
                            return false;
                        }
                        else {
                            parent.tableToolbar._showMsg(ret.msg, function() {
                                window.location.href = _Url;
                            });
                            return false;
                        }
                    },
                    error: function() {
                        parent.tableToolbar._showMsg(tableToolbar.errorMsg);
                        return false;
                    }
                });
            },
            _DeleteBus: function(_objID) { //删除汽车
                var _Url = '/OperaterCenter/OperaterLargeList.aspx?sl=' + LargePage.sl + '&type=' + LargePage.type + '&tourId=' + LargePage.tourId + "&typeId=9_3" + "&iframeId=" + LargePage.iframeId + "&m=" + new Date().getTime();
                $.newAjax({
                    type: "get",
                    url: '/OperaterCenter/OperaterLargeList.aspx?sl=' + LargePage.sl + '&action=deleteBus&planIdBus=' + _objID + '&tourid=' + LargePage.tourId,
                    cache: false,
                    dataType: 'json',
                    success: function(ret) {
                        if (ret.result == "1") {
                            parent.tableToolbar._showMsg(ret.msg, function() {
                                window.location.href = _Url;
                            });
                            return false;
                        }
                        else {
                            parent.tableToolbar._showMsg(ret.msg, function() {
                                window.location.href = _Url;
                            });
                            return false;
                        }
                    },
                    error: function() {
                        parent.tableToolbar._showMsg(tableToolbar.errorMsg);
                        return false;
                    }
                });
            },
            _OpenBoxy: function(title, iframeUrl, width, height, draggable) {
                parent.Boxy.iframeDialog({ title: title, iframeUrl: iframeUrl, width: width, height: height, draggable: draggable });
            },
            _SaveLarg: function(planId, tourId) { //飞机保存
                var _Url = '/OperaterCenter/OperaterLargeList.aspx?sl=' + LargePage.sl + '&type=' + LargePage.type + '&tourId=' + LargePage.tourId + "&typeId=9_1" + "&iframeId=" + LargePage.iframeId + "&m=" + new Date().getTime();
                $.newAjax({
                    type: "POST",
                    url: "/OperaterCenter/OperaterLargeList.aspx?action=saveAir&sl=" + LargePage.sl + "&tourId=" + tourId + "&planIdAir=" + planId,
                    cache: false,
                    data: $("#<%=largeform1.ClientID %>").serialize(),
                    dataType: "json",
                    success: function(ret) {
                        if (ret.result == "1") {
                            parent.tableToolbar._showMsg(ret.msg, function() {
                                window.location.href = _Url;
                            });
                        } else {
                            parent.tableToolbar._showMsg(ret.msg, function() {
                                $("#btnSave").text("保存").css("background-position", "0 0px").bind("click");
                                $("#btnSave").click(function() {
                                    LargePage._RemoveAttr("#h"); LargePage._RemoveAttr("#j"); LargePage._AddAttr("#g");
                                    $(this).css("background-position", "0 -62px").text("保存中...").unbind("click");
                                    var _AirID = '<%=Utils.GetQueryStringValue("planIdAir") %>';
                                    LargePage._SaveLarg(_AirID, LargePage.tourId);
                                });
                            });
                        }
                    },
                    error: function() {
                        parent.tableToolbar._showMsg(tableToolbar.errorMsg);
                        return false;
                    }
                });
            },
            _SaveTrain: function(planId, tourId) { //火车保存
                var _Url = '/OperaterCenter/OperaterLargeList.aspx?sl=' + LargePage.sl + '&type=' + LargePage.type + '&tourId=' + LargePage.tourId + "&typeId=9_2" + "&iframeId=" + LargePage.iframeId + "&m=" + new Date().getTime();
                $.newAjax({
                    type: "POST",
                    url: "/OperaterCenter/OperaterLargeList.aspx?action=saveTrain&sl=" + LargePage.sl + "&tourId=" + tourId + "&planIdTrainId=" + planId,
                    cache: false,
                    data: $("#<%=largeform1.ClientID %>").serialize(),
                    dataType: "json",
                    success: function(ret) {
                        if (ret.result == "1") {
                            parent.tableToolbar._showMsg(ret.msg, function() {
                                window.location.href = _Url;
                            });
                        } else {
                            parent.tableToolbar._showMsg(ret.msg, function() {
                                $("#btnSave1").text("保存").css("background-position", "0 0px").bind("click");
                                $("#btnSave1").click(function() {
                                    LargePage._RemoveAttr("#g"); LargePage._RemoveAttr("#j"); LargePage._AddAttr("#h");
                                    $(this).text("保存中...").css("background-position", "0 -62px").unbind("click");
                                    var _TrainID = '<%=Utils.GetQueryStringValue("planIdTrainId") %>';
                                    LargePage._SaveTrain(_TrainID, LargePage.tourId);
                                });
                            });
                        }
                    },
                    error: function() {
                        parent.tableToolbar._showMsg(tableToolbar.errorMsg);
                        return false;
                    }
                });
            },
            _SaveBus: function(planId, tourId) { //汽车保存
                var _Url = '/OperaterCenter/OperaterLargeList.aspx?sl=' + LargePage.sl + '&type=' + LargePage.type + '&tourId=' + LargePage.tourId + "&typeId=9_3" + "&iframeId=" + LargePage.iframeId + "&m=" + new Date().getTime();
                $.newAjax({
                    type: "POST",
                    url: "/OperaterCenter/OperaterLargeList.aspx?action=saveBus&sl=" + LargePage.sl + "&tourId=" + tourId + "&palnIdBus=" + planId,
                    cache: false,
                    data: $("#<%=largeform1.ClientID %>").serialize(),
                    dataType: "json",
                    success: function(ret) {
                        if (ret.result == "1") {
                            parent.tableToolbar._showMsg(ret.msg, function() {
                                window.location.href = _Url;
                            });
                        } else {
                            parent.tableToolbar._showMsg(ret.msg, function() {
                                $("#btnSave3").text("保存").css("background-position", "0 0px").bind("click");
                                $("#btnSave3").click(function() {
                                    LargePage._RemoveAttr("#h"); LargePage._RemoveAttr("#g"); LargePage._AddAttr("#j");
                                    $(this).text("保存中...").css("background-position", "0 -62px").unbind("click");
                                    var _BusID = '<%=Utils.GetQueryStringValue("palnIdBus") %>';
                                    LargePage._SaveBus(_BusID, LargePage.tourId);
                                });
                            });
                        }
                    },
                    error: function() {
                        parent.tableToolbar._showMsg(tableToolbar.errorMsg);
                        return false;
                    }
                });
            },
            _totalPrice: function() { //飞机舱位费计算
                var total = 0;
                $("input[type='text'][name='txtXiaoJi']").each(function() {
                    var peopleNums = $(this).parent().parent().find("input[type='text'][name='txtpeopleNums']").val();
                    var prices = $(this).parent().parent().find("input[type='text'][name='txtprices']").val();
                    var totalPrices = tableToolbar.calculate(peopleNums, prices, "*");
                    $(this).parent().parent().find("input[type='text'][name='txtXiaoJi']").val(totalPrices);
                    var Insurance = $(this).parent().parent().find("input[type='text'][name='txtInsurance']").val();
                    $(this).parent().parent().find("input[type='text'][name='txtXiaoJi']").val(tableToolbar.calculate(totalPrices, Insurance, "+"));
                    var totalPrices2 = $(this).parent().parent().find("input[type='text'][name='txtXiaoJi']").val();
                    var jiJianFei = $(this).parent().parent().find("input[type='text'][name='txtjiJianFei']").val();
                    $(this).parent().parent().find("input[type='text'][name='txtXiaoJi']").val(tableToolbar.calculate(totalPrices2, jiJianFei, "+"));
                    var totalPrices3 = $(this).parent().parent().find("input[type='text'][name='txtXiaoJi']").val();
                    var FujiaFei = $(this).parent().parent().find("input[type='text'][name='txtFujiaFei']").val();
                    $(this).parent().parent().find("input[type='text'][name='txtXiaoJi']").val(tableToolbar.calculate(totalPrices3, FujiaFei, "+"));
                    total = tableToolbar.calculate(total, $(this).parent().parent().find("input[type='text'][name='txtXiaoJi']").val(), "+");
                    $("#<%=txtCostAccount.ClientID %>").val(total);
                });
            },
            _totalPricesBus: function() {
                var total = 0;
                $("input[type='text'][name='txtPricesBus']").each(function() {
                    var prices = $(this).parent().parent().find("input[type='text'][name='txtPricesBus']").val();
                    var ticksNums = $(this).parent().parent().find("input[type='text'][name='txtTicketNumBus']").val();
                    var totalPrices = tableToolbar.calculate(prices, ticksNums, "*");

                    $(this).parent().parent().find("input[name='txtQiCheXiaoJi']").val(totalPrices);

                    total = tableToolbar.calculate(total, totalPrices, "+");
                });
                $("#<%=txtTotalPricesBus.ClientID %>").val(total);
            },
            _BIndBtn: function() {
                //单选按钮控制
                $("input[type=radio][name='radioLartype']").unbind("click");
                $("input[type=radio][name='radioLartype']").click(function() {
                    if ($(this).val() == "1") {
                        $("#g").css("display", "");
                        LargePage._AddAttr("#g");
                        $("#h").css("display", "none");
                        LargePage._RemoveAttr("#h");
                        $("#j").css("display", "none");
                        LargePage._RemoveAttr("#j");
                        $(this).attr("checked", "checked");
                    }
                    else if ($(this).val() == "2") {
                        $("#g").css("display", "none");
                        LargePage._RemoveAttr("#g");
                        $("#h").css("display", "");
                        LargePage._AddAttr("#h");
                        $("#j").css("display", "none");
                        LargePage._RemoveAttr("#j");
                        $(this).attr("checked", "checked");
                    } else {
                        $("#g").css("display", "none");
                        LargePage._RemoveAttr("#g");
                        $("#h").css("display", "none");
                        LargePage._RemoveAttr("#h");
                        $("#j").css("display", "");
                        LargePage._AddAttr("#j");
                        $(this).attr("checked", "checked");
                    }
                    parent.ConfigPage.SetWinHeight();
                });

                //飞机js方法
                //                $("input[type='text'][name='txtpeopleNums']").focus(function() { if ($(this).val() == "人数") { $(this).val(""); } }).blur(function() { if ($(this).val() == "") $(this).val("人数"); });

                //$("input[type='text'][name='txtpeopleNums']").live("focus", function() { if ($(this).val() == "人数") { $(this).val(""); } }).live("blur", function() { if ($(this).val() == "") $(this).val("人数"); });

                //$("input[type='text'][name='txtprices']").live("focus", function() { if ($(this).val() == "单价") { $(this).val(""); } }).live("blur", function() { if ($(this).val() == "") $(this).val("单价") });
                //$("input[type='text'][name='txtInsurance']").live("focus", function() { if ($(this).val() == "保险") { $(this).val(""); } }).live("blur", function() { if ($(this).val() == "") $(this).val("保险") });
                //$("input[type=text][name='txtjiJianFei']").live("focus", function() { if ($(this).val() == "机建费") { $(this).val(""); } }).live("blur", function() { if ($(this).val() == "") $(this).val("机建费") });
                //$("input[type='text'][name='txtFujiaFei']").live("focus", function() { if ($(this).val() == "附加费") { $(this).val(""); } }).live("blur", function() { if ($(this).val() == "") $(this).val("附加费") });
                //$("input[type='text'][name='txtZekou']").live("focus", function() { if ($(this).val() == "折扣") { $(this).val(""); } }).live("blur", function() { if ($(this).val() == "") $(this).val("折扣") });
                //$("input[type='text'][name='txtstartHours']").live("focus", function() { if ($(this).val() == "点时间") { $(this).val(""); } }).live("blur", function() { if ($(this).val() == "") $(this).val("点时间") });
                //$("input[type='text'][name='txtXiaoJi']").live("focus", function() { if ($(this).val() == "小计") { $(this).val(""); } }).live("blur", function() { if ($(this).val() == "") $(this).val("小计") });

                //预付申请
                $("#Airlist").find("a[data-class='Prepaid']").unbind("click");
                $("#Airlist").find("a[data-class='Prepaid']").click(function() {
                    var planId = $(this).attr("data-ID");
                    var soucesName = $(this).attr("data-soucesname");
                    LargePage._OpenBoxy("预付申请", '/OperaterCenter/PrepaidAppliaction.aspx?PlanId=' + planId + '&type=' + LargePage.type + '&sl=' + LargePage.sl + '&tourId=' + LargePage.tourId + '&souceName=' + escape(soucesName), "650px", "550px", true);
                    return false;
                });

                //删除
                $("#Airlist").find("a[data-class='deleteAir']").unbind("click");
                $("#Airlist").find("a[data-class='deleteAir']").click(function() {
                    var newThis = $(this);
                    parent.tableToolbar.ShowConfirmMsg("确定删除此条数据吗?", function() {
                        var planId = newThis.find("img").attr("data-ID");
                        if (planId) {
                            LargePage._DeleteLarg(planId);
                        }
                    });
                    return false;
                });

                //修改
                $("#Airlist").find("a[data-class='updateAir']").unbind("click");
                $("#Airlist").find("a[data-class='updateAir']").click(function() {
                    var planIdAir = $(this).find("img").attr("data-ID");
                    if (planIdAir) {
                        window.location.href = '/OperaterCenter/OperaterLargeList.aspx?type=' + LargePage.type + '&sl=' + LargePage.sl + '&tourId=' + LargePage.tourId + '&action=updateAir&planIdAir=' + planIdAir + "&iframeId=" + LargePage.iframeId + "&typeId=9_1";
                    }
                    return false;
                });

                //查看 
                $("#Airlist").find("a[data-class='showAir']").unbind("click");
                $("#Airlist").find("a[data-class='showAir']").click(function() {
                    var planIdAir = $(this).find("img").attr("data-ID");
                    if (planIdAir) {
                        window.location.href = '/OperaterCenter/OperaterLargeList.aspx?type=' + LargePage.type + '&sl=' + LargePage.sl + '&tourId=' + LargePage.tourId + '&action=updateAir&planIdAir=' + planIdAir + "&iframeId=" + LargePage.iframeId + "&typeId=9_1" + "&show=1";
                    }
                    return false;
                });

                $("#btnSave").unbind("click").text("保存").css("background-position", "0 0px");
                $("#btnSave").click(function() {
                    LargePage._RemoveAttr("#h"); LargePage._RemoveAttr("#j"); LargePage._AddAttr("#g");
                    $("#tabAirFilght").find("input[data-class='show']").each(function() {
                        var _self = $(this);
                        if ($.trim(_self.val()) == _self.attr("data-title")) {
                            _self.val("");
                        }
                    })

                    if (!ValiDatorForm.validator($("#<%=largeform1.ClientID %>").get(0), "parent")) {
                        return false;
                    } else {
                        $(this).text("保存中...").css("background-position", "0 -62px").unbind("click");
                        var planId = '<%=Utils.GetQueryStringValue("planIdAir") %>';
                        LargePage._SaveLarg(planId, LargePage.tourId);
                    }
                });

                $("input[type='text'][name='txtpeopleNums']").live("blur", function() {
                    LargePage._totalPrice();
                });
                $("input[type='text'][name='txtprices']").live("blur", function() {
                    LargePage._totalPrice();
                });
                $("input[type='text'][name='txtInsurance']").live("blur", function() {
                    LargePage._totalPrice();
                });
                $("input[type='text'][name='txtjiJianFei']").live("blur", function() {
                    LargePage._totalPrice();
                });
                $("input[type='text'][name='txtFujiaFei']").live("blur", function() {
                    LargePage._totalPrice();
                });


                //火车js
                //$("input[type='text'][name='txtStartHoursTrain']").live("focus", function() { if ($(this).val() == "点时间") { $(this).val(""); } }).live("blur", function() { if ($(this).val() == "") $(this).val("点时间") });
                //$("input[type='text'][name='txtStartPlaceTrain']").live("focus", function() { if ($(this).val() == "出发地") { $(this).val(""); } }).live("blur", function() { if ($(this).val() == "") $(this).val("出发地") });
                //$("input[type='text'][name='txtendPlaceTrain']").live("focus", function() { if ($(this).val() == "目的地") { $(this).val(""); } }).live("blur", function() { if ($(this).val() == "") $(this).val("目的地") });
                //$("input[type='text'][name='txtcoachNumTrain']").live("focus", function() { if ($(this).val() == "车次") { $(this).val(""); } }).live("blur", function() { if ($(this).val() == "") $(this).val("车次") });
                //$("input[type='text'][name='txtseatStandTrain']").live("focus", function() { if ($(this).val() == "坐位标准") { $(this).val(""); } }).live("blur", function() { if ($(this).val() == "") $(this).val("坐位标准") });
                //$("input[type='text'][name='txtpayNumTrain']").live("focus", function() { if ($(this).val() == "张数") { $(this).val(""); } }).live("blur", function() { if ($(this).val() == "") $(this).val("张数") });
                //$("input[type='text'][name='txtFreeNumTrain']").live("focus", function() { if ($(this).val() == "免费数量") { $(this).val(""); } }).live("blur", function() { if ($(this).val() == "") $(this).val("免费数量") });

                $("#tabTrainlist").find("a[data-class='Prepaid']").unbind("click");
                $("#tabTrainlist").find("a[data-class='Prepaid']").click(function() {
                    var planId = $(this).attr("data-ID");
                    var soucesName = $(this).attr("data-soucesname");
                    LargePage._OpenBoxy("预付申请", '/OperaterCenter/PrepaidAppliaction.aspx?PlanId=' + planId + '&type=' + LargePage.type + '&sl=' + LargePage.sl + '&tourId=' + LargePage.tourId + '&souceName=' + escape(soucesName), "650px", "550px", true);
                    return false;
                });

                $("#tabTrainlist").find("a[data-class='deleteTrain']").unbind("click");
                $("#tabTrainlist").find("a[data-class='deleteTrain']").click(function() {
                    var newThis = $(this);
                    parent.tableToolbar.ShowConfirmMsg("确定删除此条数据吗?", function() {
                        var planId = newThis.find("img").attr("data-ID");
                        if (planId) {
                            LargePage._DeleteTrain(planId);
                        }
                    });
                    return false;
                });

                $("#tabTrainlist").find("a[data-class='updateTrain']").unbind("click");
                $("#tabTrainlist").find("a[data-class='updateTrain']").click(function() {
                    var planId = $(this).find("img").attr("data-ID");
                    if (planId) {
                        window.location.href = '/OperaterCenter/OperaterLargeList.aspx?type=' + LargePage.type + '&sl=' + LargePage.sl + '&tourId=' + LargePage.tourId + '&action=updateTrain&planIdTrainId=' + planId + "&iframeId=" + LargePage.iframeId + "&typeId=9_2";
                    }
                    return false;
                });

                $("#tabTrainlist").find("a[data-class='showTrain']").unbind("click");
                $("#tabTrainlist").find("a[data-class='showTrain']").click(function() {
                    var planId = $(this).find("img").attr("data-ID");
                    if (planId) {
                        window.location.href = '/OperaterCenter/OperaterLargeList.aspx?type=' + LargePage.type + '&sl=' + LargePage.sl + '&tourId=' + LargePage.tourId + '&action=updateTrain&planIdTrainId=' + planId + "&iframeId=" + LargePage.iframeId + "&typeId=9_2" + "&show=2";
                    }
                    return false;
                });

                $("#btnSave1").unbind("click").text("保存").css("background-position", "0 0px");
                $("#btnSave1").click(function() {
                    LargePage._RemoveAttr("#g"); LargePage._RemoveAttr("#j"); LargePage._AddAttr("#h");

                    $("#tabcoachTrain").find("input[data-class='show']").each(function() {
                        if ($.trim($(this).val()) == $(this).attr("data-title")) {
                            $(this).val("");
                        }
                    });

                    if (!ValiDatorForm.validator($("#<%=largeform1.ClientID %>").get(0), "parent")) {
                        return false;
                    } else {
                        $(this).text("保存中...").css("background-position", "0 -62px").unbind("click");
                        var planId = '<%=Utils.GetQueryStringValue("planIdTrainId") %>';
                        LargePage._SaveTrain(planId, LargePage.tourId);
                    }
                });

                //汽车js
                //if ($.trim($("input[type='text'][name='txtStartTimeBus']:eq(0)").val()) == "") { $("input[type='text'][name='txtStartTimeBus']:eq(0)").val("出发日期") };
                //$("input[type='text'][name='txtStartTimeBus']:eq(0)").live("focus", function() { if ($.trim($(this).val()) == "出发日期") { $(this).val(""); } }).live("blur", function() { if ($.trim($(this).val()) == "") $(this).val("出发日期") });
                //$("input[type='text'][name='txtStartHoursBus']").live("focus", function() { if ($.trim($(this).val()) == "点时间") { $(this).val(""); } }).live("blur", function() { if ($.trim($(this).val()) == "") $(this).val("点时间") });
                //$("input[type='text'][name='txtCarNumBus']").live("focus", function() { if ($.trim($(this).val()) == "车牌号") { $(this).val(""); } }).live("blur", function() { if ($.trim($(this).val()) == "") $(this).val("车牌号") });
                //$("input[type='text'][name='txtStartPlaceBus']").live("focus", function() { if ($.trim($(this).val()) == "出发地") { $(this).val(""); } }).live("blur", function() { if ($.trim($(this).val()) == "") $(this).val("出发地") });
                //$("input[type='text'][name='txtUnderPlaceBus']").live("focus", function() { if ($.trim($(this).val()) == "目的地") { $(this).val(""); } }).live("blur", function() { if ($.trim($(this).val()) == "") $(this).val("目的地") });
                //$("input[type='text'][name='txtPricesBus']").live("focus", function() { if ($.trim($(this).val()) == "价格") { $(this).val(""); } }).live("blur", function() { if ($.trim($(this).val()) == "") $(this).val("价格") });
                //$("input[type='text'][name='txtTicketNumBus']").live("focus", function() { if ($.trim($(this).val()) == "张数") { $(this).val(""); } }).live("blur", function() { if ($.trim($(this).val()) == "") $(this).val("张数 ") });

                //预付申请 删除 修改
                $("#tabPlanBus").find("a[data-class='Prepaid']").unbind("click");
                $("#tabPlanBus").find("a[data-class='Prepaid']").click(function() {
                    var planId = $(this).attr("data-id");
                    var soucesName = $(this).attr("data-soucesname");
                    LargePage._OpenBoxy("预付申请", '/OperaterCenter/PrepaidAppliaction.aspx?PlanId=' + planId + '&type=' + LargePage.type + '&sl=' + LargePage.sl + '&tourId=' + LargePage.tourId + '&souceName=' + escape(soucesName), "650px", "550px", true);
                    return false;
                });

                $("#tabPlanBus").find("[data-class='deletebus']").unbind("click");
                $("#tabPlanBus").find("[data-class='deletebus']").click(function() {
                    var newThis = $(this);
                    parent.tableToolbar.ShowConfirmMsg("确定删除此条数据吗?", function() {
                        var planId = newThis.find("img").attr("data-id");
                        if (planId) {
                            LargePage._DeleteBus(planId);
                        }
                    });
                    return false;
                });

                $("#tabPlanBus").find("a[data-class='updatebus']").unbind("click");
                $("#tabPlanBus").find("a[data-class='updatebus']").click(function() {
                    var planId = $(this).find("img").attr("data-id");
                    if (planId) {
                        window.location.href = '/OperaterCenter/OperaterLargeList.aspx?type=' + LargePage.type + '&sl=' + LargePage.sl + '&tourId=' + LargePage.tourId + '&action=updateBus&palnIdBus=' + planId + "&iframeId=" + LargePage.iframeId + "&typeId=9_3";
                    }
                    return false;
                });

                $("#tabPlanBus").find("a[data-class='showbus']").unbind("click");
                $("#tabPlanBus").find("a[data-class='showbus']").click(function() {
                    var planId = $(this).find("img").attr("data-id");
                    if (planId) {
                        window.location.href = '/OperaterCenter/OperaterLargeList.aspx?type=' + LargePage.type + '&sl=' + LargePage.sl + '&tourId=' + LargePage.tourId + '&action=updateBus&palnIdBus=' + planId + "&iframeId=" + LargePage.iframeId + "&typeId=9_3" + "&show=3";
                    }
                    return false;
                });


                $("#btnSave3").unbind("click").text("保存").css("background-position", "0 0px");
                $("#btnSave3").click(function() {
                    LargePage._RemoveAttr("#h"); LargePage._RemoveAttr("#g"); LargePage._AddAttr("#j");

                    $("#tabCoachBus").find("input[data-class='show']").each(function() {
                        if ($.trim($(this).val()) == $(this).attr("data-title")) {
                            $(this).val("");
                        }
                    });

                    if (!ValiDatorForm.validator($("#<%=largeform1.ClientID %>").get(0), "parent")) {
                        return false;
                    } else {
                        $(this).text("保存中...").css("background-position", "0 -62px").unbind("click");
                        var planId = '<%=Utils.GetQueryStringValue("palnIdBus") %>';
                        LargePage._SaveBus(planId, LargePage.tourId);
                    }
                    return false;
                });


                $("input[name='txtPricesBus']").live("change", function() { LargePage._totalPricesBus(); });
                $("input[name='txtTicketNumBus']").live("change", function() { LargePage._totalPricesBus(); });
                $("input[name='txtQiCheXiaoJi']").live("change", function() { LargePage._totalPricesBus(); });

                $("input[name='txtpayNumTrain']").live("change", function() { LargePage._totalHuoCheJinE(); });
                $("input[name='txtHuoCheDanJia']").live("change", function() { LargePage._totalHuoCheJinE(); });
                $("input[name='txtHuoCheXiaoJi']").live("change", function() { LargePage._totalHuoCheJinE(); });
            },
            _BindBt: function(obj) {
                $(obj).find("input[data-class='show']").unbind().bt({
                    contentSelector: function() {
                        return $(this).attr("data-title");
                    },
                    trigger: 'focus',
                    positions: ['top'],
                    fill: '#fff',
                    strokeStyle: '#81b7d3',
                    strokeWidth: 2,
                    noShadowOpts: { strokeStyle: "#81b7d3", strokeWidth: 2 },
                    spikeLength: 10,
                    spikeGirth: 10,
                    width: 50,
                    centerPointY: 4,
                    cornerRadius: 4,
                    shadow: true,
                    shadowColor: 'rgba(0,0,0,.5)',
                    cssStyles: { color: '#00387E', "text-align": "center" }
                });
            },
            _PageInit: function() {
                //全局计调修改，按钮状态控制  9_1 飞机 9_2 火车 9_3 汽车
                var typeId = '<%=EyouSoft.Common.Utils.GetQueryStringValue("typeId") %>';
                if (typeId != null && typeId != "") {
                    var Id = typeId.split('_');
                    //飞机
                    if (Id[1] == "1") {
                        $("input[type='radio'][name='radioLartype'][value='1']").attr("checked", "checked");
                        $("input[type='radio'][name='radioLartype'][value='2']").removeAttr("checked");
                        $("input[type='radio'][name='radioLartype'][value='3']").removeAttr("checked");
                        $("#g").css("display", ""); $("#h").css("display", "none"); $("#j").css("display", "none");
                        LargePage._RemoveAttr("#h"); LargePage._RemoveAttr("#j"); LargePage._AddAttr("#g");
                    } else if (Id[1] == "2") {  //火车                        
                        $("input[type='radio'][name='radioLartype'][value='2']").attr("checked", "checked");
                        $("input[type='radio'][name='radioLartype'][value='1']").removeAttr("checked");
                        $("input[type='radio'][name='radioLartype'][value='3']").removeAttr("checked");
                        $("#g").css("display", "none"); $("#h").css("display", ""); $("#j").css("display", "none");
                        LargePage._RemoveAttr("#g"); LargePage._RemoveAttr("#j"); LargePage._AddAttr("#h");
                    } else {   //汽车                       
                        $("input[type='radio'][name='radioLartype'][value='3']").attr("checked", "checked");
                        $("input[type='radio'][name='radioLartype'][value='2']").removeAttr("checked");
                        $("input[type='radio'][name='radioLartype'][value='1']").removeAttr("checked");
                        $("#g").css("display", "none"); $("#h").css("display", "none"); $("#j").css("display", "");
                        LargePage._RemoveAttr("#h"); LargePage._RemoveAttr("#g"); LargePage._AddAttr("#j");
                    }
                    parent.ConfigPage.SetWinHeight();
                }

                LargePage._BIndBtn();
                LargePage._totalPrice();



                $("#tabAirFilght").autoAdd({ addCallBack: function(tr) { parent.ConfigPage.SetWinHeight(); LargePage._AddAttr("#tabAirFilght"); LargePage._BindBt(tr); }, delCallBack: parent.ConfigPage.SetWinHeight() });
                $("#tabCoachBus").autoAdd({ addCallBack: function(tr) { parent.ConfigPage.SetWinHeight(), LargePage._AddAttr("#tabCoachBus"); LargePage._BindBt(tr) }, delCallBack: parent.ConfigPage.SetWinHeight() });
                $("#tabcoachTrain").autoAdd({ addCallBack: function(tr) { parent.ConfigPage.SetWinHeight(), LargePage._AddAttr("#tabcoachTrain"); LargePage._BindBt(tr) }, delCallBack: parent.ConfigPage.SetWinHeight() });

                LargePage._BindBt($("#tabAirFilght").find("tr"));
                LargePage._BindBt($("#tabCoachBus").find("tr"));
                LargePage._BindBt($("#tabcoachTrain").find("tr"));

                if ('<%=Utils.GetQueryStringValue("show") %>' == "1") {
                    $("#ul_Btn_listAir").parent("div").hide();
                }
                if ('<%=Utils.GetQueryStringValue("show") %>' == "2") {
                    $("#ul_Btn_listTrain").parent("div").hide();
                }
                if ('<%=Utils.GetQueryStringValue("show") %>' == "3") {
                    $("#ul_Btn_listBus").parent("div").hide();
                }

            },
            _AjaxContectInfo: function(obj) {
                if (obj) {
                    $("#<%=supplierControl1.ClientValue %>").val(obj.id);
                    $("#<%=supplierControl1.ClientText %>").val(obj.name);
                    $("#<%=txtContentName.ClientID %>").val(obj.contactname);
                    $("#<%=txtContentPhone.ClientID %>").val(obj.contacttel);
                    $("#<%=txtContentFax.ClientID %>").val(obj.contactfax);
                }
            },
            _AjaxContectInfoT: function(obj) {
                $("#<%=supplierControl2.ClientValue %>").val(obj.id);
                $("#<%=supplierControl2.ClientText %>").val(obj.name);
                $("#<%=txtContentName1.ClientID %>").val(obj.contactname);
                $("#<%=txtContentPhone1.ClientID %>").val(obj.contacttel);
                $("#<%=txtContentFax1.ClientID %>").val(obj.contactfax);
            },
            _AjaxContectInfoB: function(obj) {
                $("#<%=supplierControl3.ClientValue %>").val(obj.id);
                $("#<%=supplierControl3.ClientText %>").val(obj.name);
                $("#<%=txtContentName2.ClientID %>").val(obj.contactname);
                $("#<%=txtContentPhone2.ClientID %>").val(obj.contacttel);
                $("#<%=txtContentFax2.ClientID %>").val(obj.contactfax);
            },
            //计算火车价格
            _totalHuoCheJinE: function() {
                var _t = 0;
                $("input[name='txtHuoCheDanJia']").each(function() {
                    var _$danJia = $(this);
                    var _$tr = _$danJia.closest("tr");
                    var _$zhangShu = _$tr.find("input[name='txtpayNumTrain']");
                    var _$xiaoJi = _$tr.find("input[name='txtHuoCheXiaoJi']");

                    var _xiaoJi = tableToolbar.calculate(_$danJia.val(), _$zhangShu.val(), "*");
                    _$xiaoJi.val(_xiaoJi);
                    _t = tableToolbar.calculate(_t, _xiaoJi, "+");
                });

                $("#<%=txtTotalPrices1.ClientID %>").val(_t);
            }
        }

        $(function() {
            LargePage._PageInit();
            parent.ConfigPage.SetWinHeight();
            LargePage._InitSupAttr();
        });
    </script>

</body>
</html>

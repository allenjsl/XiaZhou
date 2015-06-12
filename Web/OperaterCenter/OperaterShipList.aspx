<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OperaterShipList.aspx.cs"
    Inherits="Web.OperaterCenter.OperaterShipList" EnableEventValidation="true" %>

<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register TagName="SupplierControl" TagPrefix="uc6" Src="~/UserControl/SupplierControl.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>

    <script type="text/javascript" src="/Js/jquery-1.4.4.js"></script>

    <script src="/Js/datepicker/WdatePicker.js" type="text/javascript"></script>

    <script src="/Js/ValiDatorForm.js" type="text/javascript"></script>

    <link href="/css/style.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="/Js/table-toolbar.js"></script>

    <script type="text/javascript" src="/Js/jquery.blockUI.js"></script>

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
    <form id="formShip" runat="server">
    <div class="jidiao-r">
        <div id="con_faq_7">
            <table width="100%" cellpadding="0" cellspacing="0" class="jd-table01" height="25">
                <tr>
                    <th width="15%" align="right" class="border-l">
                        类型：
                    </th>
                    <td>
                        <input name="radshipType" id="r1" type="radio" value="1" checked="checked" /><label
                            for="r1">涉外游船</label>
                        <input name="radshipType" id="r2" type="radio" value="2" /><label for="r2">国内游船</label>
                    </td>
                </tr>
            </table>
            <div id="c" style="display: block">
                <table width="100%" cellpadding="0" cellspacing="0" class="jd-table01 line-b">
                    <tr>
                        <th align="right" class="border-l">
                            <span class="fontred">*</span>游船公司：
                        </th>
                        <td width="40%">
                            <uc6:SupplierControl ID="SupplierControl1" runat="server" CallBack="ShipPage._AjaxContectInfo"
                                SupplierType="涉外游轮" IsMust="false" />
                        </td>
                        <th width="15%" align="right">
                            <span class="addtableT">联系人：</span>
                        </th>
                        <td width="30%">
                            <asp:TextBox ID="txtContectName" runat="server" CssClass="inputtext formsize140"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th align="right" class="border-l">
                            联系电话：
                        </th>
                        <td>
                            <asp:TextBox ID="txtContectPhone" runat="server" CssClass="inputtext formsize140"
                                isvalid="1" valid="isTel" errmsg="*电话格式不正确!"></asp:TextBox>
                        </td>
                        <th align="right">
                            <span class="addtableT">联系传真：</span>
                        </th>
                        <td>
                            <asp:TextBox ID="txtContectFax" runat="server" CssClass="inputtext formsize140"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th align="right" class="border-l">
                            船名：
                        </th>
                        <td>
                            <select name="ddlShipNameList" class="inputselect">
                                <%=ShipNameListF.ToString()%>
                            </select>
                        </td>
                        <th align="right">
                            船载电话<span class="addtableT">：</span>
                        </th>
                        <td>
                            <asp:TextBox ID="txtShipPhone" runat="server" CssClass="inputtext formsize140"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th align="right" class="border-l">
                            登船日期：
                        </th>
                        <td>
                            <asp:TextBox ID="txtBoardDate" runat="server" CssClass="inputtext formsize80"></asp:TextBox>
                            <a href="javascript:" class="timesicon" onclick="WdatePicker({el:'<%=txtBoardDate.ClientID %>',minDate: '%y-%M-#{%d' })">
                            </a>
                            <asp:TextBox ID="txtBoardTime" runat="server" CssClass="inputtext formsize40"></asp:TextBox>
                        </td>
                        <th width="15%" align="right">
                            <span class="fontred">*</span><span class="addtableT">登船码头：</span>
                        </th>
                        <td>
                            <asp:TextBox ID="txtBoardWharif" runat="server" CssClass="inputtext formsize140"
                                isvalid="1" valid="required" errmsg="*请输入登船码头!"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th align="right" class="border-l">
                            登船号：
                        </th>
                        <td>
                            <asp:TextBox ID="txtBoardCode" runat="server" CssClass="inputtext formsize120"></asp:TextBox>
                        </td>
                        <th align="right">
                            <span class="addtableT"><span class="fontred">*</span>航线：</span>
                        </th>
                        <td>
                            <asp:TextBox ID="txtRoute" runat="server" CssClass="inputtext formsize140" valid="required"
                                isvalid="1" errmsg="*请输入航线!"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th align="right" class="border-l">
                            停靠景点：
                        </th>
                        <td colspan="3">
                            <asp:TextBox ID="txtStopAttr" runat="server" TextMode="MultiLine" CssClass="inputtext formsize600"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th align="right" class="border-l">
                            <span class="fontred">*</span>价格组成：
                        </th>
                        <td colspan="3">
                            <table width="100%" id="tabPrices" border="0" cellspacing="0" cellpadding="0" class="noborder">
                                <asp:PlaceHolder ID="tabViewPrices" runat="server">
                                    <tr class="tempRow">
                                        <td width="84%">
                                            <select class="inputselect" name="selRoomTypeList">
                                                <%=GetShipRoomType("") %>
                                            </select>
                                            <select class="inputselect" name="selCrowdType">
                                                <%=GetShipCrowdType("")%>
                                            </select>
                                            人数：
                                            <input type="text" name="txtAdultNum" class="inputtext formsize50" valid="required|isMoney"
                                                errmsg="*请输入人数!|请填写正确的人数!" isvalid="1" />
                                            单价：<input type="text" name="txtAdultPrice" class="inputtext formsize50" valid="required|isMoney"
                                                errmsg="*请输入单价!|单价格式不正确!" isvalid="1" />&nbsp;&nbsp;
                                            小计：
                                            <input type="text" name="txtTotalPrice" class="inputtext formsize50" valid="required|isMoney"
                                                errmsg="*请输入小计费用!|小计费用格式不正确!" isvalid="1" />
                                            备注：<input type="text" name="txtSheWaiBeiZhu" class="inputtext" maxlength="255" />
                                        </td>
                                        <td width="16%" align="right" valign="bottom">
                                            <a href="javascript:void(0);" class="addbtn">
                                                <img src="/images/addimg.gif" width="48" height="20" /></a> <a href="javascript:void(0);"
                                                    class="delbtn">
                                                    <img src="/images/delimg.gif" width="48" height="20" /></a>
                                        </td>
                                    </tr>
                                </asp:PlaceHolder>
                                <asp:Repeater ID="repPricesList" runat="server">
                                    <ItemTemplate>
                                        <tr class="tempRow">
                                            <td width="84%">
                                                <select class="inputselect" name="selRoomTypeList">
                                                    <%#GetShipRoomType(((int)Eval("RoomType")).ToString())%>
                                                </select>
                                                <select class="inputselect" name="selCrowdType">
                                                    <%# GetShipCrowdType(((int)Eval("CrowdType")).ToString())%>
                                                </select>
                                                人数：
                                                <input type="text" name="txtAdultNum" class="inputtext formsize50" value="<%# Eval("DNum","{0:F2}") %>"
                                                    valid="required|isMoney" errmsg="*请输入人数!|请填写正确的人数!" isvalid="1" />
                                                单价：
                                                <input type="text" name="txtAdultPrice" class="inputtext formsize50" value="<%# EyouSoft.Common.Utils.FilterEndOfTheZeroDecimal(EyouSoft.Common.Utils.GetDecimal(Eval("AdultPrice").ToString()))  %>"
                                                    valid="required|isMoney" errmsg="*请输入单价!|单价格式不正确！"
                                                    isvalid="1" />
                                                小计：
                                                <input type="text" name="txtTotalPrice" class="inputtext formsize50" value="<%# EyouSoft.Common.Utils.FilterEndOfTheZeroDecimal(EyouSoft.Common.Utils.GetDecimal(Eval("SumPrice").ToString())) %>"
                                                    valid="required|isMoney" min="1" errmsg="*请输入小计费用!|小计费用格式不正确!"
                                                    isvalid="1" />
                                                备注：<input type="text" name="txtSheWaiBeiZhu" class="inputtext" maxlength="255" value="<%#Eval("BeiZhu") %>" />
                                            </td>
                                            <td width="16%" align="right" valign="bottom">
                                                <a href="javascript:void(0);" class="addbtn">
                                                    <img src="/images/addimg.gif" width="48" height="20" /></a> <a href="javascript:void(0);"
                                                        class="delbtn">
                                                        <img src="/images/delimg.gif" width="48" height="20" /></a>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <th align="right" class="border-l">
                            <span class="border-l"><span class="fontred">*</span>结算费用</span>：
                        </th>
                        <td align="left">
                            <asp:TextBox ID="txtTotalPrices" runat="server" CssClass="inputtext formsize80" valid="required|isMoney"
                                errmsg="*请输入结算费用!|*结算费用格式不正确!"  isvalid="1"></asp:TextBox>
                            元
                        </td>
                        <th align="right">
                            
                        </th>
                        <td align="left">
                            
                        </td>
                    </tr>
                    <tr>
                        <th align="right" class="border-l">
                            游客资料：
                        </th>
                        <td colspan="3">
                            <asp:TextBox ID="txtCustomerInfo" runat="server" TextMode="MultiLine" CssClass="inputtext formsize600">                 
                            </asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th align="right" class="border-l">
                            费用明细：
                        </th>
                        <td colspan="3">
                            <asp:TextBox ID="txtCostDetail" runat="server" TextMode="MultiLine" CssClass="inputtext formsize600"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th align="right" class="border-l">
                            导游需知：
                        </th>
                        <td colspan="3">
                            <asp:TextBox ID="txtGuidNOtes" runat="server" TextMode="MultiLine" CssClass="inputtext formsize600">
登船日期：
船电话：
登船号：
登船码头：
                            </asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th align="right" class="border-l">
                            其它备注：
                        </th>
                        <td colspan="3">
                            <asp:TextBox ID="txtOtherRemarks" runat="server" TextMode="MultiLine" CssClass="inputtext formsize600"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th align="right">
                            <span class="fontred">*</span>支付方式：
                        </th>
                        <td align="left">
                            <select name="SelPanyMent" class="inputselect">
                                <%=UtilsCommons.GetOperaterPanyMentList(PanyMent)%>
                            </select>
                        </td>
                        <th align="right">
                            <span class="fontred">*</span>返利：
                        </th>
                        <td align="left">
                            <asp:DropDownList CssClass="inputselect" ID="ddlprofit1" runat="server">
                                <asp:ListItem Value="0">有</asp:ListItem>
                                <asp:ListItem Value="1">无</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <th align="right" class="border-l">
                            <span class="fontred">*</span>状态：
                        </th>
                        <td colspan="3" align="left">
                            <select name="SelStatus" class="inputselect">
                                <%= UtilsCommons.GetOperaterStatusList(Status)%>
                            </select>
                        </td>
                    </tr>
                </table>
                <asp:PlaceHolder ID="phdShowListFrist" runat="server">
                    <h2>
                        <p>
                            已安排涉外游轮</p>
                    </h2>
                    <table id="tabForeignShip" width="100%" border="0" cellspacing="0" cellpadding="0"
                        class="jd-table01" style="border-bottom: 1px solid #A9D7EC;">
                        <tr>
                            <th align="center" class="border-l">
                                游船公司
                            </th>
                            <th align="center">
                                船名
                            </th>
                            <th align="center">
                                人数
                            </th>
                            <th align="center">
                                费用明细
                            </th>
                            <th align="right">
                                结算费用
                            </th>
                            <th align="center">
                                支付方式
                            </th>
                            <th align="center">
                                状态
                            </th>
                            <th align="center">
                                确认单
                            </th>
                            <th align="center">
                                操作
                            </th>
                        </tr>
                        <asp:Repeater ID="repShipAboradList" runat="server">
                            <ItemTemplate>
                                <tr>
                                    <td align="center" class="border-l">
                                        <%# Eval("SourceName")%>
                                    </td>
                                    <td align="center">
                                        <%# Eval("ShipName")%>
                                    </td>
                                    <td align="center">
                                        <b class="fontblue">
                                            <%# Eval("DNum","{0:F2}")%></b>
                                    </td>
                                    <td align="center">
                                        <span title="<%# Eval("CostDetail") %>">
                                            <%# EyouSoft.Common.Utils.GetText2( Eval("CostDetail").ToString(),30,true) %></span>
                                    </td>
                                    <td align="right">
                                        <%#UtilsCommons.GetMoneyString(Convert.ToDecimal(Eval("Confirmation")),ProviderToMoney)%>
                                        <a href="javascript:void(0);" data-class="Prepaid" title="预付申请" data-id="<%# Eval("PlanId") %>"
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
                                        <a href='<%# querenforeirnUrl %>?planId=<%# Eval("PlanId") %>' target="_blank">
                                            <img src="/images/y-kehuqueding.gif" alt="" border="0" /></a>
                                    </td>
                                    <td align="center">
                                        <%if (ListPower)
                                          { %>
                                        <a href="javascript:" data-class="updateShip">
                                            <img src="/images/y-delupdateicon.gif" alt="" data-id="<%# Eval("PlanId") %>" />
                                            修改</a> <a href="javascript:" data-class="deleteShip">
                                                <img src="/images/y-delicon.gif" alt="" data-id="<%# Eval("PlanId") %>" />
                                                删除</a>
                                        <%}
                                          else
                                          { %>
                                        <a href="javascript:" data-class="showShip">
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
                        <ul id="ul_btn_list">
                            <li class="cun-cy"><a href="javascript:void(0);" id="btnSave">保存</a> </li>
                        </ul>
                    </div>
                </asp:PlaceHolder>
            </div>
            <div id="d" style="display: none">
                <table width="100%" cellpadding="0" cellspacing="0" class="jd-table01 line-b">
                    <tbody>
                        <tr>
                            <th width="15%" align="right" class="border-l">
                                <span class="fontred">*</span>游船公司：
                            </th>
                            <td width="40%">
                                <uc6:SupplierControl ID="SupplierControl2" IsMust="false" runat="server" CallBack="ShipPage._AjaxContectCInfo"
                                    SupplierType="国内游轮" />
                            </td>
                            <th width="15%" align="right">
                                <span class="addtableT">联系人：</span>
                            </th>
                            <td width="30%">
                                <asp:TextBox ID="txtContectNameC" runat="server" CssClass="inputtext formsize80"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <th align="right" class="border-l">
                                联系电话：
                            </th>
                            <td>
                                <asp:TextBox ID="txtContectPhoneC" runat="server" CssClass="inputtext formsize140"
                                    isvalid="1" valid="isTel" errmsg="*电话格式不正确!"></asp:TextBox>
                            </td>
                            <th align="right">
                                <span class="addtableT">联系传真：</span>
                            </th>
                            <td>
                                <asp:TextBox ID="txtContectFaxC" runat="server" CssClass="inputtext formsize140"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <th align="right">
                                <span class="addtableT">船名：</span>
                            </th>
                            <td>
                                <select name="SelShipNameC" class="inputselect">
                                    <%=ShipNameListC.ToString()%>
                                </select>
                            </td>
                            <th width="15%" align="right">
                                <span class="fontred">*</span>线路：
                            </th>
                            <td>
                                <asp:TextBox ID="txtRouteC" runat="server" CssClass="inputtext formsize140" valid="required"
                                    errmsg="*请输入线路!" isvalid="1"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <th align="right" class="border-l">
                                登船日期：
                            </th>
                            <td>
                                <asp:TextBox ID="txtBoardDateC" runat="server" CssClass="inputtext formsize80"></asp:TextBox>
                                <a href="javascript:" class="timesicon" onclick="WdatePicker({el:'<%=txtBoardDateC.ClientID %>',minDate: '%y-%M-#{%d' })">
                                </a>
                                <asp:TextBox ID="txtBoardTimeC" runat="server" CssClass="inputtext formsize40"></asp:TextBox>
                            </td>
                            <th width="15%" align="right">
                                <span class="fontred">*</span><span class="addtableT">登船码头：</span>
                            </th>
                            <td>
                                <asp:TextBox ID="txtBoardWhilfC" runat="server" CssClass="inputtext formsize140"
                                    valid="required" errmsg="*请输入登船码头!" isvalid="1"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <th align="right" class="border-l">
                                包含景点：
                            </th>
                            <td colspan="3">
                                <asp:TextBox ID="txtStopAttrC" TextMode="MultiLine" runat="server" CssClass="inputtext formsize600"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <th align="right" class="border-l">
                                <span class="fontred">*</span>价格组成：
                            </th>
                            <td colspan="3">
                                <table width="100%" id="tabPricesC" border="0" cellspacing="0" cellpadding="0" class="noborder">
                                    <asp:PlaceHolder ID="tabView" runat="server">
                                        <tr class="tempRow">
                                            <td width="84%">
                                                <select name="selAircraft" class="inputselect">
                                                    <%=GetShipSeat("") %>
                                                </select>
                                                人数
                                                <input type="text" name="AdultNumsC" class="inputtext formsize40" valid="required|isMoney"
                                                    errmsg="*请输入人数!|*请填写正确的人数!" isvalid="1" />
                                                单价：<input type="text" class="inputtext formsize40" name="adultPricesC" valid="required|isMoney"
                                                    errmsg="*请输入单价!|*单价格式不正确!" isvalid="1" />
                                                小计：
                                                <input type="text" name="txtGuoNeiXiaoJi" class="inputtext formsize50" valid="required|isMoney"
                                                    errmsg="*请输入小计费用!|小计费用格式不正确!" isvalid="1" />
                                                备注：<input type="text" name="txtGuoNeiBeiZhu" class="inputtext" maxlength="255" />
                                            </td>
                                            <td width="16%" align="right">
                                                <a href="javascript:void(0);" class="addbtn">
                                                    <img src="/images/addimg.gif" width="48" height="20" /></a> <a href="javascript:void(0);"
                                                        class="delbtn">
                                                        <img src="/images/delimg.gif" width="48" height="20" /></a>
                                            </td>
                                        </tr>
                                    </asp:PlaceHolder>
                                    <asp:Repeater ID="repPriceListC" runat="server">
                                        <ItemTemplate>
                                            <tr class="tempRow">
                                                <td width="84%">
                                                    <select name="selAircraft" class="inputselect" isvalid="1" valid="required" errmsg="*请选择舱位!">
                                                        <%# GetShipSeat(((int)Eval("RoomType")).ToString())%>
                                                    </select>
                                                    人数：
                                                    <input type="text" name="AdultNumsC" class="inputtext formsize40" valid="required|isMoney"
                                                        errmsg="*请输入人数!|*请填写正确的人数!" isvalid="1" value="<%# Eval("DNum","{0:F2}") %>" />
                                                    单价：
                                                    <input type="text" class="inputtext formsize40" name="adultPricesC" value="<%# EyouSoft.Common.Utils.FilterEndOfTheZeroDecimal(Convert.ToDecimal( Eval("AdultPrice"))) %>"
                                                        valid="required|isMoney"  errmsg="*请输入单价!|*单价格式不正确!"
                                                        isvalid="1" />
                                                    小计：<input type="text" name="txtGuoNeiXiaoJi" class="inputtext formsize50" valid="required|isMoney"
                                                        errmsg="*请输入小计费用!|小计费用格式不正确!" isvalid="1" value="<%#Eval("SumPrice","{0:F2}") %>" />
                                                    备注：<input type="text" name="txtGuoNeiBeiZhu" class="inputtext" maxlength="255" value="<%#Eval("BeiZhu") %>" />
                                                </td>
                                                <td width="16%" align="right">
                                                    <a href="javascript:void(0);" class="addbtn">
                                                        <img src="/images/addimg.gif" width="48" height="20" /></a> <a href="javascript:void(0);"
                                                            class="delbtn">
                                                            <img src="/images/delimg.gif" width="48" height="20" /></a>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <th align="right" class="border-l">
                                <span class="border-l"><span class="fontred">*</span>结算费用</span>：
                            </th>
                            <td align="left">
                                <asp:TextBox ID="txttotalPricesC" runat="server" CssClass="inputtext formsize80"
                                    valid="required|isMoney" errmsg="*请输入结算费用!|*结算费用格式不正确!"
                                    isvalid="1"></asp:TextBox>
                                元
                            </td>
                            <th align="right">
                                
                            </th>
                            <td align="left">
                                
                            </td>
                        </tr>
                        <tr>
                            <th align="right" class="border-l">
                                费用明细：
                            </th>
                            <td colspan="3">
                                <asp:TextBox ID="txtCostDetailC" runat="server" CssClass="inputtext formsize600" TextMode="MultiLine"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <th align="right" class="border-l">
                                导游需知：
                            </th>
                            <td colspan="3">
                                <asp:TextBox ID="txtGuidNotesC" runat="server" TextMode="MultiLine" CssClass="inputtext formsize600">
船名：
登船日期：
登船码头：
                                </asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <th align="right" class="border-l">
                                其它备注：
                            </th>
                            <td colspan="3">
                                <asp:TextBox ID="txtOtherMarkC" runat="server" CssClass="inputtext formsize600"
                                    TextMode="MultiLine"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <th align="right">
                                <span class="fontred">*</span>支付方式：
                            </th>
                            <td align="left">
                                <select name="SelPanyMentC" class="inputselect">
                                    <%=UtilsCommons.GetOperaterPanyMentList(PanyMentC)%>
                                </select>
                            </td>
                            <th align="right">
                                <span class="fontred">*</span>返利：
                            </th>
                            <td align="left">
                                <asp:DropDownList ID="ddlprofit2" runat="server" CssClass="inputselect">
                                    <asp:ListItem Value="0">有</asp:ListItem>
                                    <asp:ListItem Value="1">无</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <th align="right" class="border-l">
                                <span class="fontred">*</span>状态：
                            </th>
                            <td colspan="3" align="left">
                                <select name="SelStateC" class="inputselect">
                                    <%= UtilsCommons.GetOperaterStatusList(StatusC)%>
                                </select>
                            </td>
                        </tr>
                    </tbody>
                </table>
                <asp:PlaceHolder ID="phdShowListSecond" runat="server">
                    <h2>
                        <p>
                            已安排国内游轮</p>
                    </h2>
                    <table id="tabShipListC" width="100%" border="0" cellspacing="0" cellpadding="0"
                        class="jd-table01" style="border-bottom: 1px solid #A9D7EC;">
                        <tr>
                            <th align="center" class="border-l">
                                游船公司
                            </th>
                            <th align="center">
                                船名
                            </th>
                            <th align="center">
                                人数
                            </th>
                            <th align="center">
                                费用明细
                            </th>
                            <th align="right">
                                结算费用
                            </th>
                            <th align="center">
                                支付方式
                            </th>
                            <th align="center">
                                状态
                            </th>
                            <th align="center">
                                确认单
                            </th>
                            <th align="center">
                                操作
                            </th>
                        </tr>
                        <asp:Repeater ID="repShipListC" runat="server">
                            <ItemTemplate>
                                <tr>
                                    <td align="center" class="border-l">
                                        <%# Eval("SourceName")%>
                                    </td>
                                    <td align="center">
                                        <%# Eval("ShipName")%>
                                    </td>
                                    <td align="center">
                                        <b class="fontblue">
                                            <%# Eval("DNum","{0:F2}")%></b>
                                    </td>
                                    <td align="center">
                                        <span title="<%# Eval("CostDetail") %>">
                                            <%# EyouSoft.Common.Utils.GetText2( Eval("CostDetail").ToString(),30,true) %></span>
                                    </td>
                                    <td align="right">
                                        <%#UtilsCommons.GetMoneyString(Convert.ToDecimal(Eval("Confirmation")),ProviderToMoney)%>
                                        <a href="javascript:void(0);" data-class="PrepaidC" title="预付申请" data-id="<%# Eval("PlanId") %>"
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
                                        <a href='<%# querenchinaUrl %>?planId=<%# Eval("PlanId") %>' target="_blank">
                                            <img src="/images/y-kehuqueding.gif" alt="" border="0" /></a>
                                    </td>
                                    <td align="center">
                                        <%if (ListPower)
                                          { %>
                                        <a href="javascript:" data-class="updateShipC">
                                            <img src="/images/y-delupdateicon.gif" alt="" data-id="<%# Eval("PlanId") %>" />
                                            修改</a> <a href="javascript:" data-class="deleteShipC">
                                                <img src="/images/y-delicon.gif" alt="" data-id="<%# Eval("PlanId") %>" />
                                                删除</a>
                                        <%}
                                          else
                                          { %>
                                        <a href="javascript:" data-class="showShipC">
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
                <asp:PlaceHolder ID="panView2" runat="server">
                    <div class="mainbox cunline fixed">
                        <ul id="ul_btn_listC">
                            <li class="cun-cy"><a href="javascript:void(0);" id="btnSaveC">保存</a> </li>
                        </ul>
                    </div>
                </asp:PlaceHolder>
            </div>
        </div>
        <input type="hidden" id="hidSurpluNum" runat="server" />
        <input type="hidden" id="hidSurpluNumC" runat="server" />
    </div>

    <script type="text/javascript">
        var ShipPage = {
            sl: '<%=SL %>',
            type: '<%=Utils.GetQueryStringValue("Type") %>',
            tourId: '<%=Utils.GetQueryStringValue("tourId") %>',
            iframeId: '<%=Utils.GetQueryStringValue("iframeId") %>',
            _OpenBoxy: function(title, iframeUrl, width, height, draggable) {
                parent.Boxy.iframeDialog({ title: title, iframeUrl: iframeUrl, width: width, height: height, draggable: draggable });
            },
            //初始化设置游船公司
            InitComInput: function() {
                $("#<%=this.SupplierControl1.ClientText %>").attr("isvalid", "1").attr("errmsg", "请选择游船公司").attr("valid", "required");
                $("#<%=this.SupplierControl2.ClientText %>").attr("isvalid", "1").attr("errmsg", "请选择游船公司").attr("valid", "required");
            },
            _RemoveAttr: function(b) {
                var box = $(b);
                box.find("input[isvalid='1']").each(function() {
                    if ($(this).attr("valid")) {
                        $(this).attr("data-valid", $(this).attr("valid")).removeAttr("valid");
                    }
                })
            },
            _AddAttr: function(b) {
                var box = $(b);
                box.find("input[isvalid='1']").each(function() {
                    $(this).attr("valid", $(this).attr("data-valid"));
                })
            },
            _SaveShip: function(tourId, planId) {
                var _Url = '/OperaterCenter/OperaterShipList.aspx?sl=' + ShipPage.sl + '&tourId=' + ShipPage.tourId + '&type=' + ShipPage.type + "&iframeId=" + ShipPage.iframeId + "&m=" + new Date().getTime() + "&typeId=4_1";
                $.newAjax({
                    type: "POST",
                    url: "/OperaterCenter/OperaterShipList.aspx?action=save&sl=" + ShipPage.sl + "&tourId=" + tourId + "&planId=" + planId,
                    cache: false,
                    data: $("#btnSave").closest("form").serialize(),
                    dataType: "json",
                    success: function(data) {
                        if (data.result == "1") {
                            parent.tableToolbar._showMsg(data.msg, function() {
                                window.location.href = _Url;
                            });
                        } else {
                            parent.tableToolbar._showMsg(data.msg, function() {
                                ShipPage._RemoveAttr("#d");
                                ShipPage._AddAttr("#c");
                                $("#btnSave").text("保存").css("background-position", "0 0px").bind("click");
                                $("#btnSave").click(function() {
                                    $(this).text("保存中...").css("background-position", "0 -62px").unbind("click");
                                    var _planId = '<%=Utils.GetQueryStringValue("planId") %>';
                                    ShipPage._SaveShip(ShipPage.tourId, _planId);
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
            _DeleteShip: function(_ObjID) {
                var _Url = '/OperaterCenter/OperaterShipList.aspx?sl=' + ShipPage.sl + '&tourId=' + ShipPage.tourId + '&type=' + ShipPage.type + "&iframeId=" + ShipPage.iframeId + "&m=" + new Date().getTime() + "&typeId=4_1";
                $.newAjax({
                    type: "get",
                    url: '/OperaterCenter/OperaterShipList.aspx?sl=' + ShipPage.sl + '&action=delete&planId=' + _ObjID + '&tourid=' + ShipPage.tourId,
                    cache: false,
                    dataType: 'json',
                    success: function(data) {
                        if (data.result == "1") {
                            parent.tableToolbar._showMsg(data.msg, function() {
                                window.location.href = _Url;
                            });
                            return false;
                        }
                        else {
                            parent.tableToolbar._showMsg(data.msg, function() {
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
            _AjaxReqShipName: function(id, type, source, companyID) {
                if (id) {
                    $.newAjax({
                        type: "POST",
                        url: "/Ashx/GetSupplierContect.ashx?suppId=" + id + "&type=" + type + "&source=" + source + "&company=" + companyID,
                        dataType: "json",
                        success: function(ret) {
                            if (ret.tolist != "") {
                                if (ret.tolist.length > 0) {
                                    for (var i = 0; i < ret.tolist.length; i++) {
                                        $("select[name='ddlShipNameList']").append("<option value='" + ret.tolist[i].id + "," + ret.tolist[i].text + "'>" + ret.tolist[i].text + "</option>");
                                    }
                                }
                            }
                        },
                        error: function() {
                            parent.tableToolbar._showMsg(tableToolbar.errorMsg);
                            return false;
                        }
                    });
                }
            },
            _AjaxContectInfo: function(obj) {
                $("#<%=SupplierControl1.ClientText %>").val(obj.name);
                $("#<%=SupplierControl1.ClientValue %>").val(obj.id);
                $("#<%=SupplierControl1.ClientzyykValue %>").val(obj.hideID_zyyk);
                $("#<%=SupplierControl1.ClientIsyukong %>").val(obj.isYuKong);
                $("#<%=txtContectName.ClientID %>").val(obj.contactname);
                $("#<%=txtContectPhone.ClientID %>").val(obj.contacttel);
                $("#<%=txtContectFax.ClientID %>").val(obj.contactfax);
                $("#<%=hidSurpluNum.ClientID %>").val(tableToolbar.calculate(obj.ControlNum, obj.UserNum, "-"));
                $("select[name='ddlShipNameList'] option").each(function() {
                    if ($(this).val() != "") {
                        $(this).remove();
                    }
                });
                var supplierType = '<%=(int)EyouSoft.Model.EnumType.PlanStructure.PlanProject.涉外游轮 %>';
                var companyId = "<%=this.SiteUserInfo.CompanyId %>";
                if (obj.isYuKong == "0") {
                    ShipPage._AjaxReqShipName(obj.id, supplierType, "1", companyId);
                }
                else {
                    ShipPage._AjaxReqShipName(obj.hideID_zyyk, supplierType, "2", companyId);
                }
            },
            _AjaxReqShipNameC: function(id, type, source, companyID) {
                if (id) {
                    $.newAjax({
                        type: "POST",
                        url: "/Ashx/GetSupplierContect.ashx?suppId=" + id + "&type=" + type + "&source=" + source + "&company=" + companyID,
                        dataType: "json",
                        success: function(ret) {
                            if (ret.tolist != "") {
                                if (ret.tolist.length > 0) {
                                    for (var i = 0; i < ret.tolist.length; i++) {
                                        $("select[name='SelShipNameC']").append("<option value='" + ret.tolist[i].id + "," + ret.tolist[i].text + "'>" + ret.tolist[i].text + "</option>");
                                    }
                                }
                            }
                        },
                        error: function() {
                            parent.tableToolbar._showMsg(tableToolbar.errorMsg);
                            return false;
                        }
                    });
                }
            },
            _AjaxContectCInfo: function(obj) {
                $("#<%=SupplierControl2.ClientText %>").val(obj.name);
                $("#<%=SupplierControl2.ClientValue %>").val(obj.id);
                $("#<%=SupplierControl2.ClientzyykValue %>").val(obj.hideID_zyyk);
                $("#<%=SupplierControl2.ClientIsyukong %>").val(obj.isYuKong);
                $("#<%=txtContectNameC.ClientID %>").val(obj.contactname);
                $("#<%=txtContectPhoneC.ClientID %>").val(obj.contacttel);
                $("#<%=txtContectFaxC.ClientID %>").val(obj.contactfax);
                $("#<%=hidSurpluNumC.ClientID %>").val(tableToolbar.calculate(obj.ControlNum, obj.UserNum, "-"));
                //清空
                $("select[name='SelShipNameC'] option").each(function() {
                    if ($(this).val() != "") {
                        $(this).remove();
                    }
                });
                var supplierType = '<%=(int)EyouSoft.Model.EnumType.PlanStructure.PlanProject.国内游轮 %>';
                var companyId = "<%=this.SiteUserInfo.CompanyId %>";
                if (obj.isYuKong == "0") {
                    ShipPage._AjaxReqShipNameC(obj.id, supplierType, "1", companyId);
                }
                else {
                    ShipPage._AjaxReqShipNameC(obj.hideID_zyyk, supplierType, "2", companyId);
                }
            },
            _SaveShipC: function(tourId, planId) {
                var _Url = '/OperaterCenter/OperaterShipList.aspx?sl=' + ShipPage.sl + '&tourId=' + ShipPage.tourId + '&type=' + ShipPage.type + "&iframeId=" + ShipPage.iframeId + "&m=" + new Date().getTime() + "&typeId=4_2";
                $.newAjax({
                    type: "POST",
                    url: "/OperaterCenter/OperaterShipList.aspx?action=saveC&sl=" + ShipPage.sl + "&tourId=" + tourId + "&planId=" + planId,
                    cache: false,
                    data: $("#btnSaveC").closest("form").serialize(),
                    dataType: "json",
                    success: function(data) {
                        if (data.result == "1") {
                            parent.tableToolbar._showMsg(data.msg, function() {
                                window.location.href = _Url;
                            });
                        } else {
                            parent.tableToolbar._showMsg(data.msg, function() {
                                ShipPage._RemoveAttr("#c");
                                ShipPage._AddAttr("#d");
                                $("#btnSaveC").text("保存").css("background-position", "0 0px").bind("click");
                                $("#btnSaveC").click(function() {
                                    $(this).text("保存中...").css("background-position", "0 -62px").unbind("click");
                                    var _PlanId = '<%=Utils.GetQueryStringValue("planId") %>';
                                    ShipPage._SaveShipC(ShipPage.tourId, _PlanId);
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
            _DeleteShipC: function(_ObjID) {
                var _Url = '/OperaterCenter/OperaterShipList.aspx?sl=' + ShipPage.sl + '&tourId=' + ShipPage.tourId + '&type=' + ShipPage.type + "&iframeId=" + ShipPage.iframeId + "&m=" + new Date().getTime() + "&typeId=4_2";
                $.newAjax({
                    type: "get",
                    url: '/OperaterCenter/OperaterShipList.aspx?sl=' + ShipPage.sl + '&action=deleteC&planId=' + _ObjID,
                    cache: false,
                    dataType: 'json',
                    success: function(data) {
                        if (data.result == "1") {
                            parent.tableToolbar._showMsg(data.msg, function() {
                                window.location.href = _Url;
                            });
                            return false;
                        }
                        else {
                            parent.tableToolbar._showMsg(data.msg, function() {
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
            _TotalPrices: function() {
                //价格组成
                var Floortotal = "";
                $("input[type=text][name='txtTotalPrice']").each(function() {

                    //var trClass = $(this).parent().parent(".tempRow");
                    var trClass = $(this).closest("tr");
                    var adultNum = trClass.find("input[type=text][name='txtAdultNum']").val();
                    var adultPrices = trClass.find("input[type=text][name='txtAdultPrice']").val();
                    var adultTotalPrices = tableToolbar.calculate(adultNum, adultPrices, "*");

                    var childNum = trClass.find("input[type=text][name='txtChildNum']").val();
                    var childPrices = trClass.find("input[type=text][name='txtChildPrice']").val();
                    var childTotalPrices = tableToolbar.calculate(childNum, childPrices, "*");

                    var childNoNum = trClass.find("input[type=text][name='txtChildNoNum']").val();
                    var childNoPrices = trClass.find("input[type=text][name='txtChildNoPrice']").val();
                    var childNoTotalPrices = tableToolbar.calculate(childNoNum, childNoPrices, "*");

                    var bobyNum = trClass.find("input[type=text][name='txtBobyNum']").val();
                    var bobyPrices = trClass.find("input[type=text][name='txtBobyPrice']").val();
                    var bobyTotalPrices = tableToolbar.calculate(bobyNum, bobyPrices, "*");

                    var adultChildPrices = tableToolbar.calculate(adultTotalPrices, childTotalPrices, "+");
                    var AdChildNo = tableToolbar.calculate(adultChildPrices, childNoTotalPrices, "+");
                    trClass.find("input[type=text][name='txtTotalPrice']").val(tableToolbar.calculate(AdChildNo, bobyTotalPrices, "+"));

                    Floortotal = tableToolbar.calculate(Floortotal, tableToolbar.calculate(AdChildNo, bobyTotalPrices, "+"), "+");
                });

                //楼层
                var FloorPrices = "";
                $("input[type='text'][name='txtUnitPrice']").each(function() {
                    var unitPrices = $(this).parent().parent(".tempRow").find("input[type='text'][name='txtPeopleNum']").val();
                    FloorPrices = tableToolbar.calculate(FloorPrices, tableToolbar.calculate($(this).val(), unitPrices, "*"), "+");
                });

                var totalMoneys = tableToolbar.calculate(Floortotal, FloorPrices, "+");

                //结算费用
                $("#<%=txtTotalPrices.ClientID %>").val(totalMoneys);

                //国内游轮价格计算
                var total = 0;
                $("input[type=text][name='AdultNumsC']").each(function() {
                    var adultNums = $(this).val();
                    var adultPrices = $(this).parent().parent(".tempRow").find("input[type='text'][name='adultPricesC']").val();
                    var adultTotalPrices = tableToolbar.calculate(adultNums, adultPrices, "*");
                    var childNums = $(this).parent().parent(".tempRow").find("input[type='text'][name='childrenNumC']").val();
                    var childPrices = $(this).parent().parent(".tempRow").find("input[type='text'][name='childrenpricesC']").val();
                    var childTotalPrices = tableToolbar.calculate(childNums, childPrices, "*");

                    $(this).parent().parent(".tempRow").find("input[name='txtGuoNeiXiaoJi']").val(adultTotalPrices);

                    total = tableToolbar.calculate(total, tableToolbar.calculate(adultTotalPrices, childTotalPrices, "+"), "+");
                });
                $("#<%=txttotalPricesC.ClientID %>").val(total);
            },
            _BindBtn: function() {
                $("input[type=radio][name='radshipType']").unbind("click");
                $("input[type=radio][name='radshipType']").click(function() {
                    if ($(this).val() == "1") {
                        $("#hd_SupplierControl2_ID").val("");
                        $("#txt_SupplierControl2_Name").val("");
                        $("#hd_supplierControl2_zyykID").val("");
                        $("#c").css("display", "");
                        ShipPage._AddAttr("#c");
                        $("#d").css("display", "none");
                        ShipPage._RemoveAttr("#d");
                    } else {
                        $("#hd_SupplierControl1_ID").val("");
                        $("#txt_SupplierControl1_Name").val("");
                        $("#hd_supplierControl1_zyykID").val("");
                        $("#c").css("display", "none");
                        ShipPage._RemoveAttr("#c");
                        $("#d").css("display", "");
                        ShipPage._AddAttr("#d");
                    }
                    parent.ConfigPage.SetWinHeight();
                });
                //失去焦点计算价格
                $("#tabPricesC").find("input[type=text]").blur(function() {
                    ShipPage._TotalPrices();
                });

                $("#tabPrices").find("input[type=text][name!='txtTotalPrice']").blur(function() {
                    ShipPage._TotalPrices();
                });

                $("#floorPricetb").find("input[type='text']").blur(function() {
                    ShipPage._TotalPrices();
                });

                $("input[type='text'][name='AdultNumsC'],input[type='text'][name='adultPricesC'],input[type='text'][name='childrenNumC'],input[type='text'][name='childrenpricesC']").blur(function() {
                    ShipPage._TotalPrices();
                });


                $("#tabForeignShip").find("a[data-class='Prepaid']").unbind("click");
                $("#tabForeignShip").find("a[data-class='Prepaid']").click(function() {
                    var planId = $(this).attr("data-ID");
                    var soucesName = $(this).attr("data-soucesname");
                    ShipPage._OpenBoxy("预付申请", '/OperaterCenter/PrepaidAppliaction.aspx?PlanId=' + planId + '&type=' + ShipPage.type + '&sl=' + ShipPage.sl + '&tourId=' + ShipPage.tourId + '&souceName=' + escape(soucesName), "650px", "550px", true);
                    return false;
                });

                //删除
                $("#tabForeignShip").find("a[data-class='deleteShip']").unbind("click");
                $("#tabForeignShip").find("a[data-class='deleteShip']").click(function() {
                    var newThis = $(this);
                    tableToolbar.ShowConfirmMsg("确定删除此条数据吗?", function() {
                        var PlanId = newThis.find("img").attr("data-ID");
                        if (PlanId) {
                            ShipPage._DeleteShip(PlanId);
                        }
                    });
                    return false;
                });

                //修改
                $("#tabForeignShip").find("a[data-class='updateShip']").unbind("click");
                $("#tabForeignShip").find("a[data-class='updateShip']").click(function() {
                    var planId = $(this).find("img").attr("data-ID");
                    if (planId) {
                        window.location.href = '/OperaterCenter/OperaterShipList.aspx?type=' + ShipPage.type + '&sl=' + ShipPage.sl + '&tourId=' + ShipPage.tourId + '&action=update&planId=' + planId + "&iframeId=" + ShipPage.iframeId + "&typeId=4_1";
                    }
                    return false;
                });

                //查看
                $("#tabForeignShip").find("a[data-class='showShip']").unbind("click");
                $("#tabForeignShip").find("a[data-class='showShip']").click(function() {
                    var planId = $(this).find("img").attr("data-ID");
                    if (planId) {
                        window.location.href = '/OperaterCenter/OperaterShipList.aspx?type=' + ShipPage.type + '&sl=' + ShipPage.sl + '&tourId=' + ShipPage.tourId + '&action=update&planId=' + planId + "&iframeId=" + ShipPage.iframeId + "&typeId=4_1&" + "show=1";
                    }
                    return false;
                });

                $("#<%=txtBoardDate.ClientID %>").focus(function() { WdatePicker({ minDate: '%y-%M-#{%d' }); });
                $("#<%=txtBoardDateC.ClientID %>").focus(function() { WdatePicker({ minDate: '%y-%M-#{%d' }); });

                $("#btnSave").unbind("click").text("保存").css("background-position", "0 0px");
                $("#btnSave").click(function() {
                    if (!ValiDatorForm.validator($("#btnSave").closest("form").get(0), "parent")) {
                        return false;
                    } else {
                        if ($("#<%=SupplierControl1.ClientIsyukong %>").val() == "1") {
                            if ($("#<%=hidSurpluNum.ClientID %>").val() == "0") {
                                parent.tableToolbar._showMsg("可预控的数量不足!");
                                return false;
                            }
                        }
                        ShipPage._RemoveAttr("#d");
                        ShipPage._AddAttr("#c");
                        $(this).text("保存中...").css("background-position", "0 -62px").unbind("click");
                        var planId = '<%=Utils.GetQueryStringValue("planId") %>';
                        ShipPage._SaveShip(ShipPage.tourId, planId);
                    }

                });

                $("#btnSaveC").unbind("click").text("保存").css("background-position", "0 0px");
                $("#btnSaveC").click(function() {
                    if (!ValiDatorForm.validator($("#btnSaveC").closest("form").get(0), "parent")) {
                        return false;
                    } else {
                        if ($("#<%=SupplierControl2.ClientIsyukong %>").val() == "1") {
                            if ($("#<%=hidSurpluNumC.ClientID %>").val() == "0") {
                                parent.tableToolbar._showMsg("可预控的数量不足!");
                                return false;
                            }
                        }
                        ShipPage._AddAttr("#d");
                        ShipPage._RemoveAttr("#c");
                        $(this).text("保存中...").css("background-position", "0 -62px").unbind("click");
                        var palnId = '<%=Utils.GetQueryStringValue("planId") %>';
                        ShipPage._SaveShipC(ShipPage.tourId, palnId);
                    }
                });

                //国内游轮js
                $("#tabShipListC").find("a[data-class='PrepaidC']").unbind("click");
                $("#tabShipListC").find("a[data-class='PrepaidC']").click(function() {
                    var planId = $(this).attr("data-ID");
                    var soucesName = $(this).attr("data-soucesname");
                    ShipPage._OpenBoxy("预付申请", '/OperaterCenter/PrepaidAppliaction.aspx?PlanId=' + planId + '&type=' + ShipPage.type + '&sl=' + ShipPage.sl + '&tourId=' + ShipPage.tourId + '&souceName=' + escape(soucesName), "650px", "550px", true);
                    return false;
                });

                $("#tabShipListC").find("a[data-class='deleteShipC']").unbind("click");
                $("#tabShipListC").find("a[data-class='deleteShipC']").click(function() {
                    var newThis = $(this);
                    tableToolbar.ShowConfirmMsg("确定删除此条数据吗?", function() {
                        var PlanId = newThis.find("img").attr("data-ID");
                        if (PlanId) {
                            ShipPage._DeleteShipC(PlanId);
                        }
                    });
                    return false;
                });

                $("#tabShipListC").find("a[data-class='updateShipC']").unbind("click");
                $("#tabShipListC").find("a[data-class='updateShipC']").click(function() {
                    var planId = $(this).find("img").attr("data-ID");
                    if (planId) {
                        window.location.href = '/OperaterCenter/OperaterShipList.aspx?type=' + ShipPage.type + '&sl=' + ShipPage.sl + '&tourId=' + ShipPage.tourId + '&action=updateC&planId=' + planId + "&iframeId=" + ShipPage.iframeId + "&typeId=4_2";
                    }
                    return false;
                });

                $("#tabShipListC").find("a[data-class='showShipC']").unbind("click");
                $("#tabShipListC").find("a[data-class='showShipC']").click(function() {
                    var planId = $(this).find("img").attr("data-ID");
                    if (planId) {
                        window.location.href = '/OperaterCenter/OperaterShipList.aspx?type=' + ShipPage.type + '&sl=' + ShipPage.sl + '&tourId=' + ShipPage.tourId + '&action=updateC&planId=' + planId + "&iframeId=" + ShipPage.iframeId + "&typeId=4_2&show=2";
                    }
                    return false;
                });

                $("#tabPrices").autoAdd({ addCallBack: function() { parent.ConfigPage.SetWinHeight(); ShipPage._AddAttr("#tabPrices"); }, delCallBack: parent.ConfigPage.SetWinHeight() });
                $("#floorPricetb").autoAdd({ addCallBack: function() { parent.ConfigPage.SetWinHeight(); ShipPage._AddAttr("#floorPricetb"); }, delCallBack: parent.ConfigPage.SetWinHeight() });
                $("#tabPricesC").autoAdd({ addCallBack: function() { parent.ConfigPage.SetWinHeight(); ShipPage._AddAttr("#tabPricesC"); }, delCallBack: parent.ConfigPage.SetWinHeight() });
            },
            _PageInit: function() {
                ShipPage._BindBtn();
                //全局计调修改，按钮状态控制  4_1 涉外游轮 4_2 国内游轮
                var typeId = '<%=EyouSoft.Common.Utils.GetQueryStringValue("typeId") %>';
                if (typeId != "") {
                    var Id = typeId.split('_');
                    //涉外游轮
                    if (Id[1] == "1") {
                        $("#r1").click();
                    } else {  //国内游轮
                        $("#r2").click();
                    }

                } else {
                    $("#r1").click();
                }
                parent.ConfigPage.SetWinHeight();

                if ('<%=EyouSoft.Common.Utils.GetQueryStringValue("show") %>' == "1") {
                    $("#ul_btn_list").parent("div").hide();
                }

                if ('<%=EyouSoft.Common.Utils.GetQueryStringValue("show") %>' == "2") {
                    $("#ul_btn_listC").parent("div").hide();
                }

            }
        }

        $(document).ready(function() {
            ShipPage.InitComInput();
            ShipPage._PageInit();
            parent.ConfigPage.SetWinHeight();
        });
    </script>

    </form>
</body>
</html>

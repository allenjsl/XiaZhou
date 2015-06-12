<%@ Page Title="组团团队" Language="C#" MasterPageFile="~/MasterPage/Front.Master" AutoEventWireup="true"
    CodeBehind="AddTeamPlan.aspx.cs" Inherits="Web.TeamCenter.AddTeamPlan" ValidateRequest="false" %>

<%@ Register Src="../UserControl/Journey.ascx" TagName="Journey" TagPrefix="uc1" %>
<%@ Register Src="../UserControl/ForeignQuote.ascx" TagName="ForeignQuote" TagPrefix="uc2" %>
<%@ Register Src="../UserControl/CostAccounting.ascx" TagName="CostAccounting" TagPrefix="uc3" %>
<%@ Register Src="../UserControl/TravelControl.ascx" TagName="TravelControl" TagPrefix="uc4" %>
<%@ Register Src="../UserControl/CustomerUnitSelect.ascx" TagName="CustomerUnitSelect"
    TagPrefix="uc5" %>
<%@ Register Src="../UserControl/SellsSelect.ascx" TagName="SellsSelect" TagPrefix="uc6" %>
<%@ Register Src="../UserControl/TravelControlS.ascx" TagName="TravelControlS" TagPrefix="uc7" %>
<%@ Register Src="../UserControl/UploadControl.ascx" TagName="UploadControl" TagPrefix="uc8" %>
<%@ Register Src="~/UserControl/HeTongHao.ascx" TagName="HeTongHao" TagPrefix="uc9" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script src="/Js/kindeditor-4.1/kindeditor-min.js" type="text/javascript"></script>

    <link href="/Css/swfupload/default.css" rel="stylesheet" type="text/css" />

    <script src="/Js/swfupload/swfupload.js" type="text/javascript"></script>

    <style type="text/css">
        td.noboder td
        {
            border: 0px;
            margin: 0;
            padding: 0;
            white-space: normal;
            height: auto;
        }
        #spanPlanContent td
        {
            border: 0px;
            margin: 0;
            padding: 0;
            white-space: normal;
            height: auto;
        }
        #spanPlanContent tr
        {
            height: auto;
        }
    </style>

    <script src="/Js/ValiDatorForm.js" type="text/javascript"></script>

    <script src="/Js/datepicker/WdatePicker.js" type="text/javascript"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form id="form1" runat="server">
    <div class="mainbox mainbox-whiteback">
        <div class="addContent-box">
            <table width="100%" cellspacing="0" cellpadding="0" class="firsttable">
                <tbody>
                    <tr>
                        <td width="12%" class="addtableT">
                            团号：
                        </td>
                        <td width="25%" class="kuang2">
                            <asp:Label ID="lblTourCode" runat="server" Text="自动生成"></asp:Label>
                        </td>
                        <td width="12%" class="addtableT">
                            <font class="fontbsize12">* </font>线路区域：
                        </td>
                        <td width="13%" class="kuang2">
                            <select class="inputselect" id="sltArea" name="sltArea" valid="isNo" errmsg="请选择线路区域"
                                novalue="0">
                                <asp:Literal ID="litArea" runat="server"></asp:Literal>
                            </select>
                        </td>
                        <td width="12%" class="addtableT">
                            <font class="fontbsize12">* </font>线路名称：
                        </td>
                        <td width="26%" class="kuang2">
                            <asp:TextBox runat="server" ID="txt_RouteName" CssClass="inputtext formsize180" valid="required"
                                errmsg="请填写线路名称" MaxLength="30"></asp:TextBox>
                            <asp:HiddenField ID="hideRouteID" runat="server" />
                            <a id="xianluxy" class="xuanyong" href="javascript:void(0);"></a>
                        </td>
                    </tr>
                    <tr>
                        <td class="addtableT">
                            <font class="fontbsize12">* </font>天数：
                        </td>
                        <td class="kuang2">
                            <asp:TextBox ID="txt_Days" runat="server" CssClass="inputtext formsize40" errmsg="请输入天数!|请输入正确的天数!|天数必须大于0!"
                                valid="required|isInt|range" min="1"></asp:TextBox>
                            <button class="addtimebtn" type="button" id="btnAddDays">
                                增加日程</button>
                        </td>
                        <td class="addtableT">
                            <font class="fontbsize12">* </font>出团时间：
                        </td>
                        <td class="kuang2">
                            <asp:TextBox ID="txtLDate" runat="server" onfocus="WdatePicker()"
                                CssClass="inputtext formsize100" valid="required" errmsg="请填写天数"></asp:TextBox>
                            <a class="timesicon" href="javascript:void(0);" onclick="WdatePicker({el:'<%=txtLDate.ClientID %>'});">
                            </a>
                        </td>
                        <td class="addtableT">
                            <font class="fontbsize12">* </font>客源地：
                        </td>
                        <td class="kuang2">
                            <select class="inputselect" id="sltCountry" name="sltCountry" valid="required" errmsg="请选择客源地-国家!">
                            </select>
                            <select class="inputselect" id="sltProvince" name="sltProvince" valid="required"
                                errmsg="请选择客源地-省份!">
                            </select>
                        </td>
                    </tr>
                    <tr>
                        <td class="addtableT">
                            <font class="fontbsize12">* </font>客户单位：
                        </td>
                        <td class="kuang2">
                            <uc5:CustomerUnitSelect ID="UC_CustomerUnitSelect" runat="server" />
                        </td>
                        <td class="addtableT">
                            <font class="fontbsize12">*</font>联系人：
                        </td>
                        <td class="kuang2">
                            <asp:TextBox runat="server" ID="txt_Contact" CssClass="inputtext formsize80" errmsg="请输入联系人!"
                                valid="required"></asp:TextBox>
                            <asp:HiddenField ID="hideContactDeptId" runat="server" />
                        </td>
                        <td class="addtableT">
                            <font class="fontbsize12">*</font>联系电话：
                        </td>
                        <td class="kuang2">
                            <asp:TextBox runat="server" ID="txt_ConTel" CssClass="inputtext formsize120" errmsg="请输入联系电话!|电话格式不正确!"
                                valid="required|isTel"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="addtableT">
                            销售员：
                        </td>
                        <td class="kuang2">
                            <uc6:SellsSelect ID="SellsSelect1" runat="server" SetTitle="销售员" />
                        </td>
                        <td class="addtableT">
                            出发交通：
                        </td>
                        <td width="55%" class="kuang2" colspan="3">
                            <asp:TextBox ID="txtSuccesssStraffBegin" runat="server" CssClass="inputtext formsize120"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="addtableT">
                            <font class="fontbsize12">* </font>人数：
                        </td>
                        <td class="kuang2">
                            <img width="16" height="15" style="vertical-align: middle" src="/images/chengren.gif">成人
                            <asp:TextBox ID="txt_Adult" class="txt_Adult" runat="server" CssClass="inputtext formsize40"
                                errmsg="请输入成人数!|请输入正确的成人数!|成人数必须在1-999之间!" valid="required|isInt|range" min="1"
                                max="999"></asp:TextBox>
                            &nbsp;
                            <img style="vertical-align: middle" src="/images/child.gif">
                            儿童
                            <asp:TextBox ID="txt_Child" class="txt_Child" runat="server" CssClass="inputtext formsize40"
                                errmsg="请输入正确的儿童数!|儿童数必须在0-999之间!" valid="isInt|range" min="0" max="999"></asp:TextBox>
                        </td>
                        <td class="addtableT">
                            返程交通：
                        </td>
                        <td width="55%" class="kuang2" colspan="3">
                            <asp:TextBox ID="txtSuccesssStraffEnd" runat="server" CssClass="inputtext formsize120"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td height="14" class="addtableT">
                            集合方式：
                        </td>
                        <td width="90%" class="kuang2 pand4" colspan="5">
                            <asp:TextBox ID="txtSuccessGather" runat="server" CssClass="inputtext formsize800"
                                TextMode="MultiLine"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td height="14" class="addtableT">
                            行程特色：
                        </td>
                        <td width="90%" class="kuang2 pand4" colspan="5">
                            <span id="spanPlanContent" style="display: inline-block;">
                                <asp:TextBox ID="txtPlanContent" runat="server" TextMode="MultiLine" CssClass="inputtext formsize800"></asp:TextBox>
                            </span>
                        </td>
                    </tr>
                    <asp:PlaceHolder ID="phdVisaFile" runat="server">
                        <tr>
                            <td class="addtableT">
                                签证资料：
                            </td>
                            <td class="kuang2" colspan="5">
                                <uc8:UploadControl ID="UploadControl1" runat="server" />
                                <asp:Label ID="lblVisaFiles" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                    </asp:PlaceHolder>
                </tbody>
            </table>
        </div>
        <uc1:Journey ID="Journey1" runat="server" />
        <div class="hr_10">
        </div>
        <uc2:ForeignQuote ID="ForeignQuote1" runat="server" />
        <div class="hr_5">
        </div>
        <div class="addxc-box">
            <table style="width: 100%;" cellspacing="0" cellpadding="0" class="add-baojia">
                <tbody>
                    <tr>
                        <td bgcolor="#E0F3FC" colspan="2">
                            <img width="16" height="15" align="absmiddle" src="/images/chengren.gif">
                            成人价
                            <asp:TextBox ID="txtAdultPrice" runat="server" valid="required|isMoney" errmsg="请输入成人价!|成人价 格式不正确!"
                                CssClass="inputtext formsize40"></asp:TextBox>
                            元/人 &nbsp;&nbsp;
                            <img align="absmiddle" src="/images/child.gif">
                            儿童价
                            <asp:TextBox ID="txtChildPrice" runat="server" valid="isMoney" errmsg="儿童价 格式不正确!"
                                CssClass="inputtext formsize40"></asp:TextBox>
                            元/人&nbsp;&nbsp;&nbsp;其它费用：&nbsp;
                            <asp:TextBox ID="txtOtherPrice" runat="server" valid="isMoney" errmsg="其它价 格式不正确!"
                                CssClass="inputtext formsize40"></asp:TextBox>
                            元&nbsp;&nbsp;&nbsp;&nbsp;<span class="fontred">*</span>合计价格：
                            <asp:TextBox ID="txtSumPrice" runat="server" valid="required|isMoney" errmsg="请输入合计价!|合计价格式不正确!"
                                CssClass="inputtext formsize40"></asp:TextBox>
                            元&nbsp;&nbsp;&nbsp;
                            <label>
                        </td>
                    </tr>
                    <tr>
                        <td bgcolor="#E0F3FC" align="left" class="lefttd">
                            价格备注：
                        </td>
                        <td bgcolor="#E0F3FC" class="kuang2">
                            <asp:TextBox ID="txtQuoteRemark" runat="server" CssClass="inputtext formsize800"
                                TextMode="MultiLine"></asp:TextBox>
                        </td>
                    </tr>
                </tbody>
            </table>
            <div class="hr_5">
            </div>
            <table width="100%" cellspacing="0" cellpadding="0" class="firsttable">
                <tbody>
                    <tr>
                        <td width="118" class="addtableT">
                            增加费用：
                        </td>
                        <td class="kuang2">
                            <asp:TextBox ID="txtSuccessAddPrice" runat="server" CssClass="inputtext formsize80"
                                valid="isMoney" errmsg="增加费用格式不正确!"></asp:TextBox>
                        </td>
                        <td width="118" class="addtableT">
                            备注：
                        </td>
                        <td class="kuang2">
                            <asp:TextBox ID="txtSuccessAddPriceRemark" runat="server" CssClass="inputtext formsize80"
                                Width="260"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="addtableT">
                            减少费用：
                        </td>
                        <td class="kuang2">
                            <asp:TextBox ID="txtSuccessReducePrice" runat="server" CssClass="inputtext formsize80"
                                valid="isMoney" errmsg="减少费用格式不正确!"></asp:TextBox>
                        </td>
                        <td class="addtableT">
                            备注：
                        </td>
                        <td class="kuang2">
                            <asp:TextBox ID="txtSuccessReducePriceRemark" runat="server" CssClass="inputtext formsize80"
                                Width="260"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="addtableT">
                            导游现收：
                        </td>
                        <td class="kuang2">
                            <asp:TextBox ID="txtSuccessGuideIncome" runat="server" CssClass="inputtext formsize80"
                                valid="isMoney" errmsg="导游现收格式不正确!"></asp:TextBox>
                        </td>
                        <td class="addtableT">
                            销售应收：
                        </td>
                        <td class="kuang2">
                            <strong class="fontgreen">
                                <asp:Label ID="lblSuccessSalerIncome" runat="server" Text=""></asp:Label></strong>
                        </td>
                    </tr>
                    <tr>
                        <td class="addtableT">
                            订单备注：
                        </td>
                        <td class="kuang2 pand4" colspan="3">
                            <asp:TextBox ID="txtSuccessOrderRemark" runat="server" CssClass="inputtext formsize600"
                                Height="50" TextMode="MultiLine"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="addtableT">
                            合同号：
                        </td>
                        <td class="kuang2 pand4" colspan="3">
                            <uc9:HeTongHao runat="server" id="txtHeTongHao"></uc9:HeTongHao>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
        <uc3:CostAccounting ID="CostAccounting1" runat="server" />
        <div class="tablelist-box" style="width: 98.5%">
            <asp:PlaceHolder ID="phdTravelControl" runat="server">
                <uc4:TravelControl ID="TravelControl1" runat="server" />
            </asp:PlaceHolder>
            <asp:PlaceHolder ID="phdTravelControlS" runat="server">
                <uc7:TravelControlS ID="TravelControlS1" runat="server" />
            </asp:PlaceHolder>
        </div>
        <div class="hr_10">
        </div>
        <div class="mainbox cunline">
            <ul id="ul_AddTeamPlan_Btn">
                <asp:PlaceHolder ID="phdSave" runat="server">
                    <li class="cun-cy"><a href="javascript:void(0);" data-name="保存" id="btnSave">保存</a></li>
                </asp:PlaceHolder>
                <asp:PlaceHolder ID="phdCanel" runat="server">
                    <li class="cun-cy"><a id="btnCanel" href="javascript:void(0);" data-name="返回列表">返回列表</a></li>
                </asp:PlaceHolder>
            </ul>
            <div class="hr_10">
            </div>
        </div>
    </div>
    <div class="alertbox-outbox" id="div_AddTeamPlan_ApplyPrice" style="display: none;
        width: 620px; position: relative; padding-bottom: 0px;">
        <div id="div_AddTeamPlan_Com" style="display: none;">
            <div class="tanchuT" style="text-align: left;">
                <b class="fontred">客户单位超限：</b></div>
            <table width="99%" cellspacing="0" cellpadding="0" border="0" align="center">
                <tbody>
                    <tr style="background: url(/images/y-formykinfo.gif) repeat-x center top;">
                        <td height="23" bgcolor="#B7E0F3" align="center" class="alertboxTableT">
                            客户单位超限
                        </td>
                        <td bgcolor="#B7E0F3" align="center" class="alertboxTableT">
                            <span class="th-line">欠款额度</span>
                        </td>
                        <td bgcolor="#B7E0F3" align="center" class="alertboxTableT">
                            拖欠金额
                        </td>
                        <td bgcolor="#B7E0F3" align="center" class="alertboxTableT">
                            超限金额
                        </td>
                        <td bgcolor="#B7E0F3" align="center" class="alertboxTableT">
                            超限时间
                        </td>
                        <td bgcolor="#B7E0F3" align="center" class="alertboxTableT">
                            单团账龄期限
                        </td>
                        <td bgcolor="#B7E0F3" align="center" class="alertboxTableT">
                            最长超时天数
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
        <div class="hr_10">
        </div>
        <div id="div_AddTeamPlan_Sells" style="display: none">
            <div class="tanchuT" style="text-align: left;">
                <b class="fontred">销售员超限：</b></div>
            <table width="99%" cellspacing="0" cellpadding="0" border="0" align="center">
                <tbody>
                    <tr style="background: url(/images/y-formykinfo.gif) repeat-x center top;">
                        <td height="23" bgcolor="#B7E0F3" align="center" class="alertboxTableT">
                            销售员
                        </td>
                        <td bgcolor="#B7E0F3" align="center" class="alertboxTableT">
                            <span class="th-line">欠款额度</span>
                        </td>
                        <td bgcolor="#B7E0F3" align="center" class="alertboxTableT">
                            已确认垫款
                        </td>
                        <td bgcolor="#B7E0F3" align="center" class="alertboxTableT">
                            团队预收
                        </td>
                        <td bgcolor="#B7E0F3" align="center" class="alertboxTableT">
                            支出
                        </td>
                        <td bgcolor="#B7E0F3" align="center" class="alertboxTableT">
                            拖欠金额
                        </td>
                        <td bgcolor="#B7E0F3" align="center" class="alertboxTableT">
                            超限金额
                        </td>
                        <td bgcolor="#B7E0F3" align="center" class="alertboxTableT">
                            超限时间
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
        <div class="hr_10">
        </div>
        <table width="99%" cellspacing="0" cellpadding="0" bgcolor="#e9f4f9" align="center"
            style="margin: 0 auto">
            <tbody>
                <tr>
                    <td height="28" bgcolor="#b7e0f3" align="right" class="alertboxTableT">
                        申请人：
                    </td>
                    <td align="left">
                        <asp:Label ID="lblApplyMan" runat="server" Text=""></asp:Label>
                    </td>
                    <td bgcolor="#b7e0f3" align="right" class="alertboxTableT">
                        垫付金额：
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtApplyPrice" runat="server" CssClass="inputtext formsize80"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td width="14%" height="28" bgcolor="#b7e0f3" align="right" class="alertboxTableT">
                        申请时间：
                    </td>
                    <td width="21%" align="left">
                        <asp:Label ID="lblApplyDateTime" runat="server" Text=""></asp:Label>
                    </td>
                    <td width="12%" bgcolor="#b7e0f3" align="right" class="alertboxTableT">
                        备注：
                    </td>
                    <td width="53%" align="left">
                        <asp:TextBox ID="txtApplyRemarks" runat="server" CssClass="inputtext formsize180"></asp:TextBox>
                    </td>
                </tr>
            </tbody>
        </table>
        <div class="alertbox-btn" style="position: static">
            <a href="javascript:void(0);" id="btnApplyPrice"><s class="baochun"></s>保 存</a><a
                href="javascript:void(0);" id="btnCloseApplyPrice"><s class="chongzhi"></s>关 闭</a>
        </div>
    </div>
    <div class="alertbox-outbox03" id="div_Change" style="display: none; padding-bottom: 0px;">
        <div class="hr_10">
        </div>
        <table width="99%" cellspacing="0" cellpadding="0" bgcolor="#e9f4f9" align="center"
            style="margin: 0 auto">
            <tbody>
                <tr>
                    <td width="80" height="28" align="right" class="alertboxTableT">
                        变更标题：
                    </td>
                    <td bgcolor="#E9F4F9" align="left">
                        <input type="text" id="txt_ChangeTitle" class="inputtext formsize600" style="height: 17px;"
                            name="txt_ChangeTitle">
                    </td>
                </tr>
                <tr>
                    <td width="80" height="28" align="right" class="alertboxTableT">
                        变更明细：
                    </td>
                    <td height="28" bgcolor="#E9F4F9" align="left">
                        <textarea id="txt_ChangeRemark" style="height: 100px;" class="inputtext formsize600"
                            name="txt_ChangeRemark"></textarea>
                    </td>
                </tr>
            </tbody>
        </table>
        <div class="alertbox-btn" style="position: static">
            <a hidefocus="true" href="javascript:void(0);" id="btnChangeSave"><s class="baochun">
            </s>保 存</a><a hidefocus="true" href="javascript:void(0);" onclick="AddTeamPlan.ChangeBox.hide();return false;"><s
                class="chongzhi"></s>关闭</a>
        </div>
    </div>
    <asp:HiddenField ID="hideOverrunState" runat="server" Value="0" />
    <asp:HiddenField ID="hideIsChangeInput" runat="server" Value="0" />
    </form>

    <script type="text/javascript">
        var AddTeamPlan = {
            Data: {
                sl: '<%=Request.QueryString["sl"] %>',
                type: '<%=Request.QueryString["type"] %>',
                act: '<%=Request.QueryString["act"] %>',
                id: '<%=Request.QueryString["id"] %>'
            },
            SaveType: 1,
            ApplyPriceBox: null,
            ChangeBox: null,
            //提交表单
            Save: function() {
                $("#btnSave").text("提交中...");
                AddTeamPlan.UnBindBtn();
                KEditer.sync();
                $.newAjax({
                    type: "post",
                    cache: false,
                    url: "/TeamCenter/AddTeamPlan.aspx?dotype=save&saveType=" + this.SaveType + "&" + $.param(AddTeamPlan.Data),
                    data: $("#btnSave").closest("form").serialize(),
                    dataType: "json",
                    success: function(ret) {
                        //ajax回发提示
                        if (ret.result == "1") {
                            tableToolbar._showMsg(ret.msg, function() {
                                window.location.href = "/TeamCenter/TeamplanList.aspx?sl=" + AddTeamPlan.Data.sl + "&type=" + AddTeamPlan.Data.type;
                            });
                        } else if (ret.result == "2") {
                            AddTeamPlan.Data.id = ret.obj;
                            AddTeamPlan.ShowConfirmMsg(ret.msg);
                        } else {
                            tableToolbar._showMsg(ret.msg);
                            AddTeamPlan.BindBtn();
                        }

                    },
                    error: function() {
                        tableToolbar._showMsg(tableToolbar.errorMsg);
                        AddTeamPlan.BindBtn();
                    }
                });
            },
            //计算价格
            SetSumPrice: function() {
                //成人价
                var adultPrice = tableToolbar.getFloat($("#<%=txtAdultPrice.ClientID%>").val());
                //儿童价
                var childPrice = tableToolbar.getFloat($("#<%=txtChildPrice.ClientID%>").val());
                //成人数
                var adultCount = tableToolbar.getInt($("#<%=txt_Adult.ClientID%>").val());
                //儿童数
                var childCount = tableToolbar.getInt($("#<%=txt_Child.ClientID%>").val());

                //增加费用
                var addPrice = tableToolbar.getFloat($("#<%=txtSuccessAddPrice.ClientID%>").val());
                //减少费用
                var reducePrice = tableToolbar.getFloat($("#<%=txtSuccessReducePrice.ClientID%>").val());
                //导游现收
                var guidePrice = tableToolbar.getFloat($("#<%=txtSuccessGuideIncome.ClientID%>").val());
                //其它费用
                var otherPrice = tableToolbar.getFloat($("#<%=txtOtherPrice.ClientID%>").val());
                //合计金额
                var sunPrice = 0;
                //获取对外报价
                sunPrice = tableToolbar.getFloat(adultPrice * adultCount + childPrice * childCount + addPrice - reducePrice + otherPrice);

                $("#<%=txtSumPrice.ClientID%>").val(sunPrice);
                $("#<%=lblSuccessSalerIncome.ClientID%>").html(sunPrice - guidePrice);
            },
            //计算销售应收
            SetIncome: function() {
                var sumPrice = tableToolbar.getFloat($("#<%=txtSumPrice.ClientID%>").val());
                var guidePrice = tableToolbar.getFloat($("#<%=txtSuccessGuideIncome.ClientID%>").val());
                $("#<%=lblSuccessSalerIncome.ClientID%>").html(sumPrice - guidePrice);
            },
            //按钮绑定事件
            BindBtn: function() {
                //变更
                $("#btnChangeSave").unbind("click").click(function() {
                    if ($.trim($("#txt_ChangeTitle").val()) == "") {
                        tableToolbar._showMsg("变更标题不能为空!");
                        return false;
                    }
                    if ($.trim($("#txt_ChangeRemark").val()) == "") {
                        tableToolbar._showMsg("变更明细不能为空!");
                        return false;
                    }
                    AddTeamPlan.ChangeBox.hide();
                    AddTeamPlan.Save();

                })


                $("#ul_AddTeamPlan_Btn").find("a").css("background-position", "0 0");
                $("#ul_AddTeamPlan_Btn").find("a").each(function() {
                    $(this).html($(this).attr("data-name"));
                })
                $("#btnSave").unbind("click").click(function() {
                    var areaID = $("#sltArea").val();
                    var result = ValiDatorForm.validator($("#<%=txt_Adult.ClientID %>").closest("form").get(0), "alert");
                    if (result) {
                        if (AddTeamPlan.Data.act == "add" || AddTeamPlan.Data.act == "copy") {
                            //if (AddTeamPlan.GetIsOut()) {
                            AddTeamPlan.SaveType = 1;
                            AddTeamPlan.Save();
                            //}
                        } else {
                            if ($("#<%=hideIsChangeInput.ClientID %>").val() == "true") {
                                AddTeamPlan.ChangeBox = new Boxy($("#div_Change"), { modal: true, fixed: false, title: "变更明细", width: "725px", height: "245px", display: "none" });
                            } else {
                                AddTeamPlan.SaveType = 1;
                                AddTeamPlan.Save();
                            }
                        }

                    }
                    return false;
                });
                $("#btnCanel").unbind("click").click(function() {
                    window.history.go(-1);
                    return false;
                })
            },
            //按钮解除绑定
            UnBindBtn: function() {
                $("#ul_AddTeamPlan_Btn").find("a").unbind("click");
                $("#ul_AddTeamPlan_Btn").find("a").css("background-position", "0 -62px");
            },
            //获得选中线路的信息
            GetRouteJson: function(routeId, t) {
                $.newAjax({
                    type: "GET", async: false,
                    url: "/ashx/GetRouteToJson.ashx?routeId=" + routeId,
                    dataType: "json",
                    success: function(r) {
                        if (r.result == "1") {
                            AddTeamPlan.CreateHtmlByRoute(r.data, t);
                        }
                    }
                });
            },
            //自动表单赋值
            CreateHtmlByRoute: function(data, t) {
                if (t == "1") {
                    $("<%=txtAdultPrice.ClientID %>").val(data.AdultPrice);
                    $("#sltArea").val(data.AreaId);
                    $("<%=txtChildPrice.ClientID %>").val(data.ChildrenPrice);
                    $("#<%=txt_Days.ClientID %>").val(data.Days);
                    $("#<%=txtSuccesssStraffBegin.ClientID %>").val((data.DepartureTraffic));
                    $("#<%=txtPlanContent.ClientID %>").val((data.LineIntro));
                    KEditer.html("<%=txtPlanContent.ClientID %>", data.LineIntro);
                    $("#<%=txtOtherPrice.ClientID %>").val(data.OtherPrice);
                    $("#<%=txtSuccesssStraffEnd.ClientID %>").val((data.ReturnTraffic));
                    $("#divForeignQuote").find("textarea[name='txt_ForeignQuote_ZtService']").val((data.Service));
                    $("#<%=txtSuccessGather.ClientID %>").val((data.SetMode));
                    $("#<%=txtSumPrice.ClientID %>").val(data.TotalPrice);
                    $("#<%=hideRouteID.ClientID %>").val(data.RouteId);
                    $("#<%=hideRouteID.ClientID %>").attr("data-value", data.RouteId);
                    $("#<%=txt_RouteName.ClientID %>").val(data.RouteName);
                    $("#<%=txt_RouteName.ClientID %>").attr("data-value", data.RouteName);
                     $("#<%=txtQuoteRemark.ClientID %>").attr("value", data.PathRemark);
                } else {
                    var _$days = $("#<%=txt_Days.ClientID %>");
                    var _days = tableToolbar.getInt(_$days.val());
                    _days = _days + data.Days;
                    _$days.val(_days);
                }

                //行程安排 begin
                $("#<%=txt_Days.ClientID %>").change();
                var JourneyBox = $("#tbl_Journey_AutoAdd");
                var _$xingchengs = JourneyBox.find(".tempRow");
                var _hang = _$xingchengs.length;
                var _hang1 = 0;
                if (data.PlanModelList) _hang1 = data.PlanModelList.length;

                for (var i = 0; i < _hang1; i++) {
                    var _self = _$xingchengs.eq(_hang - _hang1 + i);

                    _self.find("input[name='txt_qujin']").val((data.PlanModelList[i].Section));
                    _self.find("input[name='txtTraffic']").val((data.PlanModelList[i].Traffic));
                    _self.find("input[name='txtHotelName']").val((data.PlanModelList[i].Hotel));
                    //用餐bengin
                    if (data.PlanModelList[i].Breakfast) {
                        _self.find("input[name='eatFrist']").val("1");
                        _self.find("input[type='checkbox'][value='1']").attr("checked", "checked");
                    } else {
                        _self.find("input[name='eatFrist']").val("");
                        _self.find("input[type='checkbox'][value='1']").attr("checked", "");
                    }
                    if (data.PlanModelList[i].Lunch) {
                        _self.find("input[name='eatSecond']").val("1");
                        _self.find("input[type='checkbox'][value='2']").attr("checked", "checked");
                    } else {
                        _self.find("input[name='eatSecond']").val("");
                        _self.find("input[type='checkbox'][value='2']").attr("checked", "");
                    }
                    if (data.PlanModelList[i].Supper) {
                        _self.find("input[name='eatThird']").val("1");
                        _self.find("input[type='checkbox'][value='3']").attr("checked", "checked");
                    } else {
                        _self.find("input[name='eatThird']").val("");
                        _self.find("input[type='checkbox'][value='3']").attr("checked", "");
                    }
                    if (data.PlanModelList[i].Breakfast && data.PlanModelList[i].Supper && data.PlanModelList[i].Lunch) {
                        _self.find("input[type='checkbox'][value='0']").attr("checked", "checked");
                    } else {
                        _self.find("input[type='checkbox'][value='0']").attr("checked", "");
                    }
                    //用餐end

                    _self.find("textarea[name='txtContent']").click();
                    KEditer.html(_self.find("textarea[name='txtContent']").attr("id"), data.PlanModelList[i].Content);

                    //附件
                    if (data.PlanModelList[i].FilePath != "") {
                        _self.find("div[data-class='Journey_upload_swfbox']").find("input[type='hidden']").val("|" + data.PlanModelList[i].FilePath);
                        if (_self.find("div[data-class='span_journey_file']").length > 0) {
                            _self.find("div[data-class='span_journey_file']").remove();
                        }
                        _self.find("div[data-class='Journey_upload_swfbox']").parent().append('<div class="upload_filename" data-class="span_journey_file"><a href="' + data.PlanModelList[i].FilePath + '" target="_blank" title="查看附件">查看附件</a><a href="javascript:void(0);" title="删除附件" onclick="Journey.RemoveFile(this);" ><img src="/images/cha.gif" border="0"></a></div>');
                    }
                    //景点
                    if (data.PlanModelList[i].TourPlanSpot.length > 0) {
                        var spotIdArray = new Array();
                        var spotNameArray = new Array();
                        _self.find("span[data-class='fontblue']").html("");
                        for (var m = 0; m < data.PlanModelList[i].TourPlanSpot.length; m++) {
                            spotIdArray.push(data.PlanModelList[i].TourPlanSpot[m].SpotId);
                            spotNameArray.push((data.PlanModelList[i].TourPlanSpot[m].SpotName));
                            _self.find("span[data-class='fontblue']").append("<span class='upload_filename'><a data-class='a_Journey_Since' data-for='" + data.PlanModelList[i].TourPlanSpot[m].SpotId + "'> " + (data.PlanModelList[i].TourPlanSpot[m].SpotName) + " </a> <a data-for='" + data.PlanModelList[i].TourPlanSpot[m].SpotId + "' href='javascript:void(0);' onclick='Journey.RemoveSince(this)'><img src='/images/cha.gif'></a></span>");
                        }
                        _self.find("input[name='hd_scenery_spot']").val(spotIdArray.join(','));
                        _self.find("input[name='show_scenery_spot']").val(spotNameArray.join(','));
                    }
                }
                //行程安排 end

                //包含项目 bengin
                var IsTourOrSubentry = data.IsTourOrSubentry;
                var divForeignQuote = $("#divForeignQuote");
                if (IsTourOrSubentry) {
                    divForeignQuote.find("input[name='rdo_ForeignQuote_SeviceType']").eq(0).click();
                    divForeignQuote.find("textarea[name='txt_ForeignQuote_ZtService']").val(data.Service);
                } else {
                    divForeignQuote.find("input[name='rdo_ForeignQuote_SeviceType']").eq(1).click();
                    var standardModelListLength = data.StandardModelList.length;
                    if (standardModelListLength > 0) {
                        if (divForeignQuote.find("tr[class='tempRow']").length != standardModelListLength) {
                            for (var m = 0; m < standardModelListLength - 1; m++) {
                                //创建行
                                divForeignQuote.find(".addbtn").eq(0).click();
                            }
                        }

                        divForeignQuote.find(".tempRow").each(function(i) {
                            if (data.StandardModelList[i]) {
                                var $this = $(this);
                                $this.find("select[data-class='slt_ForeignQuote_PlanProject']").find("option").each(function() {
                                    if (this.value.split('|')[0] == data.StandardModelList[i].Type) {
                                        $(this).attr("selected", "selected");
                                        $(this).change();
                                    }

                                })
                                $this.find("textarea[name='txt_ForeignQuote_Service']").val((data.StandardModelList[i].Standard));
                                $this.find("select[name='slt_ForeignQuote_Price']").val(data.StandardModelList[i].Unit);
                                $this.find("input[name='txt_ForeignQuote_Price']").val(data.StandardModelList[i].UnitPrice);
                            }
                        })
                    }
                }
                //包含项目 end

                //行程安排赋值 begin
                var divCostAccontingBox = $("#divCostAcconting");
                divCostAccontingBox.find("textarea[name='txt_CostAccounting_NoNeedItem']").val((data.ServicesModel.NoNeedItem));
                divCostAccontingBox.find("textarea[name='txt_CostAccounting_ShoppingItem']").val((data.ServicesModel.ShoppingItem));
                divCostAccontingBox.find("textarea[name='txt_CostAccounting_ChildServiceItem']").val((data.ServicesModel.ChildServiceItem));
                divCostAccontingBox.find("textarea[name='txt_CostAccounting_OwnExpense']").val((data.ServicesModel.OwnExpense));
                divCostAccontingBox.find("textarea[name='txt_CostAccounting_NeedAttention']").val((data.ServicesModel.NeedAttention));
                divCostAccontingBox.find("textarea[name='txt_CostAccounting_WarmRemind']").val((data.ServicesModel.WarmRemind));
                divCostAccontingBox.find("textarea[name='txt_CostAccounting_InsiderInfor']").val((data.ServicesModel.InsiderInfor));
                //行程安排赋值 end

                //价格赋值 begin
                $("#<%=txtAdultPrice.ClientID %>").val(data.AdultPrice);
                $("#<%=txtChildPrice.ClientID %>").val(data.ChildrenPrice);
                $("#<%=txtOtherPrice.ClientID %>").val(data.OtherPrice);
                $("#<%=txtSumPrice.ClientID %>").val(data.TotalPrice);
                //价格赋值 end

                //签证资料
                if (AddTeamPlan.Data.type == '3') {
                    if (data.VisaInfoList.length > 0) {
                        var str = "";
                        for (var i = 0; i < data.VisaInfoList.length; i++) {
                            str += '<span class="upload_filename">&nbsp;<a target="_blank" href="' + data.VisaInfoList[i].FilePath + '">' + (data.VisaInfoList[i].Name) + '</a><a onclick="AddTeamPlan.RemoveVisaFile(this);return false;" href="javascript:void(0);"><img src="/images/cha.gif" style="vertical-align:middle"></a><input type="hidden" value="' + (data.VisaInfoList[i].Name) + '|' + data.VisaInfoList[i].FilePath + '|' + data.VisaInfoList[i].Downloads + '" name="hideVisaFile"></span>';
                        }
                        $("#<%=lblVisaFiles.ClientID %>").html(str);
                    }

                }

            },
            //删除签证附件
            RemoveVisaFile: function(obj) {
                $(obj).parent().remove();
            },
            CustomerUnitCallBack: function(data) {
                $("#<%=txt_Contact.ClientID %>").val(data.CustomerUnitContactName);
                $("#<%=txt_ConTel.ClientID %>").val(data.CustomerUnitMobilePhone);
                $("#<%=hideContactDeptId.ClientID %>").val(data.CustomerUnitContactId);
            },
            CreatePlanEdit: function() {
                //创建行程编辑器
                KEditer.init('<%=txtPlanContent.ClientID %>');
            },
            PageInit: function() {
                this.CreatePlanEdit();

                $("#xianluxy").click(function() {
                    var areaID = $("#sltArea").val();

                    Boxy.iframeDialog({
                        iframeUrl: "/TeamCenter/selectLine.aspx?callBackFun=AddTeamPlanSelectRouteBackFun&aid=xianluxy&areaID=" + areaID + "&" + $.param(AddTeamPlan.Data),
                        title: "选用线路",
                        modal: true,
                        width: "950px",
                        height: "425px"
                    });
                });

                pcToobar.init({ gID: "#sltCountry", pID: "#sltProvince", gSelect: "<%=CountryID %>", pSelect: "<%=ProvinceID %>", comID: "<%=SiteUserInfo.CompanyId %>" });

                //为分项报价下拉框绑定事件
                $("#divForeignQuote select").change(function() {
                    var adultPrice = 0;
                    var otherPrice = 0;
                    $("#divItems").find("tr[class='tempRow']").each(function() {
                        var tr = $(this);
                        if (tr.find("select[data-class='slt_ForeignQuote_Price']").val() == "0") {
                            adultPrice = tableToolbar.calculate(adultPrice, tr.find("input[name='txt_ForeignQuote_Price']").val(), "+");
                        } else {
                            otherPrice = tableToolbar.calculate(otherPrice, tr.find("input[name='txt_ForeignQuote_Price']").val(), "+");
                        }
                    });

                    if (adultPrice > 0) {
                        $("#<%=txtAdultPrice.ClientID %>").val(adultPrice);
                    } else $("#<%=txtAdultPrice.ClientID %>").val("");
                    if (otherPrice > 0) {
                        $("#<%=txtOtherPrice.ClientID %>").val(otherPrice);
                    } else $("#<%=txtOtherPrice.ClientID %>").val("");

                    //计算金额
                    AddTeamPlan.SetSumPrice();
                });

                $("#divForeignQuote").find("input[name='txt_ForeignQuote_Price']").blur(function() {
                    //计算金额
                    $(this).next().change();
                });

                $("#<%=txtAdultPrice.ClientID%>,#<%=txtChildPrice.ClientID%>,#<%=txt_Adult.ClientID%>,#<%=txt_Child.ClientID%>,#<%=txtSuccessAddPrice.ClientID%>,#<%=txtSuccessReducePrice.ClientID%>,#<%=txtOtherPrice.ClientID%>").blur(function() {
                    //计算金额
                    AddTeamPlan.SetSumPrice();
                });

                $("#btnAddDays").click(function() {
                    var day = tableToolbar.getInt($("#<%=txt_Days.ClientID %>").val());
                    day++;
                    $("#<%=txt_Days.ClientID %>").val(day);
                    $("#<%=txt_Days.ClientID %>").change();
                });

                $("#<%=txtSumPrice.ClientID %>,#<%=txtSuccessGuideIncome.ClientID%>").blur(function() {
                    AddTeamPlan.SetIncome();
                });

                $("#<%=txt_RouteName.ClientID %>").unbind("blur").blur(function() {
                    var _$routename = $(this);
                    var _$routeid = $("#<%=hideRouteID.ClientID %>");
                    if ($.trim(_$routename.val()) != _$routename.attr("data-value")) _$routeid.val("");
                    else _$routeid.val(_$routeid.attr("data-value"));
                });
            },
            ShowConfirmMsg: function(msg) {
                var confirmMsg = msg;
                var _html = '<div style="padding: 10px 0 20px 10px;cursor: default;"><h1 style="margin:10px 5px;"><img src="/images/y-tanhao.gif" style=" vertical-align:middle; margin-right:5px;" />  {{msg}}</h1><input type="button" id="BLOCKUI_NO" value="取 消" " style="width:64px; height:24px; border:0 none; background:url(/images/cx.gif);" /> <input type="button" id="BLOCKUI_SQ" value="超限申请" style="width:64px; height:24px; border:0 none; margin-left:20px; background:url(/images/cx.gif);" /> <input type="button" id="BLOCKUI_SK" value="收 款" style="width:64px; height:24px; border:0 none; margin-left:20px; background:url(/images/cx.gif);" /></div>';
                $.blockUI({
                    message: _html.replace(/{{msg}}/, confirmMsg),
                    css: { backgroundColor: "#E9F4F9", borderColor: "#00446b", borderWidth: '1px', cursor: "pointer", color: "#ed0000", width: '375px' }
                });
                $("#BLOCKUI_SQ").click(function() {
                    $.unblockUI({ onUnblock: function() {
                        Boxy.iframeDialog({
                            iframeUrl: "/TeamCenter/ChaoXianShenQing.aspx?tourId=" + AddTeamPlan.Data.id + "&sl=" + AddTeamPlan.Data.sl + "&url=" + encodeURIComponent("/TeamCenter/TeamplanList.aspx?sl=" + AddTeamPlan.Data.sl + "&type=" + AddTeamPlan.Data.type),
                            title: "垫付申请",
                            modal: true,
                            width: "600px",
                            height: "300px"
                        });
                    }
                    });
                    return false;
                });

                $("#BLOCKUI_SK").click(function() {
                    $.unblockUI({ onUnblock: function() {
                        window.location.href = "/MarketCenter/XiaoShouShouKuan.aspx?sl=<%=(int)EyouSoft.Model.EnumType.PrivsStructure.Privs.销售中心_销售收款_栏目 %>";
                    }
                    });

                    return false;
                });

                $("#BLOCKUI_NO").click(function() {
                    $.unblockUI();
                    window.location.href = window.location.href;
                    return false;
                });
            }
        }

        $(function() {
            //表单验证初始化
            FV_onBlur.initValid($("#<%=txt_Adult.ClientID %>").closest("form").get(0));
            $("#tbl_Journey_AutoAdd").autoAdd({ changeInput: $("#<%=txt_Days.ClientID %>"), addCallBack: Journey.AddRowCallBack, upCallBack: Journey.MoveRowCallBack, downCallBack: Journey.MoveRowCallBack });
            AddTeamPlan.BindBtn();
            AddTeamPlan.PageInit();

            if ("<%=type %>" == "2") $("#<%=txt_Adult.ClientID %>").attr("max", "9999").attr("errmsg", "请输入成人数!|请输入正确的成人数!|成人数必须在1-9999之间!");
        });
        
        //选择线路打开窗口选择或合并事件回调方法 t:1选择线路 t:2合并线路
        function AddTeamPlanSelectRouteBackFun(items, t) {
            if (items && items.length == 0) return;

            if (t == "1") AddTeamPlan.GetRouteJson(items[0].id, "1");
            else if (t == "2") AddTeamPlan.GetRouteJson(items[0].id, "2");
            else return;

            for (var i = 1; i < items.length; i++) {
                AddTeamPlan.GetRouteJson(items[i].id, "2");
            }
        }
        
    </script>

</asp:Content>

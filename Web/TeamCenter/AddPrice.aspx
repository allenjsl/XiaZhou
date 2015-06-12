<%@ Page Title="团队报价" Language="C#" MasterPageFile="~/MasterPage/Front.Master" AutoEventWireup="true"
    CodeBehind="AddPrice.aspx.cs" Inherits="Web.TeamCenter.AddPrice" ValidateRequest="false" %>

<%@ Import Namespace="System.Linq" %>
<%@ Register Src="../UserControl/Journey.ascx" TagName="Journey" TagPrefix="uc1" %>
<%@ Register Src="../UserControl/ForeignQuote.ascx" TagName="ForeignQuote" TagPrefix="uc2" %>
<%@ Register Src="../UserControl/CostAccounting.ascx" TagName="CostAccounting" TagPrefix="uc3" %>
<%@ Register Src="../UserControl/SellsSelect.ascx" TagName="SellsSelect" TagPrefix="uc4" %>
<%@ Register Src="../UserControl/CustomerUnitSelect.ascx" TagName="CustomerUnitSelect"
    TagPrefix="uc5" %>
<%@ Register Src="../UserControl/TravelControl.ascx" TagName="TravelControl" TagPrefix="uc6" %>
<%@ Register Src="../UserControl/UploadControl.ascx" TagName="UploadControl" TagPrefix="uc7" %>
<%@ Register Src="../UserControl/TravelControlS.ascx" TagName="TravelControlS" TagPrefix="uc8" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script src="/Js/kindeditor-4.1/kindeditor-min.js" type="text/javascript"></script>

    <script src="/Js/ValiDatorForm.js" type="text/javascript"></script>

    <script src="/Js/datepicker/WdatePicker.js" type="text/javascript"></script>

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
        .cun-cy a
        {
            background-attachment: scroll;
            background-clip: border-box;
            background-color: transparent;
            background-image: url(/images/cun-cy.gif);
            background-origin: padding-box;
            background-position: 0 0;
            background-repeat: no-repeat;
            background-size: auto auto;
            color: #FFFFFF;
            display: block;
            font-size: 14px;
            font-weight: bold;
            height: 31px;
            line-height: 31px;
            text-align: center;
            width: 103px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form id="form1" runat="server">
    <div class="wrap" id="content">
        <!--内容-->
        <!-- InstanceBeginEditable name="EditRegion3" -->
        <div class="mainbox mainbox-whiteback">
            <div class="addContent-box">
                <asp:PlaceHolder ID="phdPriceCount" runat="server">
                    <div class="tablehead1">
                        <ul class="fixed" id="ulPriceCount">
                            <asp:Repeater ID="rptChildPrice" runat="server">
                                <ItemTemplate>
                                    <li><a data-id="<%#Eval("QuoteId") %>" class="ztorderform" href="/TeamCenter/AddPrice.aspx?id=<%#Eval("QuoteId") %>&sl=<%#sl %>&type=<%#type%>&act=<%#act%>">
                                        <span>第<%#Eval("Times")%>次报价</span></a></li>
                                </ItemTemplate>
                            </asp:Repeater>
                        </ul>
                    </div>
                </asp:PlaceHolder>
                <table width="100%" cellspacing="0" cellpadding="0" class="firsttable">
                    <tbody>
                        <tr>
                            <td width="12%" class="addtableT">
                                <font class="fontbsize12">* </font>线路区域：
                            </td>
                            <td width="20%" class="kuang2">
                                <select class="inputselect" id="sltArea" name="sltArea" valid="isNo" errmsg="请选择线路区域!"
                                    novalue="0">
                                    <asp:Literal ID="litArea" runat="server"></asp:Literal>
                                </select>
                            </td>
                            <td width="12%" class="addtableT">
                                <font class="fontbsize12">*</font> 线路名称：
                            </td>
                            <td width="26%" class="kuang2">
                                <asp:TextBox runat="server" ID="txt_RouteName" CssClass="inputtext formsize180" valid="required"
                                    errmsg="请填写线路名称!" MaxLength="30"></asp:TextBox>
                                <asp:HiddenField ID="hideRouteID" runat="server" />
                                <a id="xianluxy" class="xuanyong" href="javascript:void(0);"></a>
                            </td>
                            <td width="10%" class="addtableT">
                                <font class="fontbsize12">*</font> 天数：
                            </td>
                            <td width="20%" class="kuang2">
                                <asp:TextBox ID="txt_Days" runat="server" CssClass="inputtext formsize40" errmsg="请输入天数!|请输入正确的天数!|天数必须大于0!"
                                    valid="required|isInt|range" min="1"></asp:TextBox>
                                <button class="addtimebtn" type="button" id="btnAddDays">
                                    增加日程</button>
                                <font class="fontbsize12">*</font>
                            </td>
                        </tr>
                        <tr>
                            <td class="addtableT">
                                <font class="fontbsize12">* </font>客源地：
                            </td>
                            <td class="kuang2">
                                <select id="sltCountry" name="sltCountry" class="inputselect" valid="required" errmsg="请选择客源地-国家!">
                                </select>
                                <select id="sltProvince" name="sltProvince" class="inputselect" valid="required"
                                    errmsg="请选择客源地-省份!">
                                </select>
                            </td>
                            <td class="addtableT">
                                <font class="fontbsize12">*</font> 询价单位：
                            </td>
                            <td colspan="3" class="kuang2">
                                <uc5:CustomerUnitSelect ID="UC_CustomerUnitSelect" runat="server" />
                            </td>
                        </tr>
                        <tr>
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
                            <td colspan="3" class="kuang2">
                                <asp:TextBox runat="server" ID="txt_ConTel" CssClass="inputtext formsize120" errmsg="请输入联系电话!|电话格式不正确!"
                                    valid="required|isTel"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="addtableT">
                                销售员：
                            </td>
                            <td class="kuang2">
                                <uc4:SellsSelect ID="UC_SellsSelect" runat="server" SetTitle="销售员" />
                            </td>
                            <td class="addtableT">
                                报价员：
                            </td>
                            <td class="kuang2">
                                <asp:Label ID="lblQuote" runat="server" Text=""></asp:Label>
                            </td>
                            <td class="addtableT">
                                <font class="fontbsize12">*</font> 人数：
                            </td>
                            <td class="kuang2 imgmiddle">
                                <img width="16" height="15" align="absmiddle" src="/images/chengren.gif">成人
                                <asp:TextBox ID="txt_Adult" CssClass="inputtext formsize40" runat="server" errmsg="请输入成人数!|请输入正确的成人数!|成人数必须在1-999之间!"
                                    valid="required|isInt|range" min="1" max="999"></asp:TextBox>
                                &nbsp;
                                <img style="vertical-align: middle" src="/images/child.gif">
                                儿童
                                <asp:TextBox ID="txt_Child" CssClass="inputtext formsize40" runat="server" errmsg="请输入正确的儿童数!|儿童数必须在0-100之间!"
                                    valid="isInt|range" min="0" max="100"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="addtableT">
                                行程特色：
                            </td>
                            <td width="90%" class="kuang2 pand4" colspan="5">
                                <span id="spanPlanContent" style="display: inline-block;">
                                    <asp:TextBox ID="txtPlanContent" CssClass="inputtext" runat="server" TextMode="MultiLine"
                                        Width="800" Height="55"></asp:TextBox></span>
                            </td>
                        </tr>
                        <asp:PlaceHolder ID="phdVisaFile" runat="server">
                            <tr>
                                <td class="addtableT">
                                    签证资料：
                                </td>
                                <td class="kuang2" colspan="5">
                                    <uc7:UploadControl ID="UploadControl1" runat="server" />
                                    <asp:Label ID="lblVisaFiles" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                        </asp:PlaceHolder>
                    </tbody>
                </table>
            </div>
            <div class="hr_5">
            </div>
            <uc1:Journey ID="UC_Journey" runat="server" />
            <div class="hr_5">
            </div>
            <uc2:ForeignQuote ID="UC_ForeignQuote" runat="server" />
            <div class="hr_5">
            </div>
            <table style="width: 98.5%; margin: 0 auto;" cellspacing="0" cellpadding="0" class="add-baojia">
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
                                <asp:CheckBox ID="cbxJdxj" runat="server" value="on" />
                                向计调询价</label>
                            <asp:Label ID="lblToOper" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td bgcolor="#E0F3FC" align="left" class="lefttd">
                            价格备注：
                        </td>
                        <td bgcolor="#E0F3FC" class="kuang2">
                            <asp:TextBox ID="txtQuoteRemark" runat="server" TextMode="MultiLine" CssClass="inputtext inputtext formsize800"></asp:TextBox>
                        </td>
                    </tr>
                </tbody>
            </table>
            <uc3:CostAccounting ID="UC_CostAccounting" runat="server" />
            <div class="mainbox cunline">
                <asp:Literal ID="litMsg" runat="server"></asp:Literal>
                <ul id="ul_AddPrice_Btn">
                    <asp:PlaceHolder ID="phdSave" runat="server">
                        <li class="cun-cy"><a href="javascript:void(0);" id="btnSave" data-name="保存">保存</a></li>
                    </asp:PlaceHolder>
                    <asp:PlaceHolder ID="phdQuote" runat="server">
                        <li class="cun-cy"><a id="btnAddSuccessCx" href="javascript:void(0);" data-name="报价成功">
                            报价成功</a></li>
                    </asp:PlaceHolder>
                    <asp:PlaceHolder ID="phdNewAdd" runat="server">
                        <li class="cun-cy"><a id="btnAddNew" href="javascript:void(0);" data-name="保存为新报价">保存为新报价</a></li>
                    </asp:PlaceHolder>
                    <asp:PlaceHolder ID="phdCanel" runat="server">
                        <li class="quxiao-cy"><a id="btnCanel" href="javascript:void(0);" data-name="取消报价">取消报价</a></li>
                    </asp:PlaceHolder>
                    <li class="cun-cy"><a href="javascript:void(0);" id="btnBackToList">返回列表</a></li>
                    <asp:PlaceHolder ID="phdPrint" runat="server">
                        <li class="cun-cy"><a href="<%=PrintPageZt %>?quoteid=<%=Request.QueryString["id"]%>"
                            id="btnPrint" target="_blank">打印报价单</a></li>
                    </asp:PlaceHolder>
                </ul>
            </div>
            <div class="hr_10">
            </div>
        </div>
        <!-- InstanceEndEditable -->
    </div>
    <div class="alertbox-outbox" id="div_Addprice_SuccessPrice" style="display: none;
        padding-bottom: 0px; width: 950px;">
        <div class="hr_10">
        </div>
        <table width="99%" cellspacing="0" cellpadding="0" bgcolor="#e9f4f9" align="center">
            <tbody>
                <tr>
                    <td width="16%" height="28" bgcolor="#b7e0f3" align="right" class="alertboxTableT">
                        <font class="fontbsize12">*</font> 出团时间：
                    </td>
                    <td width="21%" bgcolor="#E9F4F9" align="left">
                        <asp:TextBox ID="txtSuccessDateBegin" runat="server" CssClass="inputtext formsize120"
                            onfocus="WdatePicker({minDate:'%y-%M-#{%d}',onpicked:function(){AddPrice.WdatePickerBack()}})"></asp:TextBox>
                    </td>
                    <td width="15%" height="28" bgcolor="#B7E0F3" align="right" class="alertboxTableT">
                        <font class="fontbsize12">*</font>返程时间：
                    </td>
                    <td width="48%" height="28">
                        <asp:TextBox ID="txtSuccessDateEnd" runat="server" CssClass="inputtext formsize120"
                            ReadOnly="true" BackColor="#dadada"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td height="28" bgcolor="#b7e0f3" align="right" class="alertboxTableT">
                        出发交通：
                    </td>
                    <td bgcolor="#E9F4F9" align="left">
                        <asp:TextBox ID="txtSuccesssStraffBegin" runat="server" CssClass="inputtext formsize120"></asp:TextBox>
                    </td>
                    <td height="28" bgcolor="#B7E0F3" align="right" class="alertboxTableT">
                        返程交通：
                    </td>
                    <td height="28">
                        <asp:TextBox ID="txtSuccesssStraffEnd" runat="server" CssClass="inputtext formsize120"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td height="28" bgcolor="#b7e0f3" align="right" class="alertboxTableT">
                        集合方式：
                    </td>
                    <td height="28" bgcolor="#E9F4F9" align="left" colspan="3">
                        <asp:TextBox ID="txtSuccessGather" runat="server" CssClass="inputtext formsize600"
                            TextMode="MultiLine" Height="50"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td height="28" bgcolor="#b7e0f3" align="center" class="alertboxTableT" colspan="4">
                        价格组成：成人价<span class="fontred"><strong><asp:Label ID="lblSuccessAdultPrice" runat="server"
                            Text=""></asp:Label></strong></span>*成人数<span class="fontgreen"><strong><asp:Label
                                ID="lblSuccessAdultCount" runat="server" Text=""></asp:Label></strong></span>
                        + 儿童价<span class="fontred"><strong><asp:Label ID="lblSuccessChildPrice" runat="server"
                            Text=""></asp:Label></strong></span>*儿童数<span class="fontgreen"><strong><asp:Label
                                ID="lblSuccessChildCount" runat="server" Text=""></asp:Label></strong></span>
                        + 其它费用<span class="fontred"><strong><asp:Label ID="lblSuccessOtherPrice" runat="server"
                            Text=""></asp:Label></strong></span> 合计金额<span class="fontred"><strong><asp:Label
                                ID="lblSuccessSumPrice" runat="server" Text=""></asp:Label></strong></span>
                    </td>
                </tr>
                <tr>
                    <td height="28" bgcolor="#b7e0f3" align="right" class="alertboxTableT">
                        价格备注：
                    </td>
                    <td height="28" bgcolor="#E9F4F9" align="left" colspan="3">
                        <asp:Label ID="lblSuccessPriceRemark" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td height="28" bgcolor="#b7e0f3" align="right" class="alertboxTableT">
                        增加费用：
                    </td>
                    <td height="28" bgcolor="#E9F4F9" align="left">
                        <asp:TextBox ID="txtSuccessAddPrice" runat="server" CssClass="inputtext formsize80"
                            valid="isMoney" errmsg="增加费用格式不正确!"></asp:TextBox>
                    </td>
                    <td height="28" bgcolor="#B7E0F3" align="right" class="alertboxTableT">
                        备注：
                    </td>
                    <td height="28" bgcolor="#E9F4F9" align="left">
                        <asp:TextBox ID="txtSuccessAddPriceRemark" runat="server" CssClass="inputtext formsize80"
                            Width="260"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td height="28" bgcolor="#b7e0f3" align="right" class="alertboxTableT">
                        减少费用：
                    </td>
                    <td height="28" bgcolor="#E9F4F9" align="left">
                        <asp:TextBox ID="txtSuccessReducePrice" runat="server" CssClass="inputtext formsize80"
                            valid="isMoney" errmsg="减少费用格式不正确!"></asp:TextBox>
                    </td>
                    <td height="28" bgcolor="#B7E0F3" align="right" class="alertboxTableT">
                        备注：
                    </td>
                    <td height="28" bgcolor="#E9F4F9" align="left">
                        <asp:TextBox ID="txtSuccessReducePriceRemark" runat="server" CssClass="inputtext formsize80"
                            Width="260"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td height="28" bgcolor="#b7e0f3" align="right" class="alertboxTableT">
                        导游现收：
                    </td>
                    <td height="28" bgcolor="#E9F4F9" align="left">
                        <asp:TextBox ID="txtSuccessGuideIncome" runat="server" CssClass="inputtext formsize80"
                            valid="isMoney" errmsg="导游现收格式不正确!"></asp:TextBox>
                    </td>
                    <td height="28" bgcolor="#B7E0F3" align="right" class="alertboxTableT">
                        销售应收：
                    </td>
                    <td height="28" bgcolor="#E9F4F9" align="left">
                        <strong class="fontgreen">
                            <asp:Label ID="lblSuccessSalerIncome" runat="server" Text=""></asp:Label></strong>
                    </td>
                </tr>
                <tr>
                    <td height="28" bgcolor="#b7e0f3" align="right" class="alertboxTableT">
                        订单备注：
                    </td>
                    <td height="28" bgcolor="#E9F4F9" align="left" colspan="3">
                        <asp:TextBox ID="txtSuccessOrderRemark" runat="server" CssClass="inputtext formsize600"
                            Height="50" TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
            </tbody>
        </table>
        <div class="hr_10">
        </div>
        <div style="margin: 0 auto; width: 99%; overflow-x: auto;">
            <asp:PlaceHolder ID="phdTravelFrist" runat="server">
                <uc6:TravelControl ID="TravelControl1" runat="server" />
            </asp:PlaceHolder>
            <asp:PlaceHolder ID="phdTavelSecond" runat="server">
                <uc8:TravelControlS ID="TravelControlS1" runat="server" />
            </asp:PlaceHolder>
        </div>
        <div class="alertbox-btn" style="text-align: center; position: static">
            <a href="javascript:void(0);" id="btn_Addprice_SuccessSave"><s class="baochun"></s>保
                存</a> <a href="javascript:void(0);" id="btn_Addprice_SuccessClose"><s class="chongzhi">
                </s>关 闭</a>
        </div>
    </div>
    <div class="alertbox-outbox03" id="div_Canel" style="display: none; padding-bottom: 0px;
        position: static; width: 585px; height: 150px">
        <div class="hr_10">
        </div>
        <table width="99%" cellspacing="0" cellpadding="0" bgcolor="#e9f4f9" align="center"
            style="margin: 0 auto">
            <tbody>
                <tr>
                    <td width="80" height="28" align="right" class="alertboxTableT">
                        取消原因：
                    </td>
                    <td height="28" bgcolor="#E9F4F9" align="left">
                        <textarea style="height: 93px;" class="inputtext formsize450" id="txtCanelRemark"
                            name="txtCanelRemark"></textarea>
                    </td>
                </tr>
            </tbody>
        </table>
        <div class="alertbox-btn" style="position: static">
            <a href="javascript:void(0);" id="btnCanelSave"><s class="baochun"></s>提 交</a><a
                href="javascript:void(0);" onclick="javascript:AddPrice.CanelBox.hide();return false;"><s
                    class="chongzhi"></s>关闭</a>
        </div>
    </div>
    <asp:HiddenField ID="hideOverrunState" runat="server" Value="0" />
    <asp:HiddenField ID="hideFromOperID" runat="server" Value="" />
    <asp:HiddenField ID="hideFristQuoteId" runat="server" Value="" />
    </form>

    <script type="text/javascript">
        var AddPrice = {
            Data: {
                sl: '<%=Request.QueryString["sl"] %>',
                type: '<%=Request.QueryString["type"] %>',
                act: '<%=Request.QueryString["act"] %>',
                id: '<%=Request.QueryString["id"] %>'
            },
            //超限弹窗
            ApplyPriceBox: null,
            //未超弹窗
            SuccessPriceBox: null,
            //取消报价
            CanelBox: null,
            //1=保存,2=报价超限，3=报价未超，4=保存新报价
            SaveType: 1,
            //提交表单
            Save: function() {
                AddPrice.UnBindBtn();
                //同步编辑器数据到文本框
                KEditer.sync();
                if (AddPrice.Data.act == "copy") {
                    AddPrice.Data.id = "";
                }
                $.newAjax({
                    type: "post",
                    cache: false,
                    url: "/TeamCenter/AddPrice.aspx?dotype=save&saveType=" + this.SaveType + "&" + $.param(AddPrice.Data),
                    data: $("#btnBackToList").closest("form").serialize(),
                    dataType: "json",
                    success: function(ret) {
                        //ajax回发提示
                        if (ret.result == "1") {
                            tableToolbar._showMsg(ret.msg, function() {
                                window.location.href = "/TeamCenter/PriceList.aspx?type=" + AddPrice.Data.type + "&sl=" + AddPrice.Data.sl;
                            });
                        } else if (ret.result == "2") {
                            tableToolbar.ShowConfirmMsg(ret.msg, function() {
                                window.location.href = "/MarketCenter/XiaoShouShouKuan.aspx?sl=<%=(int)EyouSoft.Model.EnumType.PrivsStructure.Privs.销售中心_销售收款_栏目 %>";
                            }, function() {
                                //取消就返回列表
                                window.location.href = "/TeamCenter/PriceList.aspx?type=" + AddPrice.Data.type + "&sl=" + AddPrice.Data.sl;
                            })
                        } else {
                            tableToolbar._showMsg(ret.msg);
                            AddPrice.BindBtn();
                        }

                    },
                    error: function() {
                        tableToolbar._showMsg(tableToolbar.errorMsg);
                        AddPrice.BindBtn();
                    }
                });
            },
            //计算价格
            SetSumPrice: function(type) {
                //成人价
                var adultPrice = tableToolbar.getFloat($("#<%=txtAdultPrice.ClientID%>").val());
                //儿童价
                var childPrice = tableToolbar.getFloat($("#<%=txtChildPrice.ClientID%>").val());
                //成人数
                var adultCount = tableToolbar.getInt($("#<%=txt_Adult.ClientID%>").val());
                //儿童数
                var childCount = tableToolbar.getInt($("#<%=txt_Child.ClientID%>").val());
                //其它费用
                var otherPrice = tableToolbar.getFloat($("#<%=txtOtherPrice.ClientID%>").val());
                //合计金额
                var sunPrice = 0;
                //获取对外报价
                sunPrice = tableToolbar.getFloat(adultPrice * adultCount + childPrice * childCount + otherPrice);
                if (type == "1") {
                    $("#<%=txtSumPrice.ClientID%>").val(sunPrice);
                } else {
                    //超限界面的金额显示
                    $("#<%=lblSuccessAdultPrice.ClientID %>").html(adultPrice);
                    $("#<%=lblSuccessAdultCount.ClientID %>").html(adultCount);
                    $("#<%=lblSuccessChildPrice.ClientID %>").html(childPrice);
                    $("#<%=lblSuccessChildCount.ClientID %>").html(childCount);
                    $("#<%=lblSuccessOtherPrice.ClientID %>").html(otherPrice);
                    $("#<%=lblSuccessSumPrice.ClientID %>").html($("#<%=txtSumPrice.ClientID%>").val());
                }
            },
            SetSumPriceTwo: function() {
                //增加费用
                var addPrice = tableToolbar.getFloat($("#<%=txtSuccessAddPrice.ClientID%>").val());
                //减少费用
                var reducePrice = tableToolbar.getFloat($("#<%=txtSuccessReducePrice.ClientID%>").val());
                //导游现收
                var guidePrice = tableToolbar.getFloat($("#<%=txtSuccessGuideIncome.ClientID%>").val());
                //合计金额
                var sunPrice = tableToolbar.getFloat($("#<%=txtSumPrice.ClientID%>").val());
                sunPrice = tableToolbar.getFloat(sunPrice + addPrice - reducePrice);
                $("#<%=lblSuccessSumPrice.ClientID %>").html(sunPrice);
                $("#<%=lblSuccessSalerIncome.ClientID%>").html(tableToolbar.getFloat(sunPrice - guidePrice));
            },
            //按钮绑定事件
            BindBtn: function() {
                //未超限保存
                $("#btn_Addprice_SuccessSave").unbind("click").click(function() {
                    if ($.trim($("#<%=txtSuccessDateBegin.ClientID %>").val()) == "") {
                        tableToolbar._showMsg("请输入出团日期!");
                        return false;
                    }
                    if ($.trim($("#<%=txtSuccessDateEnd.ClientID %>").val()) == "") {
                        tableToolbar._showMsg("请输入回团日期!");
                        return false;
                    }
                    if (ValiDatorForm.validator($("#btnBackToList").closest("form").get(0), "alert")) {
                        $("#<%=txtSumPrice.ClientID%>").val($("#<%=lblSuccessSumPrice.ClientID%>").html());
                        AddPrice.SaveType = 3;
                        AddPrice.SuccessPriceBox.hide();
                        $("#btnAddSuccessCx").html("正在提交");
                        AddPrice.Save();
                    }
                    return false;
                })

                //未超限关闭
                $("#btn_Addprice_SuccessClose").unbind("click").click(function() {
                    $("#div_Addprice_SuccessPrice").find("input[type='text']").val("");
                    $("#div_Addprice_SuccessPrice").find("input[type='textarea']").val("");
                    $("#div_Addprice_SuccessPrice").find("input[type='checkbox']").attr("checked", "");
                    AddPrice.SuccessPriceBox.hide();
                    return false;
                })

                $("#ul_AddPrice_Btn").find("a").css("background-position", "0 0");
                $("#ul_AddPrice_Btn").find("a").each(function() {
                    $(this).html($(this).attr("data-name"));
                })

                $("#ul_AddPrice_Btn").find("a").unbind("click").click(function() {
                    var _s = $(this);
                    var id = _s.attr("id");
                    switch (id) {
                        //保存                                                                                                                                                                                                                      
                        case "btnSave":
                            AddPrice.SaveType = 1;
                            if (ValiDatorForm.validator($("#btnBackToList").closest("form").get(0), "alert")) {
                                _s.html("正在提交..");
                                AddPrice.Save();
                            }
                            break;
                        //报价成功                                                                                                                                                                                                                                     
                        case "btnAddSuccessCx":
                            //验证表单
                            if (ValiDatorForm.validator($("#btnBackToList").closest("form").get(0), "alert")) {
                                AddPrice.SetSumPrice("2");
                                AddPrice.SetSumPriceTwo();
                                $("#<%=lblSuccessPriceRemark.ClientID %>").html($("#<%=txtQuoteRemark.ClientID %>").val());
                                if (AddPrice.SuccessPriceBox) { AddPrice.SuccessPriceBox.hide(); }
                                AddPrice.SuccessPriceBox = new Boxy($("#div_Addprice_SuccessPrice"), { modal: true, fixed: false, title: "报价(未超限)", "z-index": "1000", closeable: false });
                            }
                            break;
                        //添加新的报价                                                                                                            
                        case "btnAddNew":
                            AddPrice.SaveType = 4;
                            if (ValiDatorForm.validator($("#btnBackToList").closest("form").get(0), "alert")) {
                                _s.html("正在提交..");
                                AddPrice.Save();
                            }
                            break;
                        //取消报价                                                                                                                                                                                  
                        case "btnCanel":
                            if (AddPrice.CanelBox) { AddPrice.CanelBox.hide(); }
                            AddPrice.CanelBox = new Boxy($("#div_Canel"), { modal: true, fixed: false, title: "报价(取消)" });
                            break;
                        //打印                                          
                        case "btnPrint":
                            window.open(this.href);
                            break;
                    }

                    return false;
                })

                $("#btnBackToList").click(function() {
                    window.location.href = "/TeamCenter/PriceList.aspx?sl=" + AddPrice.Data.sl + "&type=" + AddPrice.Data.type;
                    return false;
                })

                $("#btnCanelSave").unbind("click").click(function() {
                    var remarks = $.trim($("#txtCanelRemark").val());
                    if (remarks.length > 200) {
                        tableToolbar._showMsg("输入的取消原因过长!");
                        return false;
                    }
                    if (remarks.length == 0) {
                        tableToolbar._showMsg("请输入取消原因!");
                        return false;
                    }
                    AddPrice.CanelBox.hide();
                    AddPrice.SaveType = 5;
                    AddPrice.Save();
                    return false;
                })

            },
            //按钮解除绑定
            UnBindBtn: function() {
                $("#ul_AddPrice_Btn").find("a").unbind("click");
                $("#ul_AddPrice_Btn").find("a").css("background-position", "0 -62px");
            },
            //获得选中线路的信息
            GetRouteJson: function(routeId, t) {
                $.newAjax({
                    type: "GET", async: false,
                    url: "/ashx/GetRouteToJson.ashx?routeId=" + routeId,
                    dataType: "json",
                    success: function(r) {
                        if (r.result == "1") {
                            AddPrice.CreateHtmlByRoute(r.data, t);
                        }
                    }
                });
            },
            //自动表单赋值
            CreateHtmlByRoute: function(data, t) {
                if (t == "1") {
                    $("<%=txtAdultPrice.ClientID %>").val(data.AdultPrice);
                    $("#sltArea").val(data.AreaId);
                    $("#<%=txtChildPrice.ClientID %>").val(data.ChildrenPrice);
                    $("#<%=txt_Days.ClientID %>").val(data.Days);
                    $("#<%=txtPlanContent.ClientID %>").val(data.LineIntro);
                    KEditer.html("<%=txtPlanContent.ClientID %>", data.LineIntro);
                    $("#<%=txtSuccesssStraffBegin.ClientID %>").val((data.DepartureTraffic));
                    $("#<%=txtOtherPrice.ClientID %>").val(data.OtherPrice);
                    $("#<%=txtSuccesssStraffEnd.ClientID %>").val((data.ReturnTraffic));
                    $("#divForeignQuote").find("textarea[name='txt_ForeignQuote_ZtService']").val((data.Service));
                    $("#<%=txtSuccessGather.ClientID %>").val((data.SetMode));
                    $("#<%=txtSumPrice.ClientID %>").val(data.TotalPrice);
                    $("#<%=txtPlanContent.ClientID %>").val((data.TripAdvantage));
                    $("#<%=hideRouteID.ClientID %>").val(data.RouteId);
                    $("#<%=hideRouteID.ClientID %>").attr("data-value", data.RouteId);
                    $("#<%=txt_RouteName.ClientID %>").val(data.RouteName);
                    $("#<%=txt_RouteName.ClientID %>").attr("data-value", data.RouteName);
                    $("#<%=txtQuoteRemark.ClientID %>").attr("value", data.PathRemark);
                } else if (t == "2") {
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
                    divForeignQuote.find("textarea[name='txt_ForeignQuote_ZtService']").val((data.Service));
                } else {
                    divForeignQuote.find("input[name='rdo_ForeignQuote_SeviceType']").eq(1).click();
                    var standardModelListLength = data.StandardModelList.length;
                    if (standardModelListLength > 0) {
                        if (divForeignQuote.find("tr[class='tempRow']").length != standardModelListLength) {
                            for (var m = 0; m < standardModelListLength - 1; m++) {
                                //创建行
                                divForeignQuote.find(".addbtn").eq(0)[0].click();
                            }
                        }

                        divForeignQuote.find(".tempRow").each(function(i) {
                            var $this = $(this);
                            if (data.StandardModelList[i]) {
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
                if (AddPrice.Data.type == '3') {
                    if (data.VisaInfoList.length > 0) {
                        var str = "";
                        for (var i = 0; i < data.VisaInfoList.length; i++) {
                            str += '<span class="upload_filename">&nbsp;<a target="_blank" href="' + data.VisaInfoList[i].FilePath + '">' + (data.VisaInfoList[i].Name) + '</a><a onclick="AddPrice.RemoveVisaFile(this);return false;" href="javascript:void(0);"><img src="/images/cha.gif" style="vertical-align:middle"></a><input type="hidden" value="' + (data.VisaInfoList[i].Name) + '|' + data.VisaInfoList[i].FilePath + '|' + data.VisaInfoList[i].Downloads + '" name="hideVisaFile"></span>';
                        }
                        $("#<%=lblVisaFiles.ClientID %>").html(str);
                    }

                }
            },
            //删除签证附件
            RemoveVisaFile: function(obj) {
                $(obj).parent().remove();
            },
            //向计调询价回调方法
            OperCallBack: function(data) {
                if (data && data.value) {
                    $("#<%=lblToOper.ClientID %>").html("<span class='upload_filename'>&nbsp;计调员:" + data.text + "<a href='javascript:void(0);' onclick='AddPrice.RemoveOper();return false;'> <img style='vertical-align:middle' src='/images/cha.gif'></a><input type='hidden' name='hideToOperID' value='" + data.value + "'></span>")
                } else {
                    $("#<%=cbxJdxj.ClientID %>").attr("checked", "");
                }
            },
            RemoveOper: function() {
                $("#<%=lblToOper.ClientID %>").html("");
                $("#<%=cbxJdxj.ClientID %>").attr("checked", "");
            },
            CustomerUnitCallBack: function(data) {
                $("#<%=txt_Contact.ClientID %>").val(data.CustomerUnitContactName);
                $("#<%=txt_ConTel.ClientID %>").val(data.CustomerUnitMobilePhone);
                $("#<%=hideContactDeptId.ClientID %>").val(data.CustomerUnitContactId);
            },
            UpdateCostCalculation: function(obj) {
                var _self = $(obj);
                var content = _self.prev().val();
                if (content.length == 0) {
                    tableToolbar._showMsg("请输入成本核算内容!");
                    return false;
                }
                if (content.length > 200) {
                    tableToolbar._showMsg("成本核算内容过多!");
                    return false;
                }
                _self.html("报价中...")
                $.newAjax({
                    type: "post",
                    cache: false,
                    url: "/TeamCenter/AddPrice.aspx?dotype=updateCost&" + $.param(AddPrice.Data),
                    dataType: "json",
                    data: { content: content },
                    success: function(ret) {
                        tableToolbar._showMsg(ret.msg);
                        if (ret.result == "1") {
                            _self.html("报价完成").fadeOut("slow");
                        } else {
                            _self.html("报价");
                        }
                    },
                    error: function() {
                        tableToolbar._showMsg(tableToolbar.errorMsg);
                        _self.html("报价");
                    }
                })
            },
            WdatePickerBack: function() {
                var dateStr = $.trim($("#<%=txtSuccessDateBegin.ClientID %>").val());
                if (dateStr != "") {
                    dateStr = dateStr.split('-');
                    var date = new Date(dateStr[0], dateStr[1] - 1, dateStr[2]);
                    date.setDate(date.getDate() + (tableToolbar.getInt($("#<%=txt_Days.ClientID %>").val()) - 1));
                    $("#<%=txtSuccessDateEnd.ClientID %>").val(date.getFullYear() + "-" + (date.getMonth() + 1) + "-" + date.getDate());
                } else {
                    $("#<%=txtSuccessDateEnd.ClientID %>").val("");
                }
            },
            CreatePlanEdit: function() {
                //创建行程编辑器
                KEditer.init('<%=txtPlanContent.ClientID %>');
            },
            PageInit: function() {
                this.CreatePlanEdit();

                $("#tbl_Journey_AutoAdd").autoAdd({ changeInput: $("#<%=txt_Days.ClientID %>"), addCallBack: Journey.AddRowCallBack, upCallBack: Journey.MoveRowCallBack, downCallBack: Journey.MoveRowCallBack });

                $("#xianluxy").click(function() {
                    var areaID = $("#sltArea").val();

                    Boxy.iframeDialog({
                        iframeUrl: "/TeamCenter/selectLine.aspx?callBackFun=AddPriceSelectRouteBackFun&aid=xianluxy&areaID=" + areaID + "&" + $.param(AddPrice.Data),
                        title: "选用线路",
                        modal: true,
                        width: "950px",
                        height: "425px"
                    });
                });

                $("#ulPriceCount").find("a").each(function() {
                    if ($(this).attr("data-id") == '<%=Request.QueryString["id"] %>') {
                        $(this).attr("class", "de-ztorderform").parent().append('<s class="orderformicon"></s>');
                    }
                });

                pcToobar.init({ gID: "#sltCountry", pID: "#sltProvince", gSelect: "<%=CountryID %>", pSelect: "<%=ProvinceID %>" });

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
                    })

                    if (adultPrice > 0) {
                        $("#<%=txtAdultPrice.ClientID %>").val(adultPrice);
                    } else $("#<%=txtAdultPrice.ClientID %>").val("");
                    if (otherPrice > 0) {
                        $("#<%=txtOtherPrice.ClientID %>").val(otherPrice);
                    } else $("#<%=txtOtherPrice.ClientID %>").val("");

                    //计算金额
                    AddPrice.SetSumPrice("1");
                });

                $("#divForeignQuote").find("input[name='txt_ForeignQuote_Price']").blur(function() {
                    //计算金额
                    $(this).next().change();
                });

                $("#<%=txtAdultPrice.ClientID%>,#<%=txtChildPrice.ClientID%>,#<%=txt_Adult.ClientID%>,#<%=txt_Child.ClientID%>,#<%=txtOtherPrice.ClientID%>").blur(function() {
                    //计算金额
                    AddPrice.SetSumPrice("1");
                });

                $("#<%=txtSuccessAddPrice.ClientID%>,#<%=txtSuccessReducePrice.ClientID%>,#<%=txtSuccessGuideIncome.ClientID %>").blur(function() {
                    //计算金额
                    AddPrice.SetSumPriceTwo();
                });

                //向计调询价
                $("#<%=cbxJdxj.ClientID %>").click(function() {
                    var _s = $(this);
                    if (_s.attr("checked")) {
                        Boxy.iframeDialog({
                            iframeUrl: "/CommonPage/OrderSells.aspx?callBackFun=AddPrice.OperCallBack&id=" + _s.attr("id") + "&sModel=1",
                            title: "向计调询价",
                            modal: true,
                            width: "675px",
                            height: "460px",
                            afterHide: function() {
                                if ($("input[name='hideToOperID']").length == 0) {
                                    $("#<%=cbxJdxj.ClientID %>").attr("checked", "");
                                }
                            }
                        });
                    } else {
                        AddPrice.RemoveOper();
                    }
                });

                //添加天数
                $("#btnAddDays").click(function() {
                    var day = tableToolbar.getInt($("#<%=txt_Days.ClientID %>").val());
                    day++;
                    $("#<%=txt_Days.ClientID %>").val(day);
                    $("#<%=txt_Days.ClientID %>").change();
                });

                //如果是向计调询价返回
                if ('<%=Request.QueryString["act"] %>' == "forOper") {
                    $("#divCostAcconting").find("textarea[name='txt_CostAccounting_CostCalculation']").parent().append('<a href="javascript:void(0);" class="ztorderform" onclick="AddPrice.UpdateCostCalculation(this);"><span>报价</span></a>');
                }

                if ($("#TravelControlS_tbl").length > 0) {
                    $("#TravelControlS_tbl").css("width", "115%");
                }

                $("#<%=txt_RouteName.ClientID %>").unbind("blur").blur(function() {
                    var _$routename = $(this);
                    var _$routeid = $("#<%=hideRouteID.ClientID %>");
                    if ($.trim(_$routename.val()) != _$routename.attr("data-value")) _$routeid.val("");
                    else _$routeid.val(_$routeid.attr("data-value"));
                });
            }

        }

        $(function() {
            //表单验证初始化
            FV_onBlur.initValid($("#btnBackToList").closest("form").get(0));
            AddPrice.BindBtn();
            AddPrice.PageInit();
        });

        //选择线路打开窗口选择或合并事件回调方法 t:1选择线路 t:2合并线路
        function AddPriceSelectRouteBackFun(items, t) {
            if (items && items.length == 0) return;

            if (t == "1") AddPrice.GetRouteJson(items[0].id, "1");
            else if (t == "2") AddPrice.GetRouteJson(items[0].id, "2");
            else return;

            for (var i = 1; i < items.length; i++) {
                AddPrice.GetRouteJson(items[i].id, "2");
            }
        }
    </script>

</asp:Content>

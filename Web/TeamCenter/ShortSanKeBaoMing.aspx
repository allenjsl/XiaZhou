﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShortSanKeBaoMing.aspx.cs"
    MasterPageFile="~/MasterPage/Front.Master" Inherits="EyouSoft.Web.TeamCenter.ShortSanKeBaoMing"
    EnableViewState="false" %>

<%@ Register Src="../UserControl/SellsSelect.ascx" TagName="SellsSelect" TagPrefix="uc1" %>
<%@ Register Src="../UserControl/CustomerUnitSelect.ascx" TagName="CustomerUnitSelect"
    TagPrefix="uc2" %>
<%@ Register Src="../UserControl/PriceStand.ascx" TagName="PriceStand" TagPrefix="uc3" %>
<%@ Register Src="../UserControl/TravelControl.ascx" TagName="TravelControl" TagPrefix="uc4" %>
<%@ Register Src="../UserControl/TravelControlS.ascx" TagName="TravelControlS" TagPrefix="uc5" %>
<%@ Register Src="../UserControl/SetSeat.ascx" TagName="SetSeat" TagPrefix="uc6" %>
<%@ Register Src="~/UserControl/HeTongHao.ascx" TagName="HeTongHao" TagPrefix="uc9" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form id="form1" runat="server" enctype="multipart/form-data">
    <div class="mainbox mainbox-whiteback">
        <div class="addContent-box">
            <table width="100%" cellspacing="0" cellpadding="0" class="firsttable">
                <tbody>
                    <tr>
                        <td width="12%" class="addtableT">
                            订单号：
                        </td>
                        <td width="18%" class="kuang2">
                            <asp:Label ID="lblOrderNum" runat="server" Text=""></asp:Label>
                        </td>
                        <td width="12%" class="addtableT">
                            <font class="fontbsize12">*</font> 订单销售员：
                        </td>
                        <td width="18%" class="kuang2">
                            <uc1:SellsSelect ID="SellsSelect1" runat="server" />
                        </td>
                        <td width="12%" class="addtableT">
                            客源地：
                        </td>
                        <td width="18%" class="kuang2">
                            <select class="inputselect" id="sltCountry" name="sltCountry">
                            </select>
                            <select class="inputselect" id="sltProvince" name="sltProvince">
                            </select>
                        </td>
                    </tr>
                    <tr>
                        <td class="addtableT">
                            <font class="fontbsize12">*</font> 客源单位：
                        </td>
                        <td class="kuang2">
                            <uc2:CustomerUnitSelect ID="CustomerUnitSelect1" runat="server" />
                        </td>
                        <td class="addtableT">
                            <font class="fontbsize12">*</font>联系人：
                        </td>
                        <td class="kuang2">
                            <asp:TextBox ID="txtContact" runat="server" class="inputtext formsize80" errmsg="请输入联系人!"
                                valid="required"></asp:TextBox>
                            <asp:HiddenField ID="hideContactDeptId" runat="server" />
                        </td>
                        <td class="addtableT">
                            <font class="fontbsize12">*</font>联系电话：
                        </td>
                        <td class="kuang2">
                            <asp:TextBox ID="txtContactTel" runat="server" class="inputtext formsize80" errmsg="请输入联系电话!|电话格式不正确!"
                                valid="required|isTel"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="addtableT">
                            下单人：
                        </td>
                        <td class="kuang2">
                            <asp:Label ID="lblOrderMan" runat="server" Text=""></asp:Label>
                        </td>
                        <td class="addtableT">
                            <font class="fontbsize12">*</font>人数：
                        </td>
                        <td colspan="3" class="kuang2">
                            <img width="16" height="15" style="vertical-align: middle" src="/images/chengren.gif" />成人
                            <asp:TextBox ID="txtAdultCount" runat="server" class="inputtext formsize40" errmsg="请输入成人数!|请输入正确的成人数!|成人数必须在1-100之间!"
                                valid="required|isInt|range" min="1" max="100"></asp:TextBox>
                            &nbsp;
                            <img style="vertical-align: middle" src="/images/child.gif">
                            儿童
                            <asp:TextBox ID="txtChildCount" runat="server" class="inputtext formsize40" errmsg="请输入正确的儿童数!|儿童数必须在0-100之间!"
                                valid="isInt|range" min="0" max="100"></asp:TextBox>
                        </td>
                    </tr>
                    <asp:PlaceHolder runat="server" ID="ph_PickUpPostion">
                        <tr>
                            <td class="addtableT">
                                上车地点：
                            </td>
                            <td class="kuang2" colspan="5">
                                <select id="selPickUpPosition" class="inputselect" name="selPickUpPosition">
                                    <%=strPickUpPosition %>
                                </select>
                                <input type="hidden" id="hidLocation" name="Location" value="" runat="server" />
                                <input type="hidden" id="hidOnPrice" name="onPrice" value="" runat="server" />
                                <input type="hidden" id="hidOffPrice" name="offPrice" value="" runat="server" />
                                <input type="hidden" id="hidDesc" name="desc" value="" runat="server" />
                                <b>接价：</b> <span class="red">
                                    <asp:Label ID="lbOnPrice" runat="server">-</asp:Label></span> ， <b>送价：</b> <span
                                        class="red">
                                        <asp:Label ID="lbOffPrice" runat="server">-</asp:Label></span> ， <b>描述：</b>
                                <asp:Label ID="lbDesc" runat="server">-</asp:Label>
                            </td>
                        </tr>
                    </asp:PlaceHolder>
                    <asp:PlaceHolder ID="phdSetSeat" runat="server">
                        <tr>
                            <td class="addtableT">
                                座位分配：
                            </td>
                            <td class="kuang2" colspan="5">
                                <uc6:SetSeat ID="SetSeat1" runat="server" SetTitle="座位分配" CallBackFun="OrderInfoPage.SetSeatCallBackFun" />
                            </td>
                        </tr>
                    </asp:PlaceHolder>
                    <tr>
                        <td class="addtableT">
                            <font class="fontbsize12">*</font> 价格组成：
                        </td>
                        <td width="88%" class="kuang2" colspan="5">
                            <div id="tblPrice">
                                <uc3:PriceStand ID="PriceStand1" runat="server" />
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td class="addtableT">
                            增加费用：
                        </td>
                        <td class="kuang2">
                            <asp:TextBox ID="txtAddPrice" runat="server" class="inputtext formsize80" errmsg="请输入正确的增加费用!"
                                valid="isMoney"></asp:TextBox>
                        </td>
                        <td class="addtableT">
                            备注：
                        </td>
                        <td width="55%" class="kuang2" colspan="3">
                            <asp:TextBox ID="txtRemarksFrist" runat="server" class="inputtext formsize450"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="addtableT">
                            减少费用：
                        </td>
                        <td class="kuang2">
                            <asp:TextBox ID="txtReducePrice" runat="server" class="inputtext formsize80" errmsg="请输入正确的减少费用!"
                                valid="isMoney"></asp:TextBox>
                        </td>
                        <td class="addtableT">
                            备注：
                        </td>
                        <td width="55%" class="kuang2" colspan="3">
                            <asp:TextBox ID="txtRemarksSecond" runat="server" class="inputtext formsize450"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td height="14" class="addtableT">
                            <font class="fontbsize12">*</font> 合计金额：
                        </td>
                        <td class="kuang2">
                            <asp:TextBox ID="txtTotalPrice" runat="server" class="inputtext formsize80" errmsg="请输入合计金额!|请输入正确的合计金额"
                                valid="required|isMoney"></asp:TextBox>
                        </td>
                        <td class="addtableT">
                            导游现收：
                        </td>
                        <td class="kuang2">
                            <asp:TextBox ID="txtGuidePrice" runat="server" class="inputtext formsize80" errmsg="请输入正确的导游现收!"
                                valid="isMoney"></asp:TextBox>
                        </td>
                        <td class="addtableT">
                            销售应收：
                        </td>
                        <td class="kuang2">
                            <asp:Label ID="lblSellPrice" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td height="14" class="addtableT">
                            预留截止时间：
                        </td>
                        <td width="18%" class="kuang2">
                            <asp:TextBox ID="txtSaveSeatDate" runat="server" class="inputtext formsize100"></asp:TextBox>
                            <a class="timesicon" href="javascript:void(0);" onclick="WdatePicker({el:'<%=txtSaveSeatDate.ClientID %>',dateFmt:'yyyy-MM-dd HH:mm',maxDate:'<%=this.MaxDateTime %>',minDate:'<%= DateTime.Now.AddMinutes(5).ToString("yyyy-MM-dd HH:mm")%>'});">
                                预留截止时间</a>
                        </td>
                        <td class="addtableT">
                            合同号：
                        </td>
                        <td colspan="3" class="kuang2">
                            <uc9:hetonghao runat="server" id="txtHeTongHao">
                            </uc9:hetonghao>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
        <div style="padding: 0;" class="addContent-box">
            <table width="100%" cellspacing="0" cellpadding="0" class="firsttable">
                <tbody>
                    <tr>
                        <td width="12%" class="addtableT">
                            订单备注：
                        </td>
                        <td width="88%" class="kuang2 pand4">
                            <asp:TextBox ID="txtRemarksOrder" TextMode="MultiLine" runat="server" class="inputtext formsize800"
                                Height="55px"></asp:TextBox>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
        <div class="hr_10">
        </div>
        <div style="width: 98.5%; margin-bottom: 2px; padding-bottom: 2px;" class="tablelist-box ">
            <asp:PlaceHolder ID="pnlTravelFrist" runat="server">
                <uc4:TravelControl ID="TravelControl1" runat="server" />
            </asp:PlaceHolder>
            <asp:PlaceHolder ID="pnlTravelSecond" runat="server">
                <uc5:TravelControlS ID="TravelControlS1" runat="server" />
            </asp:PlaceHolder>
        </div>
        <div class="hr_10">
        </div>
        <div style="width: 98.5%; display: none;" class="tablelist-box" id="divTuiTuan">
            <span class="formtableT formtableT02">退团信息</span>
            <table width="100%" cellspacing="0" cellpadding="0" class="content2" id="tblTuiTuan">
                <tbody>
                    <tr class="addcontentT">
                        <th width="5%" valign="middle" class="th-line">
                            序号
                        </th>
                        <th width="9%" valign="middle" class="th-line">
                            姓名
                        </th>
                        <th width="5%" valign="middle" class="th-line">
                            性别
                        </th>
                        <th width="12%" valign="middle" class="th-line">
                            退团日期
                        </th>
                        <th width="9%" valign="middle" align="right" class="th-line">
                            退款金额
                        </th>
                        <th width="29%" valign="middle" class="th-line">
                            金额说明
                        </th>
                        <th width="31%" valign="middle" class="th-line">
                            退团原因
                        </th>
                    </tr>
                </tbody>
                <tbody class="tempRow">
                    <asp:Repeater ID="rptOutTraveller" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td align="center">
                                    <%#Container.ItemIndex+1 %>
                                </td>
                                <td align="center">
                                    <%#Eval("CnName")%>
                                </td>
                                <td align="center">
                                    <%#Eval("Gender").ToString()%>
                                </td>
                                <td align="center">
                                    <%#Eval("RTime")!=null ?  EyouSoft.Common.UtilsCommons.GetDateString(Eval("RTime"),this.ProviderToDate):""%>
                                </td>
                                <td align="right">
                                    <b class="fontred">
                                        <%#EyouSoft.Common.UtilsCommons.GetMoneyString(Eval("RAmount"),this.ProviderToMoney)%></b>
                                </td>
                                <td align="left">
                                    <%#Eval("RAmountRemark") %>
                                </td>
                                <td align="left">
                                    <%#Eval("RRemark")%>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>
        </div>
        <div class="hr_10">
        </div>
        <div class="mainbox cunline">
            <asp:Literal ID="litOrderMsg" runat="server"></asp:Literal>
            <asp:PlaceHolder ID="pdhAllBtns" runat="server">
                <ul id="ulBtnList">
                    <asp:PlaceHolder ID="pdhBaoCun" runat="server">
                        <li class="cun-cy"><a href="javascript:void(0);" id="btnUpdate" data-name="保 存">保 存</a></li></asp:PlaceHolder>
                    <asp:PlaceHolder ID="phdQuXiao" runat="server">
                        <li class="quxiao-cy"><a href="javascript:void(0);" id="btnCanelOrder" data-name="取消订单">
                            取消订单</a></li></asp:PlaceHolder>
                    <asp:PlaceHolder ID="phdBuShouLi" runat="server">
                        <li class="cun-cy"><a href="javascript:void(0);" id="btnDonot" data-name="不受理">不受理</a></li></asp:PlaceHolder>
                    <asp:PlaceHolder ID="pdhJiXuLiuWei" runat="server">
                        <li class="cun-cy"><a href="javascript:void(0);" id="btnKeep" data-name="继续留位">继续留位</a></li></asp:PlaceHolder>
                    <asp:PlaceHolder ID="phdQueRenYuLiu" runat="server">
                        <li class="cun-cy"><a href="javascript:void(0);" id="btnSaveSeat" data-name="确认预留">确认预留</a></li></asp:PlaceHolder>
                    <asp:PlaceHolder ID="phsQueRenChengJiao" runat="server">
                        <li class="cun-cy"><a href="javascript:void(0);" id="btnSave" data-name="确认成交">确认成交</a></li></asp:PlaceHolder>
                </ul>
            </asp:PlaceHolder>
            <div class="hr_10">
            </div>
        </div>
    </div>
    <asp:HiddenField ID="hideTourType" runat="server" />
    <asp:HiddenField ID="hideTourId" runat="server" />
    <asp:HiddenField ID="hideOrderState" runat="server" />
    <asp:HiddenField ID="hideAdultPrice" runat="server" />
    <asp:HiddenField ID="hideChildPrcie" runat="server" />
    <asp:HiddenField ID="hideLevelID" runat="server" />
    <asp:HiddenField ID="hideSettAdultPrice" runat="server" />
    <asp:HiddenField ID="hideSettChildPrice" runat="server" />
    <asp:HiddenField ID="hideSettLevelID" runat="server" />
    <asp:HiddenField ID="hidePriceStandID" runat="server" />
    <asp:HiddenField ID="hideDeptID" runat="server" />
    <asp:HiddenField ID="hideDeptName" runat="server" />
    <asp:HiddenField ID="hideOrderFrom" runat="server" />
    <asp:HiddenField ID="hideOrderBianGeng" runat="server" />
    <asp:HiddenField ID="hideOldAdultCount" runat="server" />
    <asp:HiddenField ID="hideSourceID" runat="server" />
    <asp:HiddenField ID="hideOldSumMoney" runat="server" Value="0" />
    <asp:HiddenField ID="hideIscarmodel" runat="server" Value="" />
    <asp:HiddenField ID="hideIslocation" runat="server" Value="" />
    </form>

    <script src="/Js/datepicker/WdatePicker.js" type="text/javascript"></script>

    <script src="/Js/ValiDatorForm.js" type="text/javascript"></script>

    <script type="text/javascript">
        var OrderInfoPage = {
            Data: {
                sl: '<%=Request.QueryString["sl"] %>',
                type: '<%=Request.QueryString["type"] %>',
                tourID: '<%=Request.QueryString["tourID"] %>',
                orderID: '<%=Request.QueryString["orderID"] %>',
                dotype: "save",
                saveType: ""
            },
            ReturnUrl: '<%=Request.QueryString["url"] %>',
            CheckForm: function() {
                if ($.trim($("#<%=SetSeat1.LabMsgClientID %>").html()) != "") {
                    tableToolbar._showMsg($("#<%=SetSeat1.LabMsgClientID %>").text());
                    return false;
                }

                //如果有座位设置
                if ($("#<%=this.SetSeat1.setSeatHidClientID %>").length > 0) {
                    var val = $("#<%=this.SetSeat1.setSeatHidClientID %>").val();
                    if (val.length > 0) {
                        var json = $.parseJSON('{"data":' + val + ' }');
                        var count = tableToolbar.getInt($("#<%=txtAdultCount.ClientID %>").val()) + tableToolbar.getInt($("#<%=txtChildCount.ClientID %>").val());
                        if (json.data.length > count) {
                            tableToolbar._showMsg("人数变更,请重新设置座位!");
                            return false;
                        }
                    }
                }

                var form = $("form").get(0);
                return ValiDatorForm.validator(form, "alert");
            },
            SetSumPrice: function() {
                //成人价
                var adultPrice = tableToolbar.getFloat($("#<%=hideAdultPrice.ClientID%>").val());
                //儿童价
                var childPrice = tableToolbar.getFloat($("#<%=hideChildPrcie.ClientID%>").val());
                //成人数
                var adultCount = tableToolbar.getInt($("#<%=txtAdultCount.ClientID%>").val());
                //儿童数
                var childCount = tableToolbar.getInt($("#<%=txtChildCount.ClientID%>").val());
                //增加费用
                var addPrice = tableToolbar.getFloat($("#<%=txtAddPrice.ClientID%>").val());
                //减少费用
                var reducePrice = tableToolbar.getFloat($("#<%=txtReducePrice.ClientID%>").val());
                //导游现收
                var guidePrice = tableToolbar.getFloat($("#<%=txtGuidePrice.ClientID%>").val());
                //上车地点接价
                var onPrice = tableToolbar.getFloat($("#<%=hidOnPrice.ClientID %>").val());
                //上车地点送价
                var offPrice = tableToolbar.getFloat($("#<%=hidOffPrice.ClientID %>").val());
                //合计金额
                var sunPrice = 0;

                sunPrice = adultPrice * adultCount + childPrice * childCount + addPrice - reducePrice + (onPrice + offPrice) * adultCount + (onPrice + offPrice) * childCount;
                $("#<%=txtTotalPrice.ClientID%>").val(tableToolbar.getFloat(sunPrice).toFixed(2));
                $("#<%=lblSellPrice.ClientID%>").html(tableToolbar.getFloat(sunPrice - guidePrice).toFixed(2));
            },
            //计算销售应收
            SetIncome: function() {
                var sumPrice = tableToolbar.getFloat($("#<%=txtTotalPrice.ClientID%>").val());
                var guidePrice = tableToolbar.getFloat($("#<%=txtGuidePrice.ClientID%>").val());
                $("#<%=lblSellPrice.ClientID%>").html(tableToolbar.getFloat(sumPrice - guidePrice).toFixed(2));
            },
            ApplyPriceBox: null,
            PageSave: function() {
                OrderInfoPage.Data.dotype = "save";
                OrderInfoPage.UnBindBtn();
                $.newAjax({
                    type: "post",
                    cache: false,
                    url: "/TeamCenter/ShortSanKeBaoMing.aspx?" + $.param(OrderInfoPage.Data),
                    data: $("form").serialize(),
                    dataType: "json",
                    success: function(ret) {
                        //ajax回发提示
                        if (ret.obj.OrderId) {
                            OrderInfoPage.Data.orderID = ret.obj.OrderId;
                        }

                        if (ret.result == "1") {
                            tableToolbar._showMsg(ret.msg, function() {
                                if (OrderInfoPage.ReturnUrl != "") {
                                    window.location.href = decodeURIComponent(OrderInfoPage.ReturnUrl);
                                } else {
                                    window.history.go(-1);
                                }
                            });
                        } else if (ret.result == "2") {
                            OrderInfoPage.ShowConfirmMsg(ret.msg);
                        }
                        else if (ret.result == "-1") {
                            tableToolbar._showMsg(ret.msg, function() {
                                $("#zwfp_box").click();
                            });
                        } else {
                            tableToolbar._showMsg(ret.msg);
                        }

                        //已被占用的座位
                        if (ret.obj.list && ret.obj.list.length > 0) {
                            var jsonStr = "{\"selectedSeat\":[", tempArr = [];
                            for (var i = 0; i < ret.obj.list.length; i++) {
                                tempArr.push("{ \"SeatNumber\": \"" + ret.obj.list[i].SeatNumber + "\" , \"TourCarTypeId\": \"" + ret.obj.list[i].TourCarTypeId + "\", \"OrderId\":\"" + ret.obj.list[i].OrderId + "\"}")
                            }
                            jsonStr += tempArr.join(',') + "]}";
                            $("#<%=SetSeat1.HidSeatedDataClientID %>").val(jsonStr);
                        }

                        OrderInfoPage.BindBtn();
                    },
                    error: function() {
                        tableToolbar._showMsg(tableToolbar.errorMsg);
                        OrderInfoPage.BindBtn();
                    }
                });
            },
            BindBtn: function() {
                $("#ulBtnList").find("a").each(function() {
                    $(this).html($(this).attr("data-name"));
                })
                $("#ulBtnList").find("a").css("background-position", "0 0")
                $("#ulBtnList").find("a").click(function() {
                    var type = $(this).attr("id");
                    switch (type) {
                        //确认预留                                                                                                                                                                                                                                                                                    
                        case "btnSaveSeat":
                            if (OrderInfoPage.CheckForm()) {
                                if ($.trim($("#<%=txtSaveSeatDate.ClientID %>").val()) == "") {
                                    tableToolbar._showMsg("请输入预留截止时间!");
                                    return false;
                                } else {
                                    OrderInfoPage.Data.saveType = "1";
                                    OrderInfoPage.CheckCarAndLocation($(this));
                                }
                            }
                            break;
                        //确认成交                                                                                                                  
                        case "btnSave":
                            if (OrderInfoPage.CheckForm()) {
                                OrderInfoPage.Data.saveType = "2";
                                OrderInfoPage.CheckCarAndLocation($(this));
                            }
                            break;
                        //继续留位                                                                                                                 
                        case "btnKeep":
                            if (OrderInfoPage.CheckForm()) {
                                var v = $.trim($("#<%=txtSaveSeatDate.ClientID %>").val());
                                if (v == "") {
                                    tableToolbar._showMsg("请输入预留截止时间!");
                                    return false;
                                }
                                OrderInfoPage.Data.saveType = "1";
                                OrderInfoPage.CheckCarAndLocation($(this));
                            }

                            break;
                        //修改                                                                                            
                        case "btnUpdate":
                            if (OrderInfoPage.CheckForm()) {
                                OrderInfoPage.Data.saveType = "4";
                                //如果是供应商计划
                                if ($.trim($("#<%=hideSourceID.ClientID %>").val()) != "") {
                                    var state = $("#<%=hideOrderState.ClientID %>").val();
                                    if (state == "<%=(int)EyouSoft.Model.EnumType.TourStructure.OrderStatus.垫付申请审核成功%>") {
                                        OrderInfoPage.CheckCarAndLocation($(this));
                                    } else {
                                        if (OrderInfoPage.Data.orderID == "") {
                                            OrderInfoPage.CheckCarAndLocation($(this));
                                        }
                                    }
                                } else {
                                    OrderInfoPage.CheckCarAndLocation($(this));
                                }
                            }
                            break;
                        //取消订单                                                                                                
                        case "btnCanelOrder":
                            tableToolbar.ShowConfirmMsg("确定要取消该订单?", function() {
                                $(this).html("正在取消..");
                                OrderInfoPage.Data.saveType = "5";
                                OrderInfoPage.PageSave();
                            })

                            break;
                        //不受理                                                                                              
                        case "btnDonot":
                            tableToolbar.ShowConfirmMsg("确定不受理该订单?", function() {
                                $(this).html("正在提交..");
                                OrderInfoPage.Data.saveType = "6";
                                OrderInfoPage.PageSave();
                            });
                            break;
                        case "btnPrint": break;
                    }
                })

            },
            CheckCarAndLocation: function(self) {
                var Iscarmodel = $("#<%=hideIscarmodel.ClientID %>").val();
                var Islocation = $("#<%=hideIslocation.ClientID %>").val();
                if (Iscarmodel == "Show" && Islocation == "Show" && $("#selPickUpPosition").val() == "" && $("#<%=SetSeat1.setSeatHidClientID %>").val().length <= 2) {
                    tableToolbar.ShowConfirmMsg("确定不选择上车地点和车型么？", function() {
                        self.html("正在提交..");
                        OrderInfoPage.PageSave();
                    });
                } else if (Iscarmodel == "Show" && Islocation == "" && $("#<%=SetSeat1.setSeatHidClientID %>").val().length <= 2) {
                    tableToolbar.ShowConfirmMsg("确定不选择车型么？", function() {
                        self.html("正在提交..");
                        OrderInfoPage.PageSave();
                    });
                } else if (Islocation == "Show" && Iscarmodel == "" && $("#selPickUpPosition").val() == "") {
                    tableToolbar.ShowConfirmMsg("确定不选择上车地点么？", function() {
                        self.html("正在提交..");
                        OrderInfoPage.PageSave();
                    });
                } else {
                    self.html("正在提交..");
                    OrderInfoPage.PageSave();
                }
            },
            UnBindBtn: function() {
                $("#ulBtnList").find("a").css("background-position", "0 -62px")
                $("#ulBtnList").find("a").unbind("click");
            },
            CallBackFun: function(data) {
                $("#<%=SellsSelect1.SellsIDClient%>").val(data.value);
                $("#<%=SellsSelect1.SellsNameClient%>").val(data.text);
                $("#<%=hideDeptID.ClientID%>").val(data.deptID);
                $("#<%=hideDeptName.ClientID%>").val(data.deptName);
            },
            CustomerUnitCallBack: function(data) {
                $("#<%=txtContact.ClientID %>").val(data.CustomerUnitContactName);
                $("#<%=txtContactTel.ClientID %>").val(data.CustomerUnitMobilePhone);
                $("#<%=hideContactDeptId.ClientID %>").val(data.CustomerUnitContactId);
                if (data.CustomerUnitLV) {
                    $("#tblPrice").find("input[type='radio']").each(function() {
                        if (this.value.split('|')[0] == data.CustomerUnitLV) {
                            $(this).click();
                            return false;
                        }
                    })
                }
            },
            SetSeatCallBackFun: function(data) {
                $("#<%=this.SetSeat1.setSeatHidClientID %>").val(data.seatData);
                if (data.seatData.length > 2) {
                    $("#zwfp_box").addClass("select");

                } else {
                    $("#zwfp_box").removeClass("select");
                }
            },
            InitSelectLev: function() {
                if (OrderInfoPage.Data.tourID == "") {
                    OrderInfoPage.Data.tourID = $("#<%=hideTourId.ClientID %>").val();
                }

                var lev = $("#<%=hideLevelID.ClientID%>").val();
                var standId = $("#<%=hidePriceStandID.ClientID%>").val();
                if (lev != "" && standId != "") {
                    $("#tblPrice").find("span[data-class='standard'][data-id='" + standId + "']").eq(0).closest("tr").find("input[type='radio']").each(function() {
                        var value = this.value.split('|');
                        if (value.length > 0) {
                            if (value[0] == lev) {
                                this.checked = true;
                            }
                        }
                    })
                }

                //如果订单已经确认
                if ($("#<%=hideOrderState.ClientID %>").val() == "<%=(int)EyouSoft.Model.EnumType.TourStructure.OrderStatus.已成交 %>") {
                    var box = $("#TravelControl_tbl").length > 0 ? $("#TravelControl_tbl") : $("#TravelControlS_tbl");
                    if (box.length > 0) {
                        box.find("a[class='addbtn']").each(function() {
                            var td = $(this).parent();
                            if (td.parent().find("td").eq(0).find("input[type='hidden']").val() != "") {
                                td.append("<a class='tuituan' data-class='tuituan' onclick='OrderInfoPage.OpenTuiTuan(this);return false;' href='javascript:void(0);'><img src='/images/tuituan.gif'></a>");
                            }
                        })
                    }
                }

                //如果存在退团，则显示
                if ($("#tblTuiTuan").find("tr").length > 1) {
                    $("#divTuiTuan").css("display", "block");
                }

                //如果团已派计调
                if ($("#<%=hideOrderBianGeng.ClientID %>").val() == "true") {
                    if (OrderInfoPage.Data.orderID != "") {
                        $("#btnUpdate").html("变 更");
                        $("#btnUpdate").attr("data-name", "变 更");
                    }
                }

                $("#<%=txtTotalPrice.ClientID %>,#<%=txtGuidePrice.ClientID %>").blur(function() {
                    OrderInfoPage.SetIncome();
                })

                //如果存在座位设置，且大于0
                if ($("#zwfp_box").length > 0) {
                    if ($("#<%=SetSeat1.setSeatHidClientID %>").val() != "") {
                        $("#zwfp_box").addClass("select");
                    }
                }
            },
            TuiTuanCallBack: function(data) {
                if (data) {
                    var box = $("#TravelControl_tbl").length > 0 ? $("#TravelControl_tbl") : $("#TravelControlS_tbl");
                    box.find("#" + data.aid).closest("tr").remove();
                    if ($("#divTuiTuan").css("display") == "none") {
                        $("#divTuiTuan").fadeIn("slow");
                    }
                    var tr = '<tr><td align="center">' + $("#tblTuiTuan").find("tr").length + '</td><td align="center">' + data.name + '</td><td align="center">' + data.sex + '</td><td align="center">' + data.time + '</td><td align="right"><b class="fontred">' + data.money + '</b></td><td align="left">' + data.mks + '</td><td align="left">' + data.remarks + '</td></tr>';
                    $("#tblTuiTuan").append(tr);

                }
            },
            OpenTuiTuan: function(o) {
                var _s = $(o);
                _s.attr("id", "aid" + parseInt(Math.random() * 10000));
                var tr = _s.closest("tr");
                var data = { aid: "", id: "", callbackfun: "OrderInfoPage.TuiTuanCallBack", sl: "", orderId: "", tourId: "" };
                data.aid = _s.attr("id");
                data.id = tr.find("td").eq(0).find("input[type='hidden']").val();
                data.sl = OrderInfoPage.Data.sl;
                data.orderId = OrderInfoPage.Data.orderID;
                data.tourId = OrderInfoPage.Data.tourID;
                Boxy.iframeDialog({
                    iframeUrl: "/TeamCenter/TuiTuan.aspx?" + $.param(data),
                    title: "游客退团",
                    modal: true,
                    width: "625px",
                    height: "275px"
                });
            },
            OterBtnBind: function() {
                $("#tblPrice").find("input[type='radio']").click(function() {
                    var tr = $(this).closest("tr");
                    //获得销售价
                    $("#<%=hideAdultPrice.ClientID%>").val($(this).val().split('|')[1]);
                    $("#<%=hideChildPrcie.ClientID%>").val($(this).val().split('|')[2]);
                    $("#<%=hideLevelID.ClientID%>").val($(this).val().split('|')[0]);
                    $("#<%=hidePriceStandID.ClientID%>").val(tr.find("span[data-class='standard']").attr("data-id"));

                    //定位结算价
                    tr.find("input[name='txt_PriceStand_radio_price']").each(function() {
                        var value = $(this).val().split('|');
                        if (value.length == 3) {
                            if (value[0] == $("#<%=hideSettLevelID.ClientID %>").val()) {
                                $("#<%=hideSettAdultPrice.ClientID %>").val(value[1]);
                                $("#<%=hideSettChildPrice.ClientID %>").val(value[2]);
                            }
                        }
                    })


                    OrderInfoPage.SetSumPrice();
                })
                $("#<%=txtAdultCount.ClientID %>,#<%=txtChildCount.ClientID %>,#<%=txtAddPrice.ClientID %>,#<%=txtReducePrice.ClientID %>").blur(function() {
                    OrderInfoPage.SetSumPrice();
                })
                $("#<%=txtAdultCount.ClientID %>,#<%=txtChildCount.ClientID %>").blur(function() {
                    $("#<%=SetSeat1.PeoNumClientID %>").val(parseInt($("#<%=txtAdultCount.ClientID %>").val() || 0) + parseInt($("#<%=txtChildCount.ClientID %>").val() || 0));
                })

                //上车地点选择
                $("#selPickUpPosition").change(function() {
                    var self = $(this), closesttd = self.closest("td");
                    var onprice = tableToolbar.getFloat(self.find("option:selected").attr("data-onprice")),
                offprice = tableToolbar.getFloat(self.find("option:selected").attr("data-offprice")),
                desc = self.find("option:selected").attr("data-desc"), location = self.find("option:selected").attr("data-location");
                    if (self.find("option:selected").val() != "") {

                        $("#<%=lbDesc.ClientID %>").text(desc);
                        $("#<%=hidDesc.ClientID %>").val(desc);
                    }
                    else {
                        $("#<%=lbOnPrice.ClientID %>").text("-");
                        $("#<%=lbOffPrice.ClientID %>").text("-");
                        $("#<%=lbDesc.ClientID %>").text("-");
                    }
                    $("#<%=lbOnPrice.ClientID %>").text(onprice);
                    $("#<%=lbOffPrice.ClientID %>").text(offprice);
                    $("#<%=hidOnPrice.ClientID %>").val(onprice);
                    $("#<%=hidOffPrice.ClientID %>").val(offprice);
//                    alert(location)
                    $("#<%=hidLocation.ClientID %>").val(location);
                    OrderInfoPage.SetSumPrice();
                })

            },
            //超限确认：取消，收款，申请
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
                            iframeUrl: "/TeamCenter/ChaoXianShenQing.aspx?orderId=" + OrderInfoPage.Data.orderID + "&sl=" + OrderInfoPage.Data.sl + "&url=" + encodeURIComponent(OrderInfoPage.ReturnUrl),
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
                })

                $("#BLOCKUI_NO").click(function() {
                    $.unblockUI();
                    if (OrderInfoPage.ReturnUrl != "") {
                        window.location.href = decodeURIComponent(OrderInfoPage.ReturnUrl);
                    } else {
                        window.history.go(-1);
                    }
                    return false;
                })
            }
        }

        $(function() {
            FV_onBlur.initValid($("form").get(0));

            pcToobar.init({ gID: "#sltCountry", pID: "#sltProvince", gSelect: "<%=CountryID %>", pSelect: "<%=ProvinceID %>" });

            OrderInfoPage.BindBtn();

            OrderInfoPage.InitSelectLev();

            OrderInfoPage.OterBtnBind();
        })

        
    </script>

</asp:Content>

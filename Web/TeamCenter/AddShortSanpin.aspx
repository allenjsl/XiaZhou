<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddShortSanpin.aspx.cs"
    ValidateRequest="false" MasterPageFile="~/MasterPage/Front.Master" Inherits="EyouSoft.Web.TeamCenter.AddShortSanpin" %>

<%@ Register Src="../UserControl/PriceStand.ascx" TagName="PriceStand" TagPrefix="uc1" %>
<%@ Register Src="../UserControl/Journey.ascx" TagName="Journey" TagPrefix="uc2" %>
<%@ Register Src="../UserControl/CostAccounting.ascx" TagName="CostAccounting" TagPrefix="uc3" %>
<%@ Register Src="../UserControl/SellsSelect.ascx" TagName="SellsSelect" TagPrefix="uc4" %>
<%@ Register Src="../UserControl/UploadControl.ascx" TagName="UploadControl" TagPrefix="uc8" %>
<%@ Register Src="../UserControl/PickUpPoint.ascx" TagName="PickUpPoint" TagPrefix="uc10" %>
<%@ Register Src="../UserControl/PresetBusType.ascx" TagName="PresetBusType" TagPrefix="uc5" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script src="/Js/kindeditor-4.1/kindeditor-min.js" type="text/javascript"></script>

    <script src="/Js/datepicker/WdatePicker.js" type="text/javascript"></script>

    <script src="/Js/ValiDatorForm.js" type="text/javascript"></script>

    <link href="/Css/swfupload/default.css" rel="stylesheet" type="text/css" />

    <script src="/Js/swfupload/swfupload.js" type="text/javascript"></script>

    <style>
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form id="form1" runat="server">
    <div class="wrap">
        <!--内容-->
        <div class="mainbox mainbox-whiteback">
            <div class="addContent-box">
                <table width="100%" cellspacing="0" cellpadding="0" class="firsttable">
                    <tbody>
                        <tr>
                            <td width="12%" class="addtableT">
                                是否同行分销：
                            </td>
                            <td width="23%" class="kuang2">
                                <asp:CheckBox ID="cbxDistribution" runat="server" />
                            </td>
                            <td width="10%" class="addtableT">
                                <font class="fontbsize12">* </font>线路区域：
                            </td>
                            <td width="15%" class="kuang2">
                                <select class="inputselect" id="sltArea" name="sltArea" valid="isNo" errmsg="请选择线路区域"
                                    novalue="0">
                                    <asp:Literal ID="litArea" runat="server"></asp:Literal>
                                </select>
                            </td>
                            <td width="10%" class="addtableT">
                                <font class="fontbsize12">* </font>线路名称：
                            </td>
                            <td width="30%" class="kuang2">
                                <asp:TextBox runat="server" ID="txt_RouteName" CssClass="inputtext formsize180" valid="required"
                                    errmsg="请填写线路名称" MaxLength="30"></asp:TextBox>
                                <asp:HiddenField ID="hideRouteID" runat="server" />
                                <a id="xianluxy" class="xuanyong" href="javascript:void(0);"></a>
                            </td>
                        </tr>
                        <tr>
                            <td class="addtableT">
                                <font class="fontbsize12">*</font> 天数：
                            </td>
                            <td class="kuang2">
                                <asp:TextBox ID="txt_Days" runat="server" CssClass="inputtext formsize40" errmsg="请输入天数!|请输入正确的天数!|天数必须大于0!"
                                    valid="required|isInt|range" min="1"></asp:TextBox>
                                <button class="addtimebtn" type="button" id="btnAddDays">
                                    增加日程</button>
                            </td>
                            <td class="addtableT">
                                <font class="fontbsize12">* </font>预控人数：
                            </td>
                            <td class="kuang2">
                                <asp:TextBox ID="txtPeopleCount" runat="server" CssClass="inputtext formsize40" errmsg="请输入预控人数!|请输入正确的预控人数!|预控人数必须在1-100之间!"
                                    valid="required|isInt|range" min="1" max="100"></asp:TextBox>
                            </td>
                            <td class="addtableT">
                                <asp:Label ID="lblLeaveDateMore" runat="server" Text="发班周期："></asp:Label>
                            </td>
                            <td class="kuang2">
                                <asp:Label ID="lblLeaveDate" runat="server" Text=""></asp:Label>
                                <asp:HiddenField ID="hideLeaveDate" runat="server" />
                                <asp:PlaceHolder ID="phdSelectDate" runat="server"><a style="font-weight: bold; color: #fff;
                                    background-color: #ff6600; display: block; width: 80px; height: 17px; text-align: center;
                                    padding-top: 5px;" id="a_SelectData" href="javascript:void(0);">请选择日期</a></asp:PlaceHolder>
                            </td>
                        </tr>
                        <tr>
                            <td class="addtableT">
                                出发交通：
                            </td>
                            <td class="kuang2 pand4" colspan="5">
                                <asp:TextBox ID="txtSuccesssStraffBegin" runat="server" CssClass="inputtext formsize120"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="addtableT">
                                返程交通：
                            </td>
                            <td class="kuang2 pand4" colspan="5">
                                <asp:TextBox ID="txtSuccesssStraffEnd" runat="server" CssClass="inputtext formsize120"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="addtableT">
                                上车地点：
                            </td>
                            <td class="kuang2 pand4" colspan="5">
                                <uc10:PickUpPoint ID="PickUpPoint1" runat="server" SetTitle="上车地点" SModel="2" CallBackFun="AddSanPlan.AppendLi" />
                            </td>
                        </tr>
                        <tr>
                            <td class="addtableT">
                                集合方式：
                            </td>
                            <td class="kuang2 pand4" colspan="5">
                                <asp:TextBox ID="txtSuccessGather" runat="server" CssClass="inputtext formsize600"
                                    TextMode="MultiLine" Height="50"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="addtableT">
                                附件上传：
                            </td>
                            <td class="kuang2" colspan="5">
                                <uc8:UploadControl ID="UploadControl2" runat="server" />
                                <asp:Label ID="lblFiles" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="addtableT">
                                <font class="fontbsize12">* </font>销售员：
                            </td>
                            <td class="kuang2" colspan="3">
                                <uc4:SellsSelect ID="SellsSelect1" runat="server" SetTitle="销售员" />
                            </td>
                            <td class="addtableT">
                                提前停收天数：
                            </td>
                            <td class="kuang2">
                                <asp:TextBox ID="txtStopDate" runat="server" CssClass="inputtext formsize40"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="addtableT">
                                行程特色：
                            </td>
                            <td class="kuang2 pand4" colspan="5">
                                <asp:TextBox ID="txtPlanContent" name="txtPlanContent" runat="server" TextMode="MultiLine"
                                    CssClass="inputtext formsize800" Height="55"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="addtableT">
                                关键字：
                            </td>
                            <td class="kuang2 pand4" colspan="5">
                                <asp:TextBox ID="txtSearchKey" runat="server" Height="17px" Width="600px" CssClass="inputtext"></asp:TextBox>
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
            <div class="hr_5">
            </div>
            <div style="width: 98.5%" class="tablelist-box " runat="server" id="DivPrisetBus">
                <uc5:PresetBusType ID="PresetBusType1" runat="server" />
            </div>
            <div class="hr_5">
            </div>
            <div style="width: 98.5%" class="tablelist-box ">
                <uc1:PriceStand ID="PriceStand1" runat="server" />
            </div>
            <div class="hr_5">
            </div>
            <uc2:Journey ID="Journey1" runat="server" />
            <div class="hr_5">
            </div>
            <uc3:CostAccounting ID="CostAccounting1" runat="server" />
            <div class="mainbox cunline">
                <ul id="ul_AddSanPlan_Btn">
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
        <!-- InstanceEndEditable -->
    </div>
    <div class="alertbox-outbox03" id="div_Change" style="display: none">
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
                        <input type="text" id="txt_ChangeTitle" class="inputtext formsize600" name="txt_ChangeTitle"
                            style="height: 17px;">
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
            </s>保 存</a><a hidefocus="true" href="javascript:void(0);" onclick="AddSanPlan.ChangeBox.hide();return false;"><s
                class="chongzhi"></s>关闭</a>
        </div>
    </div>
    <asp:HiddenField ID="hideOverrunState" runat="server" Value="0" />
    <asp:HiddenField ID="hideSysStopCount" runat="server" />
    <asp:HiddenField ID="hideIsChangeInput" runat="server" />
    </form>

    <script type="text/javascript">

        var AddSanPlan = {
            Data: {
                sl: '<%=Request.QueryString["sl"] %>',
                type: '<%=Request.QueryString["type"] %>',
                act: '<%=Request.QueryString["act"] %>',
                id: '<%=Request.QueryString["id"] %>'
            },
            ChangeBox: null,
            //提交表单
            Save: function() {
                PriceStand.Init();
                KEditer.sync();
                $("#btnSave").text("提交中...");
                AddSanPlan.UnBindBtn();
                $.newAjax({
                    type: "post",
                    cache: false,
                    url: "/TeamCenter/AddShortSanpin.aspx?dotype=save&" + $.param(AddSanPlan.Data),
                    data: $("#btnSave").closest("form").serialize(),
                    dataType: "json",
                    success: function(ret) {
                        //ajax回发提示
                        if (ret.result == "1") {
                            tableToolbar._showMsg(ret.msg, function() {
                                window.location.href = "/TeamCenter/SanpinList.aspx?sl=" + AddSanPlan.Data.sl + "&type=" + AddSanPlan.Data.type;
                            });
                        } else {
                            tableToolbar._showMsg(ret.msg);
                            AddSanPlan.BindBtn();
                        }
                    },
                    error: function() {
                        tableToolbar._showMsg(tableToolbar.errorMsg);
                        AddSanPlan.BindBtn();
                    }
                });
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
                    AddSanPlan.ChangeBox.hide();
                    AddSanPlan.Save();

                })

                $("#ul_AddSanPlan_Btn").find("a").css("background-position", "0 0");
                $("#ul_AddSanPlan_Btn").find("a").each(function() {
                    $(this).html($(this).attr("data-name"));
                })

                $("#btnSave").unbind("click").click(function() {
                    var result = ValiDatorForm.validator($("#<%=txt_RouteName.ClientID %>").closest("form").get(0), "alert");
                    if (result) {
                        if ($("#<%=hideLeaveDate.ClientID %>").val() == "") {
                            tableToolbar._showMsg("请选择出团日期!");
                            return false;
                        }

                        var PostFun = function() {
                            if ($("#<%=hideIsChangeInput.ClientID %>").val() == "true" && AddSanPlan.Data.act == "update") {
                                AddSanPlan.ChangeBox = new Boxy($("#div_Change"), { modal: true, fixed: false, title: "变更明细", width: "725px", height: "245px", display: "none" });
                            } else {
                                AddSanPlan.SaveType = 1;
                                AddSanPlan.Save();
                            }
                        }

                        if ($("#<%=PickUpPoint1.PickUpIDClientID %>").val() == "" && $("#<%=PresetBusType1.hidCarTypeIdClientID %>").val() == "") {
                            tableToolbar.ShowConfirmMsg("确定不选择上车地点和车型么？", function() {
                                PostFun();
                            });
                        }
                        else if ($("#<%=PickUpPoint1.PickUpIDClientID %>").val() != "" && $("#<%=PresetBusType1.hidCarTypeIdClientID %>").val() == "") {
                            tableToolbar.ShowConfirmMsg("确定不选择车型么？", function() {
                                PostFun();
                            });
                        }
                        else if ($("#<%=PickUpPoint1.PickUpIDClientID %>").val() == "" && $("#<%=PresetBusType1.hidCarTypeIdClientID %>").val() != "") {
                            tableToolbar.ShowConfirmMsg("确定不选择上车地点么？", function() {
                                PostFun();
                            });
                        }
                        else {
                            PostFun();
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
                $("#ul_AddSanPlan_Btn").find("a").unbind("click");
                $("#ul_AddSanPlan_Btn").find("a").css("background-position", "0 -62px");
            },
            //获得选中线路的信息
            GetRouteJson: function(routeId, t) {
                $.newAjax({
                    type: "GET", async: false,
                    url: "/ashx/GetRouteToJson.ashx?routeId=" + routeId,
                    dataType: "json",
                    success: function(r) {
                        if (r.result == "1") {
                            AddSanPlan.CreateHtmlByRoute(r.data, t);
                        }
                    }
                });
            },
            //自动表单赋值
            CreateHtmlByRoute: function(data, t) {
                if (t == "1") {
                    $("#sltArea").val(data.AreaId);
                    $("#<%=txt_Days.ClientID %>").val(data.Days);
                    $("#<%=txtSuccesssStraffBegin.ClientID %>").val((data.DepartureTraffic));
                    $("#<%=txtSuccesssStraffEnd.ClientID %>").val((data.ReturnTraffic));
                    $("#<%=txtPlanContent.ClientID %>").val((data.LineIntro));
                    KEditer.html("<%=txtPlanContent.ClientID %>", data.LineIntro);
                    $("#divForeignQuote").find("textarea[name='txt_ForeignQuote_ZtService']").val((data.Service));
                    $("#<%=txtSuccessGather.ClientID %>").val((data.SetMode));
                    $("#<%=hideRouteID.ClientID %>").val(data.RouteId);
                    $("#<%=hideRouteID.ClientID %>").attr("data-value", data.RouteId);
                    $("#<%=txt_RouteName.ClientID %>").val(data.RouteName);
                    $("#<%=txt_RouteName.ClientID %>").attr("data-value", data.RouteName);
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
                    _self.find("textarea[name='txtContent']").val((data.PlanModelList[i].Content));

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

                //行程安排赋值 begin
                var divCostAccontingBox = $("#divCostAcconting");
                divCostAccontingBox.find("textarea[name='txt_CostAccounting_NoNeedItem']").val((data.ServicesModel.NoNeedItem));
                divCostAccontingBox.find("textarea[name='txt_CostAccounting_ShoppingItem']").val((data.ServicesModel.ShoppingItem));
                divCostAccontingBox.find("textarea[name='txt_CostAccounting_ChildServiceItem']").val((data.ServicesModel.ChildServiceItem));
                divCostAccontingBox.find("textarea[name='txt_CostAccounting_OwnExpense']").val((data.ServicesModel.OwnExpense));
                divCostAccontingBox.find("textarea[name='txt_CostAccounting_NeedAttention']").val((data.ServicesModel.NeedAttention));
                divCostAccontingBox.find("textarea[name='txt_CostAccounting_WarmRemind']").val((data.ServicesModel.WarmRemind));
                divCostAccontingBox.find("textarea[name='txt_CostAccounting_InsiderInfor']").val((data.ServicesModel.InsiderInfor));
                divCostAccontingBox.find("textarea[name='txt_CostAccounting_ServiceStandard']").val((data.Service));

                //行程安排赋值 end

                //签证资料
                if (AddSanPlan.Data.type == '3') {
                    if (data.VisaInfoList.length > 0) {
                        var str = "";
                        for (var i = 0; i < data.VisaInfoList.length; i++) {
                            str += '<span class="upload_filename">&nbsp;<a target="_blank" href="' + data.VisaInfoList[i].FilePath + '">' + (data.VisaInfoList[i].Name) + '</a><a onclick="AddSanPlan.RemoveVisaFile(this);return false;" href="javascript:void(0);"><img src="/images/cha.gif" style="vertical-align:middle"></a><input type="hidden" value="' + (data.VisaInfoList[i].Name) + '|' + data.VisaInfoList[i].FilePath + '|' + data.VisaInfoList[i].Downloads + '" name="hideVisaFile"></span>';
                        }
                        $("#<%=lblVisaFiles.ClientID %>").html(str);
                    }

                }
                //附件
                if (data.Attach) {
                    $("#<%=lblFiles.ClientID %>").html('<span class="upload_filename">&nbsp;<a target="_blank" href="' + data.Attach.FilePath + '">查看附件</a><a onclick="AddSanPlan.RemoveVisaFile(this);return false;" href="javascript:void(0);"> <img src="/images/cha.gif" style="vertical-align:middle"></a><input type="hidden" value="|' + data.Attach.FilePath + '" name="hideOtherFile"></span>');
                }

            }, //删除签证附件
            RemoveVisaFile: function(obj) {
                $(obj).parent().remove();
            },
            //设置
            SysSettingList: null,
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
                        iframeUrl: "/TeamCenter/selectLine.aspx?callBackFun=AddSanPlanSelectRouteBackFun&aid=xianluxy&areaID=" + areaID + "&" + $.param(AddSanPlan.Data),
                        title: "选用线路",
                        modal: true,
                        width: "950px",
                        height: "425px"
                    });
                });

                $("#a_SelectData").click(function() {
                    Boxy.iframeDialog({
                        iframeUrl: "/TeamCenter/SelectChildTourDate.aspx?hide=<%=hideLeaveDate.ClientID %>&show=<%=lblLeaveDate.ClientID %>&old=" + $("#<%=hideLeaveDate.ClientID %>").val(),
                        title: "选择发班周期",
                        modal: true,
                        width: "925px",
                        height: "315px"
                    });
                });

                $("#btnAddDays").click(function() {
                    var day = tableToolbar.getInt($("#<%=txt_Days.ClientID %>").val());
                    day++;
                    $("#<%=txt_Days.ClientID %>").val(day);
                    $("#<%=txt_Days.ClientID %>").change();
                });

                $("#sltArea").change(function() {
                    //新增和复制的时候 根据线路区域修改停收天数
                    if (AddSanPlan.Data.act == "add" || AddSanPlan.Data.act == "copy") {
                        if (AddSanPlan.SysSettingList.length > 2) {
                            $("#<%=txtStopDate.ClientID %>").val(AddSanPlan.SysSettingList[tableToolbar.getInt($(this).attr("data-type"))])
                        }
                    }
                });

                $("#<%=txt_RouteName.ClientID %>").unbind("blur").blur(function() {
                    var _$routename = $(this);
                    var _$routeid = $("#<%=hideRouteID.ClientID %>");
                    if ($.trim(_$routename.val()) != _$routename.attr("data-value")) _$routeid.val("");
                    else _$routeid.val(_$routeid.attr("data-value"));
                });
            },
            RemoveLi: function(obj) {//移除上车地点选用li，同时移除相应的隐藏域的值
                if ($(obj).attr("data-isExist") == "1") {
                    tableToolbar._showMsg("该上车地点已被使用，无法删除！");
                } else {
                    var idArr = $("#<%=PickUpPoint1.PickUpIDClientID %>").val().split(',');
                    var textArr = $("#<%=PickUpPoint1.PickUpTextClientID %>").val().split(',');
                    var priceJArr = $("#<%=PickUpPoint1.PickUpPriceJClientID %>").val().split(',');
                    var priceSArr = $("#<%=PickUpPoint1.PickUpPriceSClientID %>").val().split(',');
                    var descArr = $("#<%=PickUpPoint1.PickUpDescClientID %>").val().split('&&&');
                    var id = $(obj).attr("data-id");
                    for (var i = 0; i < idArr.length; i++) {
                        if (id == idArr[i]) {
                            idArr.splice(i, 1);
                            textArr.splice(i, 1);
                            priceJArr.splice(i, 1);
                            priceSArr.splice(i, 1);
                            descArr.splice(i, 1);
                        }
                    }
                    $("#<%=PickUpPoint1.PickUpIDClientID %>").val(idArr.join(','));
                    $("#<%=PickUpPoint1.PickUpTextClientID %>").val(textArr.join(','));
                    $("#<%=PickUpPoint1.PickUpPriceJClientID %>").val(priceJArr.join(','));
                    $("#<%=PickUpPoint1.PickUpPriceSClientID %>").val(priceSArr.join(','));
                    $("#<%=PickUpPoint1.PickUpDescClientID %>").val(descArr.join('&&&'));
                    $(obj).closest("li").remove();
                }
            },
            AppendLi: function(data) {//上车地点选用回调函数
                //选用控件隐藏域赋值
                $("#<%=PickUpPoint1.PickUpIDClientID %>").val(data.value);
                $("#<%=PickUpPoint1.PickUpTextClientID %>").val(data.text);
                $("#<%=PickUpPoint1.PickUpPriceJClientID %>").val(data.priceJ);
                $("#<%=PickUpPoint1.PickUpPriceSClientID %>").val(data.priceS);
                $("#<%=PickUpPoint1.PickUpDescClientID %>").val(data.desc);
                var idArr = [], textArr = [], priceJArr = [], priceSArr = [], descArr = [];
                idArr = data.value.split(',');
                textArr = data.text.split(',');
                priceJArr = data.priceJ.split(',');
                priceSArr = data.priceS.split(',');
                descArr = data.desc.split('&&&');
                var str = "";
                for (var i = 0; i < idArr.length; i++) {
                    if (idArr[i] != "") {
                        str += '<li><a href="javascript:void(0);" onclick="AddSanPlan.RemoveLi(this);return false;" data-id=' +
                        idArr[i] + ' data-isExist="0" class="car-close"></a><span>' + textArr[i] + '</span><input type="hidden" name="hidPickUpTourID" value=""/></li>';
                    }
                }
                $("#<%=PickUpPoint1.SetPriv %>_a_btn").closest("div").find("ul").html(str);
            }
        }

        $(function() {
            //表单验证初始化
            FV_onBlur.initValid($("#<%=txt_RouteName.ClientID %>").closest("form").get(0));
            AddSanPlan.BindBtn();
            AddSanPlan.SysSettingList = $("#<%=hideSysStopCount.ClientID %>").val().split(',');
            AddSanPlan.PageInit();
        });
        
        //选择线路打开窗口选择或合并事件回调方法 t:1选择线路 t:2合并线路
        function AddSanPlanSelectRouteBackFun(items, t) {
            if (items && items.length == 0) return;

            if (t == "1") AddSanPlan.GetRouteJson(items[0].id, "1");
            else if (t == "2") AddSanPlan.GetRouteJson(items[0].id, "2");
            else return;

            for (var i = 1; i < items.length; i++) {
                AddSanPlan.GetRouteJson(items[i].id, "2");
            }
        }
    </script>

</asp:Content>

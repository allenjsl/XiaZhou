<%@ Page Title="����ɢƴ" Language="C#" MasterPageFile="~/MasterPage/Front.Master" AutoEventWireup="true"
    CodeBehind="AddSanpin.aspx.cs" Inherits="Web.TeamCenter.AddSanpin" ValidateRequest="false" %>

<%@ Register Src="../UserControl/PriceStand.ascx" TagName="PriceStand" TagPrefix="uc1" %>
<%@ Register Src="../UserControl/Journey.ascx" TagName="Journey" TagPrefix="uc2" %>
<%@ Register Src="../UserControl/CostAccounting.ascx" TagName="CostAccounting" TagPrefix="uc3" %>
<%@ Register Src="../UserControl/SellsSelect.ascx" TagName="SellsSelect" TagPrefix="uc4" %>
<%@ Register Src="../UserControl/UploadControl.ascx" TagName="UploadControl" TagPrefix="uc8" %>
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
        <!--����-->
        <div class="mainbox mainbox-whiteback">
            <div class="addContent-box">
                <table width="100%" cellspacing="0" cellpadding="0" class="firsttable">
                    <tbody>
                        <tr>
                            <td width="12%" class="addtableT">
                                �Ƿ�ͬ�з�����
                            </td>
                            <td width="23%" class="kuang2">
                                <asp:CheckBox ID="cbxDistribution" runat="server" />
                            </td>
                            <td width="10%" class="addtableT">
                                <font class="fontbsize12">* </font>��·����
                            </td>
                            <td width="15%" class="kuang2">
                                <select class="inputselect" id="sltArea" name="sltArea" valid="isNo" errmsg="��ѡ����·����"
                                    novalue="0">
                                    <asp:Literal ID="litArea" runat="server"></asp:Literal>
                                </select>
                            </td>
                            <td width="10%" class="addtableT">
                                <font class="fontbsize12">* </font>��·���ƣ�
                            </td>
                            <td width="30%" class="kuang2">
                                <asp:TextBox runat="server" ID="txt_RouteName" CssClass="inputtext formsize180" valid="required"
                                    errmsg="����д��·����" MaxLength="30"></asp:TextBox>
                                <asp:HiddenField ID="hideRouteID" runat="server" />
                                <a id="xianluxy" class="xuanyong" href="javascript:void(0);"></a>
                            </td>
                        </tr>
                        <tr>
                            <td class="addtableT">
                                <font class="fontbsize12">*</font> ������
                            </td>
                            <td class="kuang2">
                                <asp:TextBox ID="txt_Days" runat="server" CssClass="inputtext formsize40" errmsg="����������!|��������ȷ������!|�����������0!"
                                    valid="required|isInt|range" min="1"></asp:TextBox>
                                <button class="addtimebtn" type="button" id="btnAddDays">
                                    �����ճ�</button>
                            </td>
                            <td class="addtableT">
                                <font class="fontbsize12">* </font>Ԥ��������
                            </td>
                            <td class="kuang2">
                                <asp:TextBox ID="txtPeopleCount" runat="server" CssClass="inputtext formsize40" errmsg="������Ԥ������!|��������ȷ��Ԥ������!|Ԥ������������1-1999֮��!"
                                    valid="required|isInt|range" min="1" max="1999"></asp:TextBox>
                            </td>
                            <td class="addtableT">
                                <asp:Label ID="lblLeaveDateMore" runat="server" Text="�������ڣ�"></asp:Label>
                            </td>
                            <td class="kuang2">
                                <asp:Label ID="lblLeaveDate" runat="server" Text=""></asp:Label>
                                <asp:HiddenField ID="hideLeaveDate" runat="server" />
                                <asp:PlaceHolder ID="phdSelectDate" runat="server"><a style="font-weight: bold; color: #fff;
                                    background-color: #ff6600; display: block; width: 80px; height: 17px; text-align: center;
                                    padding-top: 5px;" id="a_SelectData" href="javascript:void(0);">��ѡ������</a></asp:PlaceHolder>
                            </td>
                        </tr>
                        <tr>
                            <td class="addtableT">
                                ������ͨ��
                            </td>
                            <td class="kuang2 pand4" colspan="5">
                                <asp:TextBox ID="txtSuccesssStraffBegin" runat="server" CssClass="inputtext formsize120"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="addtableT">
                                ���̽�ͨ��
                            </td>
                            <td class="kuang2 pand4" colspan="5">
                                <asp:TextBox ID="txtSuccesssStraffEnd" runat="server" CssClass="inputtext formsize120"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="addtableT">
                                ���Ϸ�ʽ��
                            </td>
                            <td class="kuang2 pand4" colspan="5">
                                <asp:TextBox ID="txtSuccessGather" runat="server" CssClass="inputtext formsize600"
                                    TextMode="MultiLine" Height="50"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="addtableT">
                                �����ϴ���
                            </td>
                            <td class="kuang2" colspan="5">
                                <uc8:UploadControl ID="UploadControl2" runat="server" />
                                <asp:Label ID="lblFiles" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="addtableT">
                                <font class="fontbsize12">* </font>����Ա��
                            </td>
                            <td class="kuang2" colspan="3">
                                <uc4:SellsSelect ID="SellsSelect1" runat="server" SetTitle="����Ա" />
                            </td>
                            <td class="addtableT">
                                ��ǰͣ��������
                            </td>
                            <td class="kuang2">
                                <asp:TextBox ID="txtStopDate" runat="server" CssClass="inputtext formsize40"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="addtableT">
                                �г���ɫ��
                            </td>
                            <td class="kuang2 pand4" colspan="5">
                                <span id="spanPlanContent" style="display: inline-block;">
                                    <asp:TextBox ID="txtPlanContent" runat="server" TextMode="MultiLine" CssClass="inputtext formsize800"
                                        Height="55"></asp:TextBox>
                                </span>
                            </td>
                        </tr>
                        <tr>
                            <td class="addtableT">
                                �ؼ��֣�
                            </td>
                            <td class="kuang2 pand4" colspan="5">
                                <asp:TextBox ID="txtSearchKey" runat="server" CssClass="inputtext formsize600" Height="17"></asp:TextBox>
                            </td>
                        </tr>
                        <asp:PlaceHolder ID="phdVisaFile" runat="server">
                            <tr>
                                <td class="addtableT">
                                    ǩ֤���ϣ�
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
                        <li class="cun-cy"><a href="javascript:void(0);" data-name="����" id="btnSave">����</a></li>
                    </asp:PlaceHolder>
                    <asp:PlaceHolder ID="phdCanel" runat="server">
                        <li class="cun-cy"><a id="btnCanel" href="javascript:void(0);" data-name="�����б�">�����б�</a></li>
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
                        ������⣺
                    </td>
                    <td bgcolor="#E9F4F9" align="left">
                        <input type="text" id="txt_ChangeTitle" class="inputtext formsize600" style="height: 17px;"
                            name="txt_ChangeTitle">
                    </td>
                </tr>
                <tr>
                    <td width="80" height="28" align="right" class="alertboxTableT">
                        �����ϸ��
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
            </s>�� ��</a><a hidefocus="true" href="javascript:void(0);" onclick="AddSanPlan.ChangeBox.hide();return false;"><s
                class="chongzhi"></s>�ر�</a>
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
            //�ύ��
            Save: function() {
                PriceStand.Init();
                KEditer.sync();
                $("#btnSave").text("�ύ��...");
                AddSanPlan.UnBindBtn();
                $.newAjax({
                    type: "post",
                    cache: false,
                    url: "/TeamCenter/AddSanpin.aspx?dotype=save&" + $.param(AddSanPlan.Data),
                    data: $("#btnSave").closest("form").serialize(),
                    dataType: "json",
                    success: function(ret) {
                        //ajax�ط���ʾ
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
            //��ť���¼�
            BindBtn: function() {
                //���
                $("#btnChangeSave").unbind("click").click(function() {
                    if ($.trim($("#txt_ChangeTitle").val()) == "") {
                        tableToolbar._showMsg("������ⲻ��Ϊ��!");
                        return false;
                    }
                    if ($.trim($("#txt_ChangeRemark").val()) == "") {
                        tableToolbar._showMsg("�����ϸ����Ϊ��!");
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
                            tableToolbar._showMsg("��ѡ���������!");
                            return false;
                        }
                        if ($("#<%=hideIsChangeInput.ClientID %>").val() == "true" && AddSanPlan.Data.act == "update") {
                            AddSanPlan.ChangeBox = new Boxy($("#div_Change"), { modal: true, fixed: false, title: "�����ϸ", width: "725px", height: "245px", display: "none" });
                        } else {
                            AddSanPlan.SaveType = 1;
                            AddSanPlan.Save();
                        }

                    }
                    return false;
                });

                $("#btnCanel").unbind("click").click(function() {
                    window.history.go(-1);
                    return false;
                })
            },
            //��ť�����
            UnBindBtn: function() {
                $("#ul_AddSanPlan_Btn").find("a").unbind("click");
                $("#ul_AddSanPlan_Btn").find("a").css("background-position", "0 -62px");
            },
            //���ѡ����·����Ϣ
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
            //�Զ�����ֵ
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

                //�г̰��� begin
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
                    //�ò�bengin
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
                    //�ò�end
                    _self.find("textarea[name='txtContent']").val((data.PlanModelList[i].Content));

                    _self.find("textarea[name='txtContent']").click();
                    KEditer.html(_self.find("textarea[name='txtContent']").attr("id"), data.PlanModelList[i].Content);
                    //����
                    if (data.PlanModelList[i].FilePath != "") {
                        _self.find("div[data-class='Journey_upload_swfbox']").find("input[type='hidden']").val("|" + data.PlanModelList[i].FilePath);
                        if (_self.find("div[data-class='span_journey_file']").length > 0) {
                            _self.find("div[data-class='span_journey_file']").remove();
                        }
                        _self.find("div[data-class='Journey_upload_swfbox']").parent().append('<div class="upload_filename" data-class="span_journey_file"><a href="' + data.PlanModelList[i].FilePath + '" target="_blank" title="�鿴����">�鿴����</a><a href="javascript:void(0);" title="ɾ������" onclick="Journey.RemoveFile(this);" ><img src="/images/cha.gif" border="0"></a></div>');
                    }
                    //����
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
                //�г̰��� end

                //�г̰��Ÿ�ֵ begin
                var divCostAccontingBox = $("#divCostAcconting");
                divCostAccontingBox.find("textarea[name='txt_CostAccounting_NoNeedItem']").val((data.ServicesModel.NoNeedItem));
                divCostAccontingBox.find("textarea[name='txt_CostAccounting_ShoppingItem']").val((data.ServicesModel.ShoppingItem));
                divCostAccontingBox.find("textarea[name='txt_CostAccounting_ChildServiceItem']").val((data.ServicesModel.ChildServiceItem));
                divCostAccontingBox.find("textarea[name='txt_CostAccounting_OwnExpense']").val((data.ServicesModel.OwnExpense));
                divCostAccontingBox.find("textarea[name='txt_CostAccounting_NeedAttention']").val((data.ServicesModel.NeedAttention));
                divCostAccontingBox.find("textarea[name='txt_CostAccounting_WarmRemind']").val((data.ServicesModel.WarmRemind));
                divCostAccontingBox.find("textarea[name='txt_CostAccounting_InsiderInfor']").val((data.ServicesModel.InsiderInfor));
                divCostAccontingBox.find("textarea[name='txt_CostAccounting_ServiceStandard']").val((data.Service));

                //�г̰��Ÿ�ֵ end

                //ǩ֤����
                if (AddSanPlan.Data.type == '3') {
                    if (data.VisaInfoList.length > 0) {
                        var str = "";
                        for (var i = 0; i < data.VisaInfoList.length; i++) {
                            str += '<span class="upload_filename">&nbsp;<a target="_blank" href="' + data.VisaInfoList[i].FilePath + '">' + (data.VisaInfoList[i].Name) + '</a><a onclick="AddSanPlan.RemoveVisaFile(this);return false;" href="javascript:void(0);"><img src="/images/cha.gif" style="vertical-align:middle"></a><input type="hidden" value="' + (data.VisaInfoList[i].Name) + '|' + data.VisaInfoList[i].FilePath + '|' + data.VisaInfoList[i].Downloads + '" name="hideVisaFile"></span>';
                        }
                        $("#<%=lblVisaFiles.ClientID %>").html(str);
                    }

                }
                //����
                if (data.Attach) {
                    $("#<%=lblFiles.ClientID %>").html('<span class="upload_filename">&nbsp;<a target="_blank" href="' + data.Attach.FilePath + '">�鿴����</a><a onclick="AddSanPlan.RemoveVisaFile(this);return false;" href="javascript:void(0);"> <img src="/images/cha.gif" style="vertical-align:middle"></a><input type="hidden" value="|' + data.Attach.FilePath + '" name="hideOtherFile"></span>');
                }

            }, //ɾ��ǩ֤����
            RemoveVisaFile: function(obj) {
                $(obj).parent().remove();
            },
            //����
            SysSettingList: null,
            CreatePlanEdit: function() {
                //�����г̱༭��
                KEditer.init('<%=txtPlanContent.ClientID %>');
            },
            PageInit: function() {
                this.CreatePlanEdit();

                $("#tbl_Journey_AutoAdd").autoAdd({ changeInput: $("#<%=txt_Days.ClientID %>"), addCallBack: Journey.AddRowCallBack, upCallBack: Journey.MoveRowCallBack, downCallBack: Journey.MoveRowCallBack });

                $("#xianluxy").click(function() {
                    var areaID = $("#sltArea").val();
                    Boxy.iframeDialog({
                        iframeUrl: "/TeamCenter/selectLine.aspx?callBackFun=AddSanPlanSelectRouteBackFun&aid=xianluxy&areaID=" + areaID + "&" + $.param(AddSanPlan.Data),
                        title: "ѡ����·",
                        modal: true,
                        width: "950px",
                        height: "425px"
                    });
                });

                $("#a_SelectData").click(function() {
                    Boxy.iframeDialog({
                        iframeUrl: "/TeamCenter/SelectChildTourDate.aspx?hide=<%=hideLeaveDate.ClientID %>&show=<%=lblLeaveDate.ClientID %>&old=" + $("#<%=hideLeaveDate.ClientID %>").val(),
                        title: "ѡ�񷢰�����",
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
                    //�����͸��Ƶ�ʱ�� ������·�����޸�ͣ������
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
            }
        }

        $(function() {
            //����֤��ʼ��
            FV_onBlur.initValid($("#<%=txt_RouteName.ClientID %>").closest("form").get(0));
            AddSanPlan.BindBtn();
            AddSanPlan.SysSettingList = $("#<%=hideSysStopCount.ClientID %>").val().split(',');
            AddSanPlan.PageInit();
        });

        //ѡ����·�򿪴���ѡ���ϲ��¼��ص����� t:1ѡ����· t:2�ϲ���·
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

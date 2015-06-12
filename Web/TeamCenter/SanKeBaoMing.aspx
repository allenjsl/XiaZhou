<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Front.Master" AutoEventWireup="true"
    CodeBehind="SanKeBaoMing.aspx.cs" Inherits="Web.TeamCenter.SanKeBaoMing" EnableViewState="false" %>

<%@ Register Src="../UserControl/SellsSelect.ascx" TagName="SellsSelect" TagPrefix="uc1" %>
<%@ Register Src="../UserControl/CustomerUnitSelect.ascx" TagName="CustomerUnitSelect"
    TagPrefix="uc2" %>
<%@ Register Src="../UserControl/PriceStand.ascx" TagName="PriceStand" TagPrefix="uc3" %>
<%@ Register Src="../UserControl/TravelControl.ascx" TagName="TravelControl" TagPrefix="uc4" %>
<%@ Register Src="../UserControl/TravelControlS.ascx" TagName="TravelControlS" TagPrefix="uc5" %>
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
                            �����ţ�
                        </td>
                        <td width="18%" class="kuang2">
                            <asp:Label ID="lblOrderNum" runat="server" Text=""></asp:Label>
                        </td>
                        <td width="12%" class="addtableT">
                            <font class="fontbsize12">*</font> ��������Ա��
                        </td>
                        <td width="18%" class="kuang2">
                            <uc1:SellsSelect ID="SellsSelect1" runat="server" />
                        </td>
                        <td width="12%" class="addtableT">
                            ��Դ�أ�
                        </td>
                        <td width="18%" class="kuang2">
                            <select class="inputselect" id="sltCountry" name="sltCountry"  valid="required" errmsg="��ѡ���Դ��-����!">
                            </select>
                            <select class="inputselect" id="sltProvince" name="sltProvince" valid="required"
                                errmsg="��ѡ���Դ��-ʡ��!">
                            </select>
                        </td>
                    </tr>
                    <tr>
                        <td class="addtableT">
                            <font class="fontbsize12">*</font> ��Դ��λ��
                        </td>
                        <td class="kuang2">
                            <uc2:CustomerUnitSelect ID="CustomerUnitSelect1" runat="server" />
                        </td>
                        <td class="addtableT">
                            <font class="fontbsize12">*</font>��ϵ�ˣ�
                        </td>
                        <td class="kuang2">
                            <asp:TextBox ID="txtContact" runat="server" class="inputtext formsize80" errmsg="��������ϵ��!"
                                valid="required"></asp:TextBox>
                            <asp:HiddenField ID="hideContactDeptId" runat="server" />
                        </td>
                        <td class="addtableT">
                            <font class="fontbsize12">*</font>��ϵ�绰��
                        </td>
                        <td class="kuang2">
                            <asp:TextBox ID="txtContactTel" runat="server" class="inputtext formsize80" errmsg="��������ϵ�绰!|�绰��ʽ����ȷ!"
                                valid="required|isTel"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="addtableT">
                            �µ��ˣ�
                        </td>
                        <td class="kuang2">
                            <asp:Label ID="lblOrderMan" runat="server" Text=""></asp:Label>
                        </td>
                        <td class="addtableT">
                            <font class="fontbsize12">*</font>������
                        </td>
                        <td colspan="3" class="kuang2">
                            <img width="16" height="15" style="vertical-align: middle" src="/images/chengren.gif" />����
                            <asp:TextBox ID="txtAdultCount" runat="server" class="inputtext formsize40" errmsg="�����������!|��������ȷ�ĳ�����!|������������1-999֮��!"
                                valid="required|isInt|range" min="1" max="999"></asp:TextBox>
                            &nbsp;
                            <img style="vertical-align: middle" src="/images/child.gif">
                            ��ͯ
                            <asp:TextBox ID="txtChildCount" runat="server" class="inputtext formsize40" errmsg="��������ȷ�Ķ�ͯ��!|��ͯ��������0-100֮��!"
                                valid="isInt|range" min="0" max="100"></asp:TextBox>
                        </td>
                    </tr>
                    <asp:PlaceHolder runat="server" ID="phKeHuKeYuanDanWei" Visible="false">
                    <tr>
                        <td class="addtableT">(�ͻ�)��Դ��λ��</td>
                        <td class="kuang2"><asp:Literal runat="server" ID="ltrKeHuKeYuanDanWeiName"></asp:Literal></td>
                        <td class="addtableT">(�ͻ�)��Դ��λ��ϵ�ˣ�</td>
                        <td class="kuang2"><asp:Literal runat="server" ID="ltrKeHuKeYuanDanWeiLxrName"></asp:Literal></td>
                        <td class="addtableT">(�ͻ�)��Դ��λ��ϵ�绰��</td>
                        <td class="kuang2"><asp:Literal runat="server" ID="ltrKeHuKeYuanDanWeiLxrTelephone"></asp:Literal></td>
                    </tr>
                    </asp:PlaceHolder>
                    <tr>
                        <td class="addtableT">
                            <font class="fontbsize12">*</font> �۸���ɣ�
                        </td>
                        <td width="88%" class="kuang2" colspan="5">
                            <div id="tblPrice">
                                <uc3:PriceStand ID="PriceStand1" runat="server" />
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td class="addtableT">
                            ���ӷ��ã�
                        </td>
                        <td class="kuang2">
                            <asp:TextBox ID="txtAddPrice" runat="server" class="inputtext formsize80" errmsg="��������ȷ�����ӷ���!"
                                valid="isMoney"></asp:TextBox>
                        </td>
                        <td class="addtableT">
                            ��ע��
                        </td>
                        <td width="55%" class="kuang2" colspan="3">
                            <asp:TextBox ID="txtRemarksFrist" runat="server" class="inputtext formsize450"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="addtableT">
                            ���ٷ��ã�
                        </td>
                        <td class="kuang2">
                            <asp:TextBox ID="txtReducePrice" runat="server" class="inputtext formsize80" errmsg="��������ȷ�ļ��ٷ���!"
                                valid="isMoney"></asp:TextBox>
                        </td>
                        <td class="addtableT">
                            ��ע��
                        </td>
                        <td width="55%" class="kuang2" colspan="3">
                            <asp:TextBox ID="txtRemarksSecond" runat="server" class="inputtext formsize450"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td height="14" class="addtableT">
                            <font class="fontbsize12">*</font> �ϼƽ�
                        </td>
                        <td class="kuang2">
                            <asp:TextBox ID="txtTotalPrice" runat="server" class="inputtext formsize80" errmsg="������ϼƽ��!|��������ȷ�ĺϼƽ��"
                                valid="required|isMoney"></asp:TextBox>
                        </td>
                        <td class="addtableT">
                            �������գ�
                        </td>
                        <td class="kuang2">
                            <asp:TextBox ID="txtGuidePrice" runat="server" class="inputtext formsize80" errmsg="��������ȷ�ĵ�������!"
                                valid="isMoney"></asp:TextBox>
                        </td>
                        <td class="addtableT">
                            ����Ӧ�գ�
                        </td>
                        <td class="kuang2">
                            <asp:Label ID="lblSellPrice" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td height="14" class="addtableT">
                            Ԥ����ֹʱ�䣺
                        </td>
                        <td width="18%" class="kuang2">
                            <asp:TextBox ID="txtSaveSeatDate" runat="server" class="inputtext formsize100"></asp:TextBox>
                            <a class="timesicon" href="javascript:void(0);" onclick="WdatePicker({el:'<%=txtSaveSeatDate.ClientID %>',dateFmt:'yyyy-MM-dd HH:mm',maxDate:'<%=this.MaxDateTime %>',minDate:'<%= DateTime.Now.AddMinutes(5).ToString("yyyy-MM-dd HH:mm")%>'});">
                                Ԥ����ֹʱ��</a>
                        </td>
                        <td class="addtableT">
                            ��ͬ�ţ�
                        </td>
                        <td colspan="3" class="kuang2">
                            <uc9:hetonghao runat="server" id="txtHeTongHao"></uc9:hetonghao>
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
                            ������ע��
                        </td>
                        <td width="88%" class="kuang2 pand4">
                            <asp:TextBox ID="txtRemarksOrder" TextMode="MultiLine" runat="server" class="inputtext formsize800"
                                Height="55px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td width="12%" class="addtableT">
                            �ڲ���Ϣ��
                        </td>
                        <td width="88%" class="kuang2 pand4">
                            <asp:TextBox ID="txtNeiBuXinXi" TextMode="MultiLine" runat="server" class="inputtext formsize800"
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
            <span class="formtableT formtableT02">������Ϣ</span>
            <table width="100%" cellspacing="0" cellpadding="0" class="content2" id="tblTuiTuan">
                <tbody>
                    <tr class="addcontentT">
                        <th width="5%" valign="middle" class="th-line">
                            ���
                        </th>
                        <th width="9%" valign="middle" class="th-line">
                            ����
                        </th>
                        <th width="5%" valign="middle" class="th-line">
                            �Ա�
                        </th>
                        <th width="12%" valign="middle" class="th-line">
                            ��������
                        </th>
                        <th width="9%" valign="middle" align="right" class="th-line">
                            �˿���
                        </th>
                        <th width="29%" valign="middle" class="th-line">
                            ���˵��
                        </th>
                        <th width="31%" valign="middle" class="th-line">
                            ����ԭ��
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
                        <li class="cun-cy"><a href="javascript:void(0);" id="btnUpdate" data-name="�� ��">�� ��</a></li></asp:PlaceHolder>
                    <asp:PlaceHolder ID="phdQuXiao" runat="server">
                        <li class="quxiao-cy"><a href="javascript:void(0);" id="btnCanelOrder" data-name="ȡ������">
                            ȡ������</a></li></asp:PlaceHolder>
                    <asp:PlaceHolder ID="phdBuShouLi" runat="server">
                        <li class="cun-cy"><a href="javascript:void(0);" id="btnDonot" data-name="������">������</a></li></asp:PlaceHolder>
                    <asp:PlaceHolder ID="pdhJiXuLiuWei" runat="server">
                        <li class="cun-cy"><a href="javascript:void(0);" id="btnKeep" data-name="������λ">������λ</a></li></asp:PlaceHolder>
                    <asp:PlaceHolder ID="phdQueRenYuLiu" runat="server">
                        <li class="cun-cy"><a href="javascript:void(0);" id="btnSaveSeat" data-name="ȷ��Ԥ��">ȷ��Ԥ��</a></li></asp:PlaceHolder>
                    <asp:PlaceHolder ID="phsQueRenChengJiao" runat="server">
                        <li class="cun-cy"><a href="javascript:void(0);" id="btnSave" data-name="ȷ�ϳɽ�">ȷ�ϳɽ�</a></li></asp:PlaceHolder>
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
                var form = $("form").get(0);
                return ValiDatorForm.validator(form, "alert");
            },
            SetSumPrice: function() {
                //���˼�
                var adultPrice = tableToolbar.getFloat($("#<%=hideAdultPrice.ClientID%>").val());
                //��ͯ��
                var childPrice = tableToolbar.getFloat($("#<%=hideChildPrcie.ClientID%>").val());
                //������
                var adultCount = tableToolbar.getInt($("#<%=txtAdultCount.ClientID%>").val());
                //��ͯ��
                var childCount = tableToolbar.getInt($("#<%=txtChildCount.ClientID%>").val());
                //���ӷ���
                var addPrice = tableToolbar.getFloat($("#<%=txtAddPrice.ClientID%>").val());
                //���ٷ���
                var reducePrice = tableToolbar.getFloat($("#<%=txtReducePrice.ClientID%>").val());
                //��������
                var guidePrice = tableToolbar.getFloat($("#<%=txtGuidePrice.ClientID%>").val());
                //�ϼƽ��
                var sunPrice = 0;

                sunPrice = adultPrice * adultCount + childPrice * childCount + addPrice - reducePrice;

                $("#<%=txtTotalPrice.ClientID%>").val(tableToolbar.getFloat(sunPrice));
                $("#<%=lblSellPrice.ClientID%>").html(tableToolbar.getFloat(sunPrice - guidePrice));
            },
            //��������Ӧ��
            SetIncome: function() {
                var sumPrice = tableToolbar.getFloat($("#<%=txtTotalPrice.ClientID%>").val());
                var guidePrice = tableToolbar.getFloat($("#<%=txtGuidePrice.ClientID%>").val());
                $("#<%=lblSellPrice.ClientID%>").html(sumPrice - guidePrice);
            },
            ApplyPriceBox: null,
            PageSave: function() {
                if (!this.vZeRenXiaoShou() && !confirm("��ǰѡ�������������ѡ���Ŀͻ���λ�������۲�ƥ�䡣\n��ȷ��Ҫ�ύ������")) return;

                OrderInfoPage.Data.dotype = "save";
                OrderInfoPage.UnBindBtn();
                $.newAjax({
                    type: "post",
                    cache: false,
                    url: "/TeamCenter/SanKeBaoMing.aspx?" + $.param(OrderInfoPage.Data),
                    data: $("form").serialize(),
                    dataType: "json",
                    success: function(ret) {
                        //ajax�ط���ʾ
                        if (ret.result == "1") {
                            tableToolbar._showMsg(ret.msg, function() {
                                if (OrderInfoPage.ReturnUrl != "") {
                                    window.location.href = decodeURIComponent(OrderInfoPage.ReturnUrl);
                                } else {
                                    window.history.go(-1);
                                }
                            });
                        } else if (ret.result == "2") {
                            OrderInfoPage.Data.orderID = ret.obj;
                            OrderInfoPage.ShowConfirmMsg(ret.msg);
                        } else {
                            tableToolbar._showMsg(ret.msg);
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
                        //ȷ��Ԥ��                                                                                                                                                                                          
                        case "btnSaveSeat":
                            if (OrderInfoPage.CheckForm()) {
                                if ($.trim($("#<%=txtSaveSeatDate.ClientID %>").val()) == "") {
                                    tableToolbar._showMsg("������Ԥ����ֹʱ��!");
                                    return false;
                                } else {
                                    $(this).html("�����ύ..");
                                    OrderInfoPage.Data.saveType = "1";
                                    OrderInfoPage.PageSave();
                                }
                            }
                            break;
                        //ȷ�ϳɽ�                                                                                                                                                                                          
                        case "btnSave":
                            if (!OrderInfoPage.CheckForm()) {
                                return false;
                            }
                            $(this).html("�����ύ..");
                            OrderInfoPage.Data.saveType = "2";
                            OrderInfoPage.PageSave();
                            break;
                        //������λ                                                                                                                                                                                          
                        case "btnKeep":
                            if (OrderInfoPage.CheckForm()) {
                                if ($.trim($("#<%=txtSaveSeatDate.ClientID %>").val()) == "") {
                                    tableToolbar._showMsg("������Ԥ����ֹʱ��!");
                                    return false;
                                } else {
                                    $(this).html("�����ύ..");
                                    OrderInfoPage.Data.saveType = "1";
                                    OrderInfoPage.PageSave();
                                }
                            }

                            break;
                        //�޸�                                                                                                                                                                    
                        case "btnUpdate":
                            if (OrderInfoPage.CheckForm()) {
                                OrderInfoPage.Data.saveType = "4";
                                //����ǹ�Ӧ�̼ƻ�
                                if ($.trim($("#<%=hideSourceID.ClientID %>").val()) != "") {
                                    var state = $("#<%=hideOrderState.ClientID %>").val();
                                    if (state == "<%=(int)EyouSoft.Model.EnumType.TourStructure.OrderStatus.�渶������˳ɹ�%>") {
                                        $(this).html("�����޸�..");
                                        OrderInfoPage.PageSave();
                                    } else {
                                        //                                        if (OrderInfoPage.Data.orderID == "") {
                                        $(this).html("�����޸�..");
                                        OrderInfoPage.PageSave();
                                        //                                        }
                                    }
                                } else {
                                    $(this).html("�����޸�..");
                                    OrderInfoPage.PageSave();
                                }
                            }
                            break;
                        //ȡ������                                                                                                                                                                        
                        case "btnCanelOrder":
                            tableToolbar.ShowConfirmMsg("ȷ��Ҫȡ���ö���?", function() {
                                $(this).html("����ȡ��..");
                                OrderInfoPage.Data.saveType = "5";
                                OrderInfoPage.PageSave();
                            })

                            break;
                        //������                                                                                                                                                                      
                        case "btnDonot":
                            tableToolbar.ShowConfirmMsg("ȷ��������ö���?", function() {
                                $(this).html("�����ύ..");
                                OrderInfoPage.Data.saveType = "6";
                                OrderInfoPage.PageSave();
                            });
                            break;
                        case "btnPrint": break;
                    }
                })

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

                //��������Ѿ�ȷ��
                if ($("#<%=hideOrderState.ClientID %>").val() == "<%=(int)EyouSoft.Model.EnumType.TourStructure.OrderStatus.�ѳɽ� %>") {
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

                //����������ţ�����ʾ
                if ($("#tblTuiTuan").find("tr").length > 1) {
                    $("#divTuiTuan").css("display", "block");
                }

                //��������ɼƵ�
                if ($("#<%=hideOrderBianGeng.ClientID %>").val() == "true") {
                    if (OrderInfoPage.Data.orderID != "") {
                        $("#btnUpdate").html("�� ��");
                        $("#btnUpdate").attr("data-name", "�� ��");
                    }
                }

                $("#<%=txtTotalPrice.ClientID %>,#<%=txtGuidePrice.ClientID %>").blur(function() {
                    OrderInfoPage.SetIncome();
                })
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
                    title: "�ο�����",
                    modal: true,
                    width: "625px",
                    height: "275px"
                });
            },
            OterBtnBind: function() {                
                $("#tblPrice").find("input[type='radio']").click(function() {
                    var tr = $(this).closest("tr");
                    //������ۼ�
                    $("#<%=hideAdultPrice.ClientID%>").val($(this).val().split('|')[1]);
                    $("#<%=hideChildPrcie.ClientID%>").val($(this).val().split('|')[2]);
                    $("#<%=hideLevelID.ClientID%>").val($(this).val().split('|')[0]);
                    $("#<%=hidePriceStandID.ClientID%>").val(tr.find("span[data-class='standard']").attr("data-id"));

                    //��λ�����
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

            },
            ShowConfirmMsg: function(msg) {
                var confirmMsg = msg;
                var _html = '<div style="padding: 10px 0 20px 10px;cursor: default;"><h1 style="margin:10px 5px;"><img src="/images/y-tanhao.gif" style=" vertical-align:middle; margin-right:5px;" />  {{msg}}</h1><input type="button" id="BLOCKUI_NO" value="ȡ ��" " style="width:64px; height:24px; border:0 none; background:url(/images/cx.gif);" /> <input type="button" id="BLOCKUI_SQ" value="��������" style="width:64px; height:24px; border:0 none; margin-left:20px; background:url(/images/cx.gif);" /> <input type="button" id="BLOCKUI_SK" value="�� ��" style="width:64px; height:24px; border:0 none; margin-left:20px; background:url(/images/cx.gif);" /></div>';
                $.blockUI({
                    message: _html.replace(/{{msg}}/, confirmMsg),
                    css: { backgroundColor: "#E9F4F9", borderColor: "#00446b", borderWidth: '1px', cursor: "pointer", color: "#ed0000", width: '375px' }
                });
                $("#BLOCKUI_SQ").click(function() {
                    $.unblockUI({ onUnblock: function() {
                        Boxy.iframeDialog({
                            iframeUrl: "/TeamCenter/ChaoXianShenQing.aspx?orderId=" + OrderInfoPage.Data.orderID + "&sl=" + OrderInfoPage.Data.sl + "&url=" + encodeURIComponent(OrderInfoPage.ReturnUrl),
                            title: "�渶����",
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
                        window.location.href = "/MarketCenter/XiaoShouShouKuan.aspx?sl=<%=(int)EyouSoft.Model.EnumType.PrivsStructure.Privs.��������_�����տ�_��Ŀ %>";
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
            },
            //����������֤
            vZeRenXiaoShou: function() {
                var keHuDanWeiId = $("#<%=CustomerUnitSelect1.ClientNameKHBH %>").val();
                var zenRenXiaoShouId = $("#<%=SellsSelect1.SellsIDClient %>").val();
                var _data = { keHuDanWeiId: keHuDanWeiId, zenRenXiaoShouId: zenRenXiaoShouId };
                var _ret = false;
                $.ajax({
                    type: "get", url: "sankebaoming.aspx?dotype=yanzhengzerenxiaoshou", data: _data, cache: false, dataType: "json", async: false,
                    success: function(response) {
                        _ret = response.retCode;
                    }
                });

                return _ret
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

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OperaterCarList.aspx.cs"
    Inherits="Web.OperaterCenter.OperaterCarList" %>

<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register TagName="OperaterPanyment" TagPrefix="uc2" Src="~/UserControl/OperaterPanyment.ascx" %>
<%@ Register TagName="SupplierControl" TagPrefix="uc5" Src="~/UserControl/SupplierControl.ascx" %>
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
        /*��ʽ��д*/body, html
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
    <form id="formCar" runat="server">
    <div class="jidiao-r">
        <div id="con_faq_4">
            <table width="100%" cellpadding="0" cellspacing="0" class="jd-table01 line-b">
                <tr>
                    <th width="15%" align="right" class="border-l">
                        <span class="fontred">*</span><span class="addtableT">����</span>��
                    </th>
                    <td width="40%">
                        <uc5:SupplierControl ID="SupplierControl1" runat="server" IsMust="true" CallBack="CarPage._AjaxContectInfo"
                            SupplierType="�ó�" />
                    </td>
                    <th width="15%" align="right">
                        <span class="addtableT">��ϵ�ˣ�</span>
                    </th>
                    <td width="30%">
                        <asp:TextBox ID="txtContectName" runat="server" CssClass="inputtext formsize140"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th align="right" class="border-l">
                        ��ϵ�绰��
                    </th>
                    <td>
                        <asp:TextBox ID="txtContectPhone" runat="server" CssClass="inputtext formsize140"></asp:TextBox>
                    </td>
                    <th align="right">
                        <span class="addtableT">��ϵ���棺</span>
                    </th>
                    <td>
                        <asp:TextBox ID="txtContectFax" runat="server" CssClass="inputtext formsize140"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th align="right" class="border-l">
                        <span class="fontred">*</span> �ó�ʱ�䣺
                    </th>
                    <td>
                        <asp:TextBox ID="txtUseCarTime" runat="server" CssClass="inputtext formsize80" valid="required"
                            errmsg="*�������ó���ʼ����!">
                        </asp:TextBox>
                        <asp:TextBox ID="txttime1" runat="server" CssClass="inputtext formsize60 " Text="��ʱ��">
                        </asp:TextBox>
                        ��
                        <asp:TextBox ID="txtUseCarTime2" runat="server" CssClass="inputtext formsize80">
                        </asp:TextBox>
                        <asp:TextBox ID="txttime2" runat="server" CssClass="inputtext formsize60" Text="��ʱ��">
                        </asp:TextBox>
                    </td>
                    <th align="right">
                        <span class="fontred">*</span>�ó����ͣ�
                    </th>
                    <td>
                        <asp:DropDownList ID="ddlUserCarType" runat="server" class="inputselect" valid="required"
                            errmsg="*��ѡ���ó�����!">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <th align="right" class="border-l">
                        ���ͣ�
                    </th>
                    <td>
                        <select class="inputselect" name="selCarModel">
                            <%=CarModelList.ToString()%>
                        </select>
                    </td>
                    <th align="right">
                        ���ƺ�<span class="addtableT">��</span>
                    </th>
                    <td>
                        <asp:TextBox ID="txtCarNumber" runat="server" CssClass="inputtext formsize140"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th align="right" class="border-l">
                        <span class="addtableT">˾����</span>
                    </th>
                    <td>
                        <asp:TextBox ID="txtDirverName" runat="server" CssClass="inputtext formsize80"></asp:TextBox>
                    </td>
                    <th align="right">
                        <span class="addtableT">˾���绰��</span>
                    </th>
                    <td>
                        <asp:TextBox ID="txDirverPhone" runat="server" CssClass="inputtext formsize80"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th align="right" class="border-l">
                        <span class="addtableT"><span class="fontred">*</span>������ϸ��</span>
                    </th>
                    <td colspan="3">
                        <asp:TextBox ID="txtCostParticu" runat="server" TextMode="MultiLine" CssClass="inputtext formsize600"
                             valid="required" errmsg="*�����������ϸ!"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th align="right" class="border-l">
                        �ó�����
                    </th>
                    <td align="left">
                        <asp:TextBox ID="txtUseCarNums" runat="server" CssClass="inputtext formsize50"></asp:TextBox>
                    </td>
                    <th align="right">
                        <span class="addtableT"><span class="fontred">*</span> </span>�������
                    </th>
                    <td align="left">
                        <asp:TextBox ID="txttotalMoney" runat="server" CssClass="inputtext formsize50" valid="required|isMoney"
                            errmsg="*������������!|*���������������!"></asp:TextBox>
                        Ԫ
                    </td>
                </tr>
                <tr>
                    <th align="right" class="border-l">
                        <span class="addtableT">�Ӵ��г̣�</span>
                    </th>
                    <td colspan="3">
                        <asp:TextBox ID="txtTravel" runat="server" CssClass="inputtext formsize600"
                            TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th align="right" class="border-l">
                        ������֪��
                    </th>
                    <td colspan="3">
                        <asp:TextBox ID="txtGuidNotes" runat="server" TextMode="MultiLine" CssClass="inputtext formsize600">
�������ƣ�
˾����
���ƺţ�
˾���绰:
                        </asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th align="right" class="border-l">
                        ������ע��
                    </th>
                    <td colspan="3">
                        <asp:TextBox ID="txtOtherRemark" TextMode="MultiLine" runat="server" CssClass="inputtext formsize600"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th align="right" class="border-l">
                        <span class="addtableT"><span class="fontred">*</span> ������</span>
                    </th>
                    <td align="left">
                        <asp:DropDownList ID="ddlProfit" runat="server" class="inputselect">
                            <asp:ListItem Value="0">��</asp:ListItem>
                            <asp:ListItem Value="1">��</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <th align="right">
                        <span class="addtableT"><span class="fontred">*</span> ֧����ʽ��</span>
                    </th>
                    <td align="left">
                        <select name="selPenyMent" class="inputselect">
                            <%=EyouSoft.Common.UtilsCommons.GetOperaterPanyMentList(panyMent)%>
                        </select>
                    </td>
                </tr>
                <tr>
                    <th align="right" class="border-l">
                        <span class="addtableT"><span class="fontred">*</span> ״̬��</span>
                    </th>
                    <td colspan="3">
                        <select name="SelStatus" class="inputselect">
                            <%=EyouSoft.Common.UtilsCommons.GetOperaterStatusList(status)%>
                        </select>
                    </td>
                </tr>
            </table>
            <asp:PlaceHolder ID="phdShowList" runat="server">
                <h2>
                    <p>
                        �Ѱ��ų���</p>
                </h2>
                <table width="100%" border="0" cellspacing="0" cellpadding="0" class="jd-table01"
                    style="border-bottom: 1px solid #A9D7EC;" id="tblcarlist">
                    <tr>
                        <th align="center">
                            ��������
                        </th>
                        <th align="center">
                            �ó�ʱ����-ֹ
                        </th>
                        <th align="center">
                            ����
                        </th>
                        <th align="center">
                            �ó�����
                        </th>
                        <th align="center">
                            ������ϸ
                        </th>
                        <th align="right">
                            �������
                        </th>
                        <th align="center">
                            ֧����ʽ
                        </th>
                        <th align="center">
                            ״̬
                        </th>
                        <th align="center">
                            ȷ�ϵ�
                        </th>
                        <th align="center">
                            ����
                        </th>
                    </tr>
                    <asp:Repeater ID="repCarList" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td align="center">
                                    <%# Eval("SourceName")%>
                                </td>
                                <td align="center">
                                    <%#Eval("StartDate","{0:M/d}")%>
                                    <%# Eval("StartTime") %>-
                                    <%# Eval("EndDate","{0:M/d}")%>
                                    <%# Eval("EndTime") %>
                                </td>
                                <td align="center">
                                    <%# Eval("Models")%>
                                </td>
                                <td align="center">
                                    <%# Eval("Num")%>
                                </td>
                                <td align="center">
                                    <span title="<%#Eval("CostDetail")  %>">
                                        <%# EyouSoft.Common.Utils.GetText(Eval("CostDetail").ToString(),30)%></span>
                                </td>
                                <td align="right">
                                    <%#UtilsCommons.GetMoneyString(Convert.ToDecimal(Eval("Confirmation")),ProviderToMoney)%>
                                    <a href="javascript:void(0);" data-class="Prepaid" title="Ԥ������" data-id="<%# Eval("PlanId") %>"
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
                                    <a href='<%# querenUrl %>?planId=<%# Eval("PlanId") %>' target="_blank">
                                        <img src="/images/y-kehuqueding.gif" border="0" /></a>
                                </td>
                                <td align="center">
                                    <%if (ListPower)
                                      { %>
                                    <a href="javascript:void(0);" data-class="updateCar">
                                        <img src="/images/y-delupdateicon.gif" alt="" data-id="<%# Eval("PlanId") %>" />
                                        �޸�</a> <a href="javascript:void(0);" data-class="deleteCar">
                                            <img src="/images/y-delicon.gif" alt="" data-id="<%# Eval("PlanId") %>" />
                                            ɾ��</a>
                                    <%}
                                      else
                                      { %>
                                    <a href="javascript:void(0);" data-class="showCar">
                                        <img src="/images/y-delupdateicon.gif" alt="" data-id="<%# Eval("PlanId") %>" />�鿴</a>
                                    <%} %>
                                </td>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
            </asp:PlaceHolder>
            <div class="hr_5">
            </div>
            <asp:PlaceHolder ID="panView" runat="server">
                <div class="mainbox cunline fixed">
                    <ul id="ul_btn_list">
                        <li class="cun-cy"><a href="javascript:" id="btnSave">����</a> </li>
                    </ul>
                </div>
            </asp:PlaceHolder>
        </div>
        <input type="hidden" id="hidCarNum" runat="server" />
        <asp:HiddenField ID="hiddriverInfo" runat="server" />
        <asp:HiddenField ID="hidSueID" runat="server" />
        <asp:HiddenField ID="hidSueNum" runat="server" />
    </div>
    </form>

    <script type="text/javascript">
        var CarPage = {
            sl: '<%=SL %>',
            type: '<%=Utils.GetQueryStringValue("Type") %>',
            tourId: '<%=Utils.GetQueryStringValue("tourId") %>',
            iframeId: '<%=Utils.GetQueryStringValue("iframeId") %>',
            _OpenBoxy: function(title, iframeUrl, width, height, draggable) {
                parent.Boxy.iframeDialog({ title: title, iframeUrl: iframeUrl, width: width, height: height, draggable: draggable });
            },
            _DeleteCar: function(objID) {
                var _Url = '/OperaterCenter/OperaterCarList.aspx?sl=' + CarPage.sl + '&type=' + CarPage.type + '&tourId=' + CarPage.tourId + "&iframeId=" + CarPage.iframeId + "&m=" + new Date().getTime();
                $.newAjax({
                    type: "get",
                    url: '/OperaterCenter/OperaterCarList.aspx?sl=' + CarPage.sl + '&action=delete&PlanId=' + objID + '&tourid=' + CarPage.tourId,
                    cache: false,
                    dataType: "json",
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
            _SaveCar: function(planId, tourId) {
                var _url = '/OperaterCenter/OperaterCarList.aspx?sl=' + CarPage.sl + '&type=' + CarPage.type + '&tourId=' + CarPage.tourId + '&iframeId=' + CarPage.iframeId + "&m=" + new Date().getTime();
                $.newAjax({
                    type: "POST",
                    url: '/OperaterCenter/OperaterCarList.aspx?sl=' + CarPage.sl + '&action=save&planId=' + planId + "&tourId=" + tourId,
                    cache: false,
                    data: $("#btnSave").closest("form").serialize(),
                    dataType: "json",
                    async: false,
                    success: function(ret) {
                        if (ret.result == "1") {
                            parent.tableToolbar._showMsg(ret.msg, function() {
                                window.location.href = _url;
                            });
                        } else {
                            parent.tableToolbar._showMsg(ret.msg, function() {
                                $("#btnSave").text("����").css("background-position", "0 0px").bind("click");
                                $("#btnSave").click(function() {
                                    $(this).text("������...").css("background-position", "0 -62px").unbind("click");
                                    var _planId = '<%=Utils.GetQueryStringValue("PlanId") %>';
                                    CarPage._SaveCar(_planId, CarPage.tourId);
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
            _BIndBtn: function() {
                //���ڿؼ�
                $("#<%=txtUseCarTime.ClientID%>").focus(function() {
                    WdatePicker();
                });
                $("#<%=txtUseCarTime2.ClientID%>").focus(function() {
                    WdatePicker();
                });

                //�޸�
                $("#tblcarlist").find("[data-class='updateCar']").unbind("click");
                $("#tblcarlist").find("[data-class='updateCar']").click(function() {
                    var PlanId = $(this).find("img").attr("data-Id");
                    if (PlanId != "") {
                        window.location.href = '/OperaterCenter/OperaterCarList.aspx?sl=' + CarPage.sl + '&action=update&PlanId=' + PlanId + '&tourId=' + CarPage.tourId + '&iframeId=' + CarPage.iframeId;
                    }
                    return false;
                });

                //�鿴 
                $("#tblcarlist").find("[data-class='showCar']").unbind("click");
                $("#tblcarlist").find("[data-class='showCar']").click(function() {
                    var PlanId = $(this).find("img").attr("data-Id");
                    if (PlanId != "") {
                        window.location.href = '/OperaterCenter/OperaterCarList.aspx?sl=' + CarPage.sl + '&action=update&PlanId=' + PlanId + '&tourId=' + CarPage.tourId + '&iframeId=' + CarPage.iframeId + "&show=1";
                    }
                    return false;
                });

                //ɾ��
                $("#tblcarlist").find("[data-class='deleteCar']").unbind("click");
                $("#tblcarlist").find("[data-class='deleteCar']").click(function() {
                    var newThis = $(this);
                    parent.tableToolbar.ShowConfirmMsg("ȷ��ɾ������������?", function() {
                        var planId = newThis.find("img").attr("data-ID");
                        if (planId) {
                            CarPage._DeleteCar(planId);
                        }
                    });

                    return false;
                });

                //txttime1
                $("#<%=txttime1.ClientID %>").focus(function() { if ($(this).val() == "��ʱ��") { $(this).val(""); } }).blur(function() { if ($(this).val() == "") { $(this).val("��ʱ��"); } });
                $("#<%=txttime2.ClientID %>").focus(function() { if ($(this).val() == "��ʱ��") { $(this).val(""); } }).blur(function() { if ($(this).val() == "") { $(this).val("��ʱ��"); } });

                //Ԥ������
                $("#tblcarlist").find("a[data-class='Prepaid']").unbind("click");
                $("#tblcarlist").find("a[data-class='Prepaid']").click(function() {
                    var planId = $(this).attr("data-ID");
                    var soucesName = $(this).attr("data-soucesname");
                    CarPage._OpenBoxy("Ԥ������", '/OperaterCenter/PrepaidAppliaction.aspx?PlanId=' + planId + '&type=' + CarPage.type + '&sl=' + CarPage.sl + '&tourId=' + CarPage.tourId + '&souceName=' + escape(soucesName), "650px", "550px", true);
                    return false;
                });

                $("#btnSave").unbind("click").text("����").css("background-position", "0 0px");
                $("#btnSave").click(function() {
                    if (!ValiDatorForm.validator($("#<%=formCar.ClientID %>").get(0), "parent")) {
                        return false;
                    } else {
                        $(this).text("������...").css("background-position", "0 -62px").unbind("click");
                        var planId = '<%=Utils.GetQueryStringValue("PlanId") %>';
                        CarPage._SaveCar(planId, CarPage.tourId);
                    }
                });
            },
            _CarModelChangeEvent: function(ModelId) {
                if (ModelId) {
                    var hiddriverInfo = $("#<%=hiddriverInfo.ClientID %>").val();
                    if (hiddriverInfo != "") {
                        if (hiddriverInfo.split('|').length > 0) {
                            for (var i = 0; i < hiddriverInfo.split('|').length - 1; i++) {
                                if (ModelId == hiddriverInfo.split('|')[i].split(',')[0]) {
                                    $("#<%=txtCarNumber.ClientID %>").val(hiddriverInfo.split('|')[i].split(',')[1]);
                                    $("#<%=txtDirverName.ClientID %>").val(hiddriverInfo.split('|')[i].split(',')[2]);
                                    $("#<%=txDirverPhone.ClientID %>").val(hiddriverInfo.split('|')[i].split(',')[3]);
                                }
                            }
                        }
                    }
                }
            },
            _InitPage: function() {
                CarPage._BIndBtn();
                if ('<%=Utils.GetQueryStringValue("show") %>' == "1") {
                    $("#ul_btn_list").parent("div").hide();
                }

                $("select[name='selCarModel']").unbind("change");
                $("select[name='selCarModel']").change(function() {
                    if ($(this).val().split(',')[0]) {
                        CarPage._CarModelChangeEvent($(this).val().split(',')[0]);
                    }
                });
            },
            _AjaxReqCarModel: function(id, type, source, companyId) {
                if (id) {
                    $.newAjax({
                        type: "POST",
                        url: "/Ashx/GetSupplierContect.ashx?suppId=" + id + "&type=" + type + "&source=" + source + "&company=" + companyId,
                        dataType: "json",
                        success: function(ret) {
                            if (ret.tolist != "") {
                                var info = "";
                                if (ret.tolist.length > 0) {
                                    for (var i = 0; i < ret.tolist.length; i++) {
                                        $("select[name='selCarModel']").append("<option value='" + ret.tolist[i].id + "," + ret.tolist[i].text + "'>" + ret.tolist[i].text + "</option>");
                                        info += "" + ret.tolist[i].id + "," + ret.tolist[i].CarCode + "," + ret.tolist[i].driver + "," + ret.tolist[i].driverTel + "|";
                                        $("#<%=txtCarNumber.ClientID %>").val(ret.tolist[0].CarCode);
                                        $("#<%=txtDirverName.ClientID %>").val(ret.tolist[0].driver);
                                        $("#<%=txDirverPhone.ClientID %>").val(ret.tolist[0].driverTel);
                                    }
                                }
                                $("#<%=hiddriverInfo.ClientID %>").val(info);
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
                $("#<%=txtContectName.ClientID %>").val(obj.contactname);
                $("#<%=txtContectPhone.ClientID %>").val(obj.contacttel);
                $("#<%=txtContectFax.ClientID %>").val(obj.contactfax);
                $("#<%=SupplierControl1.ClientzyykValue %>").val(obj.hideID_zyyk);
                $("#<%=SupplierControl1.ClientIsyukong %>").val(obj.isYuKong);
                $("#<%=txtUseCarNums.ClientID %>").val(tableToolbar.calculate(obj.ControlNum, obj.UserNum, "-"));
                $("#<%=hidCarNum.ClientID %>").val(tableToolbar.calculate(obj.ControlNum, obj.UserNum, "-"));
                var supplierType = '<%=(int)EyouSoft.Model.EnumType.PlanStructure.PlanProject.�ó� %>';
                var companyId = "<%=this.SiteUserInfo.CompanyId %>";
                $("select[name='selCarModel'] option").each(function() {
                    if ($(this).val() != "") {
                        $(this).remove();
                    }
                });
                if (obj.isYuKong == "0") {
                    if (obj.id) {
                        CarPage._AjaxReqCarModel(obj.id, supplierType, "1", companyId);
                    }
                }
                else {
                    if (obj.hideID_zyyk) {
                        CarPage._AjaxReqCarModel(obj.hideID_zyyk, supplierType, "2", companyId);
                    }
                }
            }
        }
        $(function() {
            CarPage._InitPage();
            parent.ConfigPage.SetWinHeight();
        });
    </script>

</body>
</html>

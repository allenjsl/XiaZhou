<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OperaterGuiderList.aspx.cs"
    Inherits="Web.OperaterCenter.OperaterGuiderList" %>

<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register TagName="Guid" TagPrefix="uc4" Src="~/UserControl/selectGuid.ascx" %>
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
    <form id="formGuidPlan" runat="server">
    <div class="jidiao-r">
        <div id="con_faq_2">
            <table width="100%" cellpadding="0" cellspacing="0" class="jd-table01 line-b">
                <tr>
                    <th width="15%" align="right" class="border-l">
                        <span class="fontred">*</span>����������
                    </th>
                    <td width="40%">
                        <uc4:Guid ID="GuidControl1" runat="server" CallBackFun="GuidPage._AjaxContectInfo" />
                    </td>
                    <th width="15%" align="right">
                        ���ε绰��
                    </th>
                    <td width="30%">
                        <asp:TextBox ID="txtguidPhone" runat="server" CssClass="inputtext formsize140"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th align="right" class="border-l">
                        <span class="fontred">*</span>���ŵص㣺
                    </th>
                    <td>
                        <asp:TextBox ID="txtGroupPlace" runat="server" CssClass="inputtext formsize140" valid="required"
                            errmsg="*���������ŵص�!"></asp:TextBox>
                    </td>
                    <th align="right">
                        <span class="addtableT"><span class="fontred">*</span> ����ʱ�䣺</span>
                    </th>
                    <td>
                        <asp:TextBox ID="txtGroupTime" runat="server" CssClass="inputtext formsize80" valid="required"
                            errmsg="*����������ʱ��!"></asp:TextBox>
                        <a href="javascript:void(0);" class="timesicon" onclick="WdatePicker({el:'<%=txtGroupTime.ClientID %>',minDate:'%y-%M-#{%d}'})">
                            ����ʱ��</a>
                    </td>
                </tr>
                <tr>
                    <th align="right" class="border-l">
                        <span class="fontred">*</span>���ŵص㣺
                    </th>
                    <td>
                        <asp:TextBox ID="txtUnderPlace" runat="server" CssClass="inputtext formsize140" valid="required"
                            errmsg="*���������ŵص�!"></asp:TextBox>
                    </td>
                    <th align="right">
                        <span class="fontred">*</span>����ʱ�䣺
                    </th>
                    <td>
                        <asp:TextBox ID="txtunderTime" runat="server" CssClass="inputtext formsize80" valid="required"
                            errmsg="*����������ʱ��!"></asp:TextBox>
                        <a href="javascript:void(0);" class="timesicon" onclick="WdatePicker({el:'<%=txtunderTime.ClientID %>',minDate:'#F{$dp.$D(\'<%=txtGroupTime.ClientID %>\')}'})">
                            ����ʱ��</a>
                    </td>
                </tr>
                <tr>
                    <th align="right" class="border-l">
                        <span class="fontred">*</span>�������ͣ�
                    </th>
                    <td colspan="3" align="left">
                        <asp:DropDownList ID="guidType" runat="server" CssClass="inputselect">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <th align="right" class="border-l">
                        �Ӵ��г̣�
                    </th>
                    <td colspan="3">
                        <asp:TextBox ID="txttravel" runat="server" TextMode="MultiLine" CssClass="inputtext formsize600"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th align="right" class="border-l">
                        �����׼��
                    </th>
                    <td colspan="3">
                        <asp:TextBox ID="txtserverStand" runat="server" TextMode="MultiLine" CssClass="inputtext formsize600"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th align="right" class="border-l">
                        ������֪��
                    </th>
                    <td colspan="3">
                        <asp:TextBox ID="txtDaoYouXuZhi" runat="server" TextMode="MultiLine" CssClass="inputtext formsize600"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th align="right" class="border-l">
                        ������ϸ��
                    </th>
                    <td colspan="3">
                        <asp:TextBox ID="txtCostParticu" runat="server" TextMode="MultiLine" CssClass="inputtext formsize600"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th align="right" class="border-l">
                        <span class="fontred">*</span>������ã�
                    </th>
                    <td align="left">
                        <asp:TextBox ID="txtCostAccount" runat="server" CssClass="inputtext formsize80" valid="required|isMoney"
                            errmsg="*������������!|*���������������!"></asp:TextBox>
                        Ԫ
                    </td>
                    <th align="right">
                        <span class="fontred">*</span>״̬��
                    </th>
                    <td align="left">
                        <select name="status" class="inputselect">
                            <%=UtilsCommons.GetOperaterStatusList(states)%>
                        </select>
                    </td>
                </tr>
            </table>
            <asp:PlaceHolder ID="phdShowList" runat="server">
                <h2>
                    <p>
                        �Ѱ��ŵ���</p>
                </h2>
                <table width="100%" border="0" cellspacing="0" cellpadding="0" class="jd-table01"
                    style="border-bottom: 1px solid #A9D7EC;" id="actionType">
                    <tr>
                        <th align="center" class="border-l">
                            ��������
                        </th>
                        <th align="center">
                            ���ε绰
                        </th>
                        <th align="center">
                            ����ʱ��
                        </th>
                        <th align="center">
                            ����ʱ��
                        </th>
                        <th align="center">
                            ������ϸ
                        </th>
                        <th align="right">
                            �������
                        </th>
                        <th align="center">
                            ״̬
                        </th>
                        <th align="center">
                            ����
                        </th>
                        <th align="center">
                            ����
                        </th>
                    </tr>
                    <asp:Repeater ID="repGuidList" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td align="center" class="border-l">
                                    <%# Eval("SourceName")%>
                                </td>
                                <td align="center">
                                    <%# Eval("ContactPhone")%>
                                </td>
                                <td align="center">
                                    <%# UtilsCommons.GetDateString(Eval("StartDate"),ProviderToDate)%>
                                </td>
                                <td align="center">
                                    <%# UtilsCommons.GetDateString(Eval("EndDate"),ProviderToDate)%>
                                </td>
                                <td align="center">
                                    <span title="<%# Eval("CostDetail") %>">
                                        <%# EyouSoft.Common.Utils.GetText2( Eval("CostDetail").ToString(),30,true) %></span>
                                </td>
                                <td align="right">
                                    <%#UtilsCommons.GetMoneyString(Convert.ToDecimal(Eval("Confirmation")),ProviderToMoney)%><a
                                        href="javascript:void(0);" data-class="Prepaid" title="Ԥ������" data-id="<%# Eval("PlanId") %>"
                                        data-soucesname="<%# Eval("SourceName")%>">
                                        <img src="/images/yufu.gif" alt="" /></a>
                                </td>
                                <td align="center">
                                    <%# Eval("Status").ToString()%>
                                </td>
                                <td align="center">
                                    <a href='<%#querenUrl %>?tourId=<%# Eval("TourId") %>&anpaiid=<%#Eval("PlanId") %>' target="_blank">
                                        <img src="/images/y-kehuqueding.gif" alt="" /></a>
                                </td>
                                <td align="center">
                                    <%if (ListPower)
                                      { %>
                                    <a href="javascript:void(0);" data-class="updateGuid">
                                        <img src="/images/y-delupdateicon.gif" alt="" data-id="<%# Eval("PlanId") %>" />
                                        �޸�</a> <a href="javascript:void(0);" data-class="deleteGuid">
                                            <img src="/images/y-delicon.gif" alt="" data-id="<%# Eval("PlanId") %>" />
                                            ɾ��</a>
                                    <%}
                                      else
                                      { %>
                                    <a href="javascript:void(0);" data-class="showGuid">
                                        <img src="/images/y-delupdateicon.gif" alt="" data-id="<%# Eval("PlanId") %>" />
                                        �鿴</a>
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
                    <ul id="ul_guidBtn_list">
                        <li class="cun-cy"><a href="javascript:" id="btnSave">����</a></li>
                    </ul>
                </div>
            </asp:PlaceHolder>
        </div>
    </div>
    </form>

    <script type="text/javascript">
        var GuidPage = {
            sl: '<%=SL %>',
            type: '<%=Utils.GetQueryStringValue("Type") %>',
            tourId: '<%=Utils.GetQueryStringValue("tourId") %>',
            iframeId: '<%=Utils.GetQueryStringValue("iframeId") %>',
            _DeleteGuid: function(objID) {
                var _url = '/OperaterCenter/OperaterGuiderList.aspx?type=' + GuidPage.type + '&sl=' + GuidPage.sl + '&tourId=' + GuidPage.tourId + '&iframeId=' + GuidPage.iframeId + "&m=" + new Date().getTime();
                $.newAjax({
                    type: "get",
                    url: '/OperaterCenter/OperaterGuiderList.aspx?sl=' + GuidPage.sl + '&action=delete&planId=' + objID + '&tourid=' + GuidPage.tourId,
                    cache: false,
                    dataType: "json",
                    success: function(ret) {
                        parent.tableToolbar._showMsg(ret.msg, function() {
                            window.location.href = _url;
                        });
                        return false;
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
            _SaveGuid: function(planID, tourId) {
                if (ValiDatorForm.validator($("#<%=formGuidPlan.ClientID %>").get(0), "parent")) {
                    var grouptime = new Date($("#txtGroupTime").val());
                    var undertime = new Date($("#txtunderTime").val());
                    if (grouptime > undertime) {
                        parent.tableToolbar._showMsg("����ʱ�䲻����������ʱ��");
                        return false;
                    }

                    $("#btnSave").text("������...").css("background-position", "0 -62px").unbind("click");

                    var _url = '/OperaterCenter/OperaterGuiderList.aspx?type=' + GuidPage.type + '&sl=' + GuidPage.sl + '&tourId=' + GuidPage.tourId + '&iframeId=' + GuidPage.iframeId + "&m=" + new Date().getTime();

                    $.newAjax({
                        type: "POST",
                        url: '/OperaterCenter/OperaterGuiderList.aspx?action=save&sl=' + GuidPage.sl + '&planId=' + planID + "&tourId=" + tourId,
                        cache: false,
                        data: $("#btnSave").closest("form").serialize(),
                        dataType: "json",
                        success: function(ret) {
                            if (ret.result == "1") {
                                parent.tableToolbar._showMsg(ret.msg, function() {
                                    window.location.href = _url;
                                });
                            } else {
                                parent.tableToolbar._showMsg(ret.msg, function() {
                                    $("#btnSave").text("����").css("background-position", "0 0px").bind("click");
                                    $("#btnSave").click(function() {
                                        GuidPage._SaveGuid('<%=EyouSoft.Common.Utils.GetQueryStringValue("planId") %>', GuidPage.tourId);
                                        return false;
                                    });
                                });
                            }
                        },
                        error: function() {
                            parent.tableToolbar._showMsg(tableToolbar.errorMsg);
                            return false;
                        }
                    });
                }
            },
            _BindBtn: function() {
                $("#actionType").find("a[data-class='deleteGuid']").unbind("click");
                $("#actionType").find("a[data-class='deleteGuid']").click(function() {
                    var newThis = $(this);
                    parent.tableToolbar.ShowConfirmMsg("ȷ��ɾ������������?", function() {
                        var planId = newThis.find("img").attr("data-id");
                        if (planId) {
                            GuidPage._DeleteGuid(planId);
                        }
                    });
                    return false;
                });

                $("#actionType").find("a[data-class='updateGuid']").unbind("click");
                $("#actionType").find("a[data-class='updateGuid']").click(function() {
                    var planId = $(this).find("img").attr("data-id");
                    if (planId) {
                        window.location.href = '/OperaterCenter/OperaterGuiderList.aspx?type=' + GuidPage.type + '&sl=' + GuidPage.sl + '&tourId=' + GuidPage.tourId + '&action=update&planId=' + planId + '&iframeId=' + GuidPage.iframeId;

                    }
                    return false;
                });

                //�鿴
                $("#actionType").find("a[data-class='showGuid']").unbind("click");
                $("#actionType").find("a[data-class='showGuid']").click(function() {
                    var planId = $(this).find("img").attr("data-id");
                    if (planId) {
                        window.location.href = '/OperaterCenter/OperaterGuiderList.aspx?type=' + GuidPage.type + '&sl=' + GuidPage.sl + '&tourId=' + GuidPage.tourId + '&action=update&planId=' + planId + '&iframeId=' + GuidPage.iframeId + "&show=1";

                    }
                    return false;
                });

                $("#actionType").find("a[data-class='Prepaid']").unbind("click");
                $("#actionType").find("a[data-class='Prepaid']").click(function() {
                    var planId = $(this).attr("data-id");
                    var soucesName = $(this).attr("data-soucesname");
                    GuidPage._OpenBoxy("Ԥ������", '/OperaterCenter/PrepaidAppliaction.aspx?PlanId=' + planId + '&type=' + GuidPage.type + '&sl=' + GuidPage.sl + '&tourId=' + GuidPage.tourId + '&souceName=' + escape(soucesName), "650px", "550px", true);
                    return false;
                });

                $("#btnSave").unbind("click").text("����").css("background-position", "0 0px");
                $("#btnSave").click(function() {
                    GuidPage._SaveGuid('<%=EyouSoft.Common.Utils.GetQueryStringValue("planId") %>', GuidPage.tourId);
                    return false;
                });
            },
            _PageInit: function() {
                GuidPage._BindBtn();
                $("#<%=txtGroupTime.ClientID %>").focus(function() {
                    WdatePicker({ minDate: '%y-%M-#{%d' });
                });
                $("#<%=txtunderTime.ClientID %>").focus(function() {
                    WdatePicker({ minDate: '#F{$dp.$D(\'<%=txtGroupTime.ClientID %>\')}' });
                });
                if ('<%=EyouSoft.Common.Utils.GetQueryStringValue("show") %>' == "1") {
                    $("#ul_guidBtn_list").parent("div").hide();
                };
                $('#<%=GuidControl1.GuidNameClient %>').attr("valid", "required").attr("errmsg", "*��ѡ����!").css("background-color", "#dadada").attr("readonly", "readonly");
            },
            _AjaxContectInfo: function(obj) {
                $('#<%=GuidControl1.GuidIDClient %>').val(obj.value);
                $('#<%=GuidControl1.GuidNameClient %>').val(obj.text);
                $("#<%=txtguidPhone.ClientID %>").val(obj.phone);
            }
        }


        $(function() {
            GuidPage._PageInit();
            parent.ConfigPage.SetWinHeight();
        });
    </script>

</body>
</html>

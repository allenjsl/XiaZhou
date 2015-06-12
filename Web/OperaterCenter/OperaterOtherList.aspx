<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OperaterOtherList.aspx.cs"
    Inherits="Web.OperaterCenter.OperaterOtherList" %>

<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register TagName="suppliercon" TagPrefix="uc6" Src="~/UserControl/SupplierControl.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>

    <script src="/Js/datepicker/WdatePicker.js" type="text/javascript"></script>

    <script src="/Js/ValiDatorForm.js" type="text/javascript"></script>

    <link href="/css/style.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="/Js/jquery-1.4.4.js"></script>

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
    <form id="formpOther" runat="server">
    <div class="jidiao-r">
        <div id="con_faq_11">
            <table width="100%" cellpadding="0" cellspacing="0" class="jd-table01 line-b">
                <tr>
                    <th width="15%" align="right" class="border-l">
                        <span class="fontred">*</span>供应商：
                    </th>
                    <td width="40%">
                        <uc6:suppliercon ID="supplierControl1" runat="server" CallBack="OtherPage._AjaxContectInfo"
                            Flag="0" IsMust="true" SupplierType="其它" />
                    </td>
                    <th width="15%" align="right">
                        联系人：
                    </th>
                    <td width="30%">
                        <asp:TextBox ID="txtContactName" runat="server" CssClass="inputtext formsize140"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th width="15%" align="right" class="border-l">
                        联系电话：
                    </th>
                    <td>
                        <asp:TextBox ID="txtContactPhone" runat="server" CssClass="inputtext formsize140"></asp:TextBox>
                    </td>
                    <th width="15%" align="right">
                        <span class="addtableT">联系传真：</span>
                    </th>
                    <td>
                        <asp:TextBox ID="txtContactFax" runat="server" CssClass="inputtext formsize140"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th align="right" class="border-l">
                        支出项目：
                    </th>
                    <td colspan="3">
                        <asp:TextBox ID="txtOutProject" runat="server" CssClass="inputtext formsize600"
                            TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th align="right" class="border-l">
                        人数：
                    </th>
                    <td>
                        <asp:TextBox ID="txtNums" runat="server" CssClass="inputtext formsize50"></asp:TextBox>
                    </td>
                    <th align="right">
                        <span class="fontred">*</span> 结算费用：
                    </th>
                    <td>
                        <asp:TextBox ID="txtTotalPrices" runat="server" CssClass="inputtext formsize50" valid="required|isMoney"
                            errmsg="*请输入结算费用!|*结算费用输入有误!" ></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th align="right" class="border-l">
                        导游需知：
                    </th>
                    <td colspan="3">
                        <asp:TextBox ID="txtGuisNotes" runat="server" TextMode="MultiLine" CssClass="inputtext formsize600"></asp:TextBox>
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
                    <td>
                        <select name="selPanyMent" class="inputselect">
                            <%=UtilsCommons.GetOperaterPanyMentList(panyMent)%>
                        </select>
                    </td>
                    <th align="right">
                        <span class="addtableT"><span class="fontred">*</span> 状态：</span>
                    </th>
                    <td>
                        <select name="selStatus" class="inputselect">
                            <%=UtilsCommons.GetOperaterStatusList(status)%>
                        </select>
                    </td>
                </tr>
            </table>
            <asp:PlaceHolder ID="phdShowList" runat="server">
                <h2>
                    <p>
                        已安排其它</p>
                </h2>
                <table id="actionType" width="100%" border="0" cellspacing="0" cellpadding="0" class="jd-table01"
                    style="border-bottom: 1px solid #A9D7EC;">
                    <tr>
                        <th align="center" class="border-l">
                            供应商名称
                        </th>
                        <th align="center">
                            支出项目
                        </th>
                        <th align="center">
                            人数
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
                    <asp:Repeater ID="repOtherList" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td align="center" class="border-l">
                                    <%# Eval("SourceName")%>
                                </td>
                                <td align="center">
                                    <span title="<%# Eval("CostDetail") %>">
                                        <%# EyouSoft.Common.Utils.GetText2( Eval("CostDetail").ToString(),30,true) %></span>
                                </td>
                                <td align="center">
                                    <%# Eval("Num")%>
                                </td>
                                <td align="right">
                                    <%#UtilsCommons.GetMoneyString(Convert.ToDecimal(Eval("Confirmation")),ProviderToMoney)%>
                                    <a href="javascript:void(0);" data-class="Prepaid" title="预付申请" data-id="<%# Eval("PlanId") %>"
                                        data-soucesname="<%# Eval("SourceName")%>">
                                        <img src="/images/yufu.gif" alt="" /></a>
                                </td>
                                <td align="center">
                                    <%# Eval("PaymentType").ToString()%>
                                </td>
                                <td align="center">
                                    <%# Eval("Status").ToString()%>
                                </td>
                                <td align="center">
                                    <a href='<%#querenUrl %>?planId=<%# Eval("PlanId") %>' target="_blank">
                                        <img src="/images/y-kehuqueding.gif" border="0" alt="" /></a>
                                </td>
                                <td align="center">
                                    <%if (ListPower)
                                      { %>
                                    <a href="javascript:" data-class="update">
                                        <img src="/images/y-delupdateicon.gif" alt="" data-id="<%# Eval("PlanId") %>" />
                                        修改</a> <a href="javascript:" data-class="delete">
                                            <img src="/images/y-delicon.gif" alt="" data-id="<%# Eval("PlanId") %>" />
                                            删除</a>
                                    <%}
                                      else
                                      { %>
                                    <a href="javascript:" data-class="show">
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
            <asp:PlaceHolder ID="panView" runat="server">
                <div class="mainbox cunline fixed">
                    <ul id="ul_btn_list">
                        <li class="cun-cy"><a href="javascript:void(0);" id="btnSave">保存</a> </li>
                    </ul>
                </div>
            </asp:PlaceHolder>
        </div>
        <div class="hr_10">
        </div>
        <input type="hidden" id="hidUserNum" runat="server" />
    </div>
    </form>

    <script type="text/javascript">
        var OtherPage = {
            sl: '<%=SL %>',
            type: '<%=Utils.GetQueryStringValue("Type") %>',
            tourId: '<%=Utils.GetQueryStringValue("tourId") %>',
            iframeId: '<%=Utils.GetQueryStringValue("iframeId") %>',
            _DeleteOther: function(_ObjID) {
                var _Url = '/OperaterCenter/OperaterOtherList.aspx?sl=' + OtherPage.sl + '&type=' + OtherPage.type + '&tourId=' + OtherPage.tourId + "&iframeId=" + OtherPage.iframeId + "&m=" + new Date().getTime();
                $.newAjax({
                    type: "get",
                    url: '/OperaterCenter/OperaterOtherList.aspx?sl=' + OtherPage.sl + '&action=delete&planId=' + _ObjID + '&tourid=' + OtherPage.tourId,
                    cache: false,
                    dataType: 'json',
                    success: function(data) {
                        if (data.result == "1") {
                            tableToolbar._showMsg(data.msg, function() {
                                window.location.href = _Url;
                            });
                            return false;
                        }
                        else {
                            tableToolbar._showMsg(data.msg, function() {
                                window.location.href = _Url;
                            });
                            return false;
                        }
                    },
                    error: function() {
                        tableToolbar._showMsg(tableToolbar.errorMsg);
                        return false;
                    }
                });
            },
            _OpenBoxy: function(title, iframeUrl, width, height, draggable) {
                parent.Boxy.iframeDialog({ title: title, iframeUrl: iframeUrl, width: width, height: height, draggable: draggable });
            },
            _Saveother: function(tourId, planId) {
                var _Url = '/OperaterCenter/OperaterOtherList.aspx?sl=' + OtherPage.sl + '&type=' + OtherPage.type + '&tourId=' + OtherPage.tourId + '&iframeId=' + OtherPage.iframeId + "&m=" + new Date().getTime();
                $.newAjax({
                    type: "POST",
                    url: "/OperaterCenter/OperaterOtherList.aspx?action=save&tourId=" + tourId + "&planId=" + planId + "&sl=" + OtherPage.sl,
                    cache: false,
                    data: $("#btnSave").closest("form").serialize(),
                    dataType: "json",
                    success: function(data) {
                        if (data.result == "1") {
                            tableToolbar._showMsg(data.msg, function() {
                                window.location.href = _Url;
                            });
                        } else {
                            tableToolbar._showMsg(data.msg, function() {
                                $("#btnSave").text("保存").css("background-position", "0 0px").bind("click");
                                $("#btnSave").click(function() {
                                    $(this).text("保存中...").css("background-position", "0 -62px").unbind("click");
                                    var _planID = '<%=Utils.GetQueryStringValue("planId") %>';
                                    OtherPage._Saveother(OtherPage.tourId, _planID);
                                });
                            });
                        }
                    },
                    error: function() {
                        tableToolbar._showMsg(tableToolbar.errorMsg);
                        return false;
                    }
                });
            },
            _BindBtn: function() {
                $("#actionType").find("a[data-class='Prepaid']").unbind("click").click(function() {
                    var planId = $(this).attr("data-ID");
                    var soucesName = $(this).attr("data-soucesname");
                    OtherPage._OpenBoxy("预付申请", '/OperaterCenter/PrepaidAppliaction.aspx?PlanId=' + planId + '&type=' + OtherPage.type + '&sl=' + OtherPage.sl + '&tourId=' + OtherPage.tourId + '&souceName=' + escape(soucesName), "650px", "550px", true);
                    return false;
                });

                $("#actionType").find("a[data-class='delete']").unbind("click").click(function() {
                    var newThis = $(this);
                    tableToolbar.ShowConfirmMsg("确定删除此条数据吗?", function() {
                        var planId = newThis.find("img").attr("data-ID");
                        if (planId) {
                            OtherPage._DeleteOther(planId);
                        }
                    });
                    return false;
                });

                $("#actionType").find("a[data-class='update']").unbind("click").click(function() {
                    var planId = $(this).find("img").attr("data-ID");
                    if (planId) {
                        window.location.href = '/OperaterCenter/OperaterOtherList.aspx?type=' + OtherPage.type + '&sl=' + OtherPage.sl + '&tourId=' + OtherPage.tourId + '&action=update&planId=' + planId + '&iframeId=' + OtherPage.iframeId;
                    }
                    return false;
                });

                $("#actionType").find("a[data-class='show']").unbind("click").click(function() {
                    var planId = $(this).find("img").attr("data-ID");
                    if (planId) {
                        window.location.href = '/OperaterCenter/OperaterOtherList.aspx?type=' + OtherPage.type + '&sl=' + OtherPage.sl + '&tourId=' + OtherPage.tourId + '&action=update&planId=' + planId + '&iframeId=' + OtherPage.iframeId + "&show=1";
                    }
                    return false;
                });

                $("#btnSave").unbind("click").text("保存").css("background-position", "0 0px");
                $("#btnSave").click(function() {
                    if (!ValiDatorForm.validator($("#<%=formpOther.ClientID %>").get(0), "parent")) {
                        return false;
                    } else {
                        $(this).text("保存中...").css("background-position", "0 -62px").unbind("click");
                        var planId = '<%=Utils.GetQueryStringValue("planId") %>';
                        OtherPage._Saveother(OtherPage.tourId, planId);
                    }
                });

            },
            _PageInit: function() {
                this._BindBtn();
                if ('<%=Utils.GetQueryStringValue("show") %>' == "1") {
                    $("#ul_btn_list").parent("div").hide();
                }
            },
            _AjaxContectInfo: function(obj) {
                if (obj) {
                    $("#<%=supplierControl1.ClientText %>").val(obj.name);
                    $("#<%=supplierControl1.ClientValue %>").val(obj.id);
                    $("#<%=supplierControl1.ClientzyykValue %>").val(obj.hideID_zyyk);
                    $("#<%=supplierControl1.ClientIsyukong %>").val(obj.isYuKong);
                    $("#<%=txtContactName.ClientID %>").val(obj.contactname);
                    $("#<%=txtContactPhone.ClientID %>").val(obj.contacttel);
                    $("#<%=txtContactFax.ClientID %>").val(obj.contactfax);
                }
            	//清空支出项目
            	$("#<%=txtOutProject.ClientID %>").val("");
            	
            	//清空人数
            	$("#<%=txtNums.ClientID %>").val("");
            	$("#<%=hidUserNum.ClientID %>").val("");
            	
                var supplierType = '<%=(int)EyouSoft.Model.EnumType.PlanStructure.PlanProject.其它 %>';
                var companyId = "<%=this.SiteUserInfo.CompanyId %>";
            	var source = "1";
            	var id = obj.id;
                if (obj.isYuKong == "1") {
                    if (obj.hideID_zyyk != "") {
                    	source = "2";
                    	id = obj.hideID_zyyk;
                    }
                }
                $.newAjax({
                    type: "POST",
                    url: "/Ashx/GetSupplierContect.ashx?suppId=" + id + "&type=" + supplierType+"&source="+source+"&company="+companyId,
                    async: false,
                    dataType: "json",
                    success: function(data) {
                        if (data.tolist.length > 0) {
                            var html = [], mTp = '<%=ProviderMoneyStr %>';
                        	var outproject = "";
                            html.push('<tr><th>名称</th><th>销售价</th><th>结算价</th><th>简要描述</th></tr>');
                            for (var i = 0; i < data.tolist.length; i++) {
                            	outproject += data.tolist[i].Name + " ";
                            	if (data.tolist[i].userNum) {
                                    $("#<%=txtNums.ClientID %>").val(data.tolist[i].userNum);
                                    $("#<%=hidUserNum.ClientID %>").val(data.tolist[i].userNum);
                                }
                            	
                                html.push('<tr><td>' + data.tolist[i].Name + '</td><td>' + mTp + data.tolist[i].Price + '</td><td>' + mTp + data.tolist[i].ClosingCost + '</td><td>' + data.tolist[i].Desc + '</td></tr>');
                            }
                        	//支出项目
                        	$("#<%=txtOutProject.ClientID %>").val(outproject);
                        	
                            //从供应商选用显示参考价格
                            if (source == 1) { parent.ConfigPage.ShowListDiv(html.join('')); $("#a_openInfoDiv").click(function() { parent.ConfigPage.ShowListDiv(""); }); } else { $("#a_openInfoDiv").unbind("click"); }
                        }
                    },
                    error: function() {
                        parent.tableToolbar._showMsg(tableToolbar.errorMsg);
                        return false;
                    }
                });
            }
        }
        $(document).ready(function() {
            OtherPage._PageInit();
            parent.ConfigPage.SetWinHeight();
        });
    </script>

</body>
</html>

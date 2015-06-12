<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OperaterAttractionsList.aspx.cs"
    Inherits="Web.OperaterCenter.OperaterAttractionsList" %>

<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register TagName="supplierControl" TagPrefix="uc6" Src="~/UserControl/SupplierControl.ascx" %>
<%@ Register TagName="profit" TagPrefix="uc7" Src="~/UserControl/Operaterprofit.ascx" %>
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
    <form id="formAttr" runat="server">
    <div class="jidiao-r">
        <div id="con_faq_6">
            <table width="100%" cellpadding="0" cellspacing="0" class="jd-table01 line-b">
                <tr>
                    <th width="15%" align="right" class="border-l">
                        <span class="fontred">*</span>景点公司：
                    </th>
                    <td width="40%">
                        <uc6:supplierControl ID="supplierControl1" runat="server" CallBack="SpotPage._AjaxContectInfo"
                            IsMust="true" Flag="0" SupplierType="景点" />
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
                        <asp:TextBox ID="txtContectPhone" runat="server" CssClass="inputtext formsize140"></asp:TextBox>
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
                        <span class="fontred">*</span><a href="javascript:void(0);" id="a_openInfoDiv">景点名称</a>：
                    </th>
                    <td>
                        <select name="spotList" class="inputselect" valid="required" errmsg="*请选择景点!">
                            <option value="">--请选择--</option>
                            <%=SpotNameList.ToString() %>
                        </select>
                    </td>
                    <th align="right">
                        <span class="border-l"><span class="fontred">*</span>人数：</span>
                    </th>
                    <td>
                        成人
                        <asp:TextBox ID="txtAdultNums" runat="server" CssClass="inputtext formsize50" valid="required|RegInteger"
                            errmsg="*请输入人数!|*人数必须是正整数!"></asp:TextBox>
                        人 儿童
                        <asp:TextBox ID="txtChildrenNums" runat="server" CssClass="inputtext formsize50"></asp:TextBox>
                        人
                    </td>
                </tr>
                <tr>
                    <th align="right" class="border-l">
                        <span class="fontred">*</span> 游览时间：
                    </th>
                    <td colspan="3" id="trSpotTime">
                        <asp:TextBox ID="txtLookAbout1" runat="server" CssClass="inputtext formsize80" valid="required"
                            errmsg="*请选择浏览开始日期!"></asp:TextBox>
                        </a>
                        <asp:TextBox ID="txtScanTime1" runat="server" CssClass="inputtext formsize50"></asp:TextBox>
                        至
                        <asp:TextBox ID="txtLookAbout2" runat="server" CssClass="inputtext formsize80"></asp:TextBox>                        
                        <asp:TextBox ID="txtScanTime2" runat="server" CssClass="inputtext formsize50"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th align="right" class="border-l">
                        费用明细：
                    </th>
                    <td colspan="3">
                        <asp:TextBox ID="txtCostParticu" runat="server" TextMode="MultiLine" CssClass="inputtext formsize600">
                        </asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th align="right" class="border-l">
                        <span class="fontred">*</span>结算费用：
                    </th>
                    <td>
                        <asp:TextBox ID="txttotalMoney" runat="server" CssClass="inputtext formsize50" valid="required|isMoney"
                            errmsg="*请输入结算费用!|*结算费用输入有误!"></asp:TextBox>
                        元
                    </td>
                    <th align="right">
                        <span class="fontred">*</span>支付方式：
                    </th>
                    <td>
                        <select name="SelPanyMent" class="inputselect">
                            <%=EyouSoft.Common.UtilsCommons.GetOperaterPanyMentList(panyMent)%>
                        </select>
                    </td>
                </tr>
                <tr>
                    <th align="right" class="border-l">
                        导游需知：
                    </th>
                    <td colspan="3">
                        <asp:TextBox ID="txtGuidNotes" runat="server" CssClass="inputtext formsize600" TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th align="right" class="border-l">
                        景点备注：
                    </th>
                    <td colspan="3">
                        <asp:TextBox ID="txtOtherRemarks" runat="server" CssClass="inputtext formsize600"
                            TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th align="right" class="border-l">
                        <span class="addtableT"><span class="fontred">*</span> 返利：</span>
                    </th>
                    <td>
                        <asp:DropDownList ID="ddlProfit1" class="inputselect" runat="server">
                            <asp:ListItem Value="0">有</asp:ListItem>
                            <asp:ListItem Value="1">无</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <th align="right">
                        <span class="fontred">* </span><span class="addtableT">状态</span>：
                    </th>
                    <td>
                        <select name="SelStatus" class="inputselect">
                            <%=UtilsCommons.GetOperaterStatusList(Status)%>
                        </select>
                    </td>
                </tr>
            </table>
            <asp:PlaceHolder ID="phdShowList" runat="server">
                <h2>
                    <p>
                        已安排景点</p>
                </h2>
                <table width="100%" border="0" cellspacing="0" cellpadding="0" class="jd-table01"
                    style="border-bottom: 1px solid #A9D7EC;" id="tblSpotList">
                    <tr>
                        <th align="center">
                            景点公司
                        </th>
                        <th align="center" class="border-l">
                            游览时间
                        </th>
                        <th align="center">
                            人数
                        </th>
                        <th align="center">
                            费用明细
                        </th>
                        <th align="center">
                            支付方式
                        </th>
                        <th align="right">
                            小计费用
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
                    <asp:Repeater ID="repSpotList" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td align="center">
                                    <%# Eval("SourceName")%>
                                </td>
                                <td align="center">
                                    <%#Convert.ToDateTime(Eval("StartDate")).Month %>/<%#Convert.ToDateTime(Eval("StartDate")).Day %>
                                    <%# Eval("StartTime") %>-
                                    <%#Convert.ToDateTime(Eval("EndDate")).Month%>/<%#Convert.ToDateTime(Eval("EndDate")).Day%>
                                    <%# Eval("EndTime") %>
                                </td>
                                <td align="center">
                                    <b class="fontblue">
                                        <%# Eval("AdultNumber")%></b><sup class="fontred">+<%# Eval("ChildNumber")%></sup>
                                </td>
                                <td align="center">
                                    <span title="<%# Eval("CostDetail") %>">
                                        <%# EyouSoft.Common.Utils.GetText(Eval("CostDetail").ToString(),30)%></span>
                                </td>
                                <td align="center">
                                    <%# Eval("PaymentType").ToString()%>
                                </td>
                                <td align="right">
                                    <%# UtilsCommons.GetMoneyString(Convert.ToDecimal(Eval("Confirmation")),ProviderToMoney)%>
                                    <a href="javascript:" data-class="Prepaid" title="预付申请" data-id="<%# Eval("PlanId") %>"
                                        data-soucesname="<%# Eval("SourceName")%>">
                                        <img src="/images/yufu.gif" alt="" /></a>
                                </td>
                                <td align="center">
                                    <%# Eval("Status").ToString()%>
                                </td>
                                <td align="center">
                                    <a href='<%#querenUrl %>?planId=<%#Eval("PlanId")%>' target="_blank">
                                        <img src="/images/y-kehuqueding.gif" alt="" /></a>
                                </td>
                                <td align="center">
                                    <%if (ListPower)
                                      { %>
                                    <a href="javascript:" data-class="updateSpot">
                                        <img src="/images/y-delupdateicon.gif" alt="" data-id="<%# Eval("PlanId") %>" />
                                        修改</a> <a href="javascript:" data-class="deleteSpot">
                                            <img src="/images/y-delicon.gif" alt="" data-id="<%# Eval("PlanId") %>" />
                                            删除</a>
                                    <%}
                                      else
                                      { %>
                                    <a href="javascript:" data-class="showSpot">
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
                    <ul id="ul_spot_list">
                        <li class="cun-cy"><a href="javascript:" id="btnSave">保存</a> </li>
                    </ul>
                </div>
            </asp:PlaceHolder>
        </div>
        <input type="hidden" id="hidUserNum" runat="server" />
    </div>
    </form>

    <script type="text/javascript">
        var SpotPage = {
            sl: '<%=SL %>',
            type: '<%=Utils.GetQueryStringValue("Type") %>',
            tourId: '<%=Utils.GetQueryStringValue("tourId") %>',
            iframeId: '<%=Utils.GetQueryStringValue("iframeId") %>',
            _OpenBoxy: function(title, iframeUrl, width, height, draggable) {
                parent.Boxy.iframeDialog({ title: title, iframeUrl: iframeUrl, width: width, height: height, draggable: draggable });
            },
            _deleteSpot: function(objID) {
                var _Url = '/OperaterCenter/OperaterAttractionsList.aspx?sl=' + SpotPage.sl + '&type=' + SpotPage.type + '&tourId=' + SpotPage.tourId + "&m=" + new Date().getTime();
                $.newAjax({
                    type: "get",
                    url: '/OperaterCenter/OperaterAttractionsList.aspx?sl=' + SpotPage.sl + '&action=delete&PlanId=' + objID + '&tourid=' + SpotPage.tourId,
                    cache: false,
                    dataType: 'json',
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
            _SaveSpot: function(planId, tourId) {
                var _Url = '/OperaterCenter/OperaterAttractionsList.aspx?sl=' + SpotPage.sl + '&type=' + SpotPage.type + '&tourId=' + SpotPage.tourId + '&iframeId=' + SpotPage.iframeId + "&m=" + new Date().getTime();
                $.newAjax({
                    type: "POST",
                    url: '/OperaterCenter/OperaterAttractionsList.aspx?sl=' + SpotPage.sl + '&action=save&PlanId=' + planId + "&tourId=" + tourId,
                    cache: false,
                    data: $("#btnSave").closest("form").serialize(),
                    dataType: "json",
                    success: function(ret) {
                        if (ret.result == "1") {
                            parent.tableToolbar._showMsg(ret.msg, function() {
                                window.location.href = _Url;
                            });
                            return false;
                        } else {
                            parent.tableToolbar._showMsg(ret.msg, function() {
                                $("#btnSave").css("background-position", "0 0px").text("保存").bind("click");
                                $("#btnSave").click(function() {
                                    $(this).css("background-position", "0 -62px").text("保存中...").unbind("click");
                                    var _planID = '<%=Utils.GetQueryStringValue("PlanId") %>';
                                    SpotPage._SaveSpot(_planID, SpotPage.tourId);
                                });
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
            _Bindbtn: function() {
                $("#<%=txtLookAbout1.ClientID %>").focus(function() { WdatePicker(); });

                $("#<%=txtLookAbout2.ClientID %>").focus(function() { WdatePicker(); });

                $("#tblSpotList").find("[data-class='deleteSpot']").unbind("click");
                $("#tblSpotList").find("[data-class='deleteSpot']").click(function() {
                    var newThis = $(this);
                    parent.tableToolbar.ShowConfirmMsg("确定删除此条数据吗?", function() {
                        var PlanId = newThis.find("img").attr("data-ID");
                        if (PlanId) {
                            SpotPage._deleteSpot(PlanId);
                        }
                    });

                    return false;
                });

                //修改
                $("#tblSpotList").find("[data-class='updateSpot']").unbind("click");
                $("#tblSpotList").find("[data-class='updateSpot']").click(function() {
                    var PlanId = $(this).find("img").attr("data-ID");
                    if (PlanId) {
                        window.location.href = '/OperaterCenter/OperaterAttractionsList.aspx?sl=' + SpotPage.sl + '&action=update&PlanId=' + PlanId + '&type=' + SpotPage.type + '&tourId=' + SpotPage.tourId + '&iframeId=' + SpotPage.iframeId;
                    }
                    return false;
                });

                //查看
                $("#tblSpotList").find("[data-class='showSpot']").unbind("click");
                $("#tblSpotList").find("[data-class='showSpot']").click(function() {
                    var PlanId = $(this).find("img").attr("data-ID");
                    if (PlanId) {
                        window.location.href = '/OperaterCenter/OperaterAttractionsList.aspx?sl=' + SpotPage.sl + '&action=update&PlanId=' + PlanId + '&type=' + SpotPage.type + '&tourId=' + SpotPage.tourId + '&iframeId=' + SpotPage.iframeId + "&show=1";
                    }
                    return false;
                });


                $("#tblSpotList").find("[data-class='Prepaid']").unbind("click");
                $("#tblSpotList").find("[data-class='Prepaid']").click(function() {
                    var planId = $(this).attr("data-ID");
                    var soucesName = $(this).attr("data-soucesname");
                    SpotPage._OpenBoxy("预付申请", '/OperaterCenter/PrepaidAppliaction.aspx?PlanId=' + planId + '&type=' + SpotPage.type + '&sl=' + SpotPage.sl + '&tourId=' + SpotPage.tourId + '&souceName=' + escape(soucesName), "650px", "550px", true);
                    return false;
                });

                $("#btnSave").text("保存").css("background-position", "0 0px").unbind("click");
                $("#btnSave").click(function() {
                    if (!ValiDatorForm.validator($("#<%=formAttr.ClientID %>").get(0), "parent")) {
                        return false;
                    } else {
                        $(this).text("保存中...").css("background-position", "0 -62px").unbind("click");
                        var planId = '<%=Utils.GetQueryStringValue("PlanId") %>';
                        SpotPage._SaveSpot(planId, SpotPage.tourId);
                    }
                });

            },
            _InitPage: function() {
                SpotPage._Bindbtn();
                if ('<%=Utils.GetQueryStringValue("show") %>' == "1") {
                    $("#ul_spot_list").parent("div").hide();
                }
            },
            _AjaxContectInfo: function(obj) {
                if (obj) {
                    $("#<%=supplierControl1.ClientText %>").val(obj.name);
                    $("#<%=supplierControl1.ClientValue %>").val(obj.id);
                    $("#<%=supplierControl1.ClientzyykValue %>").val(obj.hideID_zyyk);
                    $("#<%=supplierControl1.ClientIsyukong %>").val(obj.isYuKong);
                    $("#<%=txtContectName.ClientID %>").val(obj.contactname);
                    $("#<%=txtContectPhone.ClientID %>").val(obj.contacttel);
                    $("#<%=txtContectFax.ClientID %>").val(obj.contactfax);
                }

                $("select[name='spotList'] option").each(function() {
                    if ($(this).val() != "") {
                        $(this).remove();
                    }
                });
                //清空人数
                $("#<%=txtAdultNums.ClientID %>").val("");
                $("#<%=hidUserNum.ClientID %>").val("");
                var supplierType = '<%=(int)EyouSoft.Model.EnumType.PlanStructure.PlanProject.景点 %>';
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
                    url: "/Ashx/GetSupplierContect.ashx?suppId=" + id + "&type=" + supplierType + "&source=" + source + "&company=" + companyId,
                    async: false,
                    dataType: "json",
                    success: function(data) {
                        if (data.tolist.length > 0) {
                            var html = [], mTp = '<%=ProviderMoneyStr %>';
                            html.push('<tr><th>景点名称</th><th>挂牌价格</th><th>散客价</th><th>团队价</th><th>儿童价</th><th>60-70老人价</th><th>70以上老人价 </th><th>学生价</th><th>军人价</th></tr>');
                            for (var i = 0; i < data.tolist.length; i++) {
                                $("select[name='spotList']").append("<option value='" + data.tolist[i].Id + "," + data.tolist[i].SpotName + "'>" + data.tolist[i].SpotName + "</option>");

                                if (data.tolist[i].userNum) {
                                    $("#<%=txtAdultNums.ClientID %>").val(data.tolist[i].userNum);
                                    $("#<%=hidUserNum.ClientID %>").val(data.tolist[i].userNum);
                                }

                                html.push('<tr><td>' + data.tolist[i].SpotName + '</td><td>' + mTp + data.tolist[i].PriceGP + '</td><td>' + mTp + data.tolist[i].PriceSK + '</td><td>' + mTp + data.tolist[i].PriceTD + '</td><td>' + mTp + data.tolist[i].PriceRT + '</td><td>' + mTp + data.tolist[i].PriceLR1 + '</td><td>' + mTp + data.tolist[i].PriceLR2 + '</td><td>' + mTp + data.tolist[i].PriceXS + '</td><td>' + mTp + data.tolist[i].PriceJR + '</td></tr>');
                            }
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
        $(function() {
            SpotPage._InitPage();
            parent.ConfigPage.SetWinHeight();

            $("#a_openInfoDiv").click(function() {
                parent.ConfigPage.ShowListDiv("");
                return false;
            })
        });
    </script>

</body>
</html>

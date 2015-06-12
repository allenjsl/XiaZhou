<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OperaterShoppingList.aspx.cs"
    Inherits="Web.OperaterCenter.OperaterShoppingList" %>

<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register TagName="SupplierControl" TagPrefix="uc6" Src="~/UserControl/SupplierControl.ascx" %>
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
    <form id="formShop" runat="server">
    <div class="jidiao-r">
        <div id="con_faq_9">
            <table width="100%" cellpadding="0" cellspacing="0" class="jd-table01 line-b">
                <tr>
                    <th width="15%" align="right" class="border-l">
                        <span class="fontred">*</span>购物店：
                    </th>
                    <td width="40%">
                        <uc6:SupplierControl ID="SupplierControl1" runat="server" IsMust="true" CallBack="ShopPage._AjaxContectInfo"
                            SupplierType="购物" Flag="0" />
                    </td>
                    <th width="15%" align="right">
                        联系人：
                    </th>
                    <td width="30%">
                        <asp:TextBox ID="txtContactName" runat="server" CssClass="inputtext formsize140"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th align="right" class="border-l">
                        联系电话：
                    </th>
                    <td>
                        <asp:TextBox ID="txtContactPhone" runat="server" CssClass="inputtext formsize140"></asp:TextBox>
                    </td>
                    <th align="right">
                        <span class="addtableT">联系传真：</span>
                    </th>
                    <td>
                        <asp:TextBox ID="txtContactFax" runat="server" CssClass="inputtext formsize140"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th align="right" class="border-l">
                        <span class="addtableT"><span class="fontred">*</span> 人头数：</span>
                    </th>
                    <td colspan="3">
                        <asp:TextBox ID="txtPeopleNums" runat="server" CssClass="inputtext formsize50" valid="required|RegInteger"
                            errmsg="*请输入人数!|*人数必须是正整数!"></asp:TextBox>人
                    </td>
                </tr>
                <tr>
                    <th align="right" class="border-l">
                        返利标准：
                    </th>
                    <td colspan="3">
                        <asp:TextBox ID="txtProfitStand" runat="server" CssClass="inputtext formsize600" TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th align="right" class="border-l">
                        导游需知：
                    </th>
                    <td colspan="3">
                        <asp:TextBox ID="txtGuidNotes" runat="server" CssClass="inputtext formsize600" 
                            TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th align="right" class="border-l">
                        其它备注：
                    </th>
                    <td colspan="3">
                        <asp:TextBox ID="txtOtherRemark" runat="server" CssClass="inputtext formsize600" TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th align="right" class="border-l">
                        <span class="fontred">*</span>状态：
                    </th>
                    <td colspan="3">
                        <select name="selStatus" class="inputselect">
                            <%=UtilsCommons.GetOperaterStatusList(status)%>
                        </select>
                    </td>
                </tr>
            </table>
            <asp:PlaceHolder ID="phdShowList" runat="server">
                <h2>
                    <p>
                        已安排购物</p>
                </h2>
                <table width="100%" id="tabShopList" border="0" cellspacing="0" cellpadding="0" class="jd-table01"
                    style="border-bottom: 1px solid #A9D7EC;">
                    <tr>
                        <th align="center" class="border-l">
                            购物店名称
                        </th>
                        <th align="center">
                            人头数
                        </th>
                        <th align="center">
                            返利标准
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
                    <asp:Repeater ID="repShoppingList" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td align="center" class="border-l">
                                    <%# Eval("SourceName")%>
                                </td>
                                <td align="center">
                                    <%# Eval("Num") %>
                                </td>
                                <td align="center">
                                    <span title="<%# Eval("ServiceStandard") %>">
                                        <%# EyouSoft.Common.Utils.GetText2(Eval("ServiceStandard").ToString(),30,true) %></span>
                                </td>
                                <td align="center">
                                    <%# Eval("Status").ToString()%>
                                </td>
                                <td align="center">
                                    <a href='<%#querenUrl %>?planId=<%# Eval("PlanId") %>' target="_blank">
                                        <img src="/images/y-kehuqueding.gif" /></a>
                                </td>
                                <td align="center">
                                    <%if (ListPower)
                                      { %>
                                    <a href="javascript:" data-class="updateShop">
                                        <img src="/images/y-delupdateicon.gif" data-id="<%# Eval("PlanId") %>" />
                                        修改</a> <a href="javascript:" data-class="deleteShop">
                                            <img src="/images/y-delicon.gif" data-id="<%# Eval("PlanId") %>" />
                                            删除</a>
                                    <%}
                                      else
                                      { %>
                                    <a href="javascript:" data-class="showShop">
                                        <img src="/images/y-delupdateicon.gif" data-id="<%# Eval("PlanId") %>" />
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
                        <li class="cun-cy"><a href="javascript:" id="btnSave">保存</a> </li>
                    </ul>
                </div>
            </asp:PlaceHolder>
        </div>
    </div>
    </form>

    <script type="text/javascript">
        var ShopPage = {
            sl: '<%=SL %>',
            type: '<%=Utils.GetQueryStringValue("Type") %>',
            tourId: '<%=Utils.GetQueryStringValue("tourId") %>',
            iframeId: '<%=Utils.GetQueryStringValue("iframeId") %>',
            _OpenBoxy: function(title, iframeUrl, width, height, draggable) {
                parent.Boxy.iframeDialog({ title: title, iframeUrl: iframeUrl, width: width, height: height, draggable: draggable });
            },
            _SaveShop: function(tourId, planId) {
                var _Url = '/OperaterCenter/OperaterShoppingList.aspx?type=' + ShopPage.type + '&sl=' + ShopPage.sl + '&tourId=' + ShopPage.tourId + "&iframeId=" + ShopPage.iframeId + "&m=" + new Date().getTime();
                $.newAjax({
                    type: "POST",
                    url: "/OperaterCenter/OperaterShoppingList.aspx?action=save&sl=" + ShopPage.sl + "&tourId=" + tourId + "&planId=" + planId,
                    cache: false,
                    data: $("#btnSave").closest("form").serialize(),
                    dataType: "json",
                    success: function(data) {
                        if (data.result == "1") {
                            parent.tableToolbar._showMsg(data.msg, function() {
                                window.location.href = _Url;
                            });
                        } else {
                            parent.tableToolbar._showMsg(data.msg, function() {
                                $("#btnSave").text("保存").css("background-position", "0 0px").bind("click");
                                $("#btnSave").click(function() {
                                    $(this).text("保存中...").css("background-position", "0 -62px").unbind("click");
                                    var _planID = '<%=Utils.GetQueryStringValue("planId") %>';
                                    ShopPage._SaveShop(ShopPage.tourId, _planID);
                                })

                            });
                        }
                    },
                    error: function() {
                        parent.tableToolbar._showMsg(tableToolbar.errorMsg);
                        return false;
                    }
                });
            },
            _DeleteShop: function(_ObjID) {
                var _url = '/OperaterCenter/OperaterShoppingList.aspx?type=' + ShopPage.type + '&sl=' + ShopPage.sl + '&tourId=' + ShopPage.tourId + "&iframeId=" + ShopPage.iframeId + "&m=" + new Date().getTime();
                $.newAjax({
                    type: "get",
                    url: '/OperaterCenter/OperaterDiningList.aspx?sl=' + ShopPage.sl + '&action=delete&planId=' + _ObjID + '&tourid=' + ShopPage.tourId,
                    cache: false,
                    dataType: "json",
                    success: function(data) {
                        if (data.result == "1") {
                            parent.tableToolbar._showMsg(data.msg, function() {
                                window.location.href = _url;
                            });
                            return false;
                        }
                        else {
                            parent.tableToolbar._showMsg(data.msg, function() {
                                window.location.href = _url;
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
            _AjaxContectInfo: function(obj) {
                if (obj) {
                    $("#<%=SupplierControl1.ClientText %>").val(obj.name);
                    $("#<%=SupplierControl1.ClientValue %>").val(obj.id);
                    $("#<%=txtContactName.ClientID %>").val(obj.contactname);
                    $("#<%=txtContactPhone.ClientID %>").val(obj.contacttel);
                    $("#<%=txtContactFax.ClientID %>").val(obj.contactfax);
                }
            },
            _BindBtn: function() {
                $("#btnSave").unbind("click").text("保存").css("background-position", "0 0px");
                $("#btnSave").click(function() {
                    var form = $("#<%=formShop.ClientID %>").get(0);
                    if (!ValiDatorForm.validator(form, "parent")) {
                        return;
                    } else {
                        $(this).text("保存中...").css("background-position", "0 -62px").unbind("click");
                        var planId = '<%=Utils.GetQueryStringValue("planId") %>';
                        ShopPage._SaveShop(ShopPage.tourId, planId);
                    }
                });

                $("#tabShopList").find("a[data-class='deleteShop']").unbind("click");
                $("#tabShopList").find("a[data-class='deleteShop']").click(function() {
                    var newThis = $(this);
                    parent.tableToolbar.ShowConfirmMsg("确定删除此条数据吗?", function() {
                        var planID = newThis.find("img").attr("data-ID");
                        if (planID) {
                            ShopPage._DeleteShop(planID);
                        }
                    });
                    return false;
                });

                $("#tabShopList").find("a[data-class='updateShop']").unbind("click");
                $("#tabShopList").find("a[data-class='updateShop']").click(function() {
                    var planID = $(this).find("img").attr("data-ID");
                    if (planID) {
                        window.location.href = '/OperaterCenter/OperaterShoppingList.aspx?type=' + ShopPage.type + '&sl=' + ShopPage.sl + '&tourId=' + ShopPage.tourId + '&action=update&planId=' + planID + "&iframeId=" + ShopPage.iframeId;
                    }
                    return false;
                });

                $("#tabShopList").find("a[data-class='showShop']").unbind("click");
                $("#tabShopList").find("a[data-class='showShop']").click(function() {
                    var planID = $(this).find("img").attr("data-ID");
                    if (planID) {
                        window.location.href = '/OperaterCenter/OperaterShoppingList.aspx?type=' + ShopPage.type + '&sl=' + ShopPage.sl + '&tourId=' + ShopPage.tourId + '&action=update&planId=' + planID + "&iframeId=" + ShopPage.iframeId + "&show=1";
                    }
                    return false;
                });

            },
            _PageInit: function() {
                this._BindBtn();
                if ('<%=Utils.GetQueryStringValue("show") %>' == "1") {
                    $("#ul_btn_list").parent().hide();
                }
            }
        }
        $(document).ready(function() {
            ShopPage._PageInit();
            parent.ConfigPage.SetWinHeight();
        });
    </script>

</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProductList.aspx.cs" Inherits="Web.GroupEnd.Suppliers.ProductList" %>

<%@ Register Src="~/UserControl/Suppliers.ascx" TagName="Suppliers" TagPrefix="uc1" %>
<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>
<%@ Register Src="~/UserControl/CaiWuShaiXuan.ascx" TagName="CaiWuShaiXuan" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta content="text/html; charset=utf-8" http-equiv="Content-Type" />
    <title>供应商产品投放</title>
    <link type="text/css" rel="stylesheet" href="/Css/fx_style.css" />
    <link type="text/css" rel="stylesheet" href="/Css/boxynew.css" />
</head>
<body style="background: 0 none;">
    <uc1:Suppliers runat="server" ID="Suppliers1" ProcductClass="default Producticon" />
    <div class="list-main">
        <div class="list-maincontent">
            <div class="linebox-menu">
                <a href='ProductOperating.aspx?sl=<%=EyouSoft.Common.Utils.GetQueryStringValue("sl") %>&_Type=DoAdd'>
                    新增计划</a> <a href="javascript:void(0)" id="_updatePlan">修改计划</a><a id="_deletePlan"
                        href="javascript:void(0)">删除计划</a><a id="_copyPlan" href="javascript:void(0)">复制计划</a></div>
            <div class="hr_10">
            </div>
            <div class="listsearch">
                <form id="frm" method="get" action="ProductList.aspx">
                <input type="hidden" name="sl" value="<%=EyouSoft.Common.Utils.GetQueryStringValue("sl") %>" />
                团号：<input type="text" style="width: 100px;" class="searchInput" name="txtTourCode"
                    value='<%=EyouSoft.Common.Utils.GetQueryStringValue("txtTourCode") %>' />
                线路名称：<input type="text" class="searchInput size130" name="txtRouteName" value='<%=EyouSoft.Common.Utils.GetQueryStringValue("txtRouteName") %>' />
                线路区域：
                <select id="ddlArea" name="ddlArea">
                    <%=EyouSoft.Common.UtilsCommons.GetSuppliersArea(EyouSoft.Common.Utils.GetQueryStringValue("ddlArea"),SiteUserInfo.SourceCompanyInfo.CompanyId) %>
                </select>
                出团时间：
                <input type="text" onfocus="WdatePicker()" class="searchInput size68" name="txtBeginLDate"
                    value='<%=EyouSoft.Common.Utils.GetQueryStringValue("txtBeginLDate") %>' />
                -
                <input type="text" onfocus="WdatePicker()" class="searchInput size68" name="txtEndLDate"
                    value='<%=EyouSoft.Common.Utils.GetQueryStringValue("txtEndLDate") %>' />
                实收人数：
                <uc2:CaiWuShaiXuan ID="CaiWuShaiXuan1" runat="server" />
                <a href="#" id="btnSub">
                    <img src="/Images/fx-images/searchbg.gif"></a>
                </form>
            </div>
            <div class="listtablebox">
                <table width="100%" cellspacing="0" cellpadding="0" border="0" id="liststyle">
                    <tbody>
                        <tr class="odd">
                            <th align="center" rowspan="2">
                                <input type="checkbox" id="cbAll" name="checkbox">
                            </th>
                            <th align="center" rowspan="2">
                                编号
                            </th>
                            <th align="center" rowspan="2">
                                团号
                            </th>
                            <th align="left" rowspan="2">
                                线路名称
                            </th>
                            <th align="center" rowspan="2">
                                出团时间
                            </th>
                            <th align="center" rowspan="2">
                                天数
                            </th>
                            <th align="center" colspan="2">
                                价格
                            </th>
                            <th align="center" colspan="4">
                                人数
                            </th>
                            <th align="center" rowspan="2">
                                订单
                            </th>
                            <th align="center" rowspan="2">
                                名单
                            </th>
                        </tr>
                        <tr>
                            <td bgcolor="#DCEFF3" align="center">
                                成人
                            </td>
                            <td bgcolor="#DCEFF3" align="center">
                                儿童
                            </td>
                            <td bgcolor="#DCEFF3" align="center">
                                预
                            </td>
                            <td bgcolor="#DCEFF3" align="center">
                                留
                            </td>
                            <td bgcolor="#DCEFF3" align="center">
                                实
                            </td>
                            <td bgcolor="#DCEFF3" align="center">
                                剩
                            </td>
                        </tr>
                        <asp:Repeater runat="server" ID="RpTour">
                            <ItemTemplate>
                                <tr class='<%#Container.ItemIndex%2==0?"odd":"" %>'>
                                    <td align="center" data-ischeck='<%#Eval("IsCheck") %>'>
                                        <input type="checkbox" id="cbItem" name="cbTourId" value='<%#Eval("TourId") %>'>
                                    </td>
                                    <td align="center">
                                        <%#Container.ItemIndex+1+(pageIndex-1)*pageSize %>
                                    </td>
                                    <td align="center">
                                        <%#Eval("TourCode")%>
                                    </td>
                                    <td align="left">
                                        <a class="lineInfo" href="#">
                                            <%#Eval("RouteName")%></a>
                                    </td>
                                    <td align="center">
                                        <%#EyouSoft.Common.UtilsCommons.GetDateString(Eval("LDate"), this.ProviderToDate)%>
                                    </td>
                                    <td align="center">
                                        <b>
                                            <%#Eval("TourDays")%></b>
                                    </td>
                                    <td align="right" class="fontblue">
                                        <%#EyouSoft.Common.UtilsCommons.GetMoneyString(Eval("AdultPrice"),this.ProviderToMoney) %>
                                    </td>
                                    <td align="right" class="font-orange">
                                        <%#EyouSoft.Common.UtilsCommons.GetMoneyString(Eval("ChildPrice"), this.ProviderToMoney)%>
                                    </td>
                                    <td align="center">
                                        <%#Eval("PlanPeopleNumber")%>
                                    </td>
                                    <td align="center">
                                        <a href='<%#GetPrintPage(Eval("TourId").ToString(),"1")%>' target="_blank">
                                            <%#Eval("LeavePeopleNumber")%></a>
                                    </td>
                                    <td align="center">
                                        <a href='<%#GetPrintPage(Eval("TourId").ToString(),"2")%>' target="_blank">
                                            <%#Eval("Adults")%><sup class="fontred">+<%#Eval("Childs")%></sup></a>
                                    </td>
                                    <td align="center">
                                        <%#Eval("PeopleNumberLast")%>
                                    </td>
                                    <td align="center">
                                        <a href="<%#EyouSoft.Common.Utils.GetInt(Eval("OrderCount").ToString()) == 0 ? "#" : "OrderCenter.aspx?sl="+EyouSoft.Common.Utils.GetQueryStringValue("sl")+"&TourId=" + Eval("TourId")%>">
                                            <%#Eval("OrderCount")%></a>
                                    </td>
                                    <td align="center">
                                        <a target="_blank" href="<%#GetPrintPage(Eval("TourId").ToString()) %>">名单</a>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                        <asp:PlaceHolder runat="server" ID="PhPage">
                            <tr class="odd">
                                <td bgcolor="#f4f4f4" align="center" colspan="18">
                                    <div class="pages">
                                        <cc1:ExporPageInfoSelect runat="server" ID="ExporPageInfoSelect1" />
                                    </div>
                                </td>
                            </tr>
                        </asp:PlaceHolder>
                        <asp:Literal ID="litMsg" Visible="false" runat="server" Text="<tr><td align='center' colspan='18'>暂无计划!</td></tr>"></asp:Literal>
                    </tbody>
                </table>
            </div>
            <div class="hr_10">
            </div>
        </div>
    </div>
    <!-- InstanceEndEditable -->
    <!-- InstanceEndEditable -->

    <script src="/Js/jquery-1.4.4.js" type="text/javascript"></script>

    <!--[if IE]><script src="/js/excanvas.js" type="text/javascript" charset="utf-8"></script><![endif]-->

    <script src="/Js/bt.min.js" type="text/javascript"></script>

    <script src="/Js/jquery.boxy.js" type="text/javascript"></script>

    <!--[if lte IE 6]><script src="/js/jquery.bgiframe.min.js" type="text/javascript"></script><![endif]-->

    <script src="/Js/jquery.blockUI.js" type="text/javascript"></script>

    <script src="/Js/table-toolbar.js" type="text/javascript"></script>

    <script src="/Js/datepicker/WdatePicker.js" type="text/javascript"></script>

    <script type="text/javascript">
        $(function() {
            var caiWuShaiXuan = new wuc.caiWuShaiXuan(window['<%=CaiWuShaiXuan1.ClientID%>']);
            caiWuShaiXuan.setOperator('<%=EyouSoft.Common.Utils.GetQueryStringValue(CaiWuShaiXuan1.ClientUniqueIDOperator) %>');
            caiWuShaiXuan.setOperatorNumber('<%=EyouSoft.Common.Utils.GetQueryStringValue(CaiWuShaiXuan1.ClientUniqueIDOperatorNumber) %>');

            tableToolbar.init({
                otherButtons: [{
                    button_selector: '#_updatePlan',
                    sucessRulr: 1,
                    msg: '未选中任何 计划 ',
                    msg2: '只能选中一个 计划 ',
                    buttonCallBack: function(arr) {
                        //                        var ischeck = arr[0].find(":checkbox").closest("td").attr("data-ischeck");
                        //                        if (ischeck == "False") {
                        window.location.href = '/GroupEnd/Suppliers/ProductOperating.aspx?sl=<%=Request.QueryString["sl"] %>&_Type=DoUpdate&TourId=' + arr[0].find(":checkbox").val();
                        //                        }
                        //                        else {
                        //                            tableToolbar._showMsg("线路已审核不允许修改！");
                        //                        }
                        return false;
                    }
                }, {
                    button_selector: '#_deletePlan',
                    sucessRulr: 2,
                    msg: '未选中任何 计划 ',
                    buttonCallBack: function(arr) {

                        //DELETE
                        var list = new Array();
                        //遍历按钮返回数组对象
                        for (var i = 0; i < arr.length; i++) {
                            //从数组对象中找到数据所在，并保存到数组对象中
                            if (arr[i].find("input[type='checkbox']").val() != "on") {
                                list.push(arr[i].find("input[type='checkbox']").val());
                            }
                        }
                        var ids = list.join('|');
                        var ischeck = arr[0].find(":checkbox").closest("td").attr("data-ischeck");
                        if (ischeck == "False") {
                            tableToolbar.ShowConfirmMsg("确定删除选中的线路？删除后不可恢复！", function() {
                                $.newAjax({
                                    url: '/GroupEnd/Suppliers/ProductList.aspx?sl=<%=Request.QueryString["sl"] %>&Type=Delete',
                                    type: "post",
                                    data: $.param({ id: ids }),
                                    dataType: "json",
                                    success: function(data) {
                                        tableToolbar._showMsg(data.msg, function() {
                                            window.location.href = '/GroupEnd/Suppliers/ProductList.aspx?sl=<%=Request.QueryString["sl"] %>';
                                        });

                                    },
                                    error: function() {
                                        tableToolbar._showMsg("服务器忙！");
                                    }
                                });

                            });
                        } else {
                            tableToolbar._showMsg("计划已审核不允许删除！");
                        }

                        return false;
                    }
                }, {
                    button_selector: '#_copyPlan',
                    sucessRulr: 1,
                    msg: '未选中任何 计划 ',
                    msg2: '只能选中一个 计划 ',
                    buttonCallBack: function(arr) {
                        //COPY
                        window.location.href = '/GroupEnd/Suppliers/ProductOperating.aspx?sl=<%=Request.QueryString["sl"] %>&_Type=DoCopy&TourId=' + arr[0].find(":checkbox").val();
                        return false;
                    }
}]
                });
                //提交表单
                $("#btnSub").click(function() {
                    $("#frm").submit();
                });


                //Enter搜索
                $("#frm").find(":text").keypress(function(e) {
                    if (e.keyCode == 13) {
                        $("#frm").submit();
                        return false;
                    }
                });

            });
    </script>

</body>
</html>

<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Front.Master" AutoEventWireup="true"
    CodeBehind="Traveller.aspx.cs" Inherits="EyouSoft.Web.TongJi.Traveller" %>

<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script src="/Js/datepicker/WdatePicker.js" type="text/javascript"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="mainbox">
        <div class="tablehead" style="background: none #f6f6f6;">
            <ul class="fixed">
                <li><s class="orderformicon"></s><a id="ttpMenu0" href="/TongJi/Traveller.aspx?sl=<%= EyouSoft.Common.Utils.GetQueryStringValue("sl") %>&ttp=0"
                    hidefocus="true" class="ztorderform"><span>组团游客统计表</span></a></li>
                <li><s class="orderformicon"></s><a id="ttpMenu1" href="/TongJi/Traveller.aspx?sl=<%= EyouSoft.Common.Utils.GetQueryStringValue("sl") %>&ttp=1"
                    hidefocus="true" class="ztorderform"><span>地接游客统计表</span></a></li>
                <li><s class="orderformicon"></s><a id="ttpMenu2" href="/TongJi/Traveller.aspx?sl=<%= EyouSoft.Common.Utils.GetQueryStringValue("sl") %>&ttp=2"
                    hidefocus="true" class="ztorderform"><span>出境游客统计表</span></a></li>
            </ul>
        </div>
        <form id="form1" method="get">
        <input type="hidden" size="8" name="sl" value="<%=EyouSoft.Common.Utils.GetQueryStringValue("sl") %>" />
        <input type="hidden" size="8" name="ttp" value="<%=EyouSoft.Common.Utils.GetQueryStringValue("ttp") %>" />
        <div class="searchbox fixed">
            <span class="searchT">
                <p>
                    出团时间：
                    <input type="text" onfocus="WdatePicker()" class="inputtext formsize100" id="txtLeaveDateS"
                        name="LeaveDateS" value="<%=EyouSoft.Common.Utils.GetQueryStringValue("LeaveDateS") %>" />
                    -
                    <input type="text" onfocus="WdatePicker()" class="inputtext formsize100" id="txtLeaveDateE"
                        name="LeaveDateE" value="<%=EyouSoft.Common.Utils.GetQueryStringValue("LeaveDateE") %>" />
                    分公司：
                    <select class="inputselect" name="SunCompan" id="SunCompan">
                        <asp:Literal ID="ltrSunCompany" runat="server"></asp:Literal>
                    </select>
                    <input type="submit" class="search-btn" />
                </p>
            </span>
        </div>
        </form>
        <div class="tablehead" id="div_Traveller_toolbar1">
            <ul class="fixed">
                <li><s class="dayin"></s><a href="javascript:void(0);" onclick="PrintPage('liststyle'); return false;"
                    hidefocus="true" class="toolbar_dayin"><span>打印</span></a></li>
                <li class="line"></li>
                <li><s class="daochu"></s><a href="javascript:void(0);" onclick="toXls1(); return false;" hidefocus="true"
                    class="toolbar_daochu"><span>导出</span></a></li>
            </ul>
            <div class="pages">
                <cc1:ExporPageInfoSelect ID="ExporPageInfoSelect1" runat="server" />
            </div>
        </div>
        <!--列表表格-->
        <div class="tablelist-box">
            <table width="100%" align="center" id="liststyle">
                <tr>
                    <th class="thinputbg" style="width: 35px">
                        <input type="checkbox" name="ckbAll" id="ckbAll" />
                    </th>
                    <th align="center" class="th-line">
                        人数（订单人数合计）
                    </th>
                    <th align="center" class="th-line">
                        人天数（订单人数*计划天数合计）
                    </th>
                    <th align="center">
                        <span class="th-line">客源地</span>
                    </th>
                    <th align="center" style="width: 35px">
                        &nbsp;
                    </th>
                    <th align="center" class="th-line">
                        人数（订单人数合计）
                    </th>
                    <th align="center" class="th-line">
                        人天数（订单人数*计划天数合计）
                    </th>
                    <th align="center" class="th-line">
                        客源地
                    </th>
                </tr>
                <tr>
                    <asp:Repeater runat="server" ID="rptTraveller">
                        <ItemTemplate>
                            <td align="left">
                                <input type="checkbox" name="ckbXH" id="ckbXH_<%# Container.ItemIndex + 1 %>" />
                                <%# GetXh(Container.ItemIndex + 1) %>
                            </td>
                            <td align="center">
                                <%# Eval("PeopleNum")%>
                            </td>
                            <td align="center">
                                <%# Eval("PeopleDayNum")%>
                            </td>
                            <td align="center">
                                <%# Eval("Place")%>
                                <%# EyouSoft.Common.Utils.IsOutTrOrTd(Container.ItemIndex, RecordCount, 2, 4)%>
                        </ItemTemplate>
                    </asp:Repeater>
            </table>
        </div>
        <!--列表结束-->
        <div class="tablehead" style="border: 0 none;" id="div_Traveller_toolbar2">
        </div>
    </div>

    <script type="text/javascript">
        $(function() {
            tableToolbar.init({
                objectName: "游客分析表"
            });

            //初始化选择的分公司
            //$("#SunCompan").val('<%= EyouSoft.Common.Utils.GetQueryStringValue("SunCompan") %>');
            //初始化底部的操作栏
            $("#div_Traveller_toolbar2").html($("#div_Traveller_toolbar1").clone(true));

            var ttp = "<%= TravellerType %>";
            if (ttp == "1")
                $("#ttpMenu1").attr("class", "ztorderform de-ztorderform");
            else if (ttp == "2")
                $("#ttpMenu2").attr("class", "ztorderform de-ztorderform");
            else
                $("#ttpMenu0").attr("class", "ztorderform de-ztorderform");
        });
    </script>

</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Front.Master" AutoEventWireup="true"
    CodeBehind="QueryState.aspx.cs" Inherits="EyouSoft.Web.TongJi.QueryState" %>

<%@ Import Namespace="EyouSoft.Model.EnumType.ComStructure" %>
<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>
<%@ Register Src="/UserControl/SellsSelect.ascx" TagName="SellsSelect" TagPrefix="uc1" %>
<%@ Register Src="/UserControl/selectGuid.ascx" TagName="GuidsSelect" TagPrefix="uc1" %>
<%@ Register Src="/UserControl/DuoXuanTourStatus.ascx" TagName="DuoXuanTourStatus"
    TagPrefix="uc1" %>
<%@ Register Src="~/UserControl/SelectSection.ascx" TagName="SelectSection" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script src="/Js/datepicker/WdatePicker.js" type="text/javascript"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="mainbox">        
        <form id="form1" method="get">
        <input type="hidden" size="8" name="sl" value="<%=SL %>" />
        <input type="hidden" size="8" name="iso" value="<%=EyouSoft.Common.Utils.GetQueryStringValue("iso") %>" />
        <div class="searchbox fixed">
            <span class="searchT">
                <p>
                    出团时间：
                    <input type="text" onfocus="WdatePicker()" class="inputtext formsize100" id="txtLeaveDateS"
                        name="LeaveDateS" value="<%=EyouSoft.Common.Utils.GetQueryStringValue("LeaveDateS") %>" />
                    -
                    <input type="text" onfocus="WdatePicker()" class="inputtext formsize100" id="txtLeaveDateE"
                        name="LeaveDateE" value="<%=EyouSoft.Common.Utils.GetQueryStringValue("LeaveDateE") %>" />
                    团号：
                    <input name="TourNo" type="text" class="inputtext formsize100" id="txtTourNo" value="<%=EyouSoft.Common.Utils.GetQueryStringValue("TourNo") %>" />
                    销售员部门：<uc1:SelectSection ID="txtTourSellerDept" runat="server" ReadOnly="true" SetTitle="部门"
                        SModel="2" />
                    销售员：
                    <uc1:SellsSelect ID="SellsSelect1" runat="server" SetTitle="选择销售员" SelectFrist="false" />                    
                    导游：
                    <uc1:GuidsSelect ID="GuidsSelect1" runat="server" SetTitle="选择导游" />                   
                    <br />
                    回团时间：
                    <input type="text" onfocus="WdatePicker()" class="inputtext formsize100" id="txtBackDateS"
                        name="BackDateS" value="<%=EyouSoft.Common.Utils.GetQueryStringValue("BackDateS") %>" />
                    -
                    <input type="text" onfocus="WdatePicker()" class="inputtext formsize100" id="txtBackDateE"
                        name="BackDateE" value="<%=EyouSoft.Common.Utils.GetQueryStringValue("BackDateE") %>" />
                    团队状态：<uc1:DuoXuanTourStatus runat="server" id="txtTourStatus"></uc1:DuoXuanTourStatus>
                    线路名称：<input type="text" class="inputtext formsize120" name="txtRouteName" value="<%=EyouSoft.Common.Utils.GetQueryStringValue("txtRouteName") %>" />
                    计调员：
                    <uc1:SellsSelect ID="txtJiDiaoYuan" runat="server" SetTitle="选择计调员" SelectFrist="false" />
                    <input type="submit" class="search-btn" /></p>
            </span>
        </div>
        </form>
        <div class="tablehead" id="div_State_toolbar1">
            <ul class="fixed">
                <li><s class="dayin"></s><a href="javascript:void(0);" onclick="PrintPage('liststyle'); return false;"
                    hidefocus="true" class="toolbar_dayin"><span>打印列表</span></a></li>
                <li class="line"></li>
                <li><s class="daochu"></s><a href="javascript:void(0);" onclick="toXls1(); return false;" hidefocus="true"
                    class="toolbar_daochu"><span>导出列表</span></a></li>
                <asp:PlaceHolder runat="server" ID="phSetTourStatus">
                <li><s class="updateicon"></s><a href="javascript:void(0);" hidefocus="true" id="i_a_settourstatus">
                    <span>状态变更</span></a></li>
                </asp:PlaceHolder>
            </ul>
            <div class="pages">
                <cc1:ExporPageInfoSelect ID="ExporPageInfoSelect1" runat="server" />
            </div>
        </div>
        <!--列表表格-->
        <div class="tablelist-box">
            <table width="100%" id="liststyle">
                <tr>
                    <th width="30" class="thinputbg">
                        <input type="checkbox" name="checkbox" id="checkbox" />
                    </th>
                    <th align="center" class="th-line">
                        团号
                    </th>
                    <th align="center" class="th-line">
                        线路名称
                    </th>
                    <th align="center" class="th-line">
                        出团时间
                    </th>
                    <th align="center" class="th-line">
                        回团时间
                    </th>
                    <th align="center" class="th-line">
                        人数
                    </th>
                    <th align="center" class="th-line">
                        销售员
                    </th>
                    <th align="center" class="th-line">
                        计调员
                    </th>
                    <th align="center" class="th-line">
                        导游
                    </th>
                    <th align="center" class="th-line">
                        状态
                    </th>
                </tr>
                <asp:Repeater runat="server" ID="rptTourList">
                    <ItemTemplate>
                        <tr i_tourid="<%#Eval("TourId") %>">
                            <td align="center">
                                <input type="checkbox" name="checkbox" id="checkbox" value="<%#Eval("TourId") %>" />
                            </td>
                            <td align="center">
                                <%# Eval("TourCode")%>
                            </td>
                            <td align="left">
                                <a target="_blank" href="<%#GetRouteNameHref(Eval("TourId"),Eval("TourType")) %>">
                                    <%# Eval("RouteName")%></a>
                            </td>
                            <td align="center">
                                <%# EyouSoft.Common.UtilsCommons.GetDateString(Eval("LDate"), ProviderToDate)%>
                            </td>
                            <td align="center">
                                <%# EyouSoft.Common.UtilsCommons.GetDateString(Eval("RDate"), ProviderToDate)%>
                            </td>
                            <td align="center">
                                <%# Eval("PersonNum")%>
                            </td>
                            <td align="center">
                                <%# Eval("SellerName")%>
                            </td>
                            <td align="center">
                                <%# this.GetPlanerName(Eval("Planer"))%>
                            </td>
                            <td align="center">
                                <a href='<%# new EyouSoft.BLL.ComStructure.BComSetting().GetPrintUri(SiteUserInfo.CompanyId, PrintTemplateType.导游任务单) + "?tourid=" + Eval("TourId") %>' target="_blank">
                                    <%# this.GetGuiderName(Eval("Guide"))%></a>
                            </td>
                            <td align="center">
                                <a href="javascript:void(0)" class="i_tourstatusxx"><%# GetTourStatus(Eval("TourType"),Eval("TourStatus"))%></a>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
                <asp:PlaceHolder ID="phHeJi" runat="server">
                    <tr class="even">
                        <td height="30" colspan="5" align="right">
                            合计：
                        </td>
                        <td align="center">
                            <asp:Literal runat="server" ID="ltrRenShuHeJi"></asp:Literal>
                        </td>
                        <td align="center" colspan="4">
                            &nbsp;
                        </td>
                    </tr>
                </asp:PlaceHolder>
            </table>
        </div>
        <!--列表结束-->
        <div class="tablehead" style="border: 0 none;" id="div_State_toolbar2">
        </div>
    </div>

    <script type="text/javascript">
        var iPage = {
            initToolbar: function() {
                var _options = { objectName: "状态查询", otherButtons: [] }
                var _btnSetTourStatus = { button_selector: '#i_a_settourstatus'
                    , sucessRulr: 1
                    , msg: '状态变更只能选择一个计划信息'
                    , buttonCallBack: function(objs) {
                        var _params = { sl: '<%=SL %>', tourid: '' };
                        $(objs).each(function() {
                            _params.tourid = $(this).find(":checkbox").val();
                        });
                        Boxy.iframeDialog({ iframeUrl: Boxy.createUri("SetTourStatus.aspx", _params)
                            , title: "计划状态变更"
                            , modal: true
                            , width: "540px"
                            , height: "270px"
                        });
                        return false;
                    }
                };
                _options.otherButtons.push(_btnSetTourStatus);
                tableToolbar.init(_options);
            },
            openTourStatusXX: function(obj) {
                var _$tr = $(obj).closest("tr");
                var _data = { tourid: _$tr.attr("i_tourid") };

                Boxy.iframeDialog({ iframeUrl: Boxy.createUri("TourStatusXX.aspx", _data) , title: "状态明细", modal: true, width: "540px" , height: "270px"});
            }
        };

        $(function() {
            iPage.initToolbar();
            //初始化底部的操作栏
            $("#div_State_toolbar2").html($("#div_State_toolbar1").clone(true));

            window["<%=txtTourStatus.ClientID %>"].setValue({ value: "<%=EyouSoft.Common.Utils.GetQueryStringValue(txtTourStatus.ValueClientID) %>", text: "<%=EyouSoft.Common.Utils.GetQueryStringValue(txtTourStatus.TextClientID) %>" });

            $(".i_tourstatusxx").click(function() { iPage.openTourStatusXX(this); });
        });
    </script>

</asp:Content>

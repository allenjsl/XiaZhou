<%@ Page Title="派团计划" Language="C#" MasterPageFile="~/MasterPage/Front.Master" AutoEventWireup="true"
    CodeBehind="TeamplanList.aspx.cs" Inherits="Web.TeamCenter.TeamplanList" %>

<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>
<%@ Register Src="../UserControl/SellsSelect.ascx" TagName="SellsSelect" TagPrefix="uc2" %>
<%@ Register Src="../UserControl/CustomerUnitSelect.ascx" TagName="CustomerUnitSelect"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script src="/Js/datepicker/WdatePicker.js" type="text/javascript"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="mainbox">
        <div class="searchbox fixed">
            <form method="get">
            <input type="hidden" name="type" value="<%=EyouSoft.Common.Utils.GetQueryStringValue("type") %>" />
            <input type="hidden" name="sl" value="<%=EyouSoft.Common.Utils.GetQueryStringValue("sl") %>" />
            <span class="searchT">
                <p>
                    线路区域：<select name="ddlArea" class="inputselect" style="width: 120px;">
                        <%=EyouSoft.Common.UtilsCommons.GetAreaLineForSelect(EyouSoft.Common.Utils.GetInt(EyouSoft.Common.Utils.GetQueryStringValue("ddlArea")),SiteUserInfo.CompanyId) %>
                    </select>
                    出发时间：<input onfocus="WdatePicker()" type="text" class="inputtext formsize100" name="txtBeginDateF"
                        value="<%=EyouSoft.Common.Utils.GetQueryStringValue("txtBeginDateF") %>" />
                    -
                    <input onfocus="WdatePicker()" type="text" class="inputtext formsize100" name="txtBeginDateS"
                        value="<%=EyouSoft.Common.Utils.GetQueryStringValue("txtBeginDateS") %>" />
                    销售员:
                    <uc2:SellsSelect ID="SellsSelect1" runat="server" SetTitle="销售员" SelectFrist="false" />
                    线路名称：
                    <input type="text" class="inputtext formsize120" name="txtRouteName" size="28" value="<%=EyouSoft.Common.Utils.GetQueryStringValue("txtRouteName") %>" />
                    <br />
                    团队状态：<select name="sltTourState" class="inputselect" style="width: 120px;">
                        <option value="">-请选择-</option>
                        <%=EyouSoft.Common.UtilsCommons.GetEnumDDL(EyouSoft.Common.EnumObj.GetList(typeof(EyouSoft.Model.EnumType.TourStructure.TourStatus)), EyouSoft.Common.Utils.GetQueryStringValue("sltTourState")) %>
                    </select>
                    回团时间：<input onfocus="WdatePicker()" type="text" class="inputtext formsize100" name="txtEndDateF"
                        value="<%=EyouSoft.Common.Utils.GetQueryStringValue("txtEndDateF") %>" />
                    -
                    <input onfocus="WdatePicker()" type="text" class="inputtext formsize100" name="txtEndDateS"
                        value="<%=EyouSoft.Common.Utils.GetQueryStringValue("txtEndDateS") %>" />
                    计调员:
                    <uc2:SellsSelect ID="SellsSelect2" runat="server" SetTitle="计调员" SelectFrist="false" />
                    客户单位：<uc1:CustomerUnitSelect ID="CustomerUnitSelect1" runat="server" BoxyTitle="选择客户单位"
                        SelectFrist="false" />
                    <br />                    
                    团号：<input type="text" class="inputtext formsize100" name="txtTourCode" value="<%=EyouSoft.Common.Utils.GetQueryStringValue("txtTourCode") %>" />    
                    发布人:
                    <uc2:SellsSelect ID="txtFaBuRen" runat="server" SetTitle="发布人" SelectFrist="false" />
                    <button class="search-btn" type="submit">
                        搜索</button></p>
            </span>
            </form>
        </div>
        <div class="tablehead">
            <ul class="fixed" id="btnAction">
                <asp:PlaceHolder ID="phForAdd" runat="server">
                    <li><s class="addicon"></s><a class="toolbar_add" hidefocus="true" href="AddTeamPlan.aspx?type=<%=type %>&sl=<%=sl %>&act=add">
                        <span>新增</span></a></li><li class="line"></li>
                </asp:PlaceHolder>
                <asp:PlaceHolder ID="phForUpdate" runat="server">
                    <li><s class="updateicon"></s><a class="toolbar_update" hidefocus="true" href="javascript:void(0);">
                        <span>修改</span></a></li><li class="line"></li>
                </asp:PlaceHolder>
                <asp:PlaceHolder ID="phForCopy" runat="server">
                    <li><s class="copyicon"></s><a class="toolbar_copy" hidefocus="true" href="javascript:void(0);">
                        <span>复制</span></a></li><li class="line"></li>
                </asp:PlaceHolder>
                <asp:PlaceHolder ID="phForCanel" runat="server">
                    <li><s class="cancelicon"></s><a class="toolbar_cancel" hidefocus="true" href="javascript:void(0);">
                        <span>取消</span></a></li><li class="line"></li>
                </asp:PlaceHolder>
                <asp:PlaceHolder ID="phForDelete" runat="server">
                    <li><a class="toolbar_delete" hidefocus="true" href="javascript:void(0);"><s class="delicon">
                    </s><span>删除</span></a></li></asp:PlaceHolder>
                <asp:PlaceHolder ID="phForOper" runat="server">
                    <li><s class="ptjdicon"></s><a class="toolbar_paiduan" hidefocus="true" href="javascript:void(0);">
                        <span>派团给计调</span></a></li><li class="line"></li>
                </asp:PlaceHolder>
                <li><s class="shoukuan"></s><a class="toolbar_shenqing" hidefocus="true" href="javascript:void(0);">
                    <span>超限申请</span></a></li><li class="line"></li>
            </ul>
            <div class="pages">
                <cc1:ExporPageInfoSelect ID="ExporPageInfoSelect2" runat="server" />
            </div>
        </div>
        <!--列表表格-->
        <div class="tablelist-box">
            <table width="100%" id="liststyle">
                <tbody>
                    <tr class="odd">
                        <th class="thinputbg">
                            <input name="checkbox" id="checkbox1" type="checkbox">
                        </th>
                        <th class="th-line" align="center">
                            团号
                        </th>
                        <th class="th-line" align="left">
                            线路名称
                        </th>
                        <th class="th-line" align="center">
                            出团时间
                        </th>
                        <th class="th-line" align="center">
                            天数
                        </th>
                        <%--<th class="th-line" align="center">
                            价格
                        </th>--%>
                        <th class="th-line" align="center">
                            人数
                        </th>
                        <th class="th-line" align="center">
                            客户单位
                        </th>
                        <th class="th-line" align="center">
                            销售员
                        </th>
                        <th class="th-line" align="center">
                            计调员
                        </th>
                        <th class="th-line" align="center">
                            发布人
                        </th>
                        <th class="th-line" align="center">
                            订单
                        </th>
                        <%if (EyouSoft.Common.Utils.GetQueryStringValue("type") == "3")
                          { %>
                        <th align="center" class="th-line">
                            签证资料
                        </th>
                        <%} %>
                        <th class="th-line" align="center">
                            查看计调
                        </th>
                        <th class="th-line" align="center">
                            状态
                        </th>
                    </tr>
                    <asp:Repeater runat="server" ID="rpt_List">
                        <ItemTemplate>
                            <tr <%#Container.ItemIndex%2==0?" class=\"odd\" ":""  %>>
                                <td align="center">
                                    <input name="checkbox" id="checkbox" value="<%#Eval("tourId") %>" type="checkbox">
                                </td>
                                <td align="center">
                                    <%#Eval("tourCode") %>
                                    <%#GetChangeInfo((bool)Eval("IsChange"), (bool)Eval("IsSure"), Eval("tourId").ToString(), Eval("TourStatus").ToString())%>
                                </td>
                                <td align="left" data-class="right">
                                    <a href='<%#PrintPageZt %>?tourid=<%#Eval("TourId") %>' target="_blank">
                                        <%#Eval("RouteName")%></a>
                                </td>
                                <td align="center">
                                    <%#EyouSoft.Common.UtilsCommons.GetDateString(Eval("LDate"),this.ProviderToDate )%>
                                </td>
                                <td align="center">
                                    <%#Eval("TourDays") %>
                                </td>
                                <%--<td align="right">
                                    <b class="fontblue">
                                        <%#EyouSoft.Common.UtilsCommons.GetMoneyString(Eval("AdultPrice"), this.ProviderToMoney)%>
                                    </b>
                                </td>--%>
                                <td align="center">
                                    <b><a href='<%#GetPrintUrl(Eval("OrderId"))%>'>
                                        <%#Eval("Adults") %></a><sup class="fontred">+<%#Eval("Childs") %></sup></b>
                                </td>
                                <td align="center">
                                    <a href="javascript:void(0);" data-class='paopao'>
                                        <%#Eval("CompanyInfo") ==null ?"": ((EyouSoft.Model.TourStructure.MCompanyInfo)Eval("CompanyInfo")).CompanyName%></a>
                                    <span style="display: none"><b>
                                        <%#Eval("CompanyInfo") ==null ?"": ((EyouSoft.Model.TourStructure.MCompanyInfo)Eval("CompanyInfo")).CompanyName%></b><br />
                                        联系人：<%#Eval("CompanyInfo") ==null ?"": ((EyouSoft.Model.TourStructure.MCompanyInfo)Eval("CompanyInfo")).Contact%><br />
                                        联系方式：<%#Eval("CompanyInfo") ==null ?"": ((EyouSoft.Model.TourStructure.MCompanyInfo)Eval("CompanyInfo")).Phone%></span>
                                    </span>
                                </td>
                                <td align="center">
                                    <%# Eval("SaleInfo")==null ?"": ((EyouSoft.Model.TourStructure.MSaleInfo)Eval("SaleInfo")).Name%>
                                    <input type="hidden" name="ItemUserID" value="<%# Eval("SaleInfo")==null ?"": ((EyouSoft.Model.TourStructure.MSaleInfo)Eval("SaleInfo")).SellerId%>" />
                                </td>
                                <td align="center">
                                    <%#Eval("TourPlaner")==null?"": (((System.Collections.Generic.IList<EyouSoft.Model.TourStructure.MTourPlaner>)Eval("TourPlaner")).Count>0 ? ((System.Collections.Generic.IList<EyouSoft.Model.TourStructure.MTourPlaner>)Eval("TourPlaner"))[0].Planer:"")%>
                                </td>
                                <td align="center">
                                    <%#Eval("OperatorInfo.Name")%>
                                </td>
                                <td align="center">
                                    <a href="/SellCenter/CustomerPlan/OrderInfo.aspx?type=<%=Request.QueryString["type"] %>&OrderId=<%#Eval("OrderId") %>&sl=<%=Request.QueryString["sl"] %>&url=<%=Server.UrlEncode(Request.Url.ToString()) %>"
                                        title="查看" class="check-btn orderInfo" title="查看"></a>
                                </td>
                                <%if (EyouSoft.Common.Utils.GetQueryStringValue("type") == "3")
                                  { %>
                                <td align="center">
                                    <a href="javascript:void(0);" data-id="<%#Eval("TourId") %>" data-class="visaFiles">
                                        查看资料</a>
                                </td>
                                <%} %>
                                <td align="center" data-class="GetJiDiaoIcon" data-tourid="<%#Eval("tourId") %>">
                                    <%#EyouSoft.Common.UtilsCommons.GetJiDiaoIcon((EyouSoft.Model.TourStructure.MTourPlanStatus)Eval("TourPlanStatus"))%>
                                </td>
                                <td align="center">
                                    <input type="hidden" name="hideTourStatus" value="<%#(int)Eval("TourStatus")%>" />
                                    <input type="hidden" name="hideIsPayMoney" value="<%#Eval("IsPayMoney").ToString().ToLower()%>" />
                                    <%#Eval("TourStatus").ToString() == "已取消" ? "<a data-class='cancelReason'><span class='fontgray' data-class='QuoteState' data-state='0'>已取消</span></a><div style='display: none'><b>取消原因</b>:" +EyouSoft.Common.Function.StringValidate.TextToHtml( Eval("CancelReson").ToString()) + "</div>" : Eval("TourStatus").ToString()%>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                    <asp:Literal ID="litMsg" runat="server" Text="<tr><td align='center' colspan='14'>暂无计划!</td></tr>"></asp:Literal>
                </tbody>
            </table>
        </div>
        <!--列表结束-->
        <div style="border: 0 none;" class="tablehead">
            <ul class="fixed">

                <script type="text/javascript">
                    document.write(document.getElementById("btnAction").innerHTML);
                </script>

            </ul>
            <div class="pages">
                <cc1:ExporPageInfoSelect ID="ExporPageInfoSelect1" runat="server" />
            </div>
        </div>
        <div class="alertbox-outbox03" id="div_Canel" style="display: none; padding-bottom: 0px;">
            <div class="hr_10">
            </div>
            <table width="600px" cellspacing="0" cellpadding="0" bgcolor="#e9f4f9" align="center"
                style="margin: 0 auto">
                <tbody>
                    <tr>
                        <td width="80" height="28" align="right" class="alertboxTableT">
                            取消原因：
                        </td>
                        <td height="28" bgcolor="#E9F4F9" align="left">
                            <textarea style="height: 93px;" class="inputtext formsize450" id="txtCanelRemark"></textarea>
                        </td>
                    </tr>
                </tbody>
            </table>
            <div class="alertbox-btn" style="position: static">
                <a href="javascript:void(0);" id="btnCanelSave"><s class="baochun"></s>保 存</a><a
                    href="javascript:void(0);" onclick="javascript:TeamplanList.CanelBox.hide();return false;"><s
                        class="chongzhi"></s>关闭</a>
            </div>
        </div>
    </div>

    <script type="text/javascript">
        var TeamplanList = {
            Data: { sl: '<%=Request.QueryString["sl"] %>',
                type: '<%=Request.QueryString["type"] %>'
            },
            CanelBox: null,
            DelAll: function(objArr) {
                var list = new Array();
                //遍历按钮返回数组对象
                for (var i = 0; i < objArr.length; i++) {
                    //从数组对象中找到数据所在，并保存到数组对象中
                    if (objArr[i].find("input[type='checkbox']").val() != "on") {
                        list.push(objArr[i].find("input[type='checkbox']").val());
                    }
                }
                //执行
                $.newAjax({
                    type: "get",
                    cache: false,
                    url: "/TeamCenter/TeamplanList.aspx?dotype=delete&ids=" + list.join(',') + "&" + $.param(TeamplanList.Data),
                    dataType: "json",
                    success: function(ret) {
                        if (ret.result == "1") {
                            tableToolbar._showMsg("删除成功,正在刷新页面..", function() {
                                window.location.href = window.location.href;
                            });
                        } else {
                            tableToolbar._showMsg("删除失败！");
                        }
                    },
                    error: function() {
                        tableToolbar._showMsg(tableToolbar.errorMsg);
                    }
                });

            },
            RightClick: function(type, id) {
                switch (type) {
                    case "update":
                        window.location.href = "/TeamCenter/AddTeamPlan.aspx?act=update&id=" + id + "&" + $.param(TeamplanList.Data);
                        break;
                    case "paituan":
                        if (tableToolbar.IsHandleElse == "false") {
                            var msgList = [], tr = $("#liststyle").find("input[type='checkbox'][value='" + id + "']").closest("tr");
                            if (tr.find("input[name='ItemUserID']").val() != tableToolbar.UserID) {
                                msgList.push("你不是该计划的销售员,无法派团计调!");
                            }
                            if (msgList.length > 0) {
                                tableToolbar._showMsg(msgList.join("<br />"));
                                return false;
                            }
                        }
                        Boxy.iframeDialog({
                            iframeUrl: "/TeamCenter/TeamPlanForAdjustment.aspx?id=" + id + "&" + $.param(TeamplanList.Data),
                            title: "派团给计调",
                            modal: true,
                            width: "560px",
                            height: "252px"
                        });
                        $("div.bt-wrapper").hide();
                        break;
                    case "shenqing":
                         if (tableToolbar.IsHandleElse == "false") {
                            var msgList=[],tr=$("#liststyle").find("input[type='checkbox'][value='"+id+"']").closest("tr");
                            if(tr.find("input[name='ItemUserID']").val()!=tableToolbar.UserID){
                                msgList.push("你不是该计划的销售员,无法垫付申请!");
                            }
                            if (msgList.length > 0) {
                                tableToolbar._showMsg(msgList.join("<br />"));
                                return false;
                            }
                         }
                        Boxy.iframeDialog({
                            iframeUrl: "/TeamCenter/ChaoXianShenQing.aspx?tourId=" + id + "&" + $.param(TeamplanList.Data),
                            title: "垫付申请",
                            modal: true,
                            width: "600px",
                            height: "300px"
                        });
                        $("div.bt-wrapper").hide();
                        break;
                    case "copy":
                        window.location.href = "/TeamCenter/AddTeamPlan.aspx?act=copy&id=" + id + "&" + $.param(TeamplanList.Data);
                        break;
                }
                return false;
            },
            GetFristHtml: function(tr) {
                var html = [], id = tr.find("td:eq(0)").find("input[type='checkbox']").val();

                if (this.Power[0]) {
                    html.push("<a onclick=TeamplanList.RightClick('update','" + id + "') href='javascript:void(0);' hidefocus='true' class='toolbar_update'><s class='updateicon'></s>修改</a>");
                }

                if ((tr.find("input[name='hideTourStatus']").val() == "<%=(int)EyouSoft.Model.EnumType.TourStructure.TourStatus.销售未派计划 %>"||tr.find("input[name='hideTourStatus']").val() == "<%=(int)EyouSoft.Model.EnumType.TourStructure.TourStatus.计调未接收 %>") && this.Power[1]) {
                    html.push("<a onclick=TeamplanList.RightClick('paituan','" + id + "') href='javascript:void(0);' hidefocus='true' class='toolbar_paiduan'><s class='ptjdicon'></s>派团给计调</a>");
                }
                
                if (this.Power[2] && tr.find("input[name='hideTourStatus']").val() == "<%=(int)EyouSoft.Model.EnumType.TourStructure.TourStatus.资金超限 %>") {
                    html.push("<a onclick=TeamplanList.RightClick('shenqing','" + id + "') href='javascript:void(0);' hidefocus='true' class='toolbar_shenqing'><s class='shoukuan'></s>垫付申请</a>");
                }
                else{
                    this.Power[2]=false;
                }
                if (this.Power[3]) {
                    html.push("<a onclick=TeamplanList.RightClick('copy','" + id + "') href='javascript:void(0);' hidefocus='true' class='toolbar_copy'><s class='copyicon'></s>复制</a>");
                }

                return html.join('');
            },
            Power:[<%=ListPower %>]

        }

        $(function() {
            tableToolbar.init({
                objectName: "计划",
                copyCallBack: function(arr) {
                    location.href = "AddTeamPlan.aspx?act=copy&id=" + arr[0].find(":checkbox").val() + "&" + $.param(TeamplanList.Data);
                    return false;
                },
                updateCallBack: function(arr) {
                    location.href = "AddTeamPlan.aspx?act=update&id=" + arr[0].find(":checkbox").val() + "&" + $.param(TeamplanList.Data);
                    return false;
                },
                deleteCallBack: function(arr) {
                    var msgList = new Array();
                    var state = "";
                    //遍历按钮返回数组对象
                    for (var i = 0; i < arr.length; i++) {
                        //从数组对象中找到数据所在，并保存到数组对象中
                        if (arr[i].find("input[type='checkbox']").val() != "on") {
                            state = arr[i].find("input[name='hideIsPayMoney']").val();
                            if (state == "true") {
                                msgList.push("当前选中项中第" + (i + 1) + "行有计调支出,无法删除!");
                            }
                        }
                    }
                    if (msgList.length > 0) {
                        tableToolbar._showMsg(msgList.join("<br />"));
                        return false;
                    }
                    TeamplanList.DelAll(arr);
                },
                cancelCallBack: function(arr) {
                    var msgList = new Array();
                    var state = "";
                    var cancel = "";
                    //遍历按钮返回数组对象
                    for (var i = 0; i < arr.length; i++) {
                        //从数组对象中找到数据所在，并保存到数组对象中
                        if (arr[i].find("input[type='checkbox']").val() != "on") {
                            state = arr[i].find("input[name='hideIsPayMoney']").val();
                            cancel = arr[i].find("input[name='hideTourStatus']").val();
                            if (state == "true") {
                                msgList.push("当前选中项中第" + (i + 1) + "行有计调支出,无法取消!");
                            }
                            if (cancel == "<%=(int)EyouSoft.Model.EnumType.TourStructure.TourStatus.已取消 %>") {
                                msgList.push("当前选中项中第" + (i + 1) + "行已取消,无法再次取消!");
                            }
                        }
                    }
                    if (msgList.length > 0) {
                        tableToolbar._showMsg(msgList.join("<br />"));
                        return false;
                    }
                    TeamplanList.CanelBox = new Boxy($("#div_Canel"), { modal: true, fixed: false, title: "取消", width: "580px", height: "210px" });
                },
                otherButtons: [{
                    button_selector: '.toolbar_paiduan',
                    sucessRulr: 1,
                    msg: '未选中任何 计划 ',
                    msg2: '只能选中一个 计划 ',
                    buttonCallBack: function(arr) {
                        if (tableToolbar.IsHandleElse == "false") {
                            var msgList = new Array();
                            if (arr[0].find("input[name='ItemUserID']").val() != tableToolbar.UserID) {
                                msgList.push("你不是该计划的销售员,无法派团计调!");
                            }
                            if (msgList.length > 0) {
                                tableToolbar._showMsg(msgList.join(''));
                                return false;
                            }
                        }

                        if (arr[0].find("input[type='hidden'][name='hideTourStatus']").val() == "<%=(int)EyouSoft.Model.EnumType.TourStructure.TourStatus.销售未派计划 %>"
                            ||arr[0].find("input[type='hidden'][name='hideTourStatus']").val() == "<%=(int)EyouSoft.Model.EnumType.TourStructure.TourStatus.计调未接收 %>") {
                            Boxy.iframeDialog({
                                iframeUrl: "TeamPlanForAdjustment.aspx?id=" + arr[0].find(":checkbox").val() + "&" + $.param(TeamplanList.Data),
                                title: "派团给计调",
                                modal: true,
                                width: "560px",
                                height: "260px"
                            });
                            return false;
                        } else {
                            tableToolbar._showMsg("该团不能进行派团操作!");
                        }
                    }
                }, {
                    button_selector: '.toolbar_shenqing',
                    sucessRulr: 1,
                    msg: '未选中任何 计划 ',
                    msg2: '只能选中一个 计划 ',
                    buttonCallBack: function(arr) {
                        if (tableToolbar.IsHandleElse == "false") {
                            var msgList = new Array();
                            if (arr[0].find("input[name='ItemUserID']").val() != tableToolbar.UserID) {
                                msgList.push("你不是该计划的销售员,无法垫付申请!");
                            }
                            if (msgList.length > 0) {
                                tableToolbar._showMsg(msgList.join(''));
                                return false;
                            }
                        }

                        if (arr[0].find("input[type='hidden'][name='hideTourStatus']").val() == "<%=(int)EyouSoft.Model.EnumType.TourStructure.TourStatus.资金超限 %>") {
                            Boxy.iframeDialog({
                                iframeUrl: "/TeamCenter/ChaoXianShenQing.aspx?tourId=" + arr[0].find(":checkbox").val() + "&" + $.param(TeamplanList.Data),
                                title: "垫付申请",
                                modal: true,
                                width: "600px",
                                height: "300px"
                            });
                            return false;
                        } else {
                            tableToolbar._showMsg("该团无法垫付申请!");
                        }
                    }
}]
                });

                //计划取消 确认事件
                $("#btnCanelSave").click(function() {
                    var ids = new Array();
                    $("#liststyle").find("input[type='checkbox']:checked").each(function() {
                        if (this.value && this.value != "on") {
                            ids.push(this.value);
                        }
                    })
                    var remarks = $.trim($("#txtCanelRemark").val());
                    if (remarks.length == 0) {
                        tableToolbar._showMsg("请输入取消原因!");
                        return false;
                    }
                    if (remarks.length > 200) {
                        tableToolbar._showMsg("输入的取消原因过长!");
                        return false;
                    }
                    $.newAjax({
                        type: "GET",
                        url: "/TeamCenter/TeamplanList.aspx?doType=canel&ids=" + ids.join(',') + "&remarks=" + encodeURIComponent(remarks) + "&" + $.param(TeamplanList.Data),
                        dataType: "json",
                        success: function(r) {
                            if (r.result == "1") {
                                TeamplanList.CanelBox.hide();
                                tableToolbar._showMsg("取消成功,正在刷新页面!", function() {
                                    window.location.href = window.location.href;
                                });
                            } else {
                                tableToolbar._showMsg("设置取消失败!");
                            }
                        }
                    })
                    return false;
                })

                //签证资料查看事件
                $("#liststyle").find("a[data-class='visaFiles']").click(function() {
                    Boxy.iframeDialog({
                        iframeUrl: "/TeamCenter/VisaFileList.aspx?tourId=" + $(this).attr("data-id") + "&" + $.param(TeamplanList.Data),
                        title: "查看签证资料",
                        modal: true,
                        width: "650px",
                        height: "420px"
                    });
                    return false;
                })

                //计调项泡泡
                BtFun.InitBindBt("GetJiDiaoIcon");

                $("#liststyle").find("a[data-class='paopao']").bt({
                    contentSelector: function() {
                        return $(this).next().html();
                    },
                    positions: ['left'],
                    fill: '#FFF2B5',
                    strokeStyle: '#D59228',
                    noShadowOpts: { strokeStyle: "#D59228" },
                    spikeLength: 10,
                    spikeGirth: 15,
                    width: 200,
                    overlap: 0,
                    centerPointY: 1,
                    cornerRadius: 4,
                    shadow: true,
                    shadowColor: 'rgba(0,0,0,.5)',
                    cssStyles: { color: '#00387E', 'line-height': '180%' }
                });

                $("#liststyle").find("a[data-class='cancelReason']").each(function() {
                    if ($.trim($(this).next().html()) != "") {
                        $(this).bt({
                            contentSelector: function() {
                                return $(this).next().html();
                            },
                            positions: ['left', 'right', 'bottom'],
                            fill: '#FFF2B5',
                            strokeStyle: '#D59228',
                            noShadowOpts: { strokeStyle: "#D59228" },
                            spikeLength: 10,
                            spikeGirth: 15,
                            width: 200,
                            overlap: 0,
                            centerPointY: 1,
                            cornerRadius: 4,
                            shadow: true,
                            shadowColor: 'rgba(0,0,0,.5)',
                            cssStyles: { color: '#00387E', 'line-height': '180%' }
                        });
                    }
                })
                
            $('td[data-class="right"]').bt({
            contentSelector: function() {
                var DivStr="";
                var powers=TeamplanList.Power;
                if(!(powers[0]==false && powers[1]==false && this.Power[2]==false && powers[3]==false)){
                    DivStr="<div class='td_tablehead'>" + TeamplanList.GetFristHtml($(this).parent()) + "</div>";
                }
                return DivStr;
            },
            positions: ['right'],
            fill: '#dff5ff',
            strokeStyle: '#46abdc',
            noShadowOpts: { strokeStyle: "#46abdc" },
            spikeLength: 5,
            spikeGirth: 15,
            width: 120,
            overlap: 0,
            centerPointY: 1,
            cornerRadius: 0,
            shadow: true,
            shadowColor: 'rgba(0,0,0,.5)',
            cssStyles: { color: '#00387E', 'line-height': '180%' }
        });

            });

    </script>

</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Front.Master" AutoEventWireup="true"
    CodeBehind="List.aspx.cs" Inherits="Web.ResourceCenter.LoosePulley.List" %>

<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script src="/Js/datepicker/WdatePicker.js" type="text/javascript"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="mainbox">
        <form id="SelectFrom" action="List.aspx" accept="get">
        <div class="searchbox fixed">
            <span class="searchT">
                <p>
                    游船公司：<input type="text" name="loosePulleyCor" class="inputtext formsize180" value="<%=Request.QueryString["loosePulleyCor"] %>" />
                    游船名称：<input type="text" name="loosePulleyName" class="inputtext formsize180" value="<%=Request.QueryString["loosePulleyName"] %>" />
                    登船时间：
                    <input name="periodStartTime" type="text" class="inputtext formsize80" onclick="WdatePicker()"
                        value="<%=Request.QueryString["periodStartTime"] %>" />
                    -
                    <input name="periodEndTime" type="text" class="inputtext formsize80" onclick="WdatePicker()"
                        value="<%=Request.QueryString["periodEndTime"] %>" />
                    <input type="submit" class="search-btn" /></p>
            </span>
        </div>
        <input type="hidden" name="sl" value="<%=Request.QueryString["sl"] %>" />
        </form>
        <div id="tablehead" class="tablehead">
            <asp:PlaceHolder ID="pan_Add" runat="server">
                <ul class="fixed">
                    <li id="li_Add"><s class="addicon"></s><a href="javascript:();" hidefocus="true"
                        class="toolbar_plshoukuan"><span>增加预控</span></a></li>
                    <li class="line"></li>
                </ul>
            </asp:PlaceHolder>
            <div class="pages">
                <cc1:ExporPageInfoSelect ID="ExporPageInfoSelect1" runat="server" />
            </div>
        </div>
        <!--列表表格-->
        <div class="tablelist-box">
            <table width="100%" id="liststyle">
                <tr>
                    <th class="thinputbg">
                        <input type="checkbox" name="checkbox" id="checkbox1" />
                    </th>
                    <th align="center" class="th-line">
                        游船公司
                    </th>
                    <th align="center" class="th-line">
                        游船名称
                    </th>
                    <th align="center" class="th-line">
                        控船数量
                    </th>
                    <th align="center" class="th-line">
                        登船日期
                    </th>
                    <th align="center" class="th-line">
                        最终保留日期
                    </th>
                    <th align="center" class="th-line">
                        已使用数量
                    </th>
                    <th align="center" class="th-line">
                        剩余数量
                    </th>
                    <th align="center" class="th-line">
                        航线
                    </th>
                    <th align="center" class="th-line">
                        景点
                    </th>
                    <th align="center" class="th-line">
                        操作
                    </th>
                </tr>
                <asp:Repeater ID="rpt_list" runat="server">
                    <ItemTemplate>
                        <tr data-id="<%#Eval("Id") %>">
                            <td align="center">
                                <input type="checkbox" name="checkbox" id="checkbox" />
                            </td>
                            <td align="center">
                                <a href="#" target="_blank">
                                    <%#Eval("ShipCompany")%></a>
                            </td>
                            <td align="center">
                                <span class="th-line">
                                    <%#Eval("ShipName")%></span>
                            </td>
                            <td align="center">
                                <b class="fontred">
                                    <%#Eval("ControlNum")%></b>
                            </td>
                            <td align="center">
                                <%# EyouSoft.Common.UtilsCommons.GetDateString(Eval("GoShipTime"), ProviderToDate)%>
                            </td>
                            <td align="center">
                                <%# EyouSoft.Common.UtilsCommons.GetDateString(Eval("LastTime"), ProviderToDate)%>
                            </td>
                            <td align="center">
                                <a href="javascript:();" class="a_USED"><b>
                                    <%#Eval("AlreadyNum")%></b></a>
                            </td>
                            <td align="center">
                                <b class="fontgreen">
                                    <%#(int)Eval("ControlNum") - (int)Eval("AlreadyNum")%></b>
                            </td>
                            <td align="center">
                                <%#Eval("ShipRoute")%>
                            </td>
                            <td align="center">
                                <%#Eval("ShipSpot")%>
                            </td>
                            <td align="center">
                                <a href="javascript:void(0);" class="a_updata"><span>预控变更</span></a> <a href="<%=YKQRDPrintUri %>?sueId=<%#Eval("Id") %>"
                                    target="_blank">预控确认单</a> 
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
                <asp:Panel ID="pan_Msg" runat="server">
                    <tr align="center">
                        <td colspan="11">
                            暂无数据!
                        </td>
                    </tr>
                </asp:Panel>
            </table>
        </div>
        <!--列表结束-->
        <div id="tablehead_clone">
        </div>
    </div>

    <script type="text/javascript">
        var PageJsDataObj = {
            ShowBoxy: function(data) {/*显示弹窗*/
                Boxy.iframeDialog({
                    iframeUrl: data.url,
                    title: data.title,
                    modal: true,
                    width: data.width,
                    height: data.height
                });
            },
            DataBoxy: function() {/*弹窗参数*/
                return {
                    url: "/ResourceCenter/CommonPage",
                    title: "游轮预控",
                    width: "700px",
                    height: "310px"
                }
            },
            BindBtn: function() {
                var that = this;
                tableToolbar.init({/*绑定功能按钮*/
                    tableContainerSelector: "#liststyle" //表格选择器
                })
                $("#li_Add").click(function() {
                    var data = that.DataBoxy();
                    data.title += EnglishToChanges.Ping("Add");
                    data.height = "350px"
                    data.url = "/ResourceCenter/LoosePulley/Add.aspx?" + $.param({ sl: Boxy.queryString("sl") });
                    that.ShowBoxy(data);
                    return false;
                })
                $(".a_updata").click(function() {
                    var data = that.DataBoxy();
                    data.title += "-预控变更-";
                    data.url += "/Updata.aspx?" + $.param({
                        sl: Boxy.queryString("sl"),
                        sourceControlCategory: '<%=(int)EyouSoft.Model.EnumType.SourceStructure.SourceControlCategory.游轮%>',
                        sourceId: $(this).closest("tr").attr("data-id")
                    });
                    data.height = "350px";
                    that.ShowBoxy(data);
                    return false;
                })
                $(".a_USED").click(function() {
                    var data = that.DataBoxy();
                    data.title += "-已使用数量-";
                    data.url += "/USED.aspx?" + $.param({
                        sl: Boxy.queryString("sl"),
                        sourceControlCategory: '<%=(int)EyouSoft.Model.EnumType.SourceStructure.SourceControlCategory.游轮%>',
                        sueId: $(this).closest("tr").attr("data-id")
                    });
                    data.height = "250px";
                    that.ShowBoxy(data);
                    return false;
                })
            },
            PageInit: function() {/*页面初始化*/
                /*绑定功能按钮*/
                this.BindBtn();
                $("#tablehead_clone").html($("#tablehead").clone(true).css("border-top", "0 none"));
            }
        }

        $(function() {
            //初始化页面
            PageJsDataObj.PageInit();
        })

        
        
    </script>

</asp:Content>

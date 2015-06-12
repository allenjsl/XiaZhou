<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Front.Master" AutoEventWireup="true"
    CodeBehind="List.aspx.cs" Inherits="Web.ResourceCenter.Car.List" %>

<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="mainbox">
        <form id="SelectFrom" action="List.aspx" accept="get">
        <div class="searchbox fixed">
            <span class="searchT">
                <p>
                    车队名称：<input type="text" name="txt_carName" value="<%=Request.QueryString["txt_carName"] %>"
                        class="inputtext formsize180" />
                    车型：
                    <input type="text" class="inputtext formsize120" value="<%=Request.QueryString["txt_carType"] %>"
                        name="txt_carType" />
                    天数：
                    <input value="<%=Request.QueryString["txt_day"] %>" name="txt_day" type="text" class="inputtext formsize50" />
                    <input type="submit" class="search-btn" /></p>
            </span>
        </div>
        <input type="hidden" name="sl" value="<%=Request.QueryString["sl"] %>" />
        </form>
        <div id="tablehead" class="tablehead">
            <asp:PlaceHolder ID="pan_Add" runat="server">
                <ul class="fixed">
                    <li id="li_Add"><s class="addicon"></s><a href="javascript:void(0);" hidefocus="true"
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
                        <input type="checkbox" name="checkbox" id="checkbox" />
                    </th>
                    <th align="center" class="th-line">
                        车队名称
                    </th>
                    <th align="center" class="th-line">
                        车型
                    </th>
                    <th align="center" class="th-line">
                        控车数量
                    </th>
                    <th align="center" class="th-line">
                        预控日期
                    </th>
                    <th align="center" class="th-line">
                        天数
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
                        <p>
                            操作</p>
                    </th>
                </tr>
                <asp:Repeater ID="rpt_list" runat="server">
                    <ItemTemplate>
                        <tr data-id="<%#Eval("Id") %>">
                            <td align="center">
                                <input type="checkbox" name="checkbox" id="checkbox" />
                            </td>
                            <td align="center">
                                <a hidefocus="true">
                                    <%#Eval("SourceName")%></a>
                            </td>
                            <td align="center">
                                <%#Eval("TypeName")%>
                            </td>
                            <td align="center">
                                <b class="fontred">
                                    <%#Eval("ControlNum")%></b>
                            </td>
                            <td align="center">
                                <%#EyouSoft.Common.UtilsCommons.GetDateString(Eval("SueStartTime"), ProviderToDate)%>
                                -
                                <%#EyouSoft.Common.UtilsCommons.GetDateString(Eval("SueEndTime"), ProviderToDate)%>
                            </td>
                            <td align="center">
                                <%#Eval("DaysNum")%>
                            </td>
                            <td align="center">
                                <b class="fontgreen">
                                    <%#EyouSoft.Common.UtilsCommons.GetDateString(Eval("LastTime"), ProviderToDate)%></b>
                            </td>
                            <td align="center">
                                <a href="javascript:();" class="a_USED" hidefocus="true"><b>
                                    <%#Eval("AlreadyNum")%></b></a>
                            </td>
                            <td align="center">
                                <b class="fontgreen">
                                    <%#(int)Eval("ControlNum") - (int)Eval("AlreadyNum")%></b>
                            </td>
                            <td align="center">
                                <a href="javascript:void(0);" class="a_updata" hidefocus="true"><span>预控变更</span></a>
                                <a hidefocus="true"  href="<%=YKQRDPrintUri %>?sueId=<%#Eval("Id") %>" target="_blank">预控确认单</a> 
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
                <asp:Panel ID="pan_Msg" runat="server">
                    <tr align="center">
                        <td colspan="10">
                            暂无数据!
                        </td>
                    </tr>
                </asp:Panel>
            </table>
        </div>
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
                    title: "车辆预控",
                    width: "700px",
                    height: "310px"
                }
            },
            Add: function() {/*添加弹窗*/
                var data = this.DataBoxy();
                data.title += EnglishToChanges.Ping("Add");
                data.url = "/ResourceCenter/Car/Add.aspx?" + $.param({ sl: Boxy.queryString("sl") });
                this.ShowBoxy(data);
                return false;
            },
            USED: function(obj) {/*已使用数量弹窗*/
                var data = this.DataBoxy();
                data.title += "-已使用数量-";
                data.url += "/USED.aspx?" + $.param({
                    sl: Boxy.queryString("sl"),
                    sourceControlCategory: '<%=(int)EyouSoft.Model.EnumType.SourceStructure.SourceControlCategory.车辆%>',
                    sueId: $(obj).closest("tr").attr("data-id")
                });
                data.height = "250px";
                this.ShowBoxy(data);
                return false;
            },
            BindBtn: function() {
                var that = this;
                tableToolbar.init({/*绑定功能按钮*/
                    tableContainerSelector: "#liststyle", //表格选择器
                    objectName: "记录"
                })
                $("#li_Add").click(function() {
                    that.Add();
                    return false;
                })
                $(".a_updata").click(function() {
                    var data = that.DataBoxy();
                    data.title += "-预控变更-";
                    data.url += "/Updata.aspx?" + $.param({
                        sl: Boxy.queryString("sl"),
                        sourceControlCategory: '<%=(int)EyouSoft.Model.EnumType.SourceStructure.SourceControlCategory.车辆%>',
                        sourceId: $(this).closest("tr").attr("data-id")
                    });
                    data.height = "250px";
                    that.ShowBoxy(data);
                    return false;
                })
                $(".a_USED").click(function() {
                    that.USED(this);
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

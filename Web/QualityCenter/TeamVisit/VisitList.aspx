<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="VisitList.aspx.cs" MasterPageFile="~/MasterPage/Front.Master"
    Inherits="Web.QualityCenter.TeamVisit.VisitList" %>

<%@ Register Src="~/UserControl/CustomerUnitSelect.ascx" TagName="CustomerUnitSelect"
    TagPrefix="uc2" %>
<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="mainbox">
        <div class="searchbox fixed">
            <form method="get">
            <span class="searchT">团号：<input type="text" name="teamNum" class="inputtext formsize120"
                size="30" value='<%=Request.QueryString["teamNum"]%>' />
                线路名称：<input type="text" name="lineName" class="inputtext formsize120" size="35" value='<%=Request.QueryString["lineName"]%>' />
                回团日期：<input type="text" class="inputtext formsize80" name="startTime" id="startTime"
                    onfocus="WdatePicker({maxDate:'#F{$dp.$D(\'endTime\')}'})" value='<%=Request.QueryString["startTime"]%>' /><a
                        href="javascript:document.getElementById('startTime').focus();" class="timesicon"></a>
                至<input type="text" class="inputtext formsize80" name="endTime" id="endTime" onfocus="WdatePicker({minDate:'#F{$dp.$D(\'startTime\')}'})"
                    value='<%=Request.QueryString["endTime"]%>' /><a href="javascript:document.getElementById('endTime').focus();"
                        class="timesicon"></a> 客户单位：
                <uc2:CustomerUnitSelect ID="CustomerUnitSelect1" IsMust="false" runat="server" IsMultiple="1"
                    SelectFrist="false" />
                导游：<input class="inputtext formsize40" type="text" size="10" id="txtGuid" name="txtGuid"
                    value="<%=Request.QueryString["txtGuid"]%>">
                <input type="submit" id="btnSubmit" class="search-btn" value="搜索" />
            </span>
            <input type="hidden" name="sl" value='<%=Request.QueryString["sl"]%>' />
            </form>
        </div>
        <div class="tablehead" id="select_Toolbar_Paging_1">
            <ul class="fixed">
                <li><s class="copyicon"></s><a href="VisitStatistic.aspx" hidefocus="true" class="toolbar_add tdhz">
                    <span>每日汇总表</span></a></li>
            </ul>
            <div class="pages">
                <cc1:ExporPageInfoSelect ID="ExporPageInfoSelect1" runat="server" />
            </div>
        </div>
        <!--列表表格-->
        <div class="tablelist-box">
            <table width="100%" id="liststyle">
                <tr>
                    <th align="center" class="th-line">
                        团号
                    </th>
                    <th align="left" class="th-line">
                        线路名称
                    </th>
                    <th align="center" class="th-line">
                        回团时间
                    </th>
                    <th align="center" class="th-line">
                        客户单位
                    </th>
                    <th align="center" class="th-line">
                        销售员
                    </th>
                    <th align="center" class="th-line">
                        带团导游
                    </th>
                    <th align="center" class="th-line">
                        计调员
                    </th>
                    <th align="center" class="th-line">
                        团队状态
                    </th>
                    <th align="center" class="th-line">
                        回访结果
                    </th>
                    <%if (isVisit)
                      { %>
                    <th align="center" class="th-line">
                        操作
                    </th>
                    <%}
                    %>
                </tr>
                <asp:Repeater ID="RepList" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td align="center">
                                <%#Eval("TourCode")%>
                            </td>
                            <td align="left">
                                <a href="javascript:void(0);">
                                    <%#Eval("RouteName")%></a>
                            </td>
                            <td align="center">
                                <%#Eval("RDate", "{0:yyyy-MM-dd}")%>
                            </td>
                            <td align="center">
                                <%#Eval("UnitName")%>
                            </td>
                            <td align="center">
                                <%#Eval("SalesmanName")%>
                            </td>
                            <td align="center">
                                <%#this.GetGuidName(Eval("GuideName"))%>
                            </td>
                            <td align="center">
                                <%#Eval("PlanerName")%>
                            </td>
                            <td align="center">
                                <label data-class="status">
                                    <%#Eval("TourStatus")%>
                                </label>
                            </td>
                            <td align="center">
                                <a class="a_info" href="TourVisitList.aspx?id=<%#Eval("TourId")%>">
                                    <%#Math.Round(Convert.ToDecimal( Eval("QualityScore")),2)%></a>
                            </td>
                            <%if (isVisit)
                              { %>
                            <td align="center">
                                <a href='VisitForm.aspx?doType=add&tourId=<%#Eval("TourId")%>' class="a_info">回访</a>
                            </td>
                            <%}
                            %>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
        </div>
        <!--列表结束-->
        <div class="tablehead">

            <script type="text/javascript">
                document.write(document.getElementById("select_Toolbar_Paging_1").innerHTML);
			</script>

        </div>

        <script src="/Js/datepicker/WdatePicker.js" type="text/javascript"></script>

        <%--公共方法与必须方法--%>

        <script type="text/javascript">
            var PageJsData = {
                Query: {/*URL参数对象*/
                    sl: '<%=Request.QueryString["sl"] %>'
                },
                DataBoxy: function() {/*弹窗默认参数*/
                    return {
                        url: '/ManageCenter/Notice',
                        title: "",
                        width: "748px",
                        height: "445px"
                    }
                },
                ShowBoxy: function(data) {/*显示弹窗*/
                    Boxy.iframeDialog({
                        iframeUrl: data.url,
                        title: data.title,
                        modal: true,
                        width: data.width,
                        height: data.height
                    });
                }
            }
            //绑定功能按钮
            $(function() {
                if ($("#liststyle tr td label[data-class='status']").html() == "封团") {
                    $(this).addClass("fontgray");
                }
                $(".tdhz").click(function() {
                    var data =  PageJsData.DataBoxy();
                    data.url = $(this).attr("href") + "?";
                    data.url += $.param({ sl: PageJsData.Query.sl, doType: "huifang" })
                    data.title = "每日汇总表";
                    data.width = "670px";
                    data.height = "430px";
                    PageJsData.ShowBoxy(data);
                    return false;
                });
                $(".a_info").click(function() {
                    var data =  PageJsData.DataBoxy();
                    data.url = $(this).attr("href") + "&";
                    data.url += $.param({ sl: PageJsData.Query.sl });
                    data.title = "回访明细";
                    data.width = "680px";
                    data.height = "400px";
                    PageJsData.ShowBoxy(data);
                    return false;
                });

            })
        </script>

    </div>
</asp:Content>

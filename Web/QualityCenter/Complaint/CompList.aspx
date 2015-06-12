<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CompList.aspx.cs" MasterPageFile="~/MasterPage/Front.Master"
    Inherits="Web.QualityCenter.Complaint.CompList" %>

<%@ Register Src="~/UserControl/CustomerUnitSelect.ascx" TagName="CustomerUnitSelect"
    TagPrefix="uc2" %>
<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="mainbox">
        <div class="searchbox fixed">
            <form method="get">
            <span class="searchT">
                <p>
                    团号：<input type="text" name="teamNum" class="inputtext formsize120" size="30" value='<%=Request.QueryString["teamNum"]%>' />
                    线路名称：<input type="text" name="lineName" class="inputtext formsize120" size="35" value='<%=Request.QueryString["lineName"]%>' />
                    投诉日期：<input type="text" class="inputtext formsize80" name="startTime" id="startTime"
                        onfocus="WdatePicker({maxDate:'#F{$dp.$D(\'endTime\')}'})" value='<%=Request.QueryString["startTime"]%>' /><a
                            href="javascript:document.getElementById('startTime').focus();" class="timesicon"></a>
                    至<input type="text" class="inputtext formsize80" name="endTime" id="endTime" onfocus="WdatePicker({minDate:'#F{$dp.$D(\'startTime\')}'})"
                        value='<%=Request.QueryString["endTime"]%>' /><a href="javascript:document.getElementById('endTime').focus();"
                            class="timesicon"></a> 投诉人：<input type="text" id="complaintsName" name="complaintsName"
                                class="inputtext formsize120" value='<%=Request.QueryString["complaintsName"]%>' />
                    投诉类型：<input type="text" id="complaintsType" name="complaintsType" class="inputtext formsize120"
                        value='<%=Request.QueryString["complaintsType"]%>' />
                    <input type="hidden" name="sl" value='<%=Request.QueryString["sl"]%>' />
                    <input type="submit" id="btnSubmit" class="search-btn" value="搜索" /></p>
            </span>
            </form>
        </div>
        <div class="tablehead" id="select_Toolbar_Paging_1">
            <ul class="fixed">
                <asp:PlaceHolder runat="server" ID="ph_tousu">
                    <li><s class="copyicon"></s><a href="javascript:void(0)" hidefocus="true" class="toolbar_add add_gg">
                        <span>投诉</span></a></li></asp:PlaceHolder>
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
                        投诉日期
                    </th>
                    <th align="center" class="th-line">
                        投诉人
                    </th>
                    <th align="center" class="th-line">
                        投诉类型
                    </th>
                    <th align="center" class="th-line">
                        状态
                    </th>
                    <th align="center" class="th-line">
                        处理人
                    </th>
                    <th align="center" class="th-line">
                        处理时间
                    </th>
                    <th align="center" class="th-line">
                        处理结果
                    </th>
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
                                <%#Eval("ComplaintsTime", "{0:yyyy-MM-dd}")%>
                            </td>
                            <td align="center">
                                <%#Eval("ComplaintsName")%>
                            </td>
                            <td align="center">
                                <%#Eval("ComplaintsType")%>
                            </td>
                            <td align="center">
                                <a href="javascript:void(0);" class="tsgl" data-class='<%#Eval("IsHandle") %>' data-id='<%#Eval("ComplaintsId") %>'>
                                    <%# (bool)Eval("IsHandle")?"已处理":"<lable style='color:Red'>未处理</label>"%></a>
                            </td>
                            <td align="center">
                                <%#Eval("HandleName")%>
                            </td>
                            <td align="center">
                                <%#Eval("HandleTime", "{0:yyyy-MM-dd}")%>
                            </td>
                            <td align="center">
                                <%#Eval("HandleResult")%>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
        </div>
        <!--列表结束-->
        <div class="tablehead" style="border: 0 none;">
            <script type="text/javascript">
                document.write(document.getElementById("select_Toolbar_Paging_1").innerHTML);
			</script>

        </div>
    </div>

    <script type="text/javascript">
        var PageJsDataObj = {
            Query: {/*URL参数对象*/
                sl: '<%=Request.QueryString["sl"] %>'
            },
            DataBoxy: function() {/*弹窗默认参数*/
                return {
                    url: '/QualityCenter/Complaint',
                    title: "",
                    width: "770px",
                    height: "300px"
                }
            },
            ShowBoxy: function(data) {/*显示弹窗*/
                var win = top || window;
                win.Boxy.iframeDialog({
                    iframeUrl: data.url,
                    title: data.title,
                    modal: true,
                    width: data.width,
                    height: data.height
                });
            },
            Add: function() {
                var data =  this.DataBoxy();
                data.url += '/CompForm.aspx?';
                data.title = '投诉添加';
                data.url += $.param({
                    sl: this.Query.sl,
                    doType: "add"
                });
                this.ShowBoxy(data);
            },
            BindBtn: function() {
                $(".add_gg").click(function() {
                    PageJsDataObj.Add();
                    return false;
                })
            },
            DealWithCom: function(obj) {
                var data =  this.DataBoxy();
                data.title = "投诉处理";
                data.url += "/CompStatistic.aspx?";
                data.url += $.param({
                    sl: this.Query.sl,
                    isDeal: $(obj).attr("data-class"),
                    ComplaintsId: $(obj).attr("data-id")
                })
                this.ShowBoxy(data);
            },
            PageInit: function() {
                //绑定功能按钮
                this.BindBtn();
                //当列表页面出现横向滚动条时使用以下方法 $("需要滚动最外层选择器").moveScroll();
                $('.tablelist-box').moveScroll();
            }
        }
        $(function() {
            PageJsDataObj.BindBtn();
            $(".tsgl").click(function() {
                PageJsDataObj.DealWithCom(this);
                return false;
            });
        })
    </script>

    <script src="/Js/datepicker/WdatePicker.js" type="text/javascript"></script>

</asp:Content>

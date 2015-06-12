<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AttList.aspx.cs" Inherits="Web.ManageCenter.Attendance.AttList"
    MasterPageFile="~/MasterPage/Front.Master" %>

<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>
<%@ Register Src="~/UserControl/SelectSection.ascx" TagName="SelectSection1" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="mainbox">
        <div class="searchbox fixed">
            <form method="get">
            <span class="searchT">
                <p>
                    部门：
                    <uc2:SelectSection1 ID="SelectSection1" runat="server" SModel="2" SetTitle="部门" />
                    姓名：<input type="text" class="inputtext formsize140" size="35" id="txtName" name="txtName"
                        value='<%=Request.QueryString["txtName"] %>' />
                    员工编号：<input type="text" class="inputtext formsize120" size="30" id="txtNum" name="txtNum"
                        value='<%=Request.QueryString["txtNum"] %>' />
                    <button type="submit" id="btnSubmit" class="search-btn">
                        搜索</button></p>
            </span>
            <input type="hidden" name="sl" value='<%=Request.QueryString["sl"]%>' />
            </form>
        </div>
        <div class="tablehead" id="select_Toolbar_Paging_1">
            <ul class="fixed">
                <%if (IsKaoQin)
                  { %>
                <li class="line"></li>
                <li><s class="plkaoqin"></s><a href="javascript:void(0)" hidefocus="true" class="toolbar_piliangkq">
                    <span>批量考勤</span></a></li><li class="line"></li>
                <%} %>
                <li><s class="kaoqinhz"></s><a href="AttStatistic.aspx" hidefocus="true" class="kq_hz">
                    <span>考勤汇总表</span></a></li>
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
                    <th width="130" align="center" class="th-line">
                        员工编号
                    </th>
                    <th width="133" align="center" class="th-line">
                        姓名
                    </th>
                    <th width="204" align="center" class="th-line">
                        部门
                    </th>
                    <th width="637" align="center" class="th-line">
                        当月考勤概况
                    </th>
                    <th width="136" align="center" class="th-line">
                        考勤明细
                    </th>
                    <%if (IsKaoQin)
                      { %>
                    <th width="117" align="center" class="th-line">
                        考勤登记
                    </th>
                    <%} %>
                </tr>
                <asp:Repeater ID="RepList" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td align="center">
                                <input type="checkbox" name="checkbox" value="<%#Eval("StaffId")%>" data-name="<%#Eval("StaffName") %>"
                                    data-deptid='<%#Eval("StaffDeptId") %>' />
                            </td>
                            <td align="center">
                                <%#Eval("FileNumber")%>
                            </td>
                            <td align="center">
                                <%#Eval("StaffName")%>
                            </td>
                            <td align="center">
                                <%#Eval("DepartName")%>
                            </td>
                            <td align="center">
                                全勤<%#Eval("Punctuality")%>天，迟到<%#Eval("Late")%>天，早退<%#Eval("LeaveEarly")%>天，旷工<%#Eval("Absenteeism")%>天，请假<%#Math.Round(Convert.ToDouble(Eval("Vacation")),1)%>天，加班<%#Math.Round(Convert.ToDouble(Eval("OverTime")),1)%>小时,停职<%#Math.Round(Convert.ToDouble(Eval("Suspension")),1)%>天
                                ，出差<%#Math.Round(Convert.ToDouble(Eval("Travel")),1)%>天，休假<%#Math.Round(Convert.ToDouble(Eval("Rest")),1)%>天
                            </td>
                            <td align="center">
                                <a href="AttShow.aspx?id=<%#Eval("StaffId")%>" class="kq_ck check-btn"></a>
                            </td>
                            <%if (IsKaoQin)
                              { %>
                            <td align="center">
                                <a href="AttDetail.aspx?id=<%#Eval("StaffId")%>&deptid=<%#Eval("StaffDeptId") %>"
                                    class="kq_dj">登记</a>
                            </td>
                            <%} %>
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
    </div>

    <script type="text/javascript">
        var PageJsDataObj = {
            Query: {/*URL参数对象*/
                sl: '<%=Request.QueryString["sl"] %>'
            },
            BindClose: function() {
                $("a[data-class='a_close']").unbind().click(function() {
                    window.location.reload();
                    return false;
                })
            },
            DataBoxy: function() {/*弹窗默认参数*/
                return {
                    url: '/ManageCenter/Attendance',
                    title: "",
                    width: "660px",
                    height: "503px"
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
            },
            //考勤登记
            AttentBook: function(obj) {
                var data = this.DataBoxy();
                data.url = $(obj).attr("href") + "&";
                data.url += $.param({
                    sl: this.Query.sl,
                    doType: "add"
                });
                data.title = "考勤登记";
                this.ShowBoxy(data);
            },
            //考勤明细
            AttenShow: function(obj) {
                var data = this.DataBoxy();
                data.url = $(obj).attr("href") + "&";
                data.url += $.param({
                    sl: this.Query.sl
                });
                data.width = "540px";
                data.height = "490px";
                data.title = "考勤明细";
                this.ShowBoxy(data);
            },
            //考勤汇总
            AttenStaic: function(obj) {
                var data = this.DataBoxy();
                data.url = $(obj).attr("href") + '?';
                data.url += $.param({
                    sl: this.Query.sl,
                    toxlsrecordcount: 2

                });
                data.title = "考勤汇总表";
                data.width = "850px";
                data.height = "460px";
                this.ShowBoxy(data);
            },
            BindBtn: function() {
                tableToolbar.init({
                    objectName: "考勤信息",
                    otherButtons: [{
                        button_selector: '.toolbar_piliangkq',
                        sucessRulr: 2,
                        msg: '未选中任何考勤信息 ',
                        buttonCallBack: function(objArr) {
                            var list = new Array();
                            var names = new Array();
                            var deptids = new Array();
                            for (var i = 0; i < objArr.length; i++) {
                                if (objArr[i].find("input[type='checkbox']").val() != "on") {
                                    list.push(objArr[i].find("input[type='checkbox']").val());
                                    names.push(objArr[i].find("input[type='checkbox']").attr("data-name"));
                                    deptids.push(objArr[i].find("input[type='checkbox']").attr("data-deptid"));
                                }
                            }
                            var data = PageJsDataObj.DataBoxy();
                            data.url = "AttGroup.aspx?";
                            data.url += $.param({
                                sl: PageJsDataObj.Query.sl,
                                id: list.join(','),
                                names: names.join('、'),
                                deptids: deptids.join(',')
                            });
                            data.title = "批量考勤";
                            data.width = "848px";
                            data.height = "380px";
                            PageJsDataObj.ShowBoxy(data);
                            return false;
                        }
}]
                    });
                },
                PageInit: function() {
                    //绑定功能按钮
                    this.BindBtn();
                    //当列表页面出现横向滚动条时使用以下方法 $("需要滚动最外层选择器").moveScroll();
                    $('.tablelist-box').moveScroll();
                }
            }
            $(function() {
                $(".kq_hz").click(function() {
                    PageJsDataObj.AttenStaic(this);
                    return false;
                });
                $(".kq_dj").click(function() {
                    PageJsDataObj.AttentBook(this);
                    return false;
                });
                $(".kq_ck").click(function() {
                    PageJsDataObj.AttenShow(this);
                    PageJsDataObj.BindClose();
                    return false;
                });
                PageJsDataObj.PageInit();
            })
    </script>

</asp:Content>

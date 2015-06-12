<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MeetList.aspx.cs" Inherits="Web.ManageCenter.Meet.MeetList"
    MasterPageFile="~/MasterPage/Front.Master" %>

<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="mainbox">
        <div class="searchbox fixed">
            <form>
            <span class="searchT">
                <p>
                    会议编号：<input type="text" id="txtNum" name="txtNum" class="inputtext formsize120" size="30"
                        value='<%=Request.QueryString["txtNum"] %>' />
                    会议主题：<input type="text" id="txtTitle" name="txtTitle" class="inputtext formsize140"
                        size="35" value='<%=Request.QueryString["txtTitle"] %>' />
                    会议时间：<input type="text" id="txtStartTime" name="txtStartTime" class="inputtext formsize120"
                        size="35" value='<%=Request.QueryString["txtStartTime"] %>' onfocus="WdatePicker({dateFmt:'yyyy-MM-dd HH:mm',maxDate:'#F{$dp.$D(\'txtEndTime\')}'})" />
                    至<input type="text" id="txtEndTime" name="txtEndTime" class="inputtext formsize120"
                        size="35" value='<%=Request.QueryString["txtEndTime"] %>' onfocus="WdatePicker({dateFmt:'yyyy-MM-dd HH:mm',minDate:'#F{$dp.$D(\'txtStartTime\')}'})" />
                    <button type="submit" id="btnSubmit" class="search-btn">
                        搜索</button></p>
            </span>
            <input type="hidden" name="sl" value='<%=Request.QueryString["sl"]%>' />
            </form>
        </div>
        <div class="tablehead" id="select_Toolbar_Paging_1">
            <ul class="fixed">
                <asp:PlaceHolder runat="server" ID="ph_Add">
                    <li><a href="javascript:void(0)" hidefocus="true" class="toolbar_add add_gg"><s class="addicon">
                    </s><span>添加</span></a> </li>
                </asp:PlaceHolder>
                <asp:PlaceHolder ID="ph_Update" runat="server">
                    <li class="line"></li>
                    <li><a href="javascript:void(0)" hidefocus="true" class="toolbar_update"><s class="updateicon">
                    </s><span>修改</span></a> </li>
                </asp:PlaceHolder>
                <asp:PlaceHolder ID="ph_Del" runat="server">
                    <li class="line"></li>
                    <li><a href="javascript:void(0)" hidefocus="true" class="toolbar_delete"><s class="delicon">
                    </s><span>删除</span></a> </li>
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
                    <th width="86" align="center" class="th-line">
                        会议编号
                    </th>
                    <th width="135" align="center" class="th-line">
                        会议类别
                    </th>
                    <th width="178" align="center" class="th-line">
                        会议主题
                    </th>
                    <th width="292" align="center" class="th-line">
                        参会人员
                    </th>
                    <th width="186" align="center" class="th-line">
                        会议时间
                    </th>
                    <th width="152" align="center" class="th-line">
                        会议地点
                    </th>
                    <th width="314" align="center" class="th-line">
                        会议纪要
                    </th>
                </tr>
                <asp:Repeater ID="RepList" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td align="center">
                                <input type="checkbox" name="checkbox" id="checkbox" value="<%#Eval("MeetingId")%>" />
                            </td>
                            <td align="center">
                                <a href="#" data-class="a_Num" data-id='<%#Eval("MeetingId")%>'>
                                    <%#Eval("Number")%>
                                </a>
                            </td>
                            <td align="center">
                                <%#Eval("Category")%>
                            </td>
                            <td align="center">
                                <%#Eval("Theme")%>
                            </td>
                            <td align="center">
                                <%#Eval("MeetingStaff")%>
                                <input type="hidden" name="ItemUserID" value="<%#Eval("OperatorId")%>" />
                            </td>
                            <td align="center">
                                <%#Eval("StartTime", "{0:yyyy-MM-dd HH:mm}")%>至<%#Eval("EndTime", "{0:yyyy-MM-dd HH:mm}")%>
                            </td>
                            <td align="center">
                                <%#Eval("Venue")%>
                            </td>
                            <td align="center">
                                <%#EyouSoft.Common.Utils.GetText(Eval("Minutes").ToString(),40,true)%>
                            </td>
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

    <script src="/Js/datepicker/WdatePicker.js" type="text/javascript"></script>

    <script type="text/javascript">
        var PageJsData = {
            Query: {/*URL参数对象*/
                sl: '<%=Request.QueryString["sl"] %>'
            },
            DataBoxy: function() {/*弹窗默认参数*/
                return {
                    url: '/ManageCenter/Meet',
                    title: "",
                    width: "700px",
                    height: "300px"
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
            GoAjax: function(url) {
                $.newAjax({
                    type: "post",
                    cache: false,
                    url: url,
                    dataType: "json",
                    success: function(result) {
                        if (result.result == "1") {
                            tableToolbar._showMsg(result.msg, function() {
                                $("#btnSubmit").click();
                            });

                        }
                        else { tableToolbar._showMsg(result.msg); }
                    },
                    error: function() {
                        //ajax异常--你懂得
                        tableToolbar._showMsg(tableToolbar.errorMsg);
                    }
                });
            },
            Add: function() {
                var data =  this.DataBoxy();
                data.url += '/MeetAdd.aspx?';
                data.title = '添加会议';
                data.url += $.param({
                    sl: this.Query.sl,
                    doType: "add"
                });
                this.ShowBoxy(data);
            },
            Update: function(objsArr) {
                var data =  this.DataBoxy();
                data.url += '/MeetAdd.aspx?';
                data.title = '修改会议';
                data.url += $.param({
                    sl: this.Query.sl,
                    doType: "update",
                    id: objsArr[0].find('input[type="checkbox"]').val()
                });
                this.ShowBoxy(data);
            },
            Delete: function(objsArr) {
                var list = new Array();
                for (var i = 0; i < objsArr.length; i++) {
                    list.push(objsArr[i].find("input[type='checkbox']").val());
                }
                var data =  this.DataBoxy();
                data.url += "/MeetList.aspx?";
                data.url += $.param({
                    sl: this.Query.sl,
                    doType: "delete",
                    id: list.join(",")
                });
                this.GoAjax(data.url);
            },
            BindBtn: function() {
                $(".add_gg").click(function() {
                    PageJsData.Add();
                    return false;
                })
                tableToolbar.init({
                    tableContainerSelector: "#liststyle", //表格选择器
                    objectName: "会议信息",
                    updateCallBack: function(objsArr) {
                        PageJsData.Update(objsArr);
                        return false;
                    },
                    deleteCallBack: function(objsArr) {
                        PageJsData.Delete(objsArr);
                    }
                });
            },
            PageInit: function() {
                //绑定功能按钮
                this.BindBtn();
                //当列表页面出现横向滚动条时使用以下方法 $("需要滚动最外层选择器").moveScroll();
                //$('.tablelist-box').moveScroll();
            }
        }
        $(function() {
            PageJsData.BindBtn();
            $("a[data-class='a_Num']").click(function() {
                var data = PageJsData.DataBoxy();
                data.url += '/MeetAdd.aspx?';
                data.title = '修改会议';
                data.url += $.param({
                    sl: PageJsData.Query.sl,
                    doType: "update",
                    id: $(this).attr("data-id")
                });
                PageJsData.ShowBoxy(data);
                return false;
            })
        });
    </script>

</asp:Content>

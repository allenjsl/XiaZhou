<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TrainList.aspx.cs" Inherits="Web.ManageCenter.Train.TrainList"
    MasterPageFile="~/MasterPage/Front.Master" %>

<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="mainbox">
        <div class="searchbox fixed">
            <form method="get">
            <span class="searchT">
                <p>
                    培训时间：<input type="text" class="inputtext formsize80" size="35" id="txtSTime" name="txtSTime"
                        onfocus="WdatePicker()" value='<%=Request.QueryString["txtSTime"]%>' />-<input type="text"
                            class="inputtext formsize80" size="35" id="txtETime" name="txtETime" onfocus="WdatePicker()"
                            value='<%=Request.QueryString["txtETime"]%>' />
                    主题：<input type="text" id="txtTheme" class="inputtext formsize140" name="txtTheme"
                        value='<%=Request.QueryString["txtTheme"]%>' />
                    <input type="hidden" name="sl" value='<%=Request.QueryString["sl"] %>' />
                    <button type="submit" id="btnSubmit" class="search-btn">
                        搜索</button></p>
            </span>
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
                    <th width="183" align="center" class="th-line">
                        培训时间
                    </th>
                    <th width="471" align="center" class="th-line">
                        主题
                    </th>
                    <th width="170" align="center" class="th-line">
                        参与人员
                    </th>
                    <th width="137" align="center" class="th-line">
                        培训地点
                    </th>
                    <th width="137" align="center" class="th-line">
                        附件
                    </th>
                </tr>
                <asp:Repeater ID="RepList" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td align="center">
                                <input type="checkbox" name="checkbox" id="checkbox" value="<%#Eval("TrainId")%>" />
                            </td>
                            <td align="center">
                                <%#Eval("StateTime", "{0:yyyy-MM-dd HH:mm}")%>
                            </td>
                            <td align="center">
                                <%#Eval("Title")%>
                            </td>
                            <td align="center">
                                <%#Eval("JoinPeople")%>
                            </td>
                            <td align="center">
                                <%#Eval("TrainingLocations")%>
                                <input type="hidden" name="ItemUserID" value="<%#Eval("OperatorId")%>" />
                            </td>
                            <td align="center">
                                <%#this.getFile(Eval("ComAttachList"))%>
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

    <script type="text/javascript">
        var PageJsDataObj = {
            Query: {/*URL参数对象*/
                sl: '<%=Request.QueryString["sl"] %>'
            },
            DataBoxy: function() {/*弹窗默认参数*/
                return {
                    url: '/ManageCenter/Train',
                    title: "",
                    width: "725px",
                    height: "340px"
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
                data.url += '/TrainAdd.aspx?';
                data.title = '添加培训';
                data.url += $.param({
                    sl: this.Query.sl,
                    doType: "add"
                });
                this.ShowBoxy(data);
            },
            Update: function(objsArr) {
                var data =  this.DataBoxy();
                data.url += '/TrainAdd.aspx?';
                data.title = '修改培训';
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
                data.url += "/TrainList.aspx?";
                data.url += $.param({
                    sl: this.Query.sl,
                    doType: "delete",
                    id: list.join(",")
                });
                this.GoAjax(data.url);
            },
            BindBtn: function() {
                $(".add_gg").click(function() {
                    PageJsDataObj.Add();
                    return false;
                })
                tableToolbar.init({
                    tableContainerSelector: "#liststyle", //表格选择器
                    objectName: "培训信息",
                    updateCallBack: function(objsArr) {
                        PageJsDataObj.Update(objsArr);
                        return false;
                    },
                    deleteCallBack: function(objsArr) {
                        PageJsDataObj.Delete(objsArr);
                    }
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
            PageJsDataObj.PageInit();

        })
    </script>

    <script src="/Js/datepicker/WdatePicker.js" type="text/javascript"></script>

</asp:Content>

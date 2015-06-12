<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RuleList.aspx.cs" Inherits="Web.ManageCenter.Rule.RuleList"
    MasterPageFile="~/MasterPage/Front.Master" %>

<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>
<%@ Register Src="~/UserControl/SelectSection.ascx" TagName="DeptSelect" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="mainbox">
        <div class="searchbox fixed">
            <form id="searchFrom" method="get">
            <span class="searchT">
                <p>
                    编号：<input type="text" name="txtNum" id="txtNum" class="inputtext formsize120" size="30"
                        value='<%=Request.QueryString["txtNum"] %>' />
                    制度标题：<input type="text" name="txtTitle" id="txtTitle" class="inputtext formsize140"
                        size="35" value='<%=Request.QueryString["txtTitle"] %>' />
                    适用部门:<cc2:DeptSelect ID="DeptSelect1" runat="server" SModel="2" SetTitle="适用部门" />
                    <button type="submit" id="btnSubmit" class="search-btn">
                        搜索</button></p>
            </span>
            <input type="hidden" name="sl" value='<%=Request.QueryString["sl"] %>' />
            </form>
        </div>
        <div class="tablehead" id="select_Toolbar_Paging_1">
            <ul class="fixed">
                <asp:PlaceHolder runat="server" ID="ph_Add">
                    <li><a href="RuleAdd.aspx?sl=<%=Request.QueryString["sl"]%>" hidefocus="true" class="toolbar_add add_gg">
                        <s class="addicon"></s><span>新增</span></a> </li>
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
                    <th width="318" align="center" class="th-line">
                        制度编号
                    </th>
                    <th width="1055" align="center" class="th-line">
                        制度标题
                    </th>
                    <th width="1055" align="center" class="th-line">
                        发布部门
                    </th>
                    <th width="1055" align="center" class="th-line">
                        适用部门
                    </th>
                    <th width="1055" align="center" class="th-line">
                        发布人
                    </th>
                    <th width="1055" align="center" class="th-line">
                        发布时间
                    </th>
                </tr>
                <asp:Repeater ID="RepList" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td align="center">
                                <input type="checkbox" name="checkbox" id="checkbox" value="<%#Eval("RegId")%>" />
                            </td>
                            <td align="center">
                                <%#Eval("Code")%>
                            </td>
                            <td align="center">
                                <a href="RuleShow.aspx?id=<%#Eval("RegId")%>&sl=<%=Request.QueryString["sl"]%>" class="ck_zd">
                                    <%#Eval("Title")%></a><%# GetUrl(Eval("RegId").ToString())%>
                            </td>
                            <td align="center">
                                <%#Eval("IssuedDepartName")%>
                            </td>
                            <td align="center">
                                <%#GetApplyDept(Eval("ApplyDeptList"))%>
                            </td>
                            <td align="center">
                                <%#Eval("IssuedName") %>
                                <input type="hidden" name="ItemUserID" value="<%#Eval("OperatorId")%>" />
                            </td>
                            <td align="center">
                                <%#Eval("IssueTime", "{0:yyyy-MM-dd}")%>
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
                    url: '/ManageCenter/Rule',
                    title: "",
                    width: "830px",
                    height: "550px"
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
                            parent.tableToolbar._showMsg(result.msg, function() {
                                $("#btnSubmit").click();
                            });

                        }
                        else { parent.tableToolbar._showMsg(result.msg); }
                    },
                    error: function() {
                        //ajax异常--你懂得
                        parent.tableToolbar._showMsg(tableToolbar.errorMsg);
                    }
                });
            },
            Add: function() {
                var data = this.DataBoxy();
                data.url += '/RuleAdd.aspx?';
                data.title = '添加规章制度';
                data.url += $.param({
                    sl: this.Query.sl,
                    doType: "add"
                });
                this.ShowBoxy(data);
            },
            Update: function(objsArr) {
                var data = this.DataBoxy();
                data.url += '/RuleAdd.aspx?';
                data.title = '修改规章制度';
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
                var data = this.DataBoxy();
                data.url += "/RuleList.aspx?";
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
                    objectName: "规章制度",
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
            $(".ck_zd").click(function() {
                var url = $(this).attr("href");
                Boxy.iframeDialog({
                    iframeUrl: url,
                    title: "查看详细",
                    modal: true,
                    width: "820px",
                    height: "460px"
                });
                return false;
            });
        })
    </script>

</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Front.Master" AutoEventWireup="true"
    CodeBehind="SubjectManageList.aspx.cs" Inherits="EyouSoft.Web.FinanceManage.KingdeeSubject.SubjectManageList" %>

<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="wrap">
        <div class="mainbox">
            <div class="tablehead">
                <ul class="fixed">
                    <li><s class="orderformicon"></s><a class="ztorderform de-ztorderform" hidefocus="true"
                        href="javascript:void(0);"><span>科目管理</span></a></li>
                    <li><s class="orderformicon"></s><a class="ztorderform" hidefocus="true" href="/FinanceManage/KingdeeSubject/AdjustItemList.aspx?sl=<%= Request.QueryString["sl"]%>">
                        <span>核算项目管理</span></a></li>
                </ul>
            </div>
            <div id="tablehead" class="tablehead">
                <ul class="fixed">
                    <li><s class="addicon"></s><a class="toolbar_add" hidefocus="true" href="javascript:void(0);">
                        <span>添加</span></a></li>
                    <li class="line"></li>
                    <li><s class="updateicon"></s><a class="toolbar_update" hidefocus="true" href="javascript:void(0);">
                        <span>修改</span></a></li>
                    <li class="line"></li>
                    <li><s class="delicon"></s><a class="toolbar_delete" hidefocus="true" href="javascript:void(0)">
                        <span>删除</span></a></li>
                </ul>
                <div class="pages">
                    <cc1:ExporPageInfoSelect ID="ExporPageInfoSelect1" runat="server" />
                </div>
            </div>
            <!--列表表格-->
            <div class="tablelist-box">
                <table width="100%" id="liststyle">
                    <tbody>
                        <tr class="odd">
                            <th width="30" class="thinputbg">
                                <input type="checkbox" id="checkbox" name="checkbox">
                            </th>
                            <th align="center" class="th-line">
                                科目类型
                            </th>
                            <th align="center" class="th-line">
                                科目代码
                            </th>
                            <th align="center" class="th-line">
                                助记码
                            </th>
                            <th align="center" class="th-line">
                                科目名称
                            </th>
                            <th align="center" class="th-line">
                                核算科目
                            </th>
                        </tr>
                        <asp:Repeater ID="prt_list" runat="server">
                            <ItemTemplate>
                                <tr data-subjectid="<%#Eval("SubjectId") %>">
                                    <td align="center">
                                        <input type="checkbox" value="<%#Eval("SubjectId")%>" />
                                    </td>
                                    <td align="center">
                                        <%#Eval("SubjectTyp")%>
                                    </td>
                                    <td align="center">
                                        <%#Eval("SubjectCd")%>
                                    </td>
                                    <td align="center">
                                        <%#Eval("MnemonicCd")%>
                                    </td>
                                    <td align="center">
                                        <%#Eval("SubjectNm")%>
                                    </td>
                                    <td align="center">
                                        <%#Eval("ChkItem")%>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                        <asp:Panel ID="pan_msg" runat="server">
                            <tr align="center">
                                <td colspan="7">
                                    暂无数据!
                                </td>
                            </tr>
                        </asp:Panel>
                    </tbody>
                </table>
            </div>
            <div id="tablehead_clone">
            </div>
        </div>
    </div>

    <script type="text/javascript">
        var SubjectManageList = {
            ShowBoxy: function(data) {/*显示弹窗*/
                Boxy.iframeDialog({
                    iframeUrl: data.url,
                    title: data.title,
                    modal: true,
                    width: data.width,
                    height: data.height
                });
            },
            DataBoxy: function() {/*弹窗默认参数*/
                return {
                    url: "/FinanceManage/KingdeeSubject/SubjectManageEdit.aspx?",
                    title: "科目管理",
                    width: "650px",
                    height: "200px"
                }
            },
            DelAll: function(objArr) {
                var list = [];
                for (var i in objArr) {
                    if (objArr.hasOwnProperty(i)) {
                        list.push(objArr[i].attr("data-subjectid"));
                    }
                }
                $.newAjax({
                    type: "get",
                    cache: false,
                    url: "/FinanceManage/KingdeeSubject/SubjectManageList.aspx?" + $.param({
                        doType: "delete",
                        sl: '<%=EyouSoft.Common.Utils.GetQueryStringValue("sl") %>',
                        ids: list.join(',')
                    }),
                    dataType: "json",
                    success: function(result) {
                        if (result.result == 1) {
                            tableToolbar._showMsg(result.msg, function() {
                                window.location.reload();
                            });
                        }
                        else { tableToolbar._showMsg(result.msg); }
                    },
                    error: function() {
                        //ajax异常--你懂得
                        tableToolbar._showMsg(EnglishToChanges.Ping(arguments[1]));
                    }
                });
            },
            PageInit: function() {
                var that = this;
                tableToolbar.IsHandleElse = false;
                tableToolbar.init({
                    tableContainerSelector: "#liststyle", //表格选择器
                    objectName: "记录",
                    updateCallBack: function(objArr) {/*修改*/

                        var boxyData = that.DataBoxy();

                        boxyData.title += EnglishToChanges.Ping("Update");

                        boxyData.url += $.param({
                            sl: '<%=EyouSoft.Common.Utils.GetQueryStringValue("sl") %>',
                            SubjectId: objArr ? $(objArr[0]).attr("data-subjectid") : ""
                        })
                        that.ShowBoxy(boxyData);
                        return false;
                    },
                    deleteCallBack: function(objArr) {/*删除(批量)*/
                        that.DelAll(objArr);
                    }
                })
                $(".toolbar_add").click(function() {
                    var boxyData = that.DataBoxy();
                    boxyData.title += EnglishToChanges.Ping("Add");
                    boxyData.url += $.param({
                        sl: '<%=EyouSoft.Common.Utils.GetQueryStringValue("sl") %>'
                    })
                    that.ShowBoxy(boxyData);
                    return false;
                });
                $("#tablehead_clone").html($("#tablehead").clone(true).css("border-top", "0 none"));
            }
        }
        $(function() {
            SubjectManageList.PageInit();
        })
    </script>

</asp:Content>

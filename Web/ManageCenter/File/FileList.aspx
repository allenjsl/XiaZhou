<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage/Front.Master"
    CodeBehind="FileList.aspx.cs" Inherits="Web.ManageCenter.File.FileList" %>

<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>
<asp:Content runat="server" ID="Content1" ContentPlaceHolderID="head">
</asp:Content>
<asp:Content runat="server" ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1">
    <div id="mainbox">
        <div class="searchbox fixed">
            <form method="get">
            <span class="searchT">
                <p>
                    字号:<input type="text" id="fontSize" name="fontSize" class="inputtext formsize120"
                        size="35" value="<%=Request.QueryString["fontSize"] %>" />
                    发布单位:<input type="text" id="fbdw" name="fbdw" class="inputtext formsize120" size="35"
                        value="<%=Request.QueryString["fbdw"] %>" />
                    标题:<input type="text" id="txttitle" name="txttitle" class="inputtext formsize120"
                        size="35" value="<%=Request.QueryString["txttitle"] %>" />
                    <input type="hidden" name="sl" value='<%=Request.QueryString["sl"]%>' />
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
        <div class="tablelist-box">
            <table width="100%" id="liststyle">
                <tr>
                    <th width="35" align="center" class="thinputbg">
                        <input type="checkbox" name="checkbox" />
                    </th>
                    <th width="100" align="center" class="th-line">
                        字号
                    </th>
                    <th width="250" align="center" class="th-line">
                        文件发布单位
                    </th>
                    <th width="177" align="center" class="th-line">
                        文件标题
                    </th>
                    <th width="134" align="center" class="th-line">
                        经办人
                    </th>
                    <th width="168" align="center" class="th-line">
                        审批/审阅人
                    </th>
                    <th width="168" align="center" class="th-line">
                        状态
                    </th>
                </tr>
                <asp:Repeater runat="server" ID="rptFileList">
                    <ItemTemplate>
                        <tr>
                            <td width="35" align="center" class="thinputbg">
                                <input type="checkbox" name="checkbox" value="<%#Eval("DocumentsId") %>" />
                            </td>
                            <td width="340" align="center" class="th-line">
                                <%#Eval("FontSize") %>
                            </td>
                            <td width="341" align="center" class="th-line">
                                <%#Eval("Company")%>
                            </td>
                            <td width="177" align="center" class="th-line">
                                <%#Eval("Title")%>
                            </td>
                            <td width="134" align="center" class="th-line">
                                <%#Eval("AttnName")%>
                            </td>
                            <td width="168" align="center" class="th-line">
                                <strong>
                                    <%#GetSells(Eval("GovDocumentsApproveList"))%></strong>
                            </td>
                            <td width="168" align="center" class="th-line">
                                <%#GetStatus(Eval("FileType"), Eval("ApproveState"))%>
                                <input type="hidden" name="ItemUserID" value="<%#Eval("OperatorId")%>" />
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
        </div>
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
                    url: '/ManageCenter/File',
                    title: "",
                    width: "650px",
                    height: "250px"
                }
            },
            ShowBoxy: function(data) {
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
            //新增
            Add: function() {
                var data = this.DataBoxy();
                data.title = "新增文件";
                data.url += "/FileAdd.aspx?";
                data.url += $.param({
                    sl: this.Query.sl,
                    doType: "add"
                })
                this.ShowBoxy(data);
            },
            //修改
            Update: function(objsArr) {
                var data = this.DataBoxy();
                data.title = "修改";
                data.url += "/FileAdd.aspx?";
                data.url += $.param({
                    sl: this.Query.sl,
                    doType: "update",
                    id: objsArr[0].find('input[type="checkbox"]').val()
                });
                this.ShowBoxy(data);
            },
            //删除
            Delete: function(objsArr) {
                var list = new Array();
                for (var i = 0; i < objsArr.length; i++) {
                    list.push(objsArr[i].find("input[type='checkbox']").val());
                }
                var data = this.DataBoxy();
                data.url += "/FileList.aspx?";
                data.url += $.param({
                    sl: this.Query.sl,
                    doType: "delete",
                    id: list.join(",")
                });
                this.GoAjax(data.url);
            },
            //功能按钮初始化
            BindBtn: function() {
                $(".add_gg").click(function() {
                    PageJsDataObj.Add();
                    return false;
                })
                tableToolbar.init({
                    tableContainerSelector: "#liststyle", //表格选择器
                    objectName: "文件", //这个参数讲不明白参考tableToolbar.js
                    //默认按钮
                    //修改-删除 为默认按钮，按钮class对应  toolbar_update  toolbar_delete 即可
                    updateCallBack: function(objsArr) {
                        //修改
                        PageJsDataObj.Update(objsArr);
                        return false;
                    },
                    deleteCallBack: function(objsArr) {
                        //删除(批量)
                        PageJsDataObj.Delete(objsArr);

                    }
                })
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
            $(".approve").click(function() {
                var data = PageJsDataObj.DataBoxy();
                data.url += "/FileApprove.aspx?";
                data.url += $.param({
                    sl: PageJsDataObj.Query.sl,
                    id: $(this).attr("data-id")
                })
                data.title = "文件审批"
                data.width = "630px";
                data.height = "260px";
                PageJsDataObj.ShowBoxy(data);
                PageJsDataObj.BindClose();
                return false;
            })
        })
        
    </script>

</asp:Content>

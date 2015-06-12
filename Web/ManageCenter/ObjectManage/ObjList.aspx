<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ObjList.aspx.cs" Inherits="Web.ManageCenter.ObjectManage.ObjList"
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
                    物品名称：<input type="text" class="inputtext formsize120" name="txtName" id="txtName"
                        value='<%=Request.QueryString["txtName"]%>' />
                    入库时间：<input type="text" name="txtInSTime" id="txtInSTime" class="inputtext formsize120"
                        onfocus="WdatePicker()" value='<%=Request.QueryString["txtInSTime"]%>' />-
                    <input type="text" name="txtInETime" id="txtInETime" class="inputtext formsize120"
                        onfocus="WdatePicker()" value='<%=Request.QueryString["txtInETime"]%>' />
                    <input type="hidden" name="sl" value='<%=Request.QueryString["sl"] %>' />
                    <button type="submit" id="btnSubmit" class="search-btn">
                        搜索</button></p>
            </span>
            </form>
        </div>
        <div class="tablehead" id="select_Toolbar_Paging_1">
            <ul class="fixed">
                <asp:PlaceHolder runat="server" ID="ph_Add">
                    <li><s class="addicon"></s><a href="javascript:void(0);" hidefocus="true" class="add_gg">
                        <span>入库</span></a> </li>
                </asp:PlaceHolder>
                <asp:PlaceHolder runat="server" ID="ph_Update">
                    <li class="line"></li>
                    <li><s class="updateicon"></s><a href="javascript:void(0);" hidefocus="true" class="toolbar_update">
                        <span>修改</span></a> </li>
                </asp:PlaceHolder>
                <asp:PlaceHolder runat="server" ID="ph_Delete">
                    <li class="line"></li>
                    <li><s class="delicon"></s><a href="javascript:void(0);" hidefocus="true" class="toolbar_delete">
                        <span>删除</span></a> </li>
                </asp:PlaceHolder>
                <asp:PlaceHolder runat="server" ID="ph_Get">
                    <li class="line"></li>
                    <li><s class="lyicon"></s><a href="javascript:void(0);" hidefocus="true" class="toolbar_ly">
                        <span>领用</span></a> </li>
                </asp:PlaceHolder>
                <asp:PlaceHolder runat="server" ID="ph_Send">
                    <li class="line"></li>
                    <li><s class="fficon"></s><a href="javascript:void(0);" hidefocus="true" class="toolbar_ff">
                        <span>发放</span></a> </li>
                </asp:PlaceHolder>
                <asp:PlaceHolder runat="server" ID="ph_Lend">
                    <li class="line"></li>
                    <li><s class="jieyicon"></s><a href="javascript:void(0);" hidefocus="true" class="toolbar_jy">
                        <span>借阅</span></a> </li>
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
                    <th class="thinputbg">
                        <input type="checkbox" name="checkbox" id="checkbox" />
                    </th>
                    <th align="center" class="th-line">
                        物品名称
                    </th>
                    <th align="center" class="th-line">
                        物品数量
                    </th>
                    <th align="center" class="th-line">
                        入库时间
                    </th>
                    <th align="center" class="th-line">
                        领用数量
                    </th>
                    <th align="center" class="th-line">
                        发放数量
                    </th>
                    <th align="center" class="th-line">
                        借阅数量
                    </th>
                    <th align="center" class="th-line">
                        剩余数量
                    </th>
                    <th align="center" class="th-line">
                        登记人
                    </th>
                </tr>
                <asp:Repeater ID="RepList" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td align="center">
                                <input type="checkbox" name="checkbox" id="checkbox" value="<%#Eval("GoodId")%>"
                                    data-name='<%#Eval("Name") %>' data-price='<%#Eval("Price") %>' />
                            </td>
                            <td align="center">
                                <%#Eval("Name")%>
                            </td>
                            <td align="center">
                                <%#Eval("Number")%>
                            </td>
                            <td align="center">
                                <%#Eval("Time", "{0:yyyy-MM-dd}")%>
                            </td>
                            <td align="center">
                                <a href="javascript:void(0)" class="CollarNumber" data-goodid='<%#Eval("GoodId") %>'>
                                    <%#Eval("CollarNumber")%></a>
                            </td>
                            <td align="center">
                                <a href="javascript:void(0)" class="GrantNumber" data-goodid='<%#Eval("GoodId") %>'>
                                    <%#Eval("GrantNumber")%></a>
                            </td>
                            <td align="center">
                                <a href="javascript:void(0)" class="BorrowNumber" data-goodid='<%#Eval("GoodId") %>'>
                                    <%#Eval("BorrowNumber")%></a>
                            </td>
                            <td align="center">
                                <%# Eval("Stock")%>
                                <input type="hidden" name="ItemUserID" value="<%#Eval("OperatorId")%>" />
                            </td>
                            <td align="center">
                                <%#Eval("Operator")%>
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
                    url: '/ManageCenter/ObjectManage',
                    title: "",
                    width: "700px",
                    height: "200px"
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
            BindClose: function() {
                $("a[data-class='a_close']").unbind().click(function() {
                    window.location.reload();
                    return false;
                })
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
            //添加
            Add: function() {
                var data = this.DataBoxy();
                data.url += '/ObjAdd.aspx?';
                data.title = '物品入库';
                data.url += $.param({
                    sl: this.Query.sl,
                    doType: "add"
                });
                this.ShowBoxy(data);
            },
            //修改
            Update: function(objsArr) {
                var data = this.DataBoxy();
                data.url += '/ObjAdd.aspx?';
                data.title = '物品修改';
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
                data.url += "/ObjList.aspx?";
                data.url += $.param({
                    sl: this.Query.sl,
                    doType: "delete",
                    id: list.join(",")
                });
                this.GoAjax(data.url);
            },
            //领用
            Collar: function(objsArr) {
                var list = new Array();
                var names = new Array();
                var prices = new Array();
                for (var i = 0; i < objsArr.length; i++) {
                    list.push(objsArr[i].find("input[type='checkbox']").val());
                    names.push(objsArr[i].find("input[type='checkbox']").attr("data-name"));
                    prices.push(objsArr[i].find("input[type='checkbox']").attr("data-price"));
                }
                var data = this.DataBoxy();
                data.url += '/ObjGetAdd.aspx?';
                data.title = '物品领用';
                data.url += $.param({
                    sl: this.Query.sl,
                    doType: "objget",
                    id: list.join(','),
                    names: names.join(','),
                    prices: prices.join(',')
                });
                data.width = "500px";
                data.height = "310px";
                this.ShowBoxy(data);
            },
            //发放
            Grant: function(objsArr) {
                var list = new Array();
                var names = new Array();
                var prices = new Array();
                for (var i = 0; i < objsArr.length; i++) {
                    list.push(objsArr[i].find("input[type='checkbox']").val());
                    names.push(objsArr[i].find("input[type='checkbox']").attr("data-name"));
                    prices.push(objsArr[i].find("input[type='checkbox']").attr("data-price"));
                }
                var data = this.DataBoxy();
                data.url += '/ObjSendAdd.aspx?';
                data.title = '物品发放';
                data.url += $.param({
                    sl: this.Query.sl,
                    doType: "objsend",
                    id: list.join(','),
                    names: names.join(','),
                    prices: prices.join(',')
                });
                data.width = "500px";
                data.height = "310px";
                this.ShowBoxy(data);
            },
            //物品借阅
            Borrow: function(objsArr) {
                var list = new Array();
                var names = new Array();
                var prices = new Array();
                for (var i = 0; i < objsArr.length; i++) {
                    list.push(objsArr[i].find("input[type='checkbox']").val());
                    names.push(objsArr[i].find("input[type='checkbox']").attr("data-name"));
                    prices.push(objsArr[i].find("input[type='checkbox']").attr("data-price"))
                }
                var data = this.DataBoxy();
                data.url += '/ObjLendAdd.aspx?';
                data.title = '物品借阅';
                data.url += $.param({
                    sl: this.Query.sl,
                    doType: "objlend",
                    id: list.join(','),
                    names: names.join(','),
                    prices: prices.join(',')
                });
                data.width = "500px";
                data.height = "310px";
                this.ShowBoxy(data);
            },
            BindBtn: function() {
                $(".add_gg").click(function() {
                    PageJsDataObj.Add();
                    return false;
                })
                tableToolbar.init({
                    tableContainerSelector: "#liststyle", //表格选择器
                    objectName: "物品",
                    //修改
                    updateCallBack: function(objsArr) {
                        PageJsDataObj.Update(objsArr);
                        return false;
                    },
                    //删除
                    deleteCallBack: function(objsArr) {
                        PageJsDataObj.Delete(objsArr);
                    },
                    //领用
                    otherButtons: [
                    {
                        button_selector: '.toolbar_ly',
                        sucessRulr: 2, msg: '未选中任何物品',
                        buttonCallBack: function(objsArr) {
                            PageJsDataObj.Collar(objsArr); return false;
                        }
                    },
                    {
                        button_selector: '.toolbar_ff',
                        sucessRulr: 2, msg: '未选中任何物品',
                        buttonCallBack: function(objsArr) {
                            PageJsDataObj.Grant(objsArr);
                            return false;
                        }
                    },
                    {
                        button_selector: '.toolbar_jy',
                        sucessRulr: 2,
                        msg: '未选中任何物品',
                        buttonCallBack: function(objsArr) {
                            PageJsDataObj.Borrow(objsArr); return false;
                        }
                    }
]
                });
            },
            PageInit: function() {
                //绑定功能按钮
                this.BindBtn();
                //当列表页面出现横向滚动条时使用以下方法 $("需要滚动最外层选择器").moveScroll();
                $('.tablelist-box').moveScroll();
            },
            CollarNumberPage: function(obj) {
                var goodId = $(obj).attr("data-goodid");
                var data = this.DataBoxy();
                data.url += '/ObjGetList.aspx?';
                data.url += $.param({ sl: this.Query.sl, goodId: goodId });
                data.title = '物品领用详细';
                data.width = "790px";
                data.height = "420px";
                this.ShowBoxy(data);
            },
            GrantNumberPage: function(obj) {
                var goodId = $(obj).attr("data-goodid");
                var data = this.DataBoxy();
                data.url += '/ObjSendList.aspx?';
                data.url += $.param({ sl: this.Query.sl, goodId: goodId });
                data.title = '物品发放详细';
                data.width = "790px";
                data.height = "420px";
                this.ShowBoxy(data);
            },
            BorrowNumberPage: function(obj) {
                var goodId = $(obj).attr("data-goodid");
                var data = this.DataBoxy();
                data.url += '/ObjLendList.aspx?';
                data.url += $.param({ sl: this.Query.sl, goodId: goodId });
                data.title = '物品借阅详细';
                data.width = "790px";
                data.height = "420px";
                this.ShowBoxy(data);
                this.BindClose();
            }
        }
        $(function() {
            PageJsDataObj.BindBtn();
            $(".CollarNumber").click(function() {
                PageJsDataObj.CollarNumberPage(this);
                return false;
            })
            $(".GrantNumber").click(function() {
                PageJsDataObj.GrantNumberPage(this);
                return false;
            })
            $(".BorrowNumber").click(function() {
                PageJsDataObj.BorrowNumberPage(this);
                return false;
            })
        })               
    </script>

    <script src="/Js/datepicker/WdatePicker.js" type="text/javascript"></script>

</asp:Content>

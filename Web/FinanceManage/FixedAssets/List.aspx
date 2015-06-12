<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Front.Master" AutoEventWireup="true"
    CodeBehind="List.aspx.cs" Inherits="Web.FinanceManage.FixedAssets.List" %>

<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>
<%@ Register Src="/UserControl/SelectSection.ascx" TagName="SelectSection" TagPrefix="uc1" %>
<%@ Register Src="/UserControl/SellsSelect.ascx" TagName="SellsSelect" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script src="/Js/datepicker/WdatePicker.js" type="text/javascript"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="mainbox">
        <form id="SelectFrom" action="List.aspx" accept="get">
        <div class="searchbox fixed">
            <span class="searchT">
                <p>
                    编号：<input type="text" name="txt_Id" class="inputtext formsize80" value="<%= Request.QueryString["txt_Id"]%>" />
                    资产名称：<input type="text" name="txt_propertyName" class="inputtext formsize140" value="<%= Request.QueryString["txt_propertyName"]%>" />
                    使用部门：<uc1:SelectSection ID="SelectSection1" runat="server" />
                    管理责任人：<uc1:SellsSelect ID="SellsSelect1" selectfrist="false" runat="server" />
                    <input type="submit" class="search-btn" /></p>
            </span>
        </div>
        <input type="hidden" name="sl" value="<%=Request.QueryString["sl"] %>" />
        </form>
        <div id="tablehead" class="tablehead">
            <ul class="fixed">
                <asp:PlaceHolder ID="pan_Add" runat="server">
                    <li><s class="addicon"></s><a href="javascript:void(0);" hidefocus="true" class="toolbar_add">
                        <span>新增</span></a></li><li class="line"></li>
                </asp:PlaceHolder>
                <asp:PlaceHolder ID="pan_updata" runat="server">
                    <li><s class="updateicon"></s><a href="javascript:void(0);" hidefocus="true" class="toolbar_update">
                        <span>修改</span></a></li><li class="line"></li>
                </asp:PlaceHolder>
                <asp:PlaceHolder ID="pan_delete" runat="server">
                    <li><s class="delicon"></s><a href="javascript:void(0);" hidefocus="true" class="toolbar_delete">
                        <span>删除</span></a></li><li class="line"></li>
                </asp:PlaceHolder>
                <li><s class="dayin"></s><a id="a_print" href="javascript:void(0);" hidefocus="true"
                    class="toolbar_dayin"><span>打印</span></a></li>
                <li class="line"></li>
                <li><s class="daochu"></s><a href="javascript:void(0);" id="ToXls" hidefocus="true"
                    class="toolbar_daochu"><span>导出</span></a></li>
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
                        编号
                    </th>
                    <th align="center" class="th-line">
                        资产名称
                    </th>
                    <th align="center" class="th-line">
                        购买时间
                    </th>
                    <th align="center" class="th-line">
                        原始价值
                    </th>
                    <th align="center" class="th-line">
                        折旧年限
                    </th>
                    <th align="center" class="th-line">
                        使用部门
                    </th>
                    <th align="center" class="th-line">
                        管理责任人
                    </th>
                </tr>
                <asp:Repeater ID="rpt_list" runat="server">
                    <ItemTemplate>
                        <tr data-val="<%#Eval("Id") %>">
                            <td align="center">
                                <input type="checkbox" name="checkbox" />
                            </td>
                            <td align="center">
                                <%#Eval("AssetCode")%>
                            </td>
                            <td align="center">
                                <%#Eval("AssetName")%>
                            </td>
                            <td align="center">
                                <%# EyouSoft.Common.UtilsCommons.GetDateString(Eval("BuyTime"),ProviderToDate)%>
                            </td>
                            <td align="center">
                                <%#EyouSoft.Common.UtilsCommons.GetMoneyString(Eval("BuyPrice"),ProviderToMoney)%>
                            </td>
                            <td align="center">
                                <%#Eval("DepreciableLife") %>
                            </td>
                            <td align="center">
                                <%#Eval("Department")%>
                            </td>
                            <td align="center">
                                <%#Eval("Admin")%>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
                <asp:Panel ID="pan_Msg" runat="server">
                    <tr align="center">
                        <td colspan="8">
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
        var FixedAssetsList = {
            //显示弹窗
            ShowBoxy: function(data) {
                Boxy.iframeDialog({
                    iframeUrl: data.url,
                    title: data.title,
                    modal: true,
                    width: data.width,
                    height: data.height
                });
            },
            //弹窗默认参数
            DataBoxy: function() {
                return {
                    url: "/FinanceManage/FixedAssets",
                    title: "固定资产",
                    width: "600px",
                    height: "240px"
                }
            },
            //添加
            Add: function() {
                var data = this.DataBoxy();
                data.title += EnglishToChanges.Ping("Add");
                data.url += "/Add.aspx?sl=" + '<%=Request.QueryString["sl"] %>';
                this.ShowBoxy(data);
                return false;
            },

            //绑定功能按钮
            BindBtn: function() {
                var that = this;
                $(".toolbar_add").click(function() {

                    that.Add();
                })
                tableToolbar.init({
                    tableContainerSelector: "#liststyle", //表格选择器
                    objectName: "记录",
                    updateCallBack: function(objsArr) {
                        //修改
                        var data = that.DataBoxy();
                        data.title += EnglishToChanges.Ping("Update");
                        data.url += "/Add.aspx?" + $.param({ sl: '<%=Request.QueryString["sl"] %>', id: objsArr[0] ? $(objsArr[0]).attr("data-val") : 0 });
                        that.ShowBoxy(data);
                        return false;
                    },
                    deleteCallBack: function(objsArr) {
                        var ids = []
                        $(objsArr).each(function() {
                            ids.push($(this).attr("data-val"));
                        })
                        //删除(批量)
                        var url = "/FinanceManage/FixedAssets/List.aspx?" + $.param({
                            doType: "del",
                            sl: '<%=Request.QueryString["sl"] %>',
                            Ids: ids.join(',')
                        })
                        $.newAjax({
                            type: "get",
                            cache: false,
                            url: url,
                            dataType: "json",
                            success: function(ret) {
                                if (parseInt(ret.result) === 1) {
                                    parent.tableToolbar._showMsg('删除成功!', function() {
                                        location.href = location.href;
                                    });

                                }
                                else {
                                    parent.tableToolbar._showMsg(ret.msg);
                                    that.BindBtn();
                                }
                            },
                            error: function() {
                                //ajax异常--你懂得
                                parent.tableToolbar._showMsg(EnglishToChanges.Ping(arguments[1]));
                                that.BindBtn();
                            }
                        });
                        return false;
                    }
                })
            }
        }
        $(function() {
            //绑定功能按钮
            FixedAssetsList.BindBtn();
            $("#ToXls").click(function() {
                toXls1();
                return false;
            })
            $("#a_print").click(function() {
                PrintPage("#a_print");
                return false;
            })
            $("#tablehead_clone").html($("#tablehead").clone(true).css("border-top", "0 none"));
        })
       
    </script>

</asp:Content>

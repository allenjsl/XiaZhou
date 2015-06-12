<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="YuFuQueRen.aspx.cs" Inherits="Web.MarketCenter.YuFuQueRen"
    MasterPageFile="~/MasterPage/Front.Master" %>

<%--销售中心-预付确认--%>
<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>
<asp:Content ID="MainBodyContent" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="mainbox">
        <div class="tablehead" id="select_Toolbar_Paging_1">
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
                    <th align="center" class="th-line">
                        预付单位
                    </th>
                    <th align="center" class="th-line">
                        预付类别
                    </th>
                    <th align="right" class="th-line">
                        预付金额
                    </th>
                    <th align="center" class="th-line">
                        用途说明
                    </th>
                    <th align="center" class="th-line">
                        最晚付款日期
                    </th>
                    <th align="center" class="th-line">
                        状态
                    </th>
                    <th align="center" class="th-line">
                        销售员
                    </th>
                    <th align="center" class="th-line">
                        计调员
                    </th>
                    <th align="center" class="th-line">
                        操作
                    </th>
                </tr>
                <asp:Repeater runat="server" ID="rpt">
                    <ItemTemplate>
                        <tr data-id="<%#Eval("RegisterId")%>">
                            <td align="center">
                                <%#Eval("TourCode")%>
                            </td>
                            <td align="left">
                                <%#Eval("Supplier")%>
                            </td>
                            <td align="center">
                                <%#Eval("PlanTyp")%>
                            </td>
                            <td align="right">
                                <b class="fontred">
                                    <%#EyouSoft.Common.UtilsCommons.GetMoneyString(Eval("PayAmount"), ProviderToMoney)%></b>
                            </td>
                            <td align="left">
                                <%#Eval("Remark")%>
                            </td>
                            <td align="center">
                                <%# EyouSoft.Common.UtilsCommons.GetDateString(Eval("PayExpire"), ProviderToDate)%>
                            </td>
                            <td align="center">
                                <%#Eval("Status")%>
                            </td>
                            <td align="center">
                                <%#Eval("SellerName")%>
                            </td>
                            <td align="center">
                                <%#Eval("Planer")%>
                            </td>
                            <td align="center" width="50px;">
                                <%#Eval("Status").ToString() == "销售待确认" ? "<a href=\"#\" class=\"a_ExamineV\">审核</a>" : "已审"%>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
                <asp:Panel ID="pan_Msg" runat="server" Visible = "false">
                    <tr align="center">
                        <td colspan="10">
                            暂无数据!
                        </td>
                    </tr>
                </asp:Panel>
            </table>
        </div>
        <!--列表结束-->
        <div class="tablehead" style="border-top: 0 none;" id="select_Toolbar_Paging_2">
        </div>
    </div>

    <script type="text/javascript">
        var PageJsDataObj = {
            //提交
            Submit: function(obj, type) {
                var that = this;
                var obj = $(obj);
                var txt = obj.text()
                obj.unbind("click");
                obj.css("color", "#A9A9A9");
                obj.text(txt + "中...");
                var url = "YuFuQueRen.aspx?";
                url += $.param({
                    doType: type,
                    sl: '<%=EyouSoft.Common.Utils.GetQueryStringValue("sl") %>',
                    RegisterId: obj.closest("tr").attr("data-id")
                });
                
                $.newAjax({
                    type: "post",
                    cache: false,
                    url: url,
                    dataType: "json",
                    success: function(result) {
                        if (result.result == 'True') {
                            tableToolbar._showMsg(result.msg);
                            setTimeout(function() {
                                location.href = location.href;
                            }, 1000)
                        }
                        else {
                            tableToolbar._showMsg(result.msg);
                            that.InitListBtn();
                        }
                    },
                    error: function() {
                        tableToolbar._showMsg("服务器忙！");
                        that.InitListBtn();
                    }
                });
                return false;
            },
            InitListBtn: function() {/*初始化列表按钮*/
                var that = this;
                var obj = $(".a_ExamineV");
                obj.css("color", "");
                obj.text("审核");
                obj.unbind("click").click(function() {
                    that.Submit(this, "ShenHe");
                    return false;
                })
            },
            PageInit: function() {
                this.InitListBtn();
            }

        }
        $(function() {
            PageJsDataObj.PageInit();
            $("#select_Toolbar_Paging_1").children().clone(true).prependTo("#select_Toolbar_Paging_2");
        })
    </script>

</asp:Content>

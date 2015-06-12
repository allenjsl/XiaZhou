<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Front.Master" AutoEventWireup="true"
    CodeBehind="List.aspx.cs" Inherits="Web.FinanceManage.Loan.List" %>

<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>
<%@ Register Src="/UserControl/selectGuid.ascx" TagName="SellsSelect" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="mainbox">
        <form id="SelectFrom" action="List.aspx" method="get">
        <div class="searchbox fixed">
            <span class="searchT">
                <p>
                    团号：<input type="text" name="txt_teamNumber" value="<%=Request.QueryString["txt_teamNumber"] %>"
                        class="inputtext formsize120" />
                    借款人：<uc1:SellsSelect ID="txt_Seller" runat="server" SetTitle="借款人" SelectFrist="false" />
                    <input type="submit" id="submit_select" class="search-btn" /></p>
            </span>
        </div>
        <input type="hidden" name="sl" value="<%=Request.QueryString["sl"] %>" />
        <input type="hidden" id="hd_verificated" name="verificated" value="<%=Request.QueryString["verificated"] ??"-1"%>" />
        </form>
        <div id="tablehead" class="tablehead" style="border-top: 0 none;">
            <ul class="fixed">
                <li data-class="li_verificated" data-value="-1"><s class="orderformicon"></s><a data-class="a_isDealt"
                    hidefocus="true" data-value="-1" class="ztorderform" href="javascript:void(0);">
                    <span>未核销</span></a></li>
                <li data-class="li_verificated" data-value="1"><s class="orderformicon"></s><a href="javascript:void(0);"
                    hidefocus="true" data-class="a_isDealt" data-value="1" class="ztorderform"><span>已核销</span></a></li>
            </ul>
            <div id="pages" class="pages">
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
                        团号
                    </th>
                    <th align="center" class="th-line">
                        借款人
                    </th>
                    <th align="center" class="th-line">
                        借款日期
                    </th>
                    <th align="center" class="th-line">
                        预借金额
                    </th>
                    <th align="center" class="th-line">
                        <span class="th-line h20">实借金额</span>
                    </th>
                    <th align="center" class="th-line">
                        预领签单数
                    </th>
                    <th align="center" class="th-line">
                        实领签单数
                    </th>
                    <th align="center" class="th-line">
                        是否核销
                    </th>
                    <th align="center" class="th-line">
                        操作
                    </th>
                </tr>
                <asp:Repeater ID="rpt_list" runat="server">
                    <ItemTemplate>
                        <tr data-debitid="<%#Eval("Id") %>" data-tourid="<%#Eval("TourId") %>">
                            <td align="center">
                                <input type="checkbox" name="checkbox" />
                            </td>
                            <td align="center">
                                <%#Eval("TourCode")%>
                            </td>
                            <td align="center">
                                <%#Eval("Borrower")%>
                            </td>
                            <td align="center">
                                <%#EyouSoft.Common.UtilsCommons.GetDateString(Eval("BorrowTime"), ProviderToDate)%>
                            </td>
                            <td align="center">
                                <b class="fontgreen">
                                    <%#EyouSoft.Common.UtilsCommons.GetMoneyString(Eval("BorrowAmount"), ProviderToMoney)%></b>
                            </td>
                            <td align="center">
                                <b class="fontbsize12">
                                    <%#EyouSoft.Common.UtilsCommons.GetMoneyString(Eval("RealAmount"), ProviderToMoney)%></b>
                            </td>
                            <td align="center">
                                <%#Eval("PreSignNum")%>
                            </td>
                            <td align="center">
                                <%#Eval("RelSignNum")%>
                            </td>
                            <td align="center" class="<%#Eval("IsVerificated").ToString().ToLower()=="true"?"":"fontred"%>">
                                <b>
                                    <%=EyouSoft.Common.Utils.GetQueryStringValue("verificated") == "1" ? "已核销" : "未核销"%></b>
                            </td>
                            <td align="center" data-intstatus="<%#(int)Eval("Status") %>">
                                <a data-class="a_ExamineV" href="javascript:void(0);">审核 </a><a data-class="a_Pay"
                                    href="javascript:void(0);">
                                    <%#((int)Eval("Status")) != (int)EyouSoft.Model.EnumType.FinStructure.FinStatus.账务已支付 ? "支付" : this.GetFinIn(Eval("Id"))%>
                                </a>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
                <asp:Panel ID="pan_msg" runat="server">
                    <tr align="center">
                        <td colspan="10">
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
        var LoanList = {
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
                    url: "/FinanceManage/Loan",
                    title: "借款管理",
                    width: "650px",
                    height: "200px"
                }
            },
            Pay: function(Id, intstatus, tourid) {/*支付*/
                var data = this.DataBoxy();
                data.title += EnglishToChanges.Ping("Pay");
                data.height = "250px";
                data.url += ("/Pay.aspx?" + $.param({
                    debitID: Id,
                    verificated:'<%=Request.QueryString["verificated"] %>',
                    sl: '<%=EyouSoft.Common.Utils.GetQueryStringValue("sl") %>',
                    intstatus: intstatus,
                    tourId: tourid
                }));
                this.ShowBoxy(data);
            },
            ExamineA: function(Id) { /*审核*/
                var data = this.DataBoxy();
                data.title += EnglishToChanges.Ping("ExamineA");
                data.url += ("/ExamineA.aspx?" + $.param({
                    debitID: Id,
                    verificated:'<%=Request.QueryString["verificated"] %>',
                    sl: '<%=EyouSoft.Common.Utils.GetQueryStringValue("sl") %>'
                }));
                this.ShowBoxy(data);
            },
            BindBtn: function() {
                var that = this;
                $("#liststyle a[data-class='a_ExamineV']").click(function() {
                    that.ExamineA($(this).closest("tr").attr("data-debitid"));
                    return false;
                })
                $("#liststyle a[data-class='a_Pay']").click(function() {
                    var obj = $(this);
                    that.Pay(obj.closest("tr").attr("data-debitid"), obj.closest("td").attr("data-intstatus"), obj.closest("tr").attr("data-tourid"));
                    return false;
                })
                $("li[data-class='li_verificated']").click(function() {
                    $("#hd_verificated").val($(this).attr("data-value"));
                    $("#submit_select").click();
                    return false;
                })
            },
            PageInit: function() {
                tableToolbar.init({});
                var that = this;
                var isdefaul = '<%=Request.QueryString["verificated"]??"-1" %>';
                $("a[data-class='a_isDealt']").each(function() {
                    if ($(this).attr("data-value") == isdefaul) {
                        $(this).addClass("de-ztorderform");
                    }
                    else {
                        $(this).removeClass("de-ztorderform");
                    }
                })
                var intstatus;
                $("#liststyle td[data-intstatus]").each(function() {
                    var obj = $(this)
                    switch (parseInt(obj.attr("data-intstatus"))) {
                        case 0:
                            obj.find("[data-class='a_Pay']").remove();
                            break;
                        case 1:
                            obj.find("[data-class='a_ExamineV']").remove();
                            break;
                        case 2:
                            obj.find("[data-class='a_ExamineV']").remove();
                            if ('<%=IsEnableKis %>' == 'False') {
                                $(this).find("a[data-class='a_Pay']").html("查看");
                            }
                            break;
                        default:
                            obj.find("[data-class='a_ExamineV']").remove();
                            obj.find("[data-class='a_Pay']").remove();
                            break;
                    }
                })

                that.BindBtn();
                $("#tablehead_clone").html($("#tablehead").clone(true).css("border-top", "0 none"));
            }
        }
        $(function() {
            LoanList.PageInit();

        })
    </script>

</asp:Content>

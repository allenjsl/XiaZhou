<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Front.Master" AutoEventWireup="true"
    CodeBehind="OverrunList.aspx.cs" Inherits="Web.FinanceManage.Arrearage.OverrunList" %>

<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>
<%@ Register Src="/UserControl/SellsSelect.ascx" TagName="SellsSelect" TagPrefix="uc1" %>
<%@ Register Src="/UserControl/CustomerUnitSelect.ascx" TagName="CustomerUnitSelect"
    TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script src="/Js/datepicker/WdatePicker.js" type="text/javascript"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="mainbox">
        <div class="tablehead" style="background: none #f6f6f6;">
            <ul class="fixed">
                <li><s class="orderformicon"></s><a href="ClientList.aspx?sl=<%=Request.QueryString["sl"] %>"
                    hidefocus="true" class="ztorderform"><span>客户预警</span></a></li>
                <li><s class="orderformicon"></s><a href="SellList.aspx?sl=<%=Request.QueryString["sl"] %>"
                    hidefocus="true" class="ztorderform"><span>销售预警</span></a></li>
                <li><s class="orderformicon"></s><a href="javascript:void(0);" hidefocus="true" class="ztorderform de-ztorderform">
                    <span>超限审批</span></a></li>
            </ul>
        </div>
        <form id="SelectFrom" action="OverrunList.aspx" method="get">
        <div class="searchbox fixed">
            <span class="searchT">
                <p>
                    线路名称：<input type="text" name="txt_lineName" class="inputtext formsize120" value="<%=Request.QueryString["txt_lineName"]  %>" />
                    <uc2:CustomerUnitSelect ID="CustomerUnitSelect1" runat="server" />
                    申请人：<uc1:SellsSelect ID="SellsSelect1" runat="server" SelectFrist="false" />
                    申请时间：
                    <input type="text" name="ApplyTimeS" class="inputtext formsize80" onclick="WdatePicker()"
                        value="<%=Request.QueryString["ApplyTimeS"] %>" />
                    -
                    <input type="text" name="ApplyTimeE" class="inputtext formsize80" onclick="WdatePicker()"
                        value="<%=Request.QueryString["ApplyTimeE"] %>" />
                    审批状态：<select name="txtStatus" id="txtStatus" class="inputselect">
                        <option value="">请选择</option>
                        <option value="0">未审批</option>
                        <option value="1">已通过</option>
                        <option value="2">未通过</option>
                    </select><br />
                    
                    审批人：<uc1:SellsSelect ID="txtShenPiRen" runat="server" SelectFrist="false" />
                    审批时间：
                    <input type="text" name="txtShenPiSTime" class="inputtext formsize80" onclick="WdatePicker()"
                        value="<%=Request.QueryString["txtShenPiSTime"] %>" />
                    -
                    <input type="text" name="txtShenPiETime" class="inputtext formsize80" onclick="WdatePicker()"
                        value="<%=Request.QueryString["txtShenPiETime"] %>" />
                    
                    <input type="submit" class="search-btn" /></p>
            </span>
        </div>
        <input type="hidden" name="sl" value="<%=Request.QueryString["sl"] %>" />
        </form>
        <div class="tablehead" id="tablehead" style="border-top: 0 none;">
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
                        线路名称
                    </th>
                    <th align="center" class="th-line">
                        客户单位
                    </th>
                    <th align="center" class="th-line">
                        申请人
                    </th>
                    <th align="center" class="th-line">
                        申请时间
                    </th>
                    <th align="right" class="th-line">
                        申请金额
                    </th>
                     <th align="center" class="th-line">
                        审批人
                    </th>
                    <th align="center" class="th-line">
                        审批时间
                    </th>
                    <th align="center" class="th-line">
                        操作
                    </th>
                </tr>
                <asp:Repeater ID="rpt_list" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td align="center">
                                <input type="checkbox" name="checkbox" id="checkbox" />
                            </td>
                            <td align="center">
                                <%#Eval("RouteName")%>
                            </td>
                            <td align="center">
                                <%#Eval("Crm")%>
                            </td>
                            <td align="center">
                                <%#Eval("Applier")%>
                            </td>
                            <td align="center">
                                <%#Eval("ApplyTime","{0:yyyy-MM-dd HH:mm}")%>
                            </td>
                            <td align="right" class="fonthei">
                                <b class="<%#(int)Eval("IsApprove")==1?"":"fontred" %>"><a href="<%#GetChaKanLink(Eval("ItemType"),Eval("TourId"),Eval("OrderId"),Eval("TourType")) %>" target="_blank"><%#EyouSoft.Common.UtilsCommons.GetMoneyString(Eval("DisburseAmount") ?? 0, ProviderToMoney)%></a></b>
                            </td>
                            <td align="center">
                                <%#Eval("Approver")%>
                            </td>
                            <td align="center">
                                <%#Eval("ApproveTime", "{0:yyyy-MM-dd HH:mm}")%>
                            </td>                            
                            <td align="center" data-isapprove="<%#(int)((EyouSoft.Model.EnumType.FinStructure.TransfiniteStatus)Eval("IsApprove")) %>">
                                <%if (IsHaveCheckPriv)
                                  { %>
                                <input type="hidden" name="hideData" value="<%#(int)Eval("ItemType") %>,<%#Eval("ItemId") %>,<%#Eval("DisburseId") %>,<%#Eval("CrmId") %>,<%#Eval("ApproverId") %>,<%#Eval("Applier") %>,<%#Eval("DisburseAmount") %>,<%#Eval("ApplyTime") %>"
                                    data-remark="<%#Eval("Remark") %>" />
                                <%#(EyouSoft.Model.EnumType.FinStructure.TransfiniteStatus)Eval("IsApprove") == EyouSoft.Model.EnumType.FinStructure.TransfiniteStatus.未审批 ?"<a href='javascript:void(0);' data-class='a_ExamineA'>审批</a>":""%>
                                <%}
                                  else
                                  {%>
                                <%#(EyouSoft.Model.EnumType.FinStructure.TransfiniteStatus)Eval("IsApprove") == EyouSoft.Model.EnumType.FinStructure.TransfiniteStatus.未审批 ? "<span>未审批</span>" : ""%>
                                <%} %>
                                <%#(EyouSoft.Model.EnumType.FinStructure.TransfiniteStatus)Eval("IsApprove") == EyouSoft.Model.EnumType.FinStructure.TransfiniteStatus.通过 ? "<span>已审批</span>" : ""%>
                                <%#(EyouSoft.Model.EnumType.FinStructure.TransfiniteStatus)Eval("IsApprove") == EyouSoft.Model.EnumType.FinStructure.TransfiniteStatus.未通过 ? "<span>未通过</span>" : ""%>
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
            </table>
        </div>
        <!--列表结束-->
        <div class="tablehead" style="border-top: 0 none;">

            <script type="text/javascript">
                document.write(document.getElementById("tablehead").innerHTML);
            </script>

        </div>
    </div>

    <script type="text/javascript">
        var PageJsDateObj = {
            ShowBoxy: function(data) {/*显示弹窗*/
                Boxy.iframeDialog({
                    iframeUrl: data.url,
                    title: data.title,
                    modal: true,
                    width: data.width,
                    height: data.height
                });
            },
            ExamineA: function(obj) {/*审批*/
                var obj = $(obj), hide = obj.parent().find("input[type='hidden']");
                var list = hide.val().split(','), remark = hide.attr("data-remark");
                this.ShowBoxy({
                    url: "/FinanceManage/Arrearage/ExamineA.aspx?" + $.param({
                        sl: '<%=EyouSoft.Common.Utils.GetQueryStringValue("sl") %>',
                        disburseid: list[2], //垫付id
                        itemtype: list[0], //0：报价成功/1：成团/2：报名
                        itemid: list[1], //报价编号/团队编号/订单编号
                        crmId: list[3], //客户单位ID
                        approverId: list[4], //销售员id=申请人id
                        approvername: list[5], //销售员name=申请人name
                        disburseamount: list[6], //垫付金额
                        applytime: list[7], //申请时间
                        remark: remark//备注
                    }),
                    title: "财务管理-超限审批-" + EnglishToChanges.Ping("ExamineA"),
                    width: "750px",
                    height: "360px"
                })
            },
            Bt: function() {/*泡泡提示*/
                var obj = $(obj);
                $(".bt-wrapper").html("");
                //公司名称
                var firmName = obj.text();
                //联系人姓名
                var linkName = obj.attr("linkName");
                //联系电话
                var linkTel = obj.attr("linkTel");
                //拼接提示文本
                var cont = "<b>" + firmName + "</b><br/>";
                cont += "联系人：" + linkName + "<br/>";
                cont += "联系方式：" + linkTel;
                obj.bt({
                    contentSelector: function() {
                        return cont;
                    },
                    positions: ['left', 'right', 'bottom'],
                    fill: '#FFF2B5',
                    strokeStyle: '#D59228',
                    noShadowOpts: { strokeStyle: "#D59228" },
                    spikeLength: 10,
                    spikeGirth: 15,
                    width: 200,
                    overlap: 0,
                    centerPointY: 1,
                    cornerRadius: 4,
                    shadow: true,
                    shadowColor: 'rgba(0,0,0,.5)',
                    cssStyles: { color: '#00387E', 'line-height': '180%' }
                });
            },
            PageInit: function() {/*页面初始化*/
                tableToolbar.init({});
                var that = this;
                $("a[data-class='a_ExamineA']").click(function() {
                    that.ExamineA(this);
                    return false;
                })
            }
        }
        $(function() {
            PageJsDateObj.PageInit();
            $("#txtStatus").val('<%=EyouSoft.Common.Utils.GetQueryStringValue("txtStatus") %>');
            setInterval("FlushThisPage()", 300000);
        })
        function FlushThisPage() {
            location.reload();
        }       
    </script>

</asp:Content>

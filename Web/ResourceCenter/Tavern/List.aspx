<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Front.Master" AutoEventWireup="true"
    CodeBehind="List.aspx.cs" Inherits="Web.ResourceCenter.Tavern.List" %>

<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script src="/Js/datepicker/WdatePicker.js" type="text/javascript"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="mainbox">
        <form id="SelectFrom" action="List.aspx" method="get">
        <div class="searchbox fixed">
            <span class="searchT">
                <p>
                    �Ƶ����ƣ�<input type="text" name="tavName" class="inputtext formsize180" value="<%=Request.QueryString["tavName"] %>" />
                    ���ͣ�
                    <input type="text" id="txt_rType" name="rType" class="inputtext formsize100" value="<%=Request.QueryString["rType"] %>" />
                    ��Ч��
                    <input type="text" name="dateTimeS" class="inputtext formsize80" onclick="WdatePicker()"
                        value="<%=Request.QueryString["dateTimeS"] %>" />
                    -
                    <input type="text" name="dateTimeE" class="inputtext formsize80" onclick="WdatePicker()"
                        value="<%=Request.QueryString["dateTimeE"] %>" />
                    <input type="submit" class="search-btn" />
                </p>
            </span>
        </div>
        <input type="hidden" name="sl" value="<%=Request.QueryString["sl"] %>" />
        </form>
        <div id="tablehead" class="tablehead">
            <asp:PlaceHolder ID="pan_Add" runat="server">
                <ul class="fixed">
                    <li id="li_Add"><s class="addicon"></s><a href="javascript:void(0);" hidefocus="true"
                        class="toolbar_plshoukuan"><span>����Ԥ��</span></a></li>
                    <li class="line"></li>
                </ul>
            </asp:PlaceHolder>
            <div class="pages">
                <cc1:ExporPageInfoSelect ID="ExporPageInfoSelect1" runat="server" />
            </div>
        </div>
        <!--�б���-->
        <div class="tablelist-box">
            <table width="100%" id="liststyle">
                <tr>
                    <th class="thinputbg">
                        <input type="checkbox" name="checkbox" id="checkbox1" />
                    </th>
                    <th align="center" class="th-line">
                        �Ƶ�����
                    </th>
                    <th align="center" class="th-line">
                        ����
                    </th>
                    <th align="center" class="th-line">
                        �ط�����
                    </th>
                    <th align="center" class="th-line">
                        Ԥ������
                    </th>
                    <th align="center" class="th-line">
                        ���������
                    </th>
                    <th align="center" class="th-line">
                        ��ʹ������
                    </th>
                    <th align="center" class="th-line">
                        ʣ������
                    </th>
                    <th align="center" class="th-line">
                        ����
                    </th>
                </tr>
                <asp:Repeater ID="rpt_list" runat="server">
                    <ItemTemplate>
                        <tr data-id="<%#Eval("Id") %>">
                            <td align="center">
                                <input type="checkbox" name="checkbox" id="checkbox" />
                            </td>
                            <td align="center">
                                <a href="javascript:void(0);">
                                    <%#Eval("SourceName")%></a>
                            </td>
                            <td align="center">
                                <%#Eval("RoomType")%>
                            </td>
                            <td align="center">
                                <b class="fontred">
                                    <%#Eval("ControlNum")%></b>
                            </td>
                            <td align="center" data-sourcecontroltype="<%#Eval("SourceControlType") %>" data-periodstarttime="<%#EyouSoft.Common.UtilsCommons.GetDateString(Eval("PeriodStartTime"), ProviderToDate)%>">
                                <%#EyouSoft.Common.UtilsCommons.GetDateString(Eval("PeriodStartTime"), ProviderToDate)%>
                                -
                                <%#EyouSoft.Common.UtilsCommons.GetDateString(Eval("PeriodEndTime"), ProviderToDate)%>
                            </td>
                            <td align="center">
                                <%#EyouSoft.Common.UtilsCommons.GetDateString(Eval("LastTime"), ProviderToDate)%>
                            </td>
                            <td align="center">
                                <a href="javascript:();" class="a_USED"><b>
                                    <%#Eval("AlreadyNum")%></b></a>
                            </td>
                            <td align="center">
                                <b class="fontgreen">
                                    <%#(int)Eval("ControlNum") - (int)Eval("AlreadyNum")%></b>
                            </td>
                            <td align="center">
                                <a href="javascript:void(0);" class="a_updata"><span>Ԥ�ر��</span></a> <a href="<%=YKQRDPrintUri %>?sueId=<%#Eval("Id") %>"
                                    target="_blank">Ԥ��ȷ�ϵ�</a>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
                <asp:Panel ID="pan_Msg" runat="server">
                    <tr align="center">
                        <td colspan="14">
                            ��������!
                        </td>
                    </tr>
                </asp:Panel>
            </table>
        </div>
        <div id="tablehead_clone">
        </div>
    </div>

    <script type="text/javascript">
        var PageJsDataObj = {
            ShowBoxy: function(data) {/*��ʾ����*/
                Boxy.iframeDialog({
                    iframeUrl: data.url,
                    title: data.title,
                    modal: true,
                    width: data.width,
                    height: data.height
                });
            },
            DataBoxy: function() {/*��������*/
                return {
                    url: "/ResourceCenter/CommonPage",
                    title: "�Ƶ�Ԥ��",
                    width: "700px",
                    height: "310px"
                }
            },
            Add: function() {/*��ӵ���*/
                var data = this.DataBoxy();
                data.title += EnglishToChanges.Ping("Add");
                data.url = "/ResourceCenter/Tavern/Add.aspx?" + $.param({ sl: Boxy.queryString("sl") });
                this.ShowBoxy(data);
                return false;
            },
            USED: function(obj) {/*��ʹ����������*/
                var data = this.DataBoxy();
                data.title += "-��ʹ������-";
                data.url += "/USED.aspx?" + $.param({
                    sl: Boxy.queryString("sl"),
                    sourceControlCategory: '<%=(int)EyouSoft.Model.EnumType.SourceStructure.SourceControlCategory.�Ƶ�%>',
                    sueId: $(obj).closest("tr").attr("data-id")
                });
                data.height = "250px";
                this.ShowBoxy(data);
                return false;
            },
            BindBtn: function() {
                var that = this;
                tableToolbar.init({/*�󶨹��ܰ�ť*/
                    tableContainerSelector: "#liststyle" //���ѡ����
                })
                $("#li_Add").click(function() {
                    that.Add();
                    return false;
                })
                $(".a_updata").click(function() {
                    var data = that.DataBoxy();
                    data.title += "-Ԥ�ر��-";
                    data.url += "/Updata.aspx?" + $.param({
                        sl: Boxy.queryString("sl"),
                        sourceControlCategory: '<%=(int)EyouSoft.Model.EnumType.SourceStructure.SourceControlCategory.�Ƶ�%>',
                        sourceId: $(this).closest("tr").attr("data-id")
                    });
                    data.height = "250px";
                    that.ShowBoxy(data);
                    return false;
                })
                $(".a_USED").click(function() {
                    that.USED(this);
                    return false;
                })
            },
            PageInit: function() {/*ҳ���ʼ��*/
                /*�󶨹��ܰ�ť*/
                this.BindBtn();
                $("td[data-sourcecontroltype='<%=EyouSoft.Model.EnumType.SourceStructure.SourceControlType.�ܿ�%>']").each(function() {
                    var obj = $(this);
                    obj.html(obj.attr("data-periodstarttime"));
                })
                $("#tablehead_clone").html($("#tablehead").clone(true).css("border-top", "0 none"));
            }
        }

        $(function() {
            //��ʼ��ҳ��
            PageJsDataObj.PageInit();
        })

        
        
    </script>

</asp:Content>

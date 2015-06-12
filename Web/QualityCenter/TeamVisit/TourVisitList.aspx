<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TourVisitList.aspx.cs"
    Inherits="EyouSoft.Web.QualityCenter.TeamVisit.TourVisitList" %>

<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="/css/style.css" rel="stylesheet" type="text/css" />
    <link href="/css/boxynew.css" rel="stylesheet" type="text/css" />

    <script src="/Js/datepicker/WdatePicker.js" type="text/javascript"></script>

    <script src="/Js/jquery-1.4.4.js" type="text/javascript"></script>

    <!--[if IE]><script src="/js/excanvas.js" type="text/javascript" charset="utf-8"></script><![endif]-->

    <script src="/Js/jquery.blockUI.js" type="text/javascript"></script>

    <script src="/Js/table-toolbar.js" type="text/javascript"></script>

    <script src="/Js/bt.min.js" type="text/javascript"></script>

</head>
<body style="background: 0 none;">
    <div class="alertbox-outbox" style="height: 100%">
        <form method="get">
        <table width="99%" height="100%" align="center" cellpadding="0" cellspacing="0" bgcolor="#e9f4f9">
            <tr>
                <td width="89%" height="28" align="left" bgcolor="#C1E5F5">
                    <span class="alertboxTableT">回访时间：</span>
                    <input name="txtCrmStime" type="text" class="inputtext formsize80" id="txtCrmStime"
                        onfocus="WdatePicker({maxDate:'#F{$dp.$D(\'txtCrmEtime\')}'})" value='<%=Request.QueryString["txtCrmStime"]%>' />
                    -
                    <input name="txtCrmEtime" type="text" class="inputtext formsize80" id="txtCrmEtime"
                        onfocus="WdatePicker({minDate:'#F{$dp.$D(\'txtCrmStime\')}'})" value='<%=Request.QueryString["txtCrmEtime"]%>' />
                    <button type="submit" style="width: 64px; height: 24px; background: url(/images/cx.gif) no-repeat center center;
                        border: 0 none; margin-left: 5px;">
                        查 询</button>
                </td>
            </tr>
        </table>
        <input type="hidden" name="sl" value='<%=Request.QueryString["sl"]%>' />
        <input type="hidden" name="id" value='<%=Request.QueryString["id"]%>' />
        </form>
        <div class="hr_5">
        </div>
        <table width="99%" border="0" align="center" cellpadding="0" cellspacing="0" id="liststyle">
            <tr>
                <th align="center" height="28" bgcolor="#B7E0F3" class="alertboxTableT">
                    回访类型
                </th>
                <th align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                    被访人身份
                </th>
                <th align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                    被访人
                </th>
                <th align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                    被访人电话
                </th>
                <th align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                    回访均分
                </th>
            </tr>
            <asp:Repeater ID="RepList" runat="server">
                <ItemTemplate>
                    <tr>
                        <td align="center" height="28">
                            <%#Eval("ReturnType")%>
                        </td>
                        <td align="center">
                            <%#Eval("Identity")%>
                        </td>
                        <td align="center">
                            <%#Eval("Name")%>
                        </td>
                        <td align="center">
                            <%#Eval("Telephone")%>
                        </td>
                        <td align="center">
                            <a class="a_info" href="VisitForm.aspx?doType=chakan&visitId=<%#Eval("VisitId")%>&tourId=<%=Request.QueryString["id"]%>">
                                <%#Eval("QualityScore")%></a><span style="display: none;">团队总评：<%#Eval("Total")%>
                                </span>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </table>
        <div class="pages">
            <cc1:ExporPageInfoSelect ID="ExporPageInfoSelect1" runat="server" />
        </div>
        <div class="hr_10">
        </div>
    </div>

    <script type="text/javascript">
        var PageJsData = {
            Query: {/*URL参数对象*/
                sl: '<%=Request.QueryString["sl"] %>'
            },
            DataBoxy: function() {/*弹窗默认参数*/
                return {
                    url: '/ManageCenter/Notice',
                    title: "",
                    width: "748px",
                    height: "445px"
                }
            },
            ShowBoxy: function(data) {/*显示弹窗*/
                var win = top || window;
                win.Boxy.iframeDialog({
                    iframeUrl: data.url,
                    title: data.title,
                    modal: true,
                    width: data.width,
                    height: data.height
                });
            },
            Bt: function(obj) {
                $(obj).bt({
                    contentSelector: function() {
                        return $(this).next().html();
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
                })
            }
        }
        $(function() {
            $(".a_info").click(function() {
                var data =  PageJsData.DataBoxy();
                data.url = $(this).attr("href") + "&";
                data.url += $.param({ sl: PageJsData.Query.sl });
                data.title = "回访";
                data.width = "880px";
                data.height = "308px";
                PageJsData.ShowBoxy(data);
                return false;
            });
            $('.a_info').each(function() {
                PageJsData.Bt(this);
            });
        })
    </script>

</body>
</html>

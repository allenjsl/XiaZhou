<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Front.Master" AutoEventWireup="true"
    CodeBehind="HuiYuanWenDa.aspx.cs" Inherits="EyouSoft.Web.YouLun.HuiYuanWenDa" %>

<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>
<%@ Register Src="~/YouLun/WUC/wuc1.ascx" TagName="wuc1" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="mainbox">
        <uc1:wuc1 runat="server" ID="wuc1" />
        <div class="tablehead" id="temptablehead">
            <ul class="fixed">
            </ul>
            <div class="pages">
                <cc1:ExporPageInfoSelect ID="paging" runat="server" />
            </div>
        </div>
        <!--列表表格-->
        <div class="tablelist-box">
            <table width="100%" id="liststyle">
                <tr class="odd">
                    <th class="thinputbg">
                        <input type="checkbox" id="checkbox" name="checkbox" />
                    </th>
                    <th class="th-line" style="text-align: left;">
                        产品名称
                    </th>
                    <th class="th-line" style="text-align: left;">
                        类型
                    </th>
                    <th class="th-line" style="text-align: left;" width="700px">
                        内容
                    </th>
                    <th class="th-line" style="text-align: left;">
                        时间
                    </th>
                    <th class="th-line" style="text-align: left;">
                        提问人
                    </th>
                    <th class="th-line" style="text-align: left;">
                        操作
                    </th>
                </tr>
                <asp:Repeater ID="rptlist" runat="server">
                    <ItemTemplate>
                        <tr class="odd">
                            <td class="thinputbg" style="text-align: center;">
                                <input type="checkbox" id="checkbox" name="checkbox" value="">
                            </td>
                            <td class="th-line">
                                <%# getCPMC(Eval("HangQiId").ToString())%>
                            </td>
                            <td class="th-line">
                                <%#Eval("LeiXing")%>
                            </td>
                            <td class="th-line">
                                <%#Eval("WenNeiRong")%>
                            </td>
                            <td class="th-line">
                                <%#Eval("WenShiJian", "{0:yyyy-MM-dd HH:mm}")%>
                            </td>
                            <td class="th-line">
                                <%# getDPR(Eval("WenYongHuId").ToString())%>
                            </td>
                            <td class="th-line">
                                <%# getOpt(Eval("DaNeiRong"), Eval("WenDaId").ToString())%>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
                <asp:PlaceHolder runat="server" ID="phEmpty">
                    <tr>
                        <td colspan="50" align="center">
                            暂无数据。
                        </td>
                    </tr>
                </asp:PlaceHolder>
            </table>
        </div>
        <!--列表结束-->
        <div class="tablehead" style="border: none;">

            <script type="text/javascript">
                document.write(document.getElementById("temptablehead").innerHTML);
            </script>

        </div>
    </div>

    <script type="text/javascript">
        var pageDataJs = {
            HuiFu: function(objid) {
                url = '/youlun/huifubox.aspx?sl=99&id=' + objid;
                Boxy.iframeDialog({
                    iframeUrl: url,
                    title: "问答回复",
                    modal: true,
                    width: "550",
                    height: "200"
                });
            }
        };

        $(document).ready(function() {
             wuc1("3");
        });
    </script>

</asp:Content>

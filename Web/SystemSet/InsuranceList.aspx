<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Front.Master" AutoEventWireup="true"
    CodeBehind="InsuranceList.aspx.cs" Inherits="EyouSoft.Web.SystemSet.InsuranceList" %>

<%@ Register Assembly="ControlLibrary" Namespace="ControlLibrary" TagPrefix="cc1" %>
<%@ Register Src="../UserControl/BasciSetBar.ascx" TagName="BasciSetBar" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="mainbox">
        <uc1:BasciSetBar ID="BasciSetBar1" runat="server" />
        <div class="tablehead">
            <ul class="fixed">
                <li><s class="addicon"></s><a class="baoxian_add" hidefocus="true" href="javascript:">
                    <span>添加</span></a></li>
                <li class="line"></li>
                <li><a class="toolbar_delete" hidefocus="true" href="#"><s class="delicon"></s><span>
                    删除</span> </a></li>
            </ul>
        </div>
        <div class="tablelist-box">
            <table width="100%" align="center" id="liststyle">
                <tbody>
                    <tr class="odd">
                        <th class="thinputbg" style="width: 40px">
                            <input type="checkbox" id="checkbox" name="checkbox">
                        </th>
                        <th width="264" align="left" class="th-line">
                            保险名称
                        </th>
                        <th width="236" align="right" class="th-line">
                            单价
                        </th>
                        <th width="249" align="center" class="th-line">
                            操作
                        </th>
                        <th width="40" align="center">
                            &nbsp;
                        </th>
                        <th width="264" align="left" class="th-line">
                            保险名称
                        </th>
                        <th width="236" align="right" class="th-line">
                            单价
                        </th>
                        <th width="249" align="center" class="th-line">
                            操作
                        </th>
                    </tr>
                    <tr>
                        <cc1:CustomRepeater ID="repList" runat="server">
                            <ItemTemplate>
                                <td align="left">
                                    <input type="checkbox" value="<%#Eval("InsuranceId")%>" name="chk">
                                    <%#Container.ItemIndex+1 %>
                                </td>
                                <td align="left">
                                    <%#Eval("InsuranceName")%>
                                </td>
                                <td align="right">
                                    <b class="fontblue">
                                        <%#EyouSoft.Common.UtilsCommons.GetMoneyString(Eval("UnitPrice"), this.ProviderToMoney)%></b>
                                </td>
                                <td align="center">
                                    <a class="baoxian_update" target="_blank" href="/SystemSet/InsuranceEdit.aspx?id=<%#Eval("InsuranceId")%>&sl=<%=EyouSoft.Common.Utils.GetQueryStringValue("sl") %>">
                                        修改</a>&nbsp; |&nbsp;<a href="javascript:" onclick="return InsurancelList.Del('<%#Eval("InsuranceId")%>')">
                                            删除</a>
                                    <%#EyouSoft.Common.Utils.IsOutTrOrTd(Container.ItemIndex,i,2,4) %>
                            </ItemTemplate>
                        </cc1:CustomRepeater>
                </tbody>
            </table>
        </div>
        <div class="tablehead">
            <ul class="fixed">
                <li><s class="addicon"></s><a class="baoxian_add" hidefocus="true" href="javascript:">
                    <span>添加</span></a></li>
                <li class="line"></li>
                <li><a class="toolbar_delete" hidefocus="true" href="#"><s class="delicon"></s><span>
                    删除</span> </a></li>
            </ul>
        </div>
    </div>

    <script type="text/javascript">
        var InsurancelList = {
            SL: querystring(location.href, "sl"),
            memuid: querystring(location.href, "memuid"),
            GetSelectItemValue: function() {
                var arrayList = new Array();
                $("#liststyle").find("input[name='chk']").each(function() {
                    if ($(this).attr("checked") == true) {
                        arrayList.push($(this).attr("value"));
                    }
                });
                return arrayList;
            },

            Del: function(ids) {
                //删除
                $.newAjax({
                    type: "GET",
                    async: false,
                    url: "/SystemSet/InsuranceList.aspx?ids=" + ids + "&state=del" + "&sl=" + InsurancelList.SL + "&memuid=" + InsurancelList.memuid,
                    dataType: "json",
                    success: function(ret) {
                        tableToolbar._showMsg(ret.msg, function() { window.location.href = "/SystemSet/InsuranceList.aspx?sl=" + InsurancelList.SL + "&memuid=" + InsurancelList.memuid });

                    },
                    error: function() {
                        tableToolbar._showMsg(ret.msg);
                    }
                });
                return false;
            },

            Init: function() {
                tableToolbar.IsHandleElse = "true";
                tableToolbar.init({
                    tableContainerSelector: "#liststyle",
                    objectName: " 记录",
                    //默认按钮
                    //修改-删除-取消-复制 为默认按钮，按钮class对应  toolbar_update  toolbar_delete  toolbar_cancel  toolbar_copy即可
                    deleteCallBack: function(objsArr) {
                        //删除
                        InsurancelList.Del(InsurancelList.GetSelectItemValue().toString());
                    }
                });
                $(".baoxian_add").click(function() {
                    var url = "InsuranceEdit.aspx?sl=" + InsurancelList.SL;
                    InsurancelList.openXLwindow(url, "新增保险类型", "480px", "130px");
                    return false;
                });
                $(".baoxian_update").click(function() {
                    var url = $(this).attr("href");
                    InsurancelList.openXLwindow(url, "修改保险类型", "480px", "130px");
                    return false;
                });
            },

            openXLwindow: function(url, title, width, height) {
                url = url;
                Boxy.iframeDialog({
                    iframeUrl: url,
                    title: title,
                    modal: true,
                    width: width,
                    height: height
                });
            }
        }
        $(function() {
            InsurancelList.Init();
        });
    </script>

</asp:Content>

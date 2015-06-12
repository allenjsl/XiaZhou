<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Front.Master" AutoEventWireup="true"
    CodeBehind="RouterAreaList.aspx.cs" Inherits="Web.SystemSet.RouterAreaList" %>

<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc2" %>
<%@ Register Assembly="ControlLibrary" Namespace="ControlLibrary" TagPrefix="cc1" %>
<%@ Register Src="../UserControl/BasciSetBar.ascx" TagName="BasciSetBar" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form id="form1" runat="server">
    <div class="mainbox">
        <uc1:BasciSetBar ID="BasciSetBar1" runat="server" />
        <div class="tablehead">
            <ul class="fixed">
                <li><s class="addicon"></s><a class="add_xlqy" hidefocus="true" href="javascript:"><span>
                    添加</span></a></li><li class="line"></li>
                <li><a class="toolbar_delete" hidefocus="true" href="javascript:"><s class="delicon">
                </s><span>删除</span></a></li>
            </ul>
            <div class="pages">
                <cc2:ExporPageInfoSelect ID="ExporPageInfoSelect1" runat="server" />
            </div>
        </div>
        <!--列表表格-->
        <div class="tablelist-box">
            <table width="100%" id="liststyle">
                <tbody>
                    <tr class="odd">
                        <th width="30" class="thinputbg">
                            <input type="checkbox" id="checkbox" name="checkbox">
                        </th>
                        <th width="190" align="center" class="th-line">
                            区域名称
                        </th>
                        <th width="249" align="center" class="th-line">
                            计调员
                        </th>
                        <th width="400" align="center" class="th-line">
                            区域类别
                        </th>
                        <th width="328" align="center" class="th-line">
                            操作
                        </th>
                    </tr>
                    <cc1:CustomRepeater ID="repList" runat="server">
                        <ItemTemplate>
                            <tr class="<%#Container.ItemIndex%2==0?"":"#odd" %>">
                                <td align="center">
                                    <input type="checkbox" value="<%#Eval("AreaId")%>" name="chk">
                                </td>
                                <td align="center">
                                    <%#Eval("AreaName")%>
                                </td>
                                <td align="center">
                                    <%#GetMComAreaPlan(Eval("Plan")) %>
                                </td>
                                <td align="center">
                                    <%#Eval("Type")%>
                                </td>
                                <td align="center">
                                    <a class="update_xlqy" href="RouterAreaEdit.aspx?&id=<%#Eval("AreaId") %>">修改</a>
                                    | <a href="javascript:" onclick="return RouterAreaList.Del('<%#Eval("AreaId")%>');">
                                        删除</a>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </cc1:CustomRepeater>
                </tbody>
            </table>
        </div>
        <!--列表结束-->
        <div style="border: 0 none;" class="tablehead">
            <ul class="fixed">
                <li><s class="addicon"></s><a class="toolbar_add add_xlqy" hidefocus="true" href="javascript:">
                    <span>添加</span></a></li>
                <li class="line"></li>
                <li><a class="toolbar_delete" hidefocus="true" href="javascript:"><s class="delicon">
                </s><span>删除</span></a></li>
            </ul>
            <div class="pages">
                <cc2:ExporPageInfoSelect ID="ExporPageInfoSelect2" runat="server" />
            </div>
        </div>
    </div>
    </form>

    <script type="text/javascript">
        var RouterAreaList = {
            Params: { memuid: querystring(location.href, "memuid"), sl: querystring(location.href, "sl") },
            GetSelectCount: function() {
                var count = 0;
                $("#liststyle").find("input[name='chk']").each(function() {
                    if ($(this).attr("checked") == true) {
                        count++;
                    }
                });
                return count;
            },

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
                    url: "RouterAreaList.aspx?ids=" + ids + "&state=del&" + $.param(RouterAreaList.Params),
                    dataType: "json",
                    success: function(ret) {
                        tableToolbar._showMsg(ret.msg, function() { window.location.href = "/SystemSet/RouterAreaList.aspx?&" + $.param(RouterAreaList.Params) });

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
                        RouterAreaList.Del(RouterAreaList.GetSelectItemValue().toString());
                    }
                });
                $(".add_xlqy").click(function() {
                    var url = "RouterAreaEdit.aspx?&" + $.param(RouterAreaList.Params);
                    RouterAreaList.openXLwindow(url, "添加线路区域", "500px", "163px");
                    return false;
                });
                $(".update_xlqy").click(function() {
                    var url = $(this).attr("href") + "&" + $.param(RouterAreaList.Params);
                    RouterAreaList.openXLwindow(url, "修改线路区域", "500px", "163px");
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
            RouterAreaList.Init();
        });
    </script>

</asp:Content>

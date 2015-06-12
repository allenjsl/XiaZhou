<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PreControlOther.aspx.cs" Inherits="EyouSoft.Web.UserCenter.WorkAwake.Preview.PreControlOther" MasterPageFile="~/MasterPage/UserCenter.Master" %>

<%@ Register Src="../../../UserControl/UserCenterNavi.ascx" TagName="UserCenterNavi"
    TagPrefix="uc1" %>
<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<script src="/Js/ValiDatorForm.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form id="form1" runat="server">
    <div class="grzxtabelbox">
        <div class="list_btn basicbg_01">
            <uc1:UserCenterNavi ID="UserCenterNavi1" runat="server" />
        </div>
        <div class="tablehead">
            <ul class="fixed">
                <li><s class="orderformicon"></s><a class="ztorderform" hidefocus="true" href="/UserCenter/WorkAwake/Preview/PreviewHotel.aspx?sl=<%=(int)EyouSoft.Model.EnumType.PrivsStructure.Menu2.个人中心_事务提醒 %>">
                    <span>酒店</span></a></li>
                <li><s class="orderformicon"></s><a class="ztorderform" hidefocus="true" href="/UserCenter/WorkAwake/Preview/PreviewCar.aspx?sl=<%=(int)EyouSoft.Model.EnumType.PrivsStructure.Menu2.个人中心_事务提醒 %>">
                    <span>车辆</span></a></li>
                <li><s class="orderformicon"></s><a class="ztorderform" hidefocus="true" href="/UserCenter/WorkAwake/Preview/PreviewBoat.aspx?sl=<%=(int)EyouSoft.Model.EnumType.PrivsStructure.Menu2.个人中心_事务提醒 %>">
                    <span>游船</span></a></li>
                <li><s class="orderformicon"></s><a class="ztorderform" hidefocus="true" href="/UserCenter/WorkAwake/Preview/PreControlSight.aspx?sl=<%=(int)EyouSoft.Model.EnumType.PrivsStructure.Menu2.个人中心_事务提醒 %>">
                    <span>景点</span></a></li>
                <li><s class="orderformicon"></s><a class="ztorderform de-ztorderform" hidefocus="true" href="javascript:void(0);">
                    <span>其他</span></a></li>
            </ul>
            <div class="tix-text">
                <b class="fontred">提醒的日期设定：</b>设置最后保留日前
                <input name="txtDay" type="text" class="inputtext formsize40" id="txtDay" valid="required|RegInteger"
                    errmsg="请填写天数|天数必须大于0的整数!" value="<%=GetLastTime() %>" />
                天提醒
                <img src="/images/baocunimg.gif" width="48" height="20" align="top" id="imgbtnsave" /></div>
            <div class="pages">
                <cc1:ExporPageInfoSelect ID="ExporPageInfoSelect1" runat="server" />
            </div>
        </div>
        <!--列表表格-->
        <div class="tablelist-box">
            <table width="100%" id="liststyle">
                <tbody>
                    <tr class="odd">
                        <th class="th-line">
                            序号
                        </th>
                        <th align="center" class="th-line">
                            其他公司
                        </th>
                        <th align="center" class="th-line">
                            支出类别
                        </th>
                        <th align="center" class="th-line">
                            预控数量
                        </th>
                        <th align="center" class="th-line">
                            预控日期
                        </th>
                        <th align="center" class="th-line">
                            最终保留日期
                        </th>
                        <th align="center" class="th-line">
                            已使用数量
                        </th>
                        <th align="center" class="th-line">
                            剩余数量
                        </th>
                    </tr>
                    <asp:Repeater ID="rptList" runat="server">
                        <ItemTemplate>
                            <tr class="<%#Container.ItemIndex%2==0?"":"odd" %>">
                                <td align="center">
                                    <input type="hidden" name="ItemUserID" />
                                    <%# Container.ItemIndex+1%>
                                </td>
                                <td align="center">
                                    <%# Eval("SourceName")%>
                                </td>
                                <td align="center">
                                    &nbsp;<%# Eval("TypeName") %>
                                </td>
                                <td align="center">
                                    <b class="fontred">
                                        <%# Eval("ControlNum")%></b>
                                </td>
                                <td align="center">
                                    <%# EyouSoft.Common.Utils.GetDateTime(Eval("SueStartTime").ToString()).ToShortDateString()%>-<%# EyouSoft.Common.Utils.GetDateTime(Eval("SueEndTime").ToString()).ToShortDateString()%>
                                </td>
                                <td align="center">
                                    <b>
                                        <%# EyouSoft.Common.Utils.GetDateTime(Eval("LastTime").ToString()).ToShortDateString()%></b>
                                </td>
                                <td align="center">
                                    <a class="openInfo" href="javascript:void(0);" ref="RoomUseInfo.aspx?sueId=<%# Eval("Id") %>&sueType=<%=(int)EyouSoft.Model.EnumType.SourceStructure.SourceControlCategory.其他 %>">
                                        <b class="link2">
                                            <%# Eval("AlreadyNum")%></b></a>
                                </td>
                                <td align="center">
                                    <b class="fontgreen">
                                        <%# EyouSoft.Common.Utils.GetInt(Eval("ControlNum").ToString()) - EyouSoft.Common.Utils.GetInt(Eval("AlreadyNum").ToString())%>
                                    </b>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>
        </div>
        <!--列表结束-->
        <div style="width: 100%; text-align: center; background-color: #ffffff">
            <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
        </div>
        <div style="border: 0 none;" class="tablehead">
            <div class="pages">
                <cc1:ExporPageInfoSelect ID="ExporPageInfoSelect2" runat="server" />
            </div>
        </div>
    </div>
    </form>

    <script>

     $(function() {
             tableToolbar.init({});
             $("#imgbtnsave").click(function() {
                    var day = $.trim($("#txtDay").val());
                    if(ValiDatorForm.validator($(this).closest("form").get(0), "parent")){
                        $.newAjax({
                            type: "post",
                            cache: false,
                            dataType:"json",
                            url: "PreControlOther.aspx?doType=SetLastTime&argument=" + day,
                            success: function(ret) {
                                tableToolbar._showMsg(ret.msg);
                            },
                            error: function() {
                                if(arguments[1]!=null)
                                    tableToolbar._showMsg(EnglishToChanges.Ping(arguments[1]));
                                else
                                    tableToolbar._showMsg("服务器忙");
                            }
                        });
                    }
                    return false;
            })
     
        
            $(".openInfo").click(function() {
                var url = $(this).attr("ref");
                Boxy.iframeDialog({
                    iframeUrl: url,
                    title: "已使用数量",
                    modal: true,
                    width: "550px",
                    height: "300px"
                });
                return false;
            })
        })
    </script>

</asp:Content>

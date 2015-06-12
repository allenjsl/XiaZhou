<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Front.Master" AutoEventWireup="true"
    CodeBehind="TourItemList.aspx.cs" Inherits="Web.SystemSet.TourItemList" %>

<%@ Register Assembly="ControlLibrary" Namespace="ControlLibrary" TagPrefix="cc1" %>
<%@ Register Src="../UserControl/BasciSetBar.ascx" TagName="BasciSetBar" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form id="form1" runat="server">
    <div class="mainbox mainbox-whiteback">
        <uc1:BasciSetBar ID="BasciSetBar1" runat="server" />
        <div class="hr_10">
        </div>
        <div class="jd-mainbox fixed">
            <div class="jdcz-menu">
                <ul>
                    <li class="jdczmenu-t jdczmenu-first"><s></s>基础设置</li>
                    <li class="jdczmenu-t jdczmenu-Second"><s></s>包含项目</li>
                    <li><a href="TourItemList.aspx?type=1&itemtype=1&sl=<%=SL %>&memuid=3" itemtype="1">
                        交通</a></li>
                    <li><a href="TourItemList.aspx?type=1&itemtype=2&sl=<%=SL %>&memuid=3" itemtype="2">
                        用车</a></li>
                    <li><a href="TourItemList.aspx?type=1&itemtype=3&sl=<%=SL %>&memuid=3" itemtype="3">
                        住宿</a></li>
                    <li><a href="TourItemList.aspx?type=1&itemtype=4&sl=<%=SL %>&memuid=3" itemtype="4">
                        用餐</a></li>
                    <li><a href="TourItemList.aspx?type=1&itemtype=5&sl=<%=SL %>&memuid=3" itemtype="5">
                        导服</a></li>
                    <li><a href="TourItemList.aspx?type=1&itemtype=6&sl=<%=SL %>&memuid=3" itemtype="6">
                        景点</a></li>
                    <li><a href="TourItemList.aspx?type=1&itemtype=7&sl=<%=SL %>&memuid=3" itemtype="7">
                        保险</a></li>
                    <li><a href="TourItemList.aspx?type=1&itemtype=8&sl=<%=SL %>&memuid=3" itemtype="8">
                        小交通</a></li>
                    <li><a href="TourItemList.aspx?type=1&itemtype=9&sl=<%=SL %>&memuid=3" itemtype="9">
                        综费</a></li>
                    <li><a href="TourItemList.aspx?type=1&itemtype=10&sl=<%=SL %>&memuid=3" itemtype="10">
                        其他</a></li>
                    <li><a href="TourItemList.aspx?type=1&itemtype=11&sl=<%=SL %>&memuid=3" itemtype="11">
                        国内段</a></li>
                    <li><a href="TourItemList.aspx?type=1&itemtype=12&sl=<%=SL %>&memuid=3" itemtype="12">
                        签证</a></li>
                    <li><a href="TourItemList.aspx?type=1&itemtype=13&sl=<%=SL %>&memuid=3" itemtype="13">
                        游船</a></li>
                    <li class="jdczmenu-t jdczmenu-Second"><a href="TourItemList.aspx?type=2&sl=<%=SL %>&memuid=3"
                        types="2">不含项目</a></li>
                    <li class="jdczmenu-t jdczmenu-Second"><a href="TourItemList.aspx?type=3&sl=<%=SL %>&memuid=3"
                        types="3">购物安排</a></li>
                    <li class="jdczmenu-t jdczmenu-Second"><a href="TourItemList.aspx?type=4&sl=<%=SL %>&memuid=3"
                        types="4">儿童安排</a></li>
                    <li class="jdczmenu-t jdczmenu-Second"><a href="TourItemList.aspx?type=5&sl=<%=SL %>&memuid=3"
                        types="5">自费项目</a></li>
                    <li class="jdczmenu-t jdczmenu-Second"><a href="TourItemList.aspx?type=6&sl=<%=SL %>&memuid=3"
                        types="6">注意事项</a></li>
                    <li class="jdczmenu-t jdczmenu-Second"><a href="TourItemList.aspx?type=7&sl=<%=SL %>&memuid=3"
                        types="7">温馨提醒</a></li>
                    <li class="jdczmenu-t jdczmenu-Second"><a href="TourItemList.aspx?type=8&sl=<%=SL %>&memuid=3"
                        types="8">服务标准</a></li>
                </ul>
            </div>
            <!--jdcz-menu 结束-->
            <div class="jdcz-main">
                <div class="addContent-box jd-addContent-box tablelist-box">
                    <table width="100%" cellspacing="0" cellpadding="0" border="0" align="left" class="firsttable autoAdd">
                        <tbody>
                            <tr>
                                <th width="16%" bgcolor="#137CBF" align="center">
                                    序号
                                </th>
                                <th bgcolor="#137CBF" align="center">
                                    <span class="addtableT"></span>
                                </th>
                                <th width="20%" bgcolor="#137CBF" align="center">
                                    操作
                                </th>
                            </tr>
                            <cc1:CustomRepeater ID="repList" runat="server">
                                <ItemTemplate>
                                    <tr>
                                        <td align="center">
                                            <%#Container.ItemIndex+1 %>
                                        </td>
                                        <td>
                                            <%#Eval("Content")%>
                                        </td>
                                        <td align="left">
                                            <a class="update_list" href="TourItemEdit.aspx?sl=<%=SL%>&type=<%=Type%>&itemtype=<%=ItemType%>&memuid=<%=memuid%>&id=<%#Eval("Id") %>">
                                                <img src="/images/y-delupdateicon.gif">
                                                修改</a> <a href="TourItemList.aspx?sl=<%=SL%>&type=<%=Type%>&itemtype=<%=ItemType%>&memuid=<%=memuid%>&state=del&id=<%#Eval("Id") %>">
                                                    <img src="/images/y-delicon.gif">
                                                    删除</a>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </cc1:CustomRepeater>
                            <tr>
                                <td align="center">
                                    &nbsp;
                                </td>
                                <td>
                                    <asp:TextBox ID="txtContent" Style="margin-top: 3px; margin-bottom: 3px;" CssClass=" inputtext formsize600"
                                        Height="80px" TextMode="MultiLine" runat="server"></asp:TextBox>
                                </td>
                                <td align="left">
                                    <asp:LinkButton ID="btnSave" runat="server" OnClick="btnSave_Click"><img width="48" height="20" border="0" src="../images/addimg.gif"/></asp:LinkButton>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>
                <div class="hr_10">
                </div>
            </div>
            <!--右边内容结束-->
        </div>
        <div class="hr_10">
        </div>
    </div>
    </form>

    <script type="text/javascript">
        var TourItemList = {
            title: "<%=title%>",
            //项目小类型序号
            itemtype: <%=ItemType==""?"0":ItemType %>,
            //项目大类型序号
            type: <%=Type %>
        };

        $(".addtableT").html(TourItemList.title);
        $(".update_list").click(function() {
            var url = $(this).attr("href");
            Boxy.iframeDialog({
                iframeUrl: url,
                title: "团队项目修改",
                modal: true,
                width: "600px",
                height: "175px"
            });
            return false;
        });

        $(".jdcz-menu").children("ul").children().each(function() {
            var Aobject = $(this).find("a");
            $(Aobject).removeClass();
            if ($(Aobject).attr("itemtype") == TourItemList.itemtype) {
                $(Aobject).attr("class", "jdapdefault");
            }
            if ($(Aobject).attr("types") == TourItemList.type) {
                $(Aobject).attr("class", "jdapdefault");
            }
        });

        $("#<%=btnSave.ClientID%>").click(function() {
            var val = $("#<%=txtContent.ClientID%>").val();
            if (val.length == 0) {
                tableToolbar._showMsg('内容不能为空');
                return false;
            }
        });
    </script>

</asp:Content>

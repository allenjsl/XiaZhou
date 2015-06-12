<%@ Page Title="" Language="C#"  MasterPageFile="~/MasterPage/UserCenter.Master" AutoEventWireup="true"
    CodeBehind="BirthdayRemind.aspx.cs" Inherits="Web.UserCenter.WorkAwake.BirthdayRemind" %>

<%@ Register Src="../../UserControl/UserCenterNavi.ascx" TagName="UserCenterNavi"
    TagPrefix="uc1" %>
<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server"><script src="/Js/ValiDatorForm.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form id="form1" runat="server">
    <div class="grzxtabelbox">
       <div class="list_btn basicbg_01">
            <uc1:UserCenterNavi ID="UserCenterNavi1" runat="server" />
        </div>
        <div class="tablehead">
            <div style="line-height: 25px; float: left;" class="red">
                三天内的生日列表</div>
        </div>
        <!--列表表格-->
        <div class="tablelist-box">
            <table width="100%" style="margin-bottom: 1px;" id="liststyle">
                <tbody>
                    <tr class="odd">
                        <th width="30" class="th-line">
                            序号
                        </th>
                        <th align="center" class="th-line">
                            类型
                        </th>
                        <th align="center" class="th-line">
                            姓名
                        </th>
                        <th align="center" class="th-line">
                            生日
                        </th>
                        <th align="center" class="th-line">
                            手机
                        </th>
                        <th align="center" class="th-line">
                            单位名称
                        </th>
                    </tr>
                    <asp:Repeater ID="rptList" runat="server">
                        <ItemTemplate>
                            <tr class="">
                                <td align="center">
                                   <%# Container.ItemIndex+1%>
                                </td>
                                <td align="center">
                                    <%# Eval("Type")%>
                                </td>
                                <td align="center">
                                    <%# Eval("Name")%>
                                </td>
                                <td align="center">
                                    <%# Eval("Birthday","{0:yyyy-MM-dd}")%>
                                </td>
                                <td align="center">
                                    <%# Eval("MobilePhone")%>
                                </td>
                                <td align="center">
                                    <%# Eval("CompanyName")%>
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
                <cc1:ExporPageInfoSelect ID="ExporPageInfoSelect1" runat="server" />
            </div>
        </div>
    </div>
    </form>
</asp:Content>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ContactList.aspx.cs" Inherits="Web.ManageCenter.Contact.ContactList"
    MasterPageFile="~/MasterPage/Front.Master" %>

<%@ Register Src="~/UserControl/SelectSection.ascx" TagName="SelectSection1" TagPrefix="uc2" %>
<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="mainbox">
        <div class="searchbox fixed">
            <form method="get">
            <span class="searchT">部门：
                <uc2:SelectSection1 ID="SelectSection1" runat="server" SModel="2" SetTitle="部门选用" />
                姓名：<input type="text" class="inputtext formsize140" size="35" id="txtName" name="txtName"
                    value='<%=Request.QueryString["txtName"] %>' />
                <input type="hidden" name="sl" value='<%=Request.QueryString["sl"]%>' />
                <button type="submit" class="search-btn">
                    搜索</button>
            </span>
            </form>
        </div>
        <div class="tablehead" id="select_Toolbar_Paging_1">
            <ul class="fixed">
                <li><s class="dayin"></s><a href="javascript:window.print()" hidefocus="true" class="toolbar_dayin">
                    <span>打印</span></a></li>
                <li class="line"></li>
                <li><s class="daochu"></s><a href="javascript:void(0)" hidefocus="true" class="toolbar_daochu"
                    onclick="toXls1();return false;"><span>导出</span></a></li>
            </ul>
            <div class="pages">
                <cc1:ExporPageInfoSelect ID="ExporPageInfoSelect1" runat="server" />
            </div>
        </div>
        <!--列表表格-->
        <div class="tablelist-box">
            <table width="100%" id="liststyle">
                <tr>
                    <th width="30" class="thinputbg">
                        <input type="checkbox" name="checkbox" id="checkbox" />
                    </th>
                    <th width="105" align="center" class="th-line">
                        档案编号
                    </th>
                    <th width="105" align="center" class="th-line">
                        姓名
                    </th>
                    <th width="107" align="center" class="th-line">
                        部门
                    </th>
                    <th width="168" align="center" class="th-line">
                        职务
                    </th>
                    <th width="168" align="center" class="th-line">
                        电话
                    </th>
                    <th width="158" align="center" class="th-line">
                        手机
                    </th>
                    <th width="164" align="center" class="th-line">
                        QQ
                    </th>
                </tr>
                <asp:Repeater ID="RepList" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td align="center">
                                <input type="checkbox" name="checkbox" id="checkbox" value="<%#Eval("ID")%>" />
                            </td>
                            <td align="center">
                                <%#Eval("FileNumber")%>
                            </td>
                            <td align="center">
                                <%#Eval("Name")%>
                            </td>
                            <td align="center">
                                <%#Eval("DepartName")%>
                            </td>
                            <td align="center">
                                <%#this.getSectionInfo(Eval("GovFilePositionList"))%>
                            </td>
                            <td align="center">
                                <%#Eval("Contact")%>
                            </td>
                            <td align="center">
                                <%#Eval("Mobile")%>
                            </td>
                            <td align="center">
                                <%#Eval("qq")%>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
        </div>
        <!--列表结束-->
        <div class="tablehead">

            <script type="text/javascript">
                document.write(document.getElementById("select_Toolbar_Paging_1").innerHTML);
			</script>

        </div>
    </div>

    <script type="text/javascript">
        $(function() {
            tableToolbar.init({
                tableContainerSelector: "#liststyle", //表格选择器
                objectName: "通讯信息"
            })
        });
    </script>

</asp:Content>

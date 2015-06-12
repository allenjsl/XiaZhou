<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage/Print.Master"
    ValidateRequest="false" CodeBehind="HrShow.aspx.cs" Inherits="Web.ManageCenter.Hr.HrShow" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PrintC1" runat="server">
    <table width="696" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
            <td align="center">
                <b class="font24">
                    <asp:Label ID="lbName" runat="server"></asp:Label>
                </b>
            </td>
        </tr>
    </table>
    <table width="696" border="0" align="center" cellpadding="0" cellspacing="0" class="list_2">
        <tr>
            <th colspan="5" align="center" bgcolor="#F0F0F0">
                <strong>基本信息</strong>
            </th>
        </tr>
        <tr>
            <td width="33%">
                档案编号：<asp:Label ID="lbNum" runat="server"></asp:Label>
            </td>
            <td width="30%">
                姓&nbsp;&nbsp;&nbsp;&nbsp;名：<asp:Label ID="lbName_" runat="server"></asp:Label>
            </td>
            <td width="21%">
                性&nbsp;&nbsp;&nbsp;&nbsp;别：
                <label>
                    <asp:Label ID="lbSex" runat="server"></asp:Label>
                </label>
            </td>
            <td width="16%" rowspan="5">
                <asp:Label runat="server" ID="lbImg"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                身份证号：<asp:Label ID="lbIDCard" runat="server"></asp:Label>
            </td>
            <td>
                出生日期：<asp:Label ID="lbBirth" runat="server"></asp:Label>
            </td>
            <td class="zhonghui">
                所属部门：<asp:Label ID="lbSection" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                职&nbsp;&nbsp;&nbsp;&nbsp;务：<asp:Label ID="lbDuty" runat="server"></asp:Label>
            </td>
            <td>
                类&nbsp;&nbsp;&nbsp;&nbsp;型：<asp:Label ID="lbType" runat="server"></asp:Label>
            </td>
            <td>
                员工状态：<asp:Label ID="lbState" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                入职时间：<asp:Label ID="lbJoinTime" runat="server"></asp:Label>
            </td>
            <td>
                工&nbsp;&nbsp;&nbsp;&nbsp;龄：<asp:Label ID="lbWorkAge" runat="server"></asp:Label>
            </td>
            <td>
                民&nbsp;&nbsp;&nbsp;&nbsp;族：<asp:Label ID="lbNation" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                籍&nbsp;&nbsp;&nbsp;&nbsp;贯：<asp:Label ID="lbNativePlace" runat="server"></asp:Label>
            </td>
            <td>
                政治面貌：<asp:Label ID="lbFace" runat="server"></asp:Label>
            </td>
            <td>
                婚姻状态：<asp:Label ID="lbWedState" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                联系电话：<asp:Label ID="lbTel" runat="server"></asp:Label>
            </td>
            <td>
                手&nbsp;&nbsp;&nbsp;&nbsp;机：<asp:Label ID="lbMobile" runat="server"></asp:Label>
            </td>
            <td colspan="2">
                <asp:Label ID="lbLeave" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                Q&nbsp;&nbsp; &nbsp;&nbsp; Q：<asp:Label ID="lbQQ" runat="server"></asp:Label>
            </td>
            <td>
                MSN：<asp:Label ID="lbMsn" runat="server"></asp:Label>
            </td>
            <td colspan="2">
                E-mail：<asp:Label ID="lbEmail" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="5">
                住&nbsp;&nbsp;&nbsp;&nbsp;址：<asp:Label ID="lbPlace" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="5">
                备&nbsp;&nbsp;&nbsp;&nbsp;注：<asp:Label ID="lbRemark" runat="server"></asp:Label>
            </td>
        </tr>
    </table>
    <table width="696" border="0" align="center" cellpadding="0" cellspacing="0" class="list_2"
        style="margin-top: 3px;">
        <tr>
            <th colspan="5" align="center" bgcolor="#F0F0F0">
                <strong>家庭关系</strong>
            </th>
        </tr>
        <tr>
            <th width="78" align="center">
                关系
            </th>
            <th width="72" align="center">
                姓名
            </th>
            <th width="85" align="center">
                电话
            </th>
            <th width="149" align="center">
                工作单位
            </th>
            <th width="298" align="center">
                住址
            </th>
        </tr>
        <asp:Repeater runat="server" ID="RepRation">
            <ItemTemplate>
                <tr>
                    <td class="zhonghui" align="center">
                        &nbsp;<%#Eval("Relationship")%>
                    </td>
                    <td class="zhonghui_l" align="center">
                        <%#Eval("Name")%>
                    </td>
                    <td class="zhonghui_l" align="center">
                        <%#Eval("Phone")%>
                    </td>
                    <td class="zhonghui_l" align="center">
                        <%#Eval("WorkUnit")%>
                    </td>
                    <td align="center" class="zhonghui_l">
                        <label>
                            <%#Eval("Address")%></label>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
    <table width="696" border="0" align="center" cellpadding="0" cellspacing="0" class="list_2"
        style="margin-top: 3px;">
        <tr>
            <th colspan="7" align="center" bgcolor="#F0F0F0">
                <strong>学历信息</strong>
            </th>
        </tr>
        <tr>
            <th width="78" align="center">
                开始时间
            </th>
            <th width="72" align="center">
                结束时间
            </th>
            <th width="67" align="center">
                学历
            </th>
            <th width="107" align="center">
                所学专业
            </th>
            <th width="112" align="center">
                毕业院校
            </th>
            <th width="42" align="center">
                状态
            </th>
            <th width="200" align="center">
                备注
            </th>
        </tr>
        <asp:Repeater ID="RepSchool" runat="server">
            <ItemTemplate>
                <tr>
                    <td class="zhonghui" align="center">
                        &nbsp;<%#Eval("StartTime","{0:yyyy-MM-dd}")%>
                    </td>
                    <td class="zhonghui_l" align="center">
                        <%#Eval("EndTime", "{0:yyyy-MM-dd}")%>
                    </td>
                    <td class="zhonghui_l" align="center">
                        <%#Eval("Education")%>
                    </td>
                    <td class="zhonghui_l" align="center">
                        <%#Eval("Profession")%>
                    </td>
                    <td class="zhonghui_l" align="center">
                        <%#Eval("Graduated")%>
                    </td>
                    <td class="zhonghui_l" align="center">
                        <label>
                            <%#((int)Eval("Statue")) == 0 ? "" : Eval("Statue").ToString()%>
                        </label>
                    </td>
                    <td class="zhonghui_l" align="center">
                        <%#Eval("Remarks")%>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
    <table width="696" border="0" align="center" cellpadding="0" cellspacing="0" class="list_2"
        style="margin-top: 3px;">
        <tr>
            <th colspan="6" align="center" bgcolor="#F0F0F0">
                <strong>履历信息</strong>
            </th>
        </tr>
        <tr>
            <th width="75" align="center">
                开始时间
            </th>
            <th width="72" align="center">
                结束时间
            </th>
            <th width="192" align="center">
                工作地点
            </th>
            <th width="128" align="center">
                工作单位
            </th>
            <th width="73" align="center">
                从事职业
            </th>
            <th width="140" align="center">
                备注
            </th>
        </tr>
        <asp:Repeater ID="RepWork" runat="server">
            <ItemTemplate>
                <tr>
                    <td class="zhonghui" align="center">
                        &nbsp;<%# EyouSoft.Common.UtilsCommons.GetDateString(Eval("StartTime"), ProviderToDate)%>
                    </td>
                    <td class="zhonghui_l" align="center">
                        <%# EyouSoft.Common.UtilsCommons.GetDateString(Eval("EndTime"),ProviderToDate )%>
                    </td>
                    <td class="zhonghui_l" align="center">
                        <%#Eval("Location")%>
                    </td>
                    <td class="zhonghui_l" align="center">
                        <%#Eval("WorkUnit")%>
                    </td>
                    <td class="zhonghui_l" align="center">
                        <%#Eval("Occupation")%>
                    </td>
                    <td class="zhonghui_l" align="center">
                        <%#Eval("Remarks")%>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
    <table width="696" border="0" align="center" cellpadding="0" cellspacing="0" class="list_2"
        style="margin-top: 3px;">
        <tr>
            <th colspan="6" align="center" bgcolor="#F0F0F0">
                <strong>劳动合同</strong>
            </th>
        </tr>
        <tr>
            <th width="75" align="center">
                员工编号
            </th>
            <th width="72" align="center">
                姓名
            </th>
            <th width="192" align="center">
                签订时间
            </th>
            <th width="128" align="center">
                到期时间
            </th>
            <th width="73" align="center">
                状态
            </th>
            <th width="140" align="center">
                备注
            </th>
        </tr>
        <asp:Repeater runat="server" ID="rpt_Contact">
            <ItemTemplate>
                <tr>
                    <td class="zhonghui" align="center">
                        <%#Eval("ContractNumber")%>
                    </td>
                    <td class="zhonghui_l" align="center">
                        <%#Eval("Name") %>
                    </td>
                    <td class="zhonghui_l" align="center">
                        <%#Eval("SignedTime") == null ? "" : Eval("SignedTime","{0:yyyy-MM-dd}")%>
                    </td>
                    <td class="zhonghui_l" align="center">
                        <%#Eval("MaturityTime") == null ? "" : Eval("MaturityTime","{0:yyyy-MM-dd}")%>
                    </td>
                    <td class="zhonghui_l" align="center">
                        <%#((int)Eval("Status")) == 0 ? "" : Eval("Status").ToString()%>
                    </td>
                    <td class="zhonghui_l" align="center">
                        <%#Eval("Remarks") %>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
</asp:Content>

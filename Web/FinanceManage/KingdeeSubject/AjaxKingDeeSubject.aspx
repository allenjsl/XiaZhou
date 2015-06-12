<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AjaxKingDeeSubject.aspx.cs"
    Inherits="EyouSoft.Web.FinanceManage.KingdeeSubject.AjaxKingDeeSubject" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<table width="100%">
    <asp:repeater id="rpt_list" runat="server">
    <ItemTemplate>
    <tr data-parentid="<%=EyouSoft.Common.Utils.GetQueryStringValue("parentSubjectId") %>" data-subjectcd="<%#Eval("Cd") %>"data-subjectid="<%#Eval("Id") %>">
        <td class="zzjg_jibie0<%=EyouSoft.Common.Utils.GetQueryStringValue("index") %>_bg" align="left" height="28" valign="middle" style="padding-left: 10px;">
            <span class="leftStyle">
                <img data-class="img_getNext" align="middle" height="9" width="9" data-subjectid="<%#Eval("Id") %>" alt="" src="/images/zzjg+.gif">
                <label>
                <span class="zzjg_jibie0<%=EyouSoft.Common.Utils.GetQueryStringValue("index") %>">
                    <a >
                        <input type="radio" name="KingDeeSubject" style="border: none" data-subjectnm="<%#Eval("Name") %>"
                                                data-subjectcd="<%#Eval("Cd") %>" data-subjectid="<%#Eval("Id") %>">
                        <%#Eval("Name")%>
                    </a>
                </span>
                 </label>
            </span>
        </td>
    </tr>
    </ItemTemplate>
    </asp:repeater>
    <asp:panel id="pan_msg" runat="server">
        <tr data-idout="true" data-parentid="<%=EyouSoft.Common.Utils.GetQueryStringValue("parentSubjectId") %>">
            <td>无下级类型</td>
        </tr>
    </asp:panel>
</table>
</html>

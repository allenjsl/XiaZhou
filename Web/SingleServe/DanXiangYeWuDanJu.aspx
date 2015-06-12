<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DanXiangYeWuDanJu.aspx.cs"
    Inherits="EyouSoft.Web.SingleServe.DanXiangYeWuDanJu" MasterPageFile="~/MasterPage/Boxy.Master" Title="单项业务单据" %>
<asp:Content ContentPlaceHolderID="PageHead" runat="server" ID="PageHead">
    <style type="text/css">
        .i_list p{height:24px;}
    </style>
</asp:Content>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="PageBody">
    <div class="alertbox-outbox" style="padding-top:2px;">
        <div style="width: 96%; margin: 0px auto;" class="i_list">
            <asp:Literal runat="server" ID="ltr"></asp:Literal>
        </div>
    </div>
</asp:Content>

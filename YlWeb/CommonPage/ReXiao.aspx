<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReXiao.aspx.cs" Inherits="EyouSoft.YlWeb.CommonPage.ReXiao" %>
<%@ OutputCache Duration="3600" VaryByParam="lx" %>
<asp:Repeater runat="server" ID="rptRX"><ItemTemplate>
<li>
    <a href="<%#GetHqUrl(Eval("LeiXing")) %><%# Eval("HangQiId")%>.html"><img src="<%#Eval("FuJians") != null && ((System.Collections.Generic.List<EyouSoft.Model.YlStructure.MFuJianInfo>)Eval("FuJians")).Count > 0 ? EyouSoft.YlWeb.TuPian.F1(EyouSoft.Common.Utils.GetErpFilepath() + ((System.Collections.Generic.List<EyouSoft.Model.YlStructure.MFuJianInfo>)Eval("FuJians"))[0].Filepath, 140, 75) : string.Empty %>"></a>
    <p><a href="<%#GetHqUrl(Eval("LeiXing")) %><%# Eval("HangQiId")%>.html"><%#Eval("MingCheng") %></a></p>
    <p class="paihang-price"><span class="sales">¥<em><%#Eval("QiShIJiaGe","{0:F2}") %></em>起</span><span class="floatR"><a href="<%#GetHqUrl(Eval("LeiXing")) %><%# Eval("HangQiId")%>.html" class="s-ydbtn">立即预订</a></span></p>
</li></ItemTemplate></asp:Repeater>
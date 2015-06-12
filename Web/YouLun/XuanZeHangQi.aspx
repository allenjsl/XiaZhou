<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="XuanZeHangQi.aspx.cs" Inherits="EyouSoft.Web.YouLun.XuanZeHangQi"
    MasterPageFile="~/MasterPage/Boxy.Master" %>

<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>
<%@ Register Src="~/YouLun/WUC/wucupload.ascx" TagName="wucupload" TagPrefix="uc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="PageBody">
    <div class="alertbox-outbox">
        <div style="width: 99%; margin: 0px auto;">
            <form method="get">
            <input type="hidden" name="sl" value="<%=SL %>" />
            <input type="hidden" name="RefererIfrId" value="<%=Request.QueryString["RefererIfrId"] %>" />
            <input type="hidden" name="iframeId" value="<%=Request.QueryString["iframeId"] %>" />            
            类型：<select name="txtLeiXing" class="inputselect">
                <option value="">请选择</option>
                <%=EyouSoft.Common.UtilsCommons.GetEnumDDL(EyouSoft.Common.EnumObj.GetList(typeof(EyouSoft.Model.EnumType.YlStructure.YouLunLeiXing)), EyouSoft.Common.Utils.GetQueryStringValue("txtLeiXing"))%>
            </select>
            名称：<input type="text" name="txtMingCheng" class="inputtext" value="<%=Request.QueryString["txtMingCheng"] %>" />
            编号：<input type="text" name="txtBianHao" class="inputtext" value="<%=Request.QueryString["txtBianHao"] %>" />
            <input type="submit" value="查询" />
            </form>
            
            <table id="tblList" width="100%" cellspacing="0" cellpadding="0" class="alertboxbk1"
                border="0" bgcolor="#FFFFFF" style="border-collapse: collapse; margin-top:10px;">
                <tr>
                    <td style="height:25px;">选择</td>
                    <td>类型</td>
                    <td>名称</td>
                    <td>天数</td>
                    <td>供应商</td>
                    <td>游轮公司</td>
                    <td>游轮系列</td>
                    <td>游轮船只</td>
                    <td>出发港口</td>
                    <td>抵达港口</td>
                </tr>  
                <asp:Repeater runat="server" ID="rpt">
                <ItemTemplate>
                <tr>
                    <td style="height: 25px;">
                        <input type="radio" name="radio1" i_hangqiid="<%#Eval("HangQiId") %>" i_mingcheng="<%#Eval("MingCheng") %>" id="<%#Eval("HangQiId") %>" />
                    </td>
                    <td>
                        <%#Eval("LeiXing") %>
                    </td>
                    <td>
                        <%#Eval("MingCheng") %>
                    </td>
                    <td>
                        <%#Eval("TianShu1") %>天<%#Eval("TianShu2") %>晚
                    </td>
                    <td>
                        <%#Eval("GysName") %>
                    </td>
                    <td>
                        <%#Eval("GongSiName") %>
                    </td>
                    <td>
                        <%#Eval("XiLieName") %>
                    </td>
                    <td>
                        <%#Eval("ChuanZhiName") %>
                    </td>
                    <td>
                        <%#Eval("ChuFaGangKouMingCheng") %>
                    </td>
                    <td>
                        <%#Eval("DiDaGangKouMingCheng") %>
                    </td>
                </tr>
                </ItemTemplate>
                </asp:Repeater>
            </table>
            <div style="width:100%; text-align:right; height:25px; line-height:25px;">
                <cc1:ExporPageInfoSelect ID="paging" runat="server" />
            </div>
        </div>
        <div style="text-align: right;" class="alertbox-btn">
            <a href="javascript:void(0)" id="i_xuanze">选择</a>
        </div>
    </div>

    <script type="text/javascript">
        var iPage = {
            reload: function() {
                window.location.href = window.location.href;
            },
            close: function() {
                var _win = top || window;
                _win.Boxy.getIframeDialog('<%=EyouSoft.Common.Utils.GetQueryStringValue("iframeId") %>').hide();
                return false;
            },
            xuanZe: function() {
                var _$radio = $("input[type='radio']:checked");
                if (_$radio.length < 1) { alert("请选择航期"); return false; }
                var _data = { hangQiId: _$radio.attr("i_hangqiid"), mingCheng: _$radio.attr("i_mingcheng") };
                var _window = this.getWindow();
                _window.iPage.setHangQi(_data);

                iPage.close();
            },
            getWindow: function() {
                var _RefererIfrId = '<%=Request.QueryString["RefererIfrId"] %>';
                var _window = window.parent.Boxy.getIframeWindow(_RefererIfrId);
                return _window;
            },
            initXuanZhong: function() {
                var data = this.getWindow().iPage.getHangQi();
                if (data.hangQiId.length < 1) return;
                $("#" + data.hangQiId).attr("checked", "checked");
            }
        };

        $(document).ready(function() {
            $("#i_xuanze").bind("click", function() { iPage.xuanZe(this); });
            iPage.initXuanZhong();
        });
    </script>

</asp:Content>

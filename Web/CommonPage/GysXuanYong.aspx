<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GysXuanYong.aspx.cs" Inherits="EyouSoft.Web.CommonPage.GysXuanYong"
    MasterPageFile="~/MasterPage/Boxy.Master" %>

<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="PageBody">
    <style type="text/css">
        li{float:left; width:25%; height:30px; line-height:30px;}
    </style>
    
    <div class="alertbox-outbox" style="padding-top:5px;">
        <div style="width:98%; margin:0px auto;">
            <form action="GysXuanYong.aspx">
            <input type="hidden" name="sl" value="<%=SL %>" />
            <input type="hidden" name="ClientID" id="ClientID" value="<%=EyouSoft.Common.Utils.GetQueryStringValue("ClientID") %>" />    
            <input type="hidden" name="iframeId" id="iframeId" value="<%=EyouSoft.Common.Utils.GetQueryStringValue("iframeId") %>" />
            <input type="hidden" name="pIframeId" id="pIframeId" value="<%=EyouSoft.Common.Utils.GetQueryStringValue("pIframeId") %>" />
            <input type="hidden" name="gysId" id="gysId" value="<%=EyouSoft.Common.Utils.GetQueryStringValue("gysId") %>" />
            
            <table cellspacing="0" cellpadding="0" style="width:100%;">
                <tr>
                    <td style="height: 30px;">
                        类型：<select class="inputselect" name="txtLeiXing">
                            <%= EyouSoft.Common.UtilsCommons.GetEnumDDL(EyouSoft.Common.EnumObj.GetList(typeof(EyouSoft.Model.EnumType.SourceStructure.SourceType)), EyouSoft.Common.Utils.GetQueryStringValue("txtLeiXing"), "", "请选择")%>
                        </select>
                        省份：
                        <select id="txtProvinceId" name="txtProvinceId" class="inputselect">
                        </select>
                        城市：
                        <select id="txtCityId" name="txtCityId" class="inputselect">
                        </select>
                        名称：<input type="text" name="txtName" class="inputtext formsize120" value="<%=EyouSoft.Common.Utils.GetQueryStringValue("txtName") %>" />
                        <input type="submit" style="width: 64px; height: 24px; background: url(/images/cx.gif) no-repeat center center;
                            border: 0 none; margin-left: 5px;" class="search-btn" value="查询" />
                    </td>
                </tr>
            </table>
            </form>
            
            <ul style="width: 100%; margin: 0px auto; margin-top: 10px;">
                <asp:Repeater runat="server" ID="rpt">
                <ItemTemplate>
                <li i_gysid="<%#Eval("GysId") %>" i_gysname="<%#Eval("GysName") %>"><input type="radio" name="radioGys" id="radio<%#Eval("GysId") %>" />&nbsp;<label for="radio<%#Eval("GysId") %>"><%#Eval("GysName") %></label></li>
                </ItemTemplate>
                </asp:Repeater>
            </ul>
            
            <div class="pages" style="clear:both;">
                <!--paging-->
                <cc1:ExporPageInfoSelect ID="paging" runat="server" />
            </div>
            
        </div>
    </div>
    
    <div class="alertbox-btn">
        <a id="i_a_qingchu" href="javascript:void(0);" style="text-indent: 0px;">清除</a>
        <a id="i_a_xuanze" href="javascript:void(0);" style="text-indent: 0px; ">选择</a>
    </div>
    
    <script type="text/javascript">
        var iPage = {
            close: function() {
                top.Boxy.getIframeDialog($("#iframeId").val()).hide();
            },
            getP: function() {
                var win = top.window;
                var _pIframeId = $.trim($("#pIframeId").val());
                var _ClientID = $.trim($("#ClientID").val());
                if (_pIframeId.length > 0) {
                    win = top.Boxy.getIframeWindow(_pIframeId);
                }
                return win[_ClientID];
            },
            init: function() {
                var _gysId = $.trim($("#gysId").val());
                if (_gysId.length > 0) $("#radio" + _gysId).attr("checked", "checked");
            },
            xuanZe: function() {
                var _data = { gysId: "", gysName: "" };
                var _$obj = $("input[type='radio']:checked");
                var _$li = _$obj.closest("li");

                _data.gysId = _$li.attr("i_gysid");
                _data.gysName = _$li.attr("i_gysname");

                this.getP().setValue(_data);
                this.close();
            },
            qingChu: function() {
                var _data = { gysId: "", gysName: "" };
                this.getP().setValue(_data);
                this.close();
            }
        };

        $(document).ready(function() {
            iPage.init();
            $("#i_a_xuanze").click(function() { iPage.xuanZe(); });
            $("#i_a_qingchu").click(function() { iPage.qingChu(); });
            if ($("#gysId").val().length == 0) $("#i_a_qingchu").hide();
            pcToobar.init({ pID: "#txtProvinceId", cID: "#txtCityId", pSelect: '<%=EyouSoft.Common.Utils.GetQueryStringValue("txtProvinceId") %>', cSelect: '<%=EyouSoft.Common.Utils.GetQueryStringValue("txtCityId") %>' });
        });
    </script>
</asp:Content>

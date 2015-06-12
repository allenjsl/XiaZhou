<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SelectScenic.aspx.cs" Inherits="EyouSoft.Web.TeamCenter.SelectScenic" %>

<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="/Css/style.css" rel="stylesheet" type="text/css" />
    <link href="/Css/boxynew.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="/Js/jquery-1.4.4.js"></script>

    <script type="text/javascript" src="/js/jquery.boxy.js"></script>

    <script type="text/javascript" src="/Js/jquery.blockUI.js"></script>

    <script type="text/javascript" src="/Js/table-toolbar.js"></script>

    <style type="text/css">
        table, tr, td
        {
            border: 1px solid #B8C5CE;
            border-collapse: collapse;
        }
        label
        {
            cursor: pointer;
        }
    </style>
</head>
<body style="background: 0 none;">
    <div>
        <form id="form1" method="get" action="SelectScenic.aspx">
        <table width="100%" align="center" cellspacing="0" cellpadding="0" bgcolor="#e9f4f9"
            style="margin: 10px auto">
            <tbody>
                <tr>
                    <td width="10%" align="right" height="28" bgcolor="#b7e0f3" class="alertboxTableT">
                        景点名称：
                    </td>
                    <td width="90%" align="left">
                        <input type="text" id="textfield" class="inputtext formsize120" name="textfield"
                            value="<%=Request.QueryString["textfield"] %>" />
                        国家：
                        <select id="ddlCountry" name="ddlCountry" class="inputselect">
                        </select>
                        省份：
                        <select id="ddlProvice" name="ddlProvice" class="inputselect">
                        </select>
                        城市：
                        <select id="ddlCity" name="ddlCity" class="inputselect">
                        </select>
                        县区：<select id="ddlArea" name="ddlArea" class="inputselect">
                        </select>
                        <input type="hidden" name="<%=Request.QueryString["callback"] %>" />
                        <input class="search-btn" id="Select" type="button" style="cursor: pointer; height: 24px;
                            width: 64px; background: url(/images/cx.gif) no-repeat center center; border: 0 none;
                            margin-left: 5px;" value="搜索" />
                    </td>
                </tr>
            </tbody>
        </table>
        </form>
        <div style="margin: 0 auto; width: 99%;">
            <span class="formtableT formtableT02 t1">选择<span name="SourceName">景点</span> </span>
            <div style="width: 100%" id="AjaxHotelList">
                <table id="tblList" align="center" bgcolor="#FFFFFF" cellpadding="0" cellspacing="0"
                    width="100%" class="alertboxbk1" border="0" style="border-collapse: collapse;
                    margin: 5px 0;">
                    <tr>
                        <asp:Repeater ID="rpt_ScenicList" runat="server">
                            <ItemTemplate>
                                <td align="left" height="28">
                                    &nbsp;&nbsp;
                                    <input id="<%#Eval("JingDianId") %>" name="radiogroup" type="checkbox" value="<%#Eval("JingDianId") %>"
                                        data-show="<%#Eval("Name")%>" />
                                    <input type="hidden" id="txtJingDianDesc_<%#Eval("JingDianId") %>" value="<%#Eval("MiaoShu") %>" />
                                    <label for="<%#Eval("JingDianId") %>">
                                        <%#(Container.ItemIndex+1)+(pageIndex-1)*pageSize %>
                                        <%#Eval("Name")%>
                                    </label>
                                    <%#EyouSoft.Common.Utils.IsOutTrOrTd(Container.ItemIndex, listCount, 4)%>
                            </ItemTemplate>
                        </asp:Repeater>
                    <asp:Literal ID="litMsg" runat="server"></asp:Literal>
                    <tr>
                        <td align="right" class="alertboxTableT" colspan="4" height="23">
                            <div class="pages">
                                <cc1:ExporPageInfoSelect ID="ExporPageInfoSelect1" runat="server" />
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="alertbox-btn">
                <a id="a_btn" hidefocus="true" href="javascript:void(0)"><s class="xuanzhe"></s>选 择</a></div>
        </div>
    </div>

    <script type="text/javascript">
        var useSupplierPage = {
            //选中的景点信息 {id:"景点编号",name:"景点名称",desc:"景点描述"}
            jingDians: [],
            setXuanZhongValue: function() {
                var _self = this;
                $("#tblList").find("input[type='checkbox']:checked").each(function() {
                    var _$chk = $(this);
                    var _jingDian = { id: _$chk.val(), name: _$chk.attr("data-show"), desc: "" };
                    _jingDian.desc = $("#txtJingDianDesc_" + _jingDian.id).val();
                    _self.jingDians.push(_jingDian);
                });
            },
            search_Click: function() {
                window.location = "/TeamCenter/SelectScenic.aspx?" + $.param($.extend(Boxy.getUrlParams(["textfield", "ddlCountry", "ddlProvice", "ddlCity", "ddlArea"]), {
                    textfield: $("#textfield").val(),
                    ddlCountry: $("#ddlCountry").val(),
                    ddlProvice: $("#ddlProvice").val(),
                    ddlCity: $("#ddlCity").val(),
                    ddlArea: $("#ddlArea").val()
                }))
            },
            xuanZe_Click: function() {
                this.setXuanZhongValue();

                var _callBackFn = '<%=EyouSoft.Common.Utils.GetQueryStringValue("callback") %>';
                var _data = { jingDianXuanYongAId: '<%=EyouSoft.Common.Utils.GetQueryStringValue("aid") %>', jingDians: this.jingDians };

                window.parent[_callBackFn](_data);

                parent.Boxy.getIframeDialog('<%=Request.QueryString["iframeId"] %>').hide();
                return false;
            }
        };

        $(document).ready(function() {
            $("#Select").bind("click", function() { useSupplierPage.search_Click(); });
            $("#a_btn").bind("click", function() { useSupplierPage.xuanZe_Click(); });
            pcToobar.init({ gID: "#ddlCountry", pID: "#ddlProvice"
                , cID: "#ddlCity", xID: "#ddlArea"
                , gSelect: '<%=Request.QueryString["ddlCountry"] %>', pSelect: '<%=Request.QueryString["ddlProvice"] %>'
                , cSelect: '<%=Request.QueryString["ddlCity"] %>', xSelect: '<%=Request.QueryString["ddlArea"] %>'
            });
        });
    </script>

</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HuiYuan.aspx.cs" Inherits="EyouSoft.Web.YouLun.HuiYuan"
    MasterPageFile="~/MasterPage/Front.Master" %>

<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>
<%@ Register Src="~/YouLun/WUC/wuc1.ascx" TagName="wuc1" TagPrefix="uc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="mainbox">
        <uc1:wuc1 runat="server" ID="wuc1" />
        <div class="searchbox fixed">
            <form id="form1" method="get">
            <input type="hidden" name="sl" value='<%= SL %>' />
            <input type="hidden" name="t" value="<%=Request.QueryString["t"] %>" />
            <span class="searchT">
                <p>
                    会员姓名：<input type="text" size="30" class="inputtext formsize120" name="txtXingMing"
                        value="<%=Request.QueryString["txtXingMing"] %>" />
                    会员账号：<input type="text" size="30" class="inputtext formsize120" name="txtUsername"
                        value="<%=Request.QueryString["txtUsername"] %>" />
                    注册时间：<input type="text" onfocus="WdatePicker()" class="inputtext formsize100" name="txtZhuCeShiJian1"
                        value="<%=Request.QueryString["txtZhuCeShiJian1"] %>" />-<input type="text" onfocus="WdatePicker()"
                            class="inputtext formsize100" name="txtZhuCeShiJian2" value="<%=Request.QueryString["txtZhuCeShiJian2"] %>" />
                    联系方式：<input type="text" size="30" class="inputtext formsize120" name="txtLianXiFangShi"
                        value="<%=Request.QueryString["txtLianXiFangShi"] %>" />
                    <input type="submit" value="搜索" class="search-btn" />
                </p>
            </span>
            </form>
        </div>
        
        <div class="tablehead" id="select_Toolbar_Paging_1">
            <ul class="fixed">
                
            </ul>
            <div class="pages">
                <cc1:exporpageinfoselect id="paging" runat="server" />
            </div>
        </div>
        
        <!--列表表格-->
        <div class="tablelist-box">
            <table width="100%" id="liststyle">
                <tr class="odd">
                    <th class="thinputbg">
                        <input type="checkbox" id="checkbox" name="checkbox" />
                    </th>
                    <th class="th-line" style="text-align: left;">
                        会员姓名
                    </th>
                    <th class="th-line" style="text-align: left;">
                        会员账号
                    </th>
                    <th class="th-line" style="text-align: left;">
                        性别
                    </th>
                    <th class="th-line" style="text-align: left; ">
                        联系电话
                    </th>
                    <th class="th-line" style="text-align: left; ">
                        联系手机
                    </th>
                    <th class="th-line" style="text-align: left;">
                        联系邮箱
                    </th>
                    <th class="th-line" style="text-align: right;">
                        会员积分&nbsp;
                    </th>
                    <th class="th-line" style="text-align: left;">
                        注册时间
                    </th>
                    <th class="th-line" style="text-align: left;">
                        状态
                    </th>
                    <th class="th-line" style="text-align: left;">
                        操作
                    </th>
                </tr>
                <asp:Repeater ID="rpt" runat="server">
                    <ItemTemplate>
                        <tr class="odd" i_huiyuanid="<%#Eval("HuiYuanId") %>">
                            <td class="thinputbg" style="text-align: center;">
                                <input type="checkbox" id="checkbox" name="checkbox" value='<%# Eval("HuiYuanId") %>'>
                            </td>
                            <td class="th-line">
                                <%#Eval("XingMing")%>
                            </td>
                            <td class="th-line">
                                <a href="javascript:void(0)" class="yonghuming"><%#Eval("Username") %></a>
                            </td>
                            <td class="th-line">
                                <%#Eval("XingBie")%>
                            </td>
                            <td class="th-line">
                                <%#Eval("DianHua")%>
                            </td>
                            <td class="th-line">
                                <%#Eval("ShouJi") %>
                            </td>
                            <td class="th-line">
                                <%#Eval("YouXiang")%>
                            </td>
                            <td class="th-line" style="text-align:right;">
                                <%# Eval("KeYongJiFen","{0:F2}")%>&nbsp;
                            </td>
                            <td class="th-line">
                                <%# Eval("ZhuCeShiJian", "{0:yyyy-MM-dd}")%>
                            </td>
                            <td class="th-line">
                                <%#Eval("Status")%>
                            </td>
                            <td class="th-line">
                                <%#GetStatus(Eval("Status"))%>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
                <asp:PlaceHolder runat="server" ID="phHeJi">
                    
                </asp:PlaceHolder>
                <asp:PlaceHolder runat="server" ID="phEmpty" Visible="false">
                    <tr>
                        <td colspan="50" align="center">
                            暂无数据。
                        </td>
                    </tr>
                </asp:PlaceHolder>
            </table>
        </div>
        <!--列表结束-->
        <div class="tablehead" style="border-top: 0 none;" id="select_Toolbar_Paging_2">
        </div>
    </div>

    <script src="/js/datepicker/wdatepicker.js" type="text/javascript"></script>

    <script type="text/javascript">
        var iPage = {
            reload: function() {
                window.location.href = window.location.href;
            },
            tingYong: function(obj) {
                if (!confirm("你确定要停用该账号吗？")) return false;
                var _$tr = $(obj).closest("tr");
                var _data = { txtHuiYuanId: _$tr.attr("i_huiyuanid") };
                $.ajax({ type: "POST", url: "huiyuan.aspx?doType=tingyong&sl=<%=SL %>", data: _data, cache: false, dataType: "json", async: false,
                    success: function(response) {
                        if (response.result == "1") {
                            alert(response.msg);
                            iPage.reload();
                        } else {
                            alert(response.msg);
                        }
                    }
                });
            },
            qiYong: function(obj) {
                if (!confirm("你确定要启用该账号吗？")) return false;
                var _$tr = $(obj).closest("tr");
                var _data = { txtHuiYuanId: _$tr.attr("i_huiyuanid") };
                $.ajax({ type: "POST", url: "huiyuan.aspx?doType=qiyong&sl=<%=SL %>", data: _data, cache: false, dataType: "json", async: false,
                    success: function(response) {
                        if (response.result == "1") {
                            alert(response.msg);
                            iPage.reload();
                        } else {
                            alert(response.msg);
                        }
                    }
                });
            },
            yongHuMing: function(obj) {
                var _$tr = $(obj).closest("tr");
                Boxy.iframeDialog({ iframeUrl: "HuiYuan01.aspx?sl=<%=SL %>&editid=" + _$tr.attr("i_huiyuanid"), title: "修改会员账号及密码", modal: true, width: "380px", height: "200px", afterHide: function() { iPage.reload(); } });
            }
        };

        $(document).ready(function() {
            wuc1("<%=LeiXing %>");
            $("#select_Toolbar_Paging_1").children().clone(true).prependTo("#select_Toolbar_Paging_2");
            tableToolbar.init();
            $(".i_tingyong").click(function() { iPage.tingYong(this); });
            $(".i_qiyong").click(function() { iPage.qiYong(this); });
            $(".yonghuming").click(function() { iPage.yongHuMing(this); });
        });
    </script>
</asp:Content>

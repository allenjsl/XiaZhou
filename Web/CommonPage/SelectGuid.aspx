<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SelectGuid.aspx.cs" Inherits="Web.CommonPage.SelectGuid" %>

<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="Stylesheet" type="text/css" href="/Css/style.css" />

    <script src="/Js/jquery-1.4.4.js" type="text/javascript"></script>

    <script src="/Js/jquery.boxy.js" type="text/javascript"></script>

    <script src="/Js/table-toolbar.js" type="text/javascript"></script>

</head>
<body style="background: #e9f4f9;">
    <div>
        <div class="alertbox-outbox">
            <div class="tablehead" style="background: none; border-top: none;">
                <ul class="fixed">
                    <li><s class="orderformicon"></s><a class="ztorderform" hidefocus="true" href="/GuideCenter/GuideSchedul.aspx?sl=<%=(int)EyouSoft.Model.EnumType.PrivsStructure.Menu2.导游中心_导游排班 %>"
                        target="_blank"><span>导游排班</span></a></li>
                    <li><s class="orderformicon"></s><a class="ztorderform" href="javascript:void(0);"
                        hidefocus="true" data-class="addGuidInfo" style="text-indent: 0px;" data-class="addGuidInfo">
                        <span>新增导游</span></a></li>
                </ul>
            </div>
            <div class="hr_10">
            </div>
            <form id="formguidSearch" action="/CommonPage/SelectGuid.aspx" method="get">
            <div class="searchbox fixed">
                <span class="searchT">
                    <p>
                        <input type="hidden" name="sl" value="<%=EyouSoft.Common.Utils.GetQueryStringValue("sl") %>" />
                        <input type="hidden" name="pIframeId" value="<%=EyouSoft.Common.Utils.GetQueryStringValue("pIframeId") %>" />
                        <input type="hidden" name="iframeId" value="<%=EyouSoft.Common.Utils.GetQueryStringValue("iframeId") %>" />
                        <input type="hidden" name="callBackFun" value="<%=EyouSoft.Common.Utils.GetQueryStringValue("callBackFun") %>" />
                        <input type="hidden" name="id" value="<%=EyouSoft.Common.Utils.GetQueryStringValue("id") %>" />
                        导游姓名：<input type="text" class="inputtext formsize50" name="txtguidName" value="<%=Request.QueryString["txtguidName"] %>" />
                        语 种：<input type="text" class="inputtext formsize50" name="txtguidlanguage" value="<%=Request.QueryString["txtguidlanguage"] %>" />
                        性别：<select name="guidsex" class="inputselect">
                            <%=this.GetGender(Request.QueryString["guidsex"])%>
                        </select>
                        类别：
                        <select name="guidType" class="inputselect">
                            <%=this.GetGuidType(Request.QueryString["guidType"])%>
                        </select>
                        级别：
                        <select name="leavel" class="inputselect">
                            <%=this.GetGuidLeavel(Request.QueryString["leavel"])%>
                        </select>
                        <input type="submit" id="search" style="width: 64px; height: 24px; background: url(/images/cx.gif) no-repeat center center;
                            border: 0 none; margin-left: 5px;" value="查询" /></p>
                </span>
            </div>
            </form>
            <div class="hr_10">
            </div>
            <div style="margin: 0 auto; width: 99%;">
                <table width="100%" align="center" cellpadding="0" cellspacing="0" bgcolor="#FFFFFF"
                    style="margin: 0 auto" id="liststyle">
                    <tr>
                        <td width="30" height="23" align="center" bgcolor="#b7e0f3" class="alertboxTableT">
                            &nbsp;
                        </td>
                        <td align="center" bgcolor="#b7e0f3" class="alertboxTableT">
                            导游姓名
                        </td>
                        <td align="center" bgcolor="#b7e0f3" class="alertboxTableT">
                            联系电话
                        </td>
                        <td align="center" bgcolor="#b7e0f3" class="alertboxTableT">
                            Email
                        </td>
                    </tr>
                    <asp:Repeater ID="replist" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td align="center">
                                    <input name="radioguid" type="radio" value="<%# Eval("GuideId")%>" style="border: none" />
                                </td>
                                <td height="28" align="center">
                                    <%# Eval("Name")%>
                                </td>
                                <td align="center">
                                    <%# Eval("Mobile")%>
                                </td>
                                <td align="center">
                                    <%# Eval("Email")%>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                    <tr>
                        <td height="23" colspan="4" align="right" class="alertboxTableT">
                            <div style="position: relative; height: 32px;">
                                <div class="pages">
                                    <cc1:ExporPageInfoSelect ID="ExporPageInfoSelect1" runat="server" />
                                    <asp:Literal ID="litdatamsg" runat="server"></asp:Literal>
                                </div>
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="alertbox-btn" id="btn-action_list">
                <a href="javascript:void(0);" hidefocus="true" data-class="selected"><s class="xuanzhe">
                </s>选 择</a></div>
        </div>
    </div>

    <script type="text/javascript">
        var selectGuidPage = {
            selectedTxt: "",
            selectedVal: "",
            selectedPhone: "",
            parentWindow: null,
            iframeId: '<%=EyouSoft.Common.Utils.GetQueryStringValue("iframeID") %>',
            pIframeID: '<%=Request.QueryString["pIframeId"]%>',
            //选用a标签id
            aid: '<%=Request.QueryString["id"] %>',
            //获取选中的值
            setVal: function() {
                var txtArr = new Array();
                var valArr = new Array();
                var phoneArr = new Array();
                $("input[type='radio'][name='radioguid']:checked").each(function() {
                    txtArr.push($.trim($(this).parent().next().html()));
                    valArr.push($.trim($(this).val()));
                    phoneArr.push($.trim($(this).parent().next().next().html()));
                });
                selectGuidPage.selectedTxt = txtArr.join(',');
                selectGuidPage.selectedVal = valArr.join(',');
                selectGuidPage.selectedPhone = phoneArr.join(',');
            },
            _OpenBoxy: function(title, iframeUrl, width, height, draggable) {
                parent.Boxy.iframeDialog({ title: title, iframeUrl: iframeUrl, width: width, height: height, draggable: draggable });
            },
            _BindBtn: function() {
                //新增
                $(".tablehead").find("a[data-class='addGuidInfo']").unbind("click");
                $(".tablehead").find("a[data-class='addGuidInfo']").click(function() {
                    selectGuidPage._OpenBoxy("新增导游", '/GuideCenter/GuideRecordInfo.aspx?pIframeID=' + selectGuidPage.iframeId + '&sl=<%=EyouSoft.Common.Utils.GetQueryStringValue("sl") %>', "970px", "535px", true);
                    return false;
                });

                //选用事件
                $("#btn-action_list").find("a[data-class='selected']").unbind("click");
                $("#btn-action_list").find("a[data-class='selected']").click(function() {
                    //赋值
                    selectGuidPage.setVal();

                    var backData = { id: '<%=EyouSoft.Common.Utils.GetQueryStringValue("id") %>', value: selectGuidPage.selectedVal, text: selectGuidPage.selectedTxt, phone: selectGuidPage.selectedPhone };

                    //回调方法
                    var callBackFun = '<%=Request.QueryString["callBackFun"] %>';
                    if (callBackFun.indexOf('.') == -1) {
                        selectGuidPage.parentWindow[callBackFun](backData);
                    } else {
                        selectGuidPage.parentWindow[callBackFun.split('.')[0]][callBackFun.split('.')[1]](backData);
                    }

                    //关闭页面
                    window.parent.Boxy.getIframeDialog(selectGuidPage.iframeId).hide();
                    return false;
                });

            },
            _PageInit: function() {
                //初始化省份 城市 县区
                pcToobar.init({ pID: '#proviceList', cID: '#cityList', comID: '<%=this.SiteUserInfo.CompanyId %>', xID: "#countryList" });

                selectGuidPage._BindBtn();
            },
            //根据隐藏域的值初始化选中
            _InitSelectVal: function() {
                if (selectGuidPage.aid) {
                    var parentHidVal = selectGuidPage.parentWindow.$("#" + selectGuidPage.aid).parent().find("input[type='hidden']").val();
                    if (parentHidVal != "") {
                        $("#liststyle").find("input[type='radio'][name='radioguid'][value='" + parentHidVal + "']").attr("checked", "true");
                    }
                }
            }
        }

        $(function() {
            //获得需要赋值页面的window 对象
            if (selectGuidPage.pIframeID) {
                selectGuidPage.parentWindow = window.parent.Boxy.getIframeWindow(selectGuidPage.pIframeID) || window.parent.Boxy.getIframeWindowByID(selectGuidPage.pIframeID);
            } else {
                selectGuidPage.parentWindow = parent.window;
            }

            selectGuidPage._PageInit();
            selectGuidPage._InitSelectVal();
        });
    </script>

</body>
</html>

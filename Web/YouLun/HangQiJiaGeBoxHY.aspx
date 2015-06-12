<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HangQiJiaGeBoxHY.aspx.cs"
    Inherits="EyouSoft.Web.YouLun.HangQiJiaGeBoxHY" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="/css/style.css" rel="stylesheet" type="text/css" />
    <link href="/css/boxynew.css" rel="stylesheet" type="text/css" />
    <link href="/css/tankkuang.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="/js/jquery-1.4.4.js"></script>

    <script src="/Js/jquery.blockUI.js" type="text/javascript"></script>

    <script src="/Js/ValiDatorForm.js" type="text/javascript"></script>

    <script type="text/javascript" src="/js/table-toolbar.js"></script>

    <style type="text/css">
        #tabBox td
        {
            width: 100px;
        }
        #tabBox input
        {
            width: 80px;
        }
    </style>
</head>
<body>
    <form id="form1">
    <div class="alertbox-outbox">
        <table id="tabBox" width="98%" border="0" cellspacing="0" cellpadding="0" 
            style="margin: 0 auto;">
            <tr style="background: url(/images/y-formykinfo.gif) repeat-x center top;">
                <td width="25" height="23" align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                    房型
                </td>
                <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                    楼层
                </td>
                <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                    类型
                </td>
                <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                    价格
                </td>                
                <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                    最低容纳人数
                </td>
                <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                    房差
                </td>
                <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                    说明
                </td>
                <td width="105" align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                    操作
                </td>
            </tr>
            <asp:PlaceHolder ID="PlaceHolder1" runat="server">
                    <tr>
                        <td align="center" bgcolor="#FFFFFF">
                            <input type="hidden" name="txtjiageid" value="" />
                            <select name="ddlfangxing" id="select" class="inputselect" errmsg="请选择房型!" valid="required">
                                <%=getOpts("",3)%>
                            </select>
                        </td>                        
                        <td align="center" bgcolor="#FFFFFF">
                            <input name="louceng" type="text" class="formsize50" value="" />
                        </td>
                        <td height="28" align="center" bgcolor="#FFFFFF">
                            <select name="ddlleixing" id="select3" class="inputselect" errmsg="请选择人员类型!" valid="required">
                                <%=getOpts("",4)%>
                            </select>
                        </td>
                        <td align="center" bgcolor="#FFFFFF">
                            <input name="jiage" type="text" class="formsize50" value="" />
                        </td>
                        <td align="center" bgcolor="#FFFFFF">
                            <input name="renshu" type="text" class="formsize50" value="" />
                        </td>
                        <td align="center" bgcolor="#FFFFFF">
                            <input name="fangcha" type="text" class="formsize50" value="" />
                        </td>
                        <td align="center" bgcolor="#FFFFFF">
                            <input name="shuoming" type="text" class="formsize140" id="textfield4" value="" />
                        </td>
                        <td align="center" bgcolor="#FFFFFF">
                            <a href="javascript:void(0)" class="addbtn">
                                <img src="/images/addimg.gif" width="48" height="20" /></a> <a href="javascript:void(0)"
                                    class="delbtn">
                                    <img src="/images/delimg.gif" width="48" height="20" /></a>
                        </td>
                    </tr>
            </asp:PlaceHolder>
            <asp:Repeater ID="rptlist" runat="server">
                <ItemTemplate>
                        <tr>
                            <td align="center" bgcolor="#FFFFFF">
                                <input type="hidden" name="txtjiageid" value="<%#Eval("JiaGeId") %>" />
                                <select name="ddlfangxing" id="select1" class="inputselect" errmsg="请选择房型!" valid="required">
                                    <%#getOpts(Eval("FangXingId").ToString(), 3)%>
                                </select>
                            </td>                            
                            <td align="center" bgcolor="#FFFFFF">
                                <input name="louceng" type="text" class="formsize50" value="<%# Eval("LouCeng") %>" />
                            </td>
                            <td height="28" align="center" bgcolor="#FFFFFF">
                                <select name="ddlleixing" id="select4" class="inputselect" errmsg="请选择人员类型!" valid="required">
                                    <%#getOpts(Eval("BinKeLeiXingId").ToString(), 4)%>
                                </select>
                            </td>
                            <td align="center" bgcolor="#FFFFFF">
                                <input name="jiage" type="text" class="formsize50" value="<%#Eval("JiaGe1","{0:F2}") %>" />
                            </td>
                            <td align="center" bgcolor="#FFFFFF">
                                <input name="renshu" type="text" class="formsize50" value="<%# Eval("RongNaRenShu") %>" />
                            </td>
                            <td align="center" bgcolor="#FFFFFF">
                                <input name="fangcha" type="text" class="formsize50" value="<%#Eval("FangCha","{0:F2}") %>" />
                            </td>                            
                            <td align="center" bgcolor="#FFFFFF">
                                <input name="shuoming" type="text" class="formsize140" id="textfield4" value="<%# Eval("ShuoMing") %>" />
                            </td>
                            <td align="center" bgcolor="#FFFFFF">
                                <a href="javascript:void(0)" class="addbtn">
                                    <img src="/images/addimg.gif" width="48" height="20" /></a> <a href="javascript:void(0)"
                                        class="delbtn">
                                        <img src="/images/delimg.gif" width="48" height="20" /></a>
                            </td>
                        </tr>
                </ItemTemplate>
            </asp:Repeater>
        </table>
        <div class="alertbox-btn">
            <a id="btnSave" href="javascript:;" hidefocus="true"><s class="baochun"></s>保 存</a></div>
    </div>
    <input type="hidden" name="txtriqiid" id="txtriqiid" />
    </form>

    <script type="text/javascript">
        var pageDataJs = {
        Params: { sl: '<%=EyouSoft.Common.Utils.GetQueryStringValue("sl") %>', type: '<%=EyouSoft.Common.Utils.GetQueryStringValue("type") %>', id: '<%=EyouSoft.Common.Utils.GetQueryStringValue("id") %>', aid: '<%=EyouSoft.Common.Utils.GetQueryStringValue("aid") %>', piliang: '<%=EyouSoft.Common.Utils.GetQueryStringValue("piliang") %>' },
            BoxWindos: '',
            iframeID: '<%=Request.QueryString["iframeId"]%>', //当前弹窗ID
            pIframeID: '<%=Request.QueryString["pIframeId"]%>', //父级弹窗ID
            UnBindBtn: function() {
                $("#btnSave").html("<s class='baochun'></s>提交中...").unbind("click").css("background-position", "0-57px");
            },
            //按钮绑定事件
            BindBtn: function() {
                $("#btnSave").bind("click").css("background-position", "0-28px").html("<s class='baochun'></s>保 存");
                $("#btnSave").click(function() {
                    var form = $(this).closest("form").get(0);
                    if (ValiDatorForm.validator("form", "parent")) {
                        pageDataJs.Save();
                    } else {
                        return false;
                    }
                });
            },
            Save: function() {

                var validatorResult = ValiDatorForm.validator($("#i_form").get(0), "parent");
                if (!validatorResult) return;

                pageDataJs.UnBindBtn();
                $.newAjax({
                    type: "post",
                    cache: false,
                    url: "/youlun/HangQiJiaGeBoxHY.aspx?save=save&" + $.param(pageDataJs.Params),
                    data: $("#btnSave").closest("form").serialize(),
                    dataType: "json",
                    success: function(ret) {
                        if (ret.result == "1") {
                            parent.tableToolbar._showMsg(ret.msg, function() {

                                window.top.frames[pageDataJs.pIframeID].location.href = window.top.frames[pageDataJs.pIframeID].location.href;
                                parent.Boxy.getIframeDialog(pageDataJs.iframeID).hide()

                            });
                        } else {
                            parent.tableToolbar._showMsg(ret.msg);
                            pageDataJs.BindBtn();
                        }
                    },
                    error: function() {
                        parent.tableToolbar._showMsg("操作失败，请稍后重试！");
                        pageDataJs.BindBtn();
                    }
                });
            },
            F1: function(obj) {
                var _tr1 = $(obj).closest("tr");
                var _tr2 = _tr1.clone(true);

                _tr2.find('select[name="ddlfangxing"]').val(_tr1.find('select[name="ddlfangxing"]').val());
                //_tr2.find('select[name="ddlleixing"]').val(_tr1.find('select[name="ddlleixing"]').val());
                _tr2.find('select[name="ddlleixing"]').val('');
                _tr2.find('input[name="txtjiageid"]').val('');

                _tr1.after(_tr2);
            },
            F2: function(obj) {
                var _tr1 = $(obj).closest("tr");
                var _table = _tr1.closest("table");
                if (_table.find("tr").length == 2) { alert("至少要保留一行价格信息"); return; }
                _tr1.remove();
            }
        };
        $(function() {
            $("#btnSave").click(function() {
                var form = $(this).closest("form").get(0);
                if (ValiDatorForm.validator(form, "parent")) {
                    pageDataJs.Save();
                } else {
                    return false;
                }
            });

            $(".addbtn").click(function() { pageDataJs.F1(this); });
            $(".delbtn").click(function() { pageDataJs.F2(this); });

            if ('<%=Request.QueryString["piliang"] %>' == '1') {
                var _win = parent.Boxy.getIframeWindow(pageDataJs.pIframeID);
                $("#txtriqiid").val(_win.getRiQiId().join(','));
            } else {
                $("#txtriqiid").val('<%=Request.QueryString["aid"] %>');
            }
        });
    </script>

</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HangQiYuKong.aspx.cs" Inherits="EyouSoft.Web.YouLun.HangQiYuKong" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="/css/style.css" rel="stylesheet" type="text/css" />
    <link type="text/css" rel="stylesheet" href="/css/boxynew.css" />

    <script src="/Js/jquery-1.4.4.js" type="text/javascript"></script>

    <script src="/Js/ValiDatorForm.js" type="text/javascript"></script>

    <script src="/Js/table-toolbar.js" type="text/javascript"></script>

    <script src="/Js/kindeditor-4.1/kindeditor-min.js" type="text/javascript"></script>

    <link href="/Css/swfupload/default.css" rel="stylesheet" type="text/css" />

    <script src="/Js/swfupload/swfupload.js" type="text/javascript"></script>

</head>
<body style="background:none;">
    <form id="form1" runat="server">
    <div class="alertbox-outbox02">
        <table width="99%" cellspacing="0" cellpadding="0" bgcolor="#e9f4f9" align="center"
            style="margin: 0 auto">
            <tbody>
                <tr>
                    <td height="28" align="right" style="width:50px;">
                        预控人数
                    </td>
                    <td height="28" align="left">
                        <input name="txtRS" id="txtRS" value="" class="inputtext formsize180" valid="required|isInt"
                            errmsg="请输入预控人数！|输入文字格式不正确！" runat="server" />
                    </td>
                </tr>
            </tbody>
        </table>
        <div style="text-align: center;" class="alertbox-btn">
            <a href="javascript:;" id="btnSave"><s class="baochun"></s>保 存</a> <a hidefocus="true"
                href="javascript:" onclick="parent.Boxy.getIframeDialog('<%=Request.QueryString["iframeId"]%>').hide(); return false;">
                <s class="chongzhi"></s>关 闭</a>
        </div>
    </div>
    <input type="hidden" id="txtriqi" name="txtriqi" />
    </form>

    <script type="text/javascript">
        var pageDataJS = {
        Params: { sl: '<%=EyouSoft.Common.Utils.GetQueryStringValue("sl") %>', aid: '<%=EyouSoft.Common.Utils.GetQueryStringValue("aid") %>', id: '<%=EyouSoft.Common.Utils.GetQueryStringValue("id") %>', dotype: '<%=EyouSoft.Common.Utils.GetQueryStringValue("dotype") %>', riqiid: '<%=EyouSoft.Common.Utils.GetQueryStringValue("riqiid") %>', piliang: '<%=EyouSoft.Common.Utils.GetQueryStringValue("piliang") %>' },
            BoxWindos: '',
            iframeID: '<%=Request.QueryString["iframeId"]%>', //当前弹窗ID
            pIframeID: '<%=Request.QueryString["pIframeId"]%>'//父级弹窗ID
        };
        $(function() {
            $("#btnSave").click(function() {
                if (ValiDatorForm.validator("form", "parent")) {

                    $("#btnSave").unbind().text("正在保存....");

                    $.newAjax({
                        type: "post",
                        cache: false,
                        url: "/youlun/HangQiYuKong.aspx?save=save&" + $.param(pageDataJS.Params),
                        data: $("#btnSave").closest("form").serialize(),
                        dataType: "json",
                        success: function(ret) {
                            if (ret.result == "1") {
                                parent.tableToolbar._showMsg(ret.msg, function() {
                                    window.top.frames[pageDataJS.pIframeID].location.href = window.top.frames[pageDataJS.pIframeID].location.href;
                                    parent.Boxy.getIframeDialog(pageDataJS.iframeID).hide()
                                });
                            } else {
                                parent.tableToolbar._showMsg(ret.msg);
                            }
                        }
                    });


                    //                   if (pageDataJS.pIframeID) {
                    //                       $("#" + pageDataJS.Params.aid, window.top.frames[pageDataJS.pIframeID].document).closest("td").attr("class", "Xdays on");
                    //                       $("#" + pageDataJS.Params.aid, window.top.frames[pageDataJS.pIframeID].document).html("取消").attr("class", "quxiao");
                    //                       $("#ContactBox", window.top.frames[pageDataJS.pIframeID].document).append('<div id="' + pageDataJS.Params.aid + '"><input type="hidden" name="YuKongRi" value=' + pageDataJS.Params.aid + '><input type="hidden" name="YuKongShu" value=' + $("#txtRS").val() + '></div>');
                    //                   }
                    //                   else {
                    //                       $("#" + pageDataJS.Params.aid, window.top.document).closest("td").attr("class", "Xdays on");
                    //                       $("#ContactBox", window.top.document).append('<div id="' + pageDataJS.Params.aid + '"><input type="hidden" name="YuKongRi" value=' + pageDataJS.Params.aid + '><input type="hidden" name="YuKongShu" value=' + $("#txtRS").val() + '></div>');

                    //                    }

                    //                    parent.Boxy.getIframeDialog(pageDataJS.iframeID).hide()
                }
            });

            if ('<%=Request.QueryString["piliang"] %>' == '1') {
                var _win = parent.Boxy.getIframeWindow(pageDataJS.pIframeID);
                $("#txtriqi").val(_win.getRiQi().join(','));
            } else {
                $("#txtriqi").val('<%=Request.QueryString["aid"] %>');
            }
        });
    </script>

</body>
</html>

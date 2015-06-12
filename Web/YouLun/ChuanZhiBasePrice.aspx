<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChuanZhiBasePrice.aspx.cs" Inherits="EyouSoft.Web.YouLun.ChuanZhiBasePrice" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="/css/style.css" rel="stylesheet" type="text/css" />
    <link type="text/css" rel="stylesheet" href="/css/boxynew.css" />

    <script src="/Js/jquery-1.4.4.js" type="text/javascript"></script>

    <script src="/Js/ValiDatorForm.js" type="text/javascript"></script>

    <script src="/Js/table-toolbar.js" type="text/javascript"></script>

    <link href="/Css/swfupload/default.css" rel="stylesheet" type="text/css" />

    <script src="/Js/swfupload/swfupload.js" type="text/javascript"></script>

    <script src="/Js/kindeditor-4.1/kindeditor-min.js" type="text/javascript"></script>

    <script src="/Js/jquery.blockUI.js" type="text/javascript"></script>

    <style type="text/css">
        #tabList th
        {
            border: 1px solid #46ABDC;
        }
        #tabList td
        {
            border: 1px solid #9AD2ED;
        }
    </style>
</head>
<body style="background: none;">
    <form id="form1" runat="server">
    <div class="alertbox-outbox02">
        <table width="100%" style="margin-top: 5px; margin-bottom: 5px;" id="tabList">
            <tbody>
            <tr>
            <th height="30">国籍</th>
            <th>航线</th>
            <th>价格</th>
            <th>操作</th>
            </tr>
            <asp:PlaceHolder ID="PlaceHolder1" runat="server">
            <tr class="tempRow">
                <td align="center">
                <input type="hidden" name="hidid" value="" />
                            <select name="ddlguoji" id="select2" class="inputselect" errmsg="请选择国籍!" valid="required">
                                <%=getOpts("",2)%>
                            </select>
                </td>
                <td align="center">
                            <select name="ddlhangxian" id="select1" class="inputselect" errmsg="请选择航线!" valid="required">
                                <%=getOpts("",0)%>
                            </select>
                </td>
                <td align="center">
                            <input name="zhanchuang" type="text" class="formsize50" id="textfield5" value="" valid="isMoney" errmsg="请输入金额!" />
                </td>
                <td align="center"><a class="addbtn" href="javascript:void(0)">
                        <img width="48" height="20" src="/images/addimg.gif"></a> <a class="delbtn" href="javascript:void(0)">
                            <img width="48" height="20" src="/images/delimg.gif"></a>
                </td>
            </tr>
            </asp:PlaceHolder>
            <asp:Repeater ID="rpt" runat="server">
            <ItemTemplate>
            <tr class="tempRow">
                <td align="center">
                <input type="hidden" name="hidid" value="<%#Eval("id")%>" />
                            <select name="ddlguoji" id="select2" class="inputselect" errmsg="请选择国籍!" valid="required">
                                <%#getOpts(Eval("CountryId").ToString(), 2)%>
                            </select>
                </td>
                <td align="center">
                            <select name="ddlhangxian" id="select3" class="inputselect" errmsg="请选择航线!" valid="required">
                                <%#getOpts(Eval("hangxianid").ToString(), 0)%>
                            </select>
                </td>
                <td align="center">
                            <input name="zhanchuang" type="text" class="formsize50" id="textfield5" value="<%#Eval("BasePrice","{0:F2}")%>" valid="isMoney" errmsg="请输入金额!" />
                </td>
                 <td align="center"><a class="addbtn" href="javascript:void(0)">
                        <img width="48" height="20" src="/images/addimg.gif"></a> <a class="delbtn" href="javascript:void(0)">
                            <img width="48" height="20" src="/images/delimg.gif"></a>
                </td>
            </tr>
            </ItemTemplate>
            </asp:Repeater>
            </tbody>
        </table>
        <div style="text-align: center;" class="alertbox-btn">
            <a href="javascript:;" id="btnSave"><s class="baochun"></s>保 存</a> <a hidefocus="true"
                href="javascript:" onclick="parent.Boxy.getIframeDialog('<%=Request.QueryString["iframeId"]%>').hide(); return false;">
                <s class="chongzhi"></s>关 闭</a>
        </div>
    </div>
    <!--列表结束-->
    </form>

    <script type="text/javascript">
        var pageDataJs = {
            Params: { sl: '<%=EyouSoft.Common.Utils.GetQueryStringValue("sl") %>', type: '<%=EyouSoft.Common.Utils.GetQueryStringValue("type") %>', id: '<%=EyouSoft.Common.Utils.GetQueryStringValue("id") %>' },
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
                pageDataJs.UnBindBtn();
                $.newAjax({
                    type: "post",
                    cache: false,
                    url: "/youlun/ChuanZhiBasePrice.aspx?save=save&" + $.param(pageDataJs.Params),
                    data: $("#btnSave").closest("form").serialize(),
                    dataType: "json",
                    success: function(ret) {
                        if (ret.result == "1") {
                            parent.tableToolbar._showMsg(ret.msg, function() { parent.window.location.href = parent.window.location.href });
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
            }
        };

        $(function() {
            $("#tabList").autoAdd()
            $("#btnSave").click(function() {
                var form = $(this).closest("form").get(0);
                if (ValiDatorForm.validator(form, "parent")) {
                    pageDataJs.Save();
                } else {
                    return false;
                }
            }); //
        })
    </script>

</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChuanZhiAdd.aspx.cs" Inherits="EyouSoft.Web.YouLun.ChuanZhiAdd"
    ValidateRequest="false" %>

<%@ Register Src="~/YouLun/WUC/wucupload.ascx" TagName="wucupload" TagPrefix="uc1" %>
<%@ Register Src="../UserControl/YLCompanySelect.ascx" TagName="YLCompanySelect"
    TagPrefix="uc2" %>
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

    <script src="/Js/datepicker/WdatePicker.js" type="text/javascript"></script>

</head>
<body style="background: none;">
    <form id="form1" runat="server">
    <div class="alertbox-outbox02">
        <table width="99%" cellspacing="0" cellpadding="0" bgcolor="#e9f4f9" align="center"
            style="margin: 0 auto">
            <tbody>
                <tr>
                    <th width="150px" height="28" align="right" class="alertboxTableT" bgcolor="#b7e0f3">
                        游轮公司：
                    </th>
                    <td width="30%" height="28" align="left">
                        <uc2:YLCompanySelect ID="YLCompanySelect1" runat="server" CallBackFun="initXiLie" />
                    </td>
                    <th width="150px" height="28" align="right" class="alertboxTableT" bgcolor="#b7e0f3">
                        游轮系列：
                    </th>
                    <td height="28" align="left">
                        <select id="txtXiLie" name="txtXiLie" class="inputselect" valid="required" i_val="<%=XiLieId %>"
                            errmsg="请选择游轮系列！">
                            <option value="">请选择</option>
                        </select>
                    </td>
                </tr>
                <tr>
                    <th height="28" align="right" class="alertboxTableT" bgcolor="#b7e0f3">
                        船只名称：
                    </th>
                    <td width="35%" height="28" align="left">
                        <asp:TextBox ID="txtchuanzhimingcheng" CssClass=" inputtext formsize180" runat="server"
                            valid="required" errmsg="船只名称不可为空！"></asp:TextBox>
                    </td>
                    <th height="28" align="right" class="alertboxTableT" bgcolor="#b7e0f3">
                        英文名称：
                    </th>
                    <td height="28" align="left">
                        <asp:TextBox ID="txtyingwenmingcheng" CssClass=" inputtext formsize180" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th height="28" align="right" class="alertboxTableT" bgcolor="#b7e0f3">
                        星级：
                    </th>
                    <td width="35%" height="28" align="left">
                        <asp:DropDownList ID="ddlxingji" runat="server" CssClass="inputselect">
                        </asp:DropDownList>
                    </td>
                    <th height="28" align="right" class="alertboxTableT" bgcolor="#b7e0f3">
                        总吨位(吨)：
                    </th>
                    <td height="28" align="left">
                        <asp:TextBox ID="txtdunwei" CssClass=" inputtext formsize180" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th height="28" align="right" class="alertboxTableT" bgcolor="#b7e0f3">
                        下水日期：
                    </th>
                    <td width="35%" height="28" align="left">
                        <asp:TextBox ID="txtxiashuiriqi" CssClass=" inputtext formsize180" runat="server">如2013年5月</asp:TextBox>
                    </td>
                    <th height="28" align="right" class="alertboxTableT" bgcolor="#b7e0f3">
                        装修日期：
                    </th>
                    <td height="28" align="left">
                        <asp:TextBox ID="txtzhuangxiuriqi" CssClass=" inputtext formsize180" runat="server">如2013年5月</asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th height="28" align="right" class="alertboxTableT" bgcolor="#b7e0f3">
                        载客量(人)：
                    </th>
                    <td width="35%" height="28" align="left">
                        <asp:TextBox ID="txtzaikeliang" CssClass=" inputtext formsize180" runat="server"></asp:TextBox>
                    </td>
                    <th height="28" align="right" class="alertboxTableT" bgcolor="#b7e0f3">
                        甲板楼层(层)：
                    </th>
                    <td height="28" align="left">
                        <asp:TextBox ID="txtjiaban" CssClass=" inputtext formsize180" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th height="28" align="right" class="alertboxTableT" bgcolor="#b7e0f3">
                        船员(人)：
                    </th>
                    <td width="35%" height="28" align="left">
                        <asp:TextBox ID="txtchuanyuan" CssClass=" inputtext formsize180" runat="server"></asp:TextBox>
                    </td>
                    <th height="28" align="right" class="alertboxTableT" bgcolor="#b7e0f3">
                        长度(米)：
                    </th>
                    <td height="28" align="left">
                        <asp:TextBox ID="txtchangdu" CssClass=" inputtext formsize180" runat="server"></asp:TextBox>不需要填写单位
                    </td>
                </tr>
                <tr>
                    <th height="28" align="right" class="alertboxTableT" bgcolor="#b7e0f3">
                        宽度(米)：
                    </th>
                    <td width="35%" height="28" align="left">
                        <asp:TextBox ID="txtkuandu" CssClass=" inputtext formsize180" runat="server"></asp:TextBox>不需要填写单位
                    </td>
                    <th height="28" align="right" class="alertboxTableT" bgcolor="#b7e0f3">
                        船籍：
                    </th>
                    <td height="28" align="left">
                        <asp:TextBox ID="txtchuanji" CssClass=" inputtext formsize180" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th height="28" align="right" class="alertboxTableT" bgcolor="#b7e0f3">
                        吃水(米)：
                    </th>
                    <td width="35%" height="28" align="left">
                        <asp:TextBox ID="txtChiShui" CssClass=" inputtext formsize180" runat="server"></asp:TextBox>
                    </td>
                    <th height="28" align="right" class="alertboxTableT" bgcolor="#b7e0f3">
                        客房数量(间)：
                    </th>
                    <td height="28" align="left">
                        <asp:TextBox ID="txtKeFangShuLiang" CssClass=" inputtext formsize180" runat="server"></asp:TextBox>
                    </td>
                </tr>
                
                <tr>
                    <th height="28" align="right" class="alertboxTableT" bgcolor="#b7e0f3">
                        最高航速(节)：
                    </th>
                    <td width="35%" height="28" align="left">
                        <asp:TextBox ID="txtzuigaohangsu" CssClass=" inputtext formsize180" runat="server"></asp:TextBox>不需要填写单位
                    </td>
                    <th height="28" align="right" class="alertboxTableT" bgcolor="#b7e0f3">
                        服务台电话：
                    </th>
                    <td width="35%" height="28" align="left">
                        <asp:TextBox ID="txtfuwutai" CssClass=" inputtext formsize180" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT" bgcolor="#b7e0f3">
                        平面图：
                    </th>
                    <td height="28" align="left" bgcolor="#e0e9ef" colspan="3">
                        <uc1:wucupload runat="server" ID="upload1" IsUploadMore="true" IsUploadSelf="true"
                            FileTypes="*.jpg;*.gif;*.jpeg;*.png" YangShi="1" />
                    </td>
                </tr>
                <tr>
                    <th height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT" bgcolor="#b7e0f3">
                        船只图片：
                    </th>
                    <td height="28" align="left" bgcolor="#e0e9ef" colspan="3">
                        <uc1:wucupload runat="server" ID="upload2" IsUploadMore="true" IsUploadSelf="true"
                            FileTypes="*.jpg;*.gif;*.jpeg;*.png" YangShi="1" />
                    </td>
                </tr>
                <tr>
                    <th height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                        简要介绍：
                    </th>
                    <td height="28" align="left" bgcolor="#e0e9ef" colspan="3">
                        <asp:TextBox runat="server" ID="txtJieShao" Height="65" CssClass="inputtext formsize600"
                            TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                        详细介绍：
                    </th>
                    <td height="28" align="left" bgcolor="#e0e9ef" colspan="3">
                        <span id="spanPlanContent" style="display: inline-block;">
                            <asp:TextBox ID="txtXXJieShao" CssClass="inputtext formsize450" runat="server" TextMode="MultiLine"
                                Width="600px" Height="65"></asp:TextBox></span>
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
                KEditer.sync();
                pageDataJs.UnBindBtn();
                $.newAjax({
                    type: "post",
                    cache: false,
                    url: "/youlun/ChuanZhiAdd.aspx?save=save&" + $.param(pageDataJs.Params),
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
            },
            CreateEdit: function() {
                var _keoptions = { items: keSimpleYL, height: "120px", width: "650px" }
                //创建行程编辑器
                KEditer.init('<%=txtXXJieShao.ClientID %>', _keoptions);
            },
            //删除附件
            RemoveVisaFile: function(obj) {
                $(obj).parent().remove();
            }
        };
        $(function() {
            KEditer["UPLOAD_P1"] = "YL";
            pageDataJs.CreateEdit();
            initXiLie();
            $("#btnSave").click(function() {
                var form = $(this).closest("form").get(0);
                if (ValiDatorForm.validator(form, "parent")) {
                    pageDataJs.Save();
                } else {
                    return false;
                }
            });
        });


        function initXiLie() {
            if ($("#<%=YLCompanySelect1.CompIDClient %>").val().length < 36) return;
            $.ajax({
                url: "chuanzhiadd.aspx?sl=96&getXL=1&gongsi=" + $("#<%=YLCompanySelect1.CompIDClient %>").val(),
                dataType: "json",
                success: function(ret) {
                    if (ret.result == "1") {
                        $("#txtXiLie").empty();
                        $("#txtXiLie").append(ret.obj);
                        $("#txtXiLie").val($("#txtXiLie").attr("i_val"));
                    }
                }
            });
        }
    </script>

</body>
</html>

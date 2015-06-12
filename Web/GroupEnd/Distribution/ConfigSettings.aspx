<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ConfigSettings.aspx.cs"
    Inherits="Web.GroupEnd.Distribution.ConfigSettings" %>

<%@ Register Src="~/UserControl/HeadDistributorControl.ascx" TagName="HeadDistributorControl"
    TagPrefix="uc1" %>
<%@ Register Src="~/UserControl/UploadControl.ascx" TagName="UploadControl" TagPrefix="uc3" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>分销商系统设置</title>
    <link type="text/css" rel="Stylesheet" href="/Css/fx_style.css" />
    <link type="text/css" rel="Stylesheet" href="/Css/boxynew.css" />
    <link type="text/css" rel="Stylesheet" href="/Css/swfupload/default.css" />

    <script src="/Js/jquery-1.4.4.js" type="text/javascript"></script>

    <script src="/Js/jquery.blockUI.js" type="text/javascript"></script>

    <script src="/Js/table-toolbar.js" type="text/javascript"></script>

    <script src="/Js/swfupload/swfupload.js" type="text/javascript"></script>

</head>
<body style="background: 0 none;">
    <uc1:HeadDistributorControl ID="HeadDistributorControl1" runat="server" SystemClass="default xtglicon" />
    <div class="list-main">
        <div class="list-maincontent">
            <div class="linebox-menu">
                <a href="PasswordSettings.aspx?sl=<%=EyouSoft.Common.Utils.GetQueryStringValue("sl") %>">
                    密码修改</a> <a href="CompanySettings.aspx?sl=<%=EyouSoft.Common.Utils.GetQueryStringValue("sl") %>">
                        公司信息</a> <a href="ConfigSettings.aspx?sl=<%=EyouSoft.Common.Utils.GetQueryStringValue("sl") %>"
                            class="linebox-menudefault">配置管理</a>
            </div>
            <div class="hr_10">
            </div>
            <div class="listtablebox">
                <form id="frmupload">
                <table width="100%" cellspacing="0" cellpadding="0" border="0">
                    <tbody>
                        <tr>
                            <td width="40%" bgcolor="#DCEFF3" align="right">
                                打印页眉：
                            </td>
                            <td width="60%" align="left">
                                <uc3:UploadControl runat="server" ID="UploadControl1" />
                                <br />
                                <asp:Label runat="server" ID="lblPrintHeader"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td bgcolor="#DCEFF3" align="right">
                                打印页脚：
                            </td>
                            <td align="left">
                                <uc3:UploadControl runat="server" ID="UploadControl2" />
                                <br />
                                <asp:Label runat="server" ID="lblPrintFooter"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td bgcolor="#DCEFF3" align="right">
                                打印模板：
                            </td>
                            <td align="left">
                                <uc3:UploadControl runat="server" ID="UploadControl3" />
                                <br />
                                <asp:Label runat="server" ID="lblPrintTemplates"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td bgcolor="#DCEFF3" align="right">
                                公司公章：
                            </td>
                            <td align="left">
                                <uc3:UploadControl runat="server" ID="UploadControl4" />
                                <br />
                                <asp:Label runat="server" ID="lblSeal"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" colspan="2">
                                <div class="mainbox cunline fixed">
                                    <ul>
                                        <li class="cun-cy"><a href="javascript:void(0)" id="btnSave">保存</a></li>
                                        <li class="quxiao-cy"><a href="javascript:void(0)" id="btnCancle">取消</a></li>
                                    </ul>
                                </div>
                            </td>
                        </tr>
                    </tbody>
                </table>
                </form>
            </div>
            <div class="hr_10">
            </div>
        </div>
    </div>
    <!-- InstanceEndEditable -->
</body>
</html>

<script type="text/javascript">
    var ConfigSettings = {
        Data: {
            sl: '<%=EyouSoft.Common.Utils.GetQueryStringValue("sl") %>',
            Type: "Save"
        },
        Save: function() {
            $.newAjax({
                url: "/GroupEnd/Distribution/ConfigSettings.aspx?" + $.param(ConfigSettings.Data),
                type: "post",
                data: $("#frmupload").serialize(),
                dataType: "json",
                success: function(back) {
                    if (back.result == "1") {
                        tableToolbar._showMsg(back.msg, function() {
                            window.location.reload();
                        });
                    }
                    else {
                        tableToolbar._showMsg(back.msg);
                    }
                },
                error: function() {
                    tableToolbar._showMsg("服务器忙！");
                }
            });
        },
        Cancle: function() {
            window.location.reload();
        },
        DelFile: function(obj) {
            $(obj).parent().remove();
        },
        PageInit: function() {
            $("#btnSave").click(function() {
                ConfigSettings.Save();
            });
            $("#btnCancle").click(function() {
                ConfigSettings.Cancle();
            });

        }
    };


    $(function() {
        ConfigSettings.PageInit();
    });
</script>


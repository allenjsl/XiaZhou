<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PasswordSettings.aspx.cs"
    Inherits="Web.GroupEnd.Distribution.PasswordSettings" %>

<%@ Register Src="~/UserControl/HeadDistributorControl.ascx" TagName="HeadDistributorControl"
    TagPrefix="uc1" %>
<%@ Register Src="~/UserControl/DistributorNotice.ascx" TagName="Notice" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>分销商密码修改</title>
    <link type="text/css" rel="Stylesheet" href="/Css/fx_style.css" />
    <link type="text/css" rel="Stylesheet" href="/Css/boxynew.css" />

    <script src="/Js/jquery-1.4.1-vsdoc.js" type="text/javascript"></script>

</head>
<body style="background: 0 none;">
    <uc1:HeadDistributorControl ID="HeadDistributorControl1" runat="server" SystemClass="default xtglicon" />
    <!-- InstanceBeginEditable name="EditRegion3" -->
    <div>
        <div class="list-maincontent">
            <div class="linebox-menu">
                <a href="PasswordSettings.aspx?sl=<%=EyouSoft.Common.Utils.GetQueryStringValue("sl") %>"
                    class="linebox-menudefault">密码修改</a> <a href="CompanySettings.aspx?sl=<%=EyouSoft.Common.Utils.GetQueryStringValue("sl") %>">
                        公司信息</a> <a href="ConfigSettings.aspx?sl=<%=EyouSoft.Common.Utils.GetQueryStringValue("sl") %>">
                            配置管理</a>
            </div>
            <div class="hr_10">
            </div>
            <div class="listtablebox">
                <form id="frmpass">
                <table width="100%" border="0" cellpadding="0" cellspacing="0">
                    <tr>
                        <td width="40%" align="right" bgcolor="#DCEFF3">
                            原密码：
                        </td>
                        <td width="60%" align="left">
                            <input id="txtOldPwd" name="txtOldPwd" type="password" class="searchInput size170"
                                value="" valid="required" errmsg="原始密码不能为空！" />
                            <span class="fontbsize12">*</span>
                        </td>
                    </tr>
                    <tr>
                        <td width="40%" align="right" bgcolor="#DCEFF3">
                            新密码：
                        </td>
                        <td width="60%" align="left">
                            <input id="txtNewPwd" name="txtNewPwd" type="password" class="searchInput size170"
                                value="" valid="required" errmsg="新密码不能为空！" />
                            <span class="fontbsize12">*</span>
                        </td>
                    </tr>
                    <tr>
                        <td width="40%" align="right" bgcolor="#DCEFF3">
                            确认新密码：
                        </td>
                        <td width="60%" align="left">
                            <input id="txtSurePwd" name="txtSurePwd" type="password" class="searchInput size170"
                                value="" valid="required|equal" eqaulname="txtNewPwd" errmsg="确认新密码不能为空！|新密码与确认新密码必须一致 ！" />
                            <span class="fontbsize12">*</span>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" align="right">
                            <div class="mainbox cunline fixed">
                                <ul>
                                    <li class="cun-cy"><a href="#" id="btnUpdate">修改密码</a></li>
                                    <li class="quxiao-cy"><a href="#" id="btnCancle">取消</a></li>
                                </ul>
                            </div>
                        </td>
                    </tr>
                </table>
                </form>
            </div>
            <div class="hr_10">
            </div>
        </div>
    </div>
    <!-- InstanceEndEditable -->

    <script src="/Js/jquery.blockUI.js" type="text/javascript"></script>

    <script src="/Js/table-toolbar.js" type="text/javascript"></script>

    <script src="/Js/ValiDatorForm.js" type="text/javascript"></script>

    <script type="text/javascript">
        $(function() {
            //表单验证初始化
            FV_onBlur.initValid($("#frmpass").get(0));

            $("#txtOldPwd,#txtNewPwd,#txtSurePwd").keypress(function(e) {
                if (e.keyCode == 13) {
                    PasswordSettings.Update();
                    return false;
                }
            });
            //取消
            $("#btnCancle").click(function() {
                PasswordSettings.Cancle();
            });

            //提交
            $("#btnUpdate").click(function() {
                if (ValiDatorForm.validator($("#frmpass").get(0), "alert")) {
                    PasswordSettings.Update();
                    return false;
                }
            });

        });


        var PasswordSettings = {
            Data: {
                'sl': 'EyouSoft.Common.Utils.GetQueryStringValue("sl")',
                Type: 'Update'
            },
            Cancle: function() {
                location.reload();
            },
            Update: function() {
                //防止重复提交
                $("#btnUpdate").html("提交中...");
                PasswordSettings.UnBind();
                $.newAjax({
                    type: "post",
                    url: "/GroupEnd/Distribution/PasswordSettings.aspx?" + $.param(PasswordSettings.Data),
                    data: $("#frmpass").serialize(),
                    dataType: "json",
                    success: function(data) {
                        if (data.result == "1") {
                            tableToolbar._showMsg(data.msg, function() {
                                window.location.href = "/logout.aspx";
                            });
                        }
                        else {
                            tableToolbar._showMsg(data.msg);
                            PasswordSettings.Bind();
                        }

                    },
                    error: function() {
                        tableToolbar._showMsg("服务器忙！");
                        PasswordSettings.Bind();
                    }
                });
            },
            Bind: function() {
                var that = $("#btnUpdate");
                that.html("修改密码");
                that.css("cursor", "pointer")
                that.click(function() {
                    if (ValiDatorForm.validator($("#frmpass").get(0), "alert")) {
                        PasswordSettings.Update();
                        return false;
                    }
                });

                $("#btnCancle").click(function() {
                    PasswordSettings.Cancle();
                    return false;
                });
            },
            UnBind: function() {
                $("#btnUpdate").unbind("click").css("cursor", "default");
                $("#btnCancle").unbind("click");
            }
        };

    </script>

</body>
</html>
